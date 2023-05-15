using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace B_FGMS.BusinessLogic.ViewModels
{
    /// <summary>
    /// This file defines the View Model for the user login page.
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Email property.
        /// </summary>
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                _formChanged = true;
                OnPropertyChanged(nameof(Email));
                ValidateEmail();
            }
        }

        /// <summary>
        /// Get status of form.
        /// </summary>
        protected bool _formChanged;
        public bool FormChanged
        {
            get
            {
                return _formChanged;
            }
        }

        /// <summary>
        /// Validate email property.
        /// </summary>
        private void ValidateEmail()
        {
            var email = Email?.Trim();

            ClearErrors(nameof(Email));

            if (string.IsNullOrEmpty(email))
            {
                AddError(nameof(Email), "Email is required.");
                return;
            }

            if (!new EmailAddressAttribute().IsValid(email))
            {
                AddError(nameof(Email), "Invalid email format.");
                return;
            }
        }


        /// <summary>
        /// Method calls all of the validations for the form fields.
        /// </summary>
        public void Validate()
        {
            ValidateEmail();
        }
    }
}
