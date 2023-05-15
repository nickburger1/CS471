using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

/// <FileName> ActivityLogViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/21/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/22/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file bind the activity log page to backend operations.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels
{
    public class ActivityLogViewModel : ViewModelBase
    {
        private readonly IActivityLogProvider _activityLogProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IDialogProvider _dialogProvider;
        private readonly int _noVolunteerSelectedIndex = -1;
        private ObservableCollection<ActivityLogModel>? _activityLogs;
        private ObservableCollection<VolunteerNameIdModel> _volunteers;
        private ActivityLogModel? _selectedActivityLog;
        private VolunteerNameIdModel? _selectedVolunteer;
        private DateTime? _selectedStartDate;
        private DateTime? _selectedEndDate;
        private string? _sortedColumnName;
        private string? _sortDirection;
        private int? _selectedVolunteerTuid;
        public bool saveSuccess = false;
        private bool errorFlag;
        public ICommand ConfirmDeleteCommand { get; }
        public ICommand ExportCommand { get; }

        /// <summary>
        /// Collection of ActivityLogModels.
        /// </summary>
        public ObservableCollection<ActivityLogModel>? ActivityLogs
        {
            get
            {
                return _activityLogs;
            }
            set
            {
                if (ReferenceEquals(_activityLogs, value) != true)
                {
                    _activityLogs = value;
                    OnPropertyChanged(nameof(ActivityLogs));
                }
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
        /// Currently selected ActivityLog item.
        /// </summary>
        public ActivityLogModel? SelectedActivityLog
        {
            get
            {
                return _selectedActivityLog;
            }
            set
            {
                _selectedActivityLog = value;
                OnPropertyChanged(nameof(SelectedActivityLog));
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
                    _volunteerProvider.SetPersistantSelectedVolunteer(_volunteers.IndexOf(_selectedVolunteer));

                    if (errorFlag) { errorFlag = false; return; }
                    _selectedVolunteerTuid = _selectedVolunteer == null ? 0 : (int)_selectedVolunteer.Tuid;
                }
                else
                {
                    _selectedVolunteerTuid = 0;
                    _volunteerProvider.SetPersistantSelectedVolunteer(_noVolunteerSelectedIndex);

                    if (errorFlag) { errorFlag = false; return; }
                }


                OnPropertyChanged(nameof(SelectedVolunteer));
                RefreshActivityLogs();
            }
        }

        /// <summary>
        /// Selected start date of date range.
        /// </summary>
        public DateTime? SelectedStartDate
        {
            get
            {
                return _selectedStartDate;
            }
            set
            {
                _selectedStartDate = value;
                OnPropertyChanged(nameof(SelectedStartDate));
                RefreshActivityLogs();
            }
        }

        /// <summary>
        /// Selected end date of date range.
        /// </summary>
        public DateTime? SelectedEndDate
        {
            get
            {
                return _selectedEndDate;
            }
            set
            {
                _selectedEndDate = value;
                OnPropertyChanged(nameof(SelectedEndDate));
                RefreshActivityLogs();
            }
        }

        /// <summary>
        /// Display confirm delete window and delete if true.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        public override void ConfirmDelete()
        {
            if (_selectedActivityLog != null)
            {
                bool? result = _dialogProvider.ShowConfirmationDialog("Are you sure you want to delete?", "Confirmation");

                if (result == true)
                {
                    Delete();
                }
            }
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

        /// <summary>
        /// The constructor for the view model. Gets the provider, sets commands and refreshes the table.
        /// </summary>
        /// <param name="activityLogProvider"></param>
        /// <author>Tyler Moody</author>
        /// <created>02/22/2023</created>
        public ActivityLogViewModel(IActivityLogProvider activityLogProvider, IVolunteerProvider volunteerProvider, IDialogProvider dialogProvider)
        {
            ConfirmDeleteCommand = new ConfirmDeleteCommand(this);
            ExportCommand = new ExportCommand(this);

            _activityLogProvider = activityLogProvider;
            _volunteerProvider = volunteerProvider;
            _dialogProvider = dialogProvider;

            _activityLogProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            _selectedStartDate = GetBeginningOfYear();
            _selectedEndDate = GetEndOfYear();

            OnPropertyChanged(nameof(SelectedStartDate));
            OnPropertyChanged(nameof(SelectedEndDate));
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
        /// Refresh the list of volunteers.
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
        /// Get the end of the current year date.
        /// </summary>
        /// <returns>End of year DateTime.</returns>
        private static DateTime GetEndOfYear()
        {
            return new DateTime(DateTime.Now.Year, 12, 31);
        }

        /// <summary>
        /// Get the start of the current year date.
        /// </summary>
        /// <returns>Start of year DateTime.</returns>
        private static DateTime GetBeginningOfYear()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        /// <summary>
        /// Deletes the currently selected item.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        public void Delete()
        {
            if (_selectedActivityLog != null)
            {
                _activityLogProvider.DeleteActivityLog(_selectedActivityLog.Tuid);

                if (errorFlag) { errorFlag = false; return; }
                RefreshActivityLogs();
            }
        }

        /// <summary>
        /// Exports the activity logs to excel.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        public override void Export()
        {
            if (_activityLogs != null && _activityLogs.Any())
            {
                ExcelExporter.ExportToExcel(CreateActivityLogReport(_activityLogs, _selectedStartDate, _selectedEndDate, _sortedColumnName, _sortDirection));
            }
        }

        /// <summary>
        /// Creates a custom data object for report.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        /// <returns>An object containing a summary of activity logs.</returns>
        private static ExcelFileModel CreateActivityLogReport(ObservableCollection<ActivityLogModel> activityLogs, DateTime? startDateTime, DateTime? endDateTime, string? sortColumn, string? sortDirection)
        {
            ObservableCollection<ActivityLogModel> sortedActivityLogs = sortColumn != null && sortDirection != null ? SortActivityLogs(activityLogs, sortColumn, sortDirection) : activityLogs;
            
            string startDate = startDateTime != null ? startDateTime.Value.ToShortDateString() : "None";
            string endDate = endDateTime != null ? endDateTime.Value.ToShortDateString() : "None";

            var summaryTable = new ExcelTableModel()
            {
                Title = "Summary",
                Headers = new List<string> { "Start Date", "End Date" },
                Rows = new List<object> { new { startDate, endDate } },
            };

            var activityLogTable = new ExcelTableModel()
            {
                Title = "Activity Logs",
                Headers = new List<string> { "Volunteer", "Date", "Initial", "Incident" },
                Rows = CreateActivityLogRows(sortedActivityLogs)
            };

            var excelSheetModel = new ExcelSheetModel()
            {
                Title = "Activity Logs",
                Tables = new List<ExcelTableModel> { summaryTable, activityLogTable }
            };

            var excelFileModel = new ExcelFileModel()
            {
                FileName = "Activity-Log-Report",
                Sheets = new List<ExcelSheetModel> { excelSheetModel }
            };

            return excelFileModel;
        }

        /// <summary>
        /// Takes an observable collection of activity logs and sorts it by given parameters.
        /// </summary>
        /// <param name="activityLogs">Logs to be sorted.</param>
        /// <param name="sortColumn">Sort columns. Volunteer.Name, Date, Initial, Incident.</param>
        /// <param name="sortOrder">Ascending, descending or null.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/21/2023</created>
        /// <returns>Sorted observable collection of ActivityLogModels.</returns>
        private static ObservableCollection<ActivityLogModel> SortActivityLogs(ObservableCollection<ActivityLogModel> activityLogs, string sortColumn, string sortOrder)
        {
            // Initialize the sorted activity logs collection.
            ObservableCollection<ActivityLogModel> sortedActivityLogs;

            // Sort by Volunteer.Name.
            if (sortColumn == "Volunteer.Name")
            {
                if (sortOrder == "Ascending")
                {
                    sortedActivityLogs = new ObservableCollection<ActivityLogModel>(activityLogs.OrderByDescending(a => a.Volunteer.FullName));
                }
                else
                {
                    sortedActivityLogs = new ObservableCollection<ActivityLogModel>(activityLogs.OrderBy(a => a.Volunteer.FullName));
                }
            }
            // Sort by the specified column.
            else
            {
                var propertyInfo = typeof(ActivityLogModel).GetProperty(sortColumn);

                if (sortOrder == "Ascending")
                {
                    sortedActivityLogs = new ObservableCollection<ActivityLogModel>(activityLogs.OrderByDescending(a => propertyInfo?.GetValue(a, null)));
                }
                else
                {
                    sortedActivityLogs = new ObservableCollection<ActivityLogModel>(activityLogs.OrderBy(a => propertyInfo?.GetValue(a, null)));
                }
            }

            // Return the sorted activity logs collection.
            return sortedActivityLogs;
        }

        /// <summary>
        /// Extracts activity log information and adds it to a list.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        private static List<object> CreateActivityLogRows(ObservableCollection<ActivityLogModel> activityLogs)
        {
            var tableData = new List<object>();

            foreach (ActivityLogModel log in activityLogs)
            {
                tableData.Add(new
                {
                    Volunteer = log.Volunteer.FullName,
                    Date = log.Date.ToString("MM/dd/yyyy"),
                    log.Initial,
                    log.Incident,
                });
            }

            return tableData;
        }

        /// <summary>
        /// Refreshes the list of ActivityLogs.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        public void RefreshActivityLogs()
        {
            try
            {
                SetActivityLogs();

                _sortDirection = null;
                _sortedColumnName = null;

                OnPropertyChanged(nameof(ActivityLogs));
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowAlertDialog("Could not retrieve activity logs. Please contact contact support if issue persists.", "Error");
            }
        }

        /// <summary>
        /// Sets the list of activity logs depending on the filters.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>02/23/2023</created>
        private void SetActivityLogs()
        {
            _activityLogs = new ObservableCollection<ActivityLogModel>(_activityLogProvider.GetFilteredActivityLogs(_selectedVolunteerTuid, _selectedStartDate, _selectedEndDate));

            if (errorFlag) { errorFlag = false; return; }
        }
    }
}
