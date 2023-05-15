using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

/// <FileName> FocusTableViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 4/2/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 4/2/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to manage data for the FinanceYearPageBase page.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.FiscalGrantViewModels
{
    public class FocusTableViewModel : ViewModelBase
    {
        private readonly string _retrieveErrorMessage = "Could not retrieve records. Please contact contact support if issue persists.";

        private readonly string _mealInKindTitle = "Meal-In-Kind";
        private readonly string _mileageTitle = "Volunteer Mileage";
        private readonly string _busRideTitle = "Bus Transportation";
        private readonly string _inKindTitle = "In-Kind";

        private readonly string _mealCountHeader = "Meal Count";
        private readonly string _mileCountHeader = "Mileage";
        private readonly string _busCountHeader = "Trips";

        private readonly string _mealValueHeader = "Value";
        private readonly string _mileageValueHeader = "reimbursement";
        private readonly string _busValueHeader = "Billings";

        private ObservableCollection<MealTransportMileageModel> _mealTransportMainPage;
        private ObservableCollection<FinanceFocusGridModel>? _mealTransportMileageList;
        private ObservableCollection<FinanceFocusGridModel>? _yearToDateExpenses;
        private ObservableCollection<string>? _yearRanges;
        private IInKindExpenseProvider _inKindExpenseProvider;
        private IExpenseProvider _expenseProvider;
        private IDialogProvider _dialogProvider;
        private DateTime? _selectedFocusStartDate;
        private DateTime? _selectedFocusEndDate;
        private string _selectedDateRange;
        private string _currentTitle;
        private double _focusTotal;
        private double _mealTotal;
        private double _mileageTotal;
        private double _busTotal;
        private double _inKindTotal;
        private int _selectedDateRangeIndex;
        private int _selectedStartYear;
        private int _selectedEndYear;
        private bool _isFiscal = false;
        public ICommand ExportCommand { get; }
        private bool errorFlag;

        /// <summary>
        /// Title and header values for gridview data.
        /// </summary>

        public string MealInKindTitle
        {
            get { return _mealInKindTitle;}
        }

        public string BusRideTitle
        {
            get { return _busRideTitle;}
        }

        public string MilageTitle
        {
            get { return _mileageTitle;  }
        }

        public string InKindTitle
        {
            get { return _inKindTitle;}
        }

        public string MealCountHeader
        {
            get { return _mealCountHeader;}
        }

        public string MileageCountHeader
        {
            get { return _mileCountHeader; }
        }

        public string BusCountHeader
        {
            get { return _busCountHeader;}
        }

        public string MealValueHeader
        {
            get { return _mealValueHeader; }
        }

        public string MileageValueHeader
        {
            get { return _mileageValueHeader;}
        }

        public string BusValueHeader
        { 
            get { return _busValueHeader; } 
        }

        /// <summary>
        /// Populates meal, mileage, and bus datagrids.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        public ObservableCollection<MealTransportMileageModel> MealTransportMainPage
        {
            get
            {
                return _mealTransportMainPage;
            }
            set
            {
                _mealTransportMainPage = value;
                OnPropertyChanged(nameof(MealTransportMainPage));
            }
        }

        /// <summary>
        /// List of meal or transport values for the focus grid.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/2/23</created>
        public ObservableCollection<FinanceFocusGridModel>? MealTransportMileageList
        {
            get
            {
                return _mealTransportMileageList;
            }
            set
            {
                _mealTransportMileageList = value;
                OnPropertyChanged(nameof(MealTransportMileageList));
            }
        }

        /// <summary>
        /// List of year to date expenses.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        public ObservableCollection<FinanceFocusGridModel>? YearToDateList
        {
            get
            {
                return _yearToDateExpenses;
            }
            set
            {
                _yearToDateExpenses = value;
                OnPropertyChanged(nameof(YearToDateList));
            }
        }

        /// <summary>
        /// Year ranges in string form.
        /// <example>2022-2023</example>
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        public ObservableCollection<string>? YearRanges
        {
            get
            {
                return _yearRanges;
            }
            set
            {
                _yearRanges = value;
                OnPropertyChanged(nameof(YearRanges));
            }
        }

        /// <summary>
        /// Selected date range for main fiscal/grant view.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        public string SelectedDateRange
        {
            get { return _selectedDateRange; }
            set
            {
                _selectedDateRange = value;

                string[] years = _selectedDateRange.Split("-");
                _selectedStartYear = int.Parse(years[0]);
                _selectedEndYear = int.Parse(years[1]);

                SetFocusStartAndEndDates(_isFiscal);
                RefreshMainPage();
                OnPropertyChanged(nameof(SelectedDateRange));
            }
        }

        /// <summary>
        /// Selected index of date range.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        public int SelectedDateRangeIndex
        {
            get { return _selectedDateRangeIndex;}
            set
            {
                _selectedDateRangeIndex = value;
                OnPropertyChanged(nameof(SelectedDateRangeIndex));
            }
        }

        /// <summary>
        /// Selected starting date of focus view.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        public DateTime? SelectedFocusStartDate
        {
            get { return _selectedFocusStartDate; }
            set
            {
                _selectedFocusStartDate = value;
                RefreshFocusList();
                OnPropertyChanged(nameof(SelectedFocusStartDate));
            }
        }

        /// <summary>
        /// Selected ending date of focus view.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        public DateTime? SelectedFocusEndDate
        {
            get { return _selectedFocusEndDate; }
            set
            {
                _selectedFocusEndDate = value;
                RefreshFocusList();
                OnPropertyChanged(nameof(SelectedFocusEndDate));
            }
        }

        /// <summary>
        /// Current tile of focus view.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        public string CurrentTitle
        {
            get { return _currentTitle; }
            set
            {
                _currentTitle = value;
                OnPropertyChanged(nameof(CurrentTitle));
            }
        }

        /// <summary>
        /// Totals for datagrids.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        
        public string FocusTotal
        {
            get
            {
                return _focusTotal.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        public string MealTotal
        {
            get
            {
                return _mealTotal.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        public string MileageTotal
        {
            get
            {
                return _mileageTotal.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        public string BusTotal
        {
            get
            {
                return _busTotal.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        public string InKindTotal
        {
            get
            {
                return _inKindTotal.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Flag for dynamically displaying view. True if fiscal, false if grant.
        /// </summary>
        public bool IsFiscal
        {
            get { return _isFiscal; }
            set
            {
                _isFiscal = value;
                OnPropertyChanged(nameof(IsFiscal));
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inKindExpenseProvider">For database calls to get in kind expenses.</param>
        /// <param name="isFiscal">True if fiscal, false if grant.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/2/23</created>
        public FocusTableViewModel(IInKindExpenseProvider inKindExpenseProvider, IExpenseProvider expenseProvider, IDialogProvider dialogProvider, bool isFiscal)
        {
            ExportCommand = new ExportCommand(this);

            _isFiscal = isFiscal;
            OnPropertyChanged(nameof(IsFiscal));

            _inKindExpenseProvider = inKindExpenseProvider;

            _expenseProvider = expenseProvider;
            _dialogProvider = dialogProvider;

            SetInitalYearRanges();
        }

        /// <summary>
        /// Set initial year ranges for main fiscal/grant view.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        private void SetInitalYearRanges()
        {
            try
            {
                _yearRanges = new ObservableCollection<string>(_inKindExpenseProvider.GetFinancialYearRanges());
                if (errorFlag) { errorFlag = false; return; }
                OnPropertyChanged(nameof(YearRanges));

                SetSelectedDateRange();

                SetSelectedStartEndYears();

                _selectedDateRangeIndex = _yearRanges.IndexOf(_selectedDateRange);
                OnPropertyChanged(nameof(SelectedDateRangeIndex));
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowAlertDialog(_retrieveErrorMessage, "Error");
            }
        }

        /// <summary>
        /// Set the selected start and end years.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        private void SetSelectedStartEndYears()
        {
            string[] years = _selectedDateRange.Split("-");

            _selectedStartYear = int.Parse(years[0]);
            _selectedEndYear = int.Parse(years[1]);
        }

        /// <summary>
        /// Sets the selected date range. Default to most recent years if collection is empty.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/6/23</created>
        private void SetSelectedDateRange()
        {
            if (_yearRanges.FirstOrDefault() != null)
            {
                _selectedDateRange = _yearRanges.Last();
                OnPropertyChanged(nameof(SelectedDateRange));
            }
            else
            {
                _selectedDateRange = GetDefaultDateToYearRanges();
                _yearRanges.Add(_selectedDateRange);
            }
        }

        /// <summary>
        /// Gets the most recent date range.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        private string GetDefaultDateToYearRanges()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.AddYears(-1).Year + "-" + currentDate.Year;  // Set default date range
        }

        /// <summary>
        /// Retreive meal in kind data and set values.
        /// </summary>
        /// <param name="startDate">Start date filter.</param>
        /// <param name="endDate">End date filter.</param>
        /// <param name="isFiscal">True if fiscal year, false if grant.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/2/23</created>
        public void SetMealInKindSummary(DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            try
            {
                _mealTransportMileageList = new ObservableCollection<FinanceFocusGridModel>(_inKindExpenseProvider.GetMealInKindForDates(_selectedFocusStartDate, _selectedFocusEndDate, _isFiscal));
                if (errorFlag) { errorFlag = false; return; }
                OnPropertyChanged(nameof(MealTransportMileageList));

                _currentTitle = _mealInKindTitle;
                OnPropertyChanged(nameof(CurrentTitle));

                UpdateFocusPage(_mealTransportMileageList, startDate, endDate, isFiscal);
            }
            catch
            {
                _dialogProvider.ShowAlertDialog(_retrieveErrorMessage, "Error");
            }
        }

        /// <summary>
        /// Retreive meal in kind data and set values.
        /// </summary>
        /// <param name="startDate">Start date filter.</param>
        /// <param name="endDate">End date filter.</param>
        /// <param name="isFiscal">True if fiscal year, false if grant.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/2/23</created>
        public void SetMilageSummary(DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            _mealTransportMileageList = new ObservableCollection<FinanceFocusGridModel>(_inKindExpenseProvider.GetMileageForDates(_selectedFocusStartDate, _selectedFocusEndDate, _isFiscal));
            if (errorFlag) { errorFlag = false; return; }
            OnPropertyChanged(nameof(MealTransportMileageList));

            _currentTitle = _mileageTitle;
            OnPropertyChanged(nameof(CurrentTitle));

            UpdateFocusPage(_mealTransportMileageList, startDate, endDate, isFiscal);
        }

        /// <summary>
        /// Retreive meal in kind data and set values.
        /// </summary>
        /// <param name="startDate">Start date filter.</param>
        /// <param name="endDate">End date filter.</param>
        /// <param name="isFiscal">True if fiscal year, false if grant.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/2/23</created>
        public void SetBusSummary(DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            _mealTransportMileageList = new ObservableCollection<FinanceFocusGridModel>(_inKindExpenseProvider.GetBusTransportForDates(_selectedFocusStartDate, _selectedFocusEndDate, _isFiscal));
            if (errorFlag) { errorFlag = false; return; }
            OnPropertyChanged(nameof(MealTransportMileageList));

            _currentTitle = _busRideTitle;
            OnPropertyChanged(nameof(CurrentTitle));

            UpdateFocusPage(_mealTransportMileageList, startDate, endDate, isFiscal);
        }

        /// <summary>
        /// Retreive meal in kind data and set values.
        /// </summary>
        /// <param name="startDate">Start date filter.</param>
        /// <param name="endDate">End date filter.</param>
        /// <param name="isFiscal">True if fiscal year, false if grant.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/2/23</created>
        public void SetYearToDateExpenseSummary(DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            _mealTransportMileageList = new ObservableCollection<FinanceFocusGridModel>(_expenseProvider.GetExpenseForYearToDateTable(_selectedFocusStartDate, _selectedFocusEndDate));
            if (errorFlag) { errorFlag = false; return; }
            OnPropertyChanged(nameof(MealTransportMileageList));

            _currentTitle = _inKindTitle;
            OnPropertyChanged(nameof(CurrentTitle));

            UpdateFocusPage(_mealTransportMileageList, startDate, endDate, isFiscal);
        }

        /// <summary>
        /// Update items on the focus page.
        /// </summary>
        /// <param name="financeFocusGrid">The list of focus grid items.</param>
        /// <param name="startDate">The start date of filter.</param>
        /// <param name="endDate">The end date of filter.</param>
        /// <param name="isFiscal">True if fiscal, false if not.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        private void UpdateFocusPage(ObservableCollection<FinanceFocusGridModel> financeFocusGrid, DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            _selectedFocusStartDate = startDate;
            OnPropertyChanged(nameof(SelectedFocusStartDate));

            _selectedFocusEndDate = endDate;
            OnPropertyChanged(nameof(SelectedFocusEndDate));

            _isFiscal = isFiscal;
            OnPropertyChanged(nameof(IsFiscal));

            _focusTotal = financeFocusGrid.Sum(x => x.Value);
            OnPropertyChanged(nameof(FocusTotal));
        }

        /// <summary>
        /// Refreshed the data depending on flag.
        /// </summary>
        /// <param name="isFocusedView">True to refresh focused view. False to refresh main page.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        public void RefreshData(bool isFocusedView)
        {
            if (isFocusedView)
            {
                RefreshFocusList();
            }
            else
            {
                RefreshMainPage();
            }
        }

        /// <summary>
        /// Refresh the current list.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/2/23</created>
        public void RefreshFocusList()
        {
            try
            {
                switch (_currentTitle)
                {
                    case "Meal-In-Kind":
                        SetMealInKindSummary(_selectedFocusStartDate, _selectedFocusEndDate, _isFiscal);
                        break;
                    case "Bus Transportation":
                        SetBusSummary(_selectedFocusStartDate, _selectedFocusEndDate, _isFiscal);
                        break;
                    case "Volunteer Mileage":
                        SetMilageSummary(_selectedFocusStartDate, _selectedFocusEndDate, _isFiscal);
                        break;
                    case "In-Kind":
                        SetYearToDateExpenseSummary(_selectedFocusStartDate, _selectedFocusEndDate, _isFiscal);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowAlertDialog(_retrieveErrorMessage, "Error");
            }

        }

        /// <summary>
        /// Refresh the main non focused page.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        public void RefreshMainPage()
        {
            try
            {
                SetMealTransportMainPageData();

                _mealTotal = _mealTransportMainPage.Sum(x => x.dbTotalMealValue);
                OnPropertyChanged(nameof(MealTotal));

                _mileageTotal = _mealTransportMainPage.Sum(x => x.dbTotalMileageValue);
                OnPropertyChanged(nameof(MileageTotal));

                _busTotal = _mealTransportMainPage.Sum(x => x.dbTotalBusValue);
                OnPropertyChanged(nameof(BusTotal));

                SetInKindExpenseData();
                _inKindTotal = _yearToDateExpenses != null ? Math.Round(_yearToDateExpenses.Sum(x => x.Value), 2) : 0.0;
                OnPropertyChanged(nameof(InKindTotal));
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowAlertDialog(_retrieveErrorMessage, "Error");
            }     
        }

        /// <summary>
        /// Retrieves and sets the year to date expenses for a range.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        private void SetInKindExpenseData()
        {
            _yearToDateExpenses = new ObservableCollection<FinanceFocusGridModel>(_expenseProvider.GetExpenseForYearToDateTable(_selectedFocusStartDate, _selectedFocusEndDate));
            if (errorFlag) { errorFlag = false; return; }
            OnPropertyChanged(nameof(YearToDateList));
        }

        /// <summary>
        /// Retrieves and sets all meal and transport data for a given year range.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        private void SetMealTransportMainPageData()
        {
            if (_isFiscal)
            {
                _mealTransportMainPage = new ObservableCollection<MealTransportMileageModel>(_inKindExpenseProvider.GetAllInKindForFiscalYear(_selectedStartYear, _selectedEndYear));
                if (errorFlag) { errorFlag = false; return; }
            }
            else
            {
                _mealTransportMainPage = new ObservableCollection<MealTransportMileageModel>(_inKindExpenseProvider.GetAllInKindForGrantYear(_selectedStartYear, _selectedEndYear));
                if (errorFlag) { errorFlag = false; return; }
            }

            OnPropertyChanged(nameof(MealTransportMainPage));
        }

        /// <summary>
        /// Sets the focus start and end dates.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/3/23</created>
        public void SetFocusStartAndEndDates(bool isFiscal)
        {
            if (isFiscal)
            {
                _selectedFocusStartDate = new DateTime(_selectedStartYear, 10, 1);
                _selectedFocusEndDate = new DateTime(_selectedEndYear, 9, 30);
            }
            else
            {
                _selectedFocusStartDate = new DateTime(_selectedStartYear, 7, 1);
                _selectedFocusEndDate = new DateTime(_selectedEndYear, 6, 30);
            }

            OnPropertyChanged(nameof(SelectedFocusStartDate));
            OnPropertyChanged(nameof(SelectedFocusEndDate));
        }

        /// <summary>
        /// Export the currently viewed data to excel. Depends on which is being focused.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        public override void Export()
        {
            switch (_currentTitle)
            {
                case "Meal-In-Kind":
                    ExcelExporter.ExportToExcel(CreateFocusedPageReport(_mealTransportMileageList, _selectedFocusStartDate, _selectedFocusEndDate, _mealCountHeader, _mealValueHeader));
                    break;
                case "Bus Transportation":
                    ExcelExporter.ExportToExcel(CreateFocusedPageReport(_mealTransportMileageList, _selectedFocusStartDate, _selectedFocusEndDate, _busCountHeader, _busValueHeader));
                    break;
                case "Volunteer Mileage":
                    ExcelExporter.ExportToExcel(CreateFocusedPageReport(_mealTransportMileageList, _selectedFocusStartDate, _selectedFocusEndDate, _mileCountHeader, _mileageValueHeader));
                    break;
                case "In-Kind":
                    ExcelExporter.ExportToExcel(CreateFocusedPageReport(_mealTransportMileageList, _selectedFocusStartDate, _selectedFocusEndDate, "", _mealValueHeader));
                    break;
                default:
                    ExcelExporter.ExportToExcel(CreateFinanceYearMainPageReport(_mealTransportMainPage, _yearToDateExpenses, _selectedFocusStartDate, _selectedFocusEndDate));
                    return;
            }
        }

        /// <summary>
        /// Builds the report for the non focused finance year main page.
        /// </summary>
        /// <param name="mealTransportList">Meal and transport list.</param>
        /// <param name="selectedStartDate">Selected start date filter.</param>
        /// <param name="selectedEndDate">Selected end date filter.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>Excel File Model.</returns>
        private ExcelFileModel CreateFinanceYearMainPageReport(ObservableCollection<MealTransportMileageModel> mealTransportList, ObservableCollection<FinanceFocusGridModel> yearToDateExpenses, DateTime? selectedStartDate, DateTime? selectedEndDate)
        {
            string startDate = selectedStartDate != null ? selectedStartDate.Value.ToShortDateString() : "None";
            string endDate = selectedEndDate != null ? selectedEndDate.Value.ToShortDateString() : "None";

            ExcelTableModel summaryTable = CreateFinanceYearSummaryTable(startDate, endDate);
            ExcelTableModel mealTransportTable = CreateMealTransportTable(mealTransportList);
            ExcelTableModel yearToDateExpenseTable = CreateYearToDateExpenseTable(yearToDateExpenses);

            ExcelSheetModel excelSheetModel = CreateFinanceYearSheet(summaryTable, mealTransportTable, yearToDateExpenseTable);

            ExcelFileModel excelFileModel = CreateFinanceYearExcelFileModel(excelSheetModel);

            return excelFileModel;
        }

        /// <summary>
        /// Create the excel file model for finance year report.
        /// </summary>
        /// <param name="excelSheetModel">Current sheet.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>ExcelFileModel with finance year data.</returns>
        private ExcelFileModel CreateFinanceYearExcelFileModel(ExcelSheetModel excelSheetModel)
        {
            return new ExcelFileModel()
            {
                FileName = _isFiscal ? "Fiscal-" + _selectedDateRange + "-Report" : "Grant-" + _selectedDateRange + "-Report",
                Sheets = new List<ExcelSheetModel> { excelSheetModel }
            };
        }

        /// <summary>
        /// Create excel sheet with table data.
        /// </summary>
        /// <param name="summaryTable">Table with summary of data.</param>
        /// <param name="mealTransportTable">Table with meal and transport data.</param>
        /// <param name="yearToDateExpenseTable">Table with year to date expense data.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>ExcelSheetModel with table data.</returns>
        private ExcelSheetModel CreateFinanceYearSheet(ExcelTableModel summaryTable, ExcelTableModel mealTransportTable, ExcelTableModel yearToDateExpenseTable)
        {
            return new ExcelSheetModel()
            {
                Title = _isFiscal ? "Fiscal Year " + _selectedDateRange : "Grant Year " + _selectedDateRange,
                Tables = new List<ExcelTableModel> { summaryTable, mealTransportTable, yearToDateExpenseTable }
            };
        }

        /// <summary>
        /// Creates the year to date expense data.
        /// </summary>
        /// <param name="yearToDateExpenses">List of year to date expense totals.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>ExcelTableModel with year to date expense data.</returns>
        private ExcelTableModel CreateYearToDateExpenseTable(ObservableCollection<FinanceFocusGridModel> yearToDateExpenses)
        {
            return new ExcelTableModel()
            {
                Title = _inKindTitle,
                Headers = new List<string> { "Type", "Value" },
                Rows = CreateInKindRows(yearToDateExpenses)
            };
        }

        /// <summary>
        /// Creates the meal and transport table.
        /// </summary>
        /// <param name="mealTransportList">List of year and transport data sumed in quarters.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>ExcelTableModel with meal and transport data.</returns>
        private ExcelTableModel CreateMealTransportTable(ObservableCollection<MealTransportMileageModel> mealTransportList)
        {
            return new ExcelTableModel()
            {
                Title = "Meal and Transport",
                Headers = new List<string> { "Quarter", "Date Range", "Meal-In-Kind", "Volunteer Milage", "Bus Transportation" },
                Rows = CreateMealTransportRows(mealTransportList)
            };
        }

        /// <summary>
        /// Create the finance year summary table.
        /// </summary>
        /// <param name="startDate">Start date of filter.</param>
        /// <param name="endDate">End date of filter.</param>
        /// <returns>ExcelTableModel with summary data.</returns>
        private ExcelTableModel CreateFinanceYearSummaryTable(string startDate, string endDate)
        {
            return new ExcelTableModel()
            {
                Title = "Summary",
                Headers = new List<string> { "Start Date", "End Date", "Total Meal" + _mealValueHeader, "Total " + _mileageValueHeader, "Total " +   _busValueHeader, "Total In-Kind " + _mealValueHeader },
                Rows = new List<object> { new { startDate, endDate, _mealTotal, _mileageTotal, _busTotal,  _inKindTotal } },
            };
        }

        /// <summary>
        /// Create rows for the meal and tranport table.
        /// </summary>
        /// <param name="mealTransportList">Collection of transport data.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>List of objects containing the meal and transport data.</returns>
        public List<object> CreateMealTransportRows(ObservableCollection<MealTransportMileageModel> mealTransportList)
        {
            var tableData = new List<object>();

            foreach (MealTransportMileageModel mealTransport in mealTransportList)
            {
                tableData.Add(new
                {
                    mealTransport.strQuarter,
                    mealTransport.strDate,
                    mealTransport.dbTotalMealValue,
                    mealTransport.dbTotalMileageValue,
                    mealTransport.dbTotalBusValue
                });
            }

            return tableData;
        }

        /// <summary>
        /// Create rows for the in kind table.
        /// </summary>
        /// <param name="inKindList">List of in kind items. Totaled by date range rather than quarter.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>List of objects with in kind data.</returns>
        public List<object> CreateInKindRows(ObservableCollection<FinanceFocusGridModel> inKindList)
        {
            var tableData = new List<object>();

            foreach (FinanceFocusGridModel inKind in inKindList)
            {
                tableData.Add(new
                {
                    inKind.Name,
                    inKind.Value,
                });
            }

            return tableData;
        }

        /// <summary>
        /// Create the focused page file report for ExcelFileModel
        /// </summary>
        /// <param name="focusedList">List of meal, transport, or in kind items.</param>
        /// <param name="selectedStartDate">Start date filter.</param>
        /// <param name="selectedEndDate">End date filter.</param>
        /// <param name="countHeader">Count header for current focused item.</param>
        /// <param name="valueHeader">Value header for current focused item.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>Excel file model with focused report data.</returns>
        private ExcelFileModel CreateFocusedPageReport(ObservableCollection<FinanceFocusGridModel> focusedList, DateTime? selectedStartDate, DateTime? selectedEndDate, string countHeader, string valueHeader)
        {
            string startDate = selectedStartDate != null ? selectedStartDate.Value.ToShortDateString() : "None";
            string endDate = selectedEndDate != null ? selectedEndDate.Value.ToShortDateString() : "None";

            ExcelTableModel summaryTable = CreateFocusedSummaryTable(valueHeader, startDate, endDate);
            ExcelTableModel focusedTable = CreateFocusedTable(focusedList, countHeader, valueHeader);

            ExcelSheetModel excelSheetModel = CreateFocusedSheet(summaryTable, focusedTable);

            ExcelFileModel excelFileModel = CreateFocusedExcelFileModel(excelSheetModel);

            return excelFileModel;
        }

        /// <summary>
        /// Creates the focused excel file model using sheet data.
        /// </summary>
        /// <param name="excelSheetModel">Sheet with table data.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>ExcelFileModel with sheetdata.</returns>
        private ExcelFileModel CreateFocusedExcelFileModel(ExcelSheetModel excelSheetModel)
        {
            return new ExcelFileModel()
            {
                FileName = _isFiscal ? "Fiscal-" + _selectedDateRange + "-Report" : "Grant-" + _selectedDateRange + "-Report",
                Sheets = new List<ExcelSheetModel> { excelSheetModel }
            };
        }

        /// <summary>
        /// Creates the focused excel sheet model with table data.
        /// </summary>
        /// <param name="summaryTable">Table with summary data such as date range and totals.</param>
        /// <param name="focusedTable">The table with row data.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>ExcelSheetModel with table data.</returns>
        private ExcelSheetModel CreateFocusedSheet(ExcelTableModel summaryTable, ExcelTableModel focusedTable)
        {
            return new ExcelSheetModel()
            {
                Title = _isFiscal ? "Fiscal Year" : "Grant Year",
                Tables = new List<ExcelTableModel> { summaryTable, focusedTable }
            };
        }

        /// <summary>
        /// Create the focused table.
        /// </summary>
        /// <param name="focusedList">List of meal, transport, or in kind data.</param>
        /// <param name="countHeader">String count header.</param>
        /// <param name="valueHeader">String value header </param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>ExcelTable model with meal, transport, or in kind data.</returns>
        private ExcelTableModel CreateFocusedTable(ObservableCollection<FinanceFocusGridModel> focusedList, string countHeader, string valueHeader)
        {
            ExcelTableModel focusedTable;
            if (_currentTitle == _inKindTitle)
            {
                focusedTable = new ExcelTableModel()
                {
                    Title = _currentTitle,
                    Headers = new List<string> { "Type", valueHeader },
                    Rows = CreateInKindRows(focusedList)
                };

            }
            else
            {
                focusedTable = new ExcelTableModel()
                {
                    Title = _currentTitle,
                    Headers = new List<string> { "Volunteer", "Quarter", "Date", "Rate", countHeader, valueHeader },
                    Rows = CreateFocusedListRows(focusedList)
                };
            }

            return focusedTable;
        }

        /// <summary>
        /// Create the summary table for the focused table. Includes dates and totals.
        /// </summary>
        /// <param name="valueHeader">Value header for current item.</param>
        /// <param name="startDate">start date filter.</param>
        /// <param name="endDate">End date filter.</param>
        /// <returns>ExcelTableModel with focused table summary data.</returns>
        private ExcelTableModel CreateFocusedSummaryTable(string valueHeader, string startDate, string endDate)
        {
            return new ExcelTableModel()
            {
                Title = "Summary",
                Headers = new List<string> { "Start Date", "End Date", "Total " + valueHeader },
                Rows = new List<object> { new { startDate, endDate, _focusTotal } },
            };
        }

        /// <summary>
        /// Create table for focused list.
        /// </summary>
        /// <param name="focusedList">List of Meal or Transport data.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/23</created>
        /// <returns>List of objects with meal or transport data.</returns>
        private List<object> CreateFocusedListRows(ObservableCollection<FinanceFocusGridModel> focusedList)
        {
            var tableData = new List<object>();

            foreach (FinanceFocusGridModel focused in focusedList)
            {
                tableData.Add(new
                {
                    focused.Name,
                    focused.Quarter,
                    focused.Date,
                    focused.Rate,
                    focused.Count,
                    focused.Value
                });
            }

            return tableData;
        }

        /// <summary>
        /// Show alert displaying an error message.
        /// </summary>
        /// <param name="message">_retrieveErrorMessage of alert.</param>
        /// <param name="caption">Type of alert.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        public override void ActionFailed(string message, string caption)
        {
            _dialogProvider.ShowAlertDialog(message, caption);
        }
    }
}
