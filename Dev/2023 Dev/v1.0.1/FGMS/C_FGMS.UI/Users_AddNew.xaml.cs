using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.UserProviders;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using C_FGMS.UI.Helpers;
using B_FGMS.BusinessLogic.Events;

/// <FileName>Users_AddNew.xaml.cs</FileName>
/// <PartOfProject>CS471 Senior Capstone Project / FGMS</PartOfProject>
/// <DateCreated>3/16/23</DateCreated>
/// <AdditionalContributors>CS471 WI23 Development Team</AdditionalContributors>
/// <LastModified>3/16/23</LastModified>
/// <LastModifiedBy>Nathan VanSnepson</LastModifiedBy>
/// <summary>
/// The purpose of this file is to add a new user to the current userlist 
/// or update an existing user.
/// </summary>
/// <author>Nathan VanSnepson</author>
namespace C_FGMS.UI
{
	/// <summary>
	/// Interaction logic for Users_AddNew.xaml
	/// </summary>
	/// <author>Nathan VanSnepson</author>
	/// <created>3/16/23</created>
	public partial class Users_AddNew : System.Windows.Window
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IUserProvider _userProvider;
		private readonly IDialogProvider _dialogProvider;
		private readonly bool boolNewUser = true;
		private readonly UserModel _userModel;
		private bool isSave = false;
        private bool errorFlag;
        private readonly char[] VALIDPASSWORDCHARACTERS = { '!', '@', '#', '%', '-', '_', '?', '&', '*', '%' };

		#region Constructor

		/// <summary>
		/// Constructor for the new user/ edit user page. the intUserTuid has a default
		/// value for the case of a new user
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <param name="intUserTuid"></param>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		public Users_AddNew(IServiceProvider serviceProvider, int intUserTuid = 0)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userProvider = serviceProvider.GetRequiredService<IUserProvider>();
			_dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

			_userProvider.DatabaseError += ErrorHandler;

            //Ensure error Textblocks are empty
            ResetErrors();


