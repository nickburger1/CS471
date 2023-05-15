using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static C_FGMS.UI.ReportsAnnualCheckPage;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using HandyControl.Tools.Extension;
using System.Windows.Forms;
using System.Printing;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Models;
using Microsoft.VisualBasic.Logging;
using HandyControl.Controls;
using A_FGMS.DataLayer.EventBroker;
using C_FGMS.UI.Helpers;
using B_FGMS.BusinessLogic.Events;
using A_FGMS.DataLayer.Exceptions;

/// <summary>
/// The purpose of this file is to provide the interaction logic for the Reports - Annual Check page of the application. 
/// </summary>
/// <author>Jon Maddocks</author>
/// <created>2/14/2023</created>
namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for ReportsAnnualCheckPage.xaml
    /// </summary>
    /// 
    public partial class ReportsAnnualCheckPage : System.Windows.Controls.Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        List<AnnualChecksReportModel> VolunteersModelReport;
        private bool errorFlag;

        /// <summary>
        /// This function will run an instance of ReportsAnnualCheckPage.xaml.   
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>2/14/2023</created>
        public ReportsAnnualCheckPage(IServiceProvider serviceProvider, IVolunteerProvider volunteerProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = volunteerProvider;
            _refreshEventBroker = refreshEventBroker;
            VolunteersModelReport = new List<AnnualChecksReportModel>();
            InitializeComponent();

            _volunteerProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            _refreshEventBroker.Subscribe((args, x) =>
            {
                //there is no edit functionality on this page so we just call the refresh method
                RefreshData();
            });

            try
            {
                //call refresh data method
                RefreshData();
            }
            catch (RefreshDataCustomException ex)
            {

            }
        }

        /// <summary>
        /// Error provider for the UserServiceProvider. All functionality to handle
        /// business logic errors for the Users.xaml page are called in this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void ErrorHandler(object sender, ErrorEventArgs e)
        {
            errorFlag = true;
            System.Windows.MessageBox.Show(e.ErrorMessage, "Database Error " + e.ErrorCode, MessageBoxButton.OK, MessageBoxImage.Error);
        }


        /// <summary>
        /// This method will grab data from the database for volunteers annual checks and update the datagrid with the new information
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/7/2023</created>
        private void RefreshData()
        {
            InitDataGrid();
            UpdateGrid();
        }

        /// <summary>
        /// This Function will populate a list of AnnualChecksReportModel for every volunteer.
        /// </summary>
        /// <author>Tim Johnson</author>
        /// <created>3/20/2023</created>
        public void InitDataGrid()
        {
            try
            {
                VolunteersModelReport = (List<AnnualChecksReportModel>)_volunteerProvider.GetVolunteerAnnualCheck();
            }catch(RefreshDataCustomException ex)
            {

            }catch(Exception ex)
            {

            }
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
        }

        /// <summary>
        /// This function will go through each documentation property of a given item and return true if that item is missing 
        /// (false for booleans, null for dates)
        /// </summary>
        /// <param name="checkItem"></param>
        /// <returns>true -> documentation missing, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <Created>4/7/2023</Created>
        private bool IsMissingDocumentation(AnnualChecksReportModel checkItem)
        {
            //one time checks
            if (checkItem.FilePhoto == false)
                return true;
            if (checkItem.ServiceDescription == false)
                return true;
            if (checkItem.OrientationTraining == false)
                return true;
            if (checkItem.BackgroundCheck == false)
                return true;
            if (checkItem.IDCopy == false)
                return true;
            if (checkItem.NSCHC == false)
                return true;
            if (checkItem.ConfidSOU == null)
                return true;
            if (checkItem.ServiceStartDate == null)
                return true;
            if (checkItem.NSOPW == null)
                return true;
            if (checkItem.IChat == null)
                return true;
            if (checkItem.AliasFPrint == null)
                return true;
            if (checkItem.DHS == null)
                return true;
            if (checkItem.TBShot == null)
                return true;
            if (checkItem.TrueScreen == null)
                return true;
            //annual checks
            if (checkItem.SchedulePhotoRelease == null)
                return true;
            if (checkItem.EmergancyBeneficiaryForm == null)
                return true;
            if (checkItem.HippaRelease == null)
                return true;
            if (checkItem.Physical == null)
                return true;
            if (checkItem.AnnualIncomeCarInsurance == null)
                return true;
            //if we get here this item has all checks, return false
            return false;
        }

        /// <summary>
        /// this method will go through each annual check, and filter based on if the user has current or missing documentation selected. 
        /// </summary>
        /// <returns>a List of annualChecksReportModel that contains annual checks filtered on the current/missing criteria</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/7/2023</created>
        private List<AnnualChecksReportModel> GetAnnualChecksFiltered()
        {
            List<AnnualChecksReportModel> filteredAnnualChecks = new List<AnnualChecksReportModel>();
            bool current = tglCurrent.IsChecked ?? false;
            bool missing = tglMissing.IsChecked ?? false;
            foreach (var item in VolunteersModelReport)
            {
                if (current)
                {
                    if (item.IsDeleted == true || item.SeparatedTime != null) 
                        continue;
                }

                if (missing)
                {
                    if(!IsMissingDocumentation(item))
                        continue;
                }

                filteredAnnualChecks.Add(item);
            }
            return filteredAnnualChecks;
        }

        /// <summary>
        /// This Function will populate the Datagrid with filtered information from the togglebuttons.
        /// </summary>
        /// <author>Tim Johnson</author>
        /// <created>3/20/2023</created>
        private void UpdateGrid()
        {
            if (VolunteersModelReport == null) return;
            dtgAnnualCheck.ItemsSource = GetAnnualChecksFiltered();
        }

        /// <summary>
        /// This Function will be done when clicked by the export button. this will export the datagrid to excel
        /// </summary>
        /// <author>Anthony Chippi</author>
        /// <created>3/21/2023</created>
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

            // Create tables
            ExcelTableModel summaryTable = new ExcelTableModel()
            {
                Title = "Annual Check",
                Headers = new List<string>()
                {
                    "Volunteer Name",
                    "Schedule and Photo Release",
                    "Emergency Beneficiary Form",
                    "HIPPA Release",
                    "Physical",
                    "Ann. Inc. Car Ins.",
                    "File Photo",
                    "Service Description",
                    "Orientation Training",
                    "Confid SOU",
                    "Service Start Date",
                    "NSCHC Checked Form",
                    "Background Check",
                    "ID Copy",
                    "NSOPW",
                    "I-Chat",
                    "Alias F'Print",
                    "Field-Print Cleared",
                    "DHS",
                    "TB Shot",
                    "True-Screen"

                },
                Rows = new List<object>()



            };


            if (dtgAnnualCheck.Items.Count <= 0)
            {
                Growl.Warning("There are no volunteers in the table");
                return;
            }
                foreach (AnnualChecksReportModel item in dtgAnnualCheck.Items)
                {
                    summaryTable.Rows.Add(new
                    {
                        Volunteer = item.Name,
                        Schdule = ((DateTime?)item.SchedulePhotoRelease)?.ToString("MM/dd/yyyy") ?? "N/A",
                        Emergency = ((DateTime?)item.EmergancyBeneficiaryForm)?.ToString("MM/dd/yyyy") ?? "N/A",
                        HIPPA = ((DateTime?)item.HippaRelease)?.ToString("MM/dd/yyyy") ?? "N/A",
                        Physical = ((DateTime?)item.Physical)?.ToString("MM/dd/yyyy") ?? "N/A",
                        Annual = ((DateTime?)item.AnnualIncomeCarInsurance)?.ToString("MM/dd/yyyy") ?? "N/A",
                        File = item.FilePhoto,
                        Service = item.ServiceDescription,
                        Orientation = item.OrientationTraining,
                        Confid = ((DateTime?)item.ConfidSOU)?.ToString("MM/dd/yyyy") ?? "N/A",
                        startDate = ((DateTime?)item.ServiceStartDate)?.ToString("MM/dd/yyyy") ?? "N/A",
                        NSCHCCheck = item.NSCHC,
                        Background = item.BackgroundCheck,
                        ID = item.IDCopy,
                        NSOPW = ((DateTime?)item.NSOPW)?.ToString("MM/dd/yyyy") ?? "N/A",
                        IChat = ((DateTime?)item.IChat)?.ToString("MM/dd/yyyy") ?? "N/A",
                        AliasFPrint = ((DateTime?)item.AliasFPrint)?.ToString("MM/dd/yyyy") ?? "N/A",
                        FieldPrintClear = ((DateTime?)item.FieldPrintCleared)?.ToString("MM/dd/yyyy") ?? "N/A",
                        DHS = ((DateTime?)item.DHS)?.ToString("MM/dd/yyyy") ?? "N/A",
                        TBShot = ((DateTime?)item.TBShot)?.ToString("MM/dd/yyyy") ?? "N/A",
                        TrueScreen = ((DateTime?)item.TrueScreen)?.ToString("MM/dd/yyyy") ?? "N/A"

                    }); ;

                }
            
            

          
            // Create sheets
            ExcelSheetModel annualCheckSheet = new ExcelSheetModel
            {
                Title = "Annual Check Report",
                Tables = new List<ExcelTableModel>()
                {
                    summaryTable,
                }
            };

            // Create file
            ExcelFileModel annualCheckExcel = new ExcelFileModel
            {
                FileName = "Annual Check Report",
                Sheets = new List<ExcelSheetModel>()
                {
                    annualCheckSheet,
                }
            };


            ExcelExporter.ExportToExcel(annualCheckExcel);
        }

        /// <summary>
        /// This Function will call UpdateGrid. It is called when a Togglebutton is toggled.
        /// </summary>
        /// <author>Tim Johnson</author>
        /// <created>3/20/2023</created>
        private void eUpdateGrid(object sender, EventArgs e) { 
            UpdateGrid();
        }

        /// <summary>
        /// This Function fires when you click on a row, and will toggle expand/collapse the row details.
        /// </summary>
        /// <author>Tim Johnson</author>
        /// <created>3/20/2023</created>
        private void rowClicked(object sender, MouseButtonEventArgs e)
        {
            DataGridRow? row = sender as DataGridRow;

            if (row != null)
            {
                if (row.DetailsVisibility == Visibility.Collapsed)
                {
                    row.DetailsVisibility = Visibility.Visible;
                }
                else
                {
                    row.DetailsVisibility = Visibility.Collapsed;
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(ReportsAnnualCheckPage));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
    }
}
