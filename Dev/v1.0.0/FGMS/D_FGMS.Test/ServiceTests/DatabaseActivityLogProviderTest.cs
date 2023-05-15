using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using HandyControl.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_FGMS.Test.ServiceTests
{
    [TestClass]
    public class DatabaseActivityLogProviderTest
    {
        [TestMethod]
        public void TestActivityLogCanBeUpdated()
        {
            DateTime dateTime = DateTime.Now;

            // Create updated data.
            Mock<VolunteerModel> mockVolunteer = new Mock<VolunteerModel>();
            ActivityLogModel activityLogModel = new ActivityLogModel
            (
                1,
                mockVolunteer.Object,
                dateTime,
                "N",
                "Something else happend."
            );

            // Create database records.
            IQueryable<ActivityLog> data = new List<ActivityLog>
            {
                new ActivityLog { Tuid = 1, VolunteerTuid = 1, Date = dateTime, Initial = "Y", Incident = "Something happend." },
            }.AsQueryable();

            // Set up mock dataset.
            Mock<DbSet<ActivityLog>> mockSet = new Mock<DbSet<ActivityLog>>();
            mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            // Mock the ApplicationDbContext and set up to return the dataset.
            int expectedSaves = 1;

            Mock<ApplicationDbContext> mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.ActivityLogs).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(expectedSaves);

            // Create provider and test
            DatabaseActivityLogProvider activityLogProvider = new DatabaseActivityLogProvider(mockContext.Object);
            bool recordUpdated = activityLogProvider.UpdateActivityLog(activityLogModel);

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(true, recordUpdated);
        }

        //[TestMethod]
        //public void TestActivityLogGetAll()
        //{
        //    DateTime dateTime = DateTime.Now;
        //    // Created the return data.
        //    IQueryable<ActivityLog> data = new List<ActivityLog>
        //    {
        //        new ActivityLog { Tuid = 1, VolunteerTuid = 1, Date = dateTime, Initial = "Y", Incident = "Something happend." },
        //    }.AsQueryable();

        //    // Set up mock dataset.
        //    Mock<DbSet<ActivityLog>> mockSet = new Mock<DbSet<ActivityLog>>();
        //    mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.Provider).Returns(data.Provider);
        //    mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<ActivityLog>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        //    // Mock the ApplicationDbContext and set up to return the dataset.
        //    Mock<ApplicationDbContext> mockContext = new Mock<ApplicationDbContext>();
        //    mockContext.Setup(c => c.ActivityLogs).Returns(mockSet.Object);

        //    // Create provider and test.
        //    DatabaseActivityLogProvider activityLogProvider = new DatabaseActivityLogProvider(mockContext.Object);
        //    List<ActivityLogModel> activityLogs = activityLogProvider.GetAllActivityLogs().ToList();

        //    Assert.AreEqual(1, activityLogs.Count);
        //    //Assert.AreEqual(1, Volunteer.Tuid);
        //    Assert.AreEqual(dateTime, activityLogs[0].Date);
        //    Assert.AreEqual("Y", activityLogs[0].Initial);
        //    Assert.AreEqual("Something happend.", activityLogs[0].Incident);
        //}
    }
}
