using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Events;
using A_FGMS.DataLayer.EventBroker;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using Bogus;
using Bogus.DataSets;
using C_FGMS.UI.Helpers;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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
using DataTable = System.Data.DataTable;
using ErrorEventArgs = B_FGMS.BusinessLogic.Events.ErrorEventArgs;
using System.CodeDom;
using A_FGMS.DataLayer.Exceptions;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for FinancePTOPage.xaml
    /// </summary>
    public partial class FinancePTOPage : System.Windows.Controls.Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPTOProvider _PTOProvider;
        private readonly IPTOStipendRates _PTOStipendRates;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private List<clsPTOModel>? currentListOfPTOInfo;
        private readonly GeneralBusinessLogic generalBusinessLogic;
        private bool errorFlag;
        public FinancePTOPage(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _PTOProvider = _serviceProvider.GetRequiredService<IPTOProvider>();
            _PTOStipendRates = _serviceProvider.GetRequiredService<IPTOStipendRates>();
            _refreshEventBroker = refreshEventBroker;
            generalBusinessLogic = new GeneralBusinessLogic();

            _PTOProvider.DatabaseError += ErrorHandler;
            _PTOStipendRates.DatabaseError += ErrorHandler;
            errorFlag = false;

            InitializeComponent();

            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                // TODO: Add refresh call



                cbobxPTOYear.SelectionChanged -= cbobxPTOYear_SelectionChanged;

                InitializePage();

                cbobxPTOYear.SelectionChanged += cbobxPTOYear_SelectionChanged;

            });

            InitializePage();

            cbobxPTOYear.SelectionChanged += cbobxPTOYear_SelectionChanged;
            cbobxPTOMonths.SelectionChanged += cbobxPTOMonth_SelectionChanged;

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
        /// Handles the Edit Button click toggling to an edit State
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Brendan Breuss</author>
        /// <history> 
        ///     Created: Jan 30, 2023
        ///     Last Modified: Feb 8, 2023   
        /// </history>
        private void BtnPtoEdit_Click(object sender, RoutedEventArgs e)
        {
            PTOEditState();

        }



        /// <summary>
        /// This will set the state of Stipend and mileage rate to  be editable
        /// Will also make save and cancel  visible hiding the edit and print buttons
        /// Allows Changes to the datagrid for mass entry
        /// Last will change the labels to prompt the user to edit rates
        /// </summary>
        /// <author>Brendan Breuss</author>
        /// <history> 
        ///     Created: Feb 8, 2023  
        ///     Edited:Mar 4, 2023
        ///     Changed how this method works since we changed to a menu for editing
        ///     Changed this form to create the menu required for editing. 
        /// </history>
        private void PTOEditState()
        {
            AddNewPTOMonth newForm = new AddNewPTOMonth(_serviceProvider, int.Parse(cbobxPTOYear.SelectedValue +""), cbobxPTOMonths.SelectedIndex);
            newForm.ShowDialog();
            Growl.GrowlPanel = null;
            try
            {
                populatePTOTable();
            }
            catch (RefreshDataCustomException rdce)
            {
            }
        }


        /// <summary>
        /// This method makes a Database call for the Stipend rates.
        /// It will set the retrieved values into a clsPTOStipendRatesModel and display them on screen
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/18/23
        /// </Date>
        private void populateRates()
        {
           clsPTOStipendRatesModel clsStipendRates = _PTOStipendRates.getStipendAndPTORateForAGivenMonth(int.Parse(cbobxPTOYear.SelectedItem + ""), cbobxPTOMonths.SelectedIndex + 1);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }

            txtBoxPTORate.Text = String.Format("{0:C}", Math.Round(clsStipendRates?.PTORate ?? 0, 2, MidpointRounding.AwayFromZero));
            txtBoxPTOStipendRate.Text = String.Format("{0:C}", Math.Round(clsStipendRates?.StipendRate ?? 0, 2, MidpointRounding.AwayFromZero));
        }






        /// <summary>
        /// Event Handler for the cbobxPTOYear. Calls populatePTOTable()
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cbobxPTOYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                populatePTOTable();
            }
            catch (RefreshDataCustomException rdce)
            {
            }
        }


        /// <summary>
        /// Event handler that is triggered when the user selects a different item in the cbobxPTOMonth (ComboBox for selecting a month).
        /// It calls the populatePTOTable() method, which updates the displayed data for the user to reflect the newly selected month.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <param name="sender">The object that triggered the event (the cbobxPTOMonth ComboBox).</param>
        /// <param name="e">The event arguments for the SelectionChanged event.</param>
        /// <Date>
        /// Last Modified:4/3/23
        /// </Date>
        private void cbobxPTOMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                populatePTOTable();
            }
            catch(RefreshDataCustomException rdce) 
            { 
            }
        }


        /// <summary>
        /// Initializes the page by calling two methods: populateCMDBoxes() and populatePTOTable().
        /// populateCMDBoxes() updates ComboBoxes that allow the user to filter the displayed data.
        /// populatePTOTable() retrieves and displays data related to PTO (Paid Time Off) for the current user.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:2/16/23
        /// </Date>

        private void InitializePage()
        {
            populateCMDBoxes();
            populatePTOTable();
            
        }


        /// <summary>
        /// Updates the displayed data related to PTO (Paid Time Off) statistics.
        /// It retrieves the YTD (Year-to-Date) stipends paid and displays it in the lblPTOytdStipendPaidTotalNumber label.
        /// It also retrieves and displays the beginning stipend and remaining stipend for the selected month and year in their respective labels.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last modified: 4/3/23
        /// </date>

        private void populateStats()
        {
            decimal ytdStipendsPaid = _PTOProvider.getYdtPTOStipendsPaid(int.Parse(cbobxPTOYear.SelectedItem + ""), cbobxPTOMonths.SelectedIndex + 1);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            lblPTOytdStipendPaidTotalNumber.Content = ytdStipendsPaid.ToString("C2");
            decimal[] arrGrantInto = _PTOStipendRates.getTotalGrantStipend(int.Parse(cbobxPTOYear.SelectedItem + ""), cbobxPTOMonths.SelectedIndex + 1);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            lblPTOBeginningStipendNumber.Content = arrGrantInto[0].ToString("C2");
            lblPTOStipendRemainingNumber.Content = arrGrantInto[1].ToString("C2");




        }

        /// <summary>
        /// Calls the two methods that will populate both CMD boxes with their respective months and years
        /// It will also add the event handlers to their respective combo boxes
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last modified: 3/15/23
        /// </date>

        private void populateCMDBoxes()
        {
            populateYearList();
            populateMonths();
        }




        /// <summary>
        /// Populates the PTO table with data for the selected month and year.
        /// It calls the getASpecificMonthOfPtoInfomation() method of the _PTOProvider object to retrieve a list of PTO information for the selected month and year.
        /// It assigns the retrieved list to the currentListOfPTOInfo variable and sets the ItemsSource property of the dtgPTO (DataGrid) object to the list, populating the table.
        /// It also calls three additional methods: populateTotals(), populateStats(), and populateRates(), to update other displayed data related to PTO.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last modified: 3/12/23
        /// </date>


        private void populatePTOTable()
        {
           var x = _PTOProvider.getASpecificMonthOfPtoInfomation(int.Parse(cbobxPTOYear.SelectedItem +""), cbobxPTOMonths.SelectedIndex+1).ToList();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }

            currentListOfPTOInfo = x;

            dtgPTO.ItemsSource = x;

            populateTotals();
            populateStats();
            populateRates();
            
        }
        /// <summary>
        /// Populates the cbobxPTOYear ComboBox with the available years for which PTO data is available.
        /// It calls the getTotalYears() method of the _PTOProvider object to retrieve a list of available years.
        /// It adds each year to the ComboBox.
        /// It also ensures that the current year is included in the list of available years and selects it as the default selection.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last modified: 4/1/23
        /// </date>

        private void populateYearList()
        {

            cbobxPTOYear.Items.Clear();
            int tempYear = 0;

                foreach (int year in _PTOProvider.getTotalYears())
                {
                if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }

                cbobxPTOYear.Items.Add(year);
                tempYear = year;
                }
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }

            if (tempYear != DateTime.Now.Year)
            {
                tempYear = DateTime.Now.Year;
                cbobxPTOYear.Items.Add(tempYear);
            }




                cbobxPTOYear.SelectedItem = tempYear;




        }

        /// <summary>
        /// Populates the cbobxPTOMonths 
        /// 
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified: 2/26/22
        /// </Date>
        private void populateMonths()
        {



            cbobxPTOMonths.ItemsSource = generalBusinessLogic.GetMonths();
            cbobxPTOMonths.SelectedIndex = DateTime.Now.Month-1;


            }



        /// <summary>
        /// The purpose of this method is to clear the values currently stored within the labels 
        /// that store the totals. Then it will set thoses to the values to the corresponding 
        /// total from the data grid
        /// 
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified: 2/26/22
        /// </Date>
        private void populateTotals() 
        {
            string strRegHoursTotal = "0";
            string strPTOUsedTotal = "0";
            string strPTOEarnedTotal = "0";
            string strPTOStartTotal = "0";
            string strPTOEndingTotal = "0";
            string strStipendPaidTotal = "0";
            string strYTDHoursTotal = "0";



            foreach (clsPTOModel model in dtgPTO.ItemsSource)
           {
                


                strRegHoursTotal = decimal.Parse(strRegHoursTotal)+ model.RegularHours + "";
                strPTOUsedTotal = decimal.Parse(strPTOUsedTotal) + model.PtoUsed + "";
                strPTOEarnedTotal = decimal.Parse(strPTOEarnedTotal) + model.PtoEarned + "";
                strPTOStartTotal = decimal.Parse(strPTOStartTotal) + model.PtoStart + "";
                strPTOEndingTotal = decimal.Parse(strPTOEndingTotal) + model.PtoEnd + "";
                strStipendPaidTotal = decimal.Parse(strStipendPaidTotal) + model.StipendPaid + "";
                strYTDHoursTotal = decimal.Parse(strYTDHoursTotal) + model.YearToDateHour + "";


            }

            lblPTORegHoursTotal.Content = strRegHoursTotal;
            lblPTOUsedTotal.Content = strPTOUsedTotal;
            lblPTOEarnedTotal.Content = strPTOEarnedTotal;
            lblPTOStartTotal.Content = strPTOStartTotal;
            lblPTOEndingTotal.Content = strPTOEndingTotal;
            lblPTOStipendPaidTotal.Content = decimal.Parse(strStipendPaidTotal).ToString("C2");
            lblPTOytdHoursTotal.Content = strYTDHoursTotal;
            lblPTOytdStipendPaidThisPeriodNumber.Content = decimal.Parse(strStipendPaidTotal).ToString("C2");


        }









        /// <summary>
        /// This method handles seaching for different volunteers in the datagrid.
        /// The main idea behind this is to use the global variable "currentListOfPTOInfo"
        /// to run a linq query on it to find the names of the volunteers that contain the 
        /// text stored in txtPTOSearchVolunteer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPTOSearchVolunteer_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine(txtPTOSearchVolunteer.Text);
            if (txtPTOSearchVolunteer.Text != "")
            {
                if (currentListOfPTOInfo == null)
                {

                } else
                {
                    if (currentListOfPTOInfo != null)
                    {
                        List<clsPTOModel>? filteredList = currentListOfPTOInfo
                            .Where(currentListOfPTOInfo => (currentListOfPTOInfo.strFullName ?? "").ToUpper().Contains(txtPTOSearchVolunteer.Text.ToUpper())).ToList();


                        dtgPTO.ItemsSource = null;
                        dtgPTO.ItemsSource = filteredList;

                    }

                }




            }
            else
            {

                dtgPTO.ItemsSource = null;
                dtgPTO.ItemsSource = currentListOfPTOInfo;



            }
            }


        /// <summary>
        /// Event handler for the Print to Excel button.
        /// 
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintToExcel_Click(object sender, RoutedEventArgs e)
        {
            createNewExcelDocument();
        }


        /// <summary>
        /// Creates a new Excel document containing data related to PTO (Paid Time Off).
        /// It creates a new ExcelFileModel object and initializes it with a file name and a single sheet with the title "PTO Stuff".
        /// It creates an ExcelTableModel object and initializes it with headers for the PTO information data to be exported to Excel.
        /// It then iterates through the current PTO information list in dtgPTO, creating a new object for each row and adding it to the ExcelTableModel's Rows collection.
        /// It also adds a "Totals" row, a "Year To Date Hours" row, and three additional rows for other PTO statistics data.
        /// Finally, it exports the data to Excel using the ExcelExporter class.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last modified: 4/3/2023
        /// </date>
        private void createNewExcelDocument()
        {



            ExcelFileModel excelFileModel = new ExcelFileModel();
            excelFileModel.FileName = "PTO Info" + (cbobxPTOMonths.SelectedIndex + 1) + "_" + cbobxPTOYear.SelectedItem;
            excelFileModel.Sheets = new List<ExcelSheetModel>();



                ExcelSheetModel excelSheetModel = new ExcelSheetModel()
                {
                    Title = "PTO Stuff",
                    Tables = new List<ExcelTableModel>()
                };

            excelFileModel.Sheets.Add(excelSheetModel);

            ExcelTableModel excelTableModel = new ExcelTableModel()
            {

                Title = "",
                Headers = new List<string>()


            };
            excelSheetModel.Tables.Add(excelTableModel);

            excelTableModel.Headers.Add("Volunteer Name");
            excelTableModel.Headers.Add("Reg Hrs");
            excelTableModel.Headers.Add("PTO Used");
            excelTableModel.Headers.Add("PTO Earned");
            excelTableModel.Headers.Add("PTO Start");
            excelTableModel.Headers.Add("PTO Ended");
            excelTableModel.Headers.Add("Stipend Paid");
            excelTableModel.Headers.Add("Ytd Hours");
            

            excelTableModel.Rows = new List<object>();

            foreach (var item in dtgPTO.Items.OfType<clsPTOModel>().ToList())
            {
                
                if (item.RegularHours != null && item.PtoUsed != null && item.PtoEarned != null && item.PtoStart != null && item.PtoEnd != null && item.StipendPaid != null && item.YearToDateHour != null)
                {
                    object tempItem = new
                    {
                        Fullname = item.strFullName,
                        RegularHours = (decimal)item.RegularHours,
                        PtoUsed = (decimal)item.PtoUsed,
                        PtoEarned = (decimal)item.PtoEarned,
                        PtoStart = (decimal)item.PtoStart,
                        PtoEnded = (decimal)item.PtoEnd,
                        StipendPaid = (decimal)item.StipendPaid,
                        YtdHours = (decimal)item.YearToDateHour
                    };

                    excelTableModel.Rows.Add(tempItem);

                }






            }

            if (lblPTORegHoursTotal.Content != null && lblPTOUsedTotal.Content != null && lblPTOEarnedTotal.Content != null && lblPTOStartTotal.Content != null && lblPTOEndingTotal.Content != null && lblPTOytdHoursTotal.Content != null)
            {



                object totalRow = new
                {
                    RowName = "Totals",
                    RegularHours = lblPTORegHoursTotal.Content.CastTo<decimal>(),
                    PtoUsed = lblPTOUsedTotal.Content.CastTo<decimal>(),
                    PtoEarned = lblPTOEarnedTotal.Content.CastTo<decimal>(),
                    PtoStart = lblPTOStartTotal.Content.CastTo<decimal>(),
                    PtoEnded = lblPTOEndingTotal.Content.CastTo<decimal>(),
                    StipendPaid = lblPTOStipendPaidTotal.Content,
                    YtdHours = lblPTOytdHoursTotal.Content.CastTo<decimal>()

                };


                excelTableModel.Rows.Add(totalRow);


            }
            object YTDRow = new
            {
                YTDHours = "Year To Date Hours",
                Hours = lblPTOytdHoursTotal.Content


            };

            excelTableModel.Rows.Add(YTDRow);

            object StipendPaidThisPeriod = new
            {
                StipendPaidThisPeriod = "Stipend Paid This Period",
                Hours = lblPTOytdStipendPaidTotalNumber.Content

            };
            excelTableModel.Rows.Add(StipendPaidThisPeriod);
            object BeginningStipendRow = new
            {
                BeginningStipendRow = "Beginning Stipend",
                StipendAmount = lblPTOBeginningStipendNumber.Content


            };
            excelTableModel.Rows.Add(BeginningStipendRow);
            object StipendRemainingRow = new
            {
                StipendRemainingRow = "Remaining Stipend",
                StipendAmount = lblPTOStipendRemainingNumber.Content
            };

            excelTableModel.Rows.Add(StipendRemainingRow);

            ExcelExporter.ExportToExcel(excelFileModel);
        }








        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(FinancePTOPage));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }


    }


}
   
