using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels;
using B_FGMS.BusinessLogic.ViewModels.VolunteerInfoViewModels;
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
using System.Windows.Shapes;

namespace C_FGMS.UI
{
    /// <FileName> ReportsVolunteerInfoPageEdit.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
    /// <DateCreated> 3/25/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 3/25/2023 </LastModified>
    /// <LastModifiedBy> Tyler Moody </LastModifiedBy>
    /// <summary>
    /// The purpose of this file is to interact with the ReportsVolunteerInfoPageEdit.xaml
    /// </summary>
    /// <author> Tyler Moody </author>
    public partial class ReportsVolunteerInfoPageEdit : Window
    {
        private readonly VolunteerInfoViewModel _volunteerInfoViewModel;
        private readonly UpdateVolunteerInfoViewModel _updateVolunteerViewModel;
        private readonly IDialogProvider _dialogProvider;

        /// <summary>
        /// Constructor for the window.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="volunteerInfoViewModel"></param>
        public ReportsVolunteerInfoPageEdit(IServiceProvider serviceProvider, VolunteerInfoViewModel volunteerInfoViewModel)
        {
            InitializeComponent();

            _volunteerInfoViewModel = volunteerInfoViewModel;
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

             _updateVolunteerViewModel = new UpdateVolunteerInfoViewModel(
                serviceProvider.GetRequiredService<IVolunteerProvider>(),
                volunteerInfoViewModel);

            DataContext = _updateVolunteerViewModel;
        }

        /// <summary>
        /// Save the volunteer info and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close the form without saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_updateVolunteerViewModel.FormUnchanged())
            {
                this.Close();
            }
            else
            {
                ConfirmClose();
            }
        }

        /// <summary>
        /// Close if user confirms with yes.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/17/2023</created>
        private void ConfirmClose()
        {
            bool? closeConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to exit? Changes won't be saved.", "Confirmation");

            if (closeConfirmed == true)
            {
                _volunteerInfoViewModel.SelectedVolunteerInfo = null;
                this.Close();
            }
        }

        /// <summary>
        /// Set the default combobox index on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private void comboGender_Loaded_Gender(object sender, RoutedEventArgs e)
        {
            comboGender.SelectedIndex = _updateVolunteerViewModel.initialGenderIndex;
            _updateVolunteerViewModel.formChanged = false;
        }

        /// <summary>
        /// Set the default combobox index on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private void ComboBox_Loaded_Identifies(object sender, RoutedEventArgs e)
        {
            comboIdentifies.SelectedIndex = _updateVolunteerViewModel.initialIdentifiesIndex;
            _updateVolunteerViewModel.formChanged = false;
        }

        /// <summary>
        /// Set the default combobox index on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private void ComboBox_Loaded_Ethnicity(object sender, RoutedEventArgs e)
        {
            comboEthnicity.SelectedIndex = _updateVolunteerViewModel.initialEthnicityIndex;
            _updateVolunteerViewModel.formChanged = false;
        }

        /// <summary>
        /// Set the default combobox index on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private void ComboBox_Loaded_RacialGroup(object sender, RoutedEventArgs e)
        {
            comboRacialGroup.SelectedIndex = _updateVolunteerViewModel.initialRacialGroupIndex;
            _updateVolunteerViewModel.formChanged = false;
        }

        /// <summary>
        /// Set the default combobox index on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        private void ComboBox_Loaded_Separated(object sender, RoutedEventArgs e)
        {
            comboSeparated.SelectedIndex = _updateVolunteerViewModel.initialReasonSeparatedIndex;
            _updateVolunteerViewModel.formChanged = false;
        }
    }
}
