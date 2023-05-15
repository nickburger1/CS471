using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using HandyControl.Data;
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

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for AddNewChildAssignment.xaml
    /// </summary>    
    public partial class AddNewChildAssignment : System.Windows.Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly int _selectedVolunteer;
        private readonly int _volunteerSchool;
        private bool errorFlag;

        public AddNewChildAssignment(IServiceProvider serviceProvider, int selectedVolunteer, int volunteerSchool)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _selectedVolunteer = selectedVolunteer;
            _volunteerSchool = volunteerSchool;

            _volunteerProvider.DatabaseError += ErrorHandler;
            errorFlag = false;
            InitializeComponent();
            PopulateDropdowns();

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
        /// Function Name: PopulateDropdowns
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Populates the conditons and needs drop downs  
        /// </summary>
        public void PopulateDropdowns()
        {
            PopulateRoomNumber();

            var Conditions = _volunteerProvider.GetAllConditions();
            if (errorFlag) { errorFlag = false; return; }
            cmbConditions.ItemsSource = Conditions;

            var Needs = _volunteerProvider.GetAllStudentNeeds();
            if (errorFlag) { errorFlag = false; return; }
            cmbNeeds.ItemsSource = Needs;
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
            CmbAddSelectRoom.ItemsSource = classrooms;
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

        /// <summary>
        /// This method will check that the assignment controls have valid inputs
        /// </summary>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/12/2023</created>
        private bool IsAssignmentValid()
        {
            bool isValid = true;
            if (CmbAddSelectRoom.SelectedValue == null || string.IsNullOrEmpty(CmbAddSelectRoom.SelectedValue.ToString()))
            {
                GrowlHelpers.Warning("You must select an Classroom Number before creating an Assignment.");
                isValid = false;
            }
            if (cmbAge.SelectedValue == null)
            {
                GrowlHelpers.Warning("You must select an age");
                isValid = false;
            }
            if (cmbConditions.SelectedItems.Count == 0)
            {
                GrowlHelpers.Warning("You must select a Conditon(s) before creating an assignment.");
                isValid = false;
            }
            if (cmbNeeds.SelectedItems.Count == 0)
            {
                GrowlHelpers.Warning("You must select a Need(s) before creating an assignment.");
                isValid = false;
            }
            if (_volunteerProvider.CheckIfIdentifierExists(txtIdentifier.Text))
            {
                if (errorFlag) { errorFlag = false; return false; }
                GrowlHelpers.Warning("This Identifier already exists!");
                isValid = false;
            }
            if (errorFlag) { errorFlag = false; return false; }
            if (string.IsNullOrEmpty(txtDesiredOutcome.Text) || txtDesiredOutcome.Text.Length > 45)
            {
                GrowlHelpers.Warning("Desired outcome must be between 1 and 45 characters");
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// This method is used to check that a new condition is valid
        /// </summary>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/12/2023</created>
        private bool IsConditionValid()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtNewConditionAbbreviation.Text) || txtNewConditionAbbreviation.Text.Length > 4)
            {
                GrowlHelpers.Warning("Student condition abbreviation must be between 1 and 4 characters");
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtNewCondition.Text) || txtNewCondition.Text.Length > 100)
            {
                GrowlHelpers.Warning("Student condition description must be between 1 and 100 characters");
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// This method is used to check that a new need is valid
        /// </summary>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/12/2023</created>
        private bool IsNeedValid()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtNewNeedAbbreviation.Text) || txtNewNeedAbbreviation.Text.Length > 4)
            {
                GrowlHelpers.Warning("Student need abbreviation must be between 1 and 4 characters");
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtNewNeed.Text) || txtNewNeed.Text.Length > 100)
            {
                GrowlHelpers.Warning("Student need description must be between 1 and 100 characters");
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
            //various data verification checks and growls

            if (IsAssignmentValid())
            {
                NewVolunteerChildAssignmentsModel newChildAssignment = new NewVolunteerChildAssignmentsModel();

                List<ConditionItemModel> conditions = new List<ConditionItemModel>();
                foreach (ConditionItemModel condition in cmbConditions.SelectedItems)
                {
                    conditions.Add(condition);
                }

                List<StudentNeedItemModel> needs = new List<StudentNeedItemModel>();
                foreach (StudentNeedItemModel need in cmbNeeds.SelectedItems)
                {
                    needs.Add(need);
                }

                newChildAssignment.Student = new StudentModel();
                newChildAssignment.Student.Identifier = txtIdentifier.Text;
                newChildAssignment.StudentConditions = conditions;
                newChildAssignment.StudentNeeds = needs;
                newChildAssignment.DesiredOutcome = txtDesiredOutcome.Text;
                newChildAssignment.Classroom = (ClassroomsModel)CmbAddSelectRoom.SelectedItem;

                if (cmbAge.SelectedIndex == 0)
                {
                    newChildAssignment.Student.IsAgeBirthTo5 = true;
                    newChildAssignment.Student.IsAge5To12 = false;
                }

                if (cmbAge.SelectedIndex == 1)
                {
                    newChildAssignment.Student.IsAgeBirthTo5 = false;
                    newChildAssignment.Student.IsAge5To12 = true;
                }

                int result = _volunteerProvider.InsertNewChildAssignment(newChildAssignment, _selectedVolunteer);
                if (errorFlag) { errorFlag = false; return; }

                if (result == 0)
                {
                    this.DialogResult = true;
                    Close();
                }
                else
                {
                    ShowGrowlInfo("Classroom does not exist! Please create classroom then make child assignments.");
                }
            }
        }

        /// <summary>
        /// Function Name: btnAddCondition_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles the Add Conditon click event by passing the input values to the database to 
        /// add a new Conditon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCondition_Click(object sender, RoutedEventArgs e)
        {
            if (IsConditionValid())
            {
                ConditionItemModel newConditon = new ConditionItemModel();
                newConditon.Acronym = txtNewConditionAbbreviation.Text;
                newConditon.Description = txtNewCondition.Text;

                var Conditions = _volunteerProvider.GetAllConditions();
                if (errorFlag) { errorFlag = false; return; }

                //check that this condition is not already used
                if (Conditions.Where(x => x.Acronym == null ? false : x.Acronym.Equals(newConditon.Acronym)).FirstOrDefault() != null)
                {
                    GrowlHelpers.Warning("This condition abreviation already exists, please use a different one");
                    DialogResult = false;
                    return;
                }
                if (Conditions.Where(x => x.Description == null ? false : x.Description.Equals(newConditon.Description)).FirstOrDefault() != null)
                {
                    GrowlHelpers.Warning("This Condtion name already exists, please use a different one");
                    DialogResult = false;
                    return;
                }


                _volunteerProvider.InsertNewStudentConditon(newConditon);
                if (errorFlag) { errorFlag = false; return; }
                this.DialogResult = true;
                Close();

            }
        }

        /// <summary>
        /// Function Name: btnCancelCondition_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles the Cancel Condition Buttons Click event and closes the window  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelCondition_Click(object sender, RoutedEventArgs e)
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
        /// Function Name: btnAddNeed_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles the Add Need click event by passing the input values to the database to 
        /// add a new need
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNeed_Click(object sender, RoutedEventArgs e)
        {
            if (IsNeedValid())
            {
                StudentNeedItemModel newNeed = new StudentNeedItemModel();
                newNeed.Acronym = txtNewNeedAbbreviation.Text;
                newNeed.Description = txtNewNeed.Text;

                var Needs = _volunteerProvider.GetAllStudentNeeds();
                if (errorFlag) { errorFlag = false; return; }
                //check that this condition is not already used
                if (Needs.Where(x => x.Acronym == null ? false : x.Acronym.Equals(newNeed.Acronym)).FirstOrDefault() != null)
                {
                    GrowlHelpers.Warning("This condition abreviation already exists, please use a different one");
                    DialogResult = false;
                    return;
                }
                if (Needs.Where(x => x.Description == null ? false : x.Description.Equals(newNeed.Description)).FirstOrDefault() != null)
                {
                    GrowlHelpers.Warning("This Condtion name already exists, please use a different one");
                    DialogResult = false;
                    return;
                }

                _volunteerProvider.InsertNewStudentNeed(newNeed);
                if (errorFlag) { errorFlag = false; return; }
                this.DialogResult = true;
                Close();
            }
        }

        /// <summary>
        /// Function Name: btnCancelNeed_Click 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Handles the Cancel Need Buttons Click event and closes the window  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelNeed_Click(object sender, RoutedEventArgs e)
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
                WaitTime = 3,
            });
        }

        /// <summary>
        /// This method will show a growl success with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlSuccess(string strMessage)
        {
            Growl.Success(new GrowlInfo
            {
                Message = strMessage,
                StaysOpen = false,
                ShowDateTime = false,
                WaitTime = 3,
            });
        }
        #endregion
    }
}
