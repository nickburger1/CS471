using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System.Collections.ObjectModel;
using System.Windows.Input;


/// <FileName> NeedsViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 4/12/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 4/12/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file bind the student needs datagrid and provide editing and deleting.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.AdminTaskViewModels
{
    public class NeedsViewModel : ViewModelBase
    {
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IStudentProvider _studentProvider;
        private readonly IDialogProvider _dialogProvider;
        private ObservableCollection<StudentNeedItemModel> _studentNeeds;
        private StudentNeedItemModel? _selectedNeed;
        public bool saveSuccess = false;
        private bool errorFlag;
        public ICommand ConfirmDeleteCommand { get; }
        public ICommand UpdateCommand { get;  }

        /// <summary>
        /// List of student needs.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public ObservableCollection<StudentNeedItemModel> StudentNeeds
        {
            get
            {
                return _studentNeeds;
            }
            set
            {
                _studentNeeds = value;
                OnPropertyChanged(nameof(StudentNeeds));
            }
        }

        /// <summary>
        /// Currently selected student need.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public StudentNeedItemModel? SelectedNeed
        {
            get
            {
                return _selectedNeed;
            }
            set
            {
                _selectedNeed = value;
                OnPropertyChanged(nameof(SelectedNeed));
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="volunteerProvider">For getting student needs.</param>
        /// <param name="studentProvider">For deleteing and editing student needs.</param>
        /// <param name="dialogProvider">For showing dialogs.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public NeedsViewModel (IVolunteerProvider volunteerProvider, IStudentProvider studentProvider, IDialogProvider dialogProvider)
        {
            ConfirmDeleteCommand = new ConfirmDeleteCommand(this);
            UpdateCommand = new UpdateCommand(this);

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
            if (_selectedNeed != null && !NeedInUse())
            {
                bool? deleteConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to delete?", "Confirmation");

                if (deleteConfirmed == true)
                {
                    Delete();
                }
            }
        }

        /// <summary>
        /// Delete the selected student need.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void Delete()
        {
            if (_selectedNeed != null)
            {
                _studentProvider.DeleteNeedItem(_selectedNeed.Tuid);
                if (errorFlag) { errorFlag = false; return; }
                try
                {
                    RefreshData();
                }
                catch (RefreshDataCustomException ex) { }
            }
        }

        /// <summary>
        /// Check if the need is in use and display dialog warning if it is.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        /// <returns>True if in use. False if not.</returns>
        private bool NeedInUse()
        {
            bool needInUse = false;

            if (_studentProvider.CheckNeedInUse(_selectedNeed.Tuid))
            {
                needInUse = true;
                _dialogProvider.ShowAlertDialog("Can't delete. This need is being used by one or more students.", "Need In Use");
            }

            return needInUse;
        }

        /// <summary>
        /// Refresh the student needs item collection.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public void RefreshData()
        {
            _studentNeeds = new ObservableCollection<StudentNeedItemModel>(_volunteerProvider.GetAllStudentNeeds());
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            OnPropertyChanged(nameof(StudentNeeds));
        }
    }
}
