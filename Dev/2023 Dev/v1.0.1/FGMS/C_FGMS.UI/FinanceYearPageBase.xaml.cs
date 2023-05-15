using A_FGMS.DataLayer.EventBroker;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.ViewModels.FiscalGrantViewModels;
using C_FGMS.UI.Helpers;
using DocumentFormat.OpenXml.Math;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace C_FGMS.UI
{
    /// <summary>
    /// The purpose of this file is to provide the interaction logic for the Finance  Year page of the application. 
    /// This file communicates with the database, and uses global business logic.
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>1/26/2023</created>
    /// <modification>
    ///     <author>Andrew Loesel</author>
    ///     <date>2/22/23</date>
    ///         <change>Finished functionality for populating all tables except  year - date table in the four table view
    ///                 and for viewing focussed tables of each of the 3 currently populated tables.</change>
    ///     <date>4/4/23</date>
    ///         <change>Moved most of the logic to a view model to populate tables faster.</change>            
    /// </modification>
    public partial class FinanceYearPageBase : Page
    {
        /* GLOBAL VARIABLES GLOBAL VARIABLES GLOBAL VARIABLES GLOBAL VARIABLES GLOBAL VARIABLES GLOBAL VARIABLES GLOBAL VARIABLES */
        //this is an instance of the global business logic object, which will be used for widespread functionality such as getting a list with every month in it.
        private readonly IServiceProvider _serviceProvider;
        private readonly IInKindExpenseProvider _databaseInKindExpenseProvider;
        private readonly IExpenseProvider _expenseProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private readonly FocusTableViewModel _focusViewModel;
        private readonly string _strFiscalYear = "Fiscal Year";
        private readonly string _strGrantYear = "Grant Year";
        private readonly int _countColumnIndex = 4;
        private readonly int _valueColumnIndex = 5;
        private string _strSelectedYearType = "Grant Year";
        public bool isFiscalYear = false; // Default to grant year.
        private bool errorFlag;

        /// <summary>
        /// This function runs when an instance of FinanceYearPage.xaml is created. This function will initialize the page
        /// component, and will also instanciate the global business logic object.      
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>1/26/2023</created>

        public FinanceYearPageBase(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _databaseInKindExpenseProvider = _serviceProvider.GetService<IInKindExpenseProvider>();
            _expenseProvider = _serviceProvider.GetService<IExpenseProvider>();
            _refreshEventBroker = refreshEventBroker;

            _databaseInKindExpenseProvider.DatabaseError += ErrorHandler;
            _expenseProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshPage();
            });

            _focusViewModel = new FocusTableViewModel(_databaseInKindExpenseProvider, _expenseProvider, _serviceProvider.GetRequiredService<IDialogProvider>(), isFiscalYear);

            DataContext = _focusViewModel;
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
        /// Method so the the page can be refreshed externally. For navigation.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/01/2023</created>
        public void RefreshPage()
        {
            _strSelectedYearType = isFiscalYear ? _strFiscalYear : _strGrantYear;
            _focusViewModel.IsFiscal = isFiscalYear;
            _focusViewModel.SetFocusStartAndEndDates(isFiscalYear);
            SetDateRangeTitle();
            
            _focusViewModel.RefreshData(GridYearFocusedView.Visibility == Visibility.Visible);
        }

        /// <summary>
        /// This function will use the global business logic object instance to get a list of all months, and will set the items 
        /// source for both start month and end month comboboxes as the list of months.This function then sets the selected index 
        /// for start month as january (0) and the end month slected index as the current month.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>1/26/2023</created>
        private void SetDateRangeTitle()
        {
            labelYearDateRange.Content = "Select " + _strSelectedYearType + " Date Range";
        }

        #region MealInKind

        /// <summary>
        /// This purpose of this function is to dynamically display the meal in kind focus table for the  year. We start by 
        /// setting the grid visibility of our normal view's grid to hidden, and then make the focus view grid visible. Then We 
        /// will set the combobox items to an itemssource of the months of the year via function call.We will Then delete the 
        /// prior columns of the focus table(if any) and create the necessary columns for the meal in kind table, and add them
        /// to the set of columns for the datagrid.Then we call a function to populate the datagrid with data from our database.
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">the event arguments for this event</param>
        /// <author>Andrew Loesel</author>
        /// <created>1/26/23</created>
        private void ShowMealInKindFocusTable(object sender, RoutedEventArgs e)
        {
            GridNormalView.Visibility = Visibility.Hidden;
            GridYearFocusedView.Visibility = Visibility.Visible;

            ShowFocusColumns();

            PopulateMealInKindFocusTable();
        }

        /// <summary>
        /// This function populates the focus table with meal in kind items [eventually] retrieved from the database.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>1/26/23</created>
        /// <modification>
        ///     <date>2/22/23</date>
        ///     <author>Andrew Loesel</author>
        ///         <change>
        ///             Added code implementation for populating the focussed table.
        ///         </change>
        /// </modification>
        private void PopulateMealInKindFocusTable()
        {
            _focusViewModel.SetFocusStartAndEndDates(isFiscalYear);

            _focusViewModel.SetMealInKindSummary(_focusViewModel.SelectedFocusStartDate, _focusViewModel.SelectedFocusEndDate, isFiscalYear);


            dtgFocusTable.Columns[_countColumnIndex].Header = _focusViewModel.MealCountHeader;
            dtgFocusTable.Columns[_valueColumnIndex].Header = _focusViewModel.MealValueHeader;
            _focusViewModel.CurrentTitle = _focusViewModel.MealInKindTitle;
        }

        #endregion

        #region Mileage
        /// <summary>
        /// This function will show a focused version of the volunteer mileage table.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/2/2023</created>
        /// <param name="sender">The control that fired this event.</param>
        /// <param name="e">The event arguments for this event.</param>
        private void ShowMileageFocusTable(object sender, RoutedEventArgs e)
        {
            GridNormalView.Visibility = Visibility.Hidden;
            GridYearFocusedView.Visibility = Visibility.Visible;

            ShowFocusColumns();

            PopulateMileageFocusTable();
        }

        /// <summary>
        /// This function will populate the focussed volunteer mileage table with data from the database.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/2/2023</created>
        /// <modification>
        ///     <date>2/22/23</date>
        ///     <author>Andrew Loesel</author>
        ///         <change>
        ///             Added code implementation for populating the focussed table.
        ///         </change>
        /// </modification>
        private void PopulateMileageFocusTable()
        {
            _focusViewModel.SetFocusStartAndEndDates(isFiscalYear);

            _focusViewModel.SetMilageSummary(dtpFocusStartDate.SelectedDate, dtpFocusEndDate.SelectedDate, isFiscalYear);

            dtgFocusTable.Columns[_countColumnIndex].Header = _focusViewModel.MileageCountHeader;
            dtgFocusTable.Columns[_valueColumnIndex].Header = _focusViewModel.MileageValueHeader;
            _focusViewModel.CurrentTitle = _focusViewModel.MilageTitle;
        }

        #endregion

        #region BusTransportationFocus
        /// <summary>
        /// This function will run when the view button is clicked for the Bus transporation table. We will show the focus view grid and populate the 
        /// datagrid with columns corresponding to bus transportation.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/2/2023</created>
        /// <param name="sender">The control that fired this event.</param>
        /// <param name="e">The event arguments for this event.</param>
        private void ShowBusFocusTable(object sender, RoutedEventArgs e)
        {
            GridNormalView.Visibility = Visibility.Hidden;
            GridYearFocusedView.Visibility = Visibility.Visible;

            ShowFocusColumns();

            PopulateBusFocusTable();
        }

        /// <summary>
        /// This method will populate the focus table with data from the database corresponding to the desired interval of bus transportation
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/2/2023</created>
        /// <modification>
        ///     <date>2/22/23</date>
        ///     <author>Andrew Loesel</author>
        ///         <change>
        ///             Added code implementation for populating the focussed table.
        ///         </change>
        /// </modification>
        private void PopulateBusFocusTable()
        {
            _focusViewModel.SetFocusStartAndEndDates(isFiscalYear);

            _focusViewModel.SetBusSummary(dtpFocusStartDate.SelectedDate, dtpFocusEndDate.SelectedDate, isFiscalYear);

            dtgFocusTable.Columns[_countColumnIndex].Header = _focusViewModel.BusCountHeader;
            dtgFocusTable.Columns[_valueColumnIndex].Header = _focusViewModel.BusValueHeader;
            _focusViewModel.CurrentTitle = _focusViewModel.BusRideTitle;
        }
        #endregion

        #region ToDate

        /// <summary>
        /// This method will show the focus version of the year to date total.
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the event arguments</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/15/2023</created>
        private void ShowYearToDateFocusTable(object sender, RoutedEventArgs e)
        { 
            GridNormalView.Visibility = Visibility.Hidden;
            GridYearFocusedView.Visibility = Visibility.Visible;

            HideFocusColumns();

            PopulateYearToDateFocusTable();
        }

        /// <summary>
        /// This method will populate the generated datagrid of the year to date focus with data using the datepicker values.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>3/15/2023</created>
        private void PopulateYearToDateFocusTable()
        {
            _focusViewModel.SetFocusStartAndEndDates(isFiscalYear);

            _focusViewModel.SetYearToDateExpenseSummary(dtpFocusStartDate.SelectedDate, dtpFocusEndDate.SelectedDate, isFiscalYear);

            dtgFocusTable.Columns[_countColumnIndex].Header = _focusViewModel.BusCountHeader;
            dtgFocusTable.Columns[_valueColumnIndex].Header = _focusViewModel.MealValueHeader; // Same as the meal in kind header.
            _focusViewModel.CurrentTitle = _focusViewModel.InKindTitle;
        }

        #endregion

        /// <summary>
        /// This event fires when the back button on the table focus page is clicked. It will hide the focused table grid and make the normal  year grid visible..
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/2/2023</created>
        /// <param name="sender">The control that fired this event.</param>
        /// <param name="e">The event arguments for this event.</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GridYearFocusedView.Visibility = Visibility.Hidden;
            GridNormalView.Visibility = Visibility.Visible;

            // Nothing is being focused.
            _focusViewModel.CurrentTitle = "";
            _focusViewModel.RefreshMainPage();
        }

        private void cmbYearDateRange_Loaded(object sender, RoutedEventArgs e)
        {
            cmbYearDateRange.SelectedIndex = _focusViewModel.SelectedDateRangeIndex;
        }

        private void dtpFocusStartDate_Loaded(object sender, RoutedEventArgs e)
        {
            dtpFocusStartDate.SelectedDate = _focusViewModel.SelectedFocusStartDate;
        }

        private void dtpFocusEndDate_Loaded(object sender, RoutedEventArgs e)
        {
            dtpFocusEndDate.SelectedDate = _focusViewModel.SelectedFocusEndDate;
        }

        /// <summary>
        /// Hide extra focus columns not needed for in kind expenses.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/2023</created>
        private void HideFocusColumns()
        {
            tbkCountColumn.Visibility = Visibility.Hidden;
            tbkDateColumn.Visibility = Visibility.Hidden;
            tbkQuarterColumn.Visibility = Visibility.Hidden;
            tbkRateColumn.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Show columns needed for datagrids other than the in kind expense table.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/2023</created>
        private void ShowFocusColumns()
        {
            tbkCountColumn.Visibility = Visibility.Visible;
            tbkDateColumn.Visibility = Visibility.Visible;
            tbkQuarterColumn.Visibility = Visibility.Visible;
            tbkRateColumn.Visibility = Visibility.Visible;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(FinanceYearPageBase));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
    }
}
