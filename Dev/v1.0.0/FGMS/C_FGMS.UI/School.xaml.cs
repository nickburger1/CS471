

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
/**
************************************************************************************************************************
*                                      File Name : School.xaml.cs                                                      *
*                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
************************************************************************************************************************
*                                      Created By : Chippi                                                             *
*                                      Date Created :  /  /23                                                          *
*                                      Additional Contributors : CS471 WI23 Development Team                           *
*                                      Last Modified : 2/17/23                                                         *
*                                      Last Modified By : Kiefer Thorson                                               *
************************************************************************************************************************
* File Purpose : The Purpose of this file is to show all the schools in the database                                   *
************************************************************************************************************************
* Modification Log:                                                                                                    *
* Author: Kiefer Thorson                                                                                               *
* Date: 2/17/2023                                                                                                      *
* Description: Connected Backend to perSchools page so datagrid and labels read from database                          *
*              Updated Commenting style                                                                                *
************************************************************************************************************************
**/
namespace C_FGMS.UI
{
    /// <summary>
    /// Class Name: School
    /// Created By: Chippi
    /// Date Created:  / /2023
    /// Additional Contributors: Kiefer Thorson
    /// Last Modified: 2/17/2023
    /// Last Modified By: Kiefer Thorson
    /// 
    /// Purpose:
    /// The purpose of this class is to populate the Schools page for the All Schools and Per Schools tab
    /// </summary>
    public partial class School : Page
    {

        private readonly IServiceProvider _serviceProvider;

        public School(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            schoolMainFrame.Navigate(serviceProvider.GetRequiredService<SchoolsAllSchools>());
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
        }

        private void tabSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedItem = tabSchool.SelectedItem as TabItem;
            string selectedHeader = selectedItem.Header as string;

            switch (selectedHeader)
            {
                case "All Schools":
                    schoolMainFrame.Navigate(_serviceProvider.GetRequiredService<SchoolsAllSchools>());
                    break;
                case "Per School":
                    schoolMainFrame.Navigate(_serviceProvider.GetRequiredService<SchoolPerSchoolPage>());
                    break;
                default:
                    break;
            }
        }
    }
}
