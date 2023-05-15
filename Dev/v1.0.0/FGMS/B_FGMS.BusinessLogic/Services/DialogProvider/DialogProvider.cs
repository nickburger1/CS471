using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <FileName> DiaglogProvider.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 03/14/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/14/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this file is to provide various dialogs for the project.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.Services.DialogProvider
{
    public class DialogProvider : IDialogProvider
    {
        /// <summary>
        /// Display the a confirm dialog box.
        /// </summary>
        /// <param name="message">_retrieveErrorMessage displayed.</param>
        /// <param name="caption">Caption displayed.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/14/2023</created>
        /// <returns>True if yes is selected. False if no or window is closed.</returns>
        public bool? ShowConfirmationDialog(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes ? true : false;
        }

        /// <summary>
        /// Display the a alert dialog box.
        /// </summary>
        /// <param name="message">_retrieveErrorMessage displayed.</param>
        /// <param name="caption">Caption displayed.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        public void ShowAlertDialog(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
