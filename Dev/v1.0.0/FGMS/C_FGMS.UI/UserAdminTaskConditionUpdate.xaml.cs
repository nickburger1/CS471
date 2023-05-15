using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using B_FGMS.BusinessLogic.ViewModels.AdminTaskViewModels;
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
    /// <FileName> UserAdminTaskConditionUpdate.xaml.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
    /// <DateCreated> 04/12/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 04/12/2023 </LastModified>
    /// <LastModifiedBy> Tyler Moody </LastModifiedBy>
    /// <summary>
    /// Provides interaction logic for UserAdminTaskConditionUpdate.xaml
    /// </summary>
    /// <author> Tyler Moody </author>
    public partial class UserAdminTaskConditionUpdate : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDialogProvider _dialogProvider;
        private readonly ConditionsViewModel _conditionViewModel;
        private UpdateConditionViewModel _updateConditionViewModel;
        public UserAdminTaskConditionUpdate(IServiceProvider serviceProvider, ConditionsViewModel conditionsViewModel)
        {
            _serviceProvider = serviceProvider;
            _conditionViewModel = conditionsViewModel;
            _dialogProvider = _serviceProvider.GetRequiredService<IDialogProvider>();

            _updateConditionViewModel = new UpdateConditionViewModel(
                _serviceProvider.GetRequiredService<IStudentProvider>(),
                _serviceProvider.GetRequiredService<IDialogProvider>(),
                _conditionViewModel
            );

            InitializeComponent();

            DataContext = _updateConditionViewModel;
        }

        /// <summary>
        /// Confirm that user wants to exit and then exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (AcronymUnchanged() && DescriptionUnchanged())
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
        /// <created>04/12/2023</created>
        private void ConfirmClose()
        {
            bool? closeConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to exit? Changes won't be saved.", "Confirmation");

            if (closeConfirmed == true)
            {
                _conditionViewModel.SelectedCondition = null;
                this.Close();
            }
        }

        /// <summary>
        /// Check if acronym was edited.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        /// <returns>Return true if not edited.</returns>
        private bool AcronymUnchanged()
        {
            return _conditionViewModel.SelectedCondition.Acronym == _updateConditionViewModel.Acronym;
        }

        /// <summary>
        /// Check if description was edited.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/12/2023</created>
        /// <returns>Return true if not edited.</returns>
        private bool DescriptionUnchanged()
        {
            return _conditionViewModel.SelectedCondition.Description == _updateConditionViewModel.Description;
        }

        /// <summary>
        /// Exit window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/05/2023</created>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _conditionViewModel.SelectedCondition = null;
            this.Close();
        }
    }
}
