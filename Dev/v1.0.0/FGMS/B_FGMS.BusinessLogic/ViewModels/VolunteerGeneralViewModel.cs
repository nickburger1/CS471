using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace B_FGMS.BusinessLogic.ViewModels
{
    /// <FileName> VolunteerGeneralViewModel.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
    /// <DateCreated> 3/31/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 3/31/23 </LastModified>
    /// <LastModifiedBy> Isabelle Johns </LastModifiedBy>
    /// <summary>
    /// The Purpose of this file bind the volunteer general UI to backend.
    /// </summary>
    /// <author> Isabelle Johns </author>
    public class VolunteerGeneralViewModel: ViewModelBase
    {
        #region Properties

        private bool _isFormLoading = false;

        private bool errorFlag;

        protected bool _formChanged;
        public bool FormChanged
        {
            get
            {
                return _formChanged;
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

        private IEnumerable<int> _yearList;
        public IEnumerable<int> YearList
        {
            get
            {
                return _yearList;
            }
            set
            {
                _yearList = value;
                OnPropertyChanged(nameof(YearList));
            }
        }

        private int? _year = DateTime.Now.Year;
        public int? Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
                PopulateAnnualChecks();
            }
        }

        private IEnumerable<SchoolNameIdModel> _schoolList;
        public IEnumerable<SchoolNameIdModel> SchoolList
        {
            get
            {
                return _schoolList;
            }
            set
            {
                _schoolList = value;
                OnPropertyChanged(nameof(SchoolList));
            }
        }

        private int? _schoolTuid;
        public int? SchoolTuid
        {
            get
            {
                return _schoolTuid;
            }
            set
            {
                _schoolTuid = value;
                OnPropertyChanged(nameof(SchoolTuid));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private string? _schoolName;
        public string? SchoolName
        {
            get
            {
                return _schoolName;
            }
            set
            {
                _schoolName = value;
                OnPropertyChanged(nameof(SchoolName));
                if (!_isFormLoading)
                {
                    ValidateSchool();
                }
            }
        }

        private string? _phone;
        public string? Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
                if(!_isFormLoading)
                {
                    ValidatePhone();
                    _formChanged = true;
                }
                    
            }
        }

        private string _address1;
        public string Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
                OnPropertyChanged(nameof(Address1));
                if (!_isFormLoading)
                {
                    ValidateAddressLine1();
                    _formChanged = true;
                }
                    
            }
        }

        private string? _address2;
        public string? Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
                OnPropertyChanged(nameof(Address2));
                if (!_isFormLoading)
                {
                    ValidateAddressLine2();
                    _formChanged = true;
                }
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
                if(!_isFormLoading)
                {
                    ValidateCity();
                    _formChanged = true;
                }
                    
            }
        }

        private string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
                if(!_isFormLoading)
                {
                    ValidateState();
                    _formChanged = true;
                }
            }
        }

        private string _zipCode;
        public string ZipCode
        {
            get
            {
                return _zipCode;
            }
            set
            {
                _zipCode = value;
                OnPropertyChanged(nameof(ZipCode));
                if(!_isFormLoading)
                {
                    ValidateZipCode();
                    _formChanged = true;
                }
                    
            }
        }


        private string? _email;
        public string? Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                if(!_isFormLoading)
                {
                    ValidateEmail();
                    _formChanged = true;
                }
                    
            }
        }

        private string? _altPhone;
        public string? AltPhone
        {
            get
            {
                return _altPhone;
            }
            set
            {
                _altPhone = value;
                OnPropertyChanged(nameof(AltPhone));
                if (!_isFormLoading)
                {
                    ValidateAltPhone();
                    _formChanged = true;
                }
                    
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
                ToggleActivity();
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
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

        private DateTime _startDate = DateTime.Now.Date;
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
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private bool _filePhoto = false;
        public bool FilePhoto
        {
            get
            {
                return _filePhoto;
            }
            set
            {
                _filePhoto = value;
                OnPropertyChanged(nameof(FilePhoto));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private bool _serviceDescription = false;
        public bool ServiceDescription
        {
            get
            {
                return _serviceDescription;
            }
            set
            {
                _serviceDescription = value;
                OnPropertyChanged(nameof(ServiceDescription));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private bool _orientTraining = false;
        public bool OrientTraining
        {
            get
            {
                return _orientTraining;
            }
            set
            {
                _orientTraining = value;
                OnPropertyChanged(nameof(OrientTraining));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _confidenceSOU;
        public DateTime? ConfidenceSOU
        {
            get
            {
                return _confidenceSOU;
            }
            set
            {
                _confidenceSOU = value;
                OnPropertyChanged(nameof(ConfidenceSOU));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _serviceStartDate;
        public DateTime? ServiceStartDate
        {
            get
            {
                return _serviceStartDate;
            }
            set
            {
                _serviceStartDate = value;
                OnPropertyChanged(nameof(ServiceStartDate));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private bool _nschc = false;
        public bool NSCHCCheckForm
        {
            get
            {
                return _nschc;
            }
            set
            {
                _nschc = value;
                OnPropertyChanged(nameof(NSCHCCheckForm));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private bool _backgroundCheck = false;
        public bool BackgroundCheck
        {
            get
            {
                return _backgroundCheck;
            }
            set
            {
                _backgroundCheck = value;
                OnPropertyChanged(nameof(BackgroundCheck));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private bool _idCopy = false;
        public bool IDCopy
        {
            get
            {
                return _idCopy;
            }
            set
            {
                _idCopy = value;
                OnPropertyChanged(nameof(IDCopy));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _nsopw;
        public DateTime? NSOPW
        {
            get
            {
                return _nsopw;
            }
            set
            {
                _nsopw = value;
                OnPropertyChanged(nameof(NSOPW));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _iChat;
        public DateTime? IChat
        {
            get
            {
                return _iChat;
            }
            set
            {
                _iChat = value;
                OnPropertyChanged(nameof(IChat));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _trueScreen;
        public DateTime? TrueScreen
        {
            get
            {
                return _trueScreen;
            }
            set
            {
                _trueScreen = value;
                OnPropertyChanged(nameof(TrueScreen));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _aliasFieldPrint;
        public DateTime? AliasFieldPrint
        {
            get
            {
                return _aliasFieldPrint;
            }
            set
            {
                _aliasFieldPrint = value;
                OnPropertyChanged(nameof(AliasFieldPrint));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _fieldPrint;
        public DateTime? FieldPrint
        {
            get
            {
                return _fieldPrint;
            }
            set
            {
                _fieldPrint = value;
                OnPropertyChanged(nameof(FieldPrint));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _dhs;
        public DateTime? DHS
        {
            get
            {
                return _dhs;
            }
            set
            {
                _dhs = value;
                OnPropertyChanged(nameof(DHS));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _tbShot;
        public DateTime? TBShot
        {
            get
            {
                return _tbShot;
            }
            set
            {
                _tbShot = value;
                OnPropertyChanged(nameof(TBShot));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _schedulePhotoRelease;
        public DateTime? SchedulePhotoRelease
        {
            get
            {
                return _schedulePhotoRelease;
            }
            set
            {
                _schedulePhotoRelease = value;
                OnPropertyChanged(nameof(SchedulePhotoRelease));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _emergencyBeneficiaryForm;
        public DateTime? EmergencyBeneficiary
        {
            get
            {
                return _emergencyBeneficiaryForm;
            }
            set
            {
                _emergencyBeneficiaryForm = value;
                OnPropertyChanged(nameof(EmergencyBeneficiary));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _hippaRelease;
        public DateTime? HippaRelease
        {
            get
            {
                return _hippaRelease;
            }
            set
            {
                _hippaRelease = value;
                OnPropertyChanged(nameof(HippaRelease));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _physical;
        public DateTime? Physical
        {
            get
            {
                return _physical;
            }
            set
            {
                _physical = value;
                OnPropertyChanged(nameof(Physical));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private DateTime? _annualIncomeCarInsurance;
        public DateTime? AnnualIncomeCarInsurance
        {
            get
            {
                return _annualIncomeCarInsurance;
            }
            set
            {
                _annualIncomeCarInsurance = value;
                OnPropertyChanged(nameof(AnnualIncomeCarInsurance));
                if (!_isFormLoading)
                {
                    _formChanged = true;
                }
            }
        }

        private readonly IVolunteerProvider _volunteerProvider;
        private readonly ISchoolProvider _schoolProvider;
        #endregion

        public VolunteerGeneralViewModel(IVolunteerProvider volunteerProvider, ISchoolProvider schoolProvider)
        {
            _volunteerProvider = volunteerProvider;
            _schoolProvider = schoolProvider;

            errorFlag = false;
            _schoolList = _schoolProvider.GetSchoolNameAndId();
                   if (errorFlag) { errorFlag = false; return; }

            _yearList = _volunteerProvider.GetAnnualCheckYears();
            if (errorFlag) { errorFlag = false; return; }
            if (!YearList.Contains(DateTime.Now.Year))
            {
                YearList = YearList.Append(DateTime.Now.Year);
            }

            _volunteerList = _volunteerProvider.GetVolunteerNameAndId();
            if (errorFlag) { errorFlag = false; return; }
        }

        #region Validation
        /// <summary>
        /// Validates the School Selection. Checks if selection exists in the list
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/9/23 </created>
        private void ValidateSchool()
        {
            ClearErrors(nameof(SchoolTuid));

            if(!string.IsNullOrEmpty(SchoolName) && SchoolTuid == null)
            {
                AddError(nameof(SchoolTuid), "Select school from list.");
            }
        }

        /// <summary>
        /// Validates the Phone input. The Phone is autoformatted and only checks
        /// for valid characters
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidatePhone()
        {
            ClearErrors(nameof(Phone));

            string strPhone = string.Empty;

            if(!string.IsNullOrEmpty(Phone))
            {
                strPhone = new string(Phone.Where(Char.IsDigit).ToArray());

                if (!Regex.IsMatch(Phone, @"^[\d\s\-\(\)]*$"))
                {
                    AddError(nameof(Phone), "Invalid phone format.");
                }

                if (strPhone.Length < 7)
                {
                    AddError(nameof(Phone), "Must be 7 digits long.");
                }

                if(strPhone.Length > 7 && strPhone.Length < 10)
                {
                    AddError(nameof(Phone), "Must be 10 digits long.");
                }

            }    
        }

        /// <summary>
        /// Validates the Address Line 1 input. Checks for null and special characters
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateAddressLine1()
        {
            ClearErrors(nameof(Address1));

            if (string.IsNullOrEmpty(Address1))
            {
                AddError(nameof(Address1), "Address Line 1 is required.");
            }
            else if (!Regex.IsMatch(Address1, @"^[#.0-9a-zA-Z\s,-]+$"))
            {
                AddError(nameof(Address1), "Invalid address format.");
            }
        }

        /// <summary>
        /// Validates the Address Line 2 input. Checks for special characters
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/9/23 </created>
        private void ValidateAddressLine2()
        {
            ClearErrors(nameof(Address2));

            if (!string.IsNullOrEmpty(Address2) && !Regex.IsMatch(Address2, @"^[#.0-9a-zA-Z\s,-]+$"))
            {
                AddError(nameof(Address2), "Invalid address format.");
            }
        }

        /// <summary>
        /// Validates the City input. Checks for null
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateCity()
        {
            ClearErrors(nameof(City));

            if (string.IsNullOrEmpty(City))
            {
                AddError(nameof(City), "City is requred.");
            }
        }

        /// <summary>
        /// Validates the State input. Checks for null
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateState()
        {
            ClearErrors(nameof(State));

            if (string.IsNullOrEmpty(State))
            {
                AddError(nameof(State), "State is required.");
            }
        }

        /// <summary>
        /// Validates the ZipCode input. Checks for null and strings less than 5 digits
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateZipCode()
        {
            ClearErrors(nameof(ZipCode));

            if (string.IsNullOrEmpty(ZipCode))
            {
                AddError(nameof(ZipCode), "Zip code is required.");
            }
            else
            {
                if (!Regex.IsMatch(ZipCode, @"^([\d])*"))
                {
                    AddError(nameof(ZipCode), "Zip code must numeric.");
                }

                if (ZipCode.Length != 5)
                {
                    AddError(nameof(ZipCode), "Must be 5 digits long.");
                }
            }
        }

        /// <summary>
        /// Validates the email input. Checks for valid email format
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateEmail()
        {
            ClearErrors(nameof(Email));

            //if (!new EmailAddressAttribute().IsValid(email))
            //{
            //    AddError(nameof(Email), "Invalid email format.");
            //}

            if (!string.IsNullOrEmpty(Email))
            {
                if (!Regex.IsMatch(Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    AddError(nameof(Email), "Invalid email format.");
                }
            }


        }

        /// <summary>
        /// Validates the AltPhone input. The AltPhone is autoformatted and only checks
        /// for valid characters
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateAltPhone()
        {
            ClearErrors(nameof(AltPhone));

            string strPhone = string.Empty;

            if (!string.IsNullOrEmpty(AltPhone))
            {
                strPhone = new string(AltPhone.Where(Char.IsDigit).ToArray());

                if (!Regex.IsMatch(AltPhone, @"^[\d\s\-\(\)]*$"))
                {
                    AddError(nameof(AltPhone), "Invalid phone format.");
                }

                if (strPhone.Length < 7)
                {
                    AddError(nameof(AltPhone), "Must be 7 digits long.");
                }

                if (strPhone.Length > 7 && strPhone.Length < 10)
                {
                    AddError(nameof(AltPhone), "Must be 10 digits long.");
                }

            }
        }

        #endregion

        #region DB Interaction

        /// <summary>
        /// Gets the current Volunteer's information stored on the general page
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        public void PopulateVolunteerInfo()
        {
            if(VolunteerTuid != null)
            {
                _isFormLoading = true;

                VolunteerGeneralModel? volunteerGeneralInfo = _volunteerProvider.GetVolunteerGeneralInfo((int)VolunteerTuid);
                if (errorFlag) { errorFlag = false; return; }
                OneTimeChecksModel? volunteerOneTimeChecks = _volunteerProvider.GetVolunteerOneTimeChecks((int)VolunteerTuid);
                if (errorFlag) { errorFlag = false; return; }
                PopulateAnnualChecks();
                

                if (volunteerGeneralInfo != null)
                {
                    SchoolTuid = volunteerGeneralInfo.SchoolTuid;
                    Phone = volunteerGeneralInfo.Phone;
                    Address1 = volunteerGeneralInfo.Address1!;
                    Address2 = volunteerGeneralInfo.Address2;
                    City = volunteerGeneralInfo.City!;
                    State = volunteerGeneralInfo.State!;
                    ZipCode = volunteerGeneralInfo.ZipCode!;
                    Email = volunteerGeneralInfo.Email;
                    AltPhone = volunteerGeneralInfo.AltPhone;
                    IsActive = (bool)volunteerGeneralInfo.IsActive!;
                    StartDate = (DateTime)volunteerGeneralInfo.StartDate!;
                    EndDate = volunteerGeneralInfo.EndDate;
                }

                if(volunteerOneTimeChecks !=null)
                {
                    FilePhoto = volunteerOneTimeChecks.FilePhoto;
                    ServiceDescription = volunteerOneTimeChecks.ServiceDescription;
                    OrientTraining = volunteerOneTimeChecks.OrientTraining;
                    ConfidenceSOU = volunteerOneTimeChecks.ConfidenceSOU;
                    ServiceStartDate = volunteerOneTimeChecks.ServiceStartDate;
                    NSCHCCheckForm = volunteerOneTimeChecks.NSCHCCheckForm;
                    BackgroundCheck = volunteerOneTimeChecks.BackgroundCheck;
                    IDCopy = volunteerOneTimeChecks.IDCopy;
                    NSOPW = volunteerOneTimeChecks.NSOPW;
                    IChat = volunteerOneTimeChecks.IChat;
                    TrueScreen = volunteerOneTimeChecks.TrueScreen;
                    AliasFieldPrint = volunteerOneTimeChecks.AliasFingerPrint;
                    FieldPrint = volunteerOneTimeChecks.FieldPrintCleared;
                    DHS = volunteerOneTimeChecks.DHS;
                    TBShot = volunteerOneTimeChecks.TBShot;
                }

                ClearAllErrors();
                _formChanged = false;
                _isFormLoading = false;
            }
        }

        /// <summary>
        /// Gets the current Volunteer's Annual Checks during the selected year
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        public void PopulateAnnualChecks()
        {
            if(Year != null && VolunteerTuid!= null)
            {
                AnnualChecksModel? volunteerAnnualChecks = _volunteerProvider.GetVolunteerAnnualChecks((int)VolunteerTuid, (int)Year);
                if (errorFlag) { errorFlag = false; return; }

                if (volunteerAnnualChecks != null)
                {
                    SchedulePhotoRelease = volunteerAnnualChecks.SchedulePhotoRelease;
                    EmergencyBeneficiary = volunteerAnnualChecks.EmergancyBeneficiaryForm;
                    HippaRelease = volunteerAnnualChecks.HippaRelease;
                    Physical = volunteerAnnualChecks.Physical;
                    AnnualIncomeCarInsurance = volunteerAnnualChecks.AnnualIncomeCarInsurance;
                }
                else
                {
                    SchedulePhotoRelease = null;
                    EmergencyBeneficiary = null;
                    HippaRelease = null;
                    Physical = null;
                    AnnualIncomeCarInsurance = null;
                }
            }
        }

        #endregion

        #region UI Interaction

        /// <summary>
        /// Handles Activty Toggle being switched. If Activity is false,
        /// the EndDate will auto-populate to Today's date.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ToggleActivity()
        {
            if (IsActive)
            {
                Active = "Yes";
                if (EndDate.HasValue)
                {
                    EndDate = null;
                }
            }
            else
            {
                Active = "No";
                if (!EndDate.HasValue)
                {
                    EndDate = DateTime.Now.Date;
                }
            }
        }

        #endregion
    }
}
