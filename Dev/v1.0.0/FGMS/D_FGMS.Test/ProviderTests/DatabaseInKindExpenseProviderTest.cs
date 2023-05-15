
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.EventBroker;

namespace D_FGMS.Test.ProviderTests
{
    /// <summary>
    /// This is a class to test the functionality of the DatabaseInKindExpenseProvider
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>2/15/23</created>
    [TestClass]
    public class DatabaseInKindExpenseProviderTest
    {
        private readonly ApplicationDbContext _testContext;
        private readonly DatabaseInKindExpenseProvider _testInKindExpenseProvider;
        private readonly int intStartYear = 2020;
        private readonly int intEndYear = 2021;
      

        public DatabaseInKindExpenseProviderTest()
        {
            //Set up an in memory database double
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("testDB")
                .Options;
            /* It would be more appropriate to create dbContext locally in each test method, so that each
                database test instance is clean, however this will dramatically increase test time, so The
                test will just have to be programmed according to the data that is inserted into the database. */
            _testContext = new ApplicationDbContext(options);
            //delete the database so that any old data that may still be in memory is gone (this gave me some issues, apparantly there's a chance the inMemory db persists)
            _testContext.Database.EnsureDeleted();
            _testInKindExpenseProvider = new DatabaseInKindExpenseProvider(_testContext);
            InsertTestData();

        }
        public void InsertTestData()
        {
            //create a volunteer entity to use when we insert new inkindexpenses (the logic in DatabaseInKindExpenseProvider doesn't work otherwise)
            Volunteer v = new Volunteer();
            v.FirstName = "Test";
            v.LastName = "Test";
            v.Email = "Test@test.com";
            v.Phone = "9899899899";
            //we need to add some expense types to test the GetAllExpenseType method. Just a Name should be fine since that is all the method checks
            _testContext.InKindExpenseTypeItems.Add(new InKindExpenseTypeItem { Name = "Meal-In-Kind" }); //This will be TUID 1
            _testContext.InKindExpenseTypeItems.Add(new InKindExpenseTypeItem { Name = "Bus Transport" }); //TUID 2
            _testContext.InKindExpenseTypeItems.Add(new InKindExpenseTypeItem { Name = "Donation" }); //TUID 3
            //lets add a double since the GetAllExpenseType method should return only distinct nams
            _testContext.InKindExpenseTypeItems.Add(new InKindExpenseTypeItem { Name = "Donation" }); //TUID 4
            //the next line did reveal an error in the logic because 7/1/year 12:00:00Am is equal to the date used in the provider's method and I was only checking for greater than.
            _testContext.InKindExpenses.Add(new InKindExpense { VolunteerTuid = 0, Date = new DateTime(intStartYear, 7, 1), Value = 3, Volunteer = v, ExpenseTypeTuid = 1 });
            _testContext.InKindExpenses.Add(new InKindExpense { VolunteerTuid = 0, Date = new DateTime(intEndYear, 6, 30), Value = (decimal)3.4, Volunteer = v, ExpenseTypeTuid = 2 });

            //since this is a one shot for data we will also need to keep the fiscal year in mind
            //the second and third years added above will both show up in a fiscal year of that range, lets add expenses on the first and last days of the fiscal year
            //fiscal year runs 10/1 to 9/30
            _testContext.InKindExpenses.Add(new InKindExpense { VolunteerTuid = 0, Date = new DateTime(intStartYear, 10, 1), Value = 1, Volunteer = v, ExpenseTypeTuid = 1 });
            _testContext.InKindExpenses.Add(new InKindExpense { VolunteerTuid = 0, Date = new DateTime(intEndYear, 9, 30), Value = 4, Volunteer = v, ExpenseTypeTuid = 2 });
            
            _testContext.SaveChanges();
        }
        /// <summary>
        /// Test The logic of getting all expense types from the database
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/15/23</created>
        //[TestMethod]
        /*public void GetAllExpenseTypeTest()
        {
            //Arrange
                //arranging has been done in the InsertTestData mehod
            //Act
            var AllExpenseTypes = _testInKindExpenseProvider.Get();
            //Assert
            Assert.AreEqual(3, AllExpenseTypes.Count());
        }*/

        /// <summary>
        /// Test the logic of the get all espense for a grant year method.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/16/23</created>
        /// <bugs>
        ///     <resolved>Logic error in the DatabaseInKindExpesneProvider.GetAllExpenseGrantYear method. Where clause
        ///                 of selection use x.date > fiscalStartDate, x.date < fiscalEndDate, when the operators should
        ///                 have been >= and <= respectively.</resolved>
        /// </bugs>
        /*[TestMethod]
        public void GetAllExpensesGrantYear_Test()
        {
            //Arrange
                //arranging has been done in the InsertTestData mehod
            //Act
            var expensesForGrantYear = _testInKindExpenseProvider.GetAllInKindExpensesGrantYear(intStartYear, intEndYear);

            //Assert
            *//* 4 expenses with different dates were added, we expect to see the one added for the first day of grant period, last day of grant period, and first day of
               fiscal period *//*
            Assert.AreEqual(3, expensesForGrantYear.Count());
        }
*/
        /// <summary>
        /// This tests the DatabaseInKindExpenseProvider.GetAllExpenseFiscalYear method
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/16/23</created>
       /* [TestMethod]
        public void GetAllExpenseFiscalYear_Test()
        {
            //Arrange
                //arranging has been done in the InsertTestData mehod
            //Act
            var expensesForFiscalYear = _testInKindExpenseProvider.GetAllInKindExpensesFiscalYear(intStartYear, intEndYear);

            //Assert
            *//* 4 expenses were added. We expect GetAllInKindExpensesFiscalYear to return expenses for the first and last day of fiscal period
             * and last day of grant period*//*
            Assert.AreEqual(3, expensesForFiscalYear.Count());
        }*/

        /// <summary>
        /// This tests the DatabaseInKindExpenseProvider.GetAllExpenseForType_GrantYear method
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/16/23</created>
       /* [TestMethod]
        public void GetAllExpensesForType_GrantYear_Test()
        {
            //Arrange
                //arranging has been done in the InsertTestData mehod
            //Act
                //The way TUIDS are assigned for InKindExpenses in the InsertTestData method means that we should get 2 results for Meal-In-Kind (Tuid = 1)
            var MealInKindGrantYearExpenses = _testInKindExpenseProvider.GetAllExpenseForType_GrantYear("Meal-In-Kind", intStartYear, intEndYear);

            //Assert
            Assert.AreEqual(2, MealInKindGrantYearExpenses.Count());
        }*/

        /// <summary>
        /// This tests the DatabaseInKindExpenseProvider.GetAllExpenseForType_FiscalYear method
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>2/16/23</created>
        /*[TestMethod]
        public void GetAllExpensesForType_FiscalYear_Test()
        {
            //Arrange
                //arranging has been done in the InsertTestData mehod
            //Act
                //The way TUIDS are assigned for InKindExpenses in the InsertTestData method means that we should get 2 results for Bus Transport (Tuid = 2)
            var BusTransportFiscalYearExpenses = _testInKindExpenseProvider.GetAllExpenseForType_FiscalYear("Bus Transport", intStartYear, intEndYear);

            //Assert
            Assert.AreEqual(2, BusTransportFiscalYearExpenses.Count());
        }*/

    }
}
