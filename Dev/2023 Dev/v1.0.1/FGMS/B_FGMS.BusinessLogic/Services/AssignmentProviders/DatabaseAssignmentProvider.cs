using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace B_FGMS.BusinessLogic.Services.AssignmentProviders
{
#pragma warning disable CS8602
    /**
 ************************************************************************************************************************
 *                                      File Name : DatabaseAssignmentProvider.cs                                       *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson & Nathan VanSnepson                                 *
 *                                      Date Created : 2/9/23                                                           *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 2/16/2023                                                       *
 *                                      Last Modified By : Kiefer Thorson                                               *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to retrieve Assignment data                                               *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author: Kiefer Thorson                                                                                               *
 * Date: 2/16/2023                                                                                                      *
 * Description: Added GetActiveVolunteersBySchoolTuid                                                                   *
 ************************************************************************************************************************
 **/
    public class DatabaseAssignmentProvider : IAssignmentProvider
    {
        private readonly ApplicationDbContext _dbContext; //This contains the information about the database

        public event EventHandler<Events.ErrorEventArgs> DatabaseError;


        /// <summary>
        /// Function Name: DatabaseAssignmentProvider
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Initialize the database context (dbContext)
        /// </summary>
        /// <param name="dbContext"></param>
        public DatabaseAssignmentProvider(ApplicationDbContext dbContext)
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
        /// Function Name: GetActiveVolunteersBySchoolAssignmentCount
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Returns count of active _volunteers for a school
        /// </summary>
        /// <returns>int - count of active _volunteers for a school</returns>
        public int GetActiveVolunteersBySchoolAssignmentCount(int schoolTuid)
        {
            try 
            {
                var activeAssignments = _dbContext.Assignments.Include(x => x.Classroom).Include(x => x.Volunteer)
                     .Where(x => x.Volunteer.IsDeleted != true && x.Volunteer.SeparatedDate == null)
                     .Where(x => x.Classroom.SchoolTuid == schoolTuid)
                     .Where(x => x.IsDeleted != true && x.Classroom.IsDeleted != true);
                
                return activeAssignments.Select(x => x.VolunteerTuid).Distinct().Count();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0300._message, ErrorMessages._0300._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0301._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0301._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0302._message + e.Message, ErrorMessages._0302._code);
            }

            return -1;
        }



        /// <summary>
        /// Function Name: GetAllAssignments
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Returns collection of Schools
        /// </summary>
        /// <returns>Assignment - Collection</returns>
        /// <concerns>This method is RETURNING DATABASE ENTITIES TO THE UI LAYER. Something people were told not to do.</concerns>
        public IEnumerable<Assignment> GetAllAssignments()
        {
            try
            {
                return _dbContext.Assignments.Where(x => x.IsDeleted != true).Include(x => x.Classroom).Include(x => x.Classroom.School).Include(x => x.Volunteer).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0303._message, ErrorMessages._0303._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0304._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0304._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0305._message + e.Message, ErrorMessages._0305._code);
            }

            return Enumerable.Empty<Assignment>();
        }


        /// <summary>
        /// Function Name: GetAssignmentBySchoolTuid
        /// Created By: Anthony Chippi
        /// Date Created: 4/1/2023
        /// Additional Contributors:
        /// Last Modified: 4/1/2023
        /// Last Modified By: Anthony Chippi
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Returns an Assignment using the Assignment's Tuid
        /// </summary>
        /// <returns>Assignment</returns>
        public IEnumerable<SchoolAssignmentModel> GetAssignmentBySchoolTuid(int schoolTuid)
        {
            try
            {
                return _dbContext.Assignments.Where(x => x.Classroom.SchoolTuid == schoolTuid).Include(x => x.Classroom).Include(x => x.Classroom.School)
                    .Include(x => x.Volunteer).OrderBy(x => x.Classroom.School.Name).Select(x => new SchoolAssignmentModel
                {
                    ClassroomNumber = x.Classroom.ClassroomNumber ?? "N/A",
                    ClassroomGradeLevel = x.Classroom.GradeLevel ?? "N/A",
                    ClassroomTuid = x.Classroom.Tuid,
                    PrincipalName = x.Classroom.School.Principal ?? "N/A",
                    SchoolName = x.Classroom.School.Name ?? "N/A",
                    SchoolTuid = x.Classroom.SchoolTuid,
                    TeacherName = x.Classroom.TeacherName ?? "N/A",
                    VolunteerName = x.Volunteer.FirstName + " " + x.Volunteer.LastName ?? "N/A"
                }).ToList();
                }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0306._message, ErrorMessages._0306._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0307._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0307._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0308._message + e.Message, ErrorMessages._0308._code);
            }

            return Enumerable.Empty<SchoolAssignmentModel>();
        }

        /// <summary>
        /// Function Name: GetActiveVolunteersBySchoolTuid
        /// Created By: Kiefer Thorson                    
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified:         
        /// Last Modified By:                                   
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Returns a list of active _volunteers assigned to that school's tuid
        /// </summary>
        /// <returns>Assignment</returns>
        /// <param name="schoolTuid"></param>
        /// <returns>Assignment - List of _volunteers</returns>
        public IEnumerable<AssignmentModel> GetActiveVolunteersBySchoolTuid(int schoolTuid)
        {
            try
            {
                var assignments = _dbContext.Assignments.Include(x => x.Classroom).Where(x => x.Classroom.SchoolTuid == schoolTuid)
                    .Include(x => x.Volunteer).Where(x => x.Volunteer.SeparatedDate == null).Where(x => x.Classroom.School.IsDeleted == false);
                 return assignments.Select(x => new AssignmentModel
                {
                    Classroom = new ClassroomModel
                    {
                        Room = x.Classroom.TeacherName ?? "N/A",
                        Grade =  x.Classroom.GradeLevel ?? "N/A",
                        Teacher = x.Classroom.TeacherName ?? "N/A",
                        TotalStudents = x.Classroom.ClassroomSize ?? 0
                    },
                    Schedule = JsonConvert.DeserializeObject<ScheduleModel>(x.Schedule ?? "{\"day\": \"\", \"startTime\":\"\", \"endTime\":\"\"}"),
                    Volunteer = new VolunteerModel
                    {
                        FirstName = x.Volunteer.FirstName,
                        LastName = x.Volunteer.LastName,
                        Phone = x.Volunteer.Phone
                    },
                    School = new SchoolModel
                    {
                        Name = x.Classroom.School.Name,
                        AddressTuid = x.Classroom.School.AddressTuid,
                        Address = x.Classroom.School.Address,

                    }
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0309._message, ErrorMessages._0309._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0310._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0310._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0311._message + e.Message, ErrorMessages._0311._code);
            }

            return Enumerable.Empty<AssignmentModel>();
        }

        /// <summary>
        /// Function Name: GetActiveVolunteersBySchoolTuid
        /// Created By: Kiefer Thorson                    
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified:         
        /// Last Modified By:                                   
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Returns a list of active _volunteers assigned to that school's tuid
        /// </summary>
        /// <returns>Assignment</returns>
        /// <param name="schoolTuid"></param>
        /// <returns>Assignment - List of _volunteers</returns>
        public int? GetTotalStudentsClassroomBySchoolTuid(int schoolTuid)
        {
            try
            {
                return _dbContext.Assignments.Include(x => x.Classroom).Where(x => x.Classroom.SchoolTuid == schoolTuid).ToList().Sum(x => x.Classroom.ClassroomSize);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0312._message, ErrorMessages._0312._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0313._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0313._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0314._message + e.Message, ErrorMessages._0314._code);
            }

            return null;
        }


        public int? GetTotalStudentsAssignedBySchoolTuid(int schoolTuid)
        {
            try
            {
                List<Assignment> assignments = _dbContext.Assignments.Include(x => x.Classroom).Where(x => x.Classroom.SchoolTuid == schoolTuid).ToList();
                int count = 0;
                foreach (var assignment in assignments)
                {
                    count += _dbContext.AssignmentStudents.Where(x => x.AssignmentTuid == assignment.Tuid).Count();
                }

                return count;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0315._message, ErrorMessages._0315._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0316._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0316._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0317._message + e.Message, ErrorMessages._0317._code);
            }

            return null;
        }



        public int? GetTotal0to5BySchoolTuid(int schoolTuid)
        {
            try
            {
                List<Assignment> assignments = _dbContext.Assignments.Include(x => x.Classroom).Where(x => x.Classroom.SchoolTuid == schoolTuid).ToList();
                int count = 0;
                foreach (var assignment in assignments)
                {
                    count += _dbContext.AssignmentStudents.Where(x => x.AssignmentTuid == assignment.Tuid && x.Student.IsAgeBirthTo5 == true).Count();
                }

                return count;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0318._message, ErrorMessages._0318._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0319._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0319._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0320._message + e.Message, ErrorMessages._0320._code);
            }

            return null;
        }




        public int? GetTotal6to12BySchoolTuid(int schoolTuid)
        {
            try
            {
                List<Assignment> assignments = _dbContext.Assignments.Include(x => x.Classroom).Where(x => x.Classroom.SchoolTuid == schoolTuid).ToList();
                int count = 0;
                foreach (var assignment in assignments)
                {
                    count += _dbContext.AssignmentStudents.Where(x => x.AssignmentTuid == assignment.Tuid && x.Student.IsAge5To12 == true).Count();
                }

                return count;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0321._message, ErrorMessages._0321._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0322._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0322._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0323._message + e.Message, ErrorMessages._0323._code);
            }

            return null;
        }

        /// <summary>
        /// Function Name: DeleteChildAssignments
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Reset all child assignments.
        /// </summary>
        /// <modify>Andrew Loesel : 4/12/2023 - make function delete child assignments and associated data</modify>
        public bool DeleteChildAssignments()
        {
            try
            {
                _dbContext.RemoveRange(_dbContext.StudentNeeds);
                _dbContext.RemoveRange(_dbContext.StudentConditions);
                _dbContext.RemoveRange(_dbContext.AssignmentStudents);
                _dbContext.RemoveRange(_dbContext.Classrooms);
                _dbContext.RemoveRange(_dbContext.Assignments);
                _dbContext.RemoveRange(_dbContext.Students);
                _dbContext.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Function Name: DeleteChildAssignments
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Delete the Conditions for a child
        /// </summary>
        public void DeleteStudentConditions(int studentTuid)
        {
            try
            {
                var studentConditions = _dbContext.StudentConditions.Where(x => x.StudentTuid.Equals(studentTuid)).ToList();

                if (studentConditions != null)
                {
                    foreach (var condition in studentConditions)
                    {
                        _dbContext.StudentConditions.Remove(condition);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0327._message, ErrorMessages._0327._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0328._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0328._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0329._message + e.Message, ErrorMessages._0329._code);
            }
        }

        /// <summary>
        /// Function Name: DeleteChildAssignments
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Delete the Needs for a child
        /// </summary>
        public void DeleteStudentNeeds(int studentTuid)
        {
            try
            {
                var studentNeeds = _dbContext.StudentNeeds.Where(x => x.StudentTuid.Equals(studentTuid)).ToList();

                if (studentNeeds != null)
                {
                    foreach (var need in studentNeeds)
                    {
                        _dbContext.StudentNeeds.Remove(need);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0330._message, ErrorMessages._0330._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0331._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0331._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0332._message + e.Message, ErrorMessages._0332._code);
            }
        }

        /// <summary>
        /// Function Name: DeleteChildAssignments
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Delete the child reference from the assignment table.
        /// </summary>
        public void DeleteAssignmentStudent(int studentTuid)
        {
            try
            {
                var studentAssignment = _dbContext.AssignmentStudents.Where(x => x.StudentTuid.Equals(studentTuid)).ToList();

                if (studentAssignment != null)
                {
                    foreach (var assignment in studentAssignment)
                    {
                        _dbContext.AssignmentStudents.Remove(assignment);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0330._message, ErrorMessages._0330._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0331._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0331._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0332._message + e.Message, ErrorMessages._0332._code);
            }
        }

        /// <summary>
        /// Function Name: DeleteStudent
        /// Created By: Timothy Johnson                 
        /// Date Created: 3/31/2023
        /// Additional Contributors:
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     Delete the child reference from the databasse.
        /// </summary>
        public void DeleteStudent(int studentTuid)
        {
            try
            {
                var students = _dbContext.Students.Where(x => x.Tuid.Equals(studentTuid)).ToList();
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        _dbContext.Students.Remove(student);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0333._message, ErrorMessages._0333._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0334._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0334._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0335._message + e.Message, ErrorMessages._0335._code);
            }
        }

        /// <summary>
        /// This method returns models instead of entities that contain information on all current assignments
        /// </summary>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/5/2023</created>
        public List<SchoolAssignmentModel> GetAllAssignmentsWithIncludes()
        {
            try
            {
                return _dbContext.Assignments.Include(x => x.Classroom).Include(x => x.Classroom.School).Include(x => x.Volunteer).OrderBy(x => x.Classroom.School.Name).Select(x => new SchoolAssignmentModel
                {
                    ClassroomNumber = x.Classroom.ClassroomNumber ?? "N/A",
                    ClassroomGradeLevel = x.Classroom.GradeLevel ?? "N/A",
                    ClassroomTuid = x.Classroom.Tuid,
                    PrincipalName = x.Classroom.School.Principal ?? "N/A",
                    SchoolName = x.Classroom.School.Name ?? "N/A",
                    SchoolTuid = x.Classroom.SchoolTuid,
                    TeacherName = x.Classroom.TeacherName ?? "N/A",
                    VolunteerName = x.Volunteer.FirstName + " " + x.Volunteer.LastName ?? "N/A"
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0336._message, ErrorMessages._0336._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0337._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0337._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0338._message + e.Message, ErrorMessages._0338._code);
            }

            return new List<SchoolAssignmentModel>();
        }

        /// <summary>
        /// this method returns the classroom tuid that corresponds to an assigned student given that students tuid
        /// </summary>
        /// <param name="intStudentTuid"></param>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <Created>4/12/2023</Created>
        public int? GetClassroomTuidByStudentTuid(int intStudentTuid)
        {
            try
            {
                int? assignmentTuid = _dbContext.AssignmentStudents.Where(x => x.StudentTuid == intStudentTuid).Select(x => x.AssignmentTuid).FirstOrDefault();
                if (assignmentTuid != null)
                {
                    int? intClassroomTuid = _dbContext.Assignments.Where(x => x.Tuid == assignmentTuid).Select(x => x.ClassroomTuid).FirstOrDefault();
                    if (intClassroomTuid != null)
                    {
                        return intClassroomTuid;
                    }
                }
                return null;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0339._message, ErrorMessages._0339._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0340._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0340._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0341._message + e.Message, ErrorMessages._0341._code);
            }

            return 0;

        }


    }
}
