using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

/// <FileName> VolunteerInfoViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 3/24/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 3/24/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to manage the business logic for the volunteer info report page.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.ViewModels.VolunteerInfoViewModels
{
    public class VolunteerInfoViewModel : ViewModelBase
    {
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IDialogProvider _dialogProvider;
        private ObservableCollection<VolunteerInfoReportModel>? _volunteerInfo;
        private ObservableCollection<VolunteerNameIdModel>? _volunteers;
        private VolunteerInfoReportModel? _selectedVolunteerInfo;
        private VolunteerNameIdModel? _selectedVolunteer;
        private string[] _statuses = { "Active", "Inactive" };
        private string _selectedStatus;
        private string? _sortedColumnName;
        private string? _sortDirection;
        private int? _selectedVolunteerTuid;
        private int _totalActive = 0;
        private int _totalInactive = 0;
        public bool saveSuccess = false;
        private bool errorFlag;
        public ICommand ExportCommand { get; }


        /// <summary>
        /// List of volunteers with info.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/24/2023</created>
        public ObservableCollection<VolunteerInfoReportModel>? VolunteerInfo
        {
            get
            {
                return _volunteerInfo;
            }
            set
            {
                if (ReferenceEquals(_volunteerInfo, value) != true)
                {
                    _volunteerInfo = value;
                    OnPropertyChanged(nameof(VolunteerInfo));
                }
            }
        }

        /// <summary>
        /// Selected volunteer info in the datagrid.
        /// </summary>
        public VolunteerInfoReportModel? SelectedVolunteerInfo
        {
            get
            {
                return _selectedVolunteerInfo;
            }
            set
            {
                _selectedVolunteerInfo = value;
                OnPropertyChanged(nameof(SelectedVolunteerInfo));
            }
        }

        /// <summary>
        /// Collection of Volunteers.
        /// </summary>
        public ObservableCollection<VolunteerNameIdModel> Volunteers
        {
            get
            {
                return _volunteers;
            }
            set
            {
                if (ReferenceEquals(_volunteers, value) != true)
                {
                    _volunteers = value;
                    OnPropertyChanged(nameof(Volunteers));
                }
            }
        }


        /// <summary>
        /// Currently selected volunteer.
        /// </summary>
        public VolunteerNameIdModel? SelectedVolunteer
        {
            get
            {
                return _selectedVolunteer;
            }
            set
            {
                _selectedVolunteer = value;

                if (_selectedVolunteer != null && _selectedVolunteer.Tuid != null)
                {
                    _selectedVolunteerTuid = _selectedVolunteer == null ? 0 : (int)_selectedVolunteer.Tuid;
                }
                else
                {
                    _selectedVolunteerTuid = 0;
                }    

                OnPropertyChanged(nameof(SelectedVolunteer));
                RefreshVolunteerInfo();

                SetSelectedVolunteerInfo();
            }
        }

        /// <summary>
        /// List of status.
        /// </summary>
        public string[] Statuses
        {
            get { return _statuses; }
        }

        /// <summary>
        /// The currently selected status.
        /// </summary>
        public string SelectedStatus
        {
            get
            {
                return _selectedStatus;
            }
            set
            {
                _selectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
                RefreshVolunteerInfo();
            }
        }

        /// <summary>
        /// Name of the currently sorted column.
        /// </summary>
        public string? SortedColumnName
        {
            get
            {
                return _sortedColumnName;
            }
            set
            {
                _sortedColumnName = value;
            }
        }

        /// <summary>
        /// The direction of the column being sorted.
        /// </summary>
        public string? SortDirection
        {
            get
            {
                return _sortDirection;
            }
            set
            {
                _sortDirection = value;
            }
        }

        /// <summary>
        /// FocusTotal number of active volunteers.
        /// </summary>
        public int TotalActive
        {
            get
            {
                return _totalActive;
            }
            set
            {
                _totalActive = value;
                OnPropertyChanged(nameof(TotalActive));
            }
        }

        /// <summary>
        /// FocusTotal number of inactive volunteers.
        /// </summary>
        public int TotalInactive
        {
            get
            {
                return _totalInactive;
            }
            set
            {
                _totalInactive = value;
                OnPropertyChanged(nameof(TotalInactive));
            }
        }


        /// <summary>
        /// Constructor for the view model.
        /// </summary>
        /// <param name="volunteerProvider">Provides methods to get volunteer info.</param>
        /// <param name="dialogProvider">Provides methods for displaying dialogs.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/26/2023</created>
        public VolunteerInfoViewModel(IVolunteerProvider volunteerProvider, IDialogProvider dialogProvider)
        {
            ExportCommand = new ExportCommand(this);

            _volunteerProvider = volunteerProvider;

            _volunteerProvider.DatabaseError += ErrorHandler;
            errorFlag = false;
            _dialogProvider = dialogProvider;
        }

        /// <summary>
        /// Error provider for the UserServiceProvider. All functionality to handle
        /// business logic errors for the Users.xaml page are called in this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void ErrorHandler(object sender, Events.ErrorEventArgs e)
        {
            errorFlag = true;
            System.Windows.MessageBox.Show(e.ErrorMessage, "Database Error " + e.ErrorCode, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Refresh the volunteer dropdown. Keep the previous selected volunteer.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public void RefreshVolunteers()
        {
            int? selectedVolunteerTuid = _selectedVolunteer != null ? _selectedVolunteer.Tuid : 0;

            _volunteers = new ObservableCollection<VolunteerNameIdModel>(_volunteerProvider.GetVolunteerNameAndId());
            if (errorFlag) { errorFlag = false; return; }
            OnPropertyChanged(nameof(Volunteers));

            _selectedVolunteer = _volunteers.FirstOrDefault(volunteer => volunteer.Tuid == selectedVolunteerTuid);
            _selectedVolunteerTuid = selectedVolunteerTuid;
            OnPropertyChanged(nameof(SelectedVolunteer));
        }

        /// <summary>
        /// Exports the volunteer info to excel.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public override void Export()
        {
            if (_volunteerInfo != null && _volunteerInfo.Any())
            {
                ExcelExporter.ExportToExcel(CreateVolunteerInfoReport(_volunteerInfo, _totalActive, _totalInactive, _selectedStatus, _sortedColumnName, _sortDirection));
            }
        }

        /// <summary>
        /// Creates a custom data object for report.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        /// <returns>An object containing a summary of volunteer info.</returns>
        public ExcelFileModel CreateVolunteerInfoReport(ObservableCollection<VolunteerInfoReportModel> volunteerInfo, int totalActive, int totalInactive, string selectedStatus, string sortColumn, string sortDirection)
        {
            ObservableCollection<VolunteerInfoReportModel> sortedVolunteerInfo = sortColumn != null && sortDirection != null ? SortVolunteerInfo(volunteerInfo, sortColumn, sortDirection) : volunteerInfo;

            string activeStatus = selectedStatus != null ? selectedStatus : "All";

            var summaryTable = new ExcelTableModel()
            {
                Title = "Summary",
                Headers = new List<string> { "Status filter", "FocusTotal Active", "FocusTotal Inactive"},
                Rows = new List<object> { new { activeStatus, totalActive, totalInactive } },
            };

            var activityLogTable = new ExcelTableModel()
            {
                Title = "Volunteer Info",
                Headers = new List<string> { "Volunteer", "Status", "Start Date", "End Date", "Reason Separated", "DOB", "Gender", "Pronoun(s)", "Ethnicity", "Race", "Veteran", "Family of Military" },
                Rows = CreateVolunteerInfoRows(sortedVolunteerInfo)
            };

            var excelSheetModel = new ExcelSheetModel()
            {
                Title = "Volunteer Info",
                Tables = new List<ExcelTableModel> { summaryTable, activityLogTable }
            };

            var excelFileModel = new ExcelFileModel()
            {
                FileName = "Volunteer-Info-Report",
                Sheets = new List<ExcelSheetModel> { excelSheetModel }
            };

            return excelFileModel;
        }

        /// <summary>
        /// Takes an observable collection of volunteer info and sorts it by given parameters.
        /// </summary>
        /// <param name="volunteerInfo">Info to be sorted.</param>
        /// <param name="sortColumn">Sort columns.</param>
        /// <param name="sortOrder">Ascending, descending or null.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        /// <returns>Sorted observable collection of VolunteerInfoReportModels.</returns>
        public static ObservableCollection<VolunteerInfoReportModel> SortVolunteerInfo(ObservableCollection<VolunteerInfoReportModel> volunteerInfo, string sortColumn, string sortOrder)
        {
            var sortedVolunteerInfo = new ObservableCollection<VolunteerInfoReportModel>();

            var sortingDictionary = CreateSortingDictionary(sortOrder);

            sortedVolunteerInfo = new ObservableCollection<VolunteerInfoReportModel>(sortingDictionary[sortColumn](volunteerInfo));  

            return sortedVolunteerInfo;
        }

        /// <summary>
        /// Creates a sorting map using the columns in the datagrid. The sort order is backwards due to how the datagrid operates.
        /// </summary>
        /// <param name="sortOrder">Direction of sort.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        /// <returns>A dictionary of sorting methods.</returns>
        private static Dictionary<string, Func<IEnumerable<VolunteerInfoReportModel>, IOrderedEnumerable<VolunteerInfoReportModel>>> CreateSortingDictionary(string sortOrder)
        {
            return new Dictionary<string, Func<IEnumerable<VolunteerInfoReportModel>, IOrderedEnumerable<VolunteerInfoReportModel>>>
            {
                { "Volunteer.Name", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Volunteer.FullName) : info.OrderBy(a => a.Volunteer.FullName) },
                { "Demographics.Status", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.Status) : info.OrderBy(a => a.Demographics.Status) },
                { "EndDate", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.EndDate) : info.OrderBy(a => a.EndDate) },
                { "Demographics.DateOfBirth", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.DateOfBirth) : info.OrderBy(a => a.Demographics.DateOfBirth) },
                { "Demographics.Gender", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.Gender) : info.OrderBy(a => a.Demographics.Gender) },
                { "Demographics.IdentifiesAs", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.IdentifiesAs) : info.OrderBy(a => a.Demographics.IdentifiesAs) },
                { "Demographics.Ethnicity", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.Ethnicity) : info.OrderBy(a => a.Demographics.Ethnicity) },
                { "Demographics.RacialGroup", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.RacialGroup) : info.OrderBy(a => a.Demographics.RacialGroup) },
                { "Demographics.Veteran", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.Veteran) : info.OrderBy(a => a.Demographics.Veteran) },
                { "Demographics.FamilyOfMilitary", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.Demographics.FamilyOfMilitary) : info.OrderBy(a => a.Demographics.FamilyOfMilitary) },
                { "StartDate", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.StartDate) : info.OrderBy(a => a.StartDate) },
                { "ReasonSeparated", info => sortOrder == "Ascending" ? info.OrderByDescending(a => a.InactiveStatusNameAndId.Name) : info.OrderBy(a => a.InactiveStatusNameAndId.Name) }
            };
        }

        /// <summary>
        /// Create the rows for the volunteer info table.
        /// </summary>
        /// <param name="volunteerInfo">List of volunteer info.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        /// <returns>List of objects containing volunteer info row data.</returns>
        public static List<object> CreateVolunteerInfoRows(ObservableCollection<VolunteerInfoReportModel> volunteerInfo)
        {
            var tableData = new List<object>();

            foreach (VolunteerInfoReportModel info in volunteerInfo)
            {
                tableData.Add(new
                {
                    info.Volunteer.FullName,
                    info.Demographics.Status,
                    StartDate = info.StartDate != null ? info.StartDate.Value.ToShortDateString() : "",
                    EndDate = info.EndDate != null ? info.EndDate.Value.ToShortDateString() : "",
                    info.InactiveStatusNameAndId.Name,
                    DOB = info.Demographics.DateOfBirth,
                    info.Demographics.Gender,
                    info.Demographics.IdentifiesAs,
                    info.Demographics.Ethnicity,
                    info.Demographics.RacialGroup,
                    info.Demographics.Veteran,
                    info.Demographics.FamilyOfMilitary
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
        /// <created>03/24/2023</created>
        public override void ActionFailed(string message, string caption)
        {
            _dialogProvider.ShowAlertDialog(message, caption);
        }

        /// <summary>
        /// Refresh the volunteer info list.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void RefreshVolunteerInfo()
        {
            try
            {
                SetVolunteerInfo();
                _totalActive = _volunteerInfo.Where(v => v.Demographics.Status == "Active").Count();
                _totalInactive = _volunteerInfo.Where(v => v.Demographics.Status == "Inactive").Count();
                OnPropertyChanged(nameof(TotalActive));
                OnPropertyChanged(nameof(TotalInactive));
                OnPropertyChanged(nameof(VolunteerInfo));
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowAlertDialog("Could not retrieve volunteer information. Please contact support if issue persists.", "Error");
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Set list of volunteer info depending on filter.
        /// </summary>
        public void SetVolunteerInfo()
        {
            bool activeStatus = _selectedStatus == "Active" ? true : false;

            if (_selectedStatus == null)
            {
                _volunteerInfo = new ObservableCollection<VolunteerInfoReportModel>(_volunteerProvider.GetAllVolunteerInfoReport(_selectedVolunteerTuid));
                if (errorFlag) { errorFlag = false; return; }
            }
            else
            {
                _volunteerInfo = new ObservableCollection<VolunteerInfoReportModel>(_volunteerProvider.GetAllVolunteerInfoReportByActiveStatus(_selectedVolunteerTuid, activeStatus));
                if (errorFlag) { errorFlag = false; return; }
            }

        }

        /// <summary>
        /// Set the selected volunteer info depending on filter.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/06/2023</created>
        public void SetSelectedVolunteerInfo()
        {
            if (_selectedVolunteer != null && _volunteerInfo != null)
            {
                _selectedVolunteerInfo = _volunteerInfo.FirstOrDefault(x => x.Volunteer.Tuid == _selectedVolunteer.Tuid);
                OnPropertyChanged(nameof(SelectedVolunteerInfo));
            }
        }
    }
}
