using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using B_FGMS.BusinessLogic.ViewModels;
using Bogus.DataSets;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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


namespace C_FGMS.UI
{
    /// <FileName> VolunteerDemographics.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
    /// <DateCreated> 1/24/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 4/11/23 </LastModified>
    /// <LastModifiedBy> Isabelle Johns </LastModifiedBy>
    /// <summary>
    /// The Purpose of this file bind the volunteer demographics page to backend operations.
    /// </summary>
    /// <author> Jon Maddocks </author>
    public partial class VolunteerDemographics : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IDialogProvider _dialogProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private VolunteerDemographicsViewModel _viewModel;
        private bool errorFlag;

        private bool blnIsEditing = false;
        private bool blnNeedsRefresh = false;

        public VolunteerDemographics(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _dialogProvider = _serviceProvider.GetRequiredService<IDialogProvider>();
            _viewModel = new VolunteerDemographicsViewModel(_volunteerProvider);

            _volunteerProvider.DatabaseError += ErrorHandler;

            _refreshEventBroker = refreshEventBroker;
            InitializeComponent();

            _refreshEventBroker.Subscribe((args, x) =>
            {
                if (x.StartsWith("Volunteer") || x.StartsWith("UsersAdmin"))
                {
                    RefreshData();
                }
            });

            DataContext = _viewModel;
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
        /// Handles the volunteer selection being changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Ryley Taub </author>
        /// <created> 2/19/23 </created>
        private void cmbVolunteerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Application.Current.Properties["VolunteerTuid"] = cmbVolunteerName.SelectedIndex;
            
            if(cmbVolunteerName.SelectedValue == null)
            {
                RefreshData();
            }
        }

