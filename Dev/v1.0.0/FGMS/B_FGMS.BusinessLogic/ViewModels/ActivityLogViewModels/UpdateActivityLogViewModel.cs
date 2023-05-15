using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using System.Windows;

/// <FileName> UpdateActivityLogViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
/// <DateCreated> 03/05/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/05/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file bind the update activity log page to backend operations.
/// </summary>
/// <author> Tyler Moody </author>
/// 
namespace B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels
{
    public class UpdateActivityLogViewModel : AddUpdateActivityLogBase
    {
        private new IActivityLogProvider _activityLogProvider;
        private new ActivityLogViewModel _activityLogViewModel;
        private string _selectedVolunteer;
        public ICommand UpdateCommand { get; }
        private bool errorFlag;


        /// <summary>
        /// Volunteer that selected ActivityLog belongs to.
        /// </summary>
        public string SelectedVolunteer
        {
            get
            {
                return _selectedVolunteer;
            }
            set
            {
                _selectedVolunteer = value;
            }
        }

        /// <summary>
        /// Updates an already existing activity log.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>02/23/2023</created>
        public override void Update()
        {
            if (!_formChanged)
            {
                _activityLogViewModel.saveSuccess = true;
            }
            else if (_newActivityLog != null)
            {
                _activityLogViewModel.saveSuccess = _activityLogProvider.UpdateActivityLog(_newActivityLog);
                if (errorFlag) { errorFlag = false; return; }
            }

            _activityLogViewModel.RefreshVolunteers();
            _activityLogViewModel.RefreshActivityLogs();
        }

        /// <summary>
        /// Create new UpdateActivityLogViewModel
        /// </summary>
        /// <param name="activityLogProvider">For updating database.</param>
        /// <param name="activityLogViewModel">For refreshing activity log list.</param>
        /// <param name="activityLog">The ActivityLog being edited.</param>
        public UpdateActivityLogViewModel(IActivityLogProvider activityLogProvider, IDialogProvider dialogProvider, ActivityLogViewModel activityLogViewModel, ActivityLogModel activityLog)
        {
            UpdateCommand = new UpdateCommand(this);

            _activityLogProvider = activityLogProvider;
            _activityLogViewModel = activityLogViewModel;
            _dialogProvider = dialogProvider;

            _activityLogProvider.DatabaseError += ErrorHandler;
            errorFlag = false;


            _newActivityLog = new ActivityLogModel(activityLog.Tuid, activityLog.Volunteer, activityLog.Date, activityLog.Initial, activityLog.Incident);

            _newDate = activityLog.Date;
            OnPropertyChanged(nameof(NewDate));

            _dateBeforeEdit = activityLog.Date;

            _newInitial = activityLog.Initial;
            OnPropertyChanged(nameof(NewInitial));

            _newIncident = activityLog.Incident;
            OnPropertyChanged(nameof(NewIncident));

            _selectedVolunteer = activityLog.Volunteer.FullName;

            _formChanged = false;
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
    }
}
