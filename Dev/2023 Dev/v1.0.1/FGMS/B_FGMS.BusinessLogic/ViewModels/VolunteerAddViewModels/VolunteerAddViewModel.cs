using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.ViewModels.VolunteerAddViewModels
{
    /// <FileName> VolunteerAddViewModel.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
    /// <DateCreated> 3/30/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 3/31/23 </LastModified>
    /// <LastModifiedBy> Isabelle Johns </LastModifiedBy>
    /// <summary>
    /// The Purpose of this file bind the Add Volunteer UI to backend.
    /// </summary>
    /// <author> Isabelle Johns </author>
    public class VolunteerAddViewModel: ViewModelBase
    {
        protected bool _formChanged;
        public bool FormChanged
        {
            get
            {
                return _formChanged;
            }
        }

        #region Properties
        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                _formChanged = true;
                OnPropertyChanged(nameof(FirstName));
                ValidateFirstName();
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                _formChanged = true;
                OnPropertyChanged(nameof(LastName));
                ValidateLastName();
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
                _formChanged = true;
                OnPropertyChanged(nameof(Address1));
                ValidateAddressLine1();
            }
        }

        private string _address2;
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
                _formChanged = true;
                OnPropertyChanged(nameof(Address2));
                ValidateAddressLine2();
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
                _formChanged = true;
                OnPropertyChanged(nameof(City));
                ValidateCity();
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

                if(_formChanged == false && _state != null)
                {
                    _formChanged = true;
                }

                _state = value;
                OnPropertyChanged(nameof(State));
                ValidateState();
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
                _formChanged = true;
                OnPropertyChanged(nameof(ZipCode));
                ValidateZipCode();
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
                _formChanged = true;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _dateOfBirth = DateTime.Today;
        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                _formChanged = true;
                OnPropertyChanged(nameof(DateOfBirth));
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
                _formChanged = true;
                OnPropertyChanged(nameof(GenderTuid));
                ValidateGenderSelection();
            }
        }

        private int? _identifiesAsTuid;
        public int? IdentifiesAsTuid
        {
            get
            {
                return _identifiesAsTuid;
            }
            set
            {
                _identifiesAsTuid = value;
                _formChanged = true;
                OnPropertyChanged(nameof(IdentifiesAsTuid));
                ValidateIdentifiesAsSelection();
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
                _formChanged = true;
                OnPropertyChanged(nameof(EthnicityTuid));
                ValidateEthnicitySelection();
            }
        }

        private int? _racialGroupTuid;
        public int? RacialGroupTuid
        {
            get
            {
                return _racialGroupTuid;
            }
            set
            {
                _racialGroupTuid = value;
                _formChanged = true;
                OnPropertyChanged(nameof(RacialGroupTuid));
                ValidateRacialGroupSelection();
            }
        }

        private bool _veteran;
        public bool Veteran
        {
            get
            {
                return _veteran;
            }
            set
            {
                _veteran = value;
                _formChanged = true;
                OnPropertyChanged(nameof(Veteran));
            }
        }

        private bool _familyOfMilitary;
        public bool FamilyOfMilitary
        {
            get
            {
                return _familyOfMilitary;
            }
            set
            {
                _familyOfMilitary = value;
                _formChanged = true;
                OnPropertyChanged(nameof(FamilyOfMilitary));
            }
        }

        private bool _filePhoto;
        public bool FilePhoto
        {
            get
            {
                return _filePhoto;
            }
            set
            {
                _filePhoto = value;
                _formChanged = true;
                OnPropertyChanged(nameof(FilePhoto));
            }
        }

        private bool _serviceDescription;
        public bool ServiceDescription
        {
            get
            {
                return _serviceDescription;
            }
            set
            {
                _serviceDescription = value;
                _formChanged = true;
                OnPropertyChanged(nameof(ServiceDescription));
            }
        }

        private bool _orientTraining;
        public bool OrientTraining
        {
            get
            {
                return _orientTraining;
            }
            set
            {
                _orientTraining = value;
                _formChanged = true;
                OnPropertyChanged(nameof(OrientTraining));
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
                _formChanged = true;
                OnPropertyChanged(nameof(ConfidenceSOU));
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
                _formChanged = true;
                OnPropertyChanged(nameof(ServiceStartDate));
            }
        }

        private bool _backgroundCheck;
        public bool BackgroundCheck
        {
            get
            {
                return _backgroundCheck;
            }
            set
            {
                _backgroundCheck = value;
                _formChanged = true;
                OnPropertyChanged(nameof(BackgroundCheck));
            }
        }

        private bool _idCopy;
        public bool IDCopy
        {
            get
            {
                return _idCopy;
            }
            set
            {
                _idCopy = value;
                _formChanged = true;
                OnPropertyChanged(nameof(IDCopy));
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
                _formChanged = true;
                OnPropertyChanged(nameof(AnnualIncomeCarInsurance));
            }
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates the First Name input. Checks for null
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateFirstName()
        {
            ClearErrors(nameof(FirstName));

            if(string.IsNullOrEmpty(FirstName))
            {
                AddError(nameof(FirstName), "First name is required");
                return;
            }

        }

        /// <summary>
        /// Validates the Last Name input. Checks for null
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateLastName()
        {
            ClearErrors(nameof(LastName));
            if(string.IsNullOrEmpty(LastName))
            {
                AddError(nameof(LastName), "Last name is required");
                return;
            }
        }

        /// <summary>
        /// Validates the Address Line 1 input. Checks for null
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
            if(string.IsNullOrEmpty(City))
            {
                AddError(nameof(City), "City is required");
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
            if(string.IsNullOrEmpty(State))
            {
                AddError(nameof(State), "State is required");
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
                AddError(nameof(ZipCode), "Zip code is required");
            }
            else
            {
                if (!Regex.IsMatch(ZipCode, @"^([\d])*"))
                {
                    AddError(nameof(ZipCode), "Zip code must numeric");
                }

                if (ZipCode.Length < 5)
                {
                    AddError(nameof(ZipCode), "Zip code must be 5 digits long");
                }
            }
        }

        /// <summary>
        /// Validates the Gender Selection. Checks for null value
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateGenderSelection()
        {
            ClearErrors(nameof(GenderTuid));
            if(!GenderTuid.HasValue)
            {
                AddError(nameof(GenderTuid), "Selection is required");
            }
        }

        /// <summary>
        /// Validates the Identifies as Selection. Checks for null value
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateIdentifiesAsSelection()
        {
            ClearErrors(nameof(IdentifiesAsTuid));
            if (!IdentifiesAsTuid.HasValue)
            {
                AddError(nameof(IdentifiesAsTuid), "Selection is required");
            }
        }


        /// <summary>
        /// Validates the Ethnicity Selection. Checks for null value
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateEthnicitySelection()
        {
            ClearErrors(nameof(EthnicityTuid));
            if (!EthnicityTuid.HasValue)
            {
                AddError(nameof(EthnicityTuid), "Selection is required");
            }
        }


        /// <summary>
        /// Validates the Racial Group Selection. Checks for null value
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void ValidateRacialGroupSelection()
        {
            ClearErrors(nameof(RacialGroupTuid));
            if (!RacialGroupTuid.HasValue)
            {
                AddError(nameof(RacialGroupTuid), "Selection is required");
            }
        }

        /// <summary>
        /// Calls all validation methods
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        public void ValidateAll()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateGenderSelection();
            ValidateIdentifiesAsSelection();
            ValidateEthnicitySelection();
            ValidateRacialGroupSelection();
            ValidateCity();
            ValidateAddressLine1();
            ValidateAddressLine2();
            ValidateState();
            ValidateZipCode();
        }

        #endregion
    }
}
