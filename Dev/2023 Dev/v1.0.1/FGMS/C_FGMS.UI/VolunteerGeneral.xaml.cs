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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using B_FGMS.BusinessLogic.Models.Volunteer;
using HandyControl.Controls;
using HandyControl.Data;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using A_FGMS.DataLayer.Entities;
using DatePicker = HandyControl.Controls.DatePicker;
using B_FGMS.BusinessLogic.ViewModels;
using C_FGMS.UI.Helpers;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Forms;
using CheckBox = System.Windows.Controls.CheckBox;
using A_FGMS.DataLayer.EventBroker;
using Application = System.Windows.Application;
using B_FGMS.BusinessLogic.Events;
using A_FGMS.DataLayer.Exceptions;

namespace C_FGMS.UI
{
    /// <FileName> VolunteerGeneral.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
    /// <DateCreated> 1/24/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 3/31/23 </LastModified>
    /// <LastModifiedBy> Isabelle Johns </LastModifiedBy>
    /// <summary>
    /// The Purpose of this file bind the volunteer general page to backend operations.
    /// </summary>
    /// <author> Isabelle Johns </author>
    public partial class VolunteerGeneral : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly ISchoolProvider _schoolProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private VolunteerGeneralViewModel _viewModel;
        private bool errorFlag;

        private bool blnIsEditing = false;
        private bool blnNeedsRefresh = false;

        public VolunteerGeneral(IServiceProvider serviceProvider, IVolunteerProvider volunteerProvider, ISchoolProvider schoolProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = volunteerProvider;
            _schoolProvider = schoolProvider;
            _refreshEventBroker = refreshEventBroker;
            _viewModel = new VolunteerGeneralViewModel(_volunteerProvider, _schoolProvider);

            InitializeComponent();

            _volunteerProvider.DatabaseError += ErrorHandler;
            _schoolProvider.DatabaseError += ErrorHandler;
            errorFlag = false;
            _refreshEventBroker.Subscribe((args, x) =>
            {
                if (x.StartsWith("Volunteer") || x.StartsWith("UsersAdmin"))
                {
                    RefreshData();
                }
            });

            cmbState.ItemsSource = StateHelper.States();
            PopulateTemporaryInformation();

            DataContext = _viewModel;
        }

        #region Error handlers
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

        #region Button Click Methods

        /// <summary>
        /// Handles OnClick event for Saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 1/31/23 </created>
        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ValidateAll();
            if (!ValidateDates() && !_viewModel.HasErrors)
            {
                _volunteerProvider.UpdateVolunteer(_viewModel);
                if (errorFlag) { errorFlag = false; return; }
                _volunteerProvider.PushTemporaryInfo(GetVolunteerTempInfo(), _viewModel.VolunteerTuid!.Value);
                if (errorFlag) { errorFlag = false; return; }
                changeStateView();
                SetToggleState();

                Growl.Success(new GrowlInfo
                {
                    Message = "Saved Changes",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });

                _refreshEventBroker.Publish(nameof(VolunteerGeneral));
            }
            else
            {
                Growl.Error(new GrowlInfo
                {
                    Message = "Changes cannot be saved. Please look at error messages and/or missing fields.",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
            }
        }

        /// <summary>
        ///  Handles OnClick event for Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 1/31/23 </created>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
        /// <created> 3/31/23 </created>
        private void CancelChanges()
        {
            changeStateView();
            SetToggleState();
            PopulateVolunteersGeneralData();

            Growl.Info(new GrowlInfo
            {
                Message = "Cancelled Changes",
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 3
            });
        }

        /// <summary>
        /// Handles OnClick event for Editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 1/31/23 </created>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.VolunteerTuid.HasValue && _viewModel.Year.HasValue)
            {
                changeStateEdit();
                SetToggleState();
            }
            else
            {
                if (_viewModel.VolunteerTuid == null)
                {
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "Please Select a Volunteer",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }

                if (_viewModel.Year == null)
                {
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "Please Select a Year",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }
            }

        }

