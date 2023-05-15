using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using C_FGMS.UI.Helpers;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for FinanceMealAndTransportPage.xaml 
    /// Implements below service providers to interact with database and populate the FinanceMealAndTransportPage.xaml 
    /// </summary>
    /// <author>Brendan Breuss</author>
    public partial class FinanceMealAndTransportPage : System.Windows.Controls.Page
    {
        private readonly GeneralBusinessLogic general_businessLogic;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMealAndTransportProvider _mealAndTransportProvider;
        private readonly IMealAndTransportRatesProvider _mealAndTransportRatesProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private bool errorFlag;

        public FinanceMealAndTransportPage(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _mealAndTransportProvider = _serviceProvider.GetRequiredService<IMealAndTransportProvider>();
            _mealAndTransportRatesProvider = serviceProvider.GetRequiredService<IMealAndTransportRatesProvider>();
            _refreshEventBroker = refreshEventBroker;
            general_businessLogic = new GeneralBusinessLogic();

            _mealAndTransportProvider.DatabaseError += ErrorHandler;
            _mealAndTransportRatesProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            InitializeComponent();

            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshData();
            });

            RefreshData();
        }


        /// <summary>
        /// Error provider for the UserServiceProvider. All functionality to handle
        /// business logic errors for the Users.xaml page are called in this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void ErrorHandler(object sender, ErrorEventArgs e)
        {
            errorFlag = true;
            System.Windows.MessageBox.Show(e.ErrorMessage, "Database Error " + e.ErrorCode, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Function Name: RefreshData
        /// Simply refreshes the entire pages data sources so that if major database change occurs this can be called
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <created>03/12/2023</created>
        private void RefreshData()
        {
            populateMonthComboBox();
            populateYearComboBox();
            populateTable();
            populateMonthlyTotalsMealAndTransport();
            populateYtdTotalsMealAndTransport();
            populateFilterComboBox();
        }

        #region Populate Table Monthly/YTD Totals

        /// <summary>
        /// Function Name: populateTable
        /// Populates the datagrid on the page by getting the selected year and month in the comboboxes
        /// Gets a volunteer list of all those in the current month and year range as well as correponding rates
        /// loops over each volunteer and calculates the mileage and meal total based on the volunteers
        /// num meals and mileage as well as the current rates for the selected month. Then we update the current 
        /// volunteer to have the corect meal value and mileage value totals 
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <created>03/21/2023</created>
        private void populateTable()
        {
            int currentYear = int.Parse(cbobxMealAndTransportSelectYearRange.Text);
            int tempMonthIndex = cbobxMealAndTransportSelectMonth.SelectedIndex;
            int monthNumber = tempMonthIndex + 1;
            double totalMileage = 0.0, totalMeals = 0;
                
            var VolunteerDataMealAndTranport = _mealAndTransportProvider.getAllMealAndTransportDataForSelectedTime(currentYear, monthNumber);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            var rates = _mealAndTransportRatesProvider.getMealAndTransportRatesDataForSelectedTime(int.Parse(cbobxMealAndTransportSelectYearRange.SelectedItem + ""),
               monthNumber);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            if (rates.Count() <=0 )
            {
                txtBoxYearlyMealValue.Text = "$0.00";
                txtBoxCurrentMilageRate.Text = "$0.00/mile";

                foreach (var rate in rates)
                {
                    txtBoxYearlyMealValue.Text = String.Format("{0:C}", Math.Round((rate.mealRate), 2, MidpointRounding.AwayFromZero));
                    txtBoxCurrentMilageRate.Text = String.Format("{0:C}", Math.Round((rate.mileageRate), 2, MidpointRounding.AwayFromZero)) + "/mile";
                    totalMileage = 0;
                    totalMeals = 0;
                }
            }

            if (rates.Count() > 0)
            {
                foreach (var rate in rates)
                {
                    txtBoxYearlyMealValue.Text = String.Format("{0:C}", Math.Round((rate.mealRate), 2, MidpointRounding.AwayFromZero));
                    txtBoxCurrentMilageRate.Text = String.Format("{0:C}", Math.Round((rate.mileageRate), 2, MidpointRounding.AwayFromZero)) + "/mile";
                    totalMileage += rate.mileageRate;
                    totalMeals += rate.mealRate;
                }
            }
            List<MealAndTransportModel> updateVolunteersTable = new List<MealAndTransportModel>();
            foreach (var volunteer in VolunteerDataMealAndTranport)
            {
                volunteer.totalMealCost = 0.0;
                volunteer.totalMileageCost = 0.0;

                if (volunteer.numMeals == null)
                {
                    volunteer.numMeals= 0;
                    volunteer.totalMealCost = 0.0;
                }

                if (volunteer.numMeals != null)
                {
                    volunteer.totalMealCost = System.Math.Ceiling((totalMeals * (double)volunteer.numMeals) * 100) / 100;
                }

                if (volunteer.Mileage == null)
                {
                    volunteer.Mileage = 0;
                    volunteer.totalMileageCost = 0.0;
                }

                if (volunteer.Mileage != null)
                {
                    volunteer.totalMileageCost = System.Math.Ceiling((totalMileage * (double)volunteer.Mileage) * 100) / 100;
                }

                if(volunteer.numBusRides== null)
                {
                    volunteer.numBusRides= 0;
                }
                updateVolunteersTable.Add(volunteer);
                
            } 
            dtgMealAndTransport.ItemsSource = updateVolunteersTable.ToList();


        }

        /// <summary>
        /// Function Name: populateMonthlyTotalsMealAndTransport
        /// Loops over the values of each volunteerin the datgrid for the current selected month and year Then populates
        /// the monthly totals with their corresponding columns summed up
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <created>03/22/2023</created>
        private void populateMonthlyTotalsMealAndTransport()
        {
            string strNumMealsTotal = "0";
            string strtotalMealValue = "0";
            string strNumBusRides = "0";
            string strMileage = "0";
            string strMileageTotal = "0";
            double tempMileageCost = 0, tempMealCost = 0;
            var tableDate = dtgMealAndTransport.ItemsSource;

            if (tableDate == null)
                return;

            foreach (MealAndTransportModel model in tableDate)
            {
                strNumMealsTotal = Int32.Parse(strNumMealsTotal) + model.numMeals + "";
                strtotalMealValue = Double.Parse(strtotalMealValue) + model.totalMealCost + "";
                strNumBusRides = Int32.Parse(strNumBusRides) + model.numBusRides + "";
                strMileage = Decimal.Parse(strMileage) + model.Mileage + "";
                strMileageTotal = Double.Parse(strMileageTotal) + model.totalMileageCost + "";
                if (model.totalMileageCost != null)
                    tempMileageCost += (double)model.totalMileageCost;
                if (model.totalMealCost != null)
                    tempMealCost += (double)model.totalMealCost;

            }

            lblMealNumMeals.Content = strNumMealsTotal;
            lblMealTotalMealValue.Content = String.Format("{0:C}", tempMealCost);
            lblMealBusRides.Content = strNumBusRides;
            lblMealMileage.Content = strMileage;
            lblMealMileageValue.Content = String.Format("{0:C}", tempMileageCost);
        }

        /// <summary>
        /// Function Name: populateYtdTotalsMealAndTransport
        /// This method is to update the ytd labels for a selected year and month
        /// We initialize all values to zero then get all the volunteers and rates for every month leading 
        /// up to the selected year. Ex: March is selected so all volunteers and rates from Jan-Mar will be considered
        /// We will sum all of the num meals, num bus rides, mileages, mileageTotal and Meal value total
        /// for each month to the selected month then populate the ytd labels with the summed values. 
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <created>03/23/2023</created>
        private void populateYtdTotalsMealAndTransport()
        {
            string strNumMealsTotal = "0";
            string strtotalMealValue = "0";
            string strNumBusRides = "0";
            string strMileage = "0";
            string strMileageTotal = "0";
            double totalMileage = 0, totalMeals = 0, tempMileageRate = 0, tempMealsRate = 0;

            int currentYear = int.Parse(cbobxMealAndTransportSelectYearRange.Text);
            int tempMonthIndex = cbobxMealAndTransportSelectMonth.SelectedIndex + 1;
            int tempNumMeals = 0, tempNumBusRides = 0;
            decimal tempMileage = 0;


            //loops over each month up to the current selected month
            for (int i = 0; i < tempMonthIndex; i++)
            {
                var VolunteerDataMealAndTranport = _mealAndTransportProvider.getAllMealAndTransportDataForSelectedTime(currentYear, (i + 1));
                if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
                var rates = _mealAndTransportRatesProvider.getMealAndTransportRatesDataForSelectedTime(currentYear, (i + 1));
                if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
                //chceks rates
                if (rates.Count() <= 0)
                {
                    tempMealsRate = 0;
                    tempMileageRate = 0;
                }
                else if (rates.Count() > 0)
                {
                    foreach (var rate in rates)
                    {
                        tempMealsRate = rate.mealRate;
                        tempMileageRate = rate.mileageRate;
                    }
                }

                //loops over all volunteers for given month 
                foreach (var volunteer in VolunteerDataMealAndTranport)
                {
                    if (volunteer.numMeals != null)
                    {
                        tempNumMeals += (int)volunteer.numMeals;
                        totalMeals += (tempMealsRate * (double)volunteer.numMeals);
                    }
                    if (volunteer.numMeals == null)
                    {
                        volunteer.numMeals = 0;
                        tempNumMeals += (int)volunteer.numMeals;
                        totalMeals += (tempMealsRate * (double)volunteer.numMeals);
                    }

                    if (volunteer.Mileage != null)
                    {
                        tempMileage += (decimal)volunteer.Mileage;
                        totalMileage += (tempMileageRate * (double)volunteer.Mileage);
                    }
                    if (volunteer.Mileage == null)
                    {
                        volunteer.Mileage = 0;
                        tempMileage += (decimal)volunteer.Mileage;
                        totalMileage += (tempMileageRate * (double)volunteer.Mileage);
                    }

                    if (volunteer.numBusRides != null)
                    {
                        tempNumBusRides += (int)volunteer.numBusRides;
                    }

                    if (volunteer.numBusRides == null)
                    {
                        volunteer.numBusRides = 0;
                        tempNumBusRides += (int)volunteer.numBusRides;
                    }

                }

                totalMeals = System.Math.Ceiling(totalMeals * 100) / 100;
                totalMileage = System.Math.Ceiling(totalMileage * 100) / 100;

            }
            //populates the labels with the above values
            strMileageTotal = String.Format("{0:C}", totalMileage);
            strtotalMealValue = String.Format("{0:C}", totalMeals);
            strNumMealsTotal = tempNumMeals + "";
            strNumBusRides = tempNumBusRides + "";
            strMileage = tempMileage + "";

            lblYTDMealNumMeals.Content = strNumMealsTotal;
            lblYTDMealBusRides.Content = strNumBusRides;
            lblYTDMealMileage.Content = strMileage;
            lblYTDMealTotalMealValue.Content = strtotalMealValue;
            lblYTDMealMileageValue.Content = strMileageTotal;
        }

        #endregion

        #region Populate Comboboxes

        /// <summary>
        /// Function Name: populateFilterComboBox
        /// Method uses getAllMealAndTransportVolunteersForSelectedTime database function to get all volunteer
        /// names and Tuids for the selected month and year then populates the combobox with all the volunteer names
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <Created>03/12/2023</Created>
        private void populateFilterComboBox()
        {
            cbobxFilterName.Items.Clear();
            int currentYear = int.Parse(cbobxMealAndTransportSelectYearRange.Text);
            int tempMonthIndex = cbobxMealAndTransportSelectMonth.SelectedIndex + 1;
            var volunteerNames = _mealAndTransportProvider.getAllMealAndTransportVolunteersForSelectedTime(currentYear, tempMonthIndex);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }

            foreach (var Volunteer in volunteerNames)
            {
                cbobxFilterName.Items.Add(Volunteer.strVolunteerName);
            }
        }

        /// <summary>
        /// Function Name: populateMonthComboBox
        /// uses the general business logic get months method to get all possible months, then make the current 
        /// month of the system the selected month so user does not have to select it everytime
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <Created>03/12/2023</Created>
        private void populateMonthComboBox()
        {
            cbobxMealAndTransportSelectMonth.ItemsSource = general_businessLogic.GetMonths();
            cbobxMealAndTransportSelectMonth.SelectedIndex= general_businessLogic.GetCurrentMonthIndex(); 

        }

        /// <summary>
        /// Function Name: populateYearComboBox
        /// uses the _mealAndTransportProvider.getMealAndTransportYears() method to get all the years that
        /// contains a meal and transport record then populates the combo box with these year. If the current year
        /// is not found in these years then add it to the combobox in order to make new records if year changes
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <Created>03/15/2023</Created>
        private void populateYearComboBox()
        {
            cbobxMealAndTransportSelectYearRange.Items.Clear();
            int tempYear = 0;
            foreach(int year in _mealAndTransportProvider.getMealAndTransportYears())
            {
                if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
                cbobxMealAndTransportSelectYearRange.Items.Add(year);
                tempYear = year;
            }
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }


            if (tempYear != DateTime.Now.Year)
            {
                tempYear = DateTime.Now.Year;
                cbobxMealAndTransportSelectYearRange.Items.Add(tempYear);
            }

            cbobxMealAndTransportSelectYearRange.SelectedItem = tempYear;
        }

        #endregion


        #region Selection Changed Events
        /// <summary>
        /// Function Name: cbobxFilterName_SelectionChanged
        /// If the selection in the dropdown changes look for the currently selected volunteer data in the database
        /// and get the current rates for the selected month. Then populate the datagrid with just the currently 
        /// selected volunteers info to help user find only desired volunteers info. If no volunteer selected, 
        /// then simply populate the table with all data for the selected year and month
        /// </summary>
        /// <param name="sender"> the filter name combobox</param>
        /// <param name="e">The selection in the combobox being changed event</param>
        /// <author>Brendan Breuss</author>
        /// <created>3/24/2023</created>
        private void cbobxFilterName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentYear = int.Parse(cbobxMealAndTransportSelectYearRange.Text);
            int tempMonthIndex = cbobxMealAndTransportSelectMonth.SelectedIndex + 1;
            double totalMileage = 0, totalMeals = 0;
            string? volunteerToSearch = "Test";

            if (cbobxFilterName.SelectedValue == null || cbobxFilterName.SelectedItem.ToString() == null)
            {
                try
                {
                    populateTable();
                }
                catch(RefreshDataCustomException rdce)
                {
                    return;
                }
            }
            else
            {
                volunteerToSearch = cbobxFilterName.SelectedItem.ToString();
                if(volunteerToSearch == null)
                {
                    volunteerToSearch = "Test";
                }
                var VolunteerDataMealAndTranport = _mealAndTransportProvider.getAllMealAndTransportDataForSelectedVolunteer(currentYear, tempMonthIndex, volunteerToSearch);
                if (errorFlag) { errorFlag = false; return; }

                var rates = _mealAndTransportRatesProvider.getMealAndTransportRatesDataForSelectedTime(currentYear, tempMonthIndex);
                if (errorFlag) { errorFlag = false; return; }
                if (rates.Count() <= 0)
                {
                    txtBoxYearlyMealValue.Text = "$0.00";
                    txtBoxCurrentMilageRate.Text = "$0.00/mile";

                    foreach (var rate in rates)
                    {
                        txtBoxYearlyMealValue.Text = String.Format("{0:C}", Math.Round((rate.mealRate), 2, MidpointRounding.AwayFromZero));
                        txtBoxCurrentMilageRate.Text = String.Format("{0:C}", Math.Round((rate.mileageRate), 2, MidpointRounding.AwayFromZero)) + "/mile";
                        totalMileage = 0;
                        totalMeals = 0;
                    }
                }

                if (rates.Count() > 0)
                {
                    foreach (var rate in rates)
                    {
                        txtBoxYearlyMealValue.Text = String.Format("{0:C}", Math.Round((rate.mealRate), 2, MidpointRounding.AwayFromZero));
                        txtBoxCurrentMilageRate.Text = String.Format("{0:C}", Math.Round((rate.mileageRate), 2, MidpointRounding.AwayFromZero)) + "/mile";
                        totalMileage += rate.mileageRate;
                        totalMeals += rate.mealRate;
                    }
                }

                List<MealAndTransportModel> updateVolunteersTable = new List<MealAndTransportModel>();
                foreach (var volunteer in VolunteerDataMealAndTranport)
                {
                    
                    volunteer.totalMealCost = 0.0;
                    volunteer.totalMileageCost = 0.0;

                    if (volunteer.numMeals == null)
                    {
                        volunteer.numMeals = 0;
                        volunteer.totalMealCost = 0.0;
                    }

                    if (volunteer.numMeals != null)
                    {
                        volunteer.totalMealCost = System.Math.Ceiling((totalMeals * (double)volunteer.numMeals) * 100) / 100;
                    }

                    if (volunteer.Mileage == null)
                    {
                        volunteer.Mileage = 0;
                        volunteer.totalMileageCost = 0.0;
                    }

                    if (volunteer.Mileage != null)
                    {
                        volunteer.totalMileageCost = System.Math.Ceiling((totalMileage * (double)volunteer.Mileage) * 100) / 100;
                    }

                    if (volunteer.numBusRides == null)
                    {
                        volunteer.numBusRides = 0;
                    }

                    updateVolunteersTable.Add(volunteer);
                }
                dtgMealAndTransport.ItemsSource = updateVolunteersTable.ToList();
            }
        }

        /// <summary>
        /// Function Name: dtgMealAndTransport_SelectionChanged
        /// This event will fire if the user selects a row in the datagrid and will 
        /// populate the filter text box with the selcted user
        /// </summary>
        /// <param name="sender"> The datagrid row being selected</param>
        /// <param name="e">The selection in datagrid change event</param>
        /// <author>Brendan Breuss</author>
        /// <created>3/31/2023</created>
        private void dtgMealAndTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgMealAndTransport.SelectedItem is MealAndTransportModel selectedItem)
            {
                cbobxFilterName.Text = selectedItem.strVolunteerName;
            }
        }

        #endregion


        #region Dropdown Exited Events
        /// <summary>
        /// Function Name: cboMonthlyMealValue_DropdownExited
        /// Call All functions that will repopulate the data as this will need to occur if the month changes
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/21/2022</Created>
        /// <param name="sender"> The month dropdown</param>
        /// <param name="e"> The dropdown exited event</param>
        private void cboMonthlyMealValue_DropdownExited(object sender, EventArgs e)
        {
            try
            {
                populateTable();
                populateMonthlyTotalsMealAndTransport();
                populateYtdTotalsMealAndTransport();
                populateFilterComboBox();
            }
            catch (RefreshDataCustomException rdce)
            {
                return;
            }
        }

        /// <summary>
        /// Function Name: cboYearlyMealValue_DropdownExited
        /// Call All functions that will repopulate the data as this will need to occur if the year changes
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/21/2022</Created>
        /// <param name="sender"> The Year dropdown</param>
        /// <param name="e"> The dropdown exited event</param>
        private void cboYearlyMealValue_DropdownExited(object sender, EventArgs e)
        {
            try
            {
                populateTable();
                populateMonthlyTotalsMealAndTransport();
                populateYtdTotalsMealAndTransport();
                populateFilterComboBox();
            }
            catch (RefreshDataCustomException rdce)
            {
                return;
            }
        }

        #endregion

        #region Excel Exporter

        /// <summary>
        /// Function Name: exportMealAndTransportExcel
        /// This function will use the created excel exporter class to export all items in the Meal and Transport 
        /// Datagrid. If the datagrid is empty then alert the user. Otherwise create an excel file/ sheet and name it
        /// the current year and month Meal and Transport. Then Add headers to the shhet to know what values are in 
        /// each row. Then for each row in the datagrid add it to the corresponding row in the excel sheet. Some objects
        /// added are empty simply to make the sheet look formatted nicely. 
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>03/26/2023</Created>
        private void exportMealAndTransportExcel()
        {
            if(dtgMealAndTransport.Items.Count <= 0)
            {
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Nothing to export please add info to table",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 1
                });
                return;
            }
            String month = cbobxMealAndTransportSelectMonth.Text;
            String year = cbobxMealAndTransportSelectYearRange.Text;
            ExcelFileModel excelFileModel = new ExcelFileModel();
            excelFileModel.FileName = "MealAndTransport_" + month + "_" + year;
            excelFileModel.Sheets = new List<ExcelSheetModel>();



            ExcelSheetModel excelSheetModel = new ExcelSheetModel()
            {
                Title = "M&T_" + month +  year,
                Tables = new List<ExcelTableModel>()
            };


            excelFileModel.Sheets.Add(excelSheetModel);

            ExcelTableModel excelTableModel = new ExcelTableModel()
            {

                Title = "M&T_" + month + "_" + year,
                Headers = new List<string>()


            };
            excelSheetModel.Tables.Add(excelTableModel);
            excelTableModel.Headers.Add("Volunteer Name");
            excelTableModel.Headers.Add("Num. Meals");
            excelTableModel.Headers.Add("Total Meal Value at ");
            excelTableModel.Headers.Add("Num. Bus Rides");
            excelTableModel.Headers.Add("Mileage");
            excelTableModel.Headers.Add("Total Mileage at ");

            excelTableModel.Rows = new List<object> ();

            object fixFormat = new
            {
                strVolunteerName = "",
                numMeals = "",
                totalMealCost = "Meal Rate: " + txtBoxYearlyMealValue.Text,
                numBusRides = "",
                Mileage = "",
                totalMileageCost = "Mileage Rate: " +txtBoxCurrentMilageRate.Text
            };

            excelTableModel.Rows.Add (fixFormat);

            object blank = new
            {
                strVolunteerName = ""
            };
            excelTableModel.Rows.Add(blank);


            foreach (MealAndTransportModel item in dtgMealAndTransport.Items.OfType<MealAndTransportModel>().ToList())
            {
                if (item.strVolunteerName != null && item.numMeals != null && item.totalMealCost != null 
                    && item.numBusRides != null && item.Mileage != null && item.totalMileageCost !=null)
                {
                    object tempItem = new
                    {
                        strVolunteerName = item.strVolunteerName,
                        numMeals = (int)item.numMeals,
                        totalMealCost = (double)item.totalMealCost,
                        numBusRides = (int)item.numBusRides,
                        Mileage = (decimal)item.Mileage,
                        totalMileageCost = (double)item.totalMileageCost
                    };
                    excelTableModel.Rows.Add(tempItem);
                }
            }
            object empty = new
            {
                strVolunteerName = ""
            };
            excelTableModel.Rows.Add(empty);

            int monthlyMealTotal = 0;
            int monthlyNumBusRides = 0;
            double monthlyMileage = 0;
            string? parseValue;

            parseValue = lblMealNumMeals.Content.ToString();
            if (parseValue != null)
                monthlyMealTotal = int.Parse(parseValue);

            parseValue = lblMealBusRides.Content.ToString();
            if (parseValue != null)
                monthlyNumBusRides = int.Parse(parseValue);

            parseValue = lblMealMileage.Content.ToString();
            if (parseValue != null)
                monthlyMileage = double.Parse(parseValue);

            string? monthlyTotalMealValue = "";
            double monthlyTotalMealValueExcel = 0;
            monthlyTotalMealValue = lblMealTotalMealValue.Content.ToString();
            if (monthlyTotalMealValue != null)
                double.TryParse(monthlyTotalMealValue.Replace("$", ""), out monthlyTotalMealValueExcel);

            string? monthlyMileageTotal = "";
            double monthlyMileageTotalExcel = 0;
            monthlyMileageTotal = lblMealMileageValue.Content.ToString();
            if (monthlyMileageTotal != null)
                double.TryParse(monthlyMileageTotal.Replace("$", ""), out monthlyMileageTotalExcel);

            object monthlyTotal = new
                {
                    monthTotals = "Monthly Totals",
                    monthlyMealTotal = monthlyMealTotal,
                    monthlyTotalMealValue = monthlyTotalMealValueExcel,
                    monthlyNumBusRides = monthlyNumBusRides,
                    monthlyMileage = monthlyMileage,
                    monthlyMileageTotal = monthlyMileageTotalExcel
            };

                excelTableModel.Rows.Add(monthlyTotal);

            int yearlyMealTotal = 0;
            int yearlyNumBusRides = 0;
            double yearlyMileage = 0;

            parseValue = lblYTDMealNumMeals.Content.ToString();
            if(parseValue != null)
               yearlyMealTotal = int.Parse(parseValue);

            parseValue = lblYTDMealBusRides.Content.ToString();
            if (parseValue != null)
                yearlyNumBusRides = int.Parse(parseValue);

            parseValue = lblYTDMealMileage.Content.ToString();
            if (parseValue != null)
                yearlyMileage = double.Parse(parseValue);
            
            string? yearlyTotalMealValue = "";
            double yearlyTotalMealValueExcel = 0;
            yearlyTotalMealValue = lblYTDMealTotalMealValue.Content.ToString();
            if(yearlyTotalMealValue != null)
            double.TryParse(yearlyTotalMealValue.Replace("$", ""), out yearlyTotalMealValueExcel);

            string? yearlyMileageTotal = "";
            double yearlyMileageTotalExcel = 0;
            yearlyMileageTotal = lblYTDMealMileageValue.Content.ToString();
            if (yearlyMileageTotal != null)
                double.TryParse(yearlyMileageTotal.Replace("$", ""), out yearlyMileageTotalExcel);

            object yearlyTotal = new
            {
                yearlyTotals = "YTD Totals",
                yearlyMealTotal = yearlyMealTotal,
                yearlyTotalMealValue = yearlyTotalMealValueExcel,
                yearlyNumBusRides = yearlyNumBusRides,
                yearlyMileage = yearlyMileage,
                yearlyMileageTotal = yearlyMileageTotalExcel
            };
            excelTableModel.Rows.Add(yearlyTotal);
            ExcelExporter.ExportToExcel(excelFileModel);

        }

        #endregion

        #region Button Events
        /// <summary>
        /// Function Name: BtnMealandTransportEdit_Click
        /// This will send the user into the edit mode. The edit mode is a new window that takes the selected month and year
        /// as well as the selected month and years rate to use as values on the new page. Will allow user to edit 
        /// volunteer info on this new page once page is exited will repopulate the data sources with the newly updated data
        /// </summary>
        /// <param name="sender"> The edit button being pressed</param>
        /// <param name="e"> the routed event from the button click</param>
        /// <author>Brendan Breuss</author>
        /// <history> 
        ///     Created: Jan 30, 2023
        ///     Last Modified: March 31st, 2023
        ///     Switched from edit mode being on same page to edit happening in new dialog window
        ///     Called functions so that items would update properly upon dialog close
        /// </history>
        private void BtnMealandTransportEdit_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditMealAndTransport addOrEditMealAndTransportForm = new AddOrEditMealAndTransport(_serviceProvider, int.Parse(cbobxMealAndTransportSelectYearRange.Text),
            cbobxMealAndTransportSelectMonth.SelectedItem.ToString(), cbobxMealAndTransportSelectMonth.SelectedIndex + 1, 
            txtBoxYearlyMealValue.Text, txtBoxCurrentMilageRate.Text);
            addOrEditMealAndTransportForm.Activate();
            addOrEditMealAndTransportForm.BringIntoView();
            addOrEditMealAndTransportForm.Focus();
            addOrEditMealAndTransportForm.ShowDialog();

            try
            {
                populateTable();
                populateMonthlyTotalsMealAndTransport();
                populateYtdTotalsMealAndTransport();
                populateFilterComboBox();
            }
            catch (RefreshDataCustomException rdce)
            {
                return;
            }
        }


        /// <summary>
        /// Function Name: btnMealandTransportExport_Click
        /// This is a button click event that occurs when excel button is pressed simply calls the export to excel method
        /// </summary>
        /// <param name="sender"> The export button being pressed</param>
        /// <param name="e"> the routed event from the button click</param>
        /// <author>Brendan Breuss</author>
        /// <created>3/22/2023</created>
        private void btnMealandTransportExport_Click(object sender, RoutedEventArgs e)
        {
            exportMealAndTransportExcel();
        }

        #endregion

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(FinanceMealAndTransportPage));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
    }
}
