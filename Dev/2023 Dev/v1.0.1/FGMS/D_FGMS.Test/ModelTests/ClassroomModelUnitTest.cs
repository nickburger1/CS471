using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

/**
 ************************************************************************************************************************
 *                                      File Name : ClassroomModelUnitTest.cs                                           *
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
    public class ClassroomModelUnitTest
    {
        [TestMethod]
        public void TestClassroomModelCanBeCreated()
        {
            ClassroomModel classroom = new ClassroomModel
            {
              Teacher = "Jane Doe",
                Grade = "5th",
                Room = "123A",
                TotalStudents = 30,
                IsDeleted = false,
            }
                
            ;

            Assert.IsNotNull(classroom);
            Assert.AreEqual("Jane Doe", classroom.Teacher);
            Assert.AreEqual("5th", classroom.Grade);
            Assert.AreEqual("123A", classroom.Room);
            Assert.AreEqual(30, classroom.TotalStudents);
        }
    }
}