using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> IDiaglogProvider.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 03/14/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/14/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// Provide interface for dialog boxes.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.Services.DialogProvider
{
    public interface IDialogProvider
    {
        bool? ShowConfirmationDialog(string message, string caption);
        void ShowAlertDialog(string message, string caption);
    }
}
