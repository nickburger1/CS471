using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Reports;
using B_FGMS.BusinessLogic.Models.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.ReportProviders
{
    public interface IReportProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public List<ExpenseEntryModel>? GetExpensesForReport(DateTime? startDate, DateTime? endDate, string strExpenseType, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<ExpenseEntryModel>? GetExpensesForReportForVolunteer(DateTime? startDate, DateTime? endDate, string strExpenseType, int intVolunteerTuid);
        public List<ReportAnnualCheckModel>? GetAnnualChecksAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<ReportAnnualCheckModel>? GetAnnualCheckForVolunteer(int intVolunteerTuid);
        public List<ReportVolunteerGeneralInformationModel> GetGeneralInfoAllVolunteer(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<ReportVolunteerGeneralInformationModel> GetGeneralInfoForVolunteer(int intVolunteerTuid);
        public string? GetReasonSeparated(int intVolunteerTuid);
        public List<TempInfoReportModel> GetTempInfoForAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<TempInfoReportModel> GetTempInforForVolunteer(int intVolunteerTuid);
        public List<SchoolInformationReportModel> GetSchoolInformation(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<OneTimeChecksModel> GetOneTimeChecksAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<OneTimeChecksModel> GetOneTimeChecksForVolunteer(int intVolunteerTuid);
        public List<SiteAssignmentReportModel> GetSiteAssignmentsAllVolunteers(DateTime startDate, DateTime endDate, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<SiteAssignmentReportModel> GetSiteAssignmentsForVolunteer(int intVolunteerTuid, DateTime startDate, DateTime endDate);
        public List<DemographicReportModel> GetDemographicsAllVolunteers(bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<DemographicReportModel> GetDemographicsForVolunteer(int intVolunteerTuid);
        public List<MealInKindReportModel> GetMealInKindAllVolunteers(DateTime startDate, DateTime endDate, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<MealInKindReportModel> GetMealInKindForVolunteer(DateTime startDate, DateTime endDate, int intVolunteerTuid);
        public List<VolunteerFinanceReportModel> GetAllVolunteerFinances(DateTime startDate, DateTime endDate, bool blnActive, bool blnInactive, bool blnCurrent, bool blnFormer);
        public List<VolunteerFinanceReportModel> GetFinancesForVolunteer(DateTime startDate, DateTime endDate, int intVolunteerTuid);
        public List<DonationReportModel> GetDonations(DateTime startDate, DateTime endDate);

        public double GetStipendRate(DateTime date);
    }
}
