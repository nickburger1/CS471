using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using C_FGMS.UI.Helpers;
using CloudinaryDotNet.Actions;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Word;
using B_FGMS.BusinessLogic.Events;
using A_FGMS.DataLayer.Exceptions;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for AddOrEditClassroom.xaml
    /// </summary>
    public partial class AddOrEditClassroom : System.Windows.Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private ClassroomsModel _editClassroom;
        private VolunteerNameIdModel _volunteerNameIdModel;
        private bool _isEdit;
        private bool errorFlag;

        /// <summary>
        /// Function Name: AddOrEditClassroom 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: This function builds the AddOrEditClassrooms window. This function is specifically
        /// for the Add function
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="volunteerNameIdModel"></param>
        public AddOrEditClassroom(IServiceProvider serviceProvider, VolunteerNameIdModel volunteerNameIdModel)
        {
            _isEdit = false;
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _volunteerProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            InitializeComponent();
            PopulateVolunteers();

            ShowTimePickers();

            //Hide edit time slider as it is not needed on an add
            tglEditTime.Visibility = Visibility.Hidden;
            txtEditTime.Visibility = Visibility.Hidden;
            _volunteerNameIdModel = volunteerNameIdModel;
        }

        /// <summary>
        /// Function Name: AddOrEditClassroom 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: This function builds the AddOrEditClassrooms window. This function is specifically
        /// for the Add function
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="classroom"></param>
        public AddOrEditClassroom(IServiceProvider serviceProvider, ClassroomsModel classroom)
        {
            _isEdit = true;
            _editClassroom = classroom;
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();

            _volunteerProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            InitializeComponent();
            PopulateVolunteers();
            
            
            txtRoomNumber.Text = classroom.ClassroomNumber;
            txtClassSize.Text = classroom.ClassroomSize.ToString();
            txtGrade.Text = classroom.GradeLevel;
            txtTeacherName.Text = classroom.TeacherName;
            txtStartTime.Text = classroom.Schedule.StartTime;
            txtEndTime.Text = classroom.Schedule.EndTime;
            SetDays(classroom);
                        

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
        /// Function Name: CmbAddEditSelectVolunteer_Loaded 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: When a volunteer is selected on the parent page auto selects that selected volunteer
        /// to the CmbAddEditSelectVolunteer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAddEditSelectVolunteer_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isEdit)
            {
                for (int i = 0; i < CmbAddEditSelectVolunteer.Items.Count; i++)
                {
                    VolunteerNameIdModel volunteer = (VolunteerNameIdModel)CmbAddEditSelectVolunteer.Items[i];
                    if (volunteer.Tuid == _editClassroom.Volunteer.Tuid)
                    {
                        CmbAddEditSelectVolunteer.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (_volunteerNameIdModel != null)
            {
                for (int i = 0; i < CmbAddEditSelectVolunteer.Items.Count; i++)
                {
                    VolunteerNameIdModel volunteer = (VolunteerNameIdModel)CmbAddEditSelectVolunteer.Items[i];
                    if (volunteer.Tuid == _volunteerNameIdModel.Tuid)
                    {
                        CmbAddEditSelectVolunteer.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Function Name: PopulateVolunteers 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Populates the Volunteers drop down with volunteers
        /// </summary>
        public void PopulateVolunteers()
        {
            var Names = _volunteerProvider.GetVolunteerNameAndId();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            CmbAddEditSelectVolunteer.ItemsSource = Names;
        }

        private bool IsDataValid()
        {
            bool isValid = true;
            if(CmbAddEditSelectVolunteer.SelectedItem == null)
            {
                GrowlHelpers.Warning("Please select a Volunteer to update this classroom");
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtRoomNumber.Text))
            {
                GrowlHelpers.Warning("Please enter a Room Number to update this classroom");
                isValid = false;
            }
            if (!int.TryParse(txtClassSize.Text, out int intSize) || intSize <= 0)
            {
                GrowlHelpers.Warning("Classroom size must be a positive whole number");
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtGrade.Text))
            {
                GrowlHelpers.Warning("Please enter a Grade to update this classroom");
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtTeacherName.Text))
            {
                GrowlHelpers.Warning("Please enter the teacher of this classroom");
                isValid = false;
            }
            if (string.IsNullOrEmpty(getDays()))
            {
                GrowlHelpers.Warning("Please select days for this assignment");
                isValid = false;
            }
            var selectedStartTime = tpStart.Text;
            var selecteEndTime = tpEnd.Text;
            if (tglEditTime.IsChecked == true || _isEdit == false) 
            {
                if (string.IsNullOrEmpty(selectedStartTime))
                {
                    GrowlHelpers.Warning("Please select or type in a start time");
                    isValid = false;
                }
                if (string.IsNullOrEmpty(selecteEndTime))
                {
                    GrowlHelpers.Warning("Please select or type in an end time");
                    isValid = false;
                }
            }
            
            return isValid;
        }

        /// <summary>
        /// Function Name: btnSave_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the btnSave Click event for saving a Edit or Addition of a Classroom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_isEdit)
            {
                //Logic for saving and edited classroom
                if (IsDataValid())
                {
                    ClassroomsModel updateClassroom = new ClassroomsModel();
                    updateClassroom.AssignmentTuid = _editClassroom.AssignmentTuid;
                    updateClassroom.Volunteer = (VolunteerNameIdModel)CmbAddEditSelectVolunteer.SelectedItem;
                    updateClassroom.ClassroomTuid = _editClassroom.ClassroomTuid;                    
                    updateClassroom.ClassroomNumber = txtRoomNumber.Text;
                    updateClassroom.ClassroomSize = Int32.Parse(txtClassSize.Text);
                    updateClassroom.GradeLevel = txtGrade.Text;                                      
                    updateClassroom.TeacherName = txtTeacherName.Text;

                    //Get schedule and parse to JSON
                    updateClassroom.Schedule = new VolunteerClassroomSchedule();
                    updateClassroom.Schedule.Day = getDays();

                    if (tglEditTime.IsChecked == true)
                    {
                        updateClassroom.Schedule.StartTime = tpStart.ToString();
                        updateClassroom.Schedule.EndTime = tpEnd.ToString();
                    }
                    else
                    {
                        updateClassroom.Schedule.StartTime = txtStartTime.Text;
                        updateClassroom.Schedule.EndTime = txtEndTime.Text;
                    }
                    
                    updateClassroom.JsonSchedule = JsonConvert.SerializeObject(updateClassroom.Schedule);

                    int error = _volunteerProvider.UpdateClassroom(updateClassroom);
                    if (errorFlag) { errorFlag = false; return; }

                    if (error == 1)
                    {
                        GrowlHelpers.Error("No Assignment exists for this Volunteer in the Database!");
                    }
                    else if (error == 2)
                    {
                        GrowlHelpers.Success("Volunteer is not assigned to a school. Please assign volunteer to a school before making a classroom");
                    }
                    else
                    {
                        this.DialogResult = true;
                        Close();
                    }
                }
            }
            else
            {
                //Logic for saving a new classroom
                if(IsDataValid())
                {
                    ClassroomsModel newClassroom = new ClassroomsModel();
                    
                    newClassroom.Volunteer = (VolunteerNameIdModel)CmbAddEditSelectVolunteer.SelectedItem;
                    newClassroom.ClassroomNumber = txtRoomNumber.Text;
                    newClassroom.ClassroomSize = Int32.Parse(txtClassSize.Text);
                    newClassroom.GradeLevel = txtGrade.Text;
                    newClassroom.TeacherName = txtTeacherName.Text;
                    //schedule
                    VolunteerClassroomSchedule newSchedule = new VolunteerClassroomSchedule();
                    newSchedule.Day = getDays();
                    newSchedule.StartTime = tpStart.Text;
                    newSchedule.EndTime = tpEnd.Text;
                    newClassroom.Schedule = newSchedule;
                    newClassroom.JsonSchedule = JsonConvert.SerializeObject(newSchedule);
                    int error = _volunteerProvider.InsertNewClassroom(newClassroom);
                    if (errorFlag) { errorFlag = false; return; }

                    if (error == 1)
                    {
                        GrowlHelpers.Info("Volunteer is not assigned to a school. Please assign volunteer to a school before making a classroom");
                    }
                    else
                    {
                        this.DialogResult = true;
                        Close();
                    }

                    
                }
            }
            
        }

        /// <summary>
        /// Function Name: btnCancel_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Cancel Button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Are you sure you want to exit and discard any changes?";
            string caption = "Confirm Cancel";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBoxResult.Yes;

            if (result == System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes))
            {
                Close();
            }
        }

        /// <summary>
        /// Function Name: tglEditTime_Checked 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Handles the Checked event for the Edit Time toggle to see if the Time needs to be
        /// edited when updating a classroom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tglEditTime_Checked(object sender, RoutedEventArgs e)
        {
            if (tglEditTime.IsChecked == true)
            {
                ShowTimePickers();
            }
            else
            {
                HideTimePickers();
            }
        }

        /// <summary>
        /// Function Name: ShowTimePickers 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Shows the Time pickers when updating a Classroom
        /// </summary>
        private void ShowTimePickers()
        {
            //change visibility
            txtStartTime.Visibility = Visibility.Hidden;
            txtEndTime.Visibility = Visibility.Hidden;
            tpStart.Visibility = Visibility.Visible;
            tpEnd.Visibility = Visibility.Visible;
            //assign new clocks to TimePickers
            tpStart.Clock = new Clock();
            tpEnd.Clock = new Clock();
            tpStart.SelectedTime = DateTime.Now;
            tpEnd.SelectedTime = DateTime.Now.AddHours(1);
        }

        /// <summary>
        /// Function Name: HideTimePickers 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Hides the Time pickers when updating a Classroom
        /// </summary>
        private void HideTimePickers()
        {
            txtStartTime.Visibility = Visibility.Visible;
            txtEndTime.Visibility = Visibility.Visible;
            tpStart.Visibility = Visibility.Hidden;
            tpEnd.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Function Name: getDays 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Sees what days are checked and puts their abbreviation in a string
        /// </summary>
        /// <returns> The string of checked schools </returns>
        private string getDays()
        {
            // configure days
            String SchoolDays = "";
            if (chkMonday.IsChecked == true)
            {
                SchoolDays = "M";
            }
            if (chkTuesday.IsChecked == true)
            {
                SchoolDays += "T";
            }
            if (chkWednesday.IsChecked == true)
            {
                SchoolDays += "W";
            }
            if (chkThursday.IsChecked == true)
            {
                SchoolDays += "TR";
            }
            if (chkFriday.IsChecked == true)
            {
                SchoolDays += "F";
            }
            return SchoolDays;
        }

        /// <summary>
        /// Function Name: AddOrEditClassroom 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Sets the days a volunteer is in a classroom when updating a classroom
        /// </summary>
        /// <param name="classroom"></param>
        private void SetDays(ClassroomsModel classroom)
        {
            // configure days
            string SchoolDays = "";
            if(classroom.Schedule != null && classroom.Schedule.Day != null)
            {
                if (classroom.Schedule.Day.Contains("M"))
                {
                    chkMonday.IsChecked = true;
                }
                if (classroom.Schedule.Day.Contains("T"))
                {
                    chkTuesday.IsChecked = true;
                }
                if (classroom.Schedule.Day.Contains("W"))
                {
                    chkWednesday.IsChecked = true;
                }
                if (classroom.Schedule.Day.Contains("R"))
                {
                    chkThursday.IsChecked = true;
                }
                if (classroom.Schedule.Day.Contains("F"))
                {
                    chkFriday.IsChecked = true;
                }
            }
            
        }

        /// <summary>
        /// Function Name: IsNumeric 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Checks if the passed string only has digits in it
        /// </summary>
        /// <param name="text"></param>
        /// <returns> boolean value: true if string is only digits, false if any characters are in it. </returns>
        private bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]+$");
        }

        /// <summary>
        /// Overrites the OnActivated method to set the growl parent to the current window
        /// </summary>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        protected override void OnActivated(EventArgs e)
        {
            Growl.SetGrowlParent(stkGrowl, true);   //Sets the GrowlPanel onto this page
            base.OnActivated(e);
        }

        /// <summary>
        /// Overrites the OnDeactived method to unset the growl parent from the current window
        /// </summary>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        protected override void OnDeactivated(EventArgs e)
        {
            Growl.SetGrowlParent(stkGrowl, false);
            base.OnDeactivated(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            Growl.GrowlPanel = null;
        }

        /// <summary>
        /// This method will show a growl warning with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlError(string strMessage)
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
    }
}
