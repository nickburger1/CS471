/**
 ************************************************************************************************************************
 *                                      File Name : Classroom.cs                                                        *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Tyler Moody                                                        *
 *                                      Date Created : 02/09/2023                                                       *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 02/09/2023                                                      *
 *                                      Last Modified By : Tyler Moody                                                  *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the model for a Classroom object.                              *
 ************************************************************************************************************************
 **/

namespace B_FGMS.BusinessLogic.Models
{
    public class ClassroomModel
    {
        public string Teacher { get; set; }  // Name of the teacher
        public string Grade { get; set; }
        public string Room { get; set; }
        public int TotalStudents { get; set; }
        public bool IsDeleted { get; set; }


        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="teacher">Teachers name</param>
        /// <param name="grade">Grade of the class</param>
        /// <param name="room">Room identifier</param>
        /// <param name="totalStudents">Number of total students</param>
        /// <author>Tyler Moody</author>
        /// <created>02/09/2023</created>
       /* public ClassroomModel(string teacher, string grade, string room, int totalStudents, bool isDeleted)
        {
            Teacher = teacher;
            Grade = grade;
            Room = room;
            TotalStudents = totalStudents;
            IsDeleted = isDeleted;
        }*/
    }
}
