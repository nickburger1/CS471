using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Services.AddressProviders;
using B_FGMS.BusinessLogic.Services.AssignmentProviders;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for SchoolPerSchoolPage.xaml
    /// </summary>
    public partial class SchoolPerSchoolPage : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly ISchoolProvider _schoolProvider;
        private readonly IAddressProvider _addressProvider;
        private readonly IAssignmentProvider _assignmentProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private bool errorFlag;

        public SchoolPerSchoolPage(
            IServiceProvider serviceProvider,
            ISchoolProvider schoolProvider,
            IAddressProvider addressProvider,
            IAssignmentProvider assignmentProvider,
            IVolunteerProvider volunteerProvider,
            DataRefreshEventBroker refreshEventBroker)
        {

            // Providers can be "injected" instead of needing the service provider
            _serviceProvider = serviceProvider;
            _schoolProvider = schoolProvider;
            _addressProvider = addressProvider;
            _assignmentProvider = assignmentProvider;
            _volunteerProvider = volunteerProvider;
            _refreshEventBroker = refreshEventBroker;

            InitializeComponent();

            _schoolProvider.DatabaseError += ErrorHandler;
            _addressProvider.DatabaseError += ErrorHandler;
            _assignmentProvider.DatabaseError += ErrorHandler;
            _volunteerProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshData();
            });
            try
            {
                SetCbxSchools();
                cbxSchools.SelectionChanged += cbxSchools_SelectionChanged;
            }catch(RefreshDataCustomException ex)
            {

            }
        }

        #region Error Hanlder

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
        #endregion

        #region Button Functionality
        /// <summary>
        /// Method Name: btnEdit_Click
        /// Created By: Kiefer Thorson
        /// Date Created:  1/27/2023
        /// Additional Contributors: 
        /// Last Modified: 3/2/23
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The Purpose of this function is to allow the user to edit any information about the selected school. Once in "Edit Mode" the icon on this button will change to save, and the press of the button will save the entry
        ///     - PER SCHOOLS PAGE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SchoolModel? selectedSchool = cbxSchools.SelectedItem as SchoolModel;
            if (selectedSchool != null)
            {
                Console.WriteLine(cbxSchools.SelectedItem.ToString());
                EditSelectedSchool editSchool = new EditSelectedSchool(_serviceProvider, selectedSchool.Tuid);

                editSchool.ShowDialog();
                //set the growl parent to null to lock growls onto the current page
                Growl.GrowlPanel = null;
                SetSchoolLabels();
                SetDatagrid();
            }
            else
            {
                Growl.Warning("Select School to Continue");
            }
        }


        /// <summary>
        /// This method will delete a school when the delte button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SchoolModel? school = cbxSchools?.SelectedItem as SchoolModel;
            if (school != null)
            {
                //Confirm if selected school should be deleted
                MessageBoxResult mbrDelete = System.Windows.MessageBox.Show("Are you sure you want to delete school: " + school.Name, "Delete School Confirmation", MessageBoxButton.YesNo);

                //Delete school if confirmed
                if (mbrDelete == MessageBoxResult.Yes)
                {
                    // Get all active activeVolunteers assigned to selected school's tuid
                    var activeVolunteers = _assignmentProvider.GetActiveVolunteersBySchoolTuid(school.Tuid).ToArray();
                    if (errorFlag) { errorFlag = false; return; }

                    // if there is attached data change deletion flag, if not delete
                    if (activeVolunteers.Length == 0)
                    {
                        // delete assocaited address as well
                        _addressProvider.DeleteAddress(school.AddressTuid);
                        if (errorFlag) { errorFlag = false; return; }
                        _schoolProvider.DeleteSchool(school.Tuid);
                        if (errorFlag) { errorFlag = false; return; }

                    }
                    else
                    {
                        _schoolProvider.DeleteFlagSchool(school);
                        if (errorFlag) { errorFlag = false; return; }

                    }

                    try
                    {
                        SetCbxSchools();
                        // -1 is used as a flag to reset data on Perschools page
                        cbxSchools.SelectedIndex = -1;
                    }
                    catch(RefreshDataCustomException ex)
                    {

                    }
                }

            }
            else
            {
                Growl.Warning("Select School to Continue");
            }
        }

        /// <summary>
        /// This method will attempt to use the ExcelExporters class functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelPerSchool_Click(object sender, RoutedEventArgs e)
        {
            if (cbxSchools.SelectedIndex != -1)
            {
                ExcelFileModel? excelFile = CreateNewSchoolReport();
                if (excelFile != null)
                {
                    ExcelExporter.ExportToExcel(excelFile);
                }
                else
                {
                    GrowlHelpers.Error("Failed to export to excel");
                }


            }
            else
            {
                GrowlHelpers.Warning("Select School to Continue");
            }
        }


        #endregion

        #region Page Settings
        /// <summary>
        /// Method Name: SetCbxSchools()
        /// Created By: Kiefer Thorson
        /// Date Created:  2/16/2023
        /// Additional Contributors: 
        /// Last Modified: 2/17/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The Purpose of this method is to connect the database to the combobox so a school can be selected to view relevant information
        ///     - Also called when changes are made to the database to refresh perSchools section
        ///     - PER SCHOOLS PAGE
        /// </summary>
        private void SetCbxSchools()
        {
            var allSchools = _schoolProvider.GetAllSchools();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cbxSchools.ItemsSource = allSchools;
        }

        /// <summary>
        /// Method Name: cbxSchools_SelectionChanged
        /// Created By: Kiefer Thorson
        /// Date Created:  1/27/2023
        /// Additional Contributors: 
        /// Last Modified: 2/17/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The Purpose of this method is to gather all relevant data from db for the selected school in the combobox
        ///     - SelectionChanged property activates method every time a new item selected
        ///     - PER SCHOOLS PAGE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSchools_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetSchoolLabels();
            SetDatagrid();
        }

        /// <summary>
        /// this method just resets the school drop down list
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/12/2023</created>
        private void RefreshData()
        {
            SetCbxSchools();
        }

        private string? GetSchoolAddress(SchoolModel school)
        {
            if (school.Address != null)
            {
                string strAddress = "";
                strAddress += school.Address.AddressLine1 ?? "";
                if (!string.IsNullOrEmpty(school.Address.AddressLine2))
                {
                    strAddress += " " + school.Address.AddressLine2;
                }
                strAddress += " " + school.Address.City;
                strAddress += ", " + school.Address.State;
                strAddress += " " + school.Address.Zipcode;
                return strAddress;
            }
            else
            {
                return null;
            }
        }

        private string GetStatusString(bool isActive)
        {
            if (isActive)
            {
                return "Active";
            }
            else
            {
                return "Inactive";
            }
        }
        /// <summary>
        /// This method attempts to create an excel file model for the schools when the export button to hit or return null if we fail to create the file model
        /// </summary>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/13/2023</created>
        public ExcelFileModel? CreateNewSchoolReport()
        {
            ExcelFileModel excelModel = new ExcelFileModel();
            SchoolModel? school = cbxSchools.SelectedItem as SchoolModel;
            if (school != null)
            {
                excelModel.FileName = school.Name + " Report";
                excelModel.Sheets = new List<ExcelSheetModel>();
                //school info
                ExcelTableModel schoolInfo = new ExcelTableModel();
                schoolInfo.Title = school.Name ?? "N/A";
                schoolInfo.Headers = new List<string>()
                {
                    "School Name",
                    "Address",
                    "Principal",
                    "Secratary",
                    "Phone Number",
                    "Status",

                };
                schoolInfo.Rows = new List<object>();
                schoolInfo.Rows.Add(new
                {
                    Name = school.Name,
                    Address = GetSchoolAddress(school) ?? "N/A",
                    Principal = school.Principal,
                    Secratary = school.Secretary,
                    Phone = school.ContactNumber,
                    Status = GetStatusString(school.IsActive)
                });
                //create sheet now since we don't know if we are adding assignments or not
                ExcelSheetModel infoSheet = new ExcelSheetModel()
                {
                    Title = "School Report",
                    Tables = new List<ExcelTableModel>
                    {
                        schoolInfo
                    }
                };
                //do the assignments
                if (dgVolsPerSchool.Items.Count > 0)
                {
                    ExcelTableModel volunteerAssignments = new ExcelTableModel();
                    volunteerAssignments.Title = school.Name + " : Volunteer assignments";
                    volunteerAssignments.Headers = new List<string>()
                    {
                        "Volunteer Name",
                        "Teacher Name",
                        "Grade",
                        "Room",
                        "Schedule",
                        "Phone Number",
                    };
                    volunteerAssignments.Rows = new List<object>();
                    foreach (AssignmentModel assignment in dgVolsPerSchool.Items)
                    {
                        if (assignment != null)
                        {
                            volunteerAssignments.Rows.Add(new
                            {
                                Name = assignment.Volunteer == null ? "N/A" : assignment.Volunteer.FullName,
                                Teacher = assignment.Classroom == null ? "N/A" : assignment.Classroom.Teacher,
                                Grade = assignment.Classroom == null ? "N/A" : assignment.Classroom.Grade,
                                Room = assignment.Classroom == null ? "N/A" : assignment.Classroom.Room,
                                Schedule = assignment.Schedule == null ? "N/A" : assignment.Schedule.StringSchedule,
                                Phone = assignment.Volunteer == null ? "N/A" : assignment.Volunteer.Phone
                            });
                        }
                    }
                    infoSheet.Tables.Add(volunteerAssignments);
                }

                excelModel.Sheets.Add(infoSheet);
                return excelModel;

            }
            else
            {
                return null;
            }



        }

        /// <summary>
        /// This method will use switch days from numbers to their string?
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        private string retDays(string days)
        {
            string buildDays = "";
            while (days.Length > 0)
            {
                string numDay = days.Substring(0, 1);
                days = days.Substring(1);
                if (numDay == "1")
                {
                    buildDays += "M ";
                }
                else if (numDay == "2")
                {
                    buildDays += "T ";
                }
                else if (numDay == "3")
                {
                    buildDays += "W ";
                }
                else if (numDay == "4")
                {
                    buildDays += "Th ";
                }
                else if (numDay == "5")
                {
                    buildDays += "F ";
                }
            }
            return buildDays;
        }

        #endregion



        /// <summary>
        /// This method will set the labels at the top of the page with the school information
        /// </summary>
        /// <author>Andrew Loesel + Kiefer Thorson</author>
        /// <created>4/12/2023</created>
        private void SetSchoolLabels()
        {
            SchoolModel? selectedItem = cbxSchools.SelectedItem as SchoolModel;
            if (selectedItem != null)
            {
                // need to look to find School name that matches selected and pull that entry to fill relevant boxes up top
                var selectedSchool = _schoolProvider.GetSchoolByTuid(selectedItem.Tuid);

                // This section takes the selected school and populates the information at the top of the page
                if (selectedSchool != null)
                {
                    // Set Address
                    // addresses in own table - use addressTuid of found school to locate
                    //      * I only used var AddressLine1 (street name/num) and zipcode - can change later if deemed necessary
                    var address = _addressProvider.GetAddressByTuid((int?)selectedSchool.AddressTuid);
                    if (errorFlag) { errorFlag = false; return; }
                    txtAddress.Text = address.AddressLine1 + "  --  " + address.City;

                    // Set phone number
                    txtPhoneNum.Text = selectedSchool.ContactNumber;

                    // Set days of the week school in session
                    // WILL NEED TO CHANGE TO ACOMODATE SPLIT CASE WHEN TARA GETS BACK WITH ANSWER

                    string breakDown = selectedSchool.Days.ToString();
                    int length = breakDown.Length;
                    string Days = "";
                    for (int i = 0; i < length; i++)
                    {
                        string add = breakDown.Substring(0, 1);
                        if (add == "1")
                        {
                            Days += "M ";
                        }
                        else if (add == "2")
                        {
                            Days += "T ";
                        }
                        else if (add == "3")
                        {
                            Days += "W ";
                        }
                        else if (add == "4")
                        {
                            Days += "Th ";
                        }
                        else if (add == "5")
                        {
                            Days += "F ";
                        }
                        breakDown = breakDown.Substring(1);
                    }
                    txtDays.Text = Days;


                    // Set hours of operation
                    txtHours.Text = selectedSchool.Hours;

                    // Set Principal and Secretary
                    txtPrincipal.Text = selectedSchool.Principal;
                    txtSecretary.Text = selectedSchool.Secretary;
                }
            }
            else
            {
                txtAddress.Text = "Address";
                txtDays.Text = "School Days";
                txtHours.Text = "School Hours";
                txtPhoneNum.Text = "Phone Number";
                txtPrincipal.Text = "";
                txtSecretary.Text = "";
                dgVolsPerSchool.ItemsSource = null;
            }
        }


        /// <summary>
        /// This method will set the datagrid data with assignments for a school
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/12/2023</created>
        private void SetDatagrid()
        {
            SchoolModel? selectedItem = cbxSchools.SelectedItem as SchoolModel;
            if (selectedItem != null)
            {
                var selectedSchool = _schoolProvider.GetSchoolByTuid(selectedItem.Tuid);
                if (selectedSchool != null)
                {
                    // Get all active activeVolunteers assigned to selected school's tuid
                    var activeVolunteers = _assignmentProvider.GetActiveVolunteersBySchoolTuid(selectedSchool.Tuid);
                    if (errorFlag) { errorFlag = false; return; }
                    // connect datagrid to activeVolunteers 
                    dgVolsPerSchool.ItemsSource = activeVolunteers;
                }
            }
        }

        /// <summary>
        /// This method fires when the refresh button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(SchoolPerSchoolPage));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
    }
}
