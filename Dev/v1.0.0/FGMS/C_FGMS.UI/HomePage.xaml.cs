using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Services.AssignmentProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;

/**
 ************************************************************************************************************************
 *                                      File Name : HomePage.xaml.cs                                                    *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson                                                     *
 *                                      Date Created : 1/31/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 2/10/23                                                         *
 *                                      Last Modified By : Kiefer Thorson & Nathan VanSnepson                           *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to set the Charts on the HomePage                                         *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author: Kiefer Thorson & Nathan VanSnepson                                                                           *
 * Date: 2/10/2023                                                                                                      *
 * Description: Updated pie chart and bar graph logic to go against the database.                                       *
 *                                                                                                                      *
 * Author: Kiefer Thorson                                                                                               *
 * Date: 2/15/2023                                                                                                      *
 * Description: Changed providers to be loaded at runtime and declared in the constructor                               *
 *                                                                                                                      *
 ************************************************************************************************************************
 **/
namespace C_FGMS.UI
{
    /// <summary>
    /// Class Name: HomePage
    /// Created By: Kiefer Thorson
    /// Date Created: 2/4/2023
    /// Additional Contributors: Nathan VanSnepson
    /// Last Modified: 2/10/2023
    /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
    /// 
    /// Purpose:
    /// The purpose of this class is to stage the scene and set the pie chart and bar graph values from the database to appear on the home screen
    /// </summary>
    public partial class HomePage : AuthenticatedPageBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IAssignmentProvider _assignmentProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private bool errorFlag;


        private List<Status> volStatus { get; set; }

        /// <summary>
        /// Function Name: HomePage
        /// Created By: Kiefer Thorson
        /// Date Created: 2/4/2023
        /// Additional Contributors: Nathan VanSnepson
        /// Last Modified: 2/15/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Call the method to draw the BAR GRAPH
        ///     - Call the method to draw the PIE CHART
        /// </summary>
        /// <param name="serviceProvider"></param>
        public HomePage(
            IServiceProvider serviceProvider,
            IVolunteerProvider volunteerProvider,
            IAssignmentProvider assignmentProvider,
            DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _volunteerProvider = volunteerProvider;
            _assignmentProvider = assignmentProvider;
            _refreshEventBroker = refreshEventBroker;

            _volunteerProvider.DatabaseError += ErrorHandler;
            _assignmentProvider.DatabaseError += ErrorHandler;

            errorFlag = false;

            // Stage scene
            InitializeComponent();

            _refreshEventBroker.Subscribe((args, x) =>
            {
                PieChart();
                BarGraph();
            });

            try
            {
                PieChart();
                BarGraph();
            }catch(RefreshDataCustomException rdce)
            {

            }
        }


