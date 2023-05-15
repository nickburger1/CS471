using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

/// <FileName> ExcelExporter.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 03/14/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/14/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this class is to provide utilities for exporting to excel.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.BusinessLogicObjects
{
    public class ExcelExporter
    {
        /// <summary>
        /// Export multiple data sets to excel.
        /// </summary>
        /// <param name="exportData">Data to be parsed and exported.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        public static bool ExportToExcel(ExcelFileModel exportData)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.Filter = "Excel |*.xlsx";
            svd.ShowDialog();
            string filePath = svd.FileName;
            //the showFileDialog handles everything we need as far as making sure the file name is not a duplicate path and handling for renaming/replacing.
            //just check that the path is not empty
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }
            try
            {
                var spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

                var workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                if (workbookPart.Workbook.Sheets == null )
                {
                    workbookPart.Workbook.AppendChild(new Sheets());
                }

                var sheets = workbookPart.Workbook.Sheets;

                UInt32Value sheetNumber = 1;

                foreach (ExcelSheetModel sheetModel in exportData.Sheets)
                {
                    var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                    var sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = sheetNumber, Name = sheetModel.Title };
                    sheets.Append(sheet);

                    var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    GenerateTables(sheetModel.Tables, sheetData);
                    sheetNumber++;
                }
                workbookPart.Workbook.Save();
                spreadsheetDocument.Close();

                OpenFile(filePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Opens the file in the default program
        /// </summary>
        /// <param name="filePath">Path of desired file.</param>
        private static void OpenFile(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(filePath);
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
        }

        /// <summary>
        /// Export single dataset to excel.
        /// </summary>
        /// <param name="data">IEnumerable data.</param>
        /// <param name="fileName">Name of the file.</>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        public static void ExportToExcel(IEnumerable<object> data, string fileName)
        {
            List<IEnumerable<object>> list = new()
            {
                data
            };

            ExportToExcel(list, fileName);
        }

        /// <summary>
        /// Loope through list of enumerables and generate tables.
        /// </summary>
        /// <param name="tables">List of Excel table object.</param>
        /// <param name="sheetData">Current excel sheet.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        private static void GenerateTables(IEnumerable<ExcelTableModel> tables, SheetData? sheetData)
        {
            foreach (ExcelTableModel table in tables)
            {
                GenerateTable(table, sheetData);

                var blankRow = new Row();
                sheetData.AppendChild(blankRow);
            }

        }
        /// <summary>
        /// Create a single table.
        /// </summary>
        /// <param name="excelTableModel">Table object.</param>
        /// <param name="sheetData">Destination sheet.</param>
        private static void GenerateTable(ExcelTableModel excelTableModel, SheetData? sheetData)
        {
            AddTitle(excelTableModel.Title, sheetData);
            AddHeaders(excelTableModel.Headers, sheetData);
            AddRows(excelTableModel.Rows, sheetData);
        }

        /// <summary>
        /// Add title to sheet section.
        /// </summary>
        /// <param name="tableTitle">String title of the table.</param>
        /// <param name="sheetData">Sheet to append to.</param>
        private static void AddTitle(string tableTitle, SheetData? sheetData)
        {
            // Add headers to the first row of the sheet
            var titleRow = new Row();
            var blankRow = new Row();
            
            titleRow.AppendChild(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(tableTitle),
            });

            sheetData.AppendChild(titleRow);
            sheetData.AppendChild(blankRow);
        }

        /// <summary>
        /// Get file path to downloads folder.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        /// <returns>String path to the file.</returns>
        private static string GetDownloadFolderPath(string fileName)
        {
            var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";
            return Path.Combine(downloadsPath, fileName + ".xlsx");
        }

        /// <summary>
        /// Add headers to excel sheet.
        /// </summary>
        /// <param name="headers">String list of headers for columns.</param>
        /// <param name="sheetData"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        /// <modified>
        ///     <change>Andrew Loesel - 3/27/2023 : adding columns into the sheet, when there are multiple tables
        ///         on a sheet the number of columns that are added will be the number of columns in the largest table
        ///     </change>
        /// </modified>
        private static void AddHeaders(List<string> headers, SheetData? sheetData)
        {
            if(sheetData == null)
            {
                return;
            }
            //get the columns for this sheet
            DocumentFormat.OpenXml.Spreadsheet.Columns? columns =  sheetData.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Columns>();
            bool blnAddColumns = false;
            if (columns == null)
            {
                //we need to add columns in later, set column to an empty Columns object for now
                columns = new DocumentFormat.OpenXml.Spreadsheet.Columns();
                blnAddColumns = true;
            }
            // Add headers to the first row of the sheet
            var headerRow = new Row();
            UInt32Value index = 1;
            foreach (var header in headers)
            {
                headerRow.AppendChild(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(header)
                });
                //add a column with a larger width than default
                columns.Append(new DocumentFormat.OpenXml.Spreadsheet.Column { Min = index, Max = index, Width = 18, CustomWidth = true, BestFit = true });
                index++;
            }
            if (blnAddColumns)
            {
                if(sheetData.Parent != null)
                {
                    sheetData.Parent.InsertAt(columns, 0);
                }
            }
            sheetData.AppendChild(headerRow);
        }

        /// <summary>
        /// Add data to excel sheet.
        /// </summary>
        /// <param name="rows">List of objects representing table rows.</param>
        /// <param name="sheetData">Sheet to append to.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        private static void AddRows(List<object> rows, SheetData? sheetData)
        {
            // Add data to the sheet
            foreach (var row in rows)
            {
                var dataRow = new Row();
                AddCells(row, dataRow);

                sheetData.AppendChild(dataRow);
            }
        }

        /// <summary>
        /// Add each cell to a row.
        /// </summary>
        /// <param name="row">Current row.</param>
        /// <param name="dataRow">Row being appended to.</param>
        /// <Modified> Brendan Breuss 3/29/2023 </modified>
        private static void AddCells(object row, Row dataRow)
        {
            if(row.GetType().Name.Equals("ExpandoObject"))
            {
                //here we are adding from a dynamic object, so we get it's properties as a dictionary
                IDictionary<string, object>? dynamicDictionary = row as IDictionary<string, object>;
                if (dynamicDictionary != null)
                {
                    //loop through each property
                    foreach (KeyValuePair<string, object> kvp in dynamicDictionary)
                    {
                        if(kvp.Value != null)
                        {
                            string? strValue = kvp.Value.ToString();
                            if (!string.IsNullOrEmpty(strValue))
                            {
                                var cell = new Cell
                                {
                                    CellValue = new CellValue(strValue),
                                    
                                };
                                double dblValue;
                                if(double.TryParse(strValue, out dblValue))
                                {
                                    cell.DataType = CellValues.Number;
                                }
                                else
                                {
                                    cell.DataType = kvp.Value.GetType() == typeof(int) ? CellValues.Number : CellValues.String;
                                }
                                
                                dataRow.AppendChild(cell);
                            }
                            else
                            {
                                //if the value is null or empty we still have to add a cell to keep all the columns in order.
                                var cell = new Cell
                                {
                                    CellValue = new CellValue("null")
                                };
                                cell.DataType = "null".GetType() == typeof(int) ? CellValues.Number : CellValues.String;
                                dataRow.AppendChild(cell);
                            }
                        }
                        else
                        {
                            //if the value is null or empty we still have to add a cell to keep all the columns in order.
                            var cell = new Cell
                            {
                                CellValue = new CellValue("null")
                            };
                            cell.DataType = "null".GetType() == typeof(int) ? CellValues.Number : CellValues.String;
                            dataRow.AppendChild(cell);
                        }
                        
                        
                    }
                }
            }
            else
            {
                
                foreach (var prop in row.GetType().GetProperties())
                {
                    var cell = new Cell
                    {
                        CellValue = new CellValue(prop.GetValue(row)?.ToString())
                    };

                    //cell.DataType = prop.PropertyType == typeof(int)? CellValues.Number : CellValues.String;

                    
                    if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(double) || prop.PropertyType == typeof(decimal))
                    {
                        cell.DataType = CellValues.Number;
                    }
                    else
                    {
                        cell.DataType = CellValues.String;
                    }
                    

                dataRow.AppendChild(cell);
                }
            }
        }
    }
}