        /// <summary>
        /// Handles OnClick event for Deleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 1/30/23 </created>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.VolunteerTuid != null)
            {
                string[] fullName = cmbSelectVolunteer.Text.Split(", ");
                string name = string.Format("{0} {1}", fullName[1], fullName[0]);

                var confirmDelete = MessageBox.Show($"Are you sure you want to delete {name}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (confirmDelete == MessageBoxResult.Yes)
                {
                    DeleteVolunteer();
                }
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
        /// Helper function for the delete button
        /// </summary>
        /// <returns> None </returns>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void DeleteVolunteer()
        {
            string[] fullName = cmbSelectVolunteer.Text.Split(", ");
            string name = string.Format("{0} {1}", fullName[1], fullName[0]);

            _volunteerProvider.DeleteVolunteer(_viewModel.VolunteerTuid!.Value);
            if (errorFlag) { errorFlag = false; return; }

            Growl.Info(new GrowlInfo
            {
                Message = $"{name} removed",
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 3
            });

            _refreshEventBroker.Publish(nameof(VolunteerGeneral));
        }

        /// <summary>
        /// Handles OnClick event for Adding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 1/30/23 </created>
        private void btnAddVolunteer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddVolunteerWindow wndNewVolunteer = new AddVolunteerWindow(_serviceProvider);
                bool? newVolunteer = wndNewVolunteer.ShowDialog();

                Growl.GrowlPanel = null;    // Reset the Growl Panel to Null. This Locks it onto the current Window

                if (newVolunteer == true)
                {
                    _refreshEventBroker.Publish(nameof(VolunteerGeneral));
                }
            }catch(RefreshDataCustomException ex)
            {
                return;
            }
        }

        /// <summary>
        /// Handles OnClick event for Manual Refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Richard Nader </author>
        /// <created> 4/5/23 </created>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(VolunteerGeneral));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }

        #endregion

        #region Change UI States

        /// <summary>
        /// Changes the enabled values of the controls based on the page's edit mode value.
        /// Textboxes and Datepickers will gain a white background in edit mode.
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 1/31/23 </created>
        private void SetToggleState()
        {
            foreach (HandyControl.Controls.TextBox textBox in FindChildrenControls<HandyControl.Controls.TextBox>(grdMain))
            {
                if (blnIsEditing)
                {
                    textBox.IsReadOnly = false;
                    textBox.Background = Brushes.White;
                }
                else
                {
                    textBox.IsReadOnly = true;
                    textBox.Background = Brushes.Transparent;
                }

            }

            foreach (DatePicker datePicker in FindChildrenControls<DatePicker>(grdMain))
            {
                datePicker.Focusable = blnIsEditing;
                datePicker.IsHitTestVisible = blnIsEditing;

                if (blnIsEditing)
                {
                    datePicker.Background = Brushes.White;
                }
                else
                {
                    datePicker.Background = Brushes.Transparent;
                }
            }

            foreach (CheckBox checkBox in FindChildrenControls<CheckBox>(grdMain))
            {
                checkBox.IsEnabled = blnIsEditing;
            }

        }

        /// <summary>
        /// Changes the visibility of controls to change to the editing state
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 1/31/23 </created> 
        private void changeStateEdit()
        {
            blnIsEditing = true;
            btnAdd.Visibility = Visibility.Hidden;
            btnEdit.Visibility = Visibility.Hidden;
            txtEditState.Visibility = Visibility.Visible;
            

            btnCancel.Visibility = Visibility.Visible;
            btnFinish.Visibility = Visibility.Visible;

            txtSchool.Visibility = Visibility.Hidden;
            cmbSchoolList.Visibility = Visibility.Visible;

            txtActive.Visibility = Visibility.Hidden;
            btnStatus.Visibility = Visibility.Visible;

            txtState.Visibility = Visibility.Hidden;
            cmbState.Visibility = Visibility.Visible;
            cmbState.SetValue(Grid.RowProperty, 1);
            txtCity.SetValue(Grid.ColumnSpanProperty, 2);

            cmbSelectVolunteer.IsEnabled = false;
            cmbSelectYear.IsEnabled = false;
        }

        /// <summary>
        ///  Changes the visibility of the controls to change to the viewing state
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 1/31/23 </created>
        private void changeStateView()
        {
            blnIsEditing = false;
            btnAdd.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Visible;
            txtEditState.Visibility = Visibility.Hidden;

            btnCancel.Visibility = Visibility.Collapsed;
            btnFinish.Visibility = Visibility.Collapsed;

            txtSchool.Visibility = Visibility.Visible;
            cmbSchoolList.Visibility = Visibility.Hidden;

            txtActive.Visibility = Visibility.Visible;
            btnStatus.Visibility = Visibility.Hidden;

            txtState.Visibility = Visibility.Visible;
            cmbState.Visibility = Visibility.Hidden;
            cmbState.SetValue(Grid.RowProperty, 0);
            txtCity.SetValue(Grid.ColumnSpanProperty, 1);

            cmbSelectVolunteer.IsEnabled = true;
            cmbSelectYear.IsEnabled = true;

            if(blnNeedsRefresh)
            {
                RefreshData();
            }
        }


