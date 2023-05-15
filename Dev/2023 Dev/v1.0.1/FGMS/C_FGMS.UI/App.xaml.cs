using A_FGMS.DataLayer;
using B_FGMS.BusinessLogic.Services.AddressProviders;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.AssignmentProviders;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.UserProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;
using B_FGMS.BusinessLogic.Services.ReportProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using A_FGMS.DataLayer.EventBroker;
using System.Collections.Generic;
using System.Linq;
using B_FGMS.BusinessLogic.Models;
using HandyControl.Controls;
using C_FGMS.UI.Helpers;
using MessageBox = System.Windows.MessageBox;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using System.Diagnostics;

namespace C_FGMS.UI
{
    /// <summary>
    /// Main entry point into the application. All startup routines are executed here.
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>1/16/23</created>
    public partial class App : Application
    {
        private IHost AppHost { get; set; }
        private DatabaseSettings DatabaseSettings { get; set; }
        public int? VolunteerTuid { get; set; }

        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Override function OnStartup. This function is called when the application starts. Database setup and Host are setup here.
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Disable close on last window closed. This is done due to issues when dialogs are opened and closed during OnStartup
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            DatabaseSettings = DatabaseSettings.LoadSettings();

            // If the user does not have a database set, prompt them
            if (!DatabaseSettings.HasSettings)
            {
                MessageBox.Show("Database connection has not been setup. Please configure your database settings on the next screen.", "Database connection not setup", MessageBoxButton.OK, MessageBoxImage.Information);

                if (!PromptDatabaseSettingsOnStartup())
                {
                    Shutdown();
                    return;
                }
            }

            if (!DatabaseSettings.TestDatabaseConnection(out string error))
            {
                MessageBox.Show("Failed to connect to last saved database connection. Please check your database settings on the next screen.", "Failed to connect to database", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                if (!PromptDatabaseSettingsOnStartup())
                {
                    Shutdown();
                    return;
                }
            }

            // Handle database seeding
            try
            {
                // Check if the database has been initialized
                while (!DatabaseSettings.IsDatabaseInitialized())
                {
                    // If there are not migrations applied, execute migrations for first time setup
                    MessageBox.Show("Database has not been setup yet. Please use the Migrate button on the next screen to setup the database.", "Database not setup yet", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    if (!PromptDatabaseSettingsOnStartup())
                    {
                        Shutdown();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Error occurred when seeding the database.", ex);
                MessageBox.Show("Error occurred when seeding the database. \n" + ex.Message, "Failed to seed database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Shutdown();
                return;
            }

            // Setup host provider (for loading configuration files and managing services)
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            AppHost.Start();

            var logger = AppHost.Services.GetRequiredService<ILogger<App>>();


            // Re-enable close on last window closed
            ShutdownMode = ShutdownMode.OnLastWindowClose;


            // Get the main window singleton as a service and show it
            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        /// <summary>
        /// Method configures services that are made available to other classes at runtime.
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <param name="context"></param>
        /// <param name="services">Collection of services provided through dependency injection at runtime</param>
        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Event handler for notifying pages about data being refreshed
            services.AddSingleton<EntityChangeEventBroker>();
            services.AddSingleton<DataRefreshEventBroker>();

            // Add database context as a service
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(DatabaseSettings.GetDatabaseConnectionString());
            }, ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            // Add the main window as a singleton dependency (only one is ever created)
            services.AddSingleton<MainWindow>();

            #region Top level navigation
            services.AddSingleton<LoginPage>();
            services.AddSingleton<HomePage>();
            services.AddSingleton<VolunteersPage>();
            services.AddSingleton<School>();
            services.AddSingleton<Finance>();
            services.AddSingleton<UsersMainPage>();
            services.AddSingleton<ReportsPage>();
            #endregion

            #region Page Sub Navigation
            // Volunteer sub-pages
            services.AddSingleton<VolunteerGeneral>();
            services.AddSingleton<VolunteerDemographics>();
            services.AddSingleton<VolunteerFinancials>();
            services.AddSingleton<VolunteerClassrooms>();
            services.AddSingleton<VolunteerChildAssignments>();
            services.AddSingleton<VolunteerActivityLog>();

            // School sub-pages
            services.AddSingleton<SchoolsAllSchools>();
            services.AddSingleton<SchoolPerSchoolPage>();
            services.AddSingleton<AddNewSchool>();

            // Finance sub-pages
            services.AddSingleton<FinanceGeneralPage>();
            services.AddSingleton<FinancePTOPage>();
            services.AddSingleton<FinanceMealAndTransportPage>();
            services.AddSingleton<FinanceYearPageBase>();

            //Users sub-pages
            services.AddSingleton<Users>();
            services.AddSingleton<UsersAdminTasksPage>();

            // Reports sub-pages
            services.AddSingleton<ReportsReportBuilderPage>();
            services.AddSingleton<ReportsAnnualCheckPage>();
            services.AddSingleton<ReportsVolunteerInfoPage>();
            #endregion

            // Services (a.k.a. "Helpers") are transient and created as they are needed
            // See https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#transient for details
            services.AddTransient<IVolunteerProvider, DatabaseVolunteerProvider>();
            services.AddTransient<IInKindExpenseProvider, DatabaseInKindExpenseProvider>();
            services.AddTransient<IPTOProvider, DatabasePTOProvider>();
            services.AddTransient<IMealAndTransportProvider, DatabaseMealAndTransportProvider>();
            services.AddTransient<IMealAndTransportRatesProvider, DatabaseMealAndTransportRates>();
            services.AddTransient<ISchoolProvider, DatabaseSchoolProvider>();
            services.AddTransient<IAssignmentProvider, DatabaseAssignmentProvider>();
            services.AddTransient<IAddressProvider, DatabaseAddressProvider>();
            services.AddTransient<IActivityLogProvider, DatabaseActivityLogProvider>();
            services.AddTransient<IPTOStipendRates, DatabasePTOStipendRate>();
            services.AddTransient<IReportPresetProvider, DatabaseReportPresetProvider>();
            services.AddTransient<IReportProvider, DatabaseReportProvider>();
            services.AddTransient<IDialogProvider, DialogProvider>();
            services.AddTransient<IExpenseProvider, DatabaseExpenseProvider>();
            services.AddTransient<IUserProvider, DatabaseUserProvider>();
            services.AddTransient<IStudentProvider, DatabaseStudentProvider>();
        }

        /// <summary>
        /// Method used to prompt the user with the database settings dialog in case of startup database failure
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        private bool PromptDatabaseSettingsOnStartup()
        {
            // Show dialog to enter database connection info
            var dialog = new DatabaseSettingsDialog(DatabaseSettings);

            var databaseSettingsResult = dialog.ShowDialog();

            Growl.GrowlPanel = null;    // Reset the Growl Panel to Null. This Locks it onto the current Window
            if (!(databaseSettingsResult ?? false))
            {
                // User failed to enter settings. Stop application
                return false;
            }

            return true;
        }

        /// <summary>
        /// Override function OnExit. This function is called when the application attempts to close.
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <param name="e">Event arguments for the Exit event</param>
        protected override async void OnExit(ExitEventArgs e)
        {
            if (AppHost != null)
            {
                using (AppHost)
                {
                    await AppHost.StopAsync();
                }
            }

            base.OnExit(e);
        }

    }
}
