using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using DocumentFormat.OpenXml.Bibliography;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for AddMealAndTransport.xaml used to edit volunteers for selected year and month
    /// All below private variables are used to keep track of various data throughout the page to ensure correct volunteer is
    /// being edited
    /// <Author>Brendan Breuss</Author>
    /// </summary>
    public partial class AddOrEditMealAndTransport : System.Windows.Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _VolunteerProvider;
        private readonly IMealAndTransportProvider _mealAndTransportProvider;
        private readonly IMealAndTransportRatesProvider _mealAndTransportRatesProvider;
        private readonly IDialogProvider _dialogProvider;
        private int startingYear;
        private string startingMonth;
        private int startingMonthIndex;
        private string mealRate;
        private string mileageRate;
        private bool changesMade;
        private string selectedVolunteerName;
        private string selectedNumBusRides;
        private string selectedNumMeals;
        private string selectedNumMileage;
        private MealAndTransportModel selectedVolunteerModel;
        private bool errorFlag;

        /// <summary>
        /// Creates and populates the meal and transport edit window based on the data founf on the meal and 
        /// Transport page when the edit button was clicked on that page
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/25/2023</Created>
        /// <param name="serviceProvider">The meal and Transport service provider</param>
        /// <param name="startingYear">The year selected in Meal and Trans Year combobox</param>
        /// <param name="startingMonth">The month selected in Meal and Trans monthcombobox</param>
        /// <param name="startingMonthIndex">The index of the above month</param>
        /// <param name="enteredMealRate">The meal rate for year and month on meal and trans page</param>
        /// <param name="enteredMileageRate">The mileage rate for year and month on meal and trans page</param>
        public AddOrEditMealAndTransport(IServiceProvider serviceProvider, int startingYear, string? startingMonth, int startingMonthIndex, 
            string enteredMealRate, string enteredMileageRate)
        {
            _serviceProvider = serviceProvider;
            _VolunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _mealAndTransportRatesProvider = _serviceProvider.GetRequiredService<IMealAndTransportRatesProvider>();
            _mealAndTransportProvider = _serviceProvider.GetRequiredService<IMealAndTransportProvider>();
            _dialogProvider = _serviceProvider.GetRequiredService<IDialogProvider>();

            _VolunteerProvider.DatabaseError += ErrorHandler;
            _mealAndTransportProvider.DatabaseError += ErrorHandler;


            this.startingMonth = "";
            if (startingMonth != null)
             this.startingMonth = startingMonth;
            this.startingYear = startingYear;
            this.startingMonthIndex = startingMonthIndex;
            this.mealRate = enteredMealRate;
            this.mileageRate = enteredMileageRate;
            this.changesMade = false;
            this.selectedVolunteerName = "";
            this.selectedNumBusRides = "";
            this.selectedNumMeals = "";
            this.selectedNumMileage = "";
            this.selectedVolunteerModel = new MealAndTransportModel();
            InitializeComponent();
            Refresh();
            Growl.Info(new GrowlInfo
            {
                Message = "Edit Page entered",
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 0
            });

        }

        #region Error Handling
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
        #endregion

        #region Populate Page with data
        /// <summary>
        /// Function Name: Refresh
        /// Calls all functions that would repopulate the page with data to act as a refresh of the page
        /// if no data exists for current month dummy data will be created to represent a new month
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/25/2023</Created>
        private void Refresh()
        {
            populateMealAndTransportEditNameComboBox();
            populateMealAndTransportEditDataGrid();
            populateMonthlyTotalsMealAndTransportEdit();
            if(dtgMealAndTransportEdit.Items.Count == 0)
            { 
             createNewMonthMealAndTransport();
            }

            if (this.startingMonth != null)
            lblCurrentSelectedMonthAddOrEditMealAndTransport.Content = this.startingMonth.ToString();
            lblCurrentSelectedYearAddOrEditMealAndTransport.Content = this.startingYear.ToString();
            lblMealAndTransportEditYearlyMealValue.Content = this.mealRate;
            lblMealAndTransportEditCurrentMileageRate.Content = this.mileageRate;
            if (this.startingMonth == null)
                this.startingMonth = "Error";
        }

        /// <summary>
        /// Function Name: populateMealAndTransportEditDataGrid
        /// Populates the datagrid with the volunteers information for the given month and year that were passed to 
        /// this edit page when it was initialized. 
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/25/2023</Created>
        private void populateMealAndTransportEditDataGrid()
        {
            dtgMealAndTransportEdit.Items.Clear();
            int currentYear = this.startingYear;
            int currentMonthIndex = this.startingMonthIndex;
            var volunteerDataMealAndTransportEdit = _mealAndTransportProvider.getAllMealAndTransportDataForSelectedTime(currentYear, currentMonthIndex);
            if (errorFlag) { errorFlag = false; return; }

            foreach (var volunteer in volunteerDataMealAndTransportEdit)
            {
                if (volunteer.numMeals == null)
                {
                    volunteer.numMeals = 0;
                }
                if (volunteer.Mileage == null)
                {
                    volunteer.Mileage = 0;
                }
                if (volunteer.numBusRides == null)
                {
                    volunteer.numBusRides = 0;
                }
                dtgMealAndTransportEdit.Items.Add(volunteer);
            }
        }

        /// <summary>
        /// Function Name: populateMonthlyTotalsMealAndTransportEdit
        /// Populates the monthly totals by looping through the datagrid and summing all the values in the columns
        /// then putting the sum values into their corresponding monthly value labels
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/25/2023</Created>
        private void populateMonthlyTotalsMealAndTransportEdit()
        {
            string strNumMealsTotal = "0";
            string strNumBusRides = "0";
            string strMileage = "0";
            var tableDate = dtgMealAndTransportEdit.Items;

            if (tableDate == null || tableDate.Count <= 0)
            {
                lblMealAndTransportEditMonthlyNumMeals.Content = strNumMealsTotal;
                lblMealAndTransportEditMonthlyNumBusRides.Content = strNumBusRides;
                lblMealAndTransportEditMonthlyMileage.Content = strMileage;
                return;
            }

            foreach (MealAndTransportModel model in tableDate)
            {
                strNumMealsTotal = Int32.Parse(strNumMealsTotal) + model.numMeals + "";
                strNumBusRides = Int32.Parse(strNumBusRides) + model.numBusRides + "";
                strMileage = Decimal.Parse(strMileage) + model.Mileage + "";
            }

            lblMealAndTransportEditMonthlyNumMeals.Content = strNumMealsTotal;
            lblMealAndTransportEditMonthlyNumBusRides.Content = strNumBusRides;
            lblMealAndTransportEditMonthlyMileage.Content = strMileage;
        }


        /// <summary>
        /// Function Name: populateMealAndTransportEditNameComboBox
        /// Populates the name filter combobox with the names of all volunteers as in the capstone guide packet
        /// it was addressed that if the volunteer has no data they should be given 0 in the excel sheet. So I allow all
        /// active volunteers to be selected from to add info if they have any new data
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/26/2023</Created>
        private void populateMealAndTransportEditNameComboBox()
        {
            List<VolunteerNameIdModel> listVolunteer = _VolunteerProvider.GetVolunteerNameAndId().ToList();
            if (errorFlag) { errorFlag = false; return; }

            foreach (var volunteer in listVolunteer)
            {
                cbobxMealAndTransportEditFilterName.Items.Add(volunteer);
            }
        }

        /// <summary>
        /// Function Name: createNewMonthMealAndTransport
        /// If this method fires it means that there was no data for the current month/ year selected if this is the 
        /// case then we populate the datagrid with all active volunteers and set their initial values to 0 This will 
        /// essentially act as creating a new month of meal and transport data
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>3/26/2023</Created>
        private void createNewMonthMealAndTransport()
        {
            List<MealAndTransportModel> saveNewMonthtoDatabase = new List<MealAndTransportModel>();
            int currentDay = DateTime.Now.Day;
            foreach (VolunteerNameIdModel volunteer in cbobxMealAndTransportEditFilterName.Items)
            {
                bool volunteerExists = dtgMealAndTransportEdit.Items.OfType<MealAndTransportModel>().Any(x => x.volunteerID == volunteer.Tuid);

                if (!volunteerExists)
                {
                    MealAndTransportModel volunteerToAdd = new MealAndTransportModel();
                    volunteerToAdd.strVolunteerName = volunteer.FullName;
                    volunteerToAdd.volunteerID = volunteer.Tuid;
                    volunteerToAdd.date = new DateTime(startingYear, startingMonthIndex, currentDay);
                    dtgMealAndTransportEdit.Items.Add(volunteerToAdd);
                    saveNewMonthtoDatabase.Add(volunteerToAdd);
                }
            }
            dtgMealAndTransportEdit.Items.Refresh();
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// Function Name: btnMealandTransportCancel_Click
        /// If cancel button is clicked and changes were made show a dailog asking the users if they are sure they wish to cancel the edits
        /// if the say yes then close the edit page without making changes. If user click no take them back to edit screen to allow them to keep
        /// making changes or even save them
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>03/25/2023</Created>
        /// <param name="sender">The cancel button</param>
        /// <param name="e">The cancel button event handler</param>
        private void btnMealandTransportCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.changesMade)
            {
                bool? confirmClose = _dialogProvider.ShowConfirmationDialog("Are you sure you want to cancel? Changes won't be saved unless saved button is pressed.", "Confirm Cancelation");

                if (confirmClose == true)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
        /// <summary>
        /// Function Name: MealAndTransportEditUpdateTable_Click
        /// In this method all text boxes are validated to ensure that valid data was entered to ensure integers or doubles
        /// are within the limit to not allow large numbers to cause errors. If the checks pass we check to see if the volunteer exists
        /// in the datagrid if they exist then we simply update their values in the datagrid. If the volunteer is not found in the datagrid
        /// we will create a new meal mileage volunteer model and add them to the table. 
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>03/27/2023</Created>
        /// <param name="sender">Edit Button on add edit pages</param>
        /// <param name="e">Edit button event handler</param>
        ///  /// <history> 
        ///     Last Modified: April 11th 2023
        ///     Added Error checking to not allow user to paste in dummy data and '.' will not break mileage
        /// </history>
        private void MealAndTransportEditUpdateTable_Click(object sender, RoutedEventArgs e)
        {
            bool addNewVolunteerInfo = true;
            int tempNum = 0;
            double tempDouble = 0;
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;


            if (cbobxMealAndTransportEditFilterName.SelectedItem == null)
            {
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "select a volunteer to update table",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
                return;
            }

            if (txtboxMealAndTransportEditNumBusRides.Text == null || txtboxMealAndTransportEditNumBusRides.Text == "")
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Num bus rides cannot be empty",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
                txtboxMealAndTransportEditNumBusRides.Text = this.selectedNumBusRides;
                return;
            }

            if (txtboxMealAndTransportEditNumMeals.Text == null || txtboxMealAndTransportEditNumMeals.Text == "")
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Num Meals cannot be empty",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
                txtboxMealAndTransportEditNumMeals.Text = this.selectedNumMeals;
                return;
            }

            if (txtboxMealAndTransportEditMileage.Text == null || txtboxMealAndTransportEditMileage.Text == "")
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Mileage cannot be empty",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
                txtboxMealAndTransportEditMileage.Text = this.selectedNumMileage;
                return;
            }

            if (txtboxMealAndTransportEditMileage.Text.Contains(decimalSeparator))
            {
                // If a decimal separator is present and the input is a digit, limit the number of digits after the decimal separator to 2
                string[] parts = (txtboxMealAndTransportEditMileage.Text.Split(decimalSeparator[0]));
                if (parts.Length > 1 && parts[1].Length > 2)
                {
                   
                    Growl.Clear();
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "Only two decimal places are allowed",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                    txtboxMealAndTransportEditMileage.Text = this.selectedNumMileage;
                    return;
                }              
            }
 

            bool successfullyParsedNumMeals = int.TryParse(txtboxMealAndTransportEditNumMeals.Text, out tempNum);
            if (txtboxMealAndTransportEditNumMeals.Text.Length >= 4 || int.Parse(txtboxMealAndTransportEditNumMeals.Text) < 0 
                || int.Parse(txtboxMealAndTransportEditNumMeals.Text) >= 1000 || !successfullyParsedNumMeals)
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Please input a num meals between 0-999",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
                txtboxMealAndTransportEditNumMeals.Text = this.selectedNumMeals;
                return;
            }
            bool successfullyParsedNumBusRides = int.TryParse(txtboxMealAndTransportEditNumBusRides.Text, out tempNum);
            if (txtboxMealAndTransportEditNumBusRides.Text.Length >= 4 || int.Parse(txtboxMealAndTransportEditNumBusRides.Text) < 0 
                || int.Parse(txtboxMealAndTransportEditNumBusRides.Text) >= 1000 || !successfullyParsedNumBusRides)
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Please input a num bus rides between 0-999",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
                txtboxMealAndTransportEditNumBusRides.Text = this.selectedNumBusRides;
                return;
            }
            bool successfullyParsedMileage = double.TryParse(txtboxMealAndTransportEditMileage.Text, out tempDouble);
            if (!successfullyParsedMileage || txtboxMealAndTransportEditMileage.Text.Length >= 8 || decimal.Parse(txtboxMealAndTransportEditMileage.Text) < 0 
                || decimal.Parse(txtboxMealAndTransportEditMileage.Text) >= 2000)
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Please input a mileage between 0-1999.99",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
                txtboxMealAndTransportEditMileage.Text = this.selectedNumMileage;
                return;
            }
            VolunteerNameIdModel volunteerInComboBox = cbobxMealAndTransportEditFilterName.SelectedItem.CastTo<VolunteerNameIdModel>();

            foreach (MealAndTransportModel volunteer in dtgMealAndTransportEdit.Items)
            {
                if (volunteer.volunteerID == volunteerInComboBox.Tuid)
                {
                    addNewVolunteerInfo = false;
                }
            }

            MealAndTransportModel updateVolunteerModel;
            if (addNewVolunteerInfo)
            {
                updateVolunteerModel = new MealAndTransportModel();
                updateVolunteerModel.strVolunteerName = volunteerInComboBox.FullName;
                updateVolunteerModel.volunteerID = volunteerInComboBox.Tuid;
                if (txtboxMealAndTransportEditNumMeals.Text != "" && txtboxMealAndTransportEditNumMeals.Text != null)
                {
                    updateVolunteerModel.numMeals = int.Parse(txtboxMealAndTransportEditNumMeals.Text);
                }
                else
                {
                    updateVolunteerModel.numMeals = 0;
                }

                if (txtboxMealAndTransportEditNumBusRides.Text != "" && txtboxMealAndTransportEditNumBusRides.Text != null)
                {
                    updateVolunteerModel.numBusRides = int.Parse(txtboxMealAndTransportEditNumBusRides.Text);
                }
                else
                {
                    updateVolunteerModel.numBusRides = 0;
                }

                if (txtboxMealAndTransportEditMileage.Text != "" && txtboxMealAndTransportEditMileage.Text != null)
                {
                    updateVolunteerModel.Mileage = decimal.Parse(txtboxMealAndTransportEditMileage.Text);
                }
                else
                {
                    updateVolunteerModel.Mileage = 0;
                }
                updateVolunteerModel.date = new DateTime(startingYear, startingMonthIndex, 1);

                dtgMealAndTransportEdit.Items.Add(updateVolunteerModel);
                this.selectedVolunteerModel = updateVolunteerModel;

            }

            if (!addNewVolunteerInfo)
            {
                foreach (MealAndTransportModel volunteer in dtgMealAndTransportEdit.Items)
                {
                    if (volunteer.volunteerID == this.selectedVolunteerModel.volunteerID)
                    {
                        volunteer.strVolunteerName = volunteerInComboBox.FullName;
                        volunteer.volunteerID = volunteerInComboBox.Tuid;

                        if (txtboxMealAndTransportEditNumMeals.Text != null && txtboxMealAndTransportEditNumMeals.Text != "")
                        {
                            volunteer.numMeals = int.Parse(txtboxMealAndTransportEditNumMeals.Text);
                        }
                        else
                        {
                            volunteer.numMeals = 0;
                        }

                        if (txtboxMealAndTransportEditNumBusRides.Text != "" && txtboxMealAndTransportEditNumBusRides.Text != null)
                        {
                            volunteer.numBusRides = int.Parse(txtboxMealAndTransportEditNumBusRides.Text);
                        }
                        else
                        {
                            volunteer.numBusRides = 0;
                        }

                        if (txtboxMealAndTransportEditMileage.Text != "" && txtboxMealAndTransportEditMileage.Text != null)
                        {
                            if (txtboxMealAndTransportEditMileage.Text.Contains(decimalSeparator))
                            {
                                volunteer.Mileage = decimal.Parse(txtboxMealAndTransportEditMileage.Text);
                            }
                            else
                            {
                                string fixFormat = txtboxMealAndTransportEditMileage.Text + ".00";
                                volunteer.Mileage = decimal.Parse(fixFormat);
                            }
                        }
                        else
                        {
                            volunteer.Mileage = 0;
                        }
                        this.selectedVolunteerModel = volunteer;
                        break;
                    }
                }
                dtgMealAndTransportEdit.Items.Refresh();
            }
            populateMonthlyTotalsMealAndTransportEdit();
            cbobxMealAndTransportEditFilterName.SelectedItem = null;
            dtgMealAndTransportEdit.SelectedItem = this.selectedVolunteerModel;
            this.changesMade = true;
        }

        /// <summary>
        /// Function Name: btnMealandTransportSave_Click
        /// If pressed a dialog will pop up to ask the user if they are sure they want to save as this will alter the database with no recovery
        /// to undo if they are sure then save to database and close edit page otherwise go back to edit page
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>03/31/2023</Created>
        /// <param name="sender">Save button</param>
        /// <param name="e">save button event handler</param>
        private void btnMealandTransportSave_Click(object sender, RoutedEventArgs e)
        {
            bool? confirmSave = _dialogProvider.ShowConfirmationDialog("Saving will alter updated records do you wish to proceed", "Confirm Save");
            if (confirmSave == true)
            {
                List<MealAndTransportModel> listOfMealAndTransportRecords = new List<MealAndTransportModel>();

                foreach (var volunteer in dtgMealAndTransportEdit.Items)
                {
                    listOfMealAndTransportRecords.Add(volunteer.CastTo<MealAndTransportModel>());
                }

                _mealAndTransportProvider.updateMealAndTransportDatabase(listOfMealAndTransportRecords, startingYear, startingMonthIndex);
                if (errorFlag) { errorFlag = false; return; }
                this.Close();
            }
            else if (confirmSave == false)
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Saves not completed",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
            }
        }

        #endregion

        #region Selection Changed Events
        /// <summary>
        /// Function Name: cbobxMealAndTransportEditFilterName_SelectionChanged
        /// This function will clear all the textboxes as the volunteer being selected changes, then we will search for the selected volunter 
        /// in the combobox in the datagrid of volunteers if the volunteer in the combobox is in the datagrid then select that 
        /// volunteer in the datagrid otherwise select nothing
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>03/30/2023</Created>
        /// <param name="sender">Filter name combobox</param>
        /// <param name="e">Selection changed event</param>
        private void cbobxMealAndTransportEditFilterName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtboxMealAndTransportEditNumMeals.Clear();
            txtboxMealAndTransportEditNumBusRides.Clear();
            txtboxMealAndTransportEditMileage.Clear();

            dtgMealAndTransportEdit.SelectedIndex  = -1;
            string? volunteerToSearch = "Test";
            if(cbobxMealAndTransportEditFilterName.SelectedValue == null || cbobxMealAndTransportEditFilterName.SelectedItem.ToString() == null)
            {
                volunteerToSearch = "Test";
            }
            else
            {
                volunteerToSearch = cbobxMealAndTransportEditFilterName.SelectedItem.ToString();
            }



            if (cbobxMealAndTransportEditFilterName.SelectedValue == null || cbobxMealAndTransportEditFilterName.SelectedItem.ToString() == null || cbobxMealAndTransportEditFilterName.SelectedItem.ToString() == "")
            {
                dtgMealAndTransportEdit.SelectedIndex = -1;
            }
            else
            {
                
                foreach (MealAndTransportModel volunteer in dtgMealAndTransportEdit.Items)
                {
                    if (volunteerToSearch == volunteer.strVolunteerName)
                    {
                        dtgMealAndTransportEdit.SelectedItem = volunteer;
                        if (volunteer.numMeals == null)
                        {
                            volunteer.numMeals = 0;
                        }

                        if (volunteer.Mileage == null)
                        {
                            volunteer.Mileage = 0;
                        }

                        if (volunteer.numBusRides == null)
                        {
                            volunteer.numBusRides = 0;
                        }

                        txtboxMealAndTransportEditNumMeals.Text = volunteer.numMeals + "";
                        txtboxMealAndTransportEditNumBusRides.Text = volunteer.numBusRides + "";
                        txtboxMealAndTransportEditMileage.Text = volunteer.Mileage + "";

                        if (volunteer.numMeals != null)
                        {
                            this.selectedNumMeals = volunteer.numMeals + "";
                        }
                        if (volunteer.numBusRides != null)
                        {
                            this.selectedNumBusRides = volunteer.numBusRides + "";
                        }
                        if (volunteer.Mileage != null)
                        {
                            this.selectedNumMileage = volunteer.Mileage + "";
                        }
                        break;
                    }
                    else
                    {
                        txtboxMealAndTransportEditNumMeals.Text = 0 + "";
                        txtboxMealAndTransportEditNumBusRides.Text = 0+ "";
                        txtboxMealAndTransportEditMileage.Text = 0 + "";
                    }
                }
            }
        }

        /// <summary>
        /// Function Name: dtgMealAndTransportEdit_SelectionChanged
        /// This Function will take the data of the selected volunteer and put it into all of the the filter names combobox
        /// Doing this will make the text boxes fill with the selected volunteers info because it will fire the above event
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>03/31/2023</Created>
        /// <param name="sender">Datagrid row being selected</param>
        /// <param name="e">selection changed event</param>
        private void dtgMealAndTransportEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgMealAndTransportEdit.SelectedItem is MealAndTransportModel selectedVolunteerInDatagrid)
            {
                this.selectedVolunteerModel = selectedVolunteerInDatagrid;
                if (selectedVolunteerInDatagrid.numMeals == null || selectedVolunteerInDatagrid.numMeals <= 0)
                {
                    txtboxMealAndTransportEditNumMeals.Text = 0 + "";
                    this.selectedNumMeals = 0 + "";
            }
                else
                {
                    txtboxMealAndTransportEditNumMeals.Text = selectedVolunteerInDatagrid.numMeals + "";
                    this.selectedNumMeals = selectedVolunteerInDatagrid.numMeals + "";
            }
                if (selectedVolunteerInDatagrid.numBusRides == null || selectedVolunteerInDatagrid.numBusRides <= 0)
                {
                    txtboxMealAndTransportEditNumBusRides.Text = 0 + "";
                    this.selectedNumBusRides = 0 + "";
            }
                else
                {
                    txtboxMealAndTransportEditNumBusRides.Text = selectedVolunteerInDatagrid.numBusRides + "";
                    this.selectedNumBusRides = selectedVolunteerInDatagrid.numBusRides + "";
            }
                if (selectedVolunteerInDatagrid.Mileage == null || selectedVolunteerInDatagrid.Mileage <= 0)
                {
                    txtboxMealAndTransportEditMileage.Text = 0 + "";
                    this.selectedNumMileage = 0 + "";
            }
                else
                {
                    txtboxMealAndTransportEditMileage.Text = selectedVolunteerInDatagrid.Mileage + "";
                    this.selectedNumMileage = selectedVolunteerInDatagrid.Mileage + "";
            }
                if(selectedVolunteerInDatagrid.strVolunteerName != null)
                this.selectedVolunteerName = selectedVolunteerInDatagrid.strVolunteerName;
                cbobxMealAndTransportEditFilterName.Text = selectedVolunteerInDatagrid.strVolunteerName;

            }
        }

        #endregion

        #region Validate Input

        /// <summary>
        /// Function Name: validateIntegerInput
        /// Determines if the input in the textbox is an integer if not growl at user otherwise accept input
        /// </summary>
        /// <Author>Brendan Breuss</Author>
        /// <Created>03/30/2023</Created>
        /// <param name="sender">The currently typed in textbox either num meals or num bus rides</param>
        /// <param name="e">validate textbox event</param>
        private void validateIntegerInput(object sender, TextCompositionEventArgs e)
        {
            int input;
            if (!int.TryParse(e.Text, out input))
            {
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Please input only valid numbers",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
            }  
        }

        /// <summary>
        /// Function Name: ValidDecimalNumber_PreviewTextInput
        /// 
        /// Purpose: The purpose of this function is to preview the key input that the user is typing. User is only
        ///     allowed to enter numbers. The user is allowed one decimal point with only two decimal places. 
        ///     
        ///     If the user attempts to enter bad data, the keystroke is ignored and a Growl Warning is displayed.
        /// </summary>
        /// <param name="sender">mileage textbox</param>
        /// <param name="e">validate textbox event</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 25, 2023</date>
        /// <ModifiedBy>Brendan Breuss</ModifiedBy>
        /// <Modified>Apr 1, 2023</Modified>
        private void ValidDecimalNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Get the current culture's decimal separator
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            // Check if the input is a digit, a decimal separator, or a negative sign
            bool isDigit = char.IsDigit(e.Text, 0);
            bool isDecimalSeparator = e.Text == decimalSeparator || e.Text == ".";

            // Allow digits and a single decimal separator
            if (isDigit || isDecimalSeparator)
            {
                // If a decimal separator is already present, disallow another one
                if (isDecimalSeparator && ((System.Windows.Controls.TextBox)sender).Text.Contains(decimalSeparator))
                {
                    e.Handled = true;
                    Growl.Clear();
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "Only one decimal point is valid",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }
            }
            else
            {
                // Input is not valid, disallow the event
                e.Handled = true;
                Growl.Clear();
                Growl.Warning(new GrowlInfo
                {
                    Message = "Please input only valid numbers",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });

            }
        }

        #endregion
    }
}

