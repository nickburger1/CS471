using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <FileName> UpdateNeedConditionViewModelBase.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 4/12/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 4/12/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// This is the base view model for the update needs and condition view models.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.AdminTaskViewModels
{
    public abstract class UpdateNeedConditionViewModelBase : ViewModelBase
    {
        protected IStudentProvider? _studentProvider;
        protected IDialogProvider? _dialogProvider;
        protected string _acronym;
        protected string _description;
        protected string _title;
        protected bool _formChanged = false;
        public ICommand UpdateCommand { get; }

        public string Acronym
        {
            get
            { 
                return _acronym;
            }
            set 
            {
                _formChanged = true;
                _acronym = value;
                OnPropertyChanged(nameof(Acronym));
                ValidateNewAcronym();
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _formChanged = true;
                _description = value;
                OnPropertyChanged(nameof(Description));
                ValidateNewDescription();
            }
        }

        /// <summary>
        /// Get status of form.
        /// </summary>
        public bool FormChanged
        {
            get
            {
                return _formChanged;
            }
        }

        /// <summary>
        /// Show alert displaying an error message.
        /// </summary>
        /// <param name="message">_retrieveErrorMessage of alert.</param>
        /// <param name="caption">Type of alert.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        public override void ActionFailed(string message, string caption)
        {
            if (_dialogProvider != null)
            {
                _dialogProvider.ShowAlertDialog(message, caption);
            }
        }

        /// <summary>
        /// Validate the text fields.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public override void Validate()
        {
            ValidateNewDescription();
            ValidateNewAcronym();
        }

        /// <summary>
        /// Validate acronym field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        internal void ValidateNewAcronym()
        {
            ClearErrors(nameof(Acronym));
            if (_acronym.Length > 4)
            {
                AddError(nameof(Acronym), "Acronym cannot be greater than 4 characters.");
            }
            else if (_acronym.Trim().Length == 0)
            {
                AddError(nameof(Acronym), "Acronym is required.");
            }   
        }

        /// <summary>
        /// Validate description field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        internal void ValidateNewDescription()
        {
            ClearErrors(nameof(Description));
            if (_description.Length > 100)
            {
                AddError(nameof(Description), "Description cannot be greater than 100 characters.");
            }
            else if (_description.Trim().Length == 0)
            {
                AddError(nameof(Description), "Description is required.");
            }
        }
    }
}
