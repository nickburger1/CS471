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
using System.Windows.Navigation;
using System.Windows.Shapes;
using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Services;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for FinanceGeneralPage.xaml
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>~2/1/23</created>
    /// <modification>
    ///     <author>Andrew Loesel</author>
    ///     <date>2/23/23</date>
    ///         <change>Redid most of the logic to correspond correctly with the database.</change>
    /// </modification>
    public partial class FinanceGeneralPage : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExpenseProvider _expenseProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private bool errorFlag;

        public FinanceGeneralPage(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _expenseProvider = _serviceProvider.GetRequiredService<IExpenseProvider>();
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _refreshEventBroker = refreshEventBroker;

            _expenseProvider.DatabaseError += ErrorHandler;
            _volunteerProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            InitializeComponent();

            _refreshEventBroker.Subscribe((args, x) =>
            {
                RefreshData();
            });

            RefreshData();
        }
        /* NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS
        NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS NON EVENT FUNCTIONS */


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
        /// This function will use the expense provider to get the year list and expense type list, and then
        /// bind them to their respective comboboxes.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/23/23</created>
        private void InitializeDatasources()
        {

            PopulateExpenseYearList();
            PopulateCostShareYearList();
            //now expense types using the expense provider
            List<ExpenseTypeModel> expenseTypes = new List<ExpenseTypeModel>();
            expenseTypes.Add(new ExpenseTypeModel { Name = "All", Tuid = -1 });
            expenseTypes.Add(new ExpenseTypeModel { Name = "Donations", Tuid = -2 });
            expenseTypes.AddRange(_expenseProvider.GetAllExpenseTypes());
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbExpenseType.ItemsSource = expenseTypes;
            if (expenseTypes != null)
            {
                cmbExpenseType.SelectedIndex = 0;
            }

            cmbExpenseType.SelectionChanged += cmbExpenseType_SelectionChanged;

            //just hardcode grant and fiscal years
            List<string> yearTypes = new List<string>();
            yearTypes.Add("Grant");
            yearTypes.Add("Fiscal");
            cmbYearType.ItemsSource = yearTypes;
            cmbYearType.SelectedIndex = 0;

            cmbYearType.SelectionChanged += cmbYearType_SelectionChanged;

            //now we need to get the volunteers
            List<VolunteerModel> VolunteerList = _volunteerProvider.GetAllVolunteersIncludeInactive();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbFilterName.ItemsSource = VolunteerList;

            cmbFilterName.SelectionChanged += cmbFilterName_SelectionChanged;


        }

        #region yearComboPopulation
        /// <summary>
        /// this method uses the expense provider to get a list of year ranges that may have in-kind expenses, and then sets that list of years to cmbYearRanges itemssource
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void PopulateExpenseYearList()
        {
            cmbYearRange.SelectionChanged -= cmbYearRange_SelectionChanged;

            IEnumerable<string> expenseYearList = _expenseProvider.GetExpenseYearRanges();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbYearRange.ItemsSource = expenseYearList;
            if (expenseYearList != null)
            {
                cmbYearRange.SelectedIndex = expenseYearList.Count() - 1;
            }


            cmbYearRange.SelectionChanged += cmbYearRange_SelectionChanged;
        }

        /// <summary>
        /// we want to get all the year ranges that have cost share entries from the database
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private void PopulateCostShareYearList()
        {
            //unbind the cost costShare year combobox's on change
            cmbSchoolCostShareYear.SelectionChanged -= cmbSchoolCostShareYear_SelectionChanged;
            //now get the list of cost share year
            IEnumerable<string> costShareYearList = _expenseProvider.GetCostShareYears();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            if (costShareYearList != null && costShareYearList.Count() > 0)
            {
                cmbSchoolCostShareYear.ItemsSource = costShareYearList;
                //select the last item in the list, this will be the most current year range
                cmbSchoolCostShareYear.SelectedIndex = costShareYearList.Count() - 1;
                cmbSchoolCostShareYear.SelectionChanged += cmbSchoolCostShareYear_SelectionChanged;
            }
            else
            {
                ShowGrowlInfo("No School Cost Shares were detected, the School Cost Share area will be blank until entries are added");
            }

        }
        #endregion

        #region datagridPopulation
        /// <summary>
        /// here we will set the item source of dtgDonations depending on the selected inKindExpenseType and selected volunteer.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/24/23</created>
        private void PopulateExpenseTable()
        {

            //first get our selected expense type
            ExpenseTypeModel? selectedExpenseType = (ExpenseTypeModel?)cmbExpenseType.SelectedItem;

            if (selectedExpenseType != null && !string.IsNullOrEmpty(selectedExpenseType.Name))
            {
                int intVolunteerTuid = 0;
                if (cmbFilterName.SelectedValue == null)
                {
                    intVolunteerTuid = -1;
                }
                else
                {
                    //if for some reason there was no Tuid attatched to the volunteer in the dropdown, specify -1 as a fallback value
                    intVolunteerTuid = int.Parse(cmbFilterName.SelectedValue.ToString() ?? "-1");
                }
                //before we try a specific expense type we have to first see if it is either of the values added in manually when we set cmbExpenseTypes itemssource.
                int intExpenseTypeTuid = selectedExpenseType.Tuid;
                //-1 is for all in-kind expense types
                if (intExpenseTypeTuid == -1)
                {
                    if (intVolunteerTuid == -1)
                    {
                        var allExpenses = GetAllExpenses(intVolunteerTuid);
                        dtgDonations.ItemsSource = allExpenses;
                        lblDonationTotal.Content = allExpenses.Sum(x => x.Value).ToString("C", CultureInfo.CurrentCulture);
                        //notify the user if no expenses were found with these selections so they don't think it is broken
                        if (allExpenses.Count == 0)
                        {
                            ShowGrowlInfo("Could not find expenses for current selections");
                        }
                    }
                    else
                    {
                        var allExpenses = GetAllExpenses(intVolunteerTuid);
                        allExpenses = allExpenses.Where(x => x.intVolunteerTuid == intVolunteerTuid).ToList();
                        dtgDonations.ItemsSource = allExpenses;
                        lblDonationTotal.Content = allExpenses.Sum(x => x.Value).ToString("C", CultureInfo.CurrentCulture);
                    }
                }
                else if (intExpenseTypeTuid == -2)
                {
                    if (intVolunteerTuid == -1)
                    {
                        //this is just dontaions
                        var donations = GetDonations();
                        dtgDonations.ItemsSource = donations;
                        lblDonationTotal.Content = donations.Sum(x => x.Value).ToString("C", CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        var donations = GetDonations();
                        donations = donations.Where(x => x.intVolunteerTuid == intVolunteerTuid).ToList();
                        dtgDonations.ItemsSource = donations;
                        lblDonationTotal.Content = donations.Sum(x => x.Value).ToString("C", CultureInfo.CurrentCulture);
                    }

                }
                else
                {
                    //we are looking for a specific expense type
                    var expenses = GetExpenses(selectedExpenseType.Tuid, intVolunteerTuid);
                    dtgDonations.ItemsSource = expenses;
                    lblDonationTotal.Content = expenses.Sum(x => x.Value).ToString("C", CultureInfo.CurrentCulture);
                }
            }
        }



        /// <summary>
        /// This method will populate all quarters billing values and dates, and the total labels in the school cost share area
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/25/23</created>
        private void PopulateCostShare()
        {
            //get cost share dates
            DateTime[]? propertDates = GetCostShareDates();
            if (propertDates != null)
            {
                IEnumerable<SchoolCostShareModel> costShares = _expenseProvider.GetCostShares(propertDates[0], propertDates[1]);
                if (errorFlag) { errorFlag = false; return; }
                dtgCostShare.ItemsSource = costShares;

                lblCostShareTotal.Content = costShares.Sum(x => x.Value).ToString("C", CultureInfo.CurrentCulture);
            }
        }
        #endregion

        #region GettingIn-KindExpenses
        /// <summary>
        /// This method using the expense provider to get the donations for the date range in the expense year combo box
        /// </summary>
        /// <returns>a list of inKindExpenseModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private List<InKindExpenseModel> GetDonations()
        {
            DateTime[]? properDates = GetExpenseDates();
            List<InKindExpenseModel> donations = new List<InKindExpenseModel>();
            if (properDates != null)
            {
                donations = _expenseProvider.GetDonations(properDates[0], properDates[1]).ToList();
                if (errorFlag) { errorFlag = false; return new List<InKindExpenseModel>(); }
            }
            return donations;

        }

        /// <summary>
        /// this method gets all expenses and donations and returns them as a single list
        /// </summary>
        /// <param name="intVolunteerTuid">the selected volunteer tuid derived from the filter name list.</param>
        /// <returns>a list of InKindExpenseModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private List<InKindExpenseModel> GetAllExpenses(int intVolunteerTuid)
        {
            List<InKindExpenseModel> allExpenes = new List<InKindExpenseModel>();
            DateTime[]? properDates = GetExpenseDates();
            if (properDates != null)
            {
                var expenses = _expenseProvider.GetExpensesForDateRange(properDates[0], properDates[1], -1, intVolunteerTuid).ToList();
                if (errorFlag) { errorFlag = false; return new List<InKindExpenseModel>(); }
                if (expenses != null)
                {
                    allExpenes.AddRange(expenses);
                }
                //now get donations
                var donations = _expenseProvider.GetDonations(properDates[0], properDates[1]);
                if (errorFlag) { errorFlag = false; return new List<InKindExpenseModel>(); }
                if (donations != null)
                {
                    allExpenes.AddRange(donations);
                }
            }
            return allExpenes;
        }

        /// <summary>
        /// This method will get the expenses of the selected expense type and the selected volunteer
        /// </summary>
        /// <param name="intExpenseTypeTuid">the selected expense type tuid</param>
        /// <param name="intVolunteerTuid">the selected volunteers tuid</param>
        /// <returns>a list of inkindexpensemodels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private List<InKindExpenseModel> GetExpenses(int intExpenseTypeTuid, int intVolunteerTuid)
        {
            DateTime[]? properDates = GetExpenseDates();
            List<InKindExpenseModel> expenses = new List<InKindExpenseModel>();
            if (properDates != null)
            {
                expenses = _expenseProvider.GetExpensesForDateRange(properDates[0], properDates[1], intExpenseTypeTuid, intVolunteerTuid).ToList();
                if (errorFlag) { errorFlag = false; return new List<InKindExpenseModel>(); }
            }
            return expenses;
        }
        #endregion

        #region GetDatesFromYearRangeControls
        /// <summary>
        /// This method uses the yearRange and yearType combobexes to derive a start and end date
        /// </summary>
        /// <returns>an array of datetimes or null</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private DateTime[]? GetExpenseDates()
        {
            if (cmbYearRange.SelectedItem != null && cmbYearType.SelectedItem != null)
            {
                string? strYears = cmbYearRange.SelectedItem.ToString();
                string? strYearType = cmbYearType.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(strYears) && !string.IsNullOrEmpty(strYearType))
                {
                    string strStartYear = strYears.Split("-")[0];
                    int intStartYear = Convert.ToInt32(strStartYear);
                    string strEndYear = strYears.Split("-")[1];
                    int intEndYear = Convert.ToInt32(strEndYear);
                    if (strYearType.Equals("Grant"))
                    {
                        //6-1 to 5-30
                        return new DateTime[] { new DateTime(intStartYear, 6, 1), new DateTime(intEndYear, 5, 30, 11, 59, 59) };
                    }
                    else
                    {
                        //fiscal year is 10-1 to 9-30
                        return new DateTime[] { new DateTime(intStartYear, 10, 1), new DateTime(intEndYear, 9, 30, 11, 59, 59) };
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will get the start and end date for the selected cost share year range
        /// </summary>
        /// <returns>an array of datetimes</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private DateTime[]? GetCostShareDates()
        {
            if (cmbSchoolCostShareYear.SelectedItem != null)
            {
                string? strYears = cmbSchoolCostShareYear.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(strYears))
                {
                    string strStartYear = strYears.Split("-")[0];
                    int intStartYear = Convert.ToInt32(strStartYear);
                    string strEndYear = strYears.Split("-")[1];
                    int intEndYear = Convert.ToInt32(strEndYear);
                    //fiscal year is 10-1 to 9-30, I will use fiscal year for this as in Tara's example the 1st cost share billing was 12/29, which is right at the end of the first fiscal quarter
                    return new DateTime[] { new DateTime(intStartYear, 10, 1), new DateTime(intEndYear, 9, 30, 11, 59, 59) };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// This method displays a growl info message with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlInfo(string strMessage)
        {
            Growl.Info(new GrowlInfo
            {
                Message = strMessage,
                ShowDateTime = false,
                WaitTime = 2,
                StaysOpen = false,

            });
        }
        /// <summary>
        /// This method displays a growl warning message with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlWarning(string strMessage)
        {
            Growl.Warning(new GrowlInfo
            {
                Message = strMessage,
                ShowDateTime = false,
                WaitTime = 2,
                StaysOpen = false,

            });
        }

        /* EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS 
        EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS EVENT FUNCTIONS */
        #region EventFunctions

        /// <summary>
        /// This function will run when the text input into txtFilterName. When the text is changed we will start filtering the 
        /// donations by the text value in txtFilterName.We will use our database helper class's method to search for donations 
        /// by donor name.
        /// </summary>
        /// <param name="sender">the control that fired the event.</param>
        /// <param name="e">the event arguments for the event.</param>
        /// <author>Andrew Loesel</author>
        /// <created>1/28/23</created>
        private void cmbFilterName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateExpenseTable();

        }

        /// <summary>
        /// This function will run when the expense type combobox selection is changed. We want to filter <data> of the table by the new expense type.
        /// </summary>
        /// <param name="sender">the control that fired the event.</param>
        /// <param name="e">the event arguments for the event.</param>
        /// <author>Andrew Loesel</author>
        /// <created>1/28/23</created>
        private void cmbExpenseType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateExpenseTable();
        }

        /// <summary>
        /// This function will run when the Year Range combobox selection is changed. We want to filter <data> of the table by 
        /// the new year range.
        /// </summary>
        /// <param name="sender">the control that fired the event.</param>
        /// <param name="e">the event arguments for the event.</param>
        /// <author>Andrew Loesel</author>
        /// <created>1/28/23</created>
        private void cmbYearRange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateExpenseTable();
        }

        /// <summary>
        /// This function will run when the Year Type combobox selection is changed. We want to filter <data> of the table by the
        /// new year type.
        /// </summary>
        /// <param name="sender">the control that fired the event.</param>
        /// <param name="e">the event arguments for the event.</param>
        /// <author>Andrew Loesel</author>
        /// <created>1/28/23</created>
        private void cmbYearType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateExpenseTable();
        }

        /// <summary>
        /// This is the SelectionChanged event method for our cost share year control. It will fire each time the cost share selected year is changed.
        /// We will populate cost share method which will grab data for this year.
        /// </summary>
        /// <param name="sender">The object that fired this event</param>
        /// <param name="e">The event arguments for this event</param>
        /// <author>Andrew Loesel</author>
        /// <creatd>2/5/2023</creatd>
        private void cmbSchoolCostShareYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateCostShare();
        }

        /// <summary>
        /// This method fires when the add cost share button is clicked, and creates and opens a new addOrEditCostShareDialog
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the arguments for the event</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/23/2023</created>
        private void btnAddCostShare_Click(object sender, RoutedEventArgs e)
        {

            AddOrEditCostShare addCostShareWindow = new AddOrEditCostShare(_serviceProvider, _expenseProvider);
            addCostShareWindow.Title = "Add School Cost Share";
            addCostShareWindow.ShowDialog();
            bool? result = addCostShareWindow.DialogResult;
            Growl.GrowlPanel = null;
            if (result == true)
            {
                var costShareYearList = _expenseProvider.GetCostShareYears();
                if (errorFlag) { errorFlag = false; return; }
                if (cmbSchoolCostShareYear.Items.Count < costShareYearList.Count)
                {
                    var selectedYear = cmbSchoolCostShareYear.SelectedItem;
                    try
                    {
                        PopulateCostShareYearList();
                    }
                    catch(RefreshDataCustomException rdce)
                    {
                        return;
                    }
                    if (selectedYear != null)
                    {
                        cmbSchoolCostShareYear.SelectedItem = selectedYear;
                    }

                }
                else
                {
                    PopulateCostShare();
                }
                ShowGrowlInfo("Added School Cost Share");
            }


        }

        /// <summary>
        /// This method will open a new AddOrEditExpense dialog, and catch the result of that dialog, and refresh the expense table if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private void btnAddExpense_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditExpense addExpense = new AddOrEditExpense(_serviceProvider, _expenseProvider, (IEnumerable<VolunteerModel>)cmbFilterName.ItemsSource);
            addExpense.Title = "Add In-Kind Expense";
            addExpense.ShowDialog();
            bool? added = addExpense.DialogResult;
            Growl.GrowlPanel = null;
            if (added == true)
            {
                //see if the size of the year list is the same as cmbYearList's items
                var yearList = _expenseProvider.GetExpenseYearRanges();
                if (errorFlag) { errorFlag = false; return; }
                if (cmbYearRange.Items.Count < yearList.Count)
                {
                    var selectedYear = cmbYearRange.SelectedItem;
                    try
                    {
                        PopulateExpenseYearList();
                    }catch(RefreshDataCustomException rdce)
                    {
                        return;
                    }
                    if (selectedYear != null)
                    {
                        cmbYearRange.SelectedItem = selectedYear;
                    }
                }
                else
                {
                    PopulateExpenseTable();
                }
                ShowGrowlInfo("Added new In-Kind expense");
            }

        }

        /// <summary>
        /// This method fires when the edit button for the expenses is clicked, we open an AddOrEditExpense dialog and pass the currently selected expense.
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the event arguments</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/23/2023</created>
        private void btnFinanceExpenseEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgDonations.SelectedItem == null)
            {
                ShowGrowlWarning("Please select an In-Kind expense entry to edit");
            }
            else
            {
                AddOrEditExpense editExpense = new AddOrEditExpense(_serviceProvider, _expenseProvider, (IEnumerable<VolunteerModel>)cmbFilterName.ItemsSource, (InKindExpenseModel)dtgDonations.SelectedItem);
                editExpense.Title = "Edit In-Kind Expense";
                editExpense.ShowDialog();
                Growl.GrowlPanel = null;
                bool? result = editExpense.DialogResult;
                if (result == true)
                {
                    //see if the size of the year list is the same as cmbYearList's items
                    var yearList = _expenseProvider.GetExpenseYearRanges();
                    if (errorFlag) { errorFlag = false; return; }
                    if (cmbYearRange.Items.Count < yearList.Count)
                    {
                        var selectedYear = cmbYearRange.SelectedItem;
                        try
                        {
                            PopulateExpenseYearList();
                        }
                        catch (RefreshDataCustomException rdce)
                        {
                            return;
                        }
                        if (selectedYear != null)
                        {
                            cmbYearRange.SelectedItem = selectedYear;
                        }
                    }
                    else
                    {
                        PopulateExpenseTable();
                    }
                    //refresh the expense table
                    ShowGrowlInfo("In-Kind expense was updated/deleted");
                }

            }
        }
        /// <summary>
        /// This method fires when the edit button for the cost share is clicked, we open a new AddOrEditCostShare dialog, and pass it the current values of the object to edit
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the arguments of the event</param>
        /// <author>Brendan Breuss</author>
        /// <history> 
        ///     Created: Jan 30, 2023
        ///     Last Modified: March 22, 2023   
        ///     <mod> Andrew Loesel : 3/22/2023 - edit button now opens the add or edit cost share dialog.
        /// </history>
        private void BtnFinancialGeneralEdit_Click(object sender, RoutedEventArgs e)
        {
            SchoolCostShareModel? selectedItem = dtgCostShare.SelectedItem as SchoolCostShareModel;
            if (selectedItem != null)
            {
                //since we are editing we will pass in the selectedItem
                AddOrEditCostShare editCostShare = new AddOrEditCostShare(_serviceProvider, _expenseProvider, selectedItem);
                editCostShare.txtDate.SelectedDate = selectedItem.Date;
                editCostShare.txtName.Text = selectedItem.Name;
                editCostShare.txtValue.Text = double.Parse(selectedItem.Value.ToString()).ToString();
                editCostShare.ShowDialog();
                bool? result = editCostShare.DialogResult;
                if (result == true)
                {
                    var costShareYearList = _expenseProvider.GetCostShareYears();
                    if (errorFlag) { errorFlag = false; return; }
                    if (cmbSchoolCostShareYear.Items.Count < costShareYearList.Count)
                    {
                        var selectedYear = cmbSchoolCostShareYear.SelectedItem;
                        try
                        {
                            PopulateCostShareYearList();
                        }
                        catch (RefreshDataCustomException rdce)
                        {
                            return;
                        }
                        if (selectedYear != null)
                        {
                            cmbSchoolCostShareYear.SelectedItem = selectedYear;
                        }

                    }
                    else
                    {
                        PopulateCostShare();
                    }
                    ShowGrowlInfo("School Cost Share was edited/deleted");


                }
            }
            else
            {
                Growl.Warning(new GrowlInfo
                {
                    Message = "Please select a School Cost Share entry to edit",
                    StaysOpen = false,
                    ShowDateTime = false,
                    WaitTime = 2
                });
            }
        }

        /// <summary>
        /// This method will run when the export to button is clicked. We go through and make a sheet, and the add both the cost share and expense table
        /// data to that sheet as ExcelTableModels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/3/2023</created>
        private void btnFinancialGeneralExport_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCostShare.Items.Count > 0 && dtgDonations.Items.Count > 0)
            {
                List<object> schoolCostShares = new List<object>();
                foreach (SchoolCostShareModel schoolCostShareModel in dtgCostShare.Items)
                {
                    schoolCostShares.Add(new
                    {
                        Name = schoolCostShareModel.Name,
                        Date = schoolCostShareModel.Date.ToString("d"),
                        Value = (decimal)schoolCostShareModel.Value,
                    });
                }
                List<object> expenses = new List<object>();
                foreach (InKindExpenseModel expense in dtgDonations.Items)
                {
                    expenses.Add(new
                    {
                        DonorVolunteerName = expense.VolunteerDonorName,
                        Type = expense.ExpenseTypeName,
                        Date = expense.Date.ToString("d"),
                        Value = (decimal)expense.Value
                    });
                }

                ExcelFileModel fileModel = new ExcelFileModel();
                fileModel.FileName = "Expense Report";

                ExcelTableModel costShareTable = new ExcelTableModel();
                costShareTable.Title = "Cost Shares";
                costShareTable.Headers = new string[] { "Cost Share Name", "Date Billed", "Cost Share Value" }.ToList();
                costShareTable.Rows = schoolCostShares;

                ExcelTableModel expenseTable = new ExcelTableModel();
                expenseTable.Title = "In-Kind-Expenses";
                expenseTable.Headers = new string[] { "Volunteer/Donor Name", "In-Kind Expense Type", "Date", "Value" }.ToList();
                expenseTable.Rows = expenses;

                ExcelSheetModel excelSheetModel = new ExcelSheetModel();
                excelSheetModel.Title = "Cost Shares - In-Kinds";
                excelSheetModel.Tables = new List<ExcelTableModel>();
                excelSheetModel.Tables.Add(costShareTable);
                excelSheetModel.Tables.Add(expenseTable);

                fileModel.Sheets = new List<ExcelSheetModel>();
                fileModel.Sheets.Add(excelSheetModel);

                ExcelExporter.ExportToExcel(fileModel);
            }
            else
            {
                ShowGrowlWarning("Cannot export since there are no entries.");
            }

        }
        #endregion

        /// <summary>
        /// This method will refresh all data that may be out of date when changes are made else where
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/5/2023</created>
        private void RefreshData()
        {
            InitializeDatasources();
            PopulateExpenseTable();
            PopulateCostShare();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(FinanceGeneralPage));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
    }
}
