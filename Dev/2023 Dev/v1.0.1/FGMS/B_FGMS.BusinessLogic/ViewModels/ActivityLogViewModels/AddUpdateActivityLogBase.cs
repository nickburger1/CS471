using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> AddUpdateActivityLogBase.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
/// <DateCreated> 2/22/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/22/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// This class is used for both adding and updating activity logs since both have similar fields.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels
{
    public class AddUpdateActivityLogBase : ViewModelBase
    {
        protected ActivityLogModel? _newActivityLog;
        protected IActivityLogProvider? _activityLogProvider;
        protected IDialogProvider? _dialogProvider;
        protected ActivityLogViewModel? _activityLogViewModel;
        protected string _newInitial = "";
        protected string _newIncident = "";
        protected DateTime _newDate;
        protected DateTime _dateBeforeEdit;
        protected bool _formChanged;


        /// <summary>
        /// ActivityLog Date being updated.
        /// </summary>
        public DateTime NewDate
        {
            get
            {
                return _newDate;
            }
            set
            {
                _newDate = value;

                if (_newActivityLog!= null)
                {
                    _newActivityLog.Date = value;
                }

                _formChanged = true;
                OnPropertyChanged(nameof(NewDate));
            }
        }

        /// <summary>
        /// ActivityLog initial being updated.
        /// </summary>
        public string NewInitial
        {
            get
            {
                return _newInitial;
            }
            set
            {
                _newInitial = value;

                if (_newActivityLog!= null)
                {
                    _newActivityLog.Initial = value;
                }

                _formChanged = true;
                OnPropertyChanged(nameof(NewInitial));
                ValidateNewInitial();
            }
        }

        /// <summary>
        /// ActivityLog Incident being updated.
        /// </summary>
        public string NewIncident
        {
            get
            {
                return _newIncident;
            }
            set
            {
                _newIncident = value;

                if (_newActivityLog != null)
                {
                    _newActivityLog.Incident = value;
                }

                _formChanged = true;
                OnPropertyChanged(nameof(NewIncident));
                ValidateNewIncident();
            }
        }

        /// <summary>
        /// To keep track if date was changed.
        /// </summary>
        public DateTime DateBeforeEdit
        {
            get
            {
                return _dateBeforeEdit;
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
            ValidateNewIncident();
            ValidateNewInitial();
        }

        /// <summary>
        /// Validate initial field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        internal void ValidateNewInitial()
        {
            ClearErrors(nameof(NewInitial));
            if (_newInitial.Length > 10)
            {
                AddError(nameof(NewInitial), "Initial cannot be greater than 10 characters.");
            }
            else if (_newInitial.Trim().Length == 0)
            {
                AddError(nameof(NewInitial), "Initial is required.");
            }
        }

        /// <summary>
        /// Validate incident field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        internal void ValidateNewIncident()
        {
            ClearErrors(nameof(NewIncident));
            if (_newIncident.Length > 150)
            {
                AddError(nameof(NewIncident), "Incident cannot be greater than 150 characters.");
            }
            else if (_newIncident.Trim().Length == 0)
            {
                AddError(nameof(NewIncident), "Incident is required.");
            }
        }

    }
}
