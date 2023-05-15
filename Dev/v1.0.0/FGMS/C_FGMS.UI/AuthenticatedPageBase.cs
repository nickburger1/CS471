using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace C_FGMS.UI
{
    public abstract class AuthenticatedPageBase : Page
    {
        public UserModel? LoggedInUser
        {
            get
            {
                // Get the main window reference (parent) and call the OnUserLoggedIn method
                MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
                return mainWindow?.LoggedInUser;
            }
        }

    }
}
