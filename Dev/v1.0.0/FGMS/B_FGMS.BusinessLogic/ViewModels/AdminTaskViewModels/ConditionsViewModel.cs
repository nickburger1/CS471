using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <FileName> ConditionsViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 4/12/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 4/12/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file bind the student conditions datagrid and provide editing and deleting.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.AdminTaskViewModels
{
    public class ConditionsViewModel : ViewModelBase
    {
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IStudentProvider _studentProvider;
        private readonly IDialogProvider _dialogProvider;
        private ObservableCollection<ConditionItemModel> _studentConditions;
        private ConditionItemModel? _selectedCondition;
        public bool saveSuccess = false;
        public ICommand ConfirmDeleteCommand { get; }
        public ICommand EditCommand { get; }
        private bool errorFlag;

        /// <summary>
        /// List of student conditions.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public ObservableCollection<ConditionItemModel> StudentConditions
        {
            get
            {
                return _studentConditions;
            }
            set
            {
                _studentConditions = value;
                OnPropertyChanged(nameof(StudentConditions));
            }
        }

        /// <summary>
        /// Currently selected student condition.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public ConditionItemModel? SelectedCondition
        {
            get
            {
                return _selectedCondition;
            }
            set
            {
                _selectedCondition = value;
                OnPropertyChanged(nameof(SelectedCondition));
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="volunteerProvider">For getting student conditions.</param>
        /// <param name="studentProvider">For deleteing and editing student contions</param>
        /// <param name="dialogProvider">For showing dialogs.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public ConditionsViewModel(IVolunteerProvider volunteerProvider, IStudentProvider studentProvider, IDialogProvider dialogProvider)
        {
            ConfirmDeleteCommand = new ConfirmDeleteCommand(this);
            EditCommand = new UpdateCommand(this);

            _volunteerProvider = volunteerProvider;
            _studentProvider = studentProvider;
            _dialogProvider = dialogProvider;
        }

        /// <summary>
        /// Show dialog and ask use to confirm delete.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public override void ConfirmDelete()
        {
            if (_selectedCondition != null && !ConditionInUse())
            {
                bool? deleteConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to delete?", "Confirmation");

                if (deleteConfirmed == true)
                {
                    Delete();
                }
            }
        }

        /// <summary>
        /// Delete the selected student condition.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void Delete()
        {
            if (_selectedCondition != null)
            {
                _studentProvider.DeleteConditionItem(_selectedCondition.Tuid);
                if (errorFlag) { errorFlag = false; return; }
                RefreshData();
            }
        }

        public override void Update()
        {
        }

        /// <summary>
        /// Check if the condition is in use. Display dialog if it is.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        /// <returns>True if in use. False if not.</returns>
        private bool ConditionInUse()
        {
            bool conditionInUse = false;

            if (_studentProvider.CheckConditionInUse(_selectedCondition.Tuid))
            {
                conditionInUse = true;
                _dialogProvider.ShowAlertDialog("Can't delete. This condition is being used by one or more students.", "Condition In Use");
            }

            return conditionInUse;
        }

        /// <summary>
        /// Refresh the student conditions item collection.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public void RefreshData()
        {
            _studentConditions = new ObservableCollection<ConditionItemModel>(_volunteerProvider.GetAllConditions());
            if (errorFlag) { errorFlag = false; return; }
            OnPropertyChanged(nameof(StudentConditions));
        }
    }
}
