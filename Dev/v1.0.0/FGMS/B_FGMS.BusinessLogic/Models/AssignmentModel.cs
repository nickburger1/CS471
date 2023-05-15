using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 ************************************************************************************************************************
 *                                      File Name : AssignmentModel.cs                                                  *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Tyler Moody                                                        *
 *                                      Date Created : 02/09/2023                                                       *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 02/09/2023                                                      *
 *                                      Last Modified By : Tyler Moody                                                  *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the model for an assigment object.                             *
 ************************************************************************************************************************
 **/

namespace B_FGMS.BusinessLogic.Models
{
    public class AssignmentModel
    {
        public int? Tuid { get; }
        public VolunteerModel? Volunteer { get; set; }
        public SchoolModel? School { get; set; }
        public ClassroomModel? Classroom { get; set; }
        public ScheduleModel? Schedule { get; set; }
        public int? TotalStudentsAgesBirthTo5 { get; set; }
        public int? TotalStudentsAges5To12 { get; set; }
        public int? TotalStudentsAssigned { get; set; }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="volunteer">Volunteer assigned</param>
        /// <param name="school">School of assignment</param>
        /// <param name="classroom">Classroom of assignment</param>
        /// <param name="schedule">Schedule for the assignment</param>
        /// <param name="totalStudentsAgesBirthTo5">Number of students</param>
        /// <param name="totalStudentsAges5To12">Number of students</param>
        /// <author>Tyler Moody</author>
        /// <created>02/09/2023</created>
        public AssignmentModel(VolunteerModel volunteer, SchoolModel school, ClassroomModel classroom, ScheduleModel schedule, int totalStudentsAgesBirthTo5, int totalStudentsAges5To12)
        {
            Volunteer = volunteer;
            School = school;
            Classroom = classroom;
            Schedule = schedule;
            TotalStudentsAgesBirthTo5 = totalStudentsAgesBirthTo5;
            TotalStudentsAges5To12 = totalStudentsAges5To12;
            TotalStudentsAssigned = totalStudentsAgesBirthTo5 + totalStudentsAges5To12;
        }

        /// <summary>
        /// Parameterized constructor with tuid.
        /// </summary>
        /// <param name="tuid">Identifier in the database</param>
        /// <param name="volunteer">Volunteer assigned</param>
        /// <param name="school">School of assignment</param>
        /// <param name="classroom">Classroom of assignment</param>
        /// <param name="schedule">Schedule for the assignment</param>
        /// <param name="totalStudentsAgesBirthTo5">Number of students</param>
        /// <param name="totalStudentsAges5To12">Number of students</param>
        /// <author>Tyler Moody</author>
        /// <created>02/09/2023</created>
        public AssignmentModel(int tuid, VolunteerModel volunteer, SchoolModel school, ClassroomModel classroom, ScheduleModel schedule, int totalStudentsAgesBirthTo5, int totalStudentsAges5To12)
        {
            Tuid = tuid;
            Volunteer = volunteer;
            School = school;
            Classroom = classroom;
            Schedule = schedule;
            TotalStudentsAgesBirthTo5 = totalStudentsAgesBirthTo5;
            TotalStudentsAges5To12 = totalStudentsAges5To12;
            TotalStudentsAssigned = totalStudentsAgesBirthTo5 + totalStudentsAges5To12;
        }

        public AssignmentModel()
        {

        }
    }
}
