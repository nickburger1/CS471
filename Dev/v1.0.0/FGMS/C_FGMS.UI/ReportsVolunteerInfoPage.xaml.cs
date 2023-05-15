using A_FGMS.DataLayer.EventBroker;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using B_FGMS.BusinessLogic.ViewModels.VolunteerInfoViewModels;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


/// <summary>
/// The purpose of this file is to provide the interaction logic for the Reports - Volunteer Info page of the application. 
/// </summary>
/// <author>Jon Maddocks</author>
/// <created>2/14/2023</created>
namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for ReportsVolunteerInfoPage.xaml
    /// </summary>
    public partial class ReportsVolunteerInfoPage : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly VolunteerInfoViewModel _volunteerInfoViewModel;
        private readonly DataRefreshEventBroker _refreshEventBroker;

        /// <summary>
        /// This function will run an instance of ReportsVolunteerInfoPage.xaml.   
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>2/14/2023</created>
        public ReportsVolunteerInfoPage(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _refreshEventBroker = refreshEventBroker;
            InitializeComponent();

            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshVolunteerInfo();
            });

            _volunteerInfoViewModel = new VolunteerInfoViewModel(
                _serviceProvider.GetRequiredService<IVolunteerProvider>(),
                _serviceProvider.GetRequiredService<IDialogProvider>());

            DataContext = _volunteerInfoViewModel;

            Loaded += Page_Loaded;
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
            RefreshVolunteerInfo();
        }

        /// <summary>
        /// Refresh page.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        private void RefreshVolunteerInfo()
        {
            _volunteerInfoViewModel.RefreshVolunteers();
            _volunteerInfoViewModel.RefreshVolunteerInfo();
        }

        /// <summary>
        /// Handles OnClick event for Editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
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
            if (_volunteerInfoViewModel.SelectedVolunteer != null)
            {
                _volunteerInfoViewModel.SetSelectedVolunteerInfo();
            }

            if (_volunteerInfoViewModel.SelectedVolunteerInfo != null)
            {
                ReportsVolunteerInfoPageEdit volunteerInfoWindow = new ReportsVolunteerInfoPageEdit(_serviceProvider, _volunteerInfoViewModel);
                volunteerInfoWindow.Owner = Application.Current.MainWindow;
                volunteerInfoWindow.ShowDialog();
                DisplaySavedStatusGrowl(_volunteerInfoViewModel.saveSuccess);
                _volunteerInfoViewModel.SelectedVolunteerInfo = null;
                _volunteerInfoViewModel.saveSuccess = false;
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
                GrowlHelpers.Success("Volunteer Info Saved");
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
        /// Gets the current sort column and order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private void dtgInfo_Sorting(object sender, DataGridSortingEventArgs e)
        {
            _volunteerInfoViewModel.SortedColumnName = e.Column.SortMemberPath;
            _volunteerInfoViewModel.SortDirection = e.Column.SortDirection.ToString() ?? "Descending";
        }

        /// <summary>
        /// Double click to edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void btnEdit_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenEditWindow();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(ReportsVolunteerInfoPage));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }

        /// <summary>
        /// Show warning message if no entries are present in the data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>04/06/2023</created>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (_volunteerInfoViewModel.VolunteerInfo == null || !_volunteerInfoViewModel.VolunteerInfo.Any())
            {
                GrowlHelpers.Warning("Cannot export since there are no entries");
            }
        }
    }
}
