using B_FGMS.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> ConfirmDeleteCommand.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/21/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/21/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this file is to handle the delete command being called.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.Commands
{
    public class ConfirmDeleteCommand : CommandBase
    {
        private readonly ViewModelBase _viewModel;

        public ConfirmDeleteCommand(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Calls the view models delete function to remove the selected item.
        /// </summary>
        /// <param name="parameter"></param>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        public override void Execute(object? parameter)
        {
            try
            {
                _viewModel.ConfirmDelete();
            }
            catch (Exception)
            {
                _viewModel.ActionFailed("Failed to delete record. Please contact support if issue persists.", "Error");
            }
        }
    }
}