        /// <summary>
        /// Error provider for the UserServiceProvider. All functionality to handle
        /// business logic errors for the Users.xaml page are called in this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void ErrorHandler(object sender, ErrorEventArgs e)
        {
            errorFlag = true;
            System.Windows.MessageBox.Show(e.ErrorMessage, "Database Error " + e.ErrorCode, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Function Name: PieChart
        /// Created By: Kiefer Thorson
        /// Date Created: 2/4/2023
        /// Additional Contributors: Nathan VanSnepson
        /// Last Modified: 2/15/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to draw the PIE CHART based on the database contents
        /// </summary>
        private void PieChart()
        {

            //clear the prior pie chart if any
            pieCanvas.Children.Clear();
            // This contains the PIE CHART setup for active/inactive vols and their corresponding percentages
            float pieWidth = 300, pieHeight = 300, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            pieCanvas.Width = pieWidth;
            pieCanvas.Height = pieHeight;

            // Retrieve collection of Volunteers
            int intActiveVolunteers = _volunteerProvider.GetActiveVolunteersCount();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            int intInactiveVolunteers = _volunteerProvider.GetInactiveVolunteersCount();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            int intTotalActiveInactiveVolunteers = intActiveVolunteers + intInactiveVolunteers;

            // Connect database here to read active and inactive vols into each Status
            volStatus = new List<Status>()
            {
                // Adjust values for Active volunteers in first Status
                new Status
                {
                    Title = String.Format(" Active Volunteers"),
                    Percentage = ((((float)intActiveVolunteers / (float)intTotalActiveInactiveVolunteers) * (float)100)),
                    ColorBrush = Brushes.Green,
                    Count = intActiveVolunteers
                },
                new Status
                {
                    Title = String.Format(" Inactive Volunteers"),
                    Percentage = ((((float)intInactiveVolunteers / (float)intTotalActiveInactiveVolunteers) * (float)100)),
                    ColorBrush = Brushes.Red,
                    Count = intInactiveVolunteers
                },
            };
            detailsItemsControl.ItemsSource = volStatus;

            // Draw pie - DO NOT EDIT THIS FOREACH!!!
            float angle = 0, prevAngle = 0;
            foreach (var Status in volStatus)
            {
                if (Status.Percentage == 100)
                {
                    // Draw a full circle for the 100% case
                    var ellipse = new Ellipse
                    {
                        Width = pieWidth,
                        Height = pieHeight,
                        Fill = Status.ColorBrush
                    };
                    pieCanvas.Children.Add(ellipse);
                }
                else
                {
                    double line1X = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                    double line1Y = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                    angle = Status.Percentage * (float)360 / 100 + prevAngle;

                    double arcX = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                    double arcY = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                    var line1Segment = new LineSegment(new System.Windows.Point(line1X, line1Y), false);
                    double arcWidth = radius, arcHeight = radius;
                    bool isLargeArc = Status.Percentage > 50;
                    var arcSegment = new ArcSegment()
                    {
                        Size = new Size(arcWidth, arcHeight),
                        Point = new System.Windows.Point(arcX, arcY),
                        SweepDirection = SweepDirection.Clockwise,
                        IsLargeArc = isLargeArc,
                    };
                    var line2Segment = new LineSegment(new System.Windows.Point(centerX, centerY), false);

                    var pathFigure = new PathFigure(
                        new System.Windows.Point(centerX, centerY),
                        new List<PathSegment>()
                        {
                    line1Segment,
                    arcSegment,
                    line2Segment,
                        },
                        true);

                    var pathFigures = new List<PathFigure>() { pathFigure, };
                    var pathGeometry = new PathGeometry(pathFigures);
                    var path = new Path()
                    {
                        Fill = Status.ColorBrush,
                        Data = pathGeometry
                    };
                    pieCanvas.Children.Add(path);

                    prevAngle = angle;
                }
            }
        }



        /// <summary>
        /// Function Name: BarGraph
        /// Created By: Kiefer Thorson
        /// Date Created: 2/4/2023
        /// Additional Contributors: Nathan VanSnepson 
        /// Last Modified: 2/15/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// This method draws the BAR GRAPH on the Home Page
        /// 
        /// TODO: 
        ///     -Schools database needs to be updated to include School District and a chosen coresponding color
        ///         - Color chosen by User when Adding a new school district
        ///     -Once done sort Schools by District they belong to -> distinguish by color
        ///         - Will have to update or add to DatabaseAssignmentProvider.cs
        /// </summary>
        private void BarGraph()
        {
            //clear the prior bar graph
            mainCanvas.Children.Clear();
            // Define Chart Parameters
            float chartWidth = 275, chartHeight = 400, axisMargin = 10, xAxisInterval = 25,
                blockWidth = 30, blockMargin = 1;
            mainCanvas.Width = chartWidth;
            mainCanvas.Height = chartHeight;
            Point origin = new Point();
            double xValue = 0;
            var xAxisValue = origin.X;

            //Gets Assignment Collection from database               
            var colAssignments = _assignmentProvider.GetAllAssignments();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }

            #region Title
            // add title to chart
            TextBlock chartLabel = new TextBlock()
            {
                Text = "Volunteers / School",
                FontSize = 18,
                Foreground = Brushes.Black,
            };

            mainCanvas.Children.Add(chartLabel);
            Canvas.SetTop(chartLabel, origin.X - 45);

            Canvas.SetLeft(chartLabel, origin.X + 175);
            #endregion

            #region YAxis
            // Set School Names to Y axis of bar graph

            // Iterate through assignments and collect each Schools TUID
            var margin = origin.X + blockMargin;
            foreach (var item in colAssignments.Select(x => new { x.Classroom.School, x.Classroom.SchoolTuid }).Distinct())
            {
                if (item.School.IsActive)
                {

                // Get number of volunteers belonging to each school and assign to variable to put into graph
                int intActiveVolunteersAssigned = _assignmentProvider.GetActiveVolunteersBySchoolAssignmentCount(item.SchoolTuid);
                if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }

                // Create Bar representing school volunteers
                Rectangle block = new Rectangle()
                {
                    Fill = Brushes.Gold,
                    Width = intActiveVolunteersAssigned * 25,
                    Height = blockWidth,
                };

                // Add created Bar (# Vols/School) to the xaml
                mainCanvas.Children.Add(block);
                Canvas.SetTop(block, margin);
                Canvas.SetLeft(block, block.Height + 90);

                // Shrink given string if school name too long
                string setName = item.School.Name;
                if (setName.Length > 18)
                {
                    setName = setName.Substring(0, 18);
                    setName = setName + "...";
                }


                // set School text
                TextBlock blockHeader = new TextBlock()
                {
                    Text = setName,
                    FontSize = 16,
                    Foreground = Brushes.Black,
                };

                mainCanvas.Children.Add(blockHeader);
                Canvas.SetTop(blockHeader, margin + 2);
                Canvas.SetLeft(blockHeader, origin.X - 55);

                // increment margin to draw next bar
                margin += (blockWidth + blockMargin);
            }

            }

            #endregion

            #region XAxis
            // This while loop responsible for numbering the x axis in BAR GRAPH from 0-10 by 2
            while (xValue <= 10)
            {
                TextBlock xAxisTextBlock = new TextBlock()
                {
                    Text = $"{xValue}",
                    Foreground = Brushes.Black,
                    FontSize = 16,

                };
                // add text block and position on page
                mainCanvas.Children.Add(xAxisTextBlock);
                Canvas.SetLeft(xAxisTextBlock, origin.X + 107 + (xValue * xAxisInterval));
                Canvas.SetTop(xAxisTextBlock, margin + 15);
                // increment next block (counting by 2s)
                xValue += 2;
            }
            #endregion

            #region XAxisLabel

            // add label to x axis of graph
            TextBlock xAxisLabel = new TextBlock()
            {
                Text = "Active Volunteers",
                FontSize = 14,
                Foreground = Brushes.Black,
            };
            mainCanvas.Children.Add(xAxisLabel);
            Canvas.SetTop(xAxisLabel, margin + 35);
            Canvas.SetLeft(xAxisLabel, origin.X + 200);
            #endregion
        }

    }

    /// <summary>
    /// Class Name: Status
    /// Created By: Kiefer Thorson
    /// Date Created: 2/4/2023
    /// Additional Contributors: Nathan VanSnepson
    /// Last Modified: 2/6/2023
    /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
    /// 
    /// Purpose:
    /// Getters and setters for the PIE CHART
    /// </summary>
    public class Status
    {
        public float Percentage { get; set; }
        public string Title { get; set; }
        public Brush ColorBrush { get; set; }
        public int Count { get; set; }
    }

}


