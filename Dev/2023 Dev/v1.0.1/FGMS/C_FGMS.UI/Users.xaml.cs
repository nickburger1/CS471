using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.UserProviders;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

/// <summary>
/// Provide visual tracking of the users in the system
/// </summary>
/// <author>Kiefer Thorson</author>
/// <created>1/25/23</created>
namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : AuthenticatedPageBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserProvider _userProvider;
        private IEnumerable<UserModel> users;
        private bool errorFlag;

        /// </summary>
        /// Initialize the users page and its components
        /// </summary>
        /// <author>Nathan VanSnepson & Kiefer Thorson</author>
        /// <created>1/25/23</created>
        public Users(IServiceProvider serviceProvider, IUserProvider userProvider)
        {
            _serviceProvider = serviceProvider;
            _userProvider = userProvider;

			InitializeComponent();

            _userProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            populateDgUsers();
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



		/// </summary>
		/// Add a new user to the database, then refresh the datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Nathan VanSnepson & Kiefer Thorson</author>
		/// <created>1/27/23</created>
		private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Open dialog to add new user
            Users_AddNew newUser = new Users_AddNew(_serviceProvider);
            newUser.ShowDialog();

            //Refresh datagrid
            populateDgUsers();
        }

		/// </summary>
		/// Displays the edit user dialog for a selected user.  
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Nathan VanSnepson & Kiefer Thorson</author>
		/// <created>1/27/23</created>
		private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Check is a user was selected to be edited
            if (dtgUsers.SelectedIndex >= 0)
            {
                var selectedUser = users.ToArray()[dtgUsers.SelectedIndex];

                if (selectedUser.IsReadOnly)
                {
                    GrowlHelpers.Error("Cannot edit a read-only user.");
                    return;
                }

                //Opens dialog to edit the user
                Users_AddNew editUser = new Users_AddNew(_serviceProvider, selectedUser.Tuid);
				editUser.ShowDialog();

                //Refreshes datagrid
                populateDgUsers();
            }
        }

		/// </summary>
		/// Deletes a selected user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Nathan VanSnepson & Kiefer Thorson</author>
		/// <created>1/27/23</created>
		private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Check if a user was selected to be deleted
            if (dtgUsers.SelectedIndex >= 0)
            {
                //Get selected user from table
                UserModel user = users.ToArray()[dtgUsers.SelectedIndex];

                if (LoggedInUser != null && LoggedInUser.Tuid == user.Tuid)
                {
                    GrowlHelpers.Error("Cannot delete yourself as a user.");
                    return;
                }

                if (user.IsReadOnly)
                {
                    GrowlHelpers.Error("Cannot delete a read-only user.");
                    return;
                }

                //Confirm if selected user should be deleted
                MessageBoxResult mbrDelete = MessageBox.Show("Are you sure you want to delete user: " + user.Name, "Delete User Confirmation", MessageBoxButton.YesNo);

                //Delete user if confirmed
                if (mbrDelete == MessageBoxResult.Yes)
                {
                    _userProvider.DeleteUser(user);
                    if (errorFlag) { errorFlag = false; return; }
                }

                //Refresh datagrid of users
                populateDgUsers();
            }
        }


		/// </summary>
		/// Populates list of users and the datatabale
		/// </summary>
		/// <author>Nathan VanSnepson & Kiefer Thorson</author>
		/// <created>1/27/23</created>
		private void populateDgUsers()
        {
            users = _userProvider.GetAllUsers().Where(x => !x.IsReadOnly).OrderBy(x => x.Email);
            if (errorFlag) { errorFlag = false; return; }
            dtgUsers.ItemsSource = users;
        }
	}
}
