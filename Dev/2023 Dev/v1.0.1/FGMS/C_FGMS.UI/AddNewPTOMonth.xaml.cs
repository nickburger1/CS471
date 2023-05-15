using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using Bogus;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2013.Word;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for AddNewMonth.xaml
    /// </summary>
    /// 


    public partial class AddNewPTOMonth : System.Windows.Window
    {


        private readonly IServiceProvider _serviceProvider;
        private readonly IPTOProvider _PTOProvider;
        private readonly IVolunteerProvider _VolunteerProvider;
        private readonly IPTOStipendRates _PTOStipendRates;
        private bool checkIfChanged;
        private int startingYear;
        private int startingMonth;
        private bool errorFlag;

        public AddNewPTOMonth(IServiceProvider serviceProvider, int startingYear,int startingMonth)
        {
            _serviceProvider = serviceProvider;
            _PTOProvider = _serviceProvider.GetRequiredService<IPTOProvider>();
            _VolunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _PTOStipendRates = _serviceProvider.GetRequiredService<IPTOStipendRates>();

            _PTOProvider.DatabaseError += ErrorHandler;
            _VolunteerProvider.DatabaseError += ErrorHandler;
            _PTOStipendRates.DatabaseError += ErrorHandler;

            this.startingMonth = startingMonth;
            this.startingYear = startingYear;
            checkIfChanged = false;
            InitializeComponent();
            populatePage();



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
        /// Populates the entire page with the relevant data.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void populatePage()
        {


            populateEditingTable();
            populateStipendAndPTOInfo();
            populateVolunteersNames();
            if (dtgPTO.Items.Count == 0)
            {
                createDefaultData();
            }


            //shows month currently being edited
            lblMonthName.Content = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[startingMonth];
            lblYearNumber.Content = startingYear;

        }
        /// <summary>
        /// Creates default data for each volunteer in the ComboBox who is not already in the DataGrid.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void createDefaultData()
        {
            foreach (VolunteerNameIdModel volunteer in cbobxVolunteer.Items)
            {
                // Check if volunteer already exists in the DataGrid
                bool volunteerExists = dtgPTO.Items.OfType<clsPTOModel>().Any(x => x.volunteerID == volunteer.Tuid);

                if (!volunteerExists)
                {
                    clsPTOModel createNewClsPtoModel = new clsPTOModel();
                    createNewClsPtoModel.strFullName = volunteer.FullName;
                    createNewClsPtoModel.volunteerID = volunteer.Tuid;
                    createNewClsPtoModel.date = new DateTime(startingYear, startingMonth + 1, 1);
                    createNewClsPtoModel.IsPTOEligible = true;
                    dtgPTO.Items.Add(createNewClsPtoModel);
                }
            }
        }


        /// <summary>
        /// Populates the ComboBox with volunteer names and IDs.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>

        private void populateVolunteersNames()
        {
            List<VolunteerNameIdModel> listVolunteer = _VolunteerProvider.GetVolunteerNameAndId().ToList();
            if (errorFlag) { errorFlag = false; return; }


            foreach (var volunteer in listVolunteer)
            {
                cbobxVolunteer.Items.Add(volunteer);
            }





        }


        /// <summary>
        /// Populates the stipend and PTO information for the given month.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void populateStipendAndPTOInfo()
        {
            clsPTOStipendRatesModel StipendAndPtoInfo = _PTOStipendRates.getStipendAndPTORateForAGivenMonth(startingYear,startingMonth+1);
            if (errorFlag) { errorFlag = false; return; }

            txtCurrentStipendRate.Text = StipendAndPtoInfo.StipendRate +"";
            txtPTORate.Text = StipendAndPtoInfo.PTORate + "";


        }



        /// <summary>
        /// Populates the DataGrid with PTO information for the given month.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void populateEditingTable()
        {

            List<clsPTOModel> listPTOInfomation = _PTOProvider.getASpecificMonthOfPtoInfomation(startingYear, startingMonth+1).ToList();
            if (errorFlag) { errorFlag = false; return; }

            foreach (var item in listPTOInfomation)
            {
                dtgPTO.Items.Add(item);
            }

        }

        /// <summary>
        /// Handles the selection change event for the DataGrid.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void dtgPTO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgPTO.SelectedItem is clsPTOModel selectedItemInDataGrid)
            {
                txtRegularHours.Text = selectedItemInDataGrid.RegularHours + "";
                txtPTOUsed.Text = selectedItemInDataGrid.PtoUsed + "";

                // Set ComboBox selection based on DataGrid selection
                var indexInComboBox = cbobxVolunteer.Items.OfType<VolunteerNameIdModel>().ToList().FindIndex(x => x.Tuid == selectedItemInDataGrid.volunteerID);
                cbobxVolunteer.SelectedIndex = indexInComboBox;
                chkIsPtoEligible.IsChecked = selectedItemInDataGrid.IsPTOEligible;
            }

        }


        /// <summary>
        /// Handles the selection change event for the ComboBox.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void cbobxVolunteer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbobxVolunteer.SelectedItem is VolunteerNameIdModel selectedVolunteer)
            {
                var indexInDataGrid = dtgPTO.Items.OfType<clsPTOModel>().ToList().FindIndex(x => x.volunteerID == selectedVolunteer.Tuid);
                dtgPTO.SelectedIndex = indexInDataGrid;
            }
        }

        /// <summary>
        /// Handles the cancel button click event, closing the window.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(checkIfChanged == true)
            {
                if (System.Windows.MessageBox.Show("Are you sure you want to lose your changes?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

        /// <summary>
        /// Handles the save button click event, updating the PTO information and closing the window.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<clsPTOModel> listOfPtoInfo = new List<clsPTOModel>();
            foreach (var listitem in dtgPTO.Items)
            {
                listOfPtoInfo.Add(listitem.CastTo<clsPTOModel>());

            }


            _PTOProvider.updatePTOInfomation(listOfPtoInfo, startingYear, startingMonth + 1);
            if (errorFlag) { errorFlag = false; return; }

            this.Close();



        }


        /// <summary>
        /// Handles the add person button click event, editing and adding values in the DataGrid.
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            editAndAddValuesInTable();
        }


        /// <summary>
        /// Edits and adds values to the DataGrid based on user input.
        /// Does any error checking required (Value more than 1000, Value less than 0, and NaN)
        /// Also creates the PTO item in the table
        /// </summary>
        /// <Author>
        /// Nicklas Mortensen-Seguin
        /// </Author>
        /// <Date>
        /// Last Modified:3/4/23
        /// </Date>
        private void editAndAddValuesInTable()
        {


            checkIfChanged = true;
            //stores the object we are either creating or editing
            clsPTOModel selectedItemInPTOTable;
            //if the item selected in the datagrid isn't the item in the same item as selected in the data grid we create a new model
            //otherwise we are editing a model in the grid already
            bool isAddingNewItem = dtgPTO.SelectedIndex != cbobxVolunteer.SelectedIndex;
            if (!isAddingNewItem)
            {
                selectedItemInPTOTable = dtgPTO.SelectedItem.CastTo<clsPTOModel>();
            }
            else
            {
                VolunteerNameIdModel volunteerInComboBox = cbobxVolunteer.SelectedItem.CastTo<VolunteerNameIdModel>();
                selectedItemInPTOTable = new clsPTOModel();
                
                selectedItemInPTOTable.strFullName = volunteerInComboBox.FullName;
                selectedItemInPTOTable.volunteerID = volunteerInComboBox.Tuid;


            }



            //now we try and take the values the user has entered into the data grid
            decimal PtoUsed;
            decimal RegularHours;
            List<clsPTOModel> listOfDataCurrentlyInTable = dtgPTO.Items.OfType<clsPTOModel>().ToList();
            
            //validates there is numbers within txtRegularHours and txtPtoUsed
                if (decimal.TryParse(txtRegularHours.Text, out RegularHours) && decimal.TryParse(txtPTOUsed.Text, out PtoUsed))
                {
                //makes sure they are less than 1000 and bigger than 0
                if ((PtoUsed > 1000 || PtoUsed >= 0) && (RegularHours > 1000 || RegularHours >= 0))
                {

                    List<clsPTOModel> prevoiusMonthsPTOInfo = _PTOProvider.getASpecificMonthOfPtoInfomation(startingYear, startingMonth).ToList();
                    if (errorFlag) { errorFlag = false; return; }



                    //pulls the previous pto months info
                    var lastMonthsPTOEnd = prevoiusMonthsPTOInfo.Where(x => x.volunteerID == selectedItemInPTOTable.volunteerID).Select(y => y.PtoEnd).ToList();
                    var lastMonthsYTDHours = prevoiusMonthsPTOInfo.Where(x => x.volunteerID == selectedItemInPTOTable.volunteerID).Select(y => y.YearToDateHour).ToList();

                    //checks to make sure there is data in the previous month
                    if (lastMonthsPTOEnd.Any())
                    {
                        selectedItemInPTOTable.PtoStart = lastMonthsPTOEnd.First();


                    }
                    else
                    {
                        selectedItemInPTOTable.PtoStart = 0;
                    }


                    if (lastMonthsYTDHours.Any())
                    {
                        selectedItemInPTOTable.YearToDateHour = lastMonthsYTDHours.First() + selectedItemInPTOTable.RegularHours + selectedItemInPTOTable.PtoUsed;

                    }
                    else
                    {
                        selectedItemInPTOTable.YearToDateHour = selectedItemInPTOTable.RegularHours + selectedItemInPTOTable.PtoUsed;

                    }



                    
                    selectedItemInPTOTable.IsPTOEligible = chkIsPtoEligible.IsChecked;




                    selectedItemInPTOTable.RegularHours = RegularHours;

                    if (chkIsPtoEligible.IsChecked != null)
                    {


                        if ((bool)chkIsPtoEligible.IsChecked)
                        {

                            selectedItemInPTOTable.PtoUsed = PtoUsed;
                            selectedItemInPTOTable.PtoEarned = RegularHours * decimal.Parse(txtPTORate.Text);


                            if (selectedItemInPTOTable.PtoStart != null && selectedItemInPTOTable.PtoEarned != null)
                            {
                                selectedItemInPTOTable.PtoEnd = Math.Round((decimal)(selectedItemInPTOTable.PtoStart - PtoUsed + selectedItemInPTOTable.PtoEarned));
                            }
                            
                            selectedItemInPTOTable.StipendPaid = (RegularHours + PtoUsed) * decimal.Parse(txtCurrentStipendRate.Text);

                        }
                        else
                        {
                            selectedItemInPTOTable.PtoUsed = 0;
                            selectedItemInPTOTable.PtoEarned = 0;
                            selectedItemInPTOTable.PtoEnd = 0;
                            selectedItemInPTOTable.StipendPaid = (RegularHours) * decimal.Parse(txtCurrentStipendRate.Text);


                        }
                    }

                    selectedItemInPTOTable.date = new DateTime(startingYear, startingMonth + 1, 1);




                    if (isAddingNewItem)
                    {
                        dtgPTO.Items.Add(selectedItemInPTOTable);
                    }



                    dtgPTO.Items.Refresh();


                }
                else
                {
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "You must enter a valid number for either PTO Used or Reg Hours",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3,
                        
                        
                        


                }); ;


                }

               

                }
                else
                {


                Growl.Warning(new GrowlInfo
                {
                    Message = $"You must enter a valid number for either PTO Used or Reg Hours",
                    ShowDateTime = false,
                    StaysOpen = false,
                    WaitTime = 3


                }); ;

                }

            


        }

        /// <summary>
        /// The reason behind this method is to prevent the users from putting in bogus data into the txt boxes and add them to the 
        /// table then trying to store them in the data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRegularHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
                int input;
                if (!int.TryParse(e.Text, out input))
                {
                    e.Handled = true;
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "Please input only valid numbers",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 3,
                        CancelStr = "Cancel"
                        
                    });
            }
        }

        /// <summary>
        /// Allows the Growls to work on the pop out menus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnActivated(EventArgs e)
        {
            Growl.SetGrowlParent(winAddNewMonth,true);
            base.OnActivated(e);
        }

        /// <summary>
        /// Allows the Growls to work on the pop out menus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivated(EventArgs e)
        {
            Growl.SetGrowlParent(winAddNewMonth, false);
            base.OnActivated(e);
        }
    }
}
