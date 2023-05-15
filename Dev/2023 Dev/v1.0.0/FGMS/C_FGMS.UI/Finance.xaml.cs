using B_FGMS.BusinessLogic;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Models;
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
    /**
     ************************************************************************************************************************
     *                                      File Name : Finance                                                             *
     *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
     ************************************************************************************************************************
     *                                      Created By : Andrew Loesel                                                      *
     *                                      Date Created : 1/26/23                                                          *
     *                                      Additional Contributors : (none)                                                *
     ************************************************************************************************************************
     *                                                  File Purpose                                                        *
     ************************************************************************************************************************
     * The purpose of this file is to provide the interaction logic for the Finance page of the application.                *
     ************************************************************************************************************************
     *                                           Global Variable Dicationary                                                *
     ************************************************************************************************************************
     * (none)                                                                                                               *
     ************************************************************************************************************************
     *                                              Modification History                                                    *
     ************************************************************************************************************************
     * Andrew Loesel : 02/26/2023 - created private fields for each individual page associated with a tab so that new pages *
     *                  will not have to be created each time a tab is navigated to.                                        *
     ************************************************************************************************************************
     */
    public partial class Finance : Page
    {
        private readonly IServiceProvider _serviceProvider;

        /**
        ************************************************************************************************************************
        *                                     Function Name : Finance                                                          *
        ************************************************************************************************************************
        *                                      Created By : Andrew Loesel                                                      *
        *                                      Date Created : 1/26/23                                                          *
        *                                      Additional Contributors : (none)                                                *
        ************************************************************************************************************************
        *                                                Function Purpose                                                      *
        ************************************************************************************************************************
        * This function runs when an instance of Finance.xaml is created. This function will initialize the page component.    *
        ************************************************************************************************************************
        *                                                   Parameters                                                         *
        ************************************************************************************************************************
        * (None)                                                                                                               *
        ************************************************************************************************************************
        *                                                    Returns                                                           *
        ************************************************************************************************************************
        * (None)                                                                                                               *
        ************************************************************************************************************************
        *                                               Variable Dicationary                                                   *
        ************************************************************************************************************************
        * (None)                                                                                                               *
        ************************************************************************************************************************
        *                                               Modification History                                                   *
        ************************************************************************************************************************
        * (None)                                                                                                               *
        ************************************************************************************************************************
        */
        public Finance(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
        }
        /**
        ************************************************************************************************************************
        *                                     Function Name : FinanceTabControlSelectionChanged                                *
        ************************************************************************************************************************
        *                                      Created By : Isabelle Johns                                                     *
        *                                      Date Created : 1/24/23                                                          *
        *                                      Additional Contributors : (none)                                                *
        ************************************************************************************************************************
        *                                                Function Purpose                                                      *
        ************************************************************************************************************************
        * Function Purpose : The Purpose of this function is to change between subpages on click of a tabcontrol item.         *
        ************************************************************************************************************************
        *                                                   Parameters                                                         *
        ************************************************************************************************************************
        * (None)                                                                                                               *
        ************************************************************************************************************************
        *                                                    Returns                                                           *
        ************************************************************************************************************************
        * (None)                                                                                                               *
        ************************************************************************************************************************
        *                                               Variable Dicationary                                                   *
        ************************************************************************************************************************
        * (None)                                                                                                               *
        ************************************************************************************************************************
        *                                               Modification History                                                   *
        ************************************************************************************************************************
        * Andrew Loesel : 01/26/2023 - altered function for use with financial pages (was originally for volunteer)            *
        * Andrew Loesel : 02/26/2023 - altered function so that a new page will only be created for a tab the first time the   *
        *                       tab is navigated to while the finance page is open. Also using selectedItem instead of looping *
        *                       through each item of the tab control.                                                          *
        ************************************************************************************************************************
        */
        private void FinanceTabControlSelectionChanged(object sender, RoutedEventArgs e)
        {
            TabItem selectedItem = FinanceTabControl.SelectedItem as TabItem;
            string selectedHeader = selectedItem.Header as string;

            switch (selectedHeader)
            {
                case "General":
                    FinanceMainFrame.Navigate(_serviceProvider.GetRequiredService<FinanceGeneralPage>());
                    break;
                case "Meal And Transport":
                    FinanceMainFrame.Navigate(_serviceProvider.GetRequiredService<FinanceMealAndTransportPage>());
                    break;
                case "PTO":
                    FinanceMainFrame.Navigate(_serviceProvider.GetRequiredService<FinancePTOPage>());
                    break;
                case "Fiscal Year":
                    NavigateToFinanceYearPageBase(true);
                    break;
                case "Grant Year":
                    NavigateToFinanceYearPageBase(false);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Initialize the FinanceYearPage depending on if it is fiscal or grant year.     
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>04/01/2023</created>
        private void NavigateToFinanceYearPageBase(bool isFiscalYear)
        {
            FinanceMainFrame.Navigate(_serviceProvider.GetRequiredService<FinanceYearPageBase>());

            var financeYearPage = _serviceProvider.GetRequiredService<FinanceYearPageBase>();

            financeYearPage.isFiscalYear = isFiscalYear;
            financeYearPage.RefreshPage();

            FinanceMainFrame.Navigate(financeYearPage);
        }
    }
}
