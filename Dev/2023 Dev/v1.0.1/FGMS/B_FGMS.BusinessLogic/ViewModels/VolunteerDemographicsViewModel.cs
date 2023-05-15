using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace B_FGMS.BusinessLogic.ViewModels
{
    /// <FileName> VolunteerDemographicsViewModel.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
    /// <DateCreated> 4/11/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 4/12/23 </LastModified>
    /// <LastModifiedBy> Isabelle Johns </LastModifiedBy>
    /// <summary>
    /// The Purpose of this file bind the volunteer demographics UI to backend.
    /// </summary>
    /// <author> Isabelle Johns </author>
    public class VolunteerDemographicsViewModel : ViewModelBase
    {
        #region Properties

        private int? _volunteerTuid;
        public int? VolunteerTuid
        {
            get
            {
                return _volunteerTuid;
            }
            set
            {
                _volunteerTuid = value;
                OnPropertyChanged(nameof(VolunteerTuid));
                PopulateVolunteerInfo();
            }
        }

        private VolunteerNameIdModel _selectedVolunteer;
        public VolunteerNameIdModel SelectedVolunteer
        {
            get
            {
                return _selectedVolunteer;
            }
            set
            {
                _selectedVolunteer = value;
                OnPropertyChanged(nameof(SelectedVolunteer));
            }
        }

        private string? _firstName;
        public string? FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private string? _lastName;
        public string? LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime _lastUpdated;
        public DateTime LastUpdated
        {
            get
            {
                return _lastUpdated;
            }
            set
            {
                _lastUpdated = value;
                OnPropertyChanged(nameof(LastUpdated));
            }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private int? _genderTuid;
        public int? GenderTuid
        {
            get
            {
                return _genderTuid;
            }
            set
            {
                _genderTuid = value;
                OnPropertyChanged(nameof(GenderTuid));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                    ValidateGender();
                }
                
            }
        }

        private string _genderName;
        public string GenderName
        {
            get
            {
                return _genderName;
            }
            set
            {
                _genderName = value;
                OnPropertyChanged(nameof(GenderName));
            }
        }

        private int? _indentityTuid;
        public int? IdentityTuid
        {
            get
            {
                return _indentityTuid;
            }
            set
            {
                _indentityTuid = value;
                OnPropertyChanged(nameof(IdentityTuid));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                    ValidateIdentity();
                }
                
            }
        }

        private string _indentityName;
        public string IdentityName
        {
            get
            {
                return _indentityName;
            }
            set
            {
                _indentityName = value;
                OnPropertyChanged(nameof(IdentityName));
            }
        }

        private int? _ethnicityTuid;
        public int? EthnicityTuid
        {
            get
            {
                return _ethnicityTuid;
            }
            set
            {
                _ethnicityTuid = value;
                OnPropertyChanged(nameof(EthnicityTuid));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                    ValidateEthnicity();
                }
                
            }
        }

        private string _ethnicityName;
        public string EthnicityName
        {
            get
            {
                return _ethnicityName;
            }
            set
            {
                _ethnicityName = value;
                OnPropertyChanged(nameof(EthnicityName));
            }
        }

        private int? _racialTuid;
        public int? RacialTuid
        {
            get
            {
                return _racialTuid;
            }
            set
            {
                _racialTuid = value;
                OnPropertyChanged(nameof(RacialTuid));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                    ValidateRacialGroup();
                }
                
            }
        }

        private string _racialName;
        public string RacialName
        {
            get
            {
                return _racialName;
            }
            set
            {
                _racialName = value;
                OnPropertyChanged(nameof(RacialName));
            }
        }

        private bool _isVeteran;
        public bool IsVeteran
        {
            get
            {
                return _isVeteran;
            }
            set
            {
                _isVeteran = value;
                OnPropertyChanged(nameof(IsVeteran));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }

                ToggleVeteran();
            }
        }

        private string _veteran;
        public string Veteran
        {
            get
            {
                return _veteran;
            }
            set
            {
                _veteran = value;
                OnPropertyChanged(nameof(Veteran));
            }
        }

        private bool _isFamilyOfMilitary;
        public bool IsFamilyOfMilitary
        {
            get
            {
                return _isFamilyOfMilitary;
            }
            set
            {
                _isFamilyOfMilitary = value;
                OnPropertyChanged(nameof(IsFamilyOfMilitary));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }

                ToggleMilitary();
            }
        }

        private string _familyOfMilitary;
        public string FamilyOfMilitary
        {
            get
            {
                return _familyOfMilitary;
            }
            set
            {
                _familyOfMilitary = value;
                OnPropertyChanged(nameof(FamilyOfMilitary));
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }

                ToggleActivity();
            }
        }

        private string _active;
        public string Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }

        private DateTime? _separationDate;
        public DateTime? SeparationDate
        {
            get
            {
                return _separationDate;
            }
            set
            {
                _separationDate = value;
                OnPropertyChanged(nameof(SeparationDate));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private int? _separationTuid;
        public int? SeparationTuid
        {
            get
            {
                return _separationTuid;
            }
            set
            {
                _separationTuid = value;
                OnPropertyChanged(nameof(SeparationTuid));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                    ValidateSeparationReason();
                }
                
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private string? _reasonSeparated;
        public string? ReasonSeparated
        {
            get
            {
                return _reasonSeparated;
            }
            set
            {
                _reasonSeparated = value;
                OnPropertyChanged(nameof(ReasonSeparated));
            }
        }

        private IEnumerable<VolunteerNameIdModel> _volunteerList;
        public IEnumerable<VolunteerNameIdModel> VolunteerList
        {
            get
            {
                return _volunteerList;
            }
            set
            {
                _volunteerList = value;
                OnPropertyChanged(nameof(VolunteerList));
            }
        }

        private IEnumerable<GenderNameIdModel> _genderList;
        public IEnumerable<GenderNameIdModel> GenderList
        {
            get
            {
                return _genderList;
            }
            set
            {
                _genderList = value;
                OnPropertyChanged(nameof(GenderList));
            }
        }

        private IEnumerable<EthnicityNameIdModel> _ethnicityList;
        public IEnumerable<EthnicityNameIdModel> EthnicityList
        {
            get
            {
                return _ethnicityList;
            }
            set
            {
                _ethnicityList = value;
                OnPropertyChanged(nameof(EthnicityList));
            }
        }

        private IEnumerable<RacialGroupNameIdModel> _racialList;
        public IEnumerable<RacialGroupNameIdModel> RacialList
        {
            get
            {
                return _racialList;
            }
            set
            {
                _racialList = value;
                OnPropertyChanged(nameof(RacialList));
            }
        }

        private IEnumerable<IdentifiesAsNameIdModel> _identityList;
        public IEnumerable<IdentifiesAsNameIdModel> IdentityList
        {
            get
            {
                return _identityList;
            }
            set
            {
                _identityList = value;
                OnPropertyChanged(nameof(IdentityList));
            }
        }

        private IEnumerable<ReasonsSeparatedNameIdModel> _reasonsList;
        public IEnumerable<ReasonsSeparatedNameIdModel> ReasonsList
        {
            get
            {
                return _reasonsList;
            }
            set
            {
                _reasonsList = value;
                OnPropertyChanged(nameof(ReasonsList));
            }
        }

        protected bool _formChanged;
        public bool FormChanged
        {
            get
            {
                return _formChanged;
            }
        }

        #endregion

        private readonly IVolunteerProvider _volunteerProvider;
        private bool _isFormLoading = false;
        private bool errorFlag;

        public VolunteerDemographicsViewModel(IVolunteerProvider volunteerProvider)
        {
            _volunteerProvider = volunteerProvider;

            //_volunteerProvider.DatabaseError += ErrorHandler;

            //Set combobox lists
            _volunteerList = _volunteerProvider.GetVolunteerNameAndId();
            if (errorFlag) { errorFlag = false; return; }
            _genderList = _volunteerProvider.GetGenderNameAndId(false);
            if (errorFlag) { errorFlag = false; return; }
            _ethnicityList = _volunteerProvider.GetEthnityNameAndId(false);
            if (errorFlag) { errorFlag = false; return; }
            _racialList = _volunteerProvider.GetRacialGroupNameAndId(false);
            if (errorFlag) { errorFlag = false; return; }
            _identityList = _volunteerProvider.GetIdentifiesAsNameAndId(false);
            if (errorFlag) { errorFlag = false; return; }
            _reasonsList = _volunteerProvider.GetReasonsSeparatedNameAndId(false);
            if (errorFlag) { errorFlag = false; return; }
        }

        #region Error Event
        /// <summary>
        /// Error provider for the UserServiceProvider. All functionality to handle
        /// business logic errors for the Users.xaml page are called in this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void ErrorHandler(object sender, Events.ErrorEventArgs e)
        {
            errorFlag = true;
            System.Windows.MessageBox.Show(e.ErrorMessage, "Database Error " + e.ErrorCode, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates the Gender Selection. Enforces a Selection to be made.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ValidateGender()
        {
            ClearErrors(nameof(GenderTuid));

            if (!GenderTuid.HasValue)
            {
                AddError(nameof(GenderTuid), "Selection Required");
            }
        }

        /// <summary>
        /// Validates the Identity Selection. Enforces a Selection to be made.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ValidateIdentity()
        {
            ClearErrors(nameof(IdentityTuid));

            if (!IdentityTuid.HasValue)
            {
                AddError(nameof(IdentityTuid), "Selection Required");
            }
        }

        /// <summary>
        /// Validates the Ethnicity Selection. Enforces a Selection to be made.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ValidateEthnicity()
        {
            ClearErrors(nameof(EthnicityTuid));

            if (!EthnicityTuid.HasValue)
            {
                AddError(nameof(EthnicityTuid), "Selection Required");
            }
        }

        /// <summary>
        /// Validates the Racial Group Selection. Enforces a Selection to be made.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ValidateRacialGroup()
        {
            ClearErrors(nameof(RacialTuid));

            if (!RacialTuid.HasValue)
            {
                AddError(nameof(RacialTuid), "Selection Required");
            }
        }

        /// <summary>
        /// Validates the Reason Separated Selection. Enforces a Selection to be made if
        /// the volunteer is not active.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ValidateSeparationReason()
        {
            ClearErrors(nameof(SeparationTuid));

            if (!IsActive && !SeparationTuid.HasValue)
            {
                AddError(nameof(SeparationTuid), "Selection Required");
            }
        }

        /// <summary>
        /// Calls all validation methods
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/14/23 </created>
        public void ValidateAll()
        {
            ValidateEthnicity();
            ValidateGender();
            ValidateIdentity();
            ValidateRacialGroup();
            ValidateSeparationReason();
        }

        #endregion

        #region Database Interaction

        /// <summary>
        /// Assigns all info from database call to properties on the Demographics page
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        public void PopulateVolunteerInfo()
        {
            if(VolunteerTuid != null)
            {
                _isFormLoading = true;

                VolunteerDemographicsModel? volunteerDemographics = _volunteerProvider.GetVolunteerDemographicsInfo((int)VolunteerTuid);
                if (errorFlag) { errorFlag = false; return; }

                if (volunteerDemographics != null)
                {
                    LastUpdated = volunteerDemographics.LastUpdated;
                    DateOfBirth = volunteerDemographics.DateOfBirth;
                    GenderTuid = volunteerDemographics.GenderTuid;
                    Age = volunteerDemographics.Age;
                    IdentityTuid = volunteerDemographics.IdentifiesAsTuid;
                    EthnicityTuid = volunteerDemographics.EthnicityTuid;
                    RacialTuid = volunteerDemographics.RacialGroupTuid;
                    IsVeteran = volunteerDemographics.IsVeteran;
                    IsFamilyOfMilitary = volunteerDemographics.IsFamilyOfMilitary;
                    IsActive = volunteerDemographics.IsActive;
                    SeparationDate = volunteerDemographics.SeparationDate;
                    StartDate = volunteerDemographics.StartDate;

                    if(!IsActive)
                    {
                        SeparationTuid = volunteerDemographics.ReasonSeparatedTuid;
                    }
                }

                ClearAllErrors();
                _formChanged = false;
                _isFormLoading = false;
            }
        }

        /// <summary>
        /// Assigns first and last name from selected item to Properties
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        public void PopulateName()
        {
            _isFormLoading = true;

            FirstName = SelectedVolunteer.FirstName;
            LastName = SelectedVolunteer.LastName;

            _isFormLoading = false;
        }


        #endregion

        #region UI Interaction

        /// <summary>
        /// Changes the UI textbox Text associated with Veteran
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ToggleVeteran()
        {
            if (IsVeteran)
            {
                Veteran = "Yes";
            }
            else
            {
                Veteran = "No";
            }
        }

        /// <summary>
        /// Changes the UI textbox Text associated with Family of Military
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ToggleMilitary()
        {
            if (IsFamilyOfMilitary)
            {
                FamilyOfMilitary = "Yes";
            }
            else
            {
                FamilyOfMilitary = "No";
            }
        }

        /// <summary>
        /// Changes the UI textbox Text associated with Activity
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void ToggleActivity()
        {
            if (IsActive)
            {
                Active = "Yes";
                if (SeparationDate.HasValue)
                {
                    SeparationDate = null;
                }
            }
            else
            {
                Active = "No";
                if (!SeparationDate.HasValue)
                {
                    SeparationDate = DateTime.Now.Date;
                }
            }
        }

        #endregion
    }
}
