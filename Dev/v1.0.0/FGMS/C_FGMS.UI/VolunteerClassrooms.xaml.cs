using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using C_FGMS.UI.Helpers;
using DocumentFormat.OpenXml.Spreadsheet;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Bson;
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

/**
 ************************************************************************************************************************
 *                                      File Name : VolunteerClassrooms.xaml.cs                                   *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Christopher Washburn                                                    *
 *                                      Date Created : 4/03/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 4/11/2023                                                         *
 *                                      Last Modified By : Christopher Washburn                                           *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the interaction logic for our                                  *
 * VolunteerClassrooms.xaml file.                                                                                 *
 ************************************************************************************************************************/

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for VolunteerClassrooms.xaml
    /// </summary>
    public partial class VolunteerClassrooms : System.Windows.Controls.Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly ISchoolProvider _schoolProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private bool errorFlag;

        public VolunteerClassrooms(IServiceProvider serviceProvider, IVolunteerProvider volunteerProvider, ISchoolProvider schoolProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = volunteerProvider;
            _schoolProvider = schoolProvider;
            _refreshEventBroker = refreshEventBroker;
            InitializeComponent();

            _volunteerProvider.DatabaseError += ErrorHandler;
            _schoolProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshData();
            });

            RefreshPage();
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

        #region Populate Page Logic

        /// <summary>
        /// Function Name: RefreshPage 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Calls populate functions to refresh data on page
        /// </summary>
        public void RefreshPage()
        {
            PopulateComboBoxes();
            PopulateDataGrid();
        }

        /// <summary>
        /// Function Name: RefreshData 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Calls populate function for data grid only to refresh its data
        /// </summary>
        public void RefreshData()
        {
            PopulateComboBoxes();
            PopulateDataGrid();
        }

        /// <summary>
        /// Function Name: PopulateComboBoxes 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Calls populate functions for Combo boxes to populate them with data
        /// </summary>
        public void PopulateComboBoxes()
        {
            PopulateVolunteers();
            PopulateSchools();
        }

        /// <summary>
        /// Function Name: PopulateVolunteers 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Populates Volunteers Combo box with Volunteer Names and Tuid's.
        /// </summary>
        public void PopulateVolunteers()
        {
            var Names = _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteerNameAndId();
            CmbSelectVolunteer.ItemsSource = Names;
        }

        /// <summary>
        /// Function Name: PopulateSchools 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Populates Schools Combo box with Schools names and info
        /// </summary>
        public void PopulateSchools()
        {
            var Schools = _serviceProvider.GetRequiredService<IVolunteerProvider>().GetAllSchools();
            CmbSelectSchool.ItemsSource = Schools;
        }

        /// <summary>
        /// Function Name: PopulateSelectedVolunteerClassrooms 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: This populates the Classrooms Data Grid baised on the selected Volunteer only.
        /// </summary>
        public void PopulateSelectedVolunteerClassrooms()
        {
            var ClassroomsDataGrid = _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteersClassroomsByVolunteer((int)CmbSelectVolunteer.SelectedValue);
            dtgClassrooms.ItemsSource = ClassroomsDataGrid;
        }

        /// <summary>
        /// Function Name: PopulateSelectedSchoolClassrooms 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: This populates the Classrooms Data Grid baised on the selected School only.
        /// </summary>
        public void PopulateSelectedSchoolClassrooms()
        {
            //populate on selected school
            var ClassroomsDataGrid = _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteersClassroomsBySchool((int)CmbSelectSchool.SelectedValue);
            dtgClassrooms.ItemsSource = ClassroomsDataGrid;
        }

        /// <summary>
        /// Function Name: PopulateSelectedSchoolVolunteerClassrooms 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: This populates the Classrooms Data Grid baised on the selected School and Volunteer.
        /// </summary>
        public void PopulateSelectedSchoolVolunteerClassrooms()
        {
            //populate with volunteer and school
            var ClassroomsDataGrid = _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteersClassroomsBySchoolVolunteer((int)CmbSelectSchool.SelectedValue, (int)CmbSelectVolunteer.SelectedValue);
            dtgClassrooms.ItemsSource = ClassroomsDataGrid;            
        }

        /// <summary>
        /// Function Name: PopulateAllClassrooms 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: This populates the Classrooms Data Grid with all classrooms.
        /// </summary>
        public void PopulateAllClassrooms()
        {
            //both are empty and populate all classrooms
            var ClassroomsDataGrid = _serviceProvider.GetRequiredService<IVolunteerProvider>().GetAllClassrooms();
            dtgClassrooms.ItemsSource = ClassroomsDataGrid;
        }

        /// <summary>
        /// Function Name: PopulateDataGrid 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Checks to see what kinds of population the data grid should get.
        /// </summary>
        public void PopulateDataGrid()
        {
            if (CmbSelectSchool.SelectedValue != null && CmbSelectVolunteer.SelectedValue != null)
            {
                PopulateSelectedSchoolVolunteerClassrooms();
            }
            else if(CmbSelectSchool.SelectedValue != null && CmbSelectVolunteer.SelectedValue == null)
            {
                PopulateSelectedSchoolClassrooms();
            }
            else if (CmbSelectSchool.SelectedValue == null && CmbSelectVolunteer.SelectedValue != null)
            {
                PopulateSelectedVolunteerClassrooms();
            }
            else
            {
                PopulateAllClassrooms();
            }
        }
        #endregion

        #region Button Click Logic
        /// <summary>
        /// Function Name: btnAdd_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Add Button click event to add a new classroom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddOrEditClassroom wndAddClassroom = new AddOrEditClassroom(_serviceProvider, (VolunteerNameIdModel)CmbSelectVolunteer.SelectedItem);
                wndAddClassroom.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                wndAddClassroom.ShowDialog();
                Growl.GrowlPanel = null;
                if (wndAddClassroom.DialogResult == true)
                {
                    GrowlHelpers.Success("Classroom Added!");
                }
                else
                {
                    GrowlHelpers.Error("Something went wrong while adding this classroom. Classroom not added.");
                }
                RefreshData();
            }
            catch(RefreshDataCustomException ex)
            {
                return;
            }
        }

        /// <summary>
        /// Function Name: btnAdd_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Edit Button click event to edit a classroom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgClassrooms.SelectedIndex != -1)
            {                
                AddOrEditClassroom wndAddClassroom = new AddOrEditClassroom(_serviceProvider, (ClassroomsModel)dtgClassrooms.SelectedItem);
                wndAddClassroom.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                wndAddClassroom.ShowDialog();
                bool? blnResult = wndAddClassroom.DialogResult;
                Growl.GrowlPanel = null;
                if (blnResult == true)
                {
                    GrowlHelpers.Success("Classroom Updated!");
                    RefreshData();
                }
                else
                {
                    GrowlHelpers.Error("Something went wrong, please try again. Contact support if issue persists");
                }
                
            }
            else
            {
                GrowlHelpers.Warning("Please select a classroom in the data grid to edit.");
            }
        }

        /// <summary>
        /// Function Name: btnRefresh_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Refresh Button click event to refresh all the data and combo boxes on the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

            _refreshEventBroker.Publish(nameof(VolunteerChildAssignments));
            GrowlHelpers.Success("Page Refreshed!");
        }

        /// <summary>
        /// Function Name: btnDelete_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Delete Button click event to delete a classroom.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {            
             if (dtgClassrooms.SelectedValue != null)
             {
                 string messageBoxText = "Are you sure you want to delete these Classroom(s)?";
                 string caption = "Confirm Delete";
                 MessageBoxButton button = MessageBoxButton.YesNo;
                 MessageBoxImage icon = MessageBoxImage.Warning;
                 MessageBoxResult result = MessageBoxResult.Yes;

                 if (result == System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes))
                 {
                    
                    int returnCode = _volunteerProvider.DeleteClassroom((ClassroomsModel)dtgClassrooms.SelectedItem);
                    if (errorFlag) { errorFlag = false; return; }

                    if (returnCode == 1)
                    {
                        Growl.Error("Error Deleting Classroom. Please contact system admin.");
                    }
                    else if(returnCode == 2)
                    {
                        Growl.Error("Cannot delete a classroom with an active Child Assignments. Remove Child Assignments to delete classroom");
                    }
                    else
                    {
                        GrowlHelpers.Success("Classroom Deleted!");
                    }
                    RefreshData();
                 }
             }
             else
             {
                 Growl.Error("Please select a Classroom before trying to delete a Classroom.");
             }           
        }

        /// <summary>
        /// Function Name: btnExport_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Export Button click to export the shown data in the data grid to excel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var volunteerName = (VolunteerNameIdModel)CmbSelectVolunteer.SelectedItem;
            var schoolName = (SchoolModel)CmbSelectSchool.SelectedItem;            

            ExcelFileModel excelFileModel = new ExcelFileModel();
            excelFileModel.FileName = "Classrooms";
            excelFileModel.Sheets = new List<ExcelSheetModel>();



            ExcelSheetModel excelSheetModel = new ExcelSheetModel()
            {
                Title = "Classrooms",
                Tables = new List<ExcelTableModel>()
            };

            excelFileModel.Sheets.Add(excelSheetModel);

            ExcelTableModel excelTableModel = new ExcelTableModel()
            {
                Title = "",
                Headers = new List<string>()
            };
            excelSheetModel.Tables.Add(excelTableModel);

            //create headers for Assignment General info
            excelTableModel.Headers.Add("Volunteer");
            excelTableModel.Headers.Add("School");
            excelTableModel.Headers.Add("Room Number");
            excelTableModel.Headers.Add("Class Size");
            excelTableModel.Headers.Add("Grade");
            excelTableModel.Headers.Add("Teacher");
            excelTableModel.Headers.Add("Days");
            excelTableModel.Headers.Add("Start Time");
            excelTableModel.Headers.Add("End Time");

            excelTableModel.Rows = new List<object>();
            
            //create rows for Classrooms Data Grid info
            foreach (var classroom in dtgClassrooms.Items.OfType<ClassroomsModel>().ToList())
            {
                object StudentAssignmentInfo = new
                {
                    Volunteer = classroom.Volunteer == null ? "" : classroom.Volunteer.FormattedName,
                    School = classroom.School == null ? "" : classroom.School.Name,
                    RoomNumber = classroom.ClassroomNumber,
                    ClassSize = classroom.ClassroomSize,
                    Grade = classroom.GradeLevel,
                    Teacher = classroom.TeacherName,
                    Days = classroom.Schedule.Day,
                    StartTime = classroom.Schedule.StartTime,
                    EndTime = classroom.Schedule.EndTime
                };

                excelTableModel.Rows.Add(StudentAssignmentInfo);
            }

            ExcelExporter.ExportToExcel(excelFileModel);            
        }
        #endregion

        #region ComboBox Selection Changed Logic   
        /// <summary>
        /// Function Name: cmbSelectVolunteer_SelectionChanged 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Selection Changed event for the CmbSelectVolunteer combo box to populate the data grid
        /// baised on the changing selection of volunteers. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectVolunteer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //makes the selected volunteer consistant across all tabs of the Volunteers section
            Application.Current.Properties["VolunteerTuid"] = CmbSelectVolunteer.SelectedIndex;

            PopulateDataGrid();
        }

        /// <summary>
        /// Function Name: CmbSelectVolunteer_Loaded
        /// Created By: Christopher Washburn
        /// Last Modified: 4/8/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles the on load method for the volunteer dropdown.
        /// Used to load a volunteer selected on another page to keep selected volunteer persistant across
        /// the volunteers area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbSelectVolunteer_Loaded(object sender, RoutedEventArgs e)
        {
            int? selectedVolunteer = (int?)Application.Current.Properties["VolunteerTuid"];

            if (selectedVolunteer.HasValue)
            {
                CmbSelectVolunteer.SelectedIndex = selectedVolunteer.Value;
            }
            else
            {
                CmbSelectVolunteer.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Function Name: CmbSelectSchool_SelectionChanged 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Selection Changed event for the CmbSelectSchool combo box to populate the data grid
        /// baised on the changing selection of schools. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbSelectSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateDataGrid();
        }
        #endregion

        #region Growl Logic        

        /// <summary>
        /// This method will show a growl warning with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlWarning(string strMessage)
        {
            Growl.Warning(new GrowlInfo
            {
                Message = strMessage,
                StaysOpen = false,
                ShowDateTime = false,
                WaitTime = 2

            });
        }
        /// <summary>
        /// This method will show a growl info with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlInfo(string strMessage)
        {
            Growl.Info(new GrowlInfo
            {
                Message = strMessage,
                StaysOpen = false,
                ShowDateTime = false,
                WaitTime = 2,
            });
        }
        #endregion
    }
}
