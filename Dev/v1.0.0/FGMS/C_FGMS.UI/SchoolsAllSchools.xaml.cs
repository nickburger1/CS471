using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Exceptions;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Commands;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Services.AddressProviders;
using B_FGMS.BusinessLogic.Services.AssignmentProviders;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using B_FGMS.BusinessLogic.Services.StudentProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using C_FGMS.UI.Helpers;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Application = Microsoft.Office.Interop.Word.Application;

namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for SchoolsAllSchools.xaml
    /// </summary>
    public partial class SchoolsAllSchools : System.Windows.Controls.Page
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ISchoolProvider _schoolProvider;
        private readonly IAddressProvider _addressProvider;
        private readonly IAssignmentProvider _assignmentProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private bool errorFlag;
        //private IEnumerable<SchoolModel> schools;


        /// <summary>
        /// Method Name: School
        /// Created By: Chippi
        /// Date Created:  / /2023
        /// Additional Contributors: Kiefer Thorson
        /// Last Modified: 2/17/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The purpose of this method is to initialize both the page and the interface to pull from the db
        /// </summary>
        public SchoolsAllSchools(
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

            _addressProvider.DatabaseError += ErrorHandler;
            _assignmentProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshData();
            });

            try
            {
                RefreshData();
            }
            catch(RefreshDataCustomException e)
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
        /// This method gets all schools, and will filter only active schools if chkActive is checked
        /// </summary>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/5/2023</created>
        private List<SchoolModel> GetAllSchoolsBasedOnStatusControl()
        {
            var allSchools = _schoolProvider.GetAllSchools();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            //see if we need to filter based on active status
            if (chkActive.IsChecked == true)
            {
                allSchools = allSchools.Where(x => x.IsActive == true);
            }
            return allSchools.ToList();
        }

        /// <summary>
        /// Method Name: PopulateSchoolsGrid
        /// Created By: Timothy Johnson
        /// Date Created:  2/7/2023
        /// Additional Contributors:
        /// Last Modified: 3/3/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// Function Purpose: The Purpose of this function is to populate the schools grid with the information from the database
        /// </summary>
        private void PopulateSchoolsGrid()
        {
            dtgAllSchool.ItemsSource = GetAllSchoolsBasedOnStatusControl();
        }

        /// <summary>
        /// Method Name: myGrid_selectedCellsChanged
        /// Created By: Anthony Chippi
        /// Date Created:  2/7/2023
        /// Additional Contributors: Tim
        /// Last Modified: 2/21/2023
        /// Last Modified By: Anthony Chippi
        /// Purpose: 
        /// </summary>
        private void OnSelectionChange_Click(object sender, SelectedCellsChangedEventArgs e)
        {
            //Gets All Assignments with an Active volunteer from the selected school.
            SchoolModel selectedData = (SchoolModel)dtgAllSchool.SelectedItem;
            if (selectedData == null) return;


            //Changes the Contents of the TextBoxes to the corresponding values.
            NumberofTotalStudentsClassroom.Content = _assignmentProvider.GetTotalStudentsClassroomBySchoolTuid(selectedData.Tuid);
            if (errorFlag) { errorFlag = false; return; }
            if (errorFlag) { errorFlag = false; return; }
            NumberofTotalStudentsAssigned.Content = _assignmentProvider.GetTotalStudentsAssignedBySchoolTuid(selectedData.Tuid);
            NumberofTotalStudentsAges05.Content = _assignmentProvider.GetTotal0to5BySchoolTuid(selectedData.Tuid);
            if (errorFlag) { errorFlag = false; return; }
            NumberofTotalStudentsAges612.Content = _assignmentProvider.GetTotal6to12BySchoolTuid(selectedData.Tuid);
            if (errorFlag) { errorFlag = false; return; }

        }

        /// <summary>
        /// This method creates an ExcelFileModel of the all schools data grid..
        /// </summary>
        /// <returns></returns>
        /// <author>Kiefer Thorson</author>
        /// <created>?</created>
        public ExcelFileModel CreateAllSchoolsReport()
        {

            List<object> SendInfo = buildRow();

            var summaryTable = new ExcelTableModel()
            {
                Title = "All Schools",
                Headers = new List<string> { "School Name", "Principal Name", "Phone Number", "Program Status" },
                Rows = SendInfo,
            };
            var excelSheetModel = new ExcelSheetModel()
            {
                Title = "All Schools",
                Tables = new List<ExcelTableModel> { summaryTable }
            };

            var excelFileModel = new ExcelFileModel()
            {
                FileName = "All Schools",
                Sheets = new List<ExcelSheetModel> { excelSheetModel }
            };

            return excelFileModel;
        }

        /// <summary>
        /// this method builds a row for an excel table used in the CreateAllSchoolsReport method
        /// </summary>
        /// <returns></returns>
        /// <author>kiefer Thorson</author>
        /// <created>?</created>
        public List<object> buildRow()
        {
            try
            {
                var tableData = new List<object>();
                var allschools = GetAllSchoolsBasedOnStatusControl();

                foreach (SchoolModel school in allschools)
                {
                    string retStatus;
                    if (school.IsActive == true)
                    {
                        retStatus = "ACTIVE";
                    }
                    else
                    {
                        retStatus = "INACTIVE";
                    }
                    tableData.Add(new
                    {
                        school.Name,
                        school.Principal,
                        school.ContactNumber,
                        retStatus,
                    });
                }

                return tableData;
            }
            catch (RefreshDataCustomException ex)
            {
                return new List<object>();
            }
        }

        #region buttonEvents
        /// <summary>
        /// this method fires when the excel export button is clicked and calls the CreateAllSchoolsReport method and uses that
        /// excel file model in the ExcelExporters ExportToExcel method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/5/2023</created>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExcelExporter.ExportToExcel(CreateAllSchoolsReport());
        }

        /// <summary>
        /// This method will open the edit school page for the selected school when the edit button is hit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/5/2023</created>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SchoolModel? selectedSchool = (SchoolModel?)dtgAllSchool.SelectedItem;
            if (selectedSchool != null)
            {
                if (!string.IsNullOrEmpty(selectedSchool.Name))
                {
                    EditSelectedSchool editSchool = new EditSelectedSchool(_serviceProvider, selectedSchool.Tuid);

                    editSchool.ShowDialog();
                    //set the growl panel to null which locks it onto the current page - Isabelle
                    Growl.GrowlPanel = null;
                    if (editSchool.DialogResult == false)
                    {
                        GrowlHelpers.Info("Canceled updating " + selectedSchool.Name);
                    }
                    else
                    {
                        GrowlHelpers.Info("Successfully updated " + selectedSchool.Name);
                        RefreshData();
                    }

                }
                else
                {
                    GrowlHelpers.Warning("Cannot Edit this school");
                }


            }
            else
            {
                GrowlHelpers.Warning("No selected school to edit");
            }
        }

        /// <summary>
        /// Method Name: btnAdd_Click
        /// Created By: Chippi
        /// Date Created:  / /2023
        /// Additional Contributors: Kiefer Thorson
        /// Last Modified: 2/17/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The Purpose of this method is to open the window to add a school to the db upon clicking + from Schools - All
        ///     - ALL SCHOOLS PAGE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNewSchool newSchool = new AddNewSchool(_serviceProvider);
            newSchool.ShowDialog();
            //set the growl panel to null which locks it onto the current page - Isabelle
            Growl.GrowlPanel = null;
            if (newSchool.DialogResult == true)
            {
                GrowlHelpers.Info("Successfully added new school");
            }
            else
            {
                GrowlHelpers.Info("Canceled adding new school");
            }

            RefreshData();
        }

        /// <summary>
        /// Method Name: btnPrint_Click
        /// Created By: Chippi
        /// Date Created:  / /2023
        /// Additional Contributors: Kiefer Thorson
        /// Last Modified: 1/27/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The Purpose of this method is to allow the user to print the items on screen
        ///     - ALL SCHOOLS PAGE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <modification>
        ///     4/5/2023 - Andrew Loesel : rewrote the method to not call db queries inside of a loop, and this method no longer uses database entites, now uses models.
        /// </modification>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Application appl = new Application();

            //Add a new document
            appl.Documents.Add();

            //Set the font style to italic   
            appl.Selection.Font.Italic = 0;
            appl.Selection.Font.Size = 15;

            //Type some information to the document

            DateTime today = DateTime.Today;

            if (dtgAllSchool.SelectedItem == null)
            {
                //this will be all schools with assignments
                List<SchoolAssignmentModel> assignments = _assignmentProvider.GetAllAssignmentsWithIncludes();
                if (errorFlag) { errorFlag = false; return; }
                //now create a list of distinct schools using the assignments list
                List<int> schools = assignments.Select(x => x.SchoolTuid).Distinct().ToList();
                foreach (var school in schools)
                {

                    //get the assignments for this school
                    List<SchoolAssignmentModel> perSchoolAssignments = assignments.Where(x => x.SchoolTuid == school).ToList();
                    //print the school information to word
                    appl.Selection.Font.Bold = 1;
                    appl.Selection.TypeText("Saginaw Area Foster Grandparent Program Monthly Site Visit\n");
                    appl.Selection.Font.Bold = 0;

                    SchoolAssignmentModel? currentSchool = perSchoolAssignments.FirstOrDefault();
                    if (currentSchool == null)
                    {
                        continue;
                    }

                    appl.Selection.Font.Bold = 1;
                    appl.Selection.TypeText("Date: ");
                    appl.Selection.Font.Bold = 0;
                    appl.Selection.TypeText(today.ToString("MM/dd/yyyy") + "\t");
                    appl.Selection.Font.Bold = 1;
                    appl.Selection.TypeText("Site: ");
                    appl.Selection.Font.Bold = 0;
                    appl.Selection.TypeText(currentSchool.SchoolName + "\n");
                    appl.Selection.Font.Bold = 1;
                    appl.Selection.TypeText("\t\t\t\tPrincipal: ");
                    appl.Selection.Font.Bold = 0;
                    appl.Selection.TypeText(currentSchool.PrincipalName + "\n\n");


                    appl.Selection.Font.Italic = 1;
                    appl.Selection.TypeText("Keep in mind: Is the Foster Grandparent active and involved with the students?\n" +
                        "Are they attentive and interacting kindly with praise and positive discipline?\n" +
                        "Do they interact well with the teacher?\n" +
                        "Is he/she neat in appearance, wearing their smoke or vest, and name badge?\n" +
                        "Are they signed in on their timecard?\n" +
                        "*If possible, ask the teachers and administrators how things are going.\n");

                    appl.Selection.Font.Italic = 0;
                    appl.Selection.Font.Bold = 1;

                    //now print the information about the individual assignments

                    foreach (var item in perSchoolAssignments)
                    {
                        if (item != null)
                        {
                            appl.Selection.Font.Bold = 1;
                            appl.Selection.TypeText("Grandparent Name: ");
                            appl.Selection.Font.Bold = 0;
                            appl.Selection.TypeText(item.VolunteerName + "\n");
                            appl.Selection.Font.Bold = 1;
                            appl.Selection.TypeText("Teacher, Grade: ");
                            appl.Selection.Font.Bold = 0;
                            appl.Selection.TypeText(item.TeacherName + ", " + item.ClassroomGradeLevel + "\n");
                            appl.Selection.Font.Bold = 1;
                            appl.Selection.TypeText("Classroom #: ");
                            appl.Selection.Font.Bold = 0;
                            appl.Selection.TypeText(item.ClassroomNumber + "\n\n");
                        }
                    }
                    appl.Selection.Font.Bold = 0;
                    appl.Selection.TypeText("____________________\t\t\t\t" + "____________________\n" + "FGP Supervisor\t\t\t\t\t\t\t" + "FGP Director");
                    if (schools.IndexOf(school) != schools.Count - 1)
                    {
                        appl.Selection.InsertNewPage();

                    }
                }
            }
            else
            {
                SchoolModel selectedItem = (SchoolModel)dtgAllSchool.SelectedItem;

                appl.Selection.Font.Bold = 1;

                appl.Selection.TypeText("Saginaw Area Foster Grandparent Program Monthly Site Visit\n");

                appl.Selection.Font.Bold = 0;
                appl.Selection.Font.Bold = 1;
                appl.Selection.TypeText("Date: ");
                appl.Selection.Font.Bold = 0;
                appl.Selection.TypeText(today.ToString("MM/dd/yyyy") + "\t");
                appl.Selection.Font.Bold = 1;
                appl.Selection.TypeText("Site: ");
                appl.Selection.Font.Bold = 0;
                appl.Selection.TypeText(selectedItem.Name + "\n");
                appl.Selection.Font.Bold = 1;
                appl.Selection.TypeText("\t\t\t\tPrincipal: ");
                appl.Selection.Font.Bold = 0;
                appl.Selection.TypeText(selectedItem.Principal + "\n\n");

                appl.Selection.Font.Italic = 1;


                appl.Selection.TypeText("Keep in mind: Is the Foster Grandparent active and involved with the students?\n" +
                    "Are they attentive and interacting kindly with praise and positive discipline?\n" +
                    "Do they interact well with the teacher?\n" +
                    "Is he/she neat in appearance, wearing their smoke or vest, and name badge?\n" +
                    "Are they signed in on their timecard?\n" +
                    "*If possible, ask the teachers and administrators how things are going.\n\n");

                appl.Selection.Font.Italic = 0;
                IEnumerable<SchoolAssignmentModel> perSchoolAssignments = _assignmentProvider.GetAssignmentBySchoolTuid(selectedItem.Tuid);
                if (errorFlag) { errorFlag = false; return; }
                foreach (var item in perSchoolAssignments)
                {
                    if (item != null)
                    {
                        appl.Selection.Font.Bold = 1;
                        appl.Selection.TypeText("Grandparent Name: ");
                        appl.Selection.Font.Bold = 0;
                        appl.Selection.TypeText(item.VolunteerName + "\n");
                        appl.Selection.Font.Bold = 1;
                        appl.Selection.TypeText("Teacher, Grade: ");
                        appl.Selection.Font.Bold = 0;
                        appl.Selection.TypeText(item.TeacherName + ", " + item.ClassroomGradeLevel + "\n");
                        appl.Selection.Font.Bold = 1;
                        appl.Selection.TypeText("Classroom #: ");
                        appl.Selection.Font.Bold = 0;
                        appl.Selection.TypeText(item.ClassroomNumber + "\n\n");
                    }
                }
                appl.Selection.TypeText("____________________\t\t\t\t" + "____________________\n" + "FGP Supervisor\t\t\t\t\t\t\t" + "FGP Director");


            }
            //Set the Visible property to true if you want to see Word 
            //running
            appl.Visible = true;

        }
        #endregion

        /// <summary>
        /// this method will refresh the observable data of the all schools page
        /// refreshing the Schools datagrid
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/5/2023</created>
        public void RefreshData()
        {
            PopulateSchoolsGrid();
        }

        private void chkActive_Checked(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(SchoolsAllSchools));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
    }
}