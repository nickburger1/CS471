using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using A_FGMS.DataLayer.EventBroker;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using Microsoft.Extensions.DependencyInjection;

/**
 ************************************************************************************************************************
 *                                      File Name : VolunteerChildAssignments.xaml.cs                                   *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Isabelle Johns                                                     *
 *                                      Date Created : 1/22/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 4/11/2023                                                         *
 *                                      Last Modified By : Christopher Washburn                                           *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the interaction logic for our                                  *
 * VolunteerChildAssignments.xaml file.                                                                                 *
 ************************************************************************************************************************
 **/

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for VolunteerChildAssignments.xaml
    /// </summary>
    public partial class VolunteerChildAssignments : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private bool errorFlag;
        //private bool _showConditionExtras;
        //private bool _showNeedExtras;
        private int volunteerSchool;                
        public VolunteerChildAssignments(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _refreshEventBroker = refreshEventBroker;
            InitializeComponent();

            _volunteerProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshData();
            });

            PopulateVolunteerDropDown();                                   
        }

        #region Error Handler
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

        #endregion

        #region Button Click Logic        
        /// <summary>
        /// Handles OnClick event for Editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgStudent.SelectedIndex != -1)
            {
                EditChildAssignmentGrid wndEditChildAssignment = new EditChildAssignmentGrid(_serviceProvider, (int)CmbSelectVolunteer.SelectedValue, (VolunteerChildAssignmentDataGridModel)dtgStudent.SelectedItem, volunteerSchool);
                wndEditChildAssignment.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                wndEditChildAssignment.ShowDialog();
                Growl.GrowlPanel = null;
                bool? result = wndEditChildAssignment.DialogResult;
                if (result == true)
                {
                    PopulateDataGrid();
                    GrowlHelpers.Success("Successfully updated child assignment");
                }
                else
                {
                    GrowlHelpers.Info("Canceled updating child assignment");
                }
                
            }
            else
            {
                ShowGrowlWarning("Please select a Child Assignment in the data grid to edit.");
            }

        }
       
        /// <summary>
        /// Handles OnClick event for Deleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Christopher Washburn </author>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CmbSelectVolunteer.SelectedValue != null)
            {
                if(dtgStudent.SelectedValue != null)
                {
                    string messageBoxText = "Are you sure you want to delete these Assignment(s)?";
                    string caption = "Confirm Delete";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBoxResult.Yes;

                    if (result == System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes))
                    {
                        List<VolunteerChildAssignmentDataGridModel> childAssignment = new List<VolunteerChildAssignmentDataGridModel>();

                        foreach (var item in dtgStudent.SelectedItems)
                        {
                            childAssignment.Add((VolunteerChildAssignmentDataGridModel)item);
                        }

                        //Delete Child Assignments
                        _volunteerProvider.DeleteChildAssignments(childAssignment);
                        if (errorFlag) { errorFlag = false; return; }

                        RefreshData();
                        ShowGrowlSuccess("Child Assignment Deleted!");
                    }
                }
                else
                {
                    ShowGrowlWarning("Please select a Assignment before trying to delete an Assignment.");
                }                
            }
            else
            {
                ShowGrowlWarning("Please select a Volunteer before deleting an assignment.");
            }                      
        }

        /// <summary>
        /// Function Name: btnAddChildAssignment_Click
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles the add click event by making sure there is a volunteer selected then opens
        /// the AddChildAssginment Window to allow the user to add an assignment 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddChildAssignment_Click(object sender, RoutedEventArgs e)
        {
            if(CmbSelectVolunteer.SelectedValue != null)
            {
                if((string)CmbSelectRoom.SelectedValue != "")
                {
                    if (_volunteerProvider.GetVolunteerSchool((int)CmbSelectVolunteer.SelectedValue) != null)
                    {
                        if (errorFlag) { errorFlag = false; return; }
                        AddNewChildAssignment wndChildAssignment = new AddNewChildAssignment(_serviceProvider, (int)CmbSelectVolunteer.SelectedValue, volunteerSchool);
                        wndChildAssignment.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        wndChildAssignment.ShowDialog();
                        if (wndChildAssignment.DialogResult == true)
                        {
                            Growl.GrowlPanel = null;
                            ShowGrowlSuccess("Child Assignment/Condtion/Need Added");
                        }
                        RefreshData();
                    }
                    else
                    {
                        if (errorFlag) { errorFlag = false; return; }
                        ShowGrowlWarning("Volunteer must first be assigned to a school before children can be assigned.");
                    }
                }
                else
                {
                    ShowGrowlInfo("Please enter classroom room number in classrooms before creating assignment.");
                }             
            }
            else
            {
                ShowGrowlInfo("Please select a Volunteer before adding an assignment.");
            }
        }

        /// <summary>
        /// Function Name: btnExportChildAssignment_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles the click event for the Export button. Creates and Exports excel sheet of data
        /// currently displayed on the page        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportChildAssignment_Click(object sender, RoutedEventArgs e)
        {
            if (CmbSelectVolunteer.SelectedValue != null)
            {
                var volnteerName = (VolunteerNameIdModel)CmbSelectVolunteer.SelectedItem;
                //var schoolName = (SchoolModel)CmbSelectSchool.SelectedItem;

                ExcelFileModel excelFileModel = new ExcelFileModel();
                excelFileModel.FileName = "Volunteer Child Assignments";
                excelFileModel.Sheets = new List<ExcelSheetModel>();

                ExcelSheetModel excelSheetModel = new ExcelSheetModel()
                {
                    Title = volnteerName.FullName ?? "null",
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
                excelTableModel.Headers.Add("Volunteer Name");
                excelTableModel.Headers.Add("School");
                excelTableModel.Headers.Add("Classroom Size");
                excelTableModel.Headers.Add("Kids Assigned");
                excelTableModel.Headers.Add("Grade Level");
                excelTableModel.Headers.Add("Ages 0 To 5");
                excelTableModel.Headers.Add("Ages 6 to 12");

                excelTableModel.Rows = new List<object>();

                //create rows for Assignment General info
                object GeneralAssignmentInfo = new
                {
                    VolunteerName = volnteerName.FullName,
                    //School = txtSchool.Text,
                    ClassroomSize = txtClassroomSize.Text,
                    KidsAssigned = txtKidsAssigned.Text,
                    GradeLevel = txtGradeLevel.Text,
                    Age0to5 = txtAge0to5.Text,
                    Ages6to12 = txtAge6to12.Text
                };
                excelTableModel.Rows.Add(GeneralAssignmentInfo);

                //create empty row for spacing
                object rowbreak = new
                {
                    break1 = "",
                    break2 = "",
                    break3 = "",
                    break4 = "",
                    break5 = "",
                    break6 = "",
                    break7 = ""
                };
                excelTableModel.Rows.Add(rowbreak);

                //create headers for Assignment Data Grid info
                object StudentAssignmentInfoHeaders = new
                {
                    IdentifierHeader = "Identifier",
                    ConditionHeader = "Condition",
                    ConditionDescriptionHeader = "Condition Description",
                    StudentNeedsHeader = "Student Needs",
                    StudentNeedsDescriptionHeader = "Student Needs Description",
                    DesiredOutcomeHeader = "Desired Outcome"
                };
                excelTableModel.Rows.Add(StudentAssignmentInfoHeaders);

                ////create rows for Assignment Data Grid info
                foreach (var item in dtgStudent.Items.OfType<VolunteerChildAssignmentDataGridModel>().ToList())
                {
                    object StudentAssignmentInfo = new
                    {
                        Identifier = item.Identifier,
                        Condition = item.Condition,
                        ConditionDescription = item.ConditionDescription,
                        StudentNeeds = item.StudentNeeds,
                        StudentNeedsDescription = item.StudentNeedsDescription,
                        DesiredOutcome = item.DesiredOutcome,
                    };

                    excelTableModel.Rows.Add(StudentAssignmentInfo);
                }

                ExcelExporter.ExportToExcel(excelFileModel);
            }
            else
            {
                Growl.Error("Please select a Volunteer before exporting");
            }            
        }

        /// <summary>
        /// Function Name: btnRefresh_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Refresh Button click event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(VolunteerChildAssignments));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
        #endregion

        #region Selection Changed and Loaded Logic
        /// <summary>
        /// Function Name: cmbSelectVolunteer_SelectionChanged
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Refreshes the data with the data of the selected volunteer from the volunteer combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectVolunteer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Application.Current.Properties["VolunteerTuid"] = CmbSelectVolunteer.SelectedIndex;

            if (CmbSelectVolunteer.SelectedValue != null)
            {
                RefreshPage();
            }
            else
            {
                ClearPage();
            }
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


        private void CmbSelectRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();          
        }

        /// <summary>
        /// Function Name: dtgStudent_SelectionChanged 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles when the selection of the item in the Data Grid. If the page is in edit mode
        /// it opens the edit window for the selection row in the data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnEdit.Visibility == Visibility.Hidden)
            {
                if (dtgStudent.SelectedIndex != -1)
                {
                    EditChildAssignmentGrid wndEditChildAssignment = new EditChildAssignmentGrid(_serviceProvider, (int)CmbSelectVolunteer.SelectedValue, (VolunteerChildAssignmentDataGridModel)dtgStudent.SelectedItem, volunteerSchool);
                    wndEditChildAssignment.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    wndEditChildAssignment.ShowDialog();
                    PopulateDataGrid();
                }
            }
        }
        #endregion

        #region Populate Page Logic

        /// <summary>
        /// Function Name: PopulateVolunteerDropDown
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Fills the Volunteer combo box with the volunteers name as the displayed value and 
        /// their Tuid in the database as the selected value
        /// </summary>
        public void PopulateVolunteerDropDown()
        {
            var Names = _serviceProvider.GetRequiredService<IVolunteerProvider>().GetVolunteerNameAndId();
            CmbSelectVolunteer.ItemsSource = Names;
        }

        public void PopulateRoomNumber()
        {
            if(CmbSelectVolunteer.SelectedValue != null)
            {
                var classrooms = _volunteerProvider.GetVolunteersClassrooms((int)CmbSelectVolunteer.SelectedValue);
                if (errorFlag) { errorFlag = false; return; }
                CmbSelectRoom.ItemsSource = classrooms;
            }
        }

        /// <summary>
        /// Function Name: PopulateTextBoxes
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Populates the side card with its data and the schools dropdown with the school name as the 
        /// displayed value and their Tuid in the database as the selected value
        /// </summary>
        public void PopulateTextBoxes()
        {
            if(CmbSelectRoom.SelectedValue == null) 
            {
                if(CmbSelectVolunteer.SelectedValue != null){
                    var childAssignments = _volunteerProvider.GetAllVolunteerChildAssignments((int)CmbSelectVolunteer.SelectedValue);
                    if (errorFlag) { errorFlag = false; return; }
                    //call a get school method to get school
                    txtClassroomSize.Text = "N/A";
                    txtGradeLevel.Text = "N/A";
                    volunteerSchool = childAssignments.SchoolTuid;
                    grdMain.DataContext = childAssignments;
                }
                
            }
            else
            {
                //pass room number to get kids in room
                var childAssignments = _volunteerProvider.GetVolunteerChildAssignments((int)CmbSelectVolunteer.SelectedValue, (ClassroomsModel)CmbSelectRoom.SelectedItem);
                if (errorFlag) { errorFlag = false; return; }
                volunteerSchool = childAssignments.SchoolTuid;
                grdMain.DataContext = childAssignments;                
            }

                      
        }

        /// <summary>
        /// Function Name: PopulateDataGrid 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Populates the datagrid with the selected volunteers assigned students information
        /// </summary>
        public void PopulateDataGrid()
        {
            if(CmbSelectVolunteer.SelectedValue != null)
            {
                int intSelectedValue = (int)CmbSelectVolunteer.SelectedValue;

                IEnumerable<VolunteerChildAssignmentDataGridModel> childAssignmentsGrid;

                if (CmbSelectVolunteer.SelectedValue != null)
                {
                    if (CmbSelectRoom.SelectedValue == null)
                    {
                        //get all
                        childAssignmentsGrid = _volunteerProvider.GetVolunteerChildAssignmentDataGrid(intSelectedValue);
                        if (errorFlag) { errorFlag = false; return; }
                    }
                    else
                    {
                        //get selected room
                        string strSelectedRoom = CmbSelectRoom.SelectedValue.ToString();
                        childAssignmentsGrid = _volunteerProvider.GetVolunteerChildAssignmentInRoom(intSelectedValue, volunteerSchool, (ClassroomsModel)CmbSelectRoom.SelectedItem);
                        if (errorFlag) { errorFlag = false; return; }
                    }

                    dtgStudent.ItemsSource = childAssignmentsGrid;

                }
                else
                {
                    ShowGrowlInfo("Please select a volunteer.");
                }
            }                                                           
        }

        /// <summary>
        /// Function Name: RefreshPage 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Refreshes the data on the page by calling PopulateTextBoxes() and PopulateDataGrid()
        /// </summary>
        private void RefreshPage()
        {                        
            PopulateTextBoxes();
            PopulateRoomNumber();
            PopulateDataGrid();
        }

        /// <summary>
        /// Function Name: RefreshData 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Calls the populate functions to refresh data on the page
        /// </summary>
        public void RefreshData()
        {
            PopulateTextBoxes();            
            PopulateDataGrid();
        }

        /// <summary>
        /// Function Name: RefreshData 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Clears the page of data 
        /// </summary>
        private void ClearPage()
        {
            txtSchool.Text = "";
            txtClassroomSize.Text = "";
            txtKidsAssigned.Text = "";
            txtGradeLevel.Text = "";
            txtAge0to5.Text = "";
            txtAge6to12.Text = "";
            dtgStudent.ItemsSource = null;
            CmbSelectRoom.ItemsSource = null;

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
                WaitTime = 3

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
                WaitTime = 3
            });
        }

        /// <summary>
        /// This method will show a growl info with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlSuccess(string strMessage)
        {
            Growl.Success(new GrowlInfo
            {
                Message = strMessage,
                StaysOpen = false,
                ShowDateTime = false,
                WaitTime = 3
            });
        }

        #endregion
    }
}