        /// <summary>
        /// This function is responsible for returning all of the children    
        /// controls from the parent.Such as, the function will be seeking
        /// all of a given control type on the page when given the parent.                  
        /// </summary>
        /// <typeparam name="T">Arbitrary generic type.</typeparam>
        /// <param name="depObj">Represents an object that participates in the 
        ///  dependency property system.</param>
        /// <returns></returns>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private static IEnumerable<T> FindChildrenControls<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindChildrenControls<T>(ithChild)) yield return childOfChild;
            }
        }

        /// <summary>
        /// Handles the on load method for the Year dropdown.
        /// Defaults the selection to the current year
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void cmbSelectYear_Loaded(object sender, RoutedEventArgs e)
        {
            cmbSelectYear.SelectedIndex = cmbSelectYear.Items.Count - 1;
        }

        /// <summary>
        /// Handles the on load method for the volunteer dropdown.
        /// Could be used in the future to load volunteer selected on another page
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/27/23 </created>
        private void cmbSelectVolunteer_Loaded(object sender, RoutedEventArgs e)
        {
            if(blnIsEditing)
            {
                return;
            }

            int? selectedVolunteer  = (int?)Application.Current.Properties["VolunteerTuid"];
            
            if (selectedVolunteer.HasValue)
            {
                cmbSelectVolunteer.SelectedIndex = selectedVolunteer.Value;
            }
            else
            {
                cmbSelectVolunteer.SelectedIndex = -1;
            }
        }

        #endregion

        #region Data Logic

        /// <summary>
        /// Populates the fields on the page with the selected volunteer's
        /// corresponding values
        /// </summary>
        /// <author> Christopher Washburn </author>
        /// <created> 2/19/23 </created>
        /// <lastModified> 3/31/23 </lastModified>
        /// <lastModifiedBy> Isabelle Johns </lastModifiedBy>
        public void PopulateVolunteersGeneralData()
        {
            if (_viewModel.VolunteerTuid == null)
            {
                return;
            }

            _viewModel.PopulateVolunteerInfo();
            PopulateVolunteerTempInfo();
        }

        /// <summary>
        /// Refreshes all the values for controls on the screen by
        /// updating their binding
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        public void RefreshData()
        {
            if (blnIsEditing)   // Early return if in Edit Mode
            {
                blnNeedsRefresh = true; // Sets boolean to true (checked in changeStateView() method)
                Growl.Warning("Data could not be refreshed on Volunteer General. Exit editing mode to refresh");
                return;
            }

            int? volunteerTuid = null;

            if (cmbSelectVolunteer.SelectedValue != null)
            {
                volunteerTuid = _viewModel.VolunteerTuid;
                GetVolunteerTempInfo();
            }
            else
            {
                ClearTempInfo();
            }

            DataContext = null;
            _viewModel = new VolunteerGeneralViewModel(_volunteerProvider, _schoolProvider);
            DataContext = _viewModel;

            if (volunteerTuid.HasValue)
            {
                _viewModel.VolunteerTuid = volunteerTuid.Value;
            }         
            

            blnNeedsRefresh = false;
            

            return;
        }

        /// <summary>
        /// Populates any temporary information a Volunteer has
        /// </summary>
        /// <returns> None </returns>
        /// <author> Isabelle Johns </author>
        /// <created> 3/27/23 </created>
        public void PopulateVolunteerTempInfo()
        {
            IEnumerable<TemporaryInfoModel> temporaryInfo = _volunteerProvider.GetVolunteerTemporaryInfo((int)cmbSelectVolunteer.SelectedValue);
            if (errorFlag) { errorFlag = false; return; }

            ClearTempInfo();

            int numRow = 1;

            foreach(TemporaryInfoModel tempInfo in temporaryInfo)
            {
                object ctrlCurrent = this.FindName("ctrlValue" + numRow);

                if (ctrlCurrent is DatePicker dtpCurrent)
                {
                    if (tempInfo.Value != null)
                        dtpCurrent.SelectedDate = DateTime.Parse(tempInfo.Value);
                }
                else if (ctrlCurrent is CheckBox chkCurrent)
                {
                    if (tempInfo.Value != null)
                        chkCurrent.IsChecked = bool.Parse(tempInfo.Value);
                }

                numRow++;
            }
        }

        /// <summary>
        /// Clears previous temporary information
        /// </summary>
        /// <returns> None </returns>
        /// <author> Isabelle Johns </author>
        /// <created> 3/27/23 </created>
        public void ClearTempInfo()
        {
            foreach (DatePicker datePicker in FindChildrenControls<DatePicker>(grdTempInfo))
            {
                datePicker.SelectedDate = null;
            }

            foreach (CheckBox checkBox in FindChildrenControls<CheckBox>(grdTempInfo))
            {
                checkBox.IsChecked = false;
            }
        }

        /// <summary>
        /// Initializes the temporary information card with entries in the database
        /// </summary>
        /// <returns> None </returns>
        /// <author> Christopher Washburn </author>
        /// <created> 2/19/23 </created>
        /// <lastModified> 2/28/23 </lastModified>
        /// <lastModifiedBy> Isabelle Johns </lastModifiedBy>
        public void PopulateTemporaryInformation()
        {
            IEnumerable<TemporaryInfoModel> tempInfoList = _volunteerProvider.GetTemporaryInfo();
            if (errorFlag) { errorFlag = false; return; }

            int numRow = 1;

            foreach (var tempInfo in tempInfoList)
            {
                TextBlock txtLabel = new()
                {
                    Text = tempInfo.Name
                };

                Grid.SetColumn(txtLabel, 0);        // set to first column
                Grid.SetRow(txtLabel, numRow);      // set to Nth row
                grdTempInfo.Children.Add(txtLabel); // add to grid

                Divider divider = new();
                Grid.SetRow(divider, numRow);
                grdTempInfo.Children.Add(divider);  // add divider under row

                string ctrlName = "ctrlValue" + numRow; //Dynamic name to store tuid in tag

                if (tempInfo.Type == TempInfoTypes.Date)    //Type is Date
                {
                    DatePicker datePicker = new();
                    RegisterName(ctrlName, datePicker);
                    datePicker.Tag = tempInfo.Tuid;
                    Grid.SetRow(datePicker, numRow);
                    grdTempInfo.Children.Add(datePicker);
                }
                else
                {   
                    CheckBox checkBox = new();              //Type is Bool
                    RegisterName(ctrlName, checkBox);
                    checkBox.Tag = tempInfo.Tuid;
                    Grid.SetRow(checkBox, numRow);
                    grdTempInfo.Children.Add(checkBox);
                }

                numRow++;
            }
            
        }

        /// <summary>
        /// Returns a list of the temporary info associated with the selected volunteer
        /// </summary>
        /// <returns> a list of the temporary info associated with the selected volunteer </returns>
        /// <author> Isabelle Johns </author>
        /// <created> 3/26/23 </created>
        public IEnumerable<TemporaryInfoModel> GetVolunteerTempInfo()
        {
            List<TemporaryInfoModel> tempInfo = new();


            foreach (DatePicker datePicker in FindChildrenControls<DatePicker>(grdTempInfo))
            {
                if (datePicker.SelectedDate.HasValue)
                {
                    tempInfo.Add(new TemporaryInfoModel
                    {
                        Tuid = (int)datePicker.Tag,
                        Value = datePicker.SelectedDate.Value.ToString("d")
                    });
                }
            }

            foreach (CheckBox checkBox in FindChildrenControls<CheckBox>(grdTempInfo))
            {
                tempInfo.Add(new TemporaryInfoModel
                {
                    Tuid = (int)checkBox.Tag,
                    Value = checkBox.IsChecked.ToString()
                });
            }

            return tempInfo;

        }

        /// <summary>
        /// Handles the volunteer selection being changed. If the selection is not null
        /// it will refresh data onto the screen
        /// </summary>
        /// <author> Christopher Washburn </author>
        /// <created> 2/19/23 </created>
        /// <lastModified> 3/31/23 </lastModified>
        /// <lastModifiedBy> Isabelle Johns </lastModifiedBy>
        private void cmbSelectVolunteer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Application.Current.Properties["VolunteerTuid"] = cmbSelectVolunteer.SelectedIndex;

            if(cmbSelectVolunteer.SelectedValue != null)
            {
                PopulateVolunteersGeneralData();
                PopulateVolunteerTempInfo();
            }
            else
            {
                RefreshData();
            }
            
        }

        /// <summary>
        /// Limits text input on a text box to digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void txtDigitOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool isDigit = char.IsDigit(e.Text, 0);

            if (!isDigit)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Validates the Datepickers on the screen. Will not allow a null startdate
        /// or an end date that is before the startdate
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private bool ValidateDates()
        {
            bool hasDateError = false;

            if (!dprStartDate.SelectedDate.HasValue)
            {
                hasDateError = true;
                Growl.Error(new GrowlInfo
                {
                    Message = "Start Date cannot be empty",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });
            }
            else if (btnStatus.IsChecked == false)
            {
                if (DateTime.Compare(_viewModel.EndDate!.Value, _viewModel.StartDate) < 0)
                {
                    hasDateError = true;
                    Growl.Error(new GrowlInfo
                    {
                        Message = "End Date cannot be before Start Date",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3
                    });
                }
            }

            return hasDateError;
        }

        #endregion


    }
}
