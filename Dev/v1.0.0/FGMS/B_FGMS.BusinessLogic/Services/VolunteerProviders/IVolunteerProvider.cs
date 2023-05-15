using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.ViewModels.VolunteerAddViewModels;
using B_FGMS.BusinessLogic.ViewModels;
/**
************************************************************************************************************************
*                                      File Name : IVolunteerProvider.cs                                               *
*                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
************************************************************************************************************************
*                                      Created By :                                                                    *
*                                      Date Created :                                                                  *
*                                      Additional Contributors : CS471 WI23 Development Team                           *
*                                      Last Modified : 2/16/2023                                                       *
*                                      Last Modified By : Kiefer Thorson                                               *
************************************************************************************************************************
* File Purpose : The Purpose of this file is to provide an interface for Volunteers                                    *
************************************************************************************************************************
* Modification Log:                                                                                                    *
* Author: Kiefer Thorson & Nathan VanSnepson                                                                           *
* Date: 2/9/2023                                                                                                       *
* Description: Added methods to return count of active, inactive, and total _volunteers.								*
*																														*
* Author: Kiefer Thorson                                                                                               *
* Date: 2/16/2023                                                                                                      *
* Description: Added method to return volunteer information given their tuid.          								*
************************************************************************************************************************
**/
namespace B_FGMS.BusinessLogic.Services.VolunteerProviders
{
	/// <summary>
	/// Interface Name: IVolunteerProvider
	/// Created By: Kiefer Thorson & Nathan VanSnepson
	/// Date Created: 2/9/2023
	/// Additional Contributors:
	/// Last Modified: 2/16/2023
	/// Last Modified By: Kiefer Thorson                    
	/// 
	/// Purpose:
	/// This interface is the contract that has method declarations for the school provider 
	/// </summary>
	public interface IVolunteerProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        IEnumerable<Volunteer> GetAllVolunteers();
        List<VolunteerModel> GetAllVolunteersIncludeInactive();
        Volunteer? GetVolunteerByTuid(int volunteerTuid);
        public IEnumerable<SchoolModel> GetAllSchools();
        public IEnumerable<int> GetVolunteerFinancialsYears();
        public VolunteersFinancialsRatesModel? GetVolunteerFinancialsMealTransportRates(DateTime monthYear);
        public VolunteerFinancialsMealTransportModel? GetVolunteerFinancialsMealMileage(int volunteerTuid, DateTime monthYear);
        public VolunteerFinancialsPtoStipendModel? GetVolunteersFinancialsPtoStipend(int volunteerTuid, DateTime monthYear);
        public VolunteerFinancialsHoursModel? GetVolunteerFinancialsHours(int volunteerTuid, DateTime monthYear);
        public VolunteersFinancialsRatesModel? GetVolunteerFinancalsPtoStipendRates(DateTime monthYear);
        public int GetVolunteersCount();
        public int GetActiveVolunteersCount();
        public int GetInactiveVolunteersCount();
        public void PushVolunteerFinancialsHours(VolunteerFinancialsHoursModel? hoursModel, int volunteerTuid, DateTime monthYear);
        public void PushVolunteerFinancialsMealTransport(VolunteerFinancialsMealTransportModel? mealTransportModel, int volunteerTuid, DateTime monthYear);
        public void PushVolunteerFinancialsPtoStipend(VolunteerFinancialsPtoStipendModel? ptoStipendModel, int volunteerTuid, DateTime monthYear);        
        public IEnumerable<VolunteerNameIdModel> GetVolunteerNameAndId();
		public VolunteerGeneralModel? GetVolunteerGeneralInfo(int volunteerTUID);
		public OneTimeChecksModel? GetVolunteerOneTimeChecks(int volunteerTUID);
        public AnnualChecksModel? GetVolunteerAnnualChecks(int volunteerTUID, int year);
        //public AnnualChecksReportModel GetVolunteerAnnualCheck(int volunteerTUID);
        public IEnumerable<AnnualChecksReportModel> GetVolunteerAnnualCheck();
        public IEnumerable<TemporaryInfoModel> GetTemporaryInfo();
        public IEnumerable<TemporaryInfoModel> GetVolunteerTemporaryInfo(int volunteerTUID);
        public List<ClassroomsModel> GetAllClassrooms();
        public IEnumerable<ClassroomsModel> GetVolunteersClassroomsByVolunteer(int volunteerTuid);
        public IEnumerable<ClassroomsModel> GetVolunteersClassrooms(int volunteerTuid);
        public SchoolModel? GetVolunteerSchool(int volunteerTuid);
        public IEnumerable<ClassroomsModel> GetVolunteersClassroomsBySchool(int schoolTuid);
        public IEnumerable<ClassroomsModel> GetVolunteersClassroomsBySchoolVolunteer(int schoolTuid, int volunteerTuid);
        public int InsertNewClassroom(ClassroomsModel newClassroom);
        public int UpdateClassroom(ClassroomsModel updateClassroom);
        public int DeleteClassroom(ClassroomsModel deleteClassroom);
        public IEnumerable<VolunteerChildAssignmentDataGridModel> GetVolunteerChildAssignmentDataGrid(int volunteerId);
        public IEnumerable<VolunteerChildAssignmentDataGridModel> GetVolunteerChildAssignmentInRoom(int volunteerId, int schoolTuid, ClassroomsModel classroom);
        public VolunteerChildAssignmentsModel GetAllVolunteerChildAssignments(int volunteerId);
        public VolunteerChildAssignmentsModel GetVolunteerChildAssignments(int volunteerId, ClassroomsModel classroomNumber);
        public List<int> GetVolunteerAssignments(int volunteerTuid);
        public List<int> GetVolunteerAssignmentsInClassroom(int volunteerTuid, int classroomTuid);
        public int GetAllAssignedStudentsAge0To5(List<int> volunteerTuid);
        public int GetAssignedStudentsInClassroomAge0To5(List<int> assignmentTuids, int classroomTuid);
        public int GetAllAssignedStudentsAge5To12(List<int> assignmentTuid);
        public int GetAssignedStudentsInClassroomAge5To12(List<int> assignmentTuids, int classroomTuid);
        public IEnumerable<ConditionItemModel> GetAllConditions();
        public IEnumerable<StudentNeedItemModel> GetAllStudentNeeds();
        public List<ConditionItemModel> GetStudentConditions(int studentTuid);
        public List<StudentNeedItemModel> GetStudentNeeds(int studentTuid);
        public StudentModel? GetStudent(int studentTuid);
        public string GetStudentIdentifier(int studentTuid);
        public bool CheckIfIdentifierExists(string identifier);
        public string GetStudentDesiredOutCome(int studentTuid);
        public void PushVolunteerGeneralInfo(VolunteerGeneralModel volunteerGeneral, int volunteerTuid);
		public void PushOneTimeCheck(OneTimeChecksModel oneTimeChecks, int volunteerTuid);
		public void PushAnnualChecks(AnnualChecksModel annualChecks, int volunteerTuid);
        public void PushTemporaryInfo(IEnumerable<TemporaryInfoModel> temporaryInfoModels, int volunteerTuid);
        public void PushNewVolunteer(VolunteerAddViewModel volunteerInfo);
        public int InsertNewChildAssignment(NewVolunteerChildAssignmentsModel childAssignment, int volunteerId);
        public int InsertNewStudent(NewVolunteerChildAssignmentsModel childAssignment);
        public void DeleteVolunteer(int volunteerTuid);
        public void UpdateVolunteer(VolunteerGeneralViewModel volunteerGeneral);
        public IEnumerable<int> GetAnnualCheckYears();           
        public void InsertNewStudentConditons(int studentTuid, List<ConditionItemModel> childConditions);
        public void InsertNewStudentNeeds(int studentTuid, List<StudentNeedItemModel> childNeeds);
        public void UpdateVolunteerChildAssignmentsSchool(SchoolModel school, int volunteerId);
        public void InsertNewStudentConditon(ConditionItemModel condition);
        public void InsertNewStudentNeed(StudentNeedItemModel need);
        public void UpdateChildAssignment(int volunteerId, int studentTuid, NewVolunteerChildAssignmentsModel updateChildAssignment);
        public void DeleteChildAssignments(List<VolunteerChildAssignmentDataGridModel> childAssignments);
        public void DeleteStudentConditions(int studentTuid);
        public void DeleteStudentNeeds(int studentTuid);
        public void DeleteAssignmentStudent(int studentTuid);
        public void DeleteStudent(int studentTuid);
        VolunteerDemographicsModel? GetVolunteerDemographicsInfo(int volunteerTUID);
        public void UpdateVolunteerDemographics(VolunteerDemographicsViewModel volunteerDemographics);
        GenderTypeItem? GetGenderByTuid(int genderTuid);
        IdentifiesAsTypeItem? GetIdentifiesAsByTuid(int identifiesAsTuid);
        EthnicityTypeItem? GetEthnicityByTuid(int ethnicityTuid);
        RacialGroupTypeItem? GetRacialGroupByTuid(int racialGroupTuid);
        ReasonSeparated? GetReasonsSeparatedByTuid(int volunteerTuid);
        InactiveStatusTypeItem? GetInactiveStatusByTuid(int inactiveStatusTuid);
        IEnumerable<string> GetAllVolunteerNames();
        Volunteer? GetVolunteerByFullName(String volunteerFirstName, String volunteerLastName);
        IEnumerable<GenderNameIdModel> GetGenderNameAndId(bool addNew);
        IEnumerable<EthnicityNameIdModel> GetEthnityNameAndId(bool addNew);
        IEnumerable<RacialGroupNameIdModel> GetRacialGroupNameAndId(bool addNew);
        IEnumerable<IdentifiesAsNameIdModel> GetIdentifiesAsNameAndId(bool addNew);
        IEnumerable<ReasonsSeparatedNameIdModel> GetReasonsSeparatedNameAndId(bool addNew);
        IEnumerable<InactiveStatusTypeItem> GetReasonSeparatedNameAndId(bool addNew);
        IEnumerable<TempInfoModel> GetTempInfoNameAndId(bool addNew);
        void UpdateVolunteer();        	
        public void AddGenderItem(string item);
        public void AddIdentifiesAsItem(string item);
        public void AddEthnicityItem(string item);
        public void AddRacialGroupItem(string item);
        public void AddReasonSeparatedItem(string item);
        public void AddTempInfoItem(string name, int type);
        public void DeleteGenderItem(GenderNameIdModel item);
        public void DeleteIdentifiesAsItem(IdentifiesAsNameIdModel item);
        public void DeleteEthnicityItem(EthnicityNameIdModel item);
        public void DeleteRacialGroupItem(RacialGroupNameIdModel item);
        public void DeleteReasonSeparatedItem(InactiveStatusTypeItem item);
        public void DeleteTempInfoItem(TempInfoModel item);
        IEnumerable<VolunteerNameIdModel> GetVolunteersWithGender(int genderTUID);
        IEnumerable<VolunteerNameIdModel> GetVolunteersWithIdentifiesAs(int identifiesAsTUID);
        IEnumerable<VolunteerNameIdModel> GetVolunteersWithEthnicity(int ethnicityTUID);
        IEnumerable<VolunteerNameIdModel> GetVolunteersWithRacialGroup(int racialGroupTUID);
        public void CalculateVolunterFinancialsSingle(int volunteerTUID, DateTime monthYear);
        public void CalculateVolunteerFinancialsYear(int volunteerTUID, DateTime monthYear);
        public void CalculatePTOEarned(VolunteersFinancialsRatesModel? financialRates, PTOStipend? financialPtoStipends);
        IEnumerable<VolunteerNameIdModel> GetVolunteersWithReasonSeparated(int racialGroupTUID);
        IEnumerable<VolunteerNameIdModel> GetVolunteerWithTempInfoType(int racialGroupTUID);
        public void CalculatePTOStart(PTOStipend? ptoStipends, PTOStipend? prevPtoStipends);
        public void CalculatePTOEnded(PTOStipend? ptoStipends);
        public void CalculateStipendPaid(VolunteersFinancialsRatesModel? financialsRatesModel, PTOStipend? ptoStipends);
        public void CalculateYTDHours(PTOStipend? ptoStipends, PTOStipend? prevPtoStipends);
        public bool VerifyPtoStipendRecord(int volunteerTUID, DateTime monthYear);
        public void PushPTOEligibility(PTOStipend? financialPtoStipends, int volunteerTUID, DateTime monthYear);
        public bool GetPreviousMonthPTOEligibility(int volunteerTUID, DateTime monthYear);
        public VolunteersFinancialsRatesModel? GetAllVolunteerFinancialRates(VolunteersFinancialsRatesModel? financialPtoStipendRates, VolunteersFinancialsRatesModel? financialMealTransportRates);
        public bool IsVolunteerInActive(int volunteerTUID);
        public IEnumerable<VolunteerInfoReportModel> GetAllVolunteerInfoReport(int? volunteerTuid);
        public IEnumerable<VolunteerInfoReportModel> GetAllVolunteerInfoReportByActiveStatus(int? volunteerTuid, bool activeStatus);
        public bool UpdateVolunteerInfo(VolunteerInfoReportModel volunteerInfo);
        public IEnumerable<InactiveStatusTypesNameIdModel> GetInactiveStatusTypes();
        public void RestoreVolunteer(int Tuid);
        public IEnumerable<VolunteerNameIdModel> GetVolunteersWithSchools();

        public bool VerifyMealTransportRecord(int volunteerTUID, DateTime monthYear);
        public int? GetPersistantSelectedVolunteer();
        public void SetPersistantSelectedVolunteer(int selectedVolunteerIndex);
    }
}
