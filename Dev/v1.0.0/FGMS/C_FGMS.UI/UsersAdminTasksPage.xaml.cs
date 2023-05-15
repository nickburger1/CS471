using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.AddressProviders;
using B_FGMS.BusinessLogic.Services.AssignmentProviders;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using DocumentFormat.OpenXml.Bibliography;
using System.Collections.ObjectModel;
using System.Windows.Forms.VisualStyles;
using HandyControl.Tools.Extension;
using A_FGMS.DataLayer.EventBroker;
using B_FGMS.BusinessLogic.ViewModels.AdminTaskViewModels;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using C_FGMS.UI.Helpers;
using B_FGMS.BusinessLogic.ViewModels.VolunteerInfoViewModels;
using B_FGMS.BusinessLogic.Events;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for UsersAdminTasksPage.xaml
    /// </summary>
    public partial class UsersAdminTasksPage : Page
    {
        bool loaded = false;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAssignmentProvider _assignmentProvider;
        private readonly IExpenseProvider _expenseProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IMealAndTransportRatesProvider _mealAndTransportRatesProvider;
        private readonly IPTOStipendRates _ptoStipendRates;
        private readonly IDialogProvider _dialogProvider;
        private readonly IStudentProvider _studentProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private NeedsViewModel _needsViewModel;
        private ConditionsViewModel _conditionsViewModel;
        private bool errorFlag;

        public UsersAdminTasksPage(IServiceProvider serviceProvider,
            IExpenseProvider financeProvider,
            IAssignmentProvider assignmentProvider,
            IVolunteerProvider volunteerProvider,
            IMealAndTransportRatesProvider mealAndTransportRatesProvider,
            IPTOStipendRates ptoStipendRates,
            IDialogProvider dialogProvider,
            IStudentProvider studentProvider,

            DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _assignmentProvider = assignmentProvider;
            _expenseProvider = financeProvider;
            _volunteerProvider = volunteerProvider;
            _mealAndTransportRatesProvider = mealAndTransportRatesProvider;
            _ptoStipendRates = ptoStipendRates;
            _refreshEventBroker = refreshEventBroker;
            _studentProvider = studentProvider;
            _dialogProvider = dialogProvider;

            _assignmentProvider.DatabaseError += ErrorHandler;
            _expenseProvider.DatabaseError += ErrorHandler;
            _volunteerProvider.DatabaseError += ErrorHandler;
            _mealAndTransportRatesProvider.DatabaseError += ErrorHandler;
            _ptoStipendRates.DatabaseError += ErrorHandler;
            _studentProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            InitializeComponent();

            _needsViewModel = new NeedsViewModel(_volunteerProvider, _studentProvider, _dialogProvider);
            dtgNeeds.DataContext = _needsViewModel;

            _conditionsViewModel = new ConditionsViewModel(_volunteerProvider, _studentProvider, _dialogProvider);
            dtgCondtions.DataContext = _conditionsViewModel;

            updateDemo();
            initDeleted();
            initCurrentTransportRates();
            initCurrentPTOStipendRates();

            Loaded += Page_Loaded;
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
        /// Actions to perform when page is tabbed to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _needsViewModel.RefreshData();
            _conditionsViewModel.RefreshData();
        }

        /// <summary>
        /// Function Name: updateDemo()
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     refresh the Demographics Datagrid.
        /// </summary>
        public void updateDemo()
        {
            loaded = true;
            if (rdoGender.IsChecked == true)
            {
                dtgDemographics.ItemsSource = _volunteerProvider.GetGenderNameAndId(false).ToList();
                if (errorFlag) { errorFlag = false; return; }
            }
            else if (rdoIdentifiesAs.IsChecked == true)
            {
                dtgDemographics.ItemsSource = _volunteerProvider.GetIdentifiesAsNameAndId(false).ToList();
                if (errorFlag) { errorFlag = false; return; }
            }
            else if (rdoEthnicity.IsChecked == true)
            {
                dtgDemographics.ItemsSource = _volunteerProvider.GetEthnityNameAndId(false).ToList();
                if (errorFlag) { errorFlag = false; return; }
            }
            else if (rdoRacialGroup.IsChecked == true)
            {
                dtgDemographics.ItemsSource = _volunteerProvider.GetRacialGroupNameAndId(false).ToList();
                if (errorFlag) { errorFlag = false; return; }
            }
            else if (rdoExpense.IsChecked == true)
            {
                dtgDemographics.ItemsSource = _expenseProvider.GetAllExpenseTypes();
                if (errorFlag) { errorFlag = false; return; }
            }
            else if (rdoReason.IsChecked == true)
            {
                dtgDemographics.ItemsSource = _volunteerProvider.GetReasonSeparatedNameAndId(false).ToList();
                if (errorFlag) { errorFlag = false; return; }
            }
            else if (rdoTempInfo.IsChecked == true)
            {
                dtgDemographics.ItemsSource = _volunteerProvider.GetTempInfoNameAndId(false).ToList();
                if (errorFlag) { errorFlag = false; return; }
            }
        }

        /// <summary>
        /// Function Name: initDeleted
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     refresh the datagrid of the deleted volunteers.
        /// </summary>
        private void initDeleted()
        {
            List<Volunteer> Volunteers = new List<Volunteer>();
            foreach(var item in _volunteerProvider.GetAllVolunteers().ToList())
            {
                if (errorFlag) { errorFlag = false; return; }
                if (item.IsDeleted == true)
                    Volunteers.Add(item);
            }
            if (errorFlag) { errorFlag = false; return; }
            dtgDeleted.ItemsSource = Volunteers;
        }

        /// <summary>
        /// Function Name: addRow
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Reads the form information and updates the database table based on
        ///     the selected Radio button.
        /// </summary>
        public void addRow(object sender, RoutedEventArgs e)
        {
            if (rdoGender.IsChecked == true)
            {
                string item = txtAddNew.Text;
                _volunteerProvider.AddGenderItem(item);
                if (errorFlag) { errorFlag = false; return; }
                txtAddNew.Text = "";
                updateDemo();
            }
            else if (rdoIdentifiesAs.IsChecked == true)
            {
                string item = txtAddNew.Text;
                _volunteerProvider.AddIdentifiesAsItem(item);
                if (errorFlag) { errorFlag = false; return; }
                txtAddNew.Text = "";
                updateDemo();
            }
            else if (rdoEthnicity.IsChecked == true)
            {
                string item = txtAddNew.Text;
                _volunteerProvider.AddEthnicityItem(item);
                if (errorFlag) { errorFlag = false; return; }
                txtAddNew.Text = "";
                updateDemo();
            }
            else if (rdoRacialGroup.IsChecked == true)
            {
                string item = txtAddNew.Text;
                _volunteerProvider.AddRacialGroupItem(item);
                if (errorFlag) { errorFlag = false; return; }
                txtAddNew.Text = "";
                updateDemo();
            }
            else if (rdoExpense.IsChecked == true)
            {
                string item = txtAddNew.Text;
                _expenseProvider.AddNewExpenseType(item);
                if (errorFlag) { errorFlag = false; return; }
                txtAddNew.Text = "";
                updateDemo();
            }
            else if (rdoReason.IsChecked == true)
            {
                string item = txtAddNew.Text;
                _volunteerProvider.AddReasonSeparatedItem(item);
                if (errorFlag) { errorFlag = false; return; }
                txtAddNew.Text = "";
                updateDemo();
            }
            else if (rdoTempInfo.IsChecked == true)
            {
                int tempType = -1;
                if (rdoCheckBox.IsChecked == true)
                {
                    tempType = 1;
                }
                else if (rdoDate.IsChecked == true)
                {
                    tempType = 0;
                }
                string item = txtAddNew.Text;
                if (tempType == 0 || tempType == 1)
                {
                    _volunteerProvider.AddTempInfoItem(item, tempType);
                    if (errorFlag) { errorFlag = false; return; }
                    txtAddNew.Text = "";
                }

                updateDemo();
            }

        }

        /// <summary>
        /// Function Name: delRow
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Deletes the selected item in the datagrid from the database.
        /// </summary>
        public void delRow(object sender, RoutedEventArgs e)
        {
            if (rdoGender.IsChecked == true) { 
                GenderNameIdModel selectedData = (GenderNameIdModel)dtgDemographics.SelectedItem;
                if (selectedData == null) return;
                if (_volunteerProvider.GetVolunteersWithGender(selectedData.Tuid).Count() > 0)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    System.Windows.MessageBox.Show("Can't Delete. People are using this Gender");
                }
                else if (System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    _volunteerProvider.DeleteGenderItem(selectedData);
                    if (errorFlag) { errorFlag = false; return; }
                }
                if (errorFlag) { errorFlag = false; return; }
            }

            else if (rdoIdentifiesAs.IsChecked == true)
            {
                IdentifiesAsNameIdModel selectedData = (IdentifiesAsNameIdModel)dtgDemographics.SelectedItem;
                if (_volunteerProvider.GetVolunteersWithIdentifiesAs(selectedData.Tuid).Count() > 0)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    System.Windows.MessageBox.Show("Can't Delete. People are using this Identity");
                }
                else if (System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    _volunteerProvider.DeleteIdentifiesAsItem(selectedData);
                    if (errorFlag) { errorFlag = false; return; }
                }
                if (errorFlag) { errorFlag = false; return; }
            }

            else if (rdoEthnicity.IsChecked == true)
            {
                EthnicityNameIdModel selectedData = (EthnicityNameIdModel)dtgDemographics.SelectedItem;
                if (_volunteerProvider.GetVolunteersWithEthnicity(selectedData.Tuid).Count() > 0)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    System.Windows.MessageBox.Show("Can't Delete. People are using this Ethnicity");
                }
                else if (System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    _volunteerProvider.DeleteEthnicityItem(selectedData);
                    if (errorFlag) { errorFlag = false; return; }
                }
                if (errorFlag) { errorFlag = false; return; }
            }

            else if (rdoRacialGroup.IsChecked == true)
            {
                RacialGroupNameIdModel selectedData = (RacialGroupNameIdModel)dtgDemographics.SelectedItem;
                if (_volunteerProvider.GetVolunteersWithRacialGroup(selectedData.Tuid).Count() > 0)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    System.Windows.MessageBox.Show("Can't Delete. People are using this Racial Group");
                }
                else if (System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    _volunteerProvider.DeleteRacialGroupItem(selectedData);
                    if (errorFlag) { errorFlag = false; return; }
                }
                if (errorFlag) { errorFlag = false; return; }
            }

            else if (rdoExpense.IsChecked == true)
            {
                ExpenseTypeModel selectedData = (ExpenseTypeModel)dtgDemographics.SelectedItem;
                if (_expenseProvider.GetExpenseByExpenseTypeId(selectedData.Tuid) != null)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    System.Windows.MessageBox.Show("In Use");
                }
                else if (System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    _expenseProvider.DelExpenseType(selectedData);
                    if (errorFlag) { errorFlag = false; return; }
                }
                if (errorFlag) { errorFlag = false; return; }
            }

            else if (rdoReason.IsChecked == true)
            {
                InactiveStatusTypeItem selectedData = (InactiveStatusTypeItem)dtgDemographics.SelectedItem;
                if (_volunteerProvider.GetVolunteersWithReasonSeparated(selectedData.Tuid).Count() > 0)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    System.Windows.MessageBox.Show("Can't Delete. Reason Separated is in use.");
                }
                else if (System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    _volunteerProvider.DeleteReasonSeparatedItem(selectedData);
                    if (errorFlag) { errorFlag = false; return; }
                }
                if (errorFlag) { errorFlag = false; return; }
            }
            else if (rdoTempInfo.IsChecked == true)
            {
                TempInfoModel selectedData = (TempInfoModel)dtgDemographics.SelectedItem;
                if (_volunteerProvider.GetVolunteerWithTempInfoType(selectedData.Tuid).Count() > 0)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    System.Windows.MessageBox.Show("Can't Delete. Reason Separated is in use.");
                }
                else if (System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (errorFlag) { errorFlag = false; return; }
                    _volunteerProvider.DeleteTempInfoItem(selectedData);
                    if (errorFlag) { errorFlag = false; return; }
                }
                if (errorFlag) { errorFlag = false; return; }
            }

            updateDemo();
        }
        /// <summary>
        /// Function Name: rdoGender_Checked
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     changes the header in the datagrid to match the radio butotn.
        ///     Then calls the updated table with the new information
        /// </summary>
        private void rdoGender_Checked(object sender, RoutedEventArgs e)
        {
            if (!loaded) return;
            colName.Header = "Gender";
            updateDemo();
        }

        /// <summary>
        /// Function Name: rdoIdentifiesAs_Checked
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     changes the header in the datagrid to match the radio butotn.
        ///     Then calls the updated table with the new information
        /// </summary>
        private void rdoIdentifiesAs_Checked(object sender, RoutedEventArgs e)
        {
            colName.Header = "Identifies As";
            if (!loaded) return;
            updateDemo();
        }

        /// <summary>
        /// Function Name: rdoEthnicity_Checked
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     changes the header in the datagrid to match the radio butotn.
        ///     Then calls the updated table with the new information
        /// </summary>
        private void rdoEthnicity_Checked(object sender, RoutedEventArgs e)
        {
            colName.Header = "Ethnicity";
            if (!loaded) return;
            updateDemo();
        }

        /// <summary>
        /// Function Name: rdoRacialGroup_Checked
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     changes the header in the datagrid to match the radio butotn.
        ///     Then calls the updated table with the new information
        /// </summary>
        private void rdoRacialGroup_Checked(object sender, RoutedEventArgs e)
        {
            colName.Header = "Racial Group";
            if (!loaded) return;
            updateDemo();
        }

        /// <summary>
        /// Function Name: rdoExpense_Checked
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     changes the header in the datagrid to match the radio butotn.
        ///     Then calls the updated table with the new information
        /// </summary>
        private void rdoExpense_Checked(object sender, RoutedEventArgs e)
        {
            colName.Header = "Expenses";
            if (!loaded) return;
            updateDemo();
        }

        /// <summary>
        /// Function Name: rdoReason_Checked
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     changes the header in the datagrid to match the radio butotn.
        ///     Then calls the updated table with the new information
        /// </summary>
        private void rdoReason_Checked(object sender, RoutedEventArgs e)
        {
            colName.Header = "Depart Reason";
            if (!loaded) return;
            updateDemo();
        }

        /// <summary>
        /// Function Name: rdoTempInfo_Checked
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     changes the header in the datagrid to match the radio butotn.
        ///     Then calls the updated table with the new information
        /// </summary>
        private void rdoTempInfo_Checked(object sender, RoutedEventArgs e)
        {
            colName.Header = "Temporary Info";
            if (!loaded) return;
            updateDemo();
        }

        /// <summary>
        /// Function Name: restoreVolunteer
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     sets the status of the selected volunteer to not be deleted and inactive.
        /// </summary>
        private void restoreVolunteer(object sender, RoutedEventArgs e)
        {
            Volunteer selectedData = (Volunteer)dtgDeleted.SelectedItem;
            _volunteerProvider.RestoreVolunteer(selectedData.Tuid);
            if (errorFlag) { errorFlag = false; return; }
            System.Windows.MessageBox.Show("Volunteer: " + selectedData.FullName + " has been restored.");
            initDeleted();
        }

        /// <summary>
        /// Function Name: initCurrentTransportRates
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     initializes the Transport Rate textboxes to contain their current values
        /// </summary>
        private void initCurrentTransportRates()
        {
            DateTime monthYear = new DateTime((int)DateTime.Now.Year, (int)DateTime.Now.Month, 1);

            //Get data from VolunteerDatabaseProvider
            VolunteersFinancialsRatesModel? curRates = _serviceProvider.GetRequiredService<IVolunteerProvider>()
                .GetAllVolunteerFinancialRates(
                _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteerFinancalsPtoStipendRates(monthYear)
                , _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteerFinancialsMealTransportRates(monthYear));

            txtCurrentBusValue.Text = "" + curRates.CurrentBusRideRate;
            txtCurrentMileageValue.Text = "" + curRates.CurrentMileageRate;
            txtYearlyMealValue.Text = "" + curRates.YearlyMealValue;

        }

        /// <summary>
        /// Function Name: initCurrentPTOStipendRates
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     initializes the PTO and Stipend textboxes to contain their current values
        /// </summary>
        private void initCurrentPTOStipendRates()
        {
            DateTime monthYear = new DateTime((int)DateTime.Now.Year, (int)DateTime.Now.Month, 1);

            //Get data from VolunteerDatabaseProvider
            VolunteersFinancialsRatesModel? curRates = _serviceProvider.GetRequiredService<IVolunteerProvider>()
                .GetAllVolunteerFinancialRates(
                _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteerFinancalsPtoStipendRates(monthYear)
                , _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteerFinancialsMealTransportRates(monthYear));

            txtCurrentPTORate.Text = "" + curRates.CurrentPtoRate;
            txtCurrentStipendRate.Text = "" + curRates.CurrentStipendRate;

        }

        /// <summary>
        /// Function Name: SavePTOStipends
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Saves the PTO Stipend Form to the database. Updates the Textboxes to show those values.
        /// </summary>
        private void SavePTOStipends(object sender, RoutedEventArgs e)
        {
            PTOStipendRate Rates = new PTOStipendRate();
            double val;
            if (double.TryParse(txtCurrentPTORate.Text, out val) &&
                double.TryParse(txtCurrentStipendRate.Text, out val)
                )
            {
                Rates.PTORate = Double.Parse(txtCurrentPTORate.Text);
                Rates.StipendRate = Double.Parse(txtCurrentStipendRate.Text);
                Rates.Date = new DateTime((int)DateTime.Now.Year, (int)DateTime.Now.Month, 1);
                _ptoStipendRates.PushPTOStipend(Rates);
                if (errorFlag) { errorFlag = false; return; }
                System.Windows.MessageBox.Show("Saved PTO and Stipend Rates.");
                _refreshEventBroker.Publish("UsersAdmin");
            }
            else System.Windows.MessageBox.Show("Use Numeric Values.");
            initCurrentPTOStipendRates();

        }

        /// <summary>
        /// Function Name: SaveTransportRates
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Saves the Transport Rates Form to the database. Updates the Textboxes to show those values.
        /// </summary>
        private void SaveTransportRates(object sender, RoutedEventArgs e)
        {
            MealTransportRate Rates = new MealTransportRate();
            double val;
            if (double.TryParse(txtCurrentBusValue.Text, out val) &&
                double.TryParse(txtCurrentMileageValue.Text, out val) &&
                double.TryParse(txtCurrentBusValue.Text, out val)
                ) {
                Rates.MealRate = Double.Parse(txtYearlyMealValue.Text);
                Rates.MileageRate = Double.Parse(txtCurrentMileageValue.Text);
                Rates.BusMileageRate = Double.Parse(txtCurrentBusValue.Text);
                Rates.Date = new DateTime((int)DateTime.Now.Year, (int)DateTime.Now.Month, 1);

                _mealAndTransportRatesProvider.PushMealTransportRates(Rates);
                if (errorFlag) { errorFlag = false; return; }
                System.Windows.MessageBox.Show("Saved Mileage Transport Rates.");
                _refreshEventBroker.Publish("UsersAdmin");
            }
            else System.Windows.MessageBox.Show("Use Numeric Values.");
            initCurrentTransportRates();
        }

        /// <summary>
        /// Function Name: setTotalGrantStipend
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Saves the FocusTotal Grant Stipend Form to the database.
        /// </summary>
        private void setTotalGrantStipend(object sender, RoutedEventArgs e)
        {
            if (dpGrantDate.SelectedDate != null)
            {
                GrantStipend grant = new GrantStipend();
                grant.Date = (DateTime)dpGrantDate.SelectedDate;
                decimal amount;
                if(decimal.TryParse(txtGrantAmount.Text, out amount)){
                    grant.StartValue = amount;
                    if (grant.Date.CompareTo(DateTime.Now) < 0)
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show("You selected a previous date. Changing this could cause prior data to be recalculated. Do you Wish to Continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            _ptoStipendRates.AddTotalGrantStipend(grant);
                            if (errorFlag) { errorFlag = false; return; }
                            System.Windows.MessageBox.Show("Grant Amount " + grant.StartValue + " set for Date " + grant.Date + ".");
                        }
                    }
                    else {
                        _ptoStipendRates.AddTotalGrantStipend(grant);
                        if (errorFlag) { errorFlag = false; return; }
                        System.Windows.MessageBox.Show("Grant Amount " + grant.StartValue + " set for Date " + grant.Date + ".");
                    }
                 
                }
                else
                {
                    System.Windows.MessageBox.Show("Enter a Numerical value.");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please Pick a Date");
            }
        }

        /// <summary>
        /// Function Name: resetAssignments.
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Will delete all child assignments from the database.
        /// </summary>
        private void resetAssignments(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to clear all current child assignments? This action cannot be undone, and all Classrooms, Students, and Assignments will be deleted."
                , "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) 
            {
                if (_assignmentProvider.DeleteChildAssignments())
                {
                    if (errorFlag) { errorFlag = false; return; }
                    GrowlHelpers.Success("Successfully deleted all child assignments and corresponding data");
                }
                else
                {
                    if (errorFlag) { errorFlag = false; return; }
                    GrowlHelpers.Error("An error occurred while deleting child assignments");
                }
                
            }
        }

        /// <summary>
        /// Open edit window for the currently selected need and display confirmation message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void Edit_Need_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_needsViewModel.SelectedNeed != null)
            {
                UserAdminTaskNeedUpdate needUpdateWindow = new UserAdminTaskNeedUpdate(_serviceProvider, _needsViewModel);
                needUpdateWindow.Owner = Application.Current.MainWindow;
                needUpdateWindow.ShowDialog();
                DisplaySavedStatusGrowl(_needsViewModel.saveSuccess);
                _needsViewModel.SelectedNeed = null;
                _needsViewModel.saveSuccess = false;
            }
        }

        /// <summary>
        /// Confirmation message.
        /// </summary>
        /// <param name="success"></param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void DisplaySavedStatusGrowl(bool success)
        {
            if (success)
            {
                GrowlHelpers.Success("Save Successful.");
            }
        }

        /// <summary>
        /// Open edit window for currently selected condition and show confirmation if success.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void Edit_Condition_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_conditionsViewModel.SelectedCondition != null)
            {
                UserAdminTaskConditionUpdate conditionUpdateWindow = new UserAdminTaskConditionUpdate(_serviceProvider, _conditionsViewModel);
                conditionUpdateWindow.Owner = Application.Current.MainWindow;
                conditionUpdateWindow.ShowDialog();
                DisplaySavedStatusGrowl(_conditionsViewModel.saveSuccess);
                _conditionsViewModel.SelectedCondition = null;
                _conditionsViewModel.saveSuccess = false;
            }
        }

    }
}
