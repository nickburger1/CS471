using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using B_FGMS.BusinessLogic.ViewModels.VolunteerAddViewModels;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Window = System.Windows.Window;

namespace C_FGMS.UI
{
    /// <FileName> AddVolunteerWindow.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
    /// <DateCreated> 2/26/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 3/31/23 </LastModified>
    /// <LastModifiedBy> Isabelle Johns </LastModifiedBy>
    /// <summary>
    /// The Purpose of this file bind the Add Volunteer Window to backend operations.
    /// </summary>
    /// <author> Isabelle Johns </author>
    public partial class AddVolunteerWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly VolunteerAddViewModel _viewModel;
        public bool isClosing = false;
        private bool errorFlag;

        public AddVolunteerWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _viewModel = new VolunteerAddViewModel();

            _volunteerProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            InitializeComponent();
            PopulateDropdowns();

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
        /// Handles OnClick event for Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/26/23 </created>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles OnClick event for Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/26/23 </created>
        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ValidateAll();
            if(!ValidateDates() && !_viewModel.HasErrors)
            {
                _volunteerProvider.PushNewVolunteer(_viewModel);
                if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
                isClosing = true;
                DialogResult = true;
            }       
        }

        /// <summary>
        /// Overrides the OnClosing event that occurs when the user
        /// closes the window or presses the cancel button to prompt
        /// the user if any changes where made
        /// </summary>
        /// <param name="e"> Closing Event </param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/30/23 </created>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (isClosing == true)
            {
                e.Cancel = false;
                return;
            }

            if (!_viewModel.FormChanged)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                Growl.Ask("Changes will not be saved. Do you still want to close?", isConfirmed =>
                {
                    if(isConfirmed == true)
                    {
                        isClosing = true;
                        DialogResult = false;
                    }

                    return true;
                });
            }
        }

        /// <summary>
        /// Initializes the dropdowns on the page by setting
        /// their item source
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/30/23 </created>
        private void PopulateDropdowns()
        {
            SetCmbGenderSource();
            SetCmbEthnicitySource();
            SetCmbRacialGroupSource();
            SetCmbIdentifiesAsSource();
            SetCmbStateSource();
        }

        /// <summary>
        /// Sets the item source of the Genders Dropdown
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/30/23 </created>
        private void SetCmbGenderSource()
        {
            var Genders = _volunteerProvider.GetGenderNameAndId(false);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbGender.ItemsSource = Genders;
        }

        /// <summary>
        /// Sets the item source of the Ethnicity Dropdown
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/30/23 </created>
        private void SetCmbEthnicitySource()
        {
            var Ethnicities = _volunteerProvider.GetEthnityNameAndId(false);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbEthnicity.ItemsSource = Ethnicities;
        }

        /// <summary>
        /// Sets the item source of the Racial Group Dropdown
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/30/23 </created>
        private void SetCmbRacialGroupSource()
        {
            var RacialGroups = _volunteerProvider.GetRacialGroupNameAndId(false);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbRacialGroup.ItemsSource = RacialGroups;
        }

        /// <summary>
        /// Sets the item source of the Identifies As Dropdown
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/30/23 </created>
        private void SetCmbIdentifiesAsSource()
        {
            var IdentifiesAs = _volunteerProvider.GetIdentifiesAsNameAndId(false);
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbIdentifiesAs.ItemsSource = IdentifiesAs;
        }

        /// <summary>
        /// Sets the item source of the State Dropdown
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/30/23 </created>
        private void SetCmbStateSource()
        {
            cmbState.ItemsSource = StateHelper.States();
        }

        /// <summary>
        /// Manually validates all datepickers on the screen to check for
        /// null values. If a datepicker has a null value, it displays a growl
        /// </summary>
        /// <returns> if any dates have errors</returns>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private bool ValidateDates()
        {
            bool hasDateError = false;

            if (!dprStartDate.SelectedDate.HasValue)
            {
                Growl.Error(new GrowlInfo
                {
                    Message = "Start Date is a Required Field",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });

                hasDateError = true;
            }   

            if (!dprDateOfBirth.SelectedDate.HasValue)
            {
                Growl.Error(new GrowlInfo 
                { 
                    Message = "Date of Birth is a Required Field", 
                    ShowDateTime = false, 
                    StaysOpen = false, 
                    WaitTime = 3 
                });

                hasDateError = true;
            }

            if (DateTime.Compare(_viewModel.DateOfBirth, _viewModel.StartDate) >= 0)
            {
                Growl.Error(new GrowlInfo
                {
                    Message = "Date of Birth cannot be on or after Start Date",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });

                hasDateError = true;
            }
            
            if(DateTime.Compare(_viewModel.DateOfBirth, DateTime.Today) >= 0)
            {
                Growl.Error(new GrowlInfo
                {
                    Message = "Date of Birth cannot be on or after today",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3
                });

                hasDateError = true;
            }

            return hasDateError;

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

        /// <summary>
        /// Handles the on load method for the Gender dropdown.
        /// Defaults the selection to the 1st index
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void cmbGender_Loaded(object sender, RoutedEventArgs e)
        {
            cmbGender.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the on load method for the Identifies As dropdown.
        /// Defaults the selection to the 1st index
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void cmbIdentifiesAs_Loaded(object sender, RoutedEventArgs e)
        {
            cmbIdentifiesAs.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the on load method for the Ethnicity dropdown.
        /// Defaults the selection to the 1st index
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void cmbEthnicity_Loaded(object sender, RoutedEventArgs e)
        {
            cmbEthnicity.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the on load method for the RacialGroup dropdown.
        /// Defaults the selection to the 1st index
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void cmbRacialGroup_Loaded(object sender, RoutedEventArgs e)
        {
            cmbRacialGroup.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the on load method for the State dropdown.
        /// Defaults the selection to Michigan
        /// </summary>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void cmbState_Loaded(object sender, RoutedEventArgs e)
        {
            cmbState.SelectedIndex = 22;
        }

        /// <summary>
        /// Limits text input on a text box to digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool isDigit = char.IsDigit(e.Text, 0);

            if (!isDigit)
            {
                e.Handled = true;
            }
        }

    }
}
