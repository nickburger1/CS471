using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace B_FGMS.BusinessLogic.Services.ReportProviders
{
    public class DatabaseReportPresetProvider : IReportPresetProvider
    {
        private readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        public DatabaseReportPresetProvider(ApplicationDbContext dbContext) 
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
        /// Creates a new ReportPreset into the table
        /// </summary>
        /// <param name="reportPresetModel"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        public void CreateReportPreset(ReportPresetModel reportPresetModel)
        {

            ReportPreset reportPreset = new ReportPreset()
            {
                Name = reportPresetModel.Name ?? "N/A",
                Preset = JsonConvert.SerializeObject(reportPresetModel.Preset),
                LastUpdated = DateTime.Now,
                Active = reportPresetModel.Active,
                Former = reportPresetModel.Former,
                Current = reportPresetModel.Current,
                Inactive = reportPresetModel.Inactive,
                SortBy = reportPresetModel.SortBy ?? ""
            };

            try
            {
                _dbContext.ReportPresets.Add(reportPreset);
                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1000._message, ErrorMessages._1000._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1001._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1001._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1002._message + e.Message, ErrorMessages._1002._code);
            }
        }

        /// <summary>
        /// Deletes an existing preset
        /// </summary>
        /// <param name="tuid"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        public void DeleteReportPreset(int tuid)
        {
            try
            {
                var dbReportPreset = _dbContext.ReportPresets.FirstOrDefault(x => x.Tuid == tuid);

                if (dbReportPreset != null)
                {
                    _dbContext.ReportPresets.Remove(dbReportPreset);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1003._message, ErrorMessages._1003._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1004._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1004._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1005._message + e.Message, ErrorMessages._1005._code);
            }
        }

        /// <summary>
        /// Gets all existing presets
        /// </summary>
        /// <returns></returns>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        public IEnumerable<ReportPresetModel> GetAllReportPresets()
        {
            try
            {
                return _dbContext.ReportPresets.Select(x => new ReportPresetModel
                {
                    Tuid = x.Tuid,
                    Name = x.Name,
                    Preset = JsonConvert.DeserializeObject<List<TreeNode>>(x.Preset),
                    LastUpdated = DateTime.Now,
                    Active = x.Active,
                    Former = x.Former,
                    Current = x.Current,
                    Inactive = x.Inactive,
                    SortBy = x.SortBy
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1006._message, ErrorMessages._1006._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1007._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1007._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1008._message + e.Message, ErrorMessages._1008._code);
            }

            return Enumerable.Empty<ReportPresetModel>();
        }

        /// <summary>
        /// Get a preset given its ID
        /// </summary>
        /// <param name="Tuid"></param>
        /// <returns></returns>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        public ReportPresetModel? GetReportPreset(int Tuid)
        {
            try
            {
                return _dbContext.ReportPresets.Select(x => new ReportPresetModel
                {
                    Tuid = x.Tuid,
                    Name = x.Name,
                    Preset = JsonConvert.DeserializeObject<List<TreeNode>>(x.Preset),
                    LastUpdated = DateTime.Now,
                    Active = x.Active,
                    Former = x.Former,
                    Current = x.Current,
                    Inactive = x.Inactive,
                    SortBy = x.SortBy
                }).FirstOrDefault(x => x.Tuid == Tuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1009._message, ErrorMessages._1009._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1010._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1010._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1011._message + e.Message, ErrorMessages._1011._code);
            }

            return null;
        }

        /// <summary>
        /// Update an existing preset
        /// </summary>
        /// <param name="reportPresetModel"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        public void UpdateReportPreset(ReportPresetModel reportPresetModel)
        {
            try
            {
                var dbReportPreset = _dbContext.ReportPresets.FirstOrDefault(x => x.Tuid == reportPresetModel.Tuid);

                if (dbReportPreset != null)
                {
                    dbReportPreset.Name = reportPresetModel.Name ?? "N/A";
                    dbReportPreset.Preset = JsonConvert.SerializeObject(reportPresetModel.Preset);
                    dbReportPreset.LastUpdated = DateTime.Now;
                    dbReportPreset.SortBy = reportPresetModel.SortBy ?? "";
                    dbReportPreset.Current = reportPresetModel.Current;
                    dbReportPreset.Active = reportPresetModel.Active;
                    dbReportPreset.Inactive = reportPresetModel.Inactive;
                    dbReportPreset.Former = reportPresetModel.Former;

                    _dbContext.ReportPresets.Update(dbReportPreset);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1012._message, ErrorMessages._1012._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1013._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1013._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1014._message + e.Message, ErrorMessages._1014._code);
            }
        }

        /// <summary>
        /// This method will check to see if a preset already exists in the with the same treeNode structure as provided
        /// </summary>
        /// <param name="preset"></param>
        /// <returns>the name of the existing preset, or null if none was found</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        public string? MatchPresetOnPreset(List<TreeNode> preset)
        {
            try
            {
                var existingPreset = _dbContext.ReportPresets.Where(x => x.Preset == JsonConvert.SerializeObject(preset)).FirstOrDefault();
                if (existingPreset != null)
                {
                    return existingPreset.Name;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1015._message, ErrorMessages._1015._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1016._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1016._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1017._message + e.Message, ErrorMessages._1017._code);
            }

            return null;
        }

        /// <summary>
        /// This method will check t see if a preset with the current name already exists in the database
        /// </summary>
        /// <param name="strName"></param>
        /// <returns>true if a match was found, otherwise false</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/3/2023</created>
        public bool MatchPresetOnName(string strName)
        {
            try
            {
                var preset = _dbContext.ReportPresets.Where(x => x.Name.Equals(strName)).FirstOrDefault();
                if (preset != null)
                {
                    return true;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1018._message, ErrorMessages._1018._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1019._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1019._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1020._message + e.Message, ErrorMessages._1020._code);
            }

            return false;
        }
    }
}
