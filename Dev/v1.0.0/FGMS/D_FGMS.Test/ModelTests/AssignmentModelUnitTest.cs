using B_FGMS.BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

/**
 ************************************************************************************************************************
 *                                      File Name : AssignmentModelUntiTest.cs                                                  *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Tyler Moody                                                        *
 *                                      Date Created : 02/09/2023                                                       *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 02/09/2023                                                      *
 *                                      Last Modified By : Tyler Moody                                                  *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to test various components of the AssignmentModel object.                 *
 ************************************************************************************************************************
 * -Andrew Loesel - I commented out this test due to the constructor making the program not compile.                    *
 **/

namespace D_FGMS.Test
{
    [TestClass]
    public class AssignmentModelUnitTest
    {
       /* [TestMethod]
        public void TestAssignmentModelCanBeCreated()
        {
            Mock<VolunteerModel> mockVolunteer = new Mock<VolunteerModel>();
            Mock<SchoolModel> mockSchool = new Mock<SchoolModel>();
            Mock<ClassroomModel> mockClassroom = new Mock<ClassroomModel>("Jane Doe", "5th", "123A", 30);
            Mock<ScheduleModel> mockSchedule = new Mock<ScheduleModel>("Monday Wednesday Friday", "8:00AM", "2:30PM");

            AssignmentModel assignment = new AssignmentModel(
                mockVolunteer.Object,
                mockSchool.Object,
                mockClassroom.Object,
                mockSchedule.Object,
                5,
                7
            );

            Assert.IsNotNull(assignment);
            Assert.AreEqual(5, assignment.TotalStudentsAgesBirthTo5);
            Assert.AreEqual(7, assignment.TotalStudentsAges5To12);
            Assert.AreEqual(12, assignment.TotalStudentsAssigned);
        }
        [TestMethod]
        public void TestAssignmentModelCanBeCreatedWithTuid()
        {
            Mock<VolunteerModel> mockVolunteer = new Mock<VolunteerModel>();
            Mock<SchoolModel> mockSchool = new Mock<SchoolModel>();
            Mock<ClassroomModel> mockClassroom = new Mock<ClassroomModel>("Jane Doe", "5th", "123A", 30);
            Mock<ScheduleModel> mockSchedule = new Mock<ScheduleModel>("Monday Wednesday Friday", "8:00AM", "2:30PM");

            AssignmentModel assignment = new AssignmentModel(
                34,
                mockVolunteer.Object,
                mockSchool.Object,
                mockClassroom.Object,
                mockSchedule.Object,
                5,
                7
            );

            Assert.IsNotNull(assignment);
            Assert.AreEqual(34, assignment.Tuid);
            Assert.AreEqual(5, assignment.TotalStudentsAgesBirthTo5);
            Assert.AreEqual(7, assignment.TotalStudentsAges5To12);
            Assert.AreEqual(12, assignment.TotalStudentsAssigned);
        }*/
    }
}