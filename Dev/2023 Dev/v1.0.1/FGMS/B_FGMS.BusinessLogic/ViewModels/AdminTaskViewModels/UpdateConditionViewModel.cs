using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace B_FGMS.BusinessLogic.ViewModels.AdminTaskViewModels
{
    /// <FileName> UpdateConditionViewModel.xaml.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
    /// <DateCreated> 04/12/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 04/12/2023 </LastModified>
    /// <LastModifiedBy> Tyler Moody </LastModifiedBy>
    /// <summary>
    /// View Model for the update condition window. For updating a condition record.
    /// </summary>
    /// <author> Tyler Moody </author>
    public class UpdateConditionViewModel : UpdateNeedConditionViewModelBase
    {
        private ConditionItemModel _newConditionItem;
        private ConditionsViewModel _conditionViewModel;
        private bool errorFlag;
        public ICommand UpdateCommand { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="studentProvider">For calling update condition method.</param>
        /// <param name="dialogProvider">For displaying dialogs.</param>
        /// <param name="conditionViewModel">To get currently selected condition.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>s
        public UpdateConditionViewModel(IStudentProvider studentProvider, IDialogProvider dialogProvider, ConditionsViewModel conditionViewModel)
        {
            UpdateCommand = new UpdateCommand(this);

            _studentProvider = studentProvider;
            _dialogProvider = dialogProvider;
            _conditionViewModel = conditionViewModel;

            _acronym = _conditionViewModel.SelectedCondition.Acronym;
            OnPropertyChanged(nameof(Acronym));

            _description = _conditionViewModel.SelectedCondition.Description;
            OnPropertyChanged(nameof(Description));

            _newConditionItem = new ConditionItemModel();

            _newConditionItem.Tuid = _conditionViewModel.SelectedCondition.Tuid;
            _newConditionItem.Acronym = _conditionViewModel.SelectedCondition.Acronym;
            _newConditionItem.Description = _conditionViewModel.SelectedCondition.Description;

            _formChanged = false;

            Validate();
        }

        /// <summary>
        /// Updates an already existing condition.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public override void Update()
        {
            _newConditionItem.Acronym = _acronym;
            _newConditionItem.Description = _description;

            if (!_formChanged)
            {
                _conditionViewModel.saveSuccess = true;
            }
            else if (_newConditionItem != null)
            {
                _conditionViewModel.saveSuccess = _studentProvider.UpdateCondtionItem(_newConditionItem);
                if (errorFlag) { errorFlag = false; return; }
            }

            try
            {
                _conditionViewModel.RefreshData();
            }
            catch (RefreshDataCustomException ex) { }
        }
    }
}
