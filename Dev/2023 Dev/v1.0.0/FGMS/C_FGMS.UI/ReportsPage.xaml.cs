using Microsoft.Extensions.DependencyInjection;
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
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportsPage(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            reportsMainFrame.Navigate(_serviceProvider.GetRequiredService<ReportsReportBuilderPage>());
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
                        case "Report Builder":
                            reportsMainFrame.Navigate(_serviceProvider.GetRequiredService<ReportsReportBuilderPage>());
                            break;
                        case "Annual Check":
                            reportsMainFrame.Navigate(_serviceProvider.GetRequiredService<ReportsAnnualCheckPage>());
                            break;
                        case "Volunteer Info":
                            reportsMainFrame.Navigate(_serviceProvider.GetRequiredService<ReportsVolunteerInfoPage>());
                            break;
                    }
                }
            }
        }
    }
}
