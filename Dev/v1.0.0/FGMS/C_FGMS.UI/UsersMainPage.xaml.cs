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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for UsersMainPage.xaml
    /// </summary>
    public partial class UsersMainPage : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Users _users;
        private readonly UsersAdminTasksPage _adminPage;
        

        public UsersMainPage(
            IServiceProvider serviceProvider,
            Users users,
            UsersAdminTasksPage adminPage)
        {
            _serviceProvider = serviceProvider;
            _users = users;
            _adminPage = adminPage;

            InitializeComponent();
            usersMainFrame.Navigate(_users);
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (TabItem item in TabControl.Items)
            {
                if (item.IsSelected)
                {
                    switch (item.Header)
                    {
                        case "Users":
                            usersMainFrame.Navigate(_users); break;
                        case "Admin Tasks":
                            usersMainFrame.Navigate(_adminPage); break;
                    }
                }
            }
        }
    }
}
