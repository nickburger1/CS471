using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using Bogus.DataSets;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace B_FGMS.BusinessLogic.Services.StudentProviders
{
	
	public class DatabaseStudentProvider : IStudentProvider
	{
		private readonly ApplicationDbContext _dbContext; //This contains the information about the database
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        public DatabaseStudentProvider(ApplicationDbContext dbContext)
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


        public int GetAllAssignedStudentsCount()
        {
			try
			{
				return _dbContext.Students.Count();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1300._message, ErrorMessages._1300._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1301._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1301._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1302._message + e.Message, ErrorMessages._1302._code);
            }

			return 0;
        }


        public int GetAllStudentsCount()
        {
			int classroomSize = 0;

			try
			{
				var ClassroomList = _dbContext.Classrooms.Select(x =>
				new ClassroomModel
				{
					Teacher = x.TeacherName,
					Grade = x.GradeLevel,
					Room = x.ClassroomNumber,
					TotalStudents = (int)x.ClassroomSize,
					IsDeleted = (bool)x.IsDeleted,

				}).ToList();

				foreach (var Classroom in ClassroomList)
				{
					if (Classroom.IsDeleted == false)
					{
						classroomSize += Classroom.TotalStudents;
					}
				}
				return classroomSize;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1303._message, ErrorMessages._1303._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1304._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1304._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1305._message + e.Message, ErrorMessages._1305._code);
            }

            return -1;
        }

		public int GetStudentCount6to12()
        {
            try
            {
                int studentCount = 0;
                var StudentList = _dbContext.Students.Select(x =>
                new StudentModel
                {
                    Tuid = x.Tuid,
                    Identifier = x.Identifier,
                    IsAge5To12 = x.IsAge5To12,
                    IsAgeBirthTo5 = x.IsAgeBirthTo5,
                });

                foreach (var Student in StudentList)
                {
                    if (Student.IsAge5To12 == true)
                    {
                        studentCount++;
                    }
                }

                return studentCount;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1306._message, ErrorMessages._1306._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1307._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1307._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1308._message + e.Message, ErrorMessages._1308._code);
            }

            return -1;

        }

        /// <summary>
        /// Find the conditionEntity item by tuid and delete if found.
        /// </summary>
        /// <param name="needTuid">Tuid of needs item.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public void DeleteNeedItem(int needTuid)
        {
            var need = _dbContext.StudentNeedItems.FirstOrDefault(x => x.Tuid == needTuid);

            if (need != null)
            {
                _dbContext.StudentNeedItems.Remove(need);
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Fine the conditionEntity item by tuid and update it found.
        /// </summary>
        /// <param name="studentNeedItem">Student need item being updated.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public bool UpdateNeedItem(StudentNeedItemModel studentNeedItem)
        {
            StudentNeedItem needEntity = _dbContext.StudentNeedItems.Where(x => x.Tuid == studentNeedItem.Tuid).FirstOrDefault();

            MapNeedItemModelToEntity(studentNeedItem, needEntity);

            bool databaseWasUpdated = SaveChangesToDatabase();

            return databaseWasUpdated;
        }

        /// <summary>
        /// Map need item model to entity.
        /// </summary>
        /// <param name="studentNeedItem">Model being mapped from.</param>
        /// <param name="studentNeedItemEntity">Entity being mapped to.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void MapNeedItemModelToEntity(StudentNeedItemModel studentNeedItem, StudentNeedItem studentNeedItemEntity)
        {
            studentNeedItemEntity.Tuid = studentNeedItem.Tuid;
            studentNeedItemEntity.Acronym = studentNeedItem.Acronym;
            studentNeedItemEntity.Description = studentNeedItem.Description;
        }

        /// <summary>
        /// Find the condition item by tuid and delete if found.
        /// </summary>
        /// <param name="conditionTuid">Tuid of the condition item.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        public void DeleteConditionItem(int conditionTuid)
        {
            var condition = _dbContext.ConditionItems.FirstOrDefault(x => x.Tuid == conditionTuid);

            if (condition != null)
            {
                _dbContext.ConditionItems.Remove(condition);
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Find the condition item by tuid and update if found.
        /// </summary>
        /// <param name="conditionTuid">Tuid of condition item.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateCondtionItem(ConditionItemModel conditionItemModel)
        {
            ConditionItem conditionEntity = _dbContext.ConditionItems.Where(x => x.Tuid == conditionItemModel.Tuid).FirstOrDefault();

            MapConidtionItemModelToEntity(conditionItemModel, conditionEntity);

            bool databaseWasUpdated = SaveChangesToDatabase();

            return databaseWasUpdated;
        }

        /// <summary>
        /// Map need item model to entity.
        /// </summary>
        /// <param name="conditionItem">Model being mapped from.</param>
        /// <param name="conditionItemEntity">Entity being mapped to.</param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void MapConidtionItemModelToEntity(ConditionItemModel conditionItem, ConditionItem conditionItemEntity)
        {
            conditionItemEntity.Tuid = conditionItem.Tuid;
            conditionItemEntity.Acronym = conditionItem.Acronym;
            conditionItemEntity.Description = conditionItem.Description;
        }

        /// <summary>
        /// Search student conditions by condition tuid to see if the condition is being used.
        /// </summary>
        /// <param name="conditionTuid">Tuid of condition item.</param>
        /// <returns>True if a student if using the condition. False if not.</returns>
        public bool CheckConditionInUse(int conditionTuid)
        {
            var need = _dbContext.StudentConditions.FirstOrDefault(x => x.ConditionItemTuid == conditionTuid);

            return need != null;
        }

        /// <summary>
        /// Search student needs by conditionEntity tuid to see if the conditionEntity is being used.
        /// </summary>
        /// <param name="needTuid">Tuid of conditionEntity item.</param>
        /// <returns>True if a student if using the conditionEntity. False if not.</returns>
        public bool CheckNeedInUse(int needTuid)
        {
            var need = _dbContext.StudentNeeds.FirstOrDefault(x => x.StudentNeedItemTuid == needTuid);

            return need != null;
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
    }
}