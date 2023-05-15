using B_FGMS.BusinessLogic.ViewModels;

/// <FileName> _exportCommand.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/23/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/23/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this file is to call the view models export.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.Commands
{
    public class ExportCommand : CommandBase
    {
        private readonly ViewModelBase _viewModel;

        public ExportCommand(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Calls the view models export function.
        /// </summary>
        /// <param name="parameter"></param>
        /// <author>Tyler Moody</author>
        /// <created>02/21/2023</created>
        public override void Execute(object? parameter)
        {
            try
            {
                _viewModel.Export();
            }
            catch (Exception)
            {
                _viewModel.ActionFailed("Failed to Export records. Please contact support if issue persists.", "Error");
            }
        }
    }
}
