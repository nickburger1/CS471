using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <FileName> UpdateNeedViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
/// <DateCreated> 4/12/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 4/12/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// View model for the update need window. For updating needs in database.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.AdminTaskViewModels
{
    public class UpdateNeedViewModel : UpdateNeedConditionViewModelBase
    {
        private StudentNeedItemModel _newStudentNeed;
        private NeedsViewModel _needViewModel;
        private bool errorFlag;
        public ICommand UpdateCommand { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="studentProvider">To call update for need.</param>
        /// <param name="dialogProvider">To display dialogs.</param>
        /// <param name="needViewModel">To get currently selected need and refresh data.</param>
        public UpdateNeedViewModel(IStudentProvider studentProvider, IDialogProvider dialogProvider, NeedsViewModel needViewModel)
        {
            UpdateCommand = new UpdateCommand(this);

            _studentProvider = studentProvider;
            _dialogProvider = dialogProvider;
            _needViewModel = needViewModel;

            _acronym = _needViewModel.SelectedNeed.Acronym;
            OnPropertyChanged(nameof(Acronym));

            _description = _needViewModel.SelectedNeed.Description;
            OnPropertyChanged(nameof(Description));

            _newStudentNeed = new StudentNeedItemModel();

            _newStudentNeed.Tuid = _needViewModel.SelectedNeed.Tuid;
            _newStudentNeed.Acronym = _needViewModel.SelectedNeed.Acronym;
            _newStudentNeed.Description = _needViewModel.SelectedNeed.Description;

            _formChanged = false;

            Validate();
        }

        /// <summary>
        /// Updates an already existing need.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public override void Update()
        {
            _newStudentNeed.Acronym = _acronym;
            _newStudentNeed.Description = _description;

            if (!_formChanged)
            {
                _needViewModel.saveSuccess = true;
            }
            else if (_newStudentNeed != null)
            {
                _needViewModel.saveSuccess = _studentProvider.UpdateNeedItem(_newStudentNeed);
                if (errorFlag) { errorFlag = false; return; }
            }

            _needViewModel.RefreshData();
        }
    }
}
