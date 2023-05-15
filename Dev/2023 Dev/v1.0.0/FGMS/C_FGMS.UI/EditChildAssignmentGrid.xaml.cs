using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HandyControl.Controls;
using C_FGMS.UI.Helpers;
using HandyControl.Data;
using B_FGMS.BusinessLogic.Services.AssignmentProviders;
using B_FGMS.BusinessLogic.Events;

/**
 ************************************************************************************************************************
 *                                      File Name : EditChildAssignmentGrid.xaml.cs                                   *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Christopher Washburn                                                  *
 *                                      Date Created : 3/22/2023                                                         *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 4/11/2023                                                       *
 *                                      Last Modified By : Christopher Washburn                                           *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to allow edits for the Child Assignments  Data Grid                       *
 ************************************************************************************************************************/

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for EditChildAssignmentGrid.xaml
    /// </summary>
    public partial class EditChildAssignmentGrid : System.Windows.Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IAssignmentProvider _assignmentProvider;
        private readonly int _selectedVolunteer;
        private readonly VolunteerChildAssignmentDataGridModel _selectedGridRowModel;
        private readonly int _volunteerSchool;
        private bool errorFlag;

        public EditChildAssignmentGrid(IServiceProvider serviceProvider, int selectedVolunteer, VolunteerChildAssignmentDataGridModel childAssignment, int volunteerSchool)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _assignmentProvider = _serviceProvider.GetRequiredService<IAssignmentProvider>();
            _selectedGridRowModel = childAssignment;
            _selectedVolunteer = selectedVolunteer;
            _volunteerSchool = volunteerSchool;

            _volunteerProvider.DatabaseError += ErrorHandler;
            _assignmentProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            InitializeComponent();
            PopulateRoomNumber();
            PopulateWindow((int)childAssignment.StudentTuid);

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
        /// Function Name: PopulateRoomNumber
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Fills the Room Number combo box with the volunteers schools room numbers 
        /// </summary>        
        public void PopulateRoomNumber()
        {
            var classrooms = _volunteerProvider.GetVolunteersClassrooms(_selectedVolunteer);
            if (errorFlag) { errorFlag = false; return; }
            CmbEditSelectRoom.ItemsSource = classrooms;
        }

        /// <summary>
        /// Function Name: PopulateWindow 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Populates the Conditons and Needs dropdowns with the current Conditons and Needs and 
        /// selects the passed students current Conditons and Needs while filling in all other data.
        /// </summary>
        /// <param name="studentTuid"></param>
        public void PopulateWindow(int studentTuid)
        {
            var Conditions = _volunteerProvider.GetAllConditions();
            if (errorFlag) { errorFlag = false; return; }
            cmbConditions.ItemsSource = Conditions;

            var studentConditions = _volunteerProvider.GetStudentConditions(studentTuid);
            if (errorFlag) { errorFlag = false; return; }

            foreach (var condition in Conditions)
            {
                foreach (var studentCondition in studentConditions)
                {

                    if (condition.Acronym == studentCondition.Acronym)
                    {
                        cmbConditions.SelectedItems.Add(condition);
                    }
                }
            }


            var Needs = _volunteerProvider.GetAllStudentNeeds();
            if (errorFlag) { errorFlag = false; return; }
            cmbNeeds.ItemsSource = Needs;

            var studentNeeds = _volunteerProvider.GetStudentNeeds(studentTuid);
            if (errorFlag) { errorFlag = false; return; }

            foreach (var need in Needs)
            {
                foreach (var studentNeed in studentNeeds)
                {

                    if (need.Acronym == studentNeed.Acronym)
                    {
                        cmbNeeds.SelectedItems.Add(need);
                    }
                }
            }

            var student = _volunteerProvider.GetStudent(studentTuid);
            if (errorFlag) { errorFlag = false; return; }
            if (student != null)
            {
                txtIdentifier.Text = student.Identifier;

                if (student.IsAgeBirthTo5 == true)
                {
                    cmbAge.SelectedIndex = 0;
                }

                if (student.IsAge5To12 == true)
                {
                    cmbAge.SelectedIndex = 1;
                }
                int? intClassroomTuid = _assignmentProvider.GetClassroomTuidByStudentTuid(studentTuid);
                if (errorFlag) { errorFlag = false; return; }
                if (intClassroomTuid != null)
                {
                    CmbEditSelectRoom.SelectedValue = intClassroomTuid;
                }
            }

            var desiredOutcome = _volunteerProvider.GetStudentDesiredOutCome(studentTuid);
            if (errorFlag) { errorFlag = false; return; }
            txtDesiredOutcome.Text = desiredOutcome;
        }

        /// <summary>
        /// Function Name: btnCancel_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles OnClick event for Cancel
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

        private bool IsDataValid()
        {
            bool isValid = true;
            if (txtIdentifier.Text.Length > 45)
            {
                GrowlHelpers.Warning("Student identifier must shorter than 45 characters");
                isValid = false;
            }
            if (CmbEditSelectRoom.SelectedItem == null)
            {
                GrowlHelpers.Warning("You must select a Room Number.");
                isValid = false;
            }
            if (cmbAge.SelectedItem == null)
            {
                GrowlHelpers.Warning("You must enter a Age before updating a current assignment.");
                isValid = false;
            }
            if (cmbConditions.SelectedItems.Count == 0)
            {
                GrowlHelpers.Warning("You must select a Condtion(s) before updating a current assignment.");
                isValid = false;
            }
            if (cmbNeeds.SelectedItems.Count == 0)
            {
                GrowlHelpers.Warning("You must enter a Need(s) before updating a current assignment.");
                isValid = false;
            }
            StudentModel? student = _volunteerProvider.GetStudent(_selectedGridRowModel.StudentTuid);
            if (errorFlag) { errorFlag = false; return false; }
            if (student == null)
            {
                GrowlHelpers.Error("An error ocurred while getting this student record");
                isValid = false;
            }
            //if the current identifier is not the original identifier and it exists we will not allow it
            else if (!txtIdentifier.Text.Equals(_selectedGridRowModel.Identifier))
            {
                if (_volunteerProvider.CheckIfIdentifierExists(txtIdentifier.Text))
                {
                    if (errorFlag) { errorFlag = false; return false; }
                    GrowlHelpers.Warning("This new identifier already exists");
                    isValid = false;
                }
                if (errorFlag) { errorFlag = false; return false; }
            }
            if(string.IsNullOrEmpty(txtDesiredOutcome.Text) || txtDesiredOutcome.Text.Length > 45)
            {
                GrowlHelpers.Warning("Must have a desired outcome that is between 1 and 45 characters");
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Function Name: btnSave_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles OnClick event for Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            if(IsDataValid())
            {
                //Save new child assignment to DB
                NewVolunteerChildAssignmentsModel updateChildAssignment = new NewVolunteerChildAssignmentsModel();
                List<ConditionItemModel> newConditions = new List<ConditionItemModel>();
                List<StudentNeedItemModel> newNeeds = new List<StudentNeedItemModel>();


                //Get each selected condition
                foreach (ConditionItemModel condition in cmbConditions.SelectedItems)
                {
                    newConditions.Add(condition);
                }

                //Get each selected need
                foreach (StudentNeedItemModel need in cmbNeeds.SelectedItems)
                {
                    newNeeds.Add(need);
                }

                updateChildAssignment.Student = new StudentModel();
                updateChildAssignment.Student.Identifier = txtIdentifier.Text;
                updateChildAssignment.StudentConditions = newConditions;
                updateChildAssignment.StudentNeeds = newNeeds;
                updateChildAssignment.DesiredOutcome = txtDesiredOutcome.Text;

                if (cmbAge.SelectedIndex == 0)
                {
                    updateChildAssignment.Student.IsAgeBirthTo5 = true;
                    updateChildAssignment.Student.IsAge5To12 = false;
                }

                if (cmbAge.SelectedIndex == 1)
                {
                    updateChildAssignment.Student.IsAgeBirthTo5 = false;
                    updateChildAssignment.Student.IsAge5To12 = true;
                }

                _serviceProvider.GetRequiredService<IVolunteerProvider>().UpdateChildAssignment(_selectedVolunteer, _selectedGridRowModel.StudentTuid, updateChildAssignment);
                ShowGrowlInfo("Child Assignment Updated!");
                DialogResult = true;
                Close();
            }
        }

        /// <summary>
        /// Function Name: CmbEditSelectRoom_Loaded 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: When a room is selected on the parent page auto selects that selected room
        /// to the CmbEditSelectRoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbEditSelectRoom_Loaded(object sender, RoutedEventArgs e)
        {
            if (_selectedGridRowModel.Classroom != null)
            {
                for (int i = 0; i < CmbEditSelectRoom.Items.Count; i++)
                {
                    ClassroomsModel classroom = (ClassroomsModel)CmbEditSelectRoom.Items[i];
                    if (classroom.ClassroomNumber == _selectedGridRowModel.Classroom.ClassroomNumber)
                    {
                        CmbEditSelectRoom.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        #region Growl Logic

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


            #endregion
        }
    }
}
