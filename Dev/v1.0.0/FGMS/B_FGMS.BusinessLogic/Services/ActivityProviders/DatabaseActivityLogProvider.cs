using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <FileName> DatabaseActivityLogProvider.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/20/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/22/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this file is to manage the relationship between the ActivityLogModel
/// and the ActivityLog Entity.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.Services.ActivityProviders
{
    public class DatabaseActivityLogProvider : IActivityLogProvider
    {
        public readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public DatabaseActivityLogProvider(ApplicationDbContext dbContext)
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
        /// Finds the activity log in the database by tuid and updates it with the data from the model.
        /// </summary>
        /// <param name="activityLogModel">The business logic object.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/18/2023</created>
        /// <returns>True if successfully updated. False if not. </returns>
        public bool UpdateActivityLog(ActivityLogModel activityLogModel)
        {
            ActivityLog activityLogEntity = GetActivityLogEntityByTuid(activityLogModel.Tuid);

            MapActivityLogModelToEntity(activityLogModel, activityLogEntity);

            bool databaseWasUpdated = SaveChangesToDatabase();

            return databaseWasUpdated;
        }

        /// <summary>
        /// Creates a new ActivityLog and saves it to the database.
        /// </summary>
        /// <param name="activityLogModel">The business logic object.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/23/2023</created>
        /// <returns>True if successfully updated. False if not. </returns>
        public bool AddActivityLog(ActivityLogModel activityLogModel)
        {
            try
            {
                ActivityLog activityLogEntity = new ActivityLog();

                MapActivityLogModelToEntity(activityLogModel, activityLogEntity);

                _dbContext.Add(activityLogEntity);

                bool databaseWasUpdated = SaveChangesToDatabase();

                return databaseWasUpdated;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1900._message, ErrorMessages._1900._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1901._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1901._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1902._message + e.Message, ErrorMessages._1902._code);
            }

            return false;
        }

        /// <summary>
        /// Saves changes in the context to the database and returns true if records were changed.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        /// <exception cref="Exception">Error when updating the database.</exception>
        /// <returns>True if successfully updated. False if not. </returns>
        private bool SaveChangesToDatabase()
        {
            int recordsChanged;

            try
            {
                recordsChanged = _dbContext.SaveChanges();

                return recordsChanged > 0;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1903._message, ErrorMessages._1903._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1904._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1904._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1905._message + e.Message, ErrorMessages._1905._code);
            }

           return false;
        }

        /// <summary>
        /// Get the ActivityLog entity by id and deletes it from the database.
        /// </summary>
        /// <param name="ActivityLogTuid">Tuid used to find the activity log.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        /// <returns>True if item was deleted, false if not.</returns>
        public bool DeleteActivityLog(int? ActivityLogTuid)
        {
            try
            {
                ActivityLog activityLog = GetActivityLogEntityByTuid(ActivityLogTuid);
                _dbContext.ActivityLogs.Remove(activityLog);
                bool databaseWasDeleted = SaveChangesToDatabase();

                return databaseWasDeleted;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1906._message, ErrorMessages._1906._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1907._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1907._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1908._message + e.Message, ErrorMessages._1908._code);
            }

            return false;
        }

        /// <summary>
        /// Apply filters and get a list of activity logs.
        /// </summary>
        /// <param name="volunteerTuid">Volunteer to filter by.</param>
        /// <param name="startDate">Logs after and on this date.</param>
        /// <param name="endDate">Logs before and on this date.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/01/2023</created>
        /// <returns>List of filtered ActivityLogModels.</returns>
        public IEnumerable<ActivityLogModel> GetFilteredActivityLogs(int? volunteerTuid, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                IQueryable<ActivityLog> query = _dbContext.ActivityLogs;

                query = FilterByVolunteer(query, volunteerTuid);
                query = FilterByStartDate(query, startDate);
                query = FilterByEndDate(query, endDate);

                return query.OrderByDescending(a => a.Date).Select(a => MapActivityLogEntityToModel(a)).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1909._message, ErrorMessages._1909._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1910._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1910._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1911._message + e.Message, ErrorMessages._1911._code);
            }

            return Enumerable.Empty<ActivityLogModel>();
        }

        /// <summary>
        /// Add date range filter to query.
        /// </summary>
        /// <param name="query">Activity log query.</param>
        /// <param name="startDate">Logs after and on this date.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/05/2023</created>
        /// <returns>Activity log query with date range filter.</returns>
        private static IQueryable<ActivityLog> FilterByStartDate(IQueryable<ActivityLog> query, DateTime? startDate)
        {
            if (startDate != null)
            {
                query = query.Where(a => a.Date.Date >= startDate);
            }

            return query;
        }

        /// <summary>
        /// Add date range filter to query.
        /// </summary>
        /// <param name="query">Activity log query.</param>
        /// <param name="endDate">Logs before and on this date.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/05/2023</created>
        /// <returns>Activity log query with date range filter.</returns>
        private static IQueryable<ActivityLog> FilterByEndDate(IQueryable<ActivityLog> query, DateTime? endDate)
        {
            if (endDate != null)
            {
                query = query.Where(a => a.Date.Date <= endDate);
            }

            return query;
        }

        

        /// <summary>
        /// Add volunteer filter to query.
        /// </summary>
        /// <param name="query">Activity log query.</param>
        /// <param name="volunteerTuid">Volunteer to filter by.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/01/2023</created>
        /// <returns>Activity log query with volunteer filter.</returns>
        private static IQueryable<ActivityLog> FilterByVolunteer(IQueryable<ActivityLog> query, int? volunteerTuid)
        {
            if (volunteerTuid != null && volunteerTuid != 0)
            {
                query = query.Where(a => a.VolunteerTuid == volunteerTuid);
            }

            return query;
        }

        /// <summary>
        /// Gets all of the activity log records.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>02/18/2023</created>
        /// <returns>List of ActivityLogModels.</returns>
        public IEnumerable<ActivityLogModel> GetAllActivityLogs()
        {
            try
            {
                return _dbContext.ActivityLogs.Select(a => MapActivityLogEntityToModel(a)).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1912._message, ErrorMessages._1912._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1913._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1913._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1914._message + e.Message, ErrorMessages._1914._code);
            }

            return Enumerable.Empty<ActivityLogModel>();
        }

        /// <summary>
        /// Converts an ActivityLog entity to a ActivityLogModel.
        /// </summary>
        /// <param name="activityLog">The database entity.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/18/2023</created>
        /// <returns>The activity log records stored in an ActivityLogModel.</returns>
        private static ActivityLogModel MapActivityLogEntityToModel(ActivityLog activityLog)
        {
            return new ActivityLogModel(
                activityLog.Tuid,
                new VolunteerModel {
                    Tuid = activityLog.VolunteerTuid,
                    FirstName = activityLog.Volunteer.FirstName,
                    LastName = activityLog.Volunteer.LastName },
                activityLog.Date, activityLog.Initial, activityLog.Incident);
        }

        /// <summary>
        /// Finds all activity logs for a given volunteer tuid and returns the logs.
        /// </summary>
        /// <param name="VolunteerTuid">Tuid used for filtering.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        /// <returns>A list of ActivityLogModels.</returns>
        public IEnumerable<ActivityLogModel> GetAllActivityLogsByVolunteerTuid(int VolunteerTuid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Maps the activity log model to the entity.
        /// </summary>
        /// <param name="activityLogModel">The business logic object.</param>
        /// <param name="activityLogEntity">The database entity.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/18/2023</created>
        private void MapActivityLogModelToEntity(ActivityLogModel activityLogModel, ActivityLog activityLogEntity)
        {
            activityLogEntity.VolunteerTuid = activityLogModel.VolunteerTuid;
            activityLogEntity.Date = activityLogModel.Date;
            activityLogEntity.Initial = activityLogModel.Initial;
            activityLogEntity.Incident = activityLogModel.Incident;
        }

        /// <summary>
        /// Uses tuid to find activiy log record and returns the entity.
        /// </summary>
        /// <param name="Tuid">Tuid of activity log record. </param>
        /// <returns>ActivityLog entity</returns>
        /// <author>Tyler Moody</author>
        /// <created>02/18/2023</created>
        private ActivityLog GetActivityLogEntityByTuid(int? tuid)
        {
            try
            {
                return _dbContext.ActivityLogs.Where(a => a.Tuid == tuid).FirstOrDefault();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1915._message, ErrorMessages._1915._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1916._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1916._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1917._message + e.Message, ErrorMessages._1917._code);
            }

            return new ActivityLog();
        }
    }
}
