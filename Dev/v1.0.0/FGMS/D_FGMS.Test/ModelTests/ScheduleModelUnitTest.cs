using B_FGMS.BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

/**
 ************************************************************************************************************************
 *                                      File Name : ScheduleModelUnitTest.cs                                            *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Tyler Moody                                                        *
 *                                      Date Created : 02/09/2023                                                       *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 02/09/2023                                                      *
 *                                      Last Modified By : Tyler Moody                                                  *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to test various components of the ClassroomModel object.                  *
 ************************************************************************************************************************
 **/

namespace D_FGMS.Test
{
    [TestClass]
    public class ScheduleModelUnitTest
    {
        [TestMethod]
        public void TestScheduleModelCanBeCreated()
        {
            ScheduleModel schedule = new ScheduleModel(
                "Monday Wednesday Friday",
                "8:00AM",
                "2:30PM"
            );

            Assert.IsNotNull(schedule);
        }

        //Had to comment this out due to last minute changes to page
/*        [TestMethod]
        public void TestScheduleModelPrivateFunctions()
        {
            ScheduleModel schedule = new ScheduleModel(
                "Monday Wednesday Friday",
                "8:00am",
                "2:30pm"
            );

            Assert.IsNotNull(schedule);
            Assert.AreEqual("135", schedule.DaysNumberFormat);
            Assert.AreEqual("Monday Wednesday Friday", schedule.Days);
            Assert.AreEqual("8:00am", schedule.StartTime);
            Assert.AreEqual("2:30pm", schedule.EndTime);
            Assert.AreEqual(6.5, schedule.HoursPerDay);
            Assert.AreEqual(3, schedule.NumberOfDays);
            Assert.AreEqual(19.5, schedule.HoursPerWeek);
        }*/
    }
}