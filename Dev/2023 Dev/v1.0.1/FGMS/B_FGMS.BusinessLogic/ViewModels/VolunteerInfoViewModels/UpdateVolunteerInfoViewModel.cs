using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <FileName> UpdateVolunteerInfoViewModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 3/25/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 3/25/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this file is to manage data and functionality for the volunteer info page.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.ViewModels.VolunteerInfoViewModels
{
    public class UpdateVolunteerInfoViewModel : ViewModelBase
    {
        private VolunteerInfoViewModel? _volunteerInfoViewModel;
        private VolunteerInfoReportModel? _updatedVolunteerInfo;
        private ObservableCollection<GenderNameIdModel> _genderNames;
        private ObservableCollection<IdentifiesAsNameIdModel> _identifies;
        private ObservableCollection<EthnicityNameIdModel> _ethnicities;
        private ObservableCollection<RacialGroupNameIdModel> _racialGroups;
        private ObservableCollection<InactiveStatusTypesNameIdModel> _inactiveStatuses;
        private GenderNameIdModel? _selectedGender;
        private IdentifiesAsNameIdModel? _selectedIdentifies;
        private EthnicityNameIdModel? _selectedEthnicity;
        private RacialGroupNameIdModel? _selectedRacialGroup;
        private InactiveStatusTypesNameIdModel? _selectedInactiveStatusType;
        private IVolunteerProvider _volunteerProvider;
        private DateTime? _dateOfBirth;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _firstName;
        private string _lastName;
        private bool _isVeteran;
        private bool _isFamilyOfMilitary;
        private bool _isActive;
        public bool formChanged;
        public int initialGenderIndex;
        public int initialIdentifiesIndex;
        public int initialEthnicityIndex;
        public int initialRacialGroupIndex;
        public int initialReasonSeparatedIndex;
        public ICommand UpdateCommand { get; }

        /// <summary>
        /// Volunteer info being updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>s
        public VolunteerInfoReportModel? UpdatedVolunteerInfo
        {
            get
            {
                return _updatedVolunteerInfo;
            }
            set
            {
                _updatedVolunteerInfo = value;
                OnPropertyChanged(nameof(UpdatedVolunteerInfo));
            }
        }

        /// <summary>
        /// List of genders.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public ObservableCollection<GenderNameIdModel> Genders
        {
            get
            {
                return _genderNames;
            }
            set
            {
                _genderNames = value;
                OnPropertyChanged(nameof(Genders));
            }
        }

        /// <summary>
        /// List of identifiesNameId as.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public ObservableCollection<IdentifiesAsNameIdModel> Identifies
        {
            get
            {
                return _identifies;
            }
            set
            {
                _identifies = value;
                OnPropertyChanged(nameof(Identifies));
            }
        }

        /// <summary>
        /// List of ethnicities.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public ObservableCollection<EthnicityNameIdModel> Ethnicity
        {
            get
            {
                return _ethnicities;
            }
            set
            {
                _ethnicities = value;
                OnPropertyChanged(nameof(Ethnicity));
            }
        }

        /// <summary>
        /// List of racial groups.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public ObservableCollection<RacialGroupNameIdModel> RacialGroup
        {
            get
            {
                return _racialGroups;
            }
            set
            {
                _racialGroups = value;
                OnPropertyChanged(nameof(RacialGroup));
            }
        }

        /// <summary>
        /// List of inactive reasons.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public ObservableCollection<InactiveStatusTypesNameIdModel> InactiveStatusTypes
        {
            get
            {
                return _inactiveStatuses;
            }
            set
            {
                _inactiveStatuses = value;
                OnPropertyChanged(nameof(InactiveStatusTypes));
            }
        }

        /// <summary>
        /// Selected genderNameId.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public GenderNameIdModel SelectedGender
        {
            get
            {
                return _selectedGender;
            }
            set
            {
                _selectedGender = value;
                _updatedVolunteerInfo.GenderNameAndId = _selectedGender;
                OnPropertyChanged(nameof(SelectedGender));
                ValidateGender();

                formChanged = true;
            }
        }

        /// <summary>
        /// Selected identifiesNameId.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public IdentifiesAsNameIdModel SelectedIdentifiesAs
        {
            get
            {
                return _selectedIdentifies;
            }
            set
            {
                _selectedIdentifies = value;
                _updatedVolunteerInfo.IdentifiesNameAndId = _selectedIdentifies;
                OnPropertyChanged(nameof(SelectedIdentifiesAs));
                ValidateIdentifiesAs();

                formChanged = true;
            }
        }

        /// <summary>
        /// Selected ethnicity.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public EthnicityNameIdModel SelectedEthnicity
        {
            get
            {
                return _selectedEthnicity;
            }
            set
            {
                _selectedEthnicity = value;
                _updatedVolunteerInfo.EthnicityNameAndId = _selectedEthnicity;
                OnPropertyChanged(nameof(SelectedEthnicity));
                ValidateEthnicity();

                formChanged = true;
            }
        }

        /// <summary>
        /// Selected racial group.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public RacialGroupNameIdModel SelectedRacialGroup
        {
            get
            {
                return _selectedRacialGroup;
            }
            set
            {
                _selectedRacialGroup = value;
                _updatedVolunteerInfo.RacialGroupNameAndId = _selectedRacialGroup;
                OnPropertyChanged(nameof(SelectedRacialGroup));
                ValidateRacialGroup();

                formChanged = true;
            }
        }

        /// <summary>
        /// Selected reason volunteer is inactive.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public InactiveStatusTypesNameIdModel SelectedInactiveStatus
        {
            get
            {
                return _selectedInactiveStatusType;
            }
            set
            {
                _selectedInactiveStatusType = value;
                _updatedVolunteerInfo.InactiveStatusNameAndId = _selectedInactiveStatusType;
                OnPropertyChanged(nameof(SelectedInactiveStatus));
                ValidateInactiveStatus();

                formChanged = true;
            }
        }


        /// <summary>
        /// Date volunteer started program.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/26/2023</created>
        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                _updatedVolunteerInfo.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
                ValidateStartDate();

                formChanged = true;
            }
        }

        /// <summary>
        /// Volunteer date of birth.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public DateTime? DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                _updatedVolunteerInfo.Demographics.DateOfBirth = (DateTime)value;
                OnPropertyChanged(nameof(DateOfBirth));
                ValidateDateOfBirth();

                formChanged = true;
            }
        }

        /// <summary>
        /// Date volunteer left program.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/26/2023</created>
        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                _updatedVolunteerInfo.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
                ValidateEndDate();

                formChanged = true;
            }
        }

        /// <summary>
        /// First name of volunteer.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/26/2023</created>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                _updatedVolunteerInfo.Volunteer.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
                ValidateFirstName();

                formChanged = true;
            }
        }

        /// <summary>
        /// Last name of volunteer.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/26/2023</created>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                _updatedVolunteerInfo.Volunteer.LastName = value;
                OnPropertyChanged(nameof(LastName));
                ValidateLastName();

                formChanged = true;
            }
        }


        /// <summary>
        /// Active status of voluntneer.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                _updatedVolunteerInfo.Demographics.Status = _isActive ? "Active" : "Inactive";

                _endDate = IsActive ? null : DateTime.Now;
                _selectedInactiveStatusType = IsActive ? null : _selectedInactiveStatusType = _inactiveStatuses.FirstOrDefault();
                
                if (_isActive)
                {
                    _inactiveStatuses = ResetCollection(_inactiveStatuses);
                }

                _updatedVolunteerInfo.EndDate = _endDate;
                _updatedVolunteerInfo.InactiveStatusNameAndId= _selectedInactiveStatusType;

                OnPropertyChanged(nameof(IsActive));
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(InactiveStatusTypes));
                OnPropertyChanged(nameof(SelectedInactiveStatus));
                OnPropertyChanged(nameof(UpdatedVolunteerInfo));
                ValidateEndDate();
                ValidateInactiveStatus();

                formChanged = true;
            }
        }

        /// <summary>
        /// Resets a collection with a copy of itself. This function is required to fix a combobox bug
        /// that prevents the combobox from clearing if the value is null.
        /// </summary>
        /// <typeparam name="IType">Type of collection.</typeparam>
        /// <param name="collection">Collection being reset.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        /// <returns>The same collection that was passed.</returns>
        private ObservableCollection<IType> ResetCollection<IType>(ObservableCollection<IType> collection)
        {
            var copy = new ObservableCollection<IType>(collection);

            collection.Clear();

            return new ObservableCollection<IType>(copy);
        }

        /// <summary>
        /// Family of military status of volunteer.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public bool IsFamilyOfMilitary
        {
            get
            {
                return _isFamilyOfMilitary;
            }
            set
            {
                _isFamilyOfMilitary = value;
                _updatedVolunteerInfo.Demographics.FamilyOfMilitary = _isFamilyOfMilitary ? "Yes" : "No";
                OnPropertyChanged(nameof(IsFamilyOfMilitary));

                formChanged = true;
            }
        }

        /// <summary>
        /// Veteran status of volunteer.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public bool IsVeteran
        {
            get
            {
                return _isVeteran;
            }
            set
            {
                _isVeteran = value;
                _updatedVolunteerInfo.Demographics.Veteran = _isVeteran ? "Yes" : "No";
                OnPropertyChanged(nameof(IsVeteran));

                formChanged = true;
            }
        }

        /// <summary>
        /// Constructor for view model.
        /// </summary>
        /// <param name="volunteerProvider">Volunteer provider to get data from database.</param>
        /// <param name="volunteerInfoViewModel">Parent view model. Contains selectedVolunteerInfo needed.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public UpdateVolunteerInfoViewModel(IVolunteerProvider volunteerProvider, VolunteerInfoViewModel? volunteerInfoViewModel)
        {
            UpdateCommand = new UpdateCommand(this);

            _volunteerProvider = volunteerProvider;
            _volunteerInfoViewModel = volunteerInfoViewModel;

            VolunteerInfoReportModel selectedVolunteerInfo = _volunteerInfoViewModel.SelectedVolunteerInfo;
            _updatedVolunteerInfo = CopySelectedVolunteerInfoProperties(selectedVolunteerInfo);

            _genderNames = new ObservableCollection<GenderNameIdModel>(_volunteerProvider.GetGenderNameAndId(false));
            OnPropertyChanged(nameof(Genders));
            SetInitialGender(selectedVolunteerInfo.Demographics.Gender);

            _identifies = new ObservableCollection<IdentifiesAsNameIdModel>(_volunteerProvider.GetIdentifiesAsNameAndId(false));
            OnPropertyChanged(nameof(Identifies));
            SetInitialIdentifiesAs(selectedVolunteerInfo.Demographics.IdentifiesAs);

            _ethnicities = new ObservableCollection<EthnicityNameIdModel>(_volunteerProvider.GetEthnityNameAndId(false));
            OnPropertyChanged(nameof(Ethnicity));
            SetInitialEthnicity(selectedVolunteerInfo.Demographics.Ethnicity);

            _racialGroups = new ObservableCollection<RacialGroupNameIdModel>(_volunteerProvider.GetRacialGroupNameAndId(false));
            OnPropertyChanged(nameof(RacialGroup));
            SetInitialRacialGroup(selectedVolunteerInfo.Demographics.RacialGroup);

            _inactiveStatuses = new ObservableCollection<InactiveStatusTypesNameIdModel>(volunteerProvider.GetInactiveStatusTypes());
            OnPropertyChanged(nameof(InactiveStatusTypes));
            SetInitialInactiveStatus(selectedVolunteerInfo.InactiveStatusNameAndId.Name);

            _isVeteran = CheckIfVeteran();
            OnPropertyChanged(nameof(IsVeteran));

            _isFamilyOfMilitary = CheckIfFamilyOfMilitary();
            OnPropertyChanged(nameof(IsFamilyOfMilitary));

            _isActive = CheckIfActive();
            OnPropertyChanged(nameof(IsActive));

            _firstName = _updatedVolunteerInfo.Volunteer.FirstName;
            OnPropertyChanged(nameof(FirstName));

            _lastName = _updatedVolunteerInfo.Volunteer.LastName;
            OnPropertyChanged(nameof(LastName));

            _dateOfBirth = _updatedVolunteerInfo.Demographics.DateOfBirth;
            OnPropertyChanged(nameof(DateOfBirth));

            _startDate = _updatedVolunteerInfo.StartDate;
            OnPropertyChanged(nameof(StartDate));

            _endDate = _updatedVolunteerInfo.EndDate;
            OnPropertyChanged(nameof(EndDate));

            formChanged = false;
        }

        /// <summary>
        /// Update the selected volunteer info if valid.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public override void Update()
        {
            if (!formChanged)
            {
                _volunteerInfoViewModel.saveSuccess = true;
            }
            else if (_updatedVolunteerInfo != null)
            {
                _volunteerInfoViewModel.saveSuccess =_volunteerProvider.UpdateVolunteerInfo(_updatedVolunteerInfo);
            }
            _volunteerInfoViewModel.RefreshVolunteers();
            _volunteerInfoViewModel.RefreshVolunteerInfo();
        }

        /// <summary>
        /// Validate all fields are correct.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        public override void Validate()
        {
            ValidateDateOfBirth();
            ValidateStartDate();
            ValidateEndDate();
            ValidateGender();
            ValidateEthnicity();
            ValidateFirstName();
            ValidateLastName();
            ValidateIdentifiesAs();
            ValidateInactiveStatus();
            ValidateRacialGroup();
        }

        /// <summary>
        /// Copy the selected volunteer info properties. This is so that changes can be compared later.
        /// </summary>
        /// <param name="selectedVolunteerInfo">The selected VolunteerInfoReportModel</param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private static VolunteerInfoReportModel CopySelectedVolunteerInfoProperties(VolunteerInfoReportModel selectedVolunteerInfo)
        {
            return new VolunteerInfoReportModel()
            {
                Volunteer = new Models.VolunteerModel()
                {
                    Tuid = selectedVolunteerInfo.Volunteer.Tuid,
                    FirstName = selectedVolunteerInfo.Volunteer.FirstName,
                    LastName = selectedVolunteerInfo.Volunteer.LastName,
                },
                Demographics = new VolunteerDemographicsModel()
                {
                    Status = selectedVolunteerInfo.Demographics.Status,
                    DateOfBirth = selectedVolunteerInfo.Demographics.DateOfBirth,
                    Gender = selectedVolunteerInfo.Demographics.Gender,
                    IdentifiesAs = selectedVolunteerInfo.Demographics.IdentifiesAs,
                    Ethnicity = selectedVolunteerInfo.Demographics.Ethnicity,
                    RacialGroup = selectedVolunteerInfo.Demographics.RacialGroup,
                    Veteran = selectedVolunteerInfo.Demographics.Veteran,
                    FamilyOfMilitary = selectedVolunteerInfo.Demographics.FamilyOfMilitary
                },
                InactiveStatusNameAndId = new InactiveStatusTypesNameIdModel(),
                StartDate = selectedVolunteerInfo.StartDate,
                EndDate = selectedVolunteerInfo.EndDate,
                ReasonSeparatedTuid = selectedVolunteerInfo.ReasonSeparatedTuid
            };
        }

        /// <summary>
        /// Validate First Name field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        internal void ValidateFirstName()
        {
            ClearErrors(nameof(FirstName));
            if (_firstName.Length > 45)
            {
                AddError(nameof(FirstName), "Cannot be more than 45 characters.");
            }
            else if (_firstName.Trim().Length == 0)
            {
                AddError(nameof(FirstName), "First name is required.");
            }
        }

        /// <summary>
        /// Validate Last Name field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        internal void ValidateLastName()
        {
            ClearErrors(nameof(LastName));
            if (_lastName.Length > 45)
            {
                AddError(nameof(LastName), "Cannot be more than 45 characters.");
            }
            else if (_lastName.Trim().Length == 0)
            {
                AddError(nameof(LastName), "Last name is required.");
            }
        }

        /// <summary>
        /// Validate date of birth field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        internal void ValidateDateOfBirth()
        {
            ClearErrors(nameof(DateOfBirth));
            if (_dateOfBirth == null)
            {
                AddError(nameof(DateOfBirth), "Date of birth is required.");
            }
        }


        /// <summary>
        /// Validate start date field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        internal void ValidateStartDate()
        {
            ClearErrors(nameof(StartDate));
            if (_startDate == null)
            {
                AddError(nameof(StartDate), "Start date is required.");
            }
        }

        /// <summary>
        /// Validate End date field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        internal void ValidateEndDate()
        {
            ClearErrors(nameof(EndDate));
            if (_endDate == null && !_isActive)
            {
                AddError(nameof(EndDate), "Inactive volunteer must have end date.");
            }
            else if (_endDate != null && _isActive)
            {
                AddError(nameof(EndDate), "Active volunteer should not have end date.");
            }
            else if (_endDate != null && _updatedVolunteerInfo.StartDate != null && ((DateTime)_endDate).Date < ((DateTime)_updatedVolunteerInfo.StartDate).Date)
            {
                AddError(nameof(EndDate), "End date cannot be before start date.");
            }  
        }

        /// <summary>
        /// Validate genderNameId field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        internal void ValidateGender()
        {
            ClearErrors(nameof(SelectedGender));
            if (_selectedGender == null)
            {
                AddError(nameof(SelectedGender), "Gender is required.");
            }
        }

        /// <summary>
        /// Validate identifiesNameId as field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        internal void ValidateIdentifiesAs()
        {
            ClearErrors(nameof(SelectedIdentifiesAs));
            if (_selectedIdentifies == null)
            {
                AddError(nameof(SelectedIdentifiesAs), "Identifies as is required.");
            }
        }

        /// <summary>
        /// Validate ethnicity field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        internal void ValidateEthnicity()
        {
            ClearErrors(nameof(SelectedEthnicity));
            if (_selectedEthnicity == null)
            {
                AddError(nameof(SelectedEthnicity), "Ethnicity is required.");
            }
        }

        /// <summary>
        /// Validate racial group field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        internal void ValidateRacialGroup()
        {
            ClearErrors(nameof(SelectedRacialGroup));
            if (_selectedRacialGroup == null)
            {
                AddError(nameof(SelectedRacialGroup), "Racial group is required.");
            }
        }

        /// <summary>
        /// Validate inactive status field has no errors.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        internal void ValidateInactiveStatus()
        {
            ClearErrors(nameof(SelectedInactiveStatus));
            if (_selectedInactiveStatusType == null && !_isActive) 
            {
                AddError(nameof(SelectedInactiveStatus), "Inactive volunteer must have reason separated.");
            }
            else if (_selectedInactiveStatusType != null && _isActive)
            {
                AddError(nameof(SelectedInactiveStatus), "Active volunteer should not have reason separated.");
            }
        }

        /// <summary>
        /// Check if the volunteer is active.
        /// </summary>
        /// <returns>True if active, False if not.</returns>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private bool CheckIfActive()
        {
            return (_updatedVolunteerInfo.Demographics.Status == "Active");
        }

        /// <summary>
        /// Check if the volunteer is a family of military.
        /// </summary>
        /// <returns>True if family of military, false if not.</returns>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private bool CheckIfFamilyOfMilitary()
        {
            return (_updatedVolunteerInfo.Demographics.FamilyOfMilitary == "Yes");
        }

        /// <summary>
        /// Check if volunteer is veteran.
        /// </summary>
        /// <returns>True if veteran, false if not.</returns>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private bool CheckIfVeteran()
        {
            return (_updatedVolunteerInfo.Demographics.Veteran == "Yes");
        }


        /// <summary>
        /// Check if the form was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        public bool FormUnchanged()
        {
            formChanged = NameUnchanged()
                && DateOfBirthUnchanged()
                && GenderUnchanged()
                && IdentifiesAsUnchanged()
                && EthnicityUnchanged()
                && RacialGroupUnchanged()
                && VeteranUnchanged()
                && FamilyOfMilitaryUnchanged()
                && StatusUnchanged()
                && StartDateUnchanged()
                && EndDateUnchanged()
                && ReasonSeparatedUnchanged();

            return formChanged;
        }

        /// <summary>
        /// Check if reason seperated was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool ReasonSeparatedUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.InactiveStatusNameAndId.Name == UpdatedVolunteerInfo.InactiveStatusNameAndId.Name;
        }


        /// <summary>
        /// Check if end date was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool EndDateUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.EndDate == UpdatedVolunteerInfo.EndDate;
        }


        /// <summary>
        /// Check if start date was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool StartDateUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.StartDate == UpdatedVolunteerInfo.StartDate;
        }


        /// <summary>
        /// Check if status was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool StatusUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.Status == UpdatedVolunteerInfo.Demographics.Status;
        }


        /// <summary>
        /// Check if family of military was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool FamilyOfMilitaryUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.FamilyOfMilitary == UpdatedVolunteerInfo.Demographics.FamilyOfMilitary;
        }


        /// <summary>
        /// Check if veteran status was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool VeteranUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.Veteran == UpdatedVolunteerInfo.Demographics.Veteran;
        }


        /// <summary>
        /// Check if racial group was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool RacialGroupUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.RacialGroup == _selectedRacialGroup.Name;
        }

        /// <summary>
        /// Check if ethnicity was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool EthnicityUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.Ethnicity == _selectedEthnicity.Name;
        }

        /// <summary>
        /// Check if identifiesNameId as was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool IdentifiesAsUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.IdentifiesAs == _selectedIdentifies.Name;
        }

        /// <summary>
        /// Check if genderNameId was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool GenderUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.Gender == _selectedGender.Name;
        }

        /// <summary>
        /// Check if birth date was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool DateOfBirthUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Demographics.DateOfBirth == UpdatedVolunteerInfo.Demographics.DateOfBirth;
        }

        /// <summary>
        /// Check if name was updated.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>True if changed. False if not.</returns>
        private bool NameUnchanged()
        {
            return _volunteerInfoViewModel.SelectedVolunteerInfo.Volunteer.FullName == UpdatedVolunteerInfo.Volunteer.FullName;
        }


        /// <summary>
        /// Set the initial value for initialReasonSeparatedIndex.
        /// </summary>
        /// <param name="statusName">Name to search for</param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void SetInitialInactiveStatus(string statusName)
        {
            var inactiveStatusTypeNameId = _inactiveStatuses.FirstOrDefault(status => status.Name == statusName);
            initialReasonSeparatedIndex = _inactiveStatuses.IndexOf(inactiveStatusTypeNameId);
        }

        /// <summary>
        /// Set the initial value for initialRacialGroupIndex.
        /// </summary>
        /// <param name="groupName">Name to search for</param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void SetInitialRacialGroup(string groupName)
        {
            var racialGroupNameId = _racialGroups.FirstOrDefault(racialGroup => racialGroup.Name == groupName);
            initialRacialGroupIndex = _racialGroups.IndexOf(racialGroupNameId);
        }

        /// <summary>
        /// Set the initial value for initialEthnicityIndex.
        /// </summary>
        /// <param name="ethnicityName">Name to search for</param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void SetInitialEthnicity(string ethnicityName)
        {
            var ethnicityNameId = _ethnicities.FirstOrDefault(ethnicity => ethnicity.Name == ethnicityName);
            initialEthnicityIndex = _ethnicities.IndexOf(ethnicityNameId);
        }

        /// <summary>
        /// Set the initial value for initialIdentifiesIndex.
        /// </summary>
        /// <param name="identifiesName">Name to search for</param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void SetInitialIdentifiesAs(string identifiesName)
        {
            var identifiesNameId = _identifies.FirstOrDefault(identifies => identifies.Name == identifiesName);
            initialIdentifiesIndex = _identifies.IndexOf(identifiesNameId);
        }

        /// <summary>
        /// Set the initial value for initialGenderIndex.
        /// </summary>
        /// <param name="genderName">Name to search for</param>
        /// <author>Tyler Moody</author>
        /// <created>03/28/2023</created>
        private void SetInitialGender(string genderName)
        {
            var genderNameId = _genderNames.FirstOrDefault(gender => gender.Name == genderName);
            initialGenderIndex = _genderNames.IndexOf(genderNameId);
        }
    }
}
