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

/**
 ************************************************************************************************************************
 *                                      File Name : VolunteersPage.xaml.cs                                              *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Jon Maddocks                                                       *
 *                                      Date Created : 1/20/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 1/29/23                                                         *
 *                                      Last Modified By : Richard Nader, Jr.                                           *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the interaction logic for our VolunteerPage.xaml file.         *
 ************************************************************************************************************************
 **/

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for VolunteersPage.xaml
    /// </summary>
    public partial class VolunteersPage : Page
    {
        private readonly IServiceProvider _serviceProvider;

        public VolunteersPage(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            volunteerMainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteerGeneral>());
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
        }

        /**
        ************************************************************************************************************************
        *                                      Function Name : TabControl_SelectionChanged                                     *
        ************************************************************************************************************************
        *                                      Created By : Isabelle Johns                                                     *
        *                                      Date Created : 1/24/23                                                          *
        *                                      Additional Contributors : CS471 WI23 Development Team                           *
        *                                      Last Modified : 1/29/23                                                         *
        *                                      Last Modified By : Richard Nader, Jr.                                           *
        ************************************************************************************************************************
        * Function Purpose : The Purpose of this function is to change between subpages on click of a tabcontrol item.         *
        ************************************************************************************************************************
        **/
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (TabItem item in TabControl.Items)
            {
                if (item.IsSelected)
                {
                    switch (item.Header)
                    {
                        case "General":
                            volunteerMainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteerGeneral>());
                            break;
                        case "Demographics":
                            volunteerMainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteerDemographics>());
                            break;
                        case "Financials":
                            volunteerMainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteerFinancials>());
                            break;
                        case "Classrooms":
                            volunteerMainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteerClassrooms>());
                            break;
                        case "Child Assignments":
                            volunteerMainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteerChildAssignments>());
                            break;
                        case "Activity Log":
                            volunteerMainFrame.Navigate(_serviceProvider.GetRequiredService<VolunteerActivityLog>());
                            break;
                    }
                }
            }
        }
    }
}
