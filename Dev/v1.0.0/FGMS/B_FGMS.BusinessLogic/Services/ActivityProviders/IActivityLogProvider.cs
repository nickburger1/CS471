using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <FileName> IActivityLogProvider.cs </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/18/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/23/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// This class describes the business logic of the implemented IActivityLogProvider interface so that
/// these methods may be called in the UI layer of the application
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.Services.ActivityProviders
{
    public interface IActivityLogProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        IEnumerable<ActivityLogModel> GetAllActivityLogs();
        IEnumerable<ActivityLogModel> GetFilteredActivityLogs(int? volunteerTuid, DateTime? startDate, DateTime? endDate);
        IEnumerable<ActivityLogModel> GetAllActivityLogsByVolunteerTuid(int VolunteerTuid);
        bool UpdateActivityLog(ActivityLogModel activityLog);
        bool DeleteActivityLog(int? ActivityLogTuid);
        bool AddActivityLog(ActivityLogModel activityLog);
    }
}
