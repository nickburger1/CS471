using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <FileName> WindowProvider.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/21/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 3/255/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The purpose of this file is to hold a window object and perform it's methods through a service.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.Services.WindowProviders
{
    public class WindowProvider : IWindowProvider
    {
        private readonly Window _window;

        public WindowProvider(Window window)
        {
            _window = window;
        }

        /// <summary>
        /// Calls the windows close method.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/25/2023</created>
        public void CloseWindow()
        {
            _window.Close();
        }
    }


}
