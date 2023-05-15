using A_FGMS.DataLayer;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
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
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;

namespace C_FGMS.UI
{
    /// <summary>
    /// Dialog used for setting/editing database settings
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    /// <created>4/1/23</created>
    public partial class DatabaseSettingsDialog : Window
    {
        private readonly DatabaseSettings DatabaseSettings;
        public DatabaseSettingsDialog(DatabaseSettings databaseSettings)
        {
            InitializeComponent();

            DatabaseSettings = databaseSettings;

            grdDatabaseSettings.DataContext = DatabaseSettings;
        }

        /// <summary>
        /// Click event for test connection button
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            // Update password if it is changed
            if (!string.IsNullOrEmpty(txtPassword.Password.Trim()))
            {
                DatabaseSettings.Password = txtPassword.Password.Trim();
            }

            if (!DatabaseSettings.HasSettings)
            {
                GrowlHelpers.Error("Please fill out the fields in order to test connection.");
                return;
            }

            if (DatabaseSettings.TestDatabaseConnection(out string error))
                GrowlHelpers.Info("Connection success!");
            else
                GrowlHelpers.Error(error);
        }

        /// <summary>
        /// Click event for close button
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Click event for save button
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Update password if it is changed
            if (!string.IsNullOrEmpty(txtPassword.Password.Trim()))
            {
                DatabaseSettings.Password = txtPassword.Password.Trim();
            }

            if (!DatabaseSettings.HasSettings)
            {
                GrowlHelpers.Error("Please fill out the fields in order to save.");
            }
            else if (!DatabaseSettings.TestDatabaseConnection(out string error))
            {
                GrowlHelpers.Error(error);
            }
            else
            {
                DatabaseSettings.SaveSettings();
                DialogResult = true;
            }
        }

        /// <summary>
        /// Click event for migrate button
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/9/23</created>
        private void btnMigrate_Click(object sender, RoutedEventArgs e)
        {
            if (!DatabaseSettings.HasSettings)
            {
                GrowlHelpers.Error("Please fill out the fields in order to save.");
            }
            else if (!DatabaseSettings.TestDatabaseConnection(out string error))
            {
                GrowlHelpers.Error(error);
            }
            else
            {
                var migrationsList = DatabaseSettings.CheckForMigrations(out string errorMessage);

                if (migrationsList == null)
                {
                    MessageBox.Show(errorMessage, "Error checking for migrations", MessageBoxButton.OK);
                    return;
                }

                if (migrationsList.Any())
                {
                    var result = MessageBox.Show(
                        "Migrations are available to apply. " +
                        "Would you like to apply these migrations? " +
                        "Doing so may cause issues for other connected clients on older versions of the software. Please ensure all users are on the latest version of this software before proceeding.",
                        "Database migrations available",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        if(!DatabaseSettings.ApplyMigrations(out string applyErrorMessage))
                        {
                            MessageBox.Show(applyErrorMessage, "Failed to apply database migrations", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show(
                                "All pending migrations have been applied successfully.",
                                "Database migrations successfully applied",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("There are no migrations available at this time.", "No migrations to apply", MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Overwrites the OnActivated method to set the growl parent to the current window
        /// </summary>
        /// <param name="e"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        protected override void OnActivated(EventArgs e)
        {
            Growl.SetGrowlParent(stkGrowl, true);   //Sets the GrowlPanel onto this page
            base.OnActivated(e);
        }

        /// <summary>
        /// Overwrites the OnDeactived method to unset the growl parent from the current window
        /// </summary>
        /// <param name="e"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        protected override void OnDeactivated(EventArgs e)
        {
            Growl.SetGrowlParent(stkGrowl, false);
            base.OnDeactivated(e);
        }

    }
}
