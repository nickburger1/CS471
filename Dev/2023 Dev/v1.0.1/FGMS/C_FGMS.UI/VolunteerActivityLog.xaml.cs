using A_FGMS.DataLayer.EventBroker;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels;
using B_FGMS.BusinessLogic.ViewModels.VolunteerInfoViewModels;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
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

/**
 ************************************************************************************************************************
 *                                      File Name : VolunteerActivityLog.xaml.cs                                        *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Isabelle Johns                                                     *
 *                                      Date Created : 1/22/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 1/29/23                                                         *
 *                                      Last Modified By : Richard Nader, Jr.                                           *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the interaction logic for our VolunteerActivityLog.xaml file.  *
 ************************************************************************************************************************
 **/

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for VolunteerActivityLog.xaml
    /// </summary>
    public partial class VolunteerActivityLog : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly ActivityLogViewModel _activityLogViewModel;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private readonly int _noVolunteerSelectedIndex = -1;
        private bool errorFlag;

        public VolunteerActivityLog(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _refreshEventBroker = refreshEventBroker;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            InitializeComponent();

            _volunteerProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            

            _activityLogViewModel = new ActivityLogViewModel(
                _serviceProvider.GetRequiredService<IActivityLogProvider>(),
                _volunteerProvider,
                _serviceProvider.GetRequiredService<IDialogProvider>());

        _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshActivityLogPage();
            });    DataContext = _activityLogViewModel;
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
        /// <created>03/27/2023</created>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshActivityLogPage();
        }

        /// <summary>
        /// Refresh page.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        private void RefreshActivityLogPage()
        {
            int? selectedVolunteer = _volunteerProvider.GetPersistantSelectedVolunteer();
            if (errorFlag) { errorFlag = false; return; }

            _activityLogViewModel.RefreshVolunteers();

            SetPersistantSelectedVolunteer(selectedVolunteer);

            _activityLogViewModel.RefreshActivityLogs();
        }

        /// <summary>
        /// Set persistant selected volunteer if one is selected.
        /// </summary>
        /// <param name="selectedVolunteerIndex">Index of selected volunteer.</param
        /// <author>Tyler Moody</author>
        /// <created>4/7/2023</created>
        private void SetPersistantSelectedVolunteer(int? selectedVolunteerIndex)
        {
            if (selectedVolunteerIndex.HasValue && selectedVolunteerIndex != _noVolunteerSelectedIndex)
            {
                _activityLogViewModel.SelectedVolunteer = _activityLogViewModel.Volunteers[selectedVolunteerIndex.Value];
            }
            else
            {
                cmbSelectVolunteer.SelectedIndex = _noVolunteerSelectedIndex;
            }
        }

        /// <summary>
        /// Open new window to add an activity log to selected volunteer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_activityLogViewModel.SelectedVolunteer != null)
            {
                AddActivityLog activityLogWindow = new AddActivityLog(_serviceProvider, _activityLogViewModel);
                activityLogWindow.Owner = Application.Current.MainWindow;
                activityLogWindow.ShowDialog();
                DisplaySavedStatusGrowl(_activityLogViewModel.saveSuccess);
                _activityLogViewModel.saveSuccess = false;

            }
            else
            {
                ShowSelectVolunteerGrowl();
            }
        }

        /// <summary>
        /// Displays success if record was saved and error if it was not.
        /// </summary>
        /// <param name="success">Boolean flag representing success or failure.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void DisplaySavedStatusGrowl(bool success)
        {
            if (success)
            {
                GrowlHelpers.Success("Activity Log Saved");
            }
        }

        /// <summary>
        /// Warn user to select a volunteer.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        private static void ShowSelectVolunteerGrowl()
        {
            GrowlHelpers.Warning("Please Select a Volunteer");
        }

        /// <summary>
        /// Display message if no activity log is selected when delete button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        public void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_activityLogViewModel.SelectedActivityLog == null)
            {
                ShowSelectActivityLogGrowl();
            }
        }

        /// <summary>
        /// Handles OnClick event for Editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenEditWindow();
        }

        /// <summary>
        /// Open edit window and perform edit window logic.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void OpenEditWindow()
        {
            if (_activityLogViewModel.SelectedActivityLog != null)
            {
                UpdateActivityLog activityLogWindow = new UpdateActivityLog(_serviceProvider, _activityLogViewModel);
                activityLogWindow.Owner = Application.Current.MainWindow;
                activityLogWindow.ShowDialog();
                DisplaySavedStatusGrowl(_activityLogViewModel.saveSuccess);
                _activityLogViewModel.saveSuccess = false;
            }
            else
            {
                ShowSelectActivityLogGrowl();
            }
        }

        /// <summary>
        /// Growl warning user to select activity log.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        private static void ShowSelectActivityLogGrowl()
        {
            GrowlHelpers.Warning("Please Select an Activity Log Entry");
        }

        /// <summary>
        /// Gets the current sort column and order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/21/2023</created>
        private void dtgLog_Sorting(object sender, DataGridSortingEventArgs e)
        {
            _activityLogViewModel.SortedColumnName = e.Column.SortMemberPath;
            _activityLogViewModel.SortDirection = e.Column.SortDirection.ToString() ?? "Descending";
        }

        /// <summary>
        /// Double click to edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void dtgEdit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenEditWindow();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(VolunteerActivityLog));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }

        /// <summary>
        /// Displays warning if user tries to export an empty grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tyler Moody </author>
        /// <created> 4/7/23 </created>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (_activityLogViewModel.ActivityLogs == null || !_activityLogViewModel.ActivityLogs.Any())
            {
                GrowlHelpers.Warning("Cannot export since there are no entries");
            }
        }
    }
}