            //If the tuid is not 0, then populate with exsisting user
            if (intUserTuid != 0)
            {
				this.boolNewUser = false;
				txtPageHeader.Text = "Edit User";
				_userModel = populateExistingUser(intUserTuid);
            }
            else
            {
				_userModel = new UserModel();
			}
        }

        #endregion

        #region Error Handler
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

        #endregion

        #region User Methods
        private void ResetErrors()
		{
			txtErrorName.Text = "";
			txtErrorEmail.Text = "";
			txtErrorPassword.Text = "";
			txtErrorPassword2.Text = "";
			txtErrorPhoneNum.Text = "";
		}

        private string FormatPhoneNumber(string text)
        {
            // Remove any non-digit characters from the input
            string digitsOnly = new string(text.Where(char.IsDigit).ToArray());

            // If the input contains less than 10 digits, return it as-is
            if (digitsOnly.Length < 10)
            {
                return digitsOnly;
            }

            // If the input contains more than 10 digits, truncate the extra digits
            if (digitsOnly.Length > 10)
            {
                digitsOnly = digitsOnly.Substring(0, 10);
            }

            // Format the phone number in the standard format
            return string.Format("({0}) {1}-{2}", digitsOnly.Substring(0, 3), digitsOnly.Substring(3, 3), digitsOnly.Substring(6, 4));
        }
        #endregion

        #region Set Inputs
        /// <summary>
        /// Populates exising user to edit page
        /// </summary>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/16/23</created>
        private UserModel populateExistingUser(int intTuid)
        {
            UserModel user = _userProvider.GetUser(intTuid);
            if (errorFlag) { errorFlag = false; return new UserModel(); }

            txtName.Text = user.Name;
            txtEmail.Text = user.Email;
            txtPhoneNum.Text = user.PhoneNumber;
            rdoActive.IsChecked = user.IsActive;
            rdoInactive.IsChecked = !user.IsActive;
            chkIsAdmin.IsChecked = user.IsAdmin;

            return user;
        }
        #endregion

        #region Text Change Events
        private void PhoneTextInput(object sender, TextCompositionEventArgs e)
        {
            // Get the current text and selection start/end positions
            string text = txtPhoneNum.Text;
            int selectionStart = txtPhoneNum.SelectionStart;
            int selectionLength = txtPhoneNum.SelectionLength;

            // Check if the new text will be valid according to the phone number format
            string newText = text.Substring(0, selectionStart) + e.Text + text.Substring(selectionStart + selectionLength);
            if (!PhoneNumberHelper.IsValidPhoneNumber(newText))
            {
                // Cancel the event if the new text is not valid
                e.Handled = true;
                return;
            }

            // Format the phone number in real-time
            string formattedText = FormatPhoneNumber(newText);

            // Update the text and selection start/end positions
            txtPhoneNum.Text = formattedText;
            txtPhoneNum.SelectionStart = selectionStart + e.Text.Length;
            txtPhoneNum.SelectionLength = 0;

            // Cancel the event
            e.Handled = true;
        }    
        #endregion

        #region button events

        /// <summary>
        /// Updates or creates a user, decides if the user is new or existing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/16/23</created>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            // Should have a limiting factor making sure all fields are filled in before allowed to save
            if (validateInputs())
            {
                UserModel user = new UserModel()
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhoneNum.Text,
                    IsActive = rdoActive.IsChecked.Value,
                    IsAdmin = chkIsAdmin.IsChecked.Value
                };

                if (boolNewUser)
                {
                    _userProvider.CreateUser(user, txtPassword.Password.ToString());
                    if (errorFlag) { errorFlag = false; return; }
                }
                else 
                {
                    user.Tuid = _userModel.Tuid;
                    _userProvider.UpdateUser(user, txtPassword.Password.Length > 0? txtPassword.Password : null);
                    if (errorFlag) { errorFlag = false; return; }
                }

				//Tell system that it was a save event
				isSave = true;

                this.Close(); 
            }
        }


		/// <summary>
		/// Closes the add user page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
			this.Close();
        }

		#endregion

		#region override methods
		/// <summary>
		/// Closes the add user page. Prompts user before closing if there are unsaved changes.
		/// </summary>
		/// <param name="e"></param>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		protected override void OnClosing(CancelEventArgs e)
		{
			//default form to not close
			e.Cancel = true;

			//if no changes were made or the user accepts the dialog, sets the form to close
			if (NameUnchanged() && EmailUnchanged() && PasswordUnchanged() && ReEnterPasswordUnchanged() && StatusUnchanged() && AdminUnchanged())
			{
				e.Cancel = false;
			}else if (isSave)
			{
				e.Cancel= false;
			}
			else
			{
				bool closeConfirmed = (bool)_dialogProvider.ShowConfirmationDialog("Are you sure you want to exit? Changes won't be saved.", "Confirmation");

				if (closeConfirmed)
				{
					e.Cancel = false;
				}
			}

			//closes the form
			base.OnClosing(e);
		}
		#endregion

		#region data changed checks
		/// <summary>
		/// Checks if the name is unchanged
		/// </summary>
		/// <returns>true if the name was unchanged</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool NameUnchanged()
        {
            return txtName.Text.Equals(_userModel.Name);
        }

		/// <summary>
		/// Checks if the email is unchanged
		/// </summary>
		/// <returns>true if the email was unchanged</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool EmailUnchanged()
		{
			return txtEmail.Text.Equals(_userModel.Email);
		}

		/// <summary>
		/// Checks if the password is unchanged
		/// </summary>
		/// <returns>true if the password was unchanged</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool PasswordUnchanged()
		{
			return txtPassword.Password.Length == 0;
		}

		/// <summary>
		/// Checks if the re-entered password is unchanged
		/// </summary>
		/// <returns>true if the re-entered password was unchanged</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool ReEnterPasswordUnchanged()
		{
			return txtPassword2.Password.Length == 0;
		}

		/// <summary>
		/// Checks if the status is unchanged
		/// </summary>
		/// <returns>true if the status was unchanged</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool StatusUnchanged()
		{
			return rdoActive.IsChecked == _userModel.IsActive;
		}

		/// <summary>
		/// Checks if the admin is unchanged
		/// </summary>
		/// <returns>true if the admin was unchanged</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool AdminUnchanged()
		{
			return chkIsAdmin.IsChecked == _userModel.IsAdmin;
		}

		#endregion

		#region Validate inputs
		/// <summary>
		/// Checks validation
		/// </summary>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool validateInputs()
		{
			bool validateInputs = true;

			validateInputs = ValidateName() && validateInputs;
			validateInputs = ValidateEmail() && validateInputs;
			validateInputs = ValidatePassword() && validateInputs;
			validateInputs = ValidateReEnteredPassword() && validateInputs;
			validateInputs = ValidatePhoneNumber() && validateInputs;

			return validateInputs;
		}

        /// <summary>
        /// Validates the Name
        /// </summary>
        /// <returns>True if it is valid</returns>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/16/23</created>
        private bool ValidateName()
		{
			if(txtName.Text.Length <= 0)
			{
				txtErrorName.Text = "Enter the User's name!";
                return false;
			}else if(txtName.Text.Length > 100)
			{
                txtErrorName.Text = "Name cannot be longer than 100 characters!";
                return false;
			}

			txtErrorName.Text = "";
			return true;
		}

		/// <summary>
		/// Validates the Email
		/// </summary>
		/// <returns>True if it is valid</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool ValidateEmail()
		{
			if (boolNewUser && _userProvider.EmailExists(txtEmail.Text))
			{
                if (errorFlag) { errorFlag = false; return false; }
                txtErrorEmail.Text = "Email already exists!";
                return false;
            }else if(_userProvider.EmailExistsForOtherUser(txtEmail.Text, _userModel.Tuid))
			{
                if (errorFlag) { errorFlag = false; return false; }
                txtErrorEmail.Text = "Email already exists!";
                return false;
			}
			else if(txtEmail.Text.Length <= 0)
			{
                txtErrorEmail.Text = "Enter the User's email!";
				return false;
			}else if(txtEmail.Text.Length > 100)
			{
                txtErrorEmail.Text = "Email cannot be longer than 100 characters!";
				return false;
			}else if (!(new EmailAddressAttribute().IsValid(txtEmail.Text)))
			{
                txtErrorEmail.Text = "Invalid email format.";
				return false;
            }
            if (errorFlag) { errorFlag = false; return false; }

            txtErrorEmail.Text = "";
			return true;
		}

		/// <summary>
		/// Validates the Phone Number
		/// </summary>
		/// <returns>True if it is valid</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool ValidatePhoneNumber()
		{
            if (!PhoneNumberHelper.IsValidPhoneNumber(txtPhoneNum.Text))
			{
				txtErrorPhoneNum.Text = "Invalid phone number!";
				return false;
			}else if(txtPhoneNum.Text.Length != 14)
			{
                txtErrorPhoneNum.Text = "Incomplete phone number!";
                return false;
            }


			txtErrorPhoneNum.Text = "";
			return true;
		}

		/// <summary>
		/// Validates the Password
		/// </summary>
		/// <returns>True if it is valid</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool ValidatePassword()
		{
			if ((!boolNewUser && txtPassword.Password.Length > 0) || boolNewUser)
			{
				if (txtPassword.Password.Length < 8)
				{
					txtErrorPassword.Text = "Password must be at least 8 characters!";
					return false;
				}
				else if (!txtPassword.Password.Any(c => char.IsUpper(c)))
				{
					txtErrorPassword.Text = "Password must contain a capital letter.";
					return false;
				}
				else if (!txtPassword.Password.Any(c => char.IsLower(c)))
				{
					txtErrorPassword.Text = "Password must contain a lowercase letter.";
					return false;
				}
				else if (!txtPassword.Password.Any(c => char.IsNumber(c)))
				{
					txtErrorPassword.Text = "Password must contain a number.";
					return false;
				}
				else if (!txtPassword.Password.Any(c => VALIDPASSWORDCHARACTERS.Contains(c)))
				{
					txtErrorPassword.Text = "Password must contain a symbol: !@#%-_?&*%";
					return false;
				}
			}

			txtErrorPassword.Text = "";
            return true;
		}

		/// <summary>
		/// Validates the Re-entered Password
		/// </summary>
		/// <returns>True if it is valid</returns>
		/// <author>Nathan VanSnepson</author>
		/// <created>3/16/23</created>
		private bool ValidateReEnteredPassword()
		{
			if (!txtPassword2.Password.Equals(txtPassword.Password))
			{
				txtErrorPassword2.Text = "Reentered password does not match the password";
				return false;
			}

			txtErrorPassword2.Text = "";
			return true;
		}

		#endregion
	}
}