        /// <summary>
        /// This function is responsbile for the click event of the Edit button on 
        /// the Volunteer Demographics UI page. 
        /// 
        /// The function will hide the Edit button and toggle the visibilty   
        /// for the Cancel and Finish button to Visible.Lastly, the page     
        /// will enable editting controls dedicated to this page: Date of     
        /// Birth, Separation Date, Status, Gender, Identifies As, Ethnicity,   
        /// Racial Group, End Date, Veteran, Family of Military and Reason.   
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Event specific that is being handled. </param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.VolunteerTuid.HasValue)
            {
                SetToggleState(true, Visibility.Visible, Visibility.Hidden);
            }
            else
            {
                Growl.Warning(new GrowlInfo
                {
                    Message = "Please Select a Volunteer",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
            }
        }

        /// <summary>
        /// This function is responsbile for the click event of the Edit button on 
        /// the Volunteer Demographics UI page. 
        /// 
        /// The function will hide the Edit button and toggle the visibilty   
        /// for the Cancel and Finish button to Visible.Lastly, the page     
        /// will enable editting controls dedicated to this page: Date of     
        /// Birth, Separation Date, Status, Gender, Identifies As, Ethnicity,   
        /// Racial Group, End Date, Veteran, Family of Military and Reason.   
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Event specific that is being handled. </param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            SetToggleState(true, Visibility.Visible, Visibility.Hidden);
        }

        /// <summary>
        /// This function is responsible for the click event of the Cancel    
        /// button the Volunteer Demographics UI page.                        
        ///                                                          
        /// The function will cancel all ongoing changes the user may have*
        /// made to the volunteer data. The original volunteer data will be   
        /// displayed.                                                        
        ///
        /// TextBoxes and Toggle Buttons will be disabled and set back to     
        /// Read-Only.                                                        
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Event specific that is being handled.</param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.FormChanged)
            {
                CancelChanges();
            }
            else
            {
                Growl.Ask("Changes will not be saved. Do you still want to close?", isConfirmed =>
                {
                    if (isConfirmed == true)
                    {
                        CancelChanges();
                    }

                    return true;
                });
            }
        }

        /// <summary>
        ///  Helper for the cancel function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void CancelChanges()
        {
            SetToggleState(false, Visibility.Hidden, Visibility.Visible);
            _viewModel.PopulateVolunteerInfo();

            Growl.Info(new GrowlInfo
            {
                Message = "Cancelled Changes",
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 3
            });
        }


        /// <summary>
        /// This function is responsible for the click event of the Save button
        /// on the Volunteer Demographics UI page.
        /// 
        /// The function will save all the volunteer fields to the database for the
        /// current volunteer. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Event specific that is being handled.</param>
        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateDates() && !_viewModel.HasErrors)
            {
                _volunteerProvider.UpdateVolunteerDemographics(_viewModel);
                if (errorFlag) { errorFlag = false; return; }

                SetToggleState(false, Visibility.Hidden, Visibility.Visible);

                Growl.Success(new GrowlInfo
                {
                    Message = "Saved Changes",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });

                _refreshEventBroker.Publish(nameof(VolunteerDemographics));
            }
            else
            {
                Growl.Error(new GrowlInfo
                {
                    Message = "Changes cannot be saved. Please check any error messages.",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
            }

            
        }

        /// <summary>
        /// This function is responsible for settings the toggle status for   
        /// the page.The user can change the toggle status by clicking on
        /// either the Edit, Cancel, or Finish button.                        
        ///
        /// Edit will enable TextBoxes and show Toggle Switch controls.       
        /// Cancel & Finish will disable Textboxes and hide Toggle Switch     
        /// controls.                                                         
        /// </summary>
        /// <param name="blnEditState">boolean representing if the user is in Edit mode.</param>
        /// <param name="visButtonState">Visibility status for the Toggle Switch buttons</param>
        /// <param name="visTextState">Visibility status for the TextBoxes</param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void SetToggleState(Boolean blnEditState, Visibility visButtonState, Visibility visTextState)
        {
            blnIsEditing = blnEditState;

            //EDIT MODE ENABLED
            if (blnEditState == true)
            {
                //Top right buttons
                btnEdit.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Visible;
                btnFinish.Visibility = Visibility.Visible;
                txtEditMode.Visibility = Visibility.Visible;
                expVolName.IsHitTestVisible = true;

                //disable volunteer dropdown
                cmbVolunteerName.IsEnabled = false;

                //Toggle buttons: Status, Veteran, Family of Military
                UIToggleButtonStates(Visibility.Visible, Visibility.Visible, Visibility.Hidden);
                UIDatePickerStates(true, Brushes.White, false);
            }
            //EDIT MODE DISABLED
            else
            {
                //Top right buttons
                btnEdit.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Hidden;
                btnFinish.Visibility = Visibility.Hidden;
                txtEditMode.Visibility = Visibility.Hidden;
                expVolName.IsHitTestVisible = false;
                expVolName.IsExpanded = false;

                //enable volunteer dropdown
                cmbVolunteerName.IsEnabled = true;

                //Toggle buttons: Status, Veteran, Family of Military
                UIToggleButtonStates(Visibility.Hidden, Visibility.Hidden, Visibility.Visible);
                UIDatePickerStates(false, Brushes.Transparent, false);
            }

            //Gender
            cmbGender.Visibility = visButtonState;
            txtGender.Visibility = visTextState;

            //Ethnicity
            cmbEthnicity.Visibility = visButtonState;
            txtEthnicity.Visibility = visTextState;

            //Racial Group
            cmbRacialGroup.Visibility = visButtonState;
            txtRacialGroup.Visibility = visTextState;

            //Identifies As
            cmbIdentifiesAs.Visibility = visButtonState;
            txtIdentifiesAs.Visibility = visTextState;

            //Reasons
            cmbReasonsSeparated.Visibility = visButtonState;
            txtReasonsSeparated.Visibility = visTextState;
        }

        /// <summary>
        /// Function Name: UIToggleButtonStates
        /// 
        /// Purpose: Set the visibility status for the label, button, and textfield that correlate to Status, Veteran, and Family of Military fields.
        ///     These fields contain a ToggleButton, and requires specific visibilities states in comparison to others. 
        /// </summary>
        /// <param name="lblVisibility">Visibility state for title label</param>
        /// <param name="btnVisibility">Visibility state for toggle button</param>
        /// <param name="txtVisibility">Visibility state for text field</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/19/2023</created>
        private void UIToggleButtonStates(Visibility lblVisibility, Visibility btnVisibility, Visibility txtVisibility)
        {
            lblStatus.Visibility = lblVisibility;
            btnStatus.Visibility = btnVisibility;
            txtStatus.Visibility = txtVisibility;

            lblVeteran.Visibility = lblVisibility;
            btnVeteran.Visibility = btnVisibility;
            txtVeteran.Visibility = txtVisibility;

            lblFamilyofMilitary.Visibility = lblVisibility;
            btnFamilyOfMilitary.Visibility = btnVisibility;
            txtFamilyOfMilitary.Visibility = txtVisibility;
        }

        /// <summary>
        /// Function Name: UIDatePickerStates
        /// 
        /// Purpose: Set the editability for date pickers. Date of Birth should be editable when in Edit mode, and 
        ///     Separation of Date, End Date, should only be editable when the user is 'Inactive'. As well as, change
        ///     background color to further imply the field is editable.
        /// </summary>
        /// <param name="isEditable">Boolean check to set the editability of date picker</param>
        /// <param name="colorState">Set background color of the date picker</param>
        /// <param name="isActivityToggle">Boolean check of sender</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/19/2023</created>
        private void UIDatePickerStates(bool isEditable, Brush colorState, bool isActivityToggle)
        {
            if (!isActivityToggle)
            {
                dprDateOfBirth.IsHitTestVisible = isEditable;
                dprDateOfBirth.Background = colorState;
            }

            ToggleActivity();
            
        }

        /// <summary>
        /// Set the editability for Separated Date and Reason Separated.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/2023 </created>
        private void ToggleActivity()
        {
            bool isEditable = !_viewModel.IsActive;
            Brush colorState = Brushes.Transparent;
            cmbReasonsSeparated.IsEnabled = false;

            if (!_viewModel.IsActive)
            {
                colorState = Brushes.White;
                cmbReasonsSeparated.IsEnabled = true;
            }

            if (!blnIsEditing)
            {
                isEditable = false;
                colorState = Brushes.Transparent;
            }

            dprSeparationDate.IsHitTestVisible = isEditable;
            dprSeparationDate.Focusable = isEditable;
            dprSeparationDate.Background = colorState;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(VolunteerDemographics));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }

        /// <summary>
        /// Handles the on load method for the volunteer dropdown.
        /// Persists the volunteer selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/27/23 </created>
        private void cmbVolunteerName_Loaded(object sender, RoutedEventArgs e)
        {
            if (blnIsEditing)
            {
                return;
            }

            int? selectedVolunteer = (int?)Application.Current.Properties["VolunteerTuid"];

            if (selectedVolunteer.HasValue)
            {
                cmbVolunteerName.SelectedIndex = selectedVolunteer.Value;
            }
            else
            {
                cmbVolunteerName.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Refreshes all the values for controls on the screen by
        /// updating their binding
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private void RefreshData()
        {
            if (blnIsEditing)   // Early return if in Edit Mode
            {
                blnNeedsRefresh = true; // Sets boolean to true (checked in changeStateView() method)
                Growl.Warning("Data could not be refreshed on Volunteer Demographics. Exit editing mode to refresh");
                return;
            }

            int? volunteerTuid = null;

            if (cmbVolunteerName.SelectedValue != null)
            {
                volunteerTuid = _viewModel.VolunteerTuid;
            }

            DataContext = null;
            _viewModel = new VolunteerDemographicsViewModel(_volunteerProvider);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            DataContext = _viewModel;

            if (volunteerTuid.HasValue)
            {
                _viewModel.VolunteerTuid = volunteerTuid.Value;
            }

            

            blnNeedsRefresh = false;

            return;
        }

        /// <summary>
        /// Validates the Datepickers on the screen. Will not allow a separation date
        /// or birth date to be null or before the start date
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        private bool ValidateDates()
        {
            bool hasDateError = false;

            if (!_viewModel.IsActive)
            {
                //Check Separation Date
                if (!dprSeparationDate.SelectedDate.HasValue)
                {
                    hasDateError = true;
                    Growl.Error(new GrowlInfo
                    {
                        Message = "Separation Date cannot be empty",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }
                else if (DateTime.Compare(_viewModel.SeparationDate!.Value, _viewModel.StartDate) < 0)
                {
                    hasDateError = true;
                    Growl.Error(new GrowlInfo
                    {
                        Message = "Separation cannot be before Start Date",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }

                //Check Birthday
                if (!dprDateOfBirth.SelectedDate.HasValue)
                {
                    hasDateError = true;
                    Growl.Error(new GrowlInfo
                    {
                        Message = "Date of Birth cannot be empty",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }
                else if (DateTime.Compare(_viewModel.DateOfBirth, _viewModel.StartDate) > 0)
                {
                    hasDateError = true;
                    Growl.Error(new GrowlInfo
                    {
                        Message = "Date of Birth cannot be after Start Date",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }
            }

            return hasDateError;
        }

        /// <summary>
        /// Handles the onClick of the status toggle button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            if (btnStatus.IsChecked == true)
                UIDatePickerStates(false, Brushes.Transparent, true);
            else
                UIDatePickerStates(true, Brushes.White, true);

        }

        private void expVolName_Expanded(object sender, RoutedEventArgs e)
        {
            _viewModel.PopulateName();
        }
    }
}