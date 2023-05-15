using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.UserProviders;
using B_FGMS.BusinessLogic.ViewModels;
using C_FGMS.UI.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;

namespace C_FGMS.UI
{
    /// <summary>
    /// Login page for the application.
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    public partial class LoginPage : Page
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IUserProvider _userProvider;
        private bool errorFlag;

        public LoginPage(IHostEnvironment hostEnvironment, IUserProvider userProvider, IHost AppHost)
        {
            InitializeComponent();

            _loginViewModel = new LoginViewModel();
            //_hostEnvironment = hostEnvironment; 
            _userProvider = userProvider;

            _userProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            _hostEnvironment = AppHost.Services.GetRequiredService<IHostEnvironment>();

            DataContext = _loginViewModel;
            _userProvider = userProvider;
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
        /// Event fired when the login button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (_hostEnvironment.IsDevelopment())
            {
                UserModel signedInUser = new UserModel() { Name = "debug", Email = "debug@svsu.edu", IsActive = true, PhoneNumber = "98919191919", Tuid = 100, IsAdmin = true };

                GrowlHelpers.Info($"Welcome {signedInUser.Name}");

                // Get the main window reference (parent) and call the OnUserLoggedIn method
                MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;

                if (mainWindow != null)
                {
                    mainWindow.OnUserLoggedIn(signedInUser);
                }
            } else {

                // Run validation on view model
                _loginViewModel.Validate();

                if (_loginViewModel.HasErrors)
                    return;

                if (string.IsNullOrEmpty(txtPassword.Password.ToString().Trim()))
                {
                    GrowlHelpers.Error("Password cannot be blank.");
                    return;
                }

                // Execute login lookup
                if (_userProvider.TryUserPasswordLogin(_loginViewModel.Email, txtPassword.Password.ToString().Trim(), out UserModel signedInUser))
                {
                    if (errorFlag) { errorFlag = false; return; }
                    GrowlHelpers.Info($"Welcome {signedInUser.Name}");

                    // Get the main window reference (parent) and call the OnUserLoggedIn method
                    MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;

                    if (mainWindow != null)
                    {
                        mainWindow.OnUserLoggedIn(signedInUser);
                    }
                }
                else
                {
                    if (errorFlag) { errorFlag = false; return; }
                    GrowlHelpers.Error("Failed to login. Invalid credentials or user is disabled.");
                }
            }
        }

        /// <summary>
        /// Handle on enter for the login form. This allows the user to submit the form by pressing the enter key when on any field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Button_Clicked(sender, e);

                e.Handled = true;
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var newDatabaseSettings = DatabaseSettings.LoadSettings();

            // Show dialog to enter database connection info
            DatabaseSettingsDialog dialog = new DatabaseSettingsDialog(newDatabaseSettings);
            if (dialog.ShowDialog() ?? false)
            {
                MessageBox.Show("Settings saved. Please restart the application for changes to take affect. The application will now close.", "Database settings saved", MessageBoxButton.OK, MessageBoxImage.Information);

                // Get the main window reference (parent) and call the OnUserLoggedIn method
                MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;

                if (mainWindow != null)
                {
                    mainWindow.OnUserLoggedOut();
                }
            }
        }
    }
}
