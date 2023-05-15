using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Reports;
using B_FGMS.BusinessLogic.Models.Volunteer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.ReportProviders
{
    /// <summary>
    /// This class is used to provide database interactions for the report page
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>3/23/2023</created>
    public class DatabaseReportProvider : IReportProvider
    {
        private readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        /// <summary>
        /// This constructor provides the database context
        /// </summary>
        /// <param name="dbContext"></param>
        public DatabaseReportProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// When called with invoke an event to inform user of an error
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void OnDatabaseError(string errorMessage, string errorCode)
        {
            DatabaseError?.Invoke(this, new Events.ErrorEventArgs(errorMessage, errorCode));
        }


        /// <summary>
        /// This method returns a List of a given volunteers annual check items
        /// </summary>
        /// <param name="intVolunteerTuid">the tuid of the volunteer we want annual checks for</param>
        /// <returns>a List of Report Annual Check models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<ReportAnnualCheckModel>? GetAnnualCheckForVolunteer(int intVolunteerTuid)
        {
            try
            {
                var annualChecks = _dbContext.AnnualChecks.Where(x => x.VolunteerTuid == intVolunteerTuid);
                return annualChecks.Select(x => new ReportAnnualCheckModel
                {
                    SchedulePhotoRelease = x.PhotoReleaseDate,
                    EmergancyBeneficiaryForm = x.EmergencyBeneficiaryDate,
                    HippaRelease = x.HippaReleaseDate,
                    Physical = x.PhysicalDate,
                    AnnualIncomeCarInsurance = x.CarInsuranceDate,
                    VolunteerFullName = x.Volunteer == null ? "null" : x.Volunteer.FullName ?? "null", //this covers the null case of a volunteer virtual and falls back to "null"
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1100._message, ErrorMessages._1100._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1101._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1101._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1102._message + e.Message, ErrorMessages._1102._code);
            }

            return null;
        }

        /// <summary>
        /// This method gets the annual checks from the database for all volunteers
        /// </summary>
        /// <returns>a list of report annual check models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<ReportAnnualCheckModel>? GetAnnualChecksAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            IEnumerable<AnnualCheck> annualChecks = new List<AnnualCheck>();

            try
            {
                annualChecks = _dbContext.AnnualChecks.Where(x => x.Volunteer == null ? false : x.Volunteer.IsDeleted == false);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1103._message, ErrorMessages._1103._code);
                    return new List<ReportAnnualCheckModel>();
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1104._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1104._code);
                    return new List<ReportAnnualCheckModel>();
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1105._message + e.Message, ErrorMessages._1105._code);
                return new List<ReportAnnualCheckModel>();
            }

            if (blnActive)
            {
                annualChecks = annualChecks.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == true);
            }
            if (blnInactive)
            {
                annualChecks = annualChecks.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == false);
            }
            if (blnCurrent)
            {
                annualChecks = annualChecks.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate == null);
            }
            if (blnFormer)
            {
                //here on null volunteer fallback to true, since this is for former volunteers
                annualChecks = annualChecks.Where(x => x.Volunteer == null ? true : x.Volunteer.SeparatedDate != null);
            }
            return annualChecks
                .Select(x => new ReportAnnualCheckModel
                {
                    SchedulePhotoRelease = x.PhotoReleaseDate,
                    EmergancyBeneficiaryForm = x.EmergencyBeneficiaryDate,
                    HippaRelease = x.HippaReleaseDate,
                    Physical = x.PhysicalDate,
                    AnnualIncomeCarInsurance = x.CarInsuranceDate,
                    VolunteerFullName = x.Volunteer == null ? "null" : x.Volunteer.FullName ?? "null", //this covers the null case of a volunteer virtual and falls back to "null"
                }).ToList();
        }

        /// <summary>
        /// This method will get the expenses for the provided expense type that fall in the provided date range
        /// </summary>
        /// <param name="startDate">the start date of our date range</param>
        /// <param name="endDate">the end date of our date range</param>
        /// <param name="strExpenseType">the expense type we are looking</param>
        /// <returns>a list of ExpenseEntries</returns>
        /// <created>3/25/2023</created>
        public List<ExpenseEntryModel>? GetExpensesForReport(DateTime? startDate, DateTime? endDate, string strExpenseType, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            if (startDate == null || endDate == null || string.IsNullOrEmpty(strExpenseType))
            {
                return null;
            }
            //first get the expense type tuid for the given expense type
            ExpenseTypeItem? expenseTypeItem = null;

            try
            {
                expenseTypeItem = _dbContext.ExpenseTypeItems.Where(x => x.Name.Equals(strExpenseType)).FirstOrDefault();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1106._message, ErrorMessages._1106._code);
                    return new List<ExpenseEntryModel>();
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1107._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1107._code);
                    return new List<ExpenseEntryModel>();
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1108._message + e.Message, ErrorMessages._1108._code);
                return new List<ExpenseEntryModel>();
            }



            if (expenseTypeItem != null)
            {
                IEnumerable<InKindExpense> expenses = Enumerable.Empty<InKindExpense>();
                try
                {
                    expenses = _dbContext.InKindExpenses.Where(x => x.Volunteer == null ? false : x.Volunteer.IsDeleted == false).Where(x => x.ExpenseTypeTuid == expenseTypeItem.Tuid)
                        .Where(x => x.Date >= startDate && x.Date <= endDate);
                }
                catch (SqlException e)
                {
                    if (e.ErrorCode == -2146232060)
                    {
                        OnDatabaseError(ErrorMessages._1106._message, ErrorMessages._1106._code);
                        return new List<ExpenseEntryModel>();
                    }
                    else
                    {
                        OnDatabaseError(ErrorMessages._1107._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1107._code);
                        return new List<ExpenseEntryModel>();
                    }
                }
                catch (Exception e)
                {
                    OnDatabaseError(ErrorMessages._1108._message + e.Message, ErrorMessages._1108._code);
                    return new List<ExpenseEntryModel>();
                }


                if (blnActive)
                {
                    expenses = expenses.Where(x => x.Volunteer?.IsActive ?? false);
                }
                if (blnInactive)
                {
                    expenses = expenses.Where(x => !x.Volunteer?.IsActive ?? false);
                }
                if (blnCurrent)
                {
                    expenses = expenses.Where(x => x.Volunteer?.SeparatedDate == null); //here I fallback to true because if the volunteer was null then the volunteer was probably deleted
                }
                if (blnFormer)
                {
                    expenses = expenses.Where(x => x.Volunteer?.SeparatedDate != null);
                }
                return expenses.Select(x => new ExpenseEntryModel
                {
                    DateOf = x.Date,
                    ExpenseType = new ExpenseTypeModel { Name = expenseTypeItem.Name, Tuid = expenseTypeItem.Tuid },
                    Value = x.Value,
                    Volunteer = new VolunteerModel { FirstName = x.Volunteer?.FirstName ?? "null", LastName = x.Volunteer?.LastName ?? "null" }
                }).ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method reutrns entries of the epxense provided for the provided volunteer in the provided date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">the end date</param>
        /// <param name="strExpenseType">the expense type</param>
        /// <param name="intVolunteerTuid">the volunteer tuid for the volunteer we want</param>
        /// <returns>A list of expense entry models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<ExpenseEntryModel>? GetExpensesForReportForVolunteer(DateTime? startDate, DateTime? endDate, string strExpenseType, int intVolunteerTuid)
        {
            if (startDate == null || endDate == null || string.IsNullOrEmpty(strExpenseType))
            {
                return null;
            }

            try
            {
                //first get the expense type tuid for the given expense type
                int intExpenseTypeTuid = _dbContext.ExpenseTypeItems.Where(x => x.Name.Equals(strExpenseType)).Select(x => x.Tuid).FirstOrDefault();
                IEnumerable<ExpenseEntryModel> expenses = _dbContext.InKindExpenses.Where(x => x.ExpenseTypeTuid == intExpenseTypeTuid && x.VolunteerTuid == intVolunteerTuid)
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .Select(x => new ExpenseEntryModel
                    {
                        DateOf = x.Date,
                        ExpenseType = new ExpenseTypeModel { Name = x.ExpenseTypeItem == null ? "null" : x.ExpenseTypeItem.Name ?? "null", Tuid = x.ExpenseTypeTuid },
                        Value = x.Value,
                        Volunteer = new VolunteerModel { FirstName = x.Volunteer == null ? "Null" : x.Volunteer.FirstName ?? "null", LastName = x.Volunteer == null ? "null" : x.Volunteer.LastName ?? "null" }
                    });

                expenses = expenses.OrderBy(x => x.DateOf);
                return expenses.ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1109._message, ErrorMessages._1109._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1110._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1110._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1111._message + e.Message, ErrorMessages._1111._code);
            }

            return null;
        }

        /// <summary>
        /// This method will return the reason separated for a volunteer, or null if no separated reason exists
        /// </summary>
        /// <param name="intVolunteerTuid">the tuid of the volunteer we want.</param>
        /// <returns>a string for volunteer reason separated</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public string? GetReasonSeparated(int intVolunteerTuid)
        {
            try
            {
                int inactiveStatusTypeTuid = _dbContext.ReasonsSeparated.Where(x => x.VolunteerTuid == intVolunteerTuid).Select(x => x.InactiveStatusTypeItemTuid).FirstOrDefault();
                if (inactiveStatusTypeTuid > 0)
                {
                    return _dbContext.InactiveStatusTypeItems.Where(x => x.Tuid == inactiveStatusTypeTuid).Select(x => x.Name).FirstOrDefault();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1109._message, ErrorMessages._1109._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1110._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1110._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1111._message + e.Message, ErrorMessages._1111._code);
            }

            return null;
        }

        /// <summary>
        /// this method gets volunteer general information for each volunteer
        /// </summary>
        /// <returns>a list of volunteer general information models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        /// <note>This method gave me the "cannot convert linq to SQL exception when I did it any other way than this</note>
        public List<ReportVolunteerGeneralInformationModel> GetGeneralInfoAllVolunteer(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {

            IEnumerable<Volunteer> generalInfo = Enumerable.Empty<Volunteer>();

            try
            {
                generalInfo = _dbContext.Volunteers.Where(x => x.IsDeleted == false);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1112._message, ErrorMessages._1112._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1113._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1113._code);
                }
                return new List<ReportVolunteerGeneralInformationModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1114._message + e.Message, ErrorMessages._1114._code);
                return new List<ReportVolunteerGeneralInformationModel>();
            }


            if (blnActive)
            {
                generalInfo = generalInfo.Where(x => x.IsActive == true).ToList();
            }
            if (blnInactive)
            {
                generalInfo = generalInfo.Where(x => x.IsActive == false).ToList();
            }
            if (blnCurrent)
            {
                generalInfo = generalInfo.Where(x => x.SeparatedDate == null).ToList();
            }
            if (blnFormer)
            {
                generalInfo = generalInfo.Where(x => x.SeparatedDate != null).ToList();
            }
            
            return generalInfo.Select(x => new ReportVolunteerGeneralInformationModel
            {
                Address = x.Address == null ? "null" : x.Address.AddressLine1 + ", " + x.Address.City + ", " + x.Address.State + " " + x.Address.Zipcode ?? "",
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                AltPhone = x.AltPhone,
                StartDate = x.StartDate,
                Phone = x.Phone,
                IsActive = x.IsActive,
                EndDate = x.SeparatedDate ?? DateTime.MinValue,
                Tuid = x.Tuid,
                IsDeleted = x.IsDeleted,
            }).ToList();
        }

        /// <summary>
        /// This method gets the general information of a single volunteer
        /// </summary>
        /// <param name="intVolunteerTuid">the tuid for the volunteer whose general information we want.</param>
        /// <returns>a list of reportVolunteerGeneralInformation models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<ReportVolunteerGeneralInformationModel> GetGeneralInfoForVolunteer(int intVolunteerTuid)
        {
            try
            {
                return _dbContext.Volunteers.Where(x => x.Tuid == intVolunteerTuid).Select(x => new ReportVolunteerGeneralInformationModel
                {
                    Address = x.Address == null ? "null" : x.Address.AddressLine1 + ", " + x.Address.City + ", " + x.Address.State + " " + x.Address.Zipcode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    AltPhone = x.AltPhone,
                    StartDate = x.StartDate,
                    Phone = x.Phone,
                    IsActive = x.IsActive,
                    EndDate = x.SeparatedDate,
                    Tuid = x.Tuid,
                })
                .OrderBy(x => x.LastName)
                .ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1118._message, ErrorMessages._1118._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1119._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1119._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1120._message + e.Message, ErrorMessages._1120._code);
            }

            return new List<ReportVolunteerGeneralInformationModel>();
        }

        /// <summary>
        /// This method gets temporary info for any volunteer
        /// </summary>
        /// <returns>a list of tempInforReport models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<TempInfoReportModel> GetTempInfoForAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            IEnumerable<TempInfoEntry> filter = Enumerable.Empty<TempInfoEntry>();

            try
            {
                filter = _dbContext.TempInfoEntries.Where(x => x.Volunteer == null ? false : x.Volunteer.IsDeleted == false);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1121._message, ErrorMessages._1121._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1122._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1122._code);
                }

                return new List<TempInfoReportModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1123._message + e.Message, ErrorMessages._1123._code);
                return new List<TempInfoReportModel>();
            }



            if (blnActive)
            {
                filter = filter.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == true);
            }
            if (blnInactive)
            {
                filter = filter.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == false);
            }
            if (blnCurrent)
            {
                filter = filter.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate == null);
            }
            if (blnFormer)
            {
                filter = filter.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate != null);
            }
            List<TempInfoReportModel> result = filter.Select(x => new TempInfoReportModel
            {
                VolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName,
                TempInfoName = x.TempInfoType == null ? "null" : x.TempInfoType.Name,
                TempInfoValue = x.value
            }).ToList();
            return result;
        }

        /// <summary>
        /// This method gets temporary information for a single volunteer
        /// </summary>
        /// <param name="intVolunteerTuid">the tuid of the volunteer</param>
        /// <returns>a list of TempInfoReportModels for the volunteer provided</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<TempInfoReportModel> GetTempInforForVolunteer(int intVolunteerTuid)
        {
            try
            {
                return _dbContext.TempInfoEntries.Where(x => x.VolunteerTuid == intVolunteerTuid)
                    .Select(x => new TempInfoReportModel
                    {
                        VolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName
                    ,
                        TempInfoName = x.TempInfoType == null ? "null" : x.TempInfoType.Name,
                        TempInfoValue = x.value
                    }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1124._message, ErrorMessages._1124._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1125._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1125._code);
                }

                return new List<TempInfoReportModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1126._message + e.Message, ErrorMessages._1126._code);
                return new List<TempInfoReportModel>();
            }
        }

        /// <summary>
        /// This method gets the school information of all schools in the system
        /// </summary>
        /// <returns>A list of SchoolInformationReportModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<SchoolInformationReportModel> GetSchoolInformation(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            IEnumerable<SchoolInformationReportModel> schoolInfo = new List<SchoolInformationReportModel>();

            try
            {
                schoolInfo = _dbContext.Schools.Select(x => new SchoolInformationReportModel
                {
                    SchoolName = x.Name,
                    Principal = x.Principal,
                    Secratary = x.Secretary,
                    Phone = x.ContactNumber,
                    Days = x.Days,
                    Hours = x.Hours,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    Address = x.Address == null ? "null" : x.Address.AddressLine1 + ", " + x.Address.City + ", " + x.Address.State + " " + x.Address.Zipcode
                }).OrderBy(x => x.SchoolName);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1127._message, ErrorMessages._1127._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1128._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1128._code);
                }

                return new List<SchoolInformationReportModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1129._message + e.Message, ErrorMessages._1129._code);
                return new List<SchoolInformationReportModel>();
            }


            if (blnActive)
            {
                schoolInfo = schoolInfo.Where(x => x.IsActive);
            }
            if (blnInactive)
            {
                schoolInfo = schoolInfo.Where(x => !x.IsActive);
            }
            if (blnCurrent)
            {
                schoolInfo = schoolInfo.Where(x => !x.IsDeleted);
            }
            if (blnFormer)
            {
                schoolInfo = schoolInfo.Where(x => x.IsDeleted);
            }
            return schoolInfo.ToList();
        }

        /// <summary>
        /// This method will get all volunteer's one time check items
        /// </summary>
        /// <returns>a List of OneTimeCheck models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<OneTimeChecksModel> GetOneTimeChecksAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            IEnumerable<OneTimeCheck> oneTimeChecks = Enumerable.Empty<OneTimeCheck>();

            try
            {
                oneTimeChecks = _dbContext.OneTimeChecks.Where(x => x.Volunteer == null ? false : x.Volunteer.IsDeleted == false);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1130._message, ErrorMessages._1130._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1131._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1131._code);
                }

                return new List<OneTimeChecksModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1132._message + e.Message, ErrorMessages._1132._code);
                return new List<OneTimeChecksModel>();
            }

            //sort by provided criteria
            IEnumerable<OneTimeCheck> sort = oneTimeChecks;
            if (blnActive)
            {
                sort = sort.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == true);
            }
            if (blnInactive)
            {
                sort = sort.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == false);
            }
            if (blnCurrent)
            {
                sort = sort.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate == null);
            }
            if (blnFormer)
            {
                sort = sort.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate != null);
            }

            var returnVal = sort.Select(x => new OneTimeChecksModel
            {
                AliasFingerPrint = x.AliasFingerprintDate,
                BackgroundCheck = x.HasBackgroundCheck,
                ConfidenceSOU = x.ConfidenceSouDate,
                DHS = x.DhsDate,
                FieldPrintCleared = x.FieldPrintDate,
                FilePhoto = x.HasFilePhoto,
                IChat = x.IChatDate,
                IDCopy = x.HasIdCopy,
                NSCHCCheckForm = x.HasNschc,
                NSOPW = x.NsopwDate,
                OrientTraining = x.HasTrainingSheet,
                ServiceDescription = x.HasServiceDescription,
                ServiceStartDate = x.ServiceStartDate,
                TBShot = x.TbShotDate,
                TrueScreen = x.TrueScreenDate,
                VolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName ?? "null",
            }).ToList();

            return returnVal;

        }

        /// <summary>
        /// This method will get the one time checks for the provided volunteer
        /// </summary>
        /// <param name="intVolunteerTuid">the tuid of the volunteer</param>
        /// <returns>a list of OneTimeChecksModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<OneTimeChecksModel> GetOneTimeChecksForVolunteer(int intVolunteerTuid)
        {
            try
            {
                return _dbContext.OneTimeChecks.Where(x => x.VolunteerTuid == intVolunteerTuid).Select(x => new OneTimeChecksModel
                {
                    AliasFingerPrint = x.AliasFingerprintDate,
                    BackgroundCheck = x.HasBackgroundCheck,
                    ConfidenceSOU = x.ConfidenceSouDate,
                    DHS = x.DhsDate,
                    FieldPrintCleared = x.FieldPrintDate,
                    FilePhoto = x.HasFilePhoto,
                    IChat = x.IChatDate,
                    IDCopy = x.HasIdCopy,
                    NSCHCCheckForm = x.HasNschc,
                    NSOPW = x.NsopwDate,
                    OrientTraining = x.HasTrainingSheet,
                    ServiceDescription = x.HasServiceDescription,
                    ServiceStartDate = x.ServiceStartDate,
                    TBShot = x.TbShotDate,
                    TrueScreen = x.TrueScreenDate,
                    VolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName ?? "null"
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1133._message, ErrorMessages._1133._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1134._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1134._code);
                }

                return new List<OneTimeChecksModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1135._message + e.Message, ErrorMessages._1135._code);
                return new List<OneTimeChecksModel>();
            }
        }

        /// <summary>
        /// this method gets site assignment information that is tied to all volunteers
        /// </summary>
        /// <returns>a list of SiteAssignmentReportModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        /// <modified>
        ///     Author : Richard Nader, jr., 4/2/2023 - changes : using a group by to get conditions and needs for students instead of looping, including instead of joinging for siteAssignment query
        /// </modified>
        public List<SiteAssignmentReportModel> GetSiteAssignmentsAllVolunteers(DateTime startDate, DateTime endDate, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            try
            {
                IEnumerable<SiteAssignmentReportModel> siteAssignments = _dbContext.AssignmentStudents.Where(x => x.Date >= startDate && x.Date <= endDate).Include(x => x.Assignment)
                    .Include(x => x.Student).Include(x => x.Assignment!.Classroom)
                    .Where(x => x.Assignment == null ? false : x.Assignment.Volunteer == null ? false : x.Assignment.Volunteer.IsDeleted == false).Select(x => new SiteAssignmentReportModel
                    {
                        //we specify fallbacks for possible null references to avoid errors and warnings
                        Classroom = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.ClassroomNumber ?? "null",
                        Age0To5 = x.Student == null ? false : x.Student.IsAgeBirthTo5 ?? false,
                        Age5To12 = x.Student == null ? false : x.Student.IsAge5To12 ?? false,
                        Days = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.School == null ? "null" : x.Assignment.Classroom.School.Days ?? "null",
                        DesiredOutcome = x.DesiredOutcome,
                        GradeLevel = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.GradeLevel ?? "null",
                        SchoolName = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.School == null ? "null" : x.Assignment.Classroom.School.Name ?? "null",
                        StudentIdentifier = x.Student == null ? "null" : x.Student.Identifier ?? "null",
                        Teacher = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.TeacherName ?? "null",
                        VolunteerName = x.Assignment == null ? "null" : x.Assignment.Volunteer == null ? "null" : x.Assignment.Volunteer.FullName ?? "null",
                        HoursPerWeek = "",
                        ClassroomSize = x.Assignment == null ? 0 : x.Assignment.Classroom == null ? 0 : x.Assignment.Classroom.ClassroomSize ?? 0,
                        StudentTuid = x.StudentTuid,
                        VolunteerTuid = x.Assignment == null ? -1 : x.Assignment.VolunteerTuid,
                        Date = x.Date,
                        IsActive = x.Assignment == null ? false : x.Assignment.Volunteer == null ? false : x.Assignment.Volunteer.IsActive,
                        SeperatedDate = x.Assignment == null ? null : x.Assignment.Volunteer == null ? null : x.Assignment.Volunteer.SeparatedDate,
                    });


                var sortedAssignments = siteAssignments;
                if (blnActive)
                {
                    sortedAssignments = sortedAssignments.Where(x => x.IsActive == true);
                }
                if (blnInactive)
                {
                    sortedAssignments = sortedAssignments.Where(x => x.IsActive == false);
                }
                if (blnCurrent)
                {
                    sortedAssignments = sortedAssignments.Where(x => x.SeperatedDate == null);
                }
                if (blnFormer)
                {
                    sortedAssignments = sortedAssignments.Where(x => x.SeperatedDate != null);
                }

                //RJ -- get needs and conditions as a dictionary for each student 
                var needAcronymsByStudentTuid = _dbContext.StudentNeeds.Include(x => x.StudentNeedItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.StudentNeedItem!.Acronym, (studentTuid, studentNeedItems) => new { StudentTuid = studentTuid, StudentNeedItems = studentNeedItems })
                    .ToDictionary(x => x.StudentTuid, x => x.StudentNeedItems);

                var conditionAcronymnsStudentTuid = _dbContext.StudentConditions.Include(x => x.ConditionItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.ConditionItem!.Acronym, (studentTuid, conditions) => new { StudentTuid = studentTuid, Conditions = conditions })
                    .ToDictionary(x => x.StudentTuid, x => x.Conditions);


                var queriedAssignments = sortedAssignments.ToList();


                foreach (var item in queriedAssignments)
                {
                    //get needs and conditions, and join them to make a comma separated list
                    if (needAcronymsByStudentTuid.ContainsKey(item.StudentTuid))
                    {
                        item.Needs = string.Join(", ", needAcronymsByStudentTuid[item.StudentTuid]);
                    }
                    else
                    {
                        item.Needs = string.Empty;
                    }

                    if (conditionAcronymnsStudentTuid.ContainsKey(item.StudentTuid))
                    {
                        item.Condition = string.Join(", ", conditionAcronymnsStudentTuid[item.StudentTuid]);
                    }
                    else
                    {
                        item.Condition = string.Empty;
                    }
                }
                return queriedAssignments;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1136._message, ErrorMessages._1136._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1137._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1137._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1138._message + e.Message, ErrorMessages._1138._code);
            }

            return new List<SiteAssignmentReportModel>();
        }


        /// <summary>
        /// This method gets site assignment information for the provided volunteer
        /// </summary>
        /// <param name="intVolunteerTuid">the tuid of our volunteer</param>
        /// <returns>a list of SiteAssignmentReportModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<SiteAssignmentReportModel> GetSiteAssignmentsForVolunteer(int intVolunteerTuid, DateTime startDate, DateTime endDate)
        {
            try
            {
                IEnumerable<SiteAssignmentReportModel> siteAssignments = _dbContext.AssignmentStudents.Where(x => x.Date >= startDate && x.Date <= endDate).Include(x => x.Assignment)
                    .Include(x => x.Student).Include(x => x.Assignment!.Classroom).Select(x => new SiteAssignmentReportModel
                    {
                        //we specify fallbacks for possible null references to avoid errors and warnings
                        Classroom = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.ClassroomNumber ?? "null",
                        Age0To5 = x.Student == null ? false : x.Student.IsAgeBirthTo5 ?? false,
                        Age5To12 = x.Student == null ? false : x.Student.IsAge5To12 ?? false,
                        Days = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.School == null ? "null" : x.Assignment.Classroom.School.Days ?? "null",
                        DesiredOutcome = x.DesiredOutcome,
                        GradeLevel = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.GradeLevel ?? "null",
                        SchoolName = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.School == null ? "null" : x.Assignment.Classroom.School.Name ?? "null",
                        StudentIdentifier = x.Student == null ? "null" : x.Student.Identifier ?? "null",
                        Teacher = x.Assignment == null ? "null" : x.Assignment.Classroom == null ? "null" : x.Assignment.Classroom.TeacherName ?? "null",
                        VolunteerName = x.Assignment == null ? "null" : x.Assignment.Volunteer == null ? "null" : x.Assignment.Volunteer.FullName ?? "null",
                        HoursPerWeek = "",
                        ClassroomSize = x.Assignment == null ? 0 : x.Assignment.Classroom == null ? 0 : x.Assignment.Classroom.ClassroomSize ?? 0,
                        StudentTuid = x.StudentTuid,
                        VolunteerTuid = x.Assignment == null ? -1 : x.Assignment.VolunteerTuid,
                        Date = x.Date,
                        IsActive = x.Assignment == null ? false : x.Assignment.Volunteer == null ? false : x.Assignment.Volunteer.IsActive,
                        SeperatedDate = x.Assignment == null ? null : x.Assignment.Volunteer == null ? null : x.Assignment.Volunteer.SeparatedDate,
                    }).Where(x => x.VolunteerTuid == intVolunteerTuid);

                var sortedAssignments = siteAssignments;

                //RJ -- get needs and conditions as a dictionary for each student 
                var needAcronymsByStudentTuid = _dbContext.StudentNeeds.Include(x => x.StudentNeedItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.StudentNeedItem!.Acronym, (studentTuid, studentNeedItems) => new { StudentTuid = studentTuid, StudentNeedItems = studentNeedItems })
                    .ToDictionary(x => x.StudentTuid, x => x.StudentNeedItems);

                var conditionAcronymnsStudentTuid = _dbContext.StudentConditions.Include(x => x.ConditionItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.ConditionItem!.Acronym, (studentTuid, conditions) => new { StudentTuid = studentTuid, Conditions = conditions })
                    .ToDictionary(x => x.StudentTuid, x => x.Conditions);


                var queriedAssignments = sortedAssignments.ToList();


                foreach (var item in queriedAssignments)
                {
                    //get needs and conditions, and join them to make a comma separated list
                    if (needAcronymsByStudentTuid.ContainsKey(item.StudentTuid))
                    {
                        item.Needs = string.Join(", ", needAcronymsByStudentTuid[item.StudentTuid]);
                    }
                    else
                    {
                        item.Needs = string.Empty;
                    }

                    if (conditionAcronymnsStudentTuid.ContainsKey(item.StudentTuid))
                    {
                        item.Condition = string.Join(", ", conditionAcronymnsStudentTuid[item.StudentTuid]);
                    }
                    else
                    {
                        item.Condition = string.Empty;
                    }
                }
                return queriedAssignments;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1139._message, ErrorMessages._1139._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1140._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1140._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1141._message + e.Message, ErrorMessages._1141._code);
            }

            return new List<SiteAssignmentReportModel>();

        }

        /// <summary>
        /// This method gets demographics information for all volunteers
        /// </summary>
        /// <returns>a List of DemographicReportModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<DemographicReportModel> GetDemographicsAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            IEnumerable<DemographicReportModel> vols = Enumerable.Empty<DemographicReportModel>();
            try
            {
                vols = _dbContext.Volunteers.Where(x => x.IsDeleted == false).Select(x => new DemographicReportModel
                {
                    DOB = x.Birthday,
                    Age = CalculateAge(x.Birthday, DateTime.Now),
                    Ethnicity = x.Ethnicity == null ? "null" : x.Ethnicity.Name,
                    FamilyOfMilitary = x.IsFamilyOfMilitary,
                    Gender = x.Gender == null ? "null" : x.Gender.Name,
                    IdentifiesAs = x.IdentifiesAs == null ? "null" : x.IdentifiesAs.Name,
                    IsVeteran = x.IsVeteran,
                    LastUpdated = x.LastUpdated,
                    RacialGroup = x.RacialGroup == null ? "null" : x.RacialGroup.Name,
                    VolunteerName = x.FullName,
                    IsActive = x.IsActive,
                    SeparatedDate = x.SeparatedDate
                });
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1142._message, ErrorMessages._1142._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1143._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1143._code);
                }
                new List<DemographicReportModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1144._message + e.Message, ErrorMessages._1144._code);
                return new List<DemographicReportModel>();
            }


            var sort = vols;
            if (blnActive)
            {
                sort = sort.Where(x => x.IsActive);
            }
            if (blnInactive)
            {
                sort = sort.Where(x => x.IsActive == false);
            }
            if (blnCurrent)
            {
                sort = sort.Where(x => x.SeparatedDate == null);
            }
            if (blnFormer)
            {
                sort = sort.Where(x => x.SeparatedDate != null);
            }
            return sort.ToList();
        }

        /// <summary>
        /// This method gets demographic information for the provided volunteer
        /// </summary>
        /// <param name="intVolunteerTuid">the tuid of our volunteer</param>
        /// <returns>a list of DemographicReportModel items</returns>
        public List<DemographicReportModel> GetDemographicsForVolunteer(int intVolunteerTuid)
        {
            try
            {
                return _dbContext.Volunteers.Where(x => x.Tuid == intVolunteerTuid).Select(x => new DemographicReportModel
                {
                    DOB = x.Birthday,
                    Age = CalculateAge(x.Birthday, DateTime.Now),
                    Ethnicity = x.Ethnicity == null ? "null" : x.Ethnicity.Name,
                    FamilyOfMilitary = x.IsFamilyOfMilitary,
                    Gender = x.Gender == null ? "null" : x.Gender.Name,
                    IdentifiesAs = x.IdentifiesAs == null ? "null" : x.IdentifiesAs.Name,
                    IsVeteran = x.IsVeteran,
                    LastUpdated = x.LastUpdated,
                    RacialGroup = x.RacialGroup == null ? "null" : x.RacialGroup.Name,
                    VolunteerName = x.FullName,
                    IsActive = x.IsActive,
                    SeparatedDate = x.SeparatedDate
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1145._message, ErrorMessages._1145._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1146._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1146._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1147._message + e.Message, ErrorMessages._1147._code);
            }

            return new List<DemographicReportModel>();
        }

        /// <summary>
        /// This method gets meal in kind entries for all volunteers in the provided date range
        /// </summary>
        /// <param name="startDate">the start date</param>
        /// <param name="endDate">the end date</param>
        /// <returns>a list of MealInKindReport model items</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<MealInKindReportModel> GetMealInKindAllVolunteers(DateTime startDate, DateTime endDate, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            try
            {
                IEnumerable<MealMileage> counts = _dbContext.MealMileages.Where(x => x.Volunteer == null ? false : x.Volunteer.IsDeleted == false)
                    .Where(x => x.Date >= startDate && x.Date <= endDate);
                if (blnActive)
                {
                    counts = counts.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == true);
                }
                if (blnInactive)
                {
                    counts = counts.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == false);
                }
                if (blnCurrent)
                {
                    counts = counts.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate == null);
                }
                if (blnFormer)
                {
                    counts = counts.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate != null);
                }
                List<MealInKindReportModel> modelCounts = counts.Select(x => new MealInKindReportModel
                {
                    date = x.Date,
                    Mileage = x.Mileage,
                    numBusRides = x.BusRideCount,
                    numMeals = x.MealCount,
                    strVolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName
                }).ToList();
                foreach (var count in modelCounts)
                {
                    //get the first rate item that occurs before the count
                    var rates = _dbContext.MealTransportRates.Where(x => x.Date <= count.date).Select(x => new MealTransportRate
                    {
                        BusMileageRate = x.BusMileageRate,
                        MileageRate = x.MileageRate,
                        MealRate = x.MealRate
                    }).FirstOrDefault();

                    if (rates != null)
                    {
                        count.busRate = rates.BusMileageRate;
                        count.mealRate = rates.MealRate;
                        count.mileRate = rates.MileageRate;
                    }
                    else
                    {
                        count.busRate = 0;
                        count.mealRate = 0;
                        count.mileRate = 0;
                    }
                }
                return modelCounts;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1148._message, ErrorMessages._1148._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1149._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1149._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1150._message + e.Message, ErrorMessages._1150._code);
            }

            return new List<MealInKindReportModel>();
        }

        /// <summary>
        /// This method gets the meal in kind information for the provided volunteer in the provided date range
        /// </summary>
        /// <param name="startDate">the start date</param>
        /// <param name="endDate">the end date</param>
        /// <param name="intVolunteerTuid">the tuid of our volunteer</param>
        /// <returns>a list of meal in kind report models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<MealInKindReportModel> GetMealInKindForVolunteer(DateTime startDate, DateTime endDate, int intVolunteerTuid)
        {
            try
            {
                var counts = _dbContext.MealMileages.Where(x => x.VolunteerTuid == intVolunteerTuid).Where(x => x.Date >= startDate && x.Date <= endDate)
                    .Select(x => new MealInKindReportModel
                    {
                        date = x.Date,
                        Mileage = x.Mileage,
                        numBusRides = x.BusRideCount,
                        numMeals = x.MealCount,
                        strVolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName
                    }).ToList();

                foreach (var count in counts)
                {
                    //get the first rate item that occurs before the count
                    var rates = _dbContext.MealTransportRates.Where(x => x.Date <= count.date)
                        .Select(x => new MealTransportRate { BusMileageRate = x.BusMileageRate, MileageRate = x.MileageRate, MealRate = x.MealRate }).FirstOrDefault();
                    if (rates != null)
                    {
                        count.busRate = rates.BusMileageRate;
                        count.mealRate = rates.MealRate;
                        count.mileRate = rates.MileageRate;
                    }
                    else
                    {
                        count.busRate = 0;
                        count.mealRate = 0;
                        count.mileRate = 0;
                    }
                }

                return counts;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1151._message, ErrorMessages._1151._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1152._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1152._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1153._message + e.Message, ErrorMessages._1153._code);
            }

            return new List<MealInKindReportModel>();
        }
        /// <summary>
        /// This method gets volunteer finances for all volunteers in the daterange
        /// </summary>
        /// <param name="startDate">the start date</param>
        /// <param name="endDate">the end date</param>
        /// <returns>a List of VolunteerFinanceReport models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<VolunteerFinanceReportModel> GetAllVolunteerFinances(DateTime startDate, DateTime endDate, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer)
        {
            IEnumerable<PTOStipend> ptoStipendItems = Enumerable.Empty<PTOStipend>();

            try
            {
                ptoStipendItems = _dbContext.PTOStipends
                    .Where(x => x.Volunteer == null ? false : x.Volunteer.IsDeleted == false)
                    .Where(x => x.Date >= startDate && x.Date <= endDate && (x.Volunteer == null ? false : x.Volunteer.IsDeleted == false));
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1154._message, ErrorMessages._1154._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1155._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1155._code);
                }

                return new List<VolunteerFinanceReportModel>();
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1156._message + e.Message, ErrorMessages._1156._code);
                return new List<VolunteerFinanceReportModel>();
            }

            if (blnActive)
            {
                ptoStipendItems = ptoStipendItems.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == true);
            }
            if (blnInactive)
            {
                ptoStipendItems = ptoStipendItems.Where(x => x.Volunteer == null ? false : x.Volunteer.IsActive == false);
            }
            if (blnFormer)
            {
                ptoStipendItems = ptoStipendItems.Where(x => x.Volunteer == null ? true : x.Volunteer.SeparatedDate != null);
            }
            if (blnCurrent)
            {
                ptoStipendItems = ptoStipendItems.Where(x => x.Volunteer == null ? false : x.Volunteer.SeparatedDate == null);
            }

            return ptoStipendItems.Select(x => new VolunteerFinanceReportModel
            {
                VolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName,
                PTOEarned = (double)x.PtoEarned,
                PTOUsed = (double)x.PtoUsed,
                RegHours = (double)x.RegularHours,
                YTDHours = (double)x.YearToDateHour,
                StipendPaid = (double)x.StipendPaid,
                Date = x.Date
            }).ToList();
        }

        /// <summary>
        /// gets the volunteer finances for the provided volunteer in the provided date range
        /// </summary>
        /// <param name="startDate">the start date</param>
        /// <param name="endDate">the end date</param>
        /// <param name="intVolunteerTuid">the tuid of our volunteer</param>
        /// <returns>a List of VolunteerFinanceReport models</returns>
        public List<VolunteerFinanceReportModel> GetFinancesForVolunteer(DateTime startDate, DateTime endDate, int intVolunteerTuid)
        {
            try
            {
                return _dbContext.PTOStipends.Where(x => x.VolunteerTuid == intVolunteerTuid).Where(x => x.Date >= startDate && x.Date <= endDate)
                    .Select(x => new VolunteerFinanceReportModel
                    {
                        VolunteerName = x.Volunteer == null ? "null" : x.Volunteer.FullName,
                        PTOEarned = (double)x.PtoEarned,
                        PTOUsed = (double)x.PtoUsed,
                        RegHours = (double)x.RegularHours,
                        YTDHours = (double)x.YearToDateHour,
                        StipendPaid = (double)x.StipendPaid,
                        Date = x.Date
                    }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1157._message, ErrorMessages._1157._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1158._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1158._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1159._message + e.Message, ErrorMessages._1159._code);
            }
            return new List<VolunteerFinanceReportModel>();
        }

        /// <summary>
        /// This method will return a list of all donation items in the database in the provided time range
        /// </summary>
        /// <param name="startDate">the start date of the time range</param>
        /// <param name="endDate">the end date of the time range.</param>
        /// <returns>A list of DonationReportModel items</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/29/2023</created>
        public List<DonationReportModel> GetDonations(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _dbContext.InKindDonationTypeItems.Where(x => x.Date >= startDate && x.Date <= endDate)
                   .Select(x => new DonationReportModel { Name = x.Name, Type = x.DonationTypeItem == null ? "null" : x.DonationTypeItem.Name, Value = x.Value, Date = x.Date }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1160._message, ErrorMessages._1160._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1161._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1161._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1162._message + e.Message, ErrorMessages._1162._code);
            }
            return new List<DonationReportModel>();
        }

        /// <summary>
        /// This method gets the stipend rate on a given date
        /// </summary>
        /// <param name="date">the given date</param>
        /// <returns>a doule representing stipend rate</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public double GetStipendRate(DateTime date)
        {
            try
            {
                var stipendItem = _dbContext.PTOStipendRates.Where(x => x.Date <= date).FirstOrDefault();
                if (stipendItem != null)
                {
                    return (double)stipendItem.StipendRate;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1163._message, ErrorMessages._1163._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1164._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1164._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1165._message + e.Message, ErrorMessages._1165._code);
            }
            return 0;
        }

        /// <summary>
        /// This method calculates the age of a volunteer given their birthday and the current date
        /// </summary>
        /// <param name="birthDate">the volunteers birthday</param>
        /// <param name="now">todays date</param>
        /// <returns>an int representing date</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        private static int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }
    }
}
