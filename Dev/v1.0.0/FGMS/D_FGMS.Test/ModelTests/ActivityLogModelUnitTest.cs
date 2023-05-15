using B_FGMS.BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

/// <FileName> ActivityLogModelUnitTest.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/14/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/14/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to test various components of the ActivityLogModel object.
/// </summary>
/// <author> Tyler Moody </author>

namespace D_FGMS.Test
{
    [TestClass]
    public class ActivityLogModelUnitTest
    {
        [TestMethod]
        public void TestActivityLogModelCanBeCreated()
        {
            Mock<VolunteerModel> mockVolunteer = new Mock<VolunteerModel>();
            DateTime dateTime = DateTime.Now;

            ActivityLogModel activityLog = new ActivityLogModel(
                mockVolunteer.Object,
                dateTime,
                "Y",
                "Was sick."
            );

            Assert.IsNotNull(activityLog);
            Assert.IsNotNull(activityLog.Volunteer);
            Assert.AreEqual(dateTime, activityLog.Date);
            Assert.AreEqual("Y", activityLog.Initial);
            Assert.AreEqual("Was sick.", activityLog.Incident);
        }

        [TestMethod]
        public void TestActivityLogModelCanBeCreatedWithTuid()
        {
            Mock<VolunteerModel> mockVolunteer = new Mock<VolunteerModel>();
            DateTime dateTime = DateTime.Now;

            ActivityLogModel activityLog = new ActivityLogModel(
                43,
                mockVolunteer.Object,
                dateTime,
                "Y",
                "Was sick."
            );

            Assert.IsNotNull(activityLog);
            Assert.IsNotNull(activityLog.Volunteer);
            Assert.AreEqual(43, activityLog.Tuid);
            Assert.AreEqual(dateTime, activityLog.Date);
            Assert.AreEqual("Y", activityLog.Initial);
            Assert.AreEqual("Was sick.", activityLog.Incident);
        }
    }
}