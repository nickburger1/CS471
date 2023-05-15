using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.ActivityProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.WindowProviders;
using B_FGMS.BusinessLogic.ViewModels;
using B_FGMS.BusinessLogic.ViewModels.ActivityLogViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace C_FGMS.UI
{
    /// <FileName> AddActivityLog.xaml.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
    /// <DateCreated> 03/05/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 03/05/2023 </LastModified>
    /// <LastModifiedBy> Tyler Moody </LastModifiedBy>
    /// <summary>
    /// This provides the window for adding an activity log.
    /// </summary>
    /// <author> Tyler Moody </author>
    public partial class AddActivityLog : Window
    {
        private readonly ActivityLogViewModel _activityLogViewModel;
        private readonly AddActivityLogViewModel _addActivityLogViewModel;
        private readonly IDialogProvider _dialogProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider">Holds services.</param>
        /// <param name="activityLogViewModel">Parent view model.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/05/2023</created>
        public AddActivityLog(IServiceProvider serviceProvider, ActivityLogViewModel activityLogViewModel)
        {
            InitializeComponent();

            _activityLogViewModel = activityLogViewModel;

            WindowProvider window = new WindowProvider(this); 

            _addActivityLogViewModel = new AddActivityLogViewModel
            (
                serviceProvider.GetRequiredService<IActivityLogProvider>(),
                serviceProvider.GetRequiredService<IDialogProvider>(),
                window,
                _activityLogViewModel,
                _activityLogViewModel.SelectedVolunteer
            );

            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

            DataContext = _addActivityLogViewModel;
        }


        /// <summary>
        /// Confirm that user wants to exit and then exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/05/2023</created>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (DateUnchanged() && InititalUnchanged() && IncidentUnchanged())
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
                _activityLogViewModel.SelectedActivityLog = null;
                this.Close();
            }
        }

        /// <summary>
        /// Check if date was edited.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/15/2023</created>
        /// <returns>True if date was not edited.</returns>
        private bool DateUnchanged()
        {
            return _addActivityLogViewModel.DateBeforeEdit.Date == _addActivityLogViewModel.NewDate.Date;
        }

        /// <summary>
        /// Check if initial was edited.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/15/2023</created>
        /// <returns>Return true if not edited.</returns>
        private bool InititalUnchanged()
        {
            return _addActivityLogViewModel.NewInitial == "";
        }

        /// <summary>
        /// Check if incident was edited.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/15/2023</created>
        /// <returns>Return true if not edited.</returns>
        private bool IncidentUnchanged()
        {
            return _addActivityLogViewModel.NewIncident == "";
        }
    }
}
