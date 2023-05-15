using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using HandyControl.Controls;
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

namespace C_FGMS.UI
{
    /// <summary>
    /// This file is the main starting point of the application and houses logic for showing various
    /// top level pages as well of the login page.
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    public partial class MainWindow : System.Windows.Window
    {
        // Define pages as private readonly properties
        private readonly IServiceProvider _serviceProvider;

        public UserModel? LoggedInUser { get; set; } = null;

        public MainWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();

            // We start the user out at the login screen
            HideNavMenu();
            mainFrame.Navigate(_serviceProvider.GetRequiredService<LoginPage>());
        }

        /// <summary>
        /// Method called from the login page on successful login. Method sets up usermodel and navigates to home page.
        /// </summary>
        /// <param name="user">Logged in user model</param>
        public void OnUserLoggedIn(UserModel user)
        {
            LoggedInUser = user;

            ShowNavMenu(user.IsAdmin);
            mainFrame.Navigate(_serviceProvider.GetRequiredService<HomePage>());
        }

        /// <summary>
        /// Method called from main window navigation in order to log the user out. The application shuts down on logout.
        /// </summary>
        public void OnUserLoggedOut()
        {
            LoggedInUser = null;
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Shows the left nav menu to the user
        /// </summary>
        private void ShowNavMenu(bool isAdmin)
        {
            if(isAdmin)
            {
                // Show admin navigation
                smiAdminPage.Visibility = Visibility.Visible;
            }
            else
            {
                // Hide admin navigation
                smiAdminPage.Visibility = Visibility.Collapsed;
            }

            grdNavigation.Visibility = Visibility.Visible;
            grdNavigationDivider.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Hides the left nav menu from the user
        /// </summary>
        private void HideNavMenu()
        {
            grdNavigation.Visibility = Visibility.Collapsed;
            grdNavigationDivider.Visibility = Visibility.Collapsed;
            smiAdminPage.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// The Purpose of this function is to change between pages on click of a sidemenu option.
        /// </summary>
        /// <param name="sender">Reference to the control that spawned the event</param>
        /// <param name="e">Event arguments</param>
        /// <author>Isabelle Johns</author>
        private void SideMenu_SelectionChanged(object sender, HandyControl.Data.FunctionEventArgs<object> e)
        {
            // Prevent user navigation unless the user is logged in
            if (LoggedInUser == null)
            {
                // Return the user to the login page
                HideNavMenu();
                mainFrame.Navigate(_serviceProvider.GetRequiredService<LoginPage>());
                return;
            }

            // Remove welcome message when a user navigates
            Growl.Clear();

            // I am using GetRequiredService here so that singletons are only initialized as they are needed
            foreach(SideMenuItem item in mnuSideMenu.Items)
            {
                if (item.IsSelected)
                {
                    switch (item.Header)
                    {
                        case "Home":
                            mainFrame.Navigate(_serviceProvider.GetRequiredService<HomePage>());
                            break;
                        case "Volunteers":
                            mainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteersPage>());
                            break;
                        case "Schools":
                            mainFrame.Navigate(_serviceProvider.GetRequiredService<School>());
                            break;
                        case "Finance":
                            mainFrame.Navigate(_serviceProvider.GetRequiredService<Finance>());
                            break;
                        case "Reports":
                            mainFrame.Navigate(_serviceProvider.GetRequiredService<ReportsPage>());
                            break;
                        case "Admin":
                            // Only allow admins to access the admin/users page
                            if (LoggedInUser?.IsAdmin ?? false)
                            {
                                mainFrame.Navigate(_serviceProvider.GetRequiredService<UsersMainPage>());
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handle the logout button click. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            OnUserLoggedOut();
        }
    }
}

