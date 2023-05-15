using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Principal;
using System.Windows;
using B_FGMS.BusinessLogic.Services.WindowProviders;

/// <FileName> AddActivityLogViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
/// <DateCreated> 03/05/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/05/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file bind the add activity log page to backend operations.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels
{
    public class AddActivityLogViewModel : AddUpdateActivityLogBase
    {
        private VolunteerNameIdModel _selectedVolunteer;
        IWindowProvider _window;
        public ICommand AddCommand { get; }
        private bool errorFlag;

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
            MessageBox.Show(e.ErrorMessage, "Database Error " + e.ErrorCode, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Currently selected volunteer.
        /// </summary>
        public VolunteerNameIdModel SelectedVolunteer
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
        /// Adds a new activity log.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>02/23/2023</created>
        public override void Add()
        {
            _newActivityLog.VolunteerTuid = (int)_selectedVolunteer.Tuid;
            _activityLogViewModel.saveSuccess = _activityLogProvider.AddActivityLog(_newActivityLog);
            if (errorFlag) { errorFlag = false; return; }

            _activityLogViewModel.RefreshVolunteers();
            if (errorFlag) { errorFlag = false; return; }

            _activityLogViewModel.RefreshActivityLogs();
            if (errorFlag) { errorFlag = false; return; }

            _window.CloseWindow();
        }

        /// <summary>
        /// Create new AddActivityLogViewModel.
        /// </summary>
        /// <param name="activityLogProvider">For updating database.</param>
        /// <param name="activityLogViewModel">To refresh the activity log table.</param>
        /// <param name="selectedVolunteer">For linking the activity log to the volunteer.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/23/2023</created>
        public AddActivityLogViewModel(IActivityLogProvider activityLogProvider, IDialogProvider dialogProvider, IWindowProvider window, ActivityLogViewModel activityLogViewModel, VolunteerNameIdModel selectedVolunteer)
        {
            AddCommand = new AddCommand(this);
            _window = window;
            _selectedVolunteer = selectedVolunteer;
            _activityLogProvider = activityLogProvider;
            _activityLogViewModel = activityLogViewModel;
            _dialogProvider = dialogProvider;

            activityLogProvider.DatabaseError += ErrorHandler;


            DateTime currentDate = DateTime.Now;

            _newActivityLog = new ActivityLogModel(new VolunteerModel(), currentDate, "", "");

            _dateBeforeEdit = currentDate;
            NewDate = currentDate;
            OnPropertyChanged(nameof(NewDate));

            _formChanged = false;     
        }
    }
}
