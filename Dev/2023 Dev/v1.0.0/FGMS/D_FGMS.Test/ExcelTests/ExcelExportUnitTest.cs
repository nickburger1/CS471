using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Windows;

/// <FileName> ExcelExportUnitTest.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/14/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/14/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to test exporting to excel. Must be commented out when not using.
/// </summary>
/// <author> Tyler Moody </author>

namespace D_FGMS.Test
{
    [TestClass]
    public class ExcelExportUnitTest
    {
        [TestMethod]
        public void TestExportToExcel()
        {
            // Create tables
            //ExcelTableModel summaryTable = new ExcelTableModel()
            //{
            //    Title = "Summary",
            //    Headers = new List<string>()
            //    {
            //        "Volunteer",
            //        "Start Date",
            //        "End Date"
            //    },
            //    Rows = new List<object>()
            //    {
            //        new { Volunteer = "Bob", StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date }
            //    }
            //};

            //ExcelTableModel addressTable = new ExcelTableModel
            //{
            //    Title = "Address2",
            //    Headers = new List<string>()
            //    {
            //        "Address",
            //        "Address 2",
            //        "City",
            //        "State",
            //        "Zip Code"
            //    },
            //    Rows = new List<object>()
            //    {
            //        new { AddressLine1 = "1543 West st.", AddressLine2 = "apt #14", City = "Saginaw", State = "MI", Zipcode = "45234" },
            //        new { AddressLine1 = "6432 West st.", AddressLine2 = "apt #54", City = "Westtown", State = "IL", Zipcode = "56645" },
            //        new { AddressLine1 = "725 West st.", AddressLine2 = "apt #23", City = "Place", State = "OH", Zipcode = "23423" }
            //    }
            //};

            //ExcelTableModel CatTable = new ExcelTableModel()
            //{
            //    Title = "Cats",
            //    Headers = new List<string>()
            //    {
            //        "Name",
            //        "Age"
            //    },
            //    Rows = new List<object>()
            //    {
            //        new {Name = "Bobby", Age = 2},
            //        new {Name = "Sophie", Age = 4},
            //        new {Name = "Sue", Age = 2},
            //    }
            //};


            //// Create sheets
            //ExcelSheetModel AddressesSheet = new ExcelSheetModel
            //{
            //    Title = "Addresses",
            //    Tables = new List<ExcelTableModel>()
            //    {
            //        summaryTable,
            //        addressTable
            //    }
            //};

            //ExcelSheetModel CatsSheet = new ExcelSheetModel
            //{
            //    Title = "Cats",
            //    Tables = new List<ExcelTableModel>()
            //    {
            //        CatTable
            //    }
            //};

            //// Create file
            //ExcelFileModel excelFileModel = new ExcelFileModel
            //{
            //    FileName = "Report",
            //    Sheets = new List<ExcelSheetModel>()
            //    {
            //        AddressesSheet,
            //        CatsSheet
            //    }
            //};


            //ExcelExporter.ExportToExcel(excelFileModel);
        }
    }
}