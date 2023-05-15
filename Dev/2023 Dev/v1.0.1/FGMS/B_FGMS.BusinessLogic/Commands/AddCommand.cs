using B_FGMS.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> AddCommand.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/21/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/23/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this file is to handle calling a view models add method.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.Commands
{
    public class AddCommand : CommandBase
    {
        private readonly ViewModelBase _viewModel;

        public AddCommand(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Calls the view models add function to add a new item.
        /// </summary>
        /// <param name="parameter"></param>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        public override void Execute(object? parameter)
        {
            try
            {
                _viewModel.Validate();

                if (!_viewModel.HasErrors)
                {
                    _viewModel.Add();
                }
            }
            catch (Exception)
            {
                _viewModel.ActionFailed("Failed to Add record. Please contact support if issue persists.", "Error");
            }
        }
    }
}
