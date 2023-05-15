using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> IWindowProvider.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 3/25/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 3/25/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The interface for the WindowProvider
/// </summary>
/// <author> Tyler Moody </author>
/// 
namespace B_FGMS.BusinessLogic.Services.WindowProviders
{
    public interface IWindowProvider
    {
        void CloseWindow();
    }
}
