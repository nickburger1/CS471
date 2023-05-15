using System.Security;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System;
using HandyControl.Tools.Extension;
using A_FGMS.DataLayer;
using A_FGMS.DataLayer.EventBroker;
using Microsoft.EntityFrameworkCore;
using B_FGMS.BusinessLogic.ViewModels;
using C_FGMS.UI.Helpers;
using C_FGMS.UI.Properties;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace C_FGMS.UI
{
    /// <summary>
    /// Class used to represent the saved database settings.
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    /// <created>4/1/23</created>
    public class DatabaseSettings
    {
        public string ServerName { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool UseWindowsAuthentication { get; set; } = false;

        /// <summary>
        /// Method loads settings from disk (located in user's AppData\Local folder)
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        public static DatabaseSettings LoadSettings()
        {
            var settings = new DatabaseSettings
            {
                ServerName = Database.Default.ServerName,
                DatabaseName = Database.Default.DatabaseName,
                Username = !string.IsNullOrEmpty(Database.Default.Username) ? DecryptString(Database.Default.Username) : string.Empty,
                Password = !string.IsNullOrEmpty(Database.Default.Password) ? DecryptString(Database.Default.Password) : string.Empty,
                UseWindowsAuthentication = Database.Default.UseWindowsAuthentication
            };

            return settings;
        }

        /// <summary>
        /// Method saves settings to disk (located in user's AppData\Local folder)
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        public void SaveSettings()
        {
            Database.Default.ServerName = ServerName.Trim();
            Database.Default.DatabaseName = DatabaseName.Trim();
            Database.Default.Username = EncryptString(Username.Trim());
            Database.Default.Password = EncryptString(Password.Trim());
            Database.Default.UseWindowsAuthentication = UseWindowsAuthentication;
            Database.Default.Save();
        }

        // Checks if form data is filled in or if the settings file has content when loaded
        public bool HasSettings
        {
            get
            {
                return !string.IsNullOrEmpty(ServerName) && !string.IsNullOrEmpty(DatabaseName) && (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) || UseWindowsAuthentication);
            }
        }

        /// <summary>
        /// Method builds SQL Server connection string
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        /// <returns>Database connection string</returns>
        public string GetDatabaseConnectionString()
        {
            if (UseWindowsAuthentication)
            {
                return $"Server={ServerName};Database={DatabaseName};Integrated Security=true;";
            }

            return $"Server={ServerName};Database={DatabaseName};user id={Username};password={Password};Connection Timeout=5;";
        }

        /// <summary>
        /// Method creates a new dbcontext instance and checks if a connection can be opened using the database credentials
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        /// <returns>True if the connection was successful, False otherwise</returns>
        public bool TestDatabaseConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!HasSettings)
            {
                errorMessage = "Invalid settings";
                return false;
            }

            try
            {
                using (var sqlConnection = new SqlConnection(GetDatabaseConnectionString()))
                {
                    sqlConnection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error connecting to the database. " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Checks if the database has been initialized yet
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/9/23</created>
        public bool IsDatabaseInitialized()
        {
            if (!TestDatabaseConnection(out string errorMessage))
            {
                return false;
            }

            try
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseSqlServer(GetDatabaseConnectionString());

                using (var dbContext = new ApplicationDbContext(builder.Options))
                {
                    return dbContext.Database.GetAppliedMigrations().Any();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if there are any pending migrations to be applied to the database
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/9/23</created>
        public List<string>? CheckForMigrations(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!TestDatabaseConnection(out errorMessage))
            {
                return null;
            }

            try
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseSqlServer(GetDatabaseConnectionString());

                using (var dbContext = new ApplicationDbContext(builder.Options))
                {
                    return dbContext.Database.GetPendingMigrations().ToList();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error checking for migrations. " + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Applies any pending migrations to the database
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/9/23</created>
        public bool ApplyMigrations(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!TestDatabaseConnection(out errorMessage))
            {
                return false;
            }

            try
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseSqlServer(GetDatabaseConnectionString());

                using (var dbContext = new ApplicationDbContext(builder.Options))
                {
                    dbContext.Database.Migrate();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error applying migrations. " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Method encrypts a given string and returns the base64 version of the encrypted byte array
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        /// <returns>Encrypted version of the passed in string</returns>
        private static string EncryptString(string str)
        {
            byte[] encryptedStr = ProtectedData.Protect(Encoding.Unicode.GetBytes(str), null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedStr);
        }

        /// <summary>
        /// Method decrypts a given string and returns the original string content of the encrypted base64 string
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <created>4/1/23</created>
        /// <returns>Decrypted version of the passed in base64 string</returns>
        private static string DecryptString(string str)
        {
            byte[] decryptedStr = ProtectedData.Unprotect(Convert.FromBase64String(str), null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decryptedStr);
        }

    }
}
