using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.ReportProviders;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Newtonsoft.Json;
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

using System.Windows.Forms.VisualStyles;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using System.Drawing;
using HandyControl.Controls;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using B_FGMS.BusinessLogic.Models.Volunteer;
using System.Dynamic;
using B_FGMS.BusinessLogic.Models.Reports;
using System.Collections.ObjectModel;
using HandyControl.Data;
using A_FGMS.DataLayer.EventBroker;
using C_FGMS.UI.Helpers;
using B_FGMS.BusinessLogic.Events;
using A_FGMS.DataLayer.Exceptions;

/// <summary>
/// The purpose of this file is to provide the interaction logic for the Reports - Report Builder page of the application. 
/// </summary>
/// <author>Jon Maddocks</author>
/// <created>2/14/2023</created>
namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for ReportsReportBuilderPage.xaml
    /// </summary>
    public partial class ReportsReportBuilderPage : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IReportPresetProvider _reportPresetProvider;
        private readonly IReportProvider _reportProvider;
        private readonly IExpenseProvider _expenseProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        List<TreeViewItem> structure;
        ObservableCollection<TreeViewItem> report;
        private bool errorFlag;

        /// <summary>
        /// This function will run an instance of ReportsReportBuilderPage.xaml.   
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>2/14/2023</created>
        public ReportsReportBuilderPage(IServiceProvider serviceProvider, DataRefreshEventBroker refreshEventBroker)
        {
            structure = new List<TreeViewItem>();
            report = new ObservableCollection<TreeViewItem>();
            _serviceProvider = serviceProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _reportPresetProvider = _serviceProvider.GetRequiredService<IReportPresetProvider>();
            _expenseProvider = _serviceProvider.GetRequiredService<IExpenseProvider>();
            _reportProvider = _serviceProvider.GetRequiredService<IReportProvider>();
            _refreshEventBroker = refreshEventBroker;
            InitializeComponent();

            _volunteerProvider.DatabaseError += ErrorHandler;
            _expenseProvider.DatabaseError += ErrorHandler;
            _reportPresetProvider.DatabaseError += ErrorHandler;
            _reportProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            _refreshEventBroker.Subscribe((args, x) =>
            {
                // TODO: Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshData();
            });

            try
            {
                AddTreeViewItemsToTreeViewStructure();
                PopulateVolunteers();
                PopulatePresets();
                PopulateSortBy();
                dtpEndDate.SelectedDate = DateTime.Now;
            }
            catch (RefreshDataCustomException ex)
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
        /// This method is used to replace the number value for a weekday with its abbreviation counterpart.
        /// </summary>
        /// <param name="strDays">the days as numbers</param>
        /// <returns>the days as letters</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        private string ReplaceDays(string strDays)
        {
            strDays = strDays.Replace("1", "M");
            strDays = strDays.Replace("2", "T");
            strDays = strDays.Replace("3", "W");
            strDays = strDays.Replace("4", "R");
            strDays = strDays.Replace("5", "F");
            return strDays;
        }

        #region initialization

        /// <summary>
        /// This method will populate the volunteer dropdown combobox
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>?</created>
        public void PopulateVolunteers()
        {
            var Names = _volunteerProvider.GetVolunteerNameAndId();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbSelectVolunteer.ItemsSource = Names;
            //select all volunteers by default
            chkSelectAllVolunteers.IsChecked = true;
        }

        /// <summary>
        /// this method adds the volunteer name sort and date sort option to cmbSortBy
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>?</created>
        public void PopulateSortBy()
        {
            List<string> sorts = new List<string>();
            sorts.Add("Volunteer Name");
            sorts.Add("Date");
            cmbSortBy.ItemsSource = sorts;
            cmbSortBy.SelectedValue = null;
        }

        /// <summary>
        /// This method will create the treeview structure of the report page and add it to the structure TreeView
        /// </summary>
        /// <author>Andrew Loesel & Ryley Taub</author>
        /// <created>3/2/23</created>
        private void AddTreeViewItemsToTreeViewStructure()
        {
            /* We can just use a list of TreeViewItems to display the tree structure of our report building structure */
            structure = new List<TreeViewItem>();

            //now add the volunteer node to the parent structure
            structure.Add(GetVolunteerTreeViewItem());
            structure.Add(GetSchoolsTreeViewItem());
            structure.Add(GetFinanceTreeViewItem());
            TreeViewStructure.ItemsSource = structure;
        }

        /// <summary>
        /// This method creates and returns a TreeViewItem that contains the structure of how we want the finance
        /// items to look in the structure treeview
        /// </summary>
        /// <returns>a TreeViewItem for expenses</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        private TreeViewItem GetFinanceTreeViewItem()
        {
            TreeViewItem finance = new TreeViewItem() { Header = "Finance" };

            TreeViewItem schools = new TreeViewItem() { Header = "School Cost Share" };

            schools.Items.Add(new TreeViewItem() { Header = "Date" });
            schools.Items.Add(new TreeViewItem() { Header = "Value" });

            finance.Items.Add(schools);

            //in kind expenses
            TreeViewItem mealInKindExpense = new TreeViewItem { Header = "Meal-In-Kind" };
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Volunteer Name" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Meal Count" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Meal Rate" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Meal FocusTotal" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Bus Rides" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Bus Rate" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Bus FocusTotal" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Volunteer Mileages" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Mileage Rate" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Mileage FocusTotal" });
            mealInKindExpense.Items.Add(new TreeViewItem { Header = "Date" });

            finance.Items.Add(mealInKindExpense);



            TreeViewItem? inKindItems = GetInKindTreeViewItems();
            if (inKindItems != null)
            {
                finance.Items.Add(inKindItems);
            }

            return finance;
        }

        /// <summary>
        /// This method creates and returns a treeview item with the structure of how we want the schools to look
        /// in the structure treeview
        /// </summary>
        /// <returns>a treeviewitem representing the schools structure</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        private TreeViewItem GetSchoolsTreeViewItem()
        {
            TreeViewItem schools = new TreeViewItem() { Header = "Schools" };

            TreeViewItem assignmentInfo = new TreeViewItem() { Header = "Site Assignment" };
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Volunteer Name" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Student Identifier" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "School Name" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Teacher" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Grade Level" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Age 0-5" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Age 5-12" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Classroom Size" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Condition" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Needs" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Desired Outcome" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Hours per Week" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Classroom" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Days" });
            assignmentInfo.Items.Add(new TreeViewItem() { Header = "Date" });

            schools.Items.Add(assignmentInfo);

            TreeViewItem schoolInfo = new TreeViewItem() { Header = "School Information" };
            schoolInfo.Items.Add(new TreeViewItem() { Header = "School Name" });
            schoolInfo.Items.Add(new TreeViewItem() { Header = "Status" });
            schoolInfo.Items.Add(new TreeViewItem() { Header = "Address" });
            schoolInfo.Items.Add(new TreeViewItem() { Header = "Principal" });
            schoolInfo.Items.Add(new TreeViewItem() { Header = "Secratary" });
            schoolInfo.Items.Add(new TreeViewItem() { Header = "Contact Number" });
            schoolInfo.Items.Add(new TreeViewItem { Header = "Days" });
            schoolInfo.Items.Add(new TreeViewItem() { Header = "School Hours" });

            schools.Items.Add(schoolInfo);
            return schools;
        }

        /// <summary>
        /// This method creates and returns a treeview item that represents how we want the volunteers
        /// section to look in the structure treeview
        /// </summary>
        /// <returns>a treeviewitem for the volunteer structure</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        private TreeViewItem GetVolunteerTreeViewItem()
        {
            TreeViewItem volunteer = new TreeViewItem() { Header = "Volunteer" };
            TreeViewItem volunteerGeneral = new TreeViewItem() { Header = "General Information" };

            //here we add items that we want to have volunteer general as a parent to the items of volunteer general
            volunteerGeneral.Items.Add(new TreeViewItem() { Header = "First Name" });
            volunteerGeneral.Items.Add(new TreeViewItem() { Header = "Last Name" });
            volunteerGeneral.Items.Add(new TreeViewItem { Header = "Status" });
            volunteerGeneral.Items.Add(new TreeViewItem { Header = "Phone" });
            volunteerGeneral.Items.Add(new TreeViewItem { Header = "Address" });
            volunteerGeneral.Items.Add(new TreeViewItem { Header = "Email" });
            volunteerGeneral.Items.Add(new TreeViewItem { Header = "Alternate Phone" });
            volunteerGeneral.Items.Add(new TreeViewItem { Header = "Start Date" });
            volunteerGeneral.Items.Add(new TreeViewItem { Header = "Separation Date" });

            //then add it to the volunteer node
            volunteer.Items.Add(volunteerGeneral);

            TreeViewItem demographics = new TreeViewItem { Header = "Demographic Information" };
            demographics.Items.Add(new TreeViewItem() { Header = "Volunteer Name" });
            demographics.Items.Add(new TreeViewItem() { Header = "Last Updated" });
            demographics.Items.Add(new TreeViewItem() { Header = "Date of Birth" });
            demographics.Items.Add(new TreeViewItem() { Header = "Gender" });
            demographics.Items.Add(new TreeViewItem() { Header = "Age" });
            demographics.Items.Add(new TreeViewItem() { Header = "Identifies As" });
            demographics.Items.Add(new TreeViewItem() { Header = "Ethnicity" });
            demographics.Items.Add(new TreeViewItem() { Header = "Racial Group" });
            demographics.Items.Add(new TreeViewItem() { Header = "Veteran" });
            demographics.Items.Add(new TreeViewItem() { Header = "Family of Military" });

            volunteer.Items.Add(demographics);

            TreeViewItem financials = new TreeViewItem { Header = "Financials" };
            financials.Items.Add(new TreeViewItem() { Header = "Volunteer Name" });
            financials.Items.Add(new TreeViewItem() { Header = "Reg Hours" });
            financials.Items.Add(new TreeViewItem() { Header = "YTD Hours" });
            financials.Items.Add(new TreeViewItem() { Header = "PTO Earned" });
            financials.Items.Add(new TreeViewItem() { Header = "PTO Used" });
            financials.Items.Add(new TreeViewItem() { Header = "Stipend Paid" });
            financials.Items.Add(new TreeViewItem() { Header = "Stipend Rate" });
            financials.Items.Add(new TreeViewItem() { Header = "Date" });

            volunteer.Items.Add(financials);

            TreeViewItem oneTimeChecks = new TreeViewItem() { Header = "One Time Checks" };
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "File Photo" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Service Description" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Orientation Training" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Confidence SOU" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Service Start Date" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "NSCHC Check Form" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Background Check" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "ID Copy NSOPW" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "I-Chat" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "True-Screen" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Alias F'Print" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Field-Print Cleared" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "DHS" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "TB Shot" });
            oneTimeChecks.Items.Add(new TreeViewItem() { Header = "Volunteer Name" });

            volunteer.Items.Add(oneTimeChecks);

            TreeViewItem annualChecks = new TreeViewItem() { Header = "Annual Checks" };
            annualChecks.Items.Add(new TreeViewItem { Header = "Schedule and Photo Release" });
            annualChecks.Items.Add(new TreeViewItem() { Header = "Emergency Beneficiary Form" });
            annualChecks.Items.Add(new TreeViewItem() { Header = "Hippa Release" });
            annualChecks.Items.Add(new TreeViewItem() { Header = "Physical" });
            annualChecks.Items.Add(new TreeViewItem() { Header = "Annual Income Car Insurance" });
            annualChecks.Items.Add(new TreeViewItem() { Header = "Volunteer Name" });

            volunteer.Items.Add(annualChecks);

            TreeViewItem tempInfo = new TreeViewItem { Header = "Temporary Information" };
            tempInfo.Items.Add(new TreeViewItem() { Header = "Field Name" });
            tempInfo.Items.Add(new TreeViewItem() { Header = "Field Value" });
            tempInfo.Items.Add(new TreeViewItem() { Header = "Volunteer Name" });

            volunteer.Items.Add(tempInfo);

            return volunteer;
        }

        private TreeViewItem? GetInKindTreeViewItems()
        {
            TreeViewItem inKind = new TreeViewItem() { Header = "In-Kind Expenses" };
            List<ExpenseTypeModel> expenseTypes = _expenseProvider.GetAllExpenseTypes();
            if (errorFlag) { errorFlag = false; return null; }
            if (expenseTypes != null)
            {
                foreach (ExpenseTypeModel expenseType in expenseTypes)
                {
                    TreeViewItem thisExpenseType = new TreeViewItem { Header = expenseType.Name };
                    thisExpenseType.Items.Add(new TreeViewItem() { Header = "Value" });
                    thisExpenseType.Items.Add(new TreeViewItem() { Header = "Volunteer Name" });
                    thisExpenseType.Items.Add(new TreeViewItem() { Header = "Date" });
                    inKind.Items.Add(thisExpenseType);
                }
            }
            TreeViewItem donations = new TreeViewItem { Header = "Donations" };
            donations.Items.Add(new TreeViewItem { Header = "Name" });
            donations.Items.Add(new TreeViewItem { Header = "Type" });
            donations.Items.Add(new TreeViewItem { Header = "Value" });
            donations.Items.Add(new TreeViewItem { Header = "Date" });
            inKind.Items.Add(donations);
            return inKind;
        }
        #endregion


        //Region that holds new code for setting the treeview source from
        //an object rather then by manipulating the item list of the treeview directly
        #region TreeView Methods V2

        /// <summary>
        /// This method will return the treeview item's parent by using the visual tree helper
        /// </summary>
        /// <param name="item">the item whos parent we want to find</param>
        /// <returns>either the parent as a treeview item or null if none exists.</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private TreeViewItem? GetTreeViewItemParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView) && parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeViewItem;
        }

        /// <summary>
        /// This method makes a deep copy of the original item and any children it may have, but does not copy parents. 
        /// We only worry about copying over headers
        /// </summary>
        /// <param name="originalItem">the item we want to copy</param>
        /// <returns>a copy of the original item</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private TreeViewItem CopyTreeViewItemNoParents(TreeViewItem originalItem)
        {
            TreeViewItem copy = new TreeViewItem { Header = originalItem.Header };
            if (originalItem.Items != null)
            {
                foreach (TreeViewItem originalChild in originalItem.Items)
                {
                    copy.Items.Add(CopyTreeViewItemNoParents(originalChild));
                }
                return copy;
            }
            else
            {
                return copy;
            }
        }
        /// <summary>
        /// This method first calls CopyTreeViewItemNoParents to get a copy of the originalItem
        /// without parents, and then works from the original item up until we hit the top parent
        /// </summary>
        /// <param name="originalItem">the item we want to fully copy</param>
        /// <returns>a full copy of the original items parents and children, when returned the head is the topmost parent</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private TreeViewItem CopyTreeViewItemWithParents(TreeViewItem originalItem)
        {
            //get a copy of the item and any children
            TreeViewItem copy = CopyTreeViewItemNoParents(originalItem);
            //now go through the item's parents until we hit null
            TreeViewItem? parent = GetTreeViewItemParent(originalItem);
            while (parent != null)
            {
                TreeViewItem newItem = new TreeViewItem { Header = parent.Header };
                newItem.Items.Add(copy);
                copy = newItem;
                parent = GetTreeViewItemParent(parent);
            }
            return copy;
        }

        /// <summary>
        /// This method will search the List of report treeview items for the itemToFind and return a refernce to that item
        /// </summary>
        /// <param name="itemToFind">the item we want to find</param>
        /// <param name="reportRef">a List of treeviewitems that is derived from report ObservableCollection</param>
        /// <returns>the item we are searching for or null if not found</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private TreeViewItem? SearchReport(TreeViewItem itemToFind, List<TreeViewItem> reportRef)
        {

            foreach (TreeViewItem reportItem in reportRef)
            {
                if (reportItem == itemToFind)
                {
                    return reportItem;
                }
                else
                {
                    return SearchReport(itemToFind, (from TreeViewItem lItem in reportItem.Items select lItem).ToList());
                }
            }
            return null;

        }

        /// <summary>
        /// This method will search for a string match of the provided header text in the report collection,
        /// if found a reference to that item is returned.
        /// </summary>
        /// <param name="strHeaderText">the text we try to match in the report collection</param>
        /// <param name="reportRef">a reference as List<TreeViewItem> to our report collection</param>
        /// <returns>either a reference to a found match, or null</returns>
        private TreeViewItem? SearchReportByHeader(string strHeaderText, List<TreeViewItem> reportRef)
        {
            TreeViewItem? match;
            foreach (TreeViewItem reportItem in reportRef)
            {
                if (reportItem.Header.Equals(strHeaderText))
                {
                    return reportItem;
                }
                match = SearchReportByHeader(strHeaderText, (from TreeViewItem lItem in reportItem.Items select lItem).ToList());
                if (match != null)
                {
                    return match;
                }
            }
            return null;
        }

        /// <summary>
        /// This method is used to add an item that has no parents to the report collection. We fully copy
        /// the item to add and its children, and then see if some form of the item already exists in the report collection,
        /// if so we remove the old item and add the new item.
        /// </summary>
        /// <param name="rootItem">the item we want to add to the top of our report collection</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private void AddRootLevelItem(TreeViewItem rootItem)
        {
            //Attempt to get the original item with root header from the report list
            TreeViewItem? structureItem = report.Where(x => x.Header.Equals(rootItem.Header)).FirstOrDefault();
            if (structureItem != null)
            {
                //if we found the orignal item we can just remove it and add this one in to get all items.
                report.Remove(structureItem);
                report.Add(CopyTreeViewItemNoParents(rootItem));
            }
            else
            {
                //add this item in.
                report.Add(CopyTreeViewItemNoParents(rootItem));
            }
            TreeViewReport.ItemsSource = report;
        }
        /// <summary>
        /// This method will add an item that has both children and parents to the report collection. If a match is already found (an item
        /// that has the same parents and same header) we renmove it and add the new one. If no match is found we create a full copy with parents
        /// from the structure treeview included, and go down each level until we don't find a match and then add this item to the previous references
        /// items, if no match is found then we just add the full copy to report.
        /// </summary>
        /// <param name="midLevelItem">the item with parents and children we want to add to the report collection</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private void AddMidLevelItem(TreeViewItem midLevelItem)
        {
            //search for a match in our report structure
            TreeViewItem? match = SearchReportByHeader(midLevelItem.Header.ToString() ?? "null", report.ToList());
            if (match != null)
            {
                /* this item already exists in some form in the report treeview, since this item has children we will want to
                 * replace the existing item in the report with the this item and all its children */
                TreeViewItem? parent = GetTreeViewItemParent(match);
                if (parent != null)
                {
                    parent.Items.Remove(match);
                    parent.Items.Add(CopyTreeViewItemNoParents(midLevelItem));
                    //refresh the datasource
                    TreeViewReport.ItemsSource = report;
                    return;
                }
                else
                {
                    //we shouldn't make it here if, but if this method is called at the wrong place (say for adding a root) it's possible so return to prevent weirdness
                    return;
                }
            }
            else
            {
                //this item is not in the report yet, so we will want to add it to it's parent, first we have to copy the full item
                TreeViewItem fullCopy = CopyTreeViewItemWithParents(midLevelItem);
                //now we will search through the report top down by header until we don't find a match
                TreeViewItem? reportRef = SearchReportByHeader(fullCopy.Header.ToString() ?? "null", report.ToList());
                TreeViewItem? parseItem = fullCopy.Items[0] as TreeViewItem;
                while (reportRef != null && parseItem != null)
                {
                    TreeViewItem? newRef = SearchReportByHeader(parseItem.Header.ToString() ?? "null", report.ToList());
                    if (newRef == null)
                    {
                        //no reference found so we can add this item to the previous references children
                        reportRef.Items.Add(CopyTreeViewItemNoParents(parseItem));
                        TreeViewReport.ItemsSource = report;
                        return;
                    }
                    //change the previous reference to newRef
                    reportRef = newRef;
                    parseItem = parseItem.Items[0] as TreeViewItem;
                }
            }
            //if we make it here, the item was not in the report and no references to parents were found, we can add the full copy to the report
            report.Add(CopyTreeViewItemWithParents(midLevelItem));
            TreeViewReport.ItemsSource = report;

        }

        /// <summary>
        /// this method attempts to add a treeview item that has no parents to the report collection. We first make a full of the structure item, with parents.
        /// we then work our way down the tree until we do not find a header match in the report collection, at that point we add to the previous resolved reference.
        /// if no matches are in the report for any of the items we just add the whole copied item to the report.
        /// </summary>
        /// <param name="itemToAdd">the item with no children we want to add to the report collection</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private void AddBottomLevelItem(TreeViewItem itemToAdd)
        {
            TreeViewItem? copy = CopyTreeViewItemWithParents(itemToAdd);
            if (copy != null)
            {
                TreeViewItem? refInReport = SearchReportByHeader(copy.Header.ToString() ?? "null", report.ToList());
                if (refInReport == null)
                {
                    //this case covers the most root node of the item to add not being in the report
                    report.Add(copy);
                    TreeViewReport.ItemsSource = report;
                }
                else
                {
                    //now we go through each item in the copy, and see if it is already in the report or not
                    while (copy != null && copy.HasItems)
                    {
                        copy = copy.Items[0] as TreeViewItem;
                        if (copy != null)
                        {
                            string? strCopyHeader = copy.Header.ToString();
                            if (!string.IsNullOrEmpty(strCopyHeader))
                            {
                                TreeViewItem? newRef = SearchReportByHeader(strCopyHeader, report.ToList());
                                if (newRef == null)
                                {
                                    //we didn't find a reference!
                                    foreach (TreeViewItem item in refInReport.Items)
                                    {
                                        //loop through the items of our last know reference to check for a header match
                                        if (item.Header.ToString() == strCopyHeader)
                                        {
                                            return;
                                        }
                                    }
                                    //no header match, add this as a child to our last matched reference and refresh the itemssource
                                    refInReport.Items.Add(CopyTreeViewItemNoParents(copy));
                                    TreeViewReport.ItemsSource = report;
                                    return;
                                }
                                else
                                {
                                    //we found a reference, so we point refInReport to the newRef and loop again
                                    refInReport = newRef;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //we somehow got a bad item from the structure
            }


        }

        /// <summary>
        /// This method adds the selected tree view item with the proper method, depending on if the selected structure
        /// item has children or parent.
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private void AddItemToTreeViewReport()
        {
            TreeViewItem? structureItem = TreeViewStructure.SelectedItem as TreeViewItem;
            if (structureItem != null)
            {
                if (GetTreeViewItemParent(structureItem) != null)
                {
                    if (structureItem.HasItems)
                    {
                        //the item has a parent and has children
                        AddMidLevelItem(structureItem);
                    }
                    else
                    {
                        //the item just has a parent
                        AddBottomLevelItem(structureItem);
                    }

                }
                else
                {
                    //adding a root level item
                    AddRootLevelItem(structureItem);
                }

            }
        }

        /// <summary>
        /// This method will remove the item from it's parent TreeViewItem node, and then check
        /// each level of parent's Items to see if there are other children, if not then the parent
        /// is deleted until we find a parent that has more children.
        /// </summary>
        /// <param name="deleteItem">The item we wish to delete</param>
        /// <returns>true if the item was deleted, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/27/2023</created>
        private bool RemoveTreeViewReportItem(TreeViewItem deleteItem)
        {
            //search item tree
            if (deleteItem != null)
            {
                //get reference to the item's parent
                TreeViewItem? parent = GetTreeViewItemParent(deleteItem);
                if (parent != null)
                {
                    parent.Items.Remove(deleteItem);
                    //now we need to make sure that parent still has items
                    TreeViewItem? grandpParent = GetTreeViewItemParent(parent);
                    while (grandpParent != null && !parent.HasItems)
                    {
                        grandpParent.Items.Remove(parent);
                        parent = grandpParent;
                        grandpParent = GetTreeViewItemParent(grandpParent);
                    }
                    //now we are on the final parent
                    if (!parent.HasItems)
                    {
                        report.Remove(parent);
                    }
                }
                else
                {
                    report.Remove(deleteItem);
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion




        #region Excel Methods
        /// <summary>
        /// Add sheets to excel file model.
        /// </summary>
        /// <param name="excelFile">Excel file model.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/18/23</created>
        private void AddSheetsToExcelModel(ExcelFileModel excelFile)
        {
            foreach (TreeViewItem sheetItem in TreeViewReport.Items)
            {
                if (sheetItem != null)
                {
                    ExcelSheetModel excelSheetModel = new ExcelSheetModel()
                    {
                        Title = (string)sheetItem.Header,
                        Tables = new List<ExcelTableModel>()
                    };

                    excelFile.Sheets.Add(excelSheetModel);

                    AddTablesToExcelModel(sheetItem, excelSheetModel);
                }
            }
        }

        /// <summary>
        /// Add tree items to excel to excel model tables.
        /// </summary>
        /// <param name="sheetItem">Tree sheet level item.</param>
        /// <param name="excelSheetModel">Excel sheet model.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/18/23</created>
        private void AddTablesToExcelModel(TreeViewItem sheetItem, ExcelSheetModel excelSheetModel)
        {
            foreach (TreeViewItem table in sheetItem.Items)
            {
                if (string.IsNullOrEmpty(table.Header.ToString()))
                {
                    continue;
                }
                else if (table.Header.Equals("In-Kind Expenses"))
                {
                    /* In-Kind expenses are generated from the database and they have an extra level than the other items
                       so we need to call this method recursively for each of TreeViewItem "In-Kind Expenses" children */
                    foreach (TreeViewItem expenseName in table.Items)
                    {
                        //we call using table instead of expenseName here, expense name will be Physicals, Paper, etc. and then the table
                        //would be what we want the columns of the excel file to be
                        AddTablesToExcelModel(table, excelSheetModel);
                        return;
                    }
                }
                ExcelTableModel excelTable = new ExcelTableModel()
                {
                    Title = (string)table.Header,
                    Headers = new List<string>()
                };

                excelSheetModel.Tables.Add(excelTable);

                AddColumnsToExcelModel(table, excelTable);


                /*
                 * This is where the rows will need to be dynamically added/
                 * I'm adding an empty table row as an example.
                 */
                List<object>? dataObjects = GetDataObjectsFromHeader(table);
                if (dataObjects != null)
                {
                    excelTable.Rows = dataObjects;
                }

            }
        }

        /// <summary>
        /// Add the columns from the table to the excel headers.
        /// </summary>
        /// <param name="table">Tree table.</param>
        /// <param name="excelTable">Excel table model.</param>
        /// <author>Tyler Moody</author>
        /// <created>03/18/23</created>
        private static void AddColumnsToExcelModel(TreeViewItem table, ExcelTableModel excelTable)
        {
            foreach (TreeViewItem columns in table.Items)
            {
                excelTable.Headers.Add((string)columns.Header);
            }
        }

        /// <summary>
        /// This method is used to create the initial excelfilemodel
        /// </summary>
        /// <returns>an ExcelFileModel with the filename</returns>
        /// <author>Tyler Moody</author>
        /// <created>??</created>
        private ExcelFileModel ConvertTreeToExcelFileModel()
        {
            ExcelFileModel excelFile = new ExcelFileModel()
            {
                FileName = "Report",
                Sheets = new List<ExcelSheetModel>()
            };

            AddSheetsToExcelModel(excelFile);

            return excelFile;
        }
        #endregion

        #region gettingdatafromprovider
        /// <summary>
        /// This method will go through each possible "2nd level" header case and use the provider to get the necessary information from the provider.
        /// </summary>
        /// <param name="strHeader">the "2nd level" header we are getting information for</param>
        /// <returns>a generics list of object type.</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/24/2023</created>
        private List<object>? GetDataObjectsFromHeader(TreeViewItem itemToMakeATable)
        {
            if (itemToMakeATable.Header == null)
            {
                return null;
            }
            List<object>? dataObjects = new List<object>();

            switch (itemToMakeATable.Header)
            {
                case "General Information":
                    dataObjects = GetGeneralInformation(itemToMakeATable);
                    break;
                case "Demographic Information":
                    dataObjects = GetDemographicInformation(itemToMakeATable);
                    break;
                case "Financials":
                    dataObjects = GetVolunteerFinancials(itemToMakeATable);
                    break;
                case "One Time Checks":
                    dataObjects = GetOneTimeChecks(itemToMakeATable);
                    break;
                case "Annual Checks":
                    dataObjects = GetAnnualChecks(itemToMakeATable);
                    break;
                case "Temporary Information":
                    dataObjects = GetTempInfo(itemToMakeATable);
                    break;
                case "Site Assignment":
                    dataObjects = GetSiteAssignments(itemToMakeATable);
                    break;
                case "School Information":
                    dataObjects = GetSchoolInformation(itemToMakeATable);
                    break;
                case "School Cost Share":
                    dataObjects = GetSchoolCostShare(itemToMakeATable);
                    break;
                case "Meal-In-Kind":
                    dataObjects = GetMealInKind(itemToMakeATable);
                    break;
                default:
                    dataObjects = SearchConfigurables(itemToMakeATable);
                    break;

            }

            return dataObjects;
        }


        #region volunteerFinancials
        /// <summary>
        /// This method gets a list of dynamic objects for the with properties and values corresponding
        /// to the items that are in the report treeview item for the volunteer finance section
        /// </summary>
        /// <param name="volunteerFinancialTreeViewItem">the volunteer finance report treeview item</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<object>? GetVolunteerFinancials(TreeViewItem volunteerFinancialTreeViewItem)
        {
            List<object> dataObjects = new List<object>();
            List<VolunteerFinanceReportModel> volunteerFinances = new List<VolunteerFinanceReportModel>();
            DateTime? startDate = dtpStartDate.SelectedDate;
            DateTime? endDate = dtpEndDate.SelectedDate;
            if (volunteerFinancialTreeViewItem != null && startDate != null && endDate != null && chkSelectAllVolunteers.IsChecked != null)
            {
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                if (blnSelectAllVolunteers)
                {
                    bool? blnActive = chkActive.IsChecked;
                    bool? blnInactive = chkInactive.IsChecked;
                    bool? blnFormer = chkFormer.IsChecked;
                    bool? blnCurrent = chkCurrent.IsChecked;
                    if (blnActive != null && blnInactive != null && blnFormer != null && blnCurrent != null)
                    {
                        volunteerFinances = _reportProvider.GetAllVolunteerFinances((DateTime)startDate, (DateTime)endDate, (bool)blnActive, (bool)blnInactive, (bool)blnCurrent, (bool)blnFormer);
                        if (errorFlag) { errorFlag = false; return null; }
                    }
                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? intSelectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (intSelectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? selectedVolunteerTuid = intSelectedVolunteer.Tuid;
                        if (selectedVolunteerTuid != null)
                        {
                            volunteerFinances = _reportProvider.GetFinancesForVolunteer((DateTime)startDate, (DateTime)endDate, (int)selectedVolunteerTuid);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
                if (volunteerFinances != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            volunteerFinances = volunteerFinances.OrderBy(x => x.VolunteerName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            volunteerFinances = volunteerFinances.OrderByDescending(x => x.Date).ToList();
                        }
                    }
                    //loop through each volunteer finance item returned from the database
                    foreach (VolunteerFinanceReportModel financeItem in volunteerFinances)
                    {
                        dynamic dataObject = new ExpandoObject();

                        //loop through each item in the volunteer finance treeview report item to get the desired attributes
                        foreach (TreeViewItem columnName in volunteerFinancialTreeViewItem.Items)
                        {
                            switch (columnName.Header)
                            {
                                case "Volunteer Name":
                                    dataObject.VolunteerName = financeItem.VolunteerName;
                                    break;
                                case "Reg Hours":
                                    dataObject.RegHours = financeItem.RegHours;
                                    break;
                                case "YTD Hours":
                                    dataObject.YTD = financeItem.YTDHours;
                                    break;
                                case "PTO Earned":
                                    dataObject.PTOEarned = financeItem.PTOEarned;
                                    break;
                                case "PTO Used":
                                    dataObject.PTOUsed = financeItem.PTOUsed;
                                    break;
                                case "Stipend Paid":
                                    dataObject.StipendPaid = financeItem.StipendPaid;
                                    break;
                                case "Stipend Rate":
                                    DateTime? date = financeItem.Date;
                                    if (date != null)
                                    {
                                        dataObject.StipendRate = _reportProvider.GetStipendRate((DateTime)date);
                                        if (errorFlag) { errorFlag = false; return null; }
                                    }
                                    else
                                    {
                                        dataObject.StipendRate = 0;
                                    }
                                    break;
                                case "Date":
                                    DateTime? date2 = financeItem.Date;
                                    if (date2 != null)
                                    {
                                        dataObject.Date = ((DateTime)date2).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.Date = "null";
                                    }
                                    break;
                                default:
                                    break;

                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                    return dataObjects;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region schoolCostShare

        /// <summary>
        /// This method gets a list of dynamic objects containing values for the fields
        /// of school cost share that are in the report treeview.
        /// </summary>
        /// <param name="schoolCostShareTreeViewItem">the school cost share report treeview item</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<object>? GetSchoolCostShare(TreeViewItem schoolCostShareTreeViewItem)
        {
            List<object> dataObjects = new List<object>();
            IEnumerable<SchoolCostShareModel> costShares = new List<SchoolCostShareModel>();
            DateTime? startDate = dtpStartDate.SelectedDate;
            DateTime? endDate = dtpEndDate.SelectedDate;
            if (schoolCostShareTreeViewItem != null && startDate != null && endDate != null)
            {
                costShares = _expenseProvider.GetCostShares((DateTime)startDate, (DateTime)endDate);
                if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
                if (costShares != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Date"))
                        {
                            costShares = costShares.OrderByDescending(x => x.Date).ToList();
                        }
                    }
                    //loop through each cost share returned
                    foreach (SchoolCostShareModel costShareItem in costShares)
                    {
                        dynamic dataObject = new ExpandoObject();
                        //loop through each item in the school cost share treeview item to see which attributes to add
                        foreach (TreeViewItem columnName in schoolCostShareTreeViewItem.Items)
                        {
                            switch (columnName.Header)
                            {
                                case "Date":
                                    dataObject.Date = costShareItem.Date.ToString("d");
                                    break;
                                case "Value":
                                    dataObject.Value = costShareItem.Value;
                                    break;
                                default:
                                    break;
                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                    return dataObjects;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region mealInKind
        /// <summary>
        /// This method returns a list of dynamic objects for the meal in kind items in the report treeview
        /// </summary>
        /// <param name="mealInKindTreeViewItem">the meal in kind report treeview item</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<object>? GetMealInKind(TreeViewItem mealInKindTreeViewItem)
        {
            List<object>? dataObjects = new List<object>();
            List<MealInKindReportModel> mealInKindEntries = new List<MealInKindReportModel>();
            DateTime? startDate = dtpStartDate.SelectedDate;
            DateTime? endDate = dtpEndDate.SelectedDate;
            if (mealInKindTreeViewItem != null && chkSelectAllVolunteers.IsChecked != null && startDate != null && endDate != null)
            {
                //see if we are doing all volunteers or just one
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                if (blnSelectAllVolunteers)
                {
                    mealInKindEntries = _reportProvider.GetMealInKindAllVolunteers((DateTime)startDate, (DateTime)endDate
                        , chkActive.IsChecked ?? false, chkInactive.IsChecked ?? false, chkCurrent.IsChecked ?? false, chkFormer.IsChecked ?? false);
                    if (errorFlag) { errorFlag = false; return null; }
                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? intSelectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (intSelectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? selectedVolunteerTuid = intSelectedVolunteer.Tuid;
                        if (selectedVolunteerTuid != null)
                        {
                            mealInKindEntries = _reportProvider.GetMealInKindForVolunteer((DateTime)startDate, (DateTime)endDate, (int)selectedVolunteerTuid);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
                if (mealInKindEntries != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            mealInKindEntries = mealInKindEntries.OrderBy(x => x.strVolunteerName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            mealInKindEntries = mealInKindEntries.OrderByDescending(x => x.date).ToList();
                        }
                    }
                    //loop through each meal in kind entry returned from the database
                    foreach (MealInKindReportModel mikEntry in mealInKindEntries)
                    {
                        dynamic dataObject = new ExpandoObject();
                        //loop throug each item that is in the meal in kind treeview report items
                        foreach (TreeViewItem columnName in mealInKindTreeViewItem.Items)
                        {
                            switch (columnName.Header)
                            {
                                case "Volunteer Name":
                                    dataObject.VolunteerName = mikEntry.strVolunteerName;
                                    break;
                                case "Meal Count":
                                    dataObject.MealCount = mikEntry.numMeals;
                                    break;
                                case "Meal Rate":
                                    dataObject.MealRate = mikEntry.mealRate.ToString("c");
                                    break;
                                case "Meal FocusTotal":
                                    dataObject.MealTotal = (double)(mikEntry.numMeals * mikEntry.mealRate);
                                    break;
                                case "Bus Rides":
                                    dataObject.BusRides = mikEntry.numBusRides;
                                    break;
                                case "Bus Rate":
                                    dataObject.BusRate = mikEntry.busRate.ToString("c");
                                    break;
                                case "Bus FocusTotal":
                                    dataObject.BusTotal = (double)(mikEntry.numBusRides * mikEntry.busRate);
                                    break;
                                case "Volunteer Mileages":
                                    dataObject.VolunteerMileage = mikEntry.Mileage;
                                    break;
                                case "Mileage Rate":
                                    dataObject.MileageRate = mikEntry.mileRate.ToString("c");
                                    break;
                                case "Mileage FocusTotal":
                                    dataObject.MileageTotal = (double)((double)mikEntry.Mileage * mikEntry.mileRate);
                                    break;
                                case "Date":
                                    DateTime? date = mikEntry.date;
                                    if (date != null)
                                    {
                                        dataObject.Date = ((DateTime)date).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.Date = "null";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                    return dataObjects;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region demographicInformation

        /// <summary>
        /// This method gets a list of dynamic objects containing the selected volunteer demographic
        /// items in the report treeview
        /// </summary>
        /// <param name="demographicTreeViewItem">the demographic report treeview item</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<object>? GetDemographicInformation(TreeViewItem demographicTreeViewItem)
        {
            List<object> dataObjects = new List<object>();
            List<DemographicReportModel> demographicsItems = new List<DemographicReportModel>();
            if (demographicTreeViewItem != null && chkSelectAllVolunteers.IsChecked != null)
            {
                //see if we are doing all volunteers or just one
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                if (blnSelectAllVolunteers)
                {
                    demographicsItems = _reportProvider.GetDemographicsAllVolunteers(chkActive.IsChecked ?? false, chkInactive.IsChecked ?? false, chkCurrent.IsChecked ?? false, chkFormer.IsChecked ?? false);
                    if (errorFlag) { errorFlag = false; return null; }
                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? intSelectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (intSelectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? selectedVolunteerTuid = intSelectedVolunteer.Tuid;
                        if (selectedVolunteerTuid != null)
                        {
                            demographicsItems = _reportProvider.GetDemographicsForVolunteer((int)selectedVolunteerTuid);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }

                    }
                }

                if (demographicsItems != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            demographicsItems = demographicsItems.OrderBy(x => x.VolunteerName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            demographicsItems = demographicsItems.OrderByDescending(x => x.DOB).ToList();
                        }
                    }
                    //loop through each item returned from the database
                    foreach (DemographicReportModel item in demographicsItems)
                    {
                        dynamic dataObject = new ExpandoObject(); //a dynamic object to add only the data we need to
                        //loop through each item in the demographics treeview report item
                        foreach (TreeViewItem columnName in demographicTreeViewItem.Items)
                        {
                            switch (columnName.Header)
                            {
                                case "Volunteer Name":
                                    dataObject.VolunteerName = item.VolunteerName;
                                    break;
                                case "Last Updated":
                                    DateTime? lastUpdate = item.LastUpdated;
                                    if (lastUpdate != null)
                                    {
                                        dataObject.LastUpdated = ((DateTime)lastUpdate).ToString("");
                                    }
                                    else
                                    {
                                        dataObject.LastUpdated = "null";
                                    }
                                    break;
                                case "Date of Birth":
                                    DateTime? DOB = item.DOB;
                                    if (DOB != null)
                                    {
                                        dataObject.DOB = item.DOB.ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.DOB = "null";
                                    }
                                    break;
                                case "Gender":
                                    dataObject.Gender = item.Gender;
                                    break;
                                case "Age":
                                    dataObject.Age = item.Age;
                                    break;
                                case "Identifies As":
                                    dataObject.IdentifiesAs = item.IdentifiesAs;
                                    break;
                                case "Ethnicity":
                                    dataObject.Ethnicity = item.Ethnicity;
                                    break;
                                case "Racial Group":
                                    dataObject.RacialGroup = item.RacialGroup;
                                    break;
                                case "Veteran":
                                    dataObject.IsVeteran = item.IsVeteran;
                                    break;
                                case "Family of Military":
                                    dataObject.FamilyOfMilitary = item.FamilyOfMilitary;
                                    break;
                                default:
                                    break;
                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                    return dataObjects;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region siteAssignments

        /// <summary>
        /// This method will get a list of dynamic objects that contain properties corresponding to the site
        /// assignment options that are in the report treeview
        /// </summary>
        /// <param name="siteAssignmentItem">the site assignment treeview item from the report treeview</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/26/2023</created>
        public List<object>? GetSiteAssignments(TreeViewItem siteAssignmentItem)
        {
            List<object>? dataObjects = new List<object>();//a list of dynamic objects
            List<SiteAssignmentReportModel> assignments = new List<SiteAssignmentReportModel>();
            if (siteAssignmentItem != null && chkSelectAllVolunteers.IsChecked != null)
            {
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                DateTime? startDate = dtpStartDate.SelectedDate;
                DateTime? endDate = GetEndDate();
                if (blnSelectAllVolunteers)
                {
                    bool? blnActive = chkActive.IsChecked;
                    bool? blnInactive = chkInactive.IsChecked;
                    bool? blnFormer = chkFormer.IsChecked;
                    bool? blnCurrent = chkCurrent.IsChecked;

                    if (startDate != null && endDate != null && blnActive != null && blnInactive != null && blnFormer != null && blnCurrent != null)
                    {
                        assignments = _reportProvider.GetSiteAssignmentsAllVolunteers((DateTime)startDate, (DateTime)endDate, (bool)blnActive, (bool)blnInactive, (bool)blnCurrent, (bool)blnFormer);
                        if (errorFlag) { errorFlag = false; return null; }
                    }

                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? selectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (selectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? intSelectedVolunteerTuid = selectedVolunteer.Tuid;
                        if (intSelectedVolunteerTuid != null && startDate != null && endDate != null)
                        {
                            assignments = _reportProvider.GetSiteAssignmentsForVolunteer((int)intSelectedVolunteerTuid, (DateTime)startDate, (DateTime)endDate);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }

                    }
                }

                if (assignments != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            assignments = assignments.OrderBy(x => x.VolunteerName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            assignments = assignments.OrderByDescending(x => x.Date).ToList();
                        }
                    }
                    //loop through each assignment item returnerd from the database
                    foreach (SiteAssignmentReportModel assignment in assignments)
                    {
                        dynamic dataObject = new ExpandoObject(); //a dynamic object to add only the selected properties to
                        //loop through each selected property in the report treeview
                        foreach (TreeViewItem columnName in siteAssignmentItem.Items)
                        {
                            switch (columnName.Header)
                            {
                                case "Volunteer Name":
                                    dataObject.VolunteerName = assignment.VolunteerName;
                                    break;
                                case "Student Identifier":
                                    dataObject.StudentIdentifier = assignment.StudentIdentifier;
                                    break;
                                case "School Name":
                                    dataObject.SchoolName = assignment.SchoolName;
                                    break;
                                case "Teacher":
                                    dataObject.Teacher = assignment.Teacher;
                                    break;
                                case "Grade Level":
                                    dataObject.Grade = assignment.GradeLevel;
                                    break;
                                case "Age 0-5":
                                    dataObject.Age0To5 = assignment.Age0To5;
                                    break;
                                case "Age 5-12":
                                    dataObject.Age5To12 = assignment.Age5To12;
                                    break;
                                case "Classroom Size":
                                    dataObject.ClassroomSize = assignment.ClassroomSize;
                                    break;
                                case "Condition":
                                    dataObject.Condition = assignment.Condition;
                                    break;
                                case "Needs":
                                    dataObject.Needs = assignment.Needs;
                                    break;
                                case "Desired Outcome":
                                    dataObject.DesiredOutcome = assignment.DesiredOutcome;
                                    break;
                                case "Hours per Week":
                                    dataObject.HoursPerWeek = assignment.HoursPerWeek;
                                    break;
                                case "Classroom":
                                    dataObject.Classroom = assignment.Classroom;
                                    break;
                                case "Days":
                                    string? strDays = assignment.Days;
                                    if (strDays != null)
                                    {
                                        strDays = ReplaceDays(strDays);
                                        dataObject.Days = strDays;
                                    }
                                    else
                                    {
                                        dataObject.Days = "null";
                                    }
                                    break;
                                case "Date":
                                    DateTime? date = assignment.Date;
                                    if (date != null)
                                    {
                                        dataObject.Date = ((DateTime)date).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.Date = null;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                    return dataObjects;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }

        #endregion

        #region oneTimeChecks
        /// <summary>
        /// This method will get a list of dynamic objects containing the volunteer one time check information that is
        /// selected in the report treeview.
        /// </summary>
        /// <param name="oneTimeCheckItem"></param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<object>? GetOneTimeChecks(TreeViewItem oneTimeCheckItem)
        {
            List<object>? dataObjects = new List<object>(); //list of dynamic objects
            List<OneTimeChecksModel> oneTimeChecks = new List<OneTimeChecksModel>();
            if (oneTimeCheckItem != null && chkSelectAllVolunteers.IsChecked != null)
            {
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                if (blnSelectAllVolunteers)
                {
                    //get filtering booleans 
                    bool? blnActive = chkActive.IsChecked;
                    bool? blnInactive = chkInactive.IsChecked;
                    bool? blnFormer = chkFormer.IsChecked;
                    bool? blnCurrent = chkCurrent.IsChecked;
                    if (blnActive != null && blnInactive != null && blnCurrent != null && blnFormer != null)
                    {
                        oneTimeChecks = _reportProvider.GetOneTimeChecksAllVolunteers((bool)blnActive, (bool)blnInactive, (bool)blnCurrent, (bool)blnFormer);
                        if (errorFlag) { errorFlag = false; return null; }
                    }

                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? selectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (selectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? intSelectedVolunteerTuid = selectedVolunteer.Tuid;
                        if (intSelectedVolunteerTuid != null)
                        {
                            oneTimeChecks = _reportProvider.GetOneTimeChecksForVolunteer((int)intSelectedVolunteerTuid);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                if (oneTimeChecks != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            oneTimeChecks = oneTimeChecks.OrderBy(x => x.VolunteerName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            oneTimeChecks = oneTimeChecks.OrderByDescending(x => x.OrientTraining).ToList();
                        }
                    }
                    //loop through each one time check item returned from the database provider
                    foreach (OneTimeChecksModel currentChecks in oneTimeChecks)
                    {
                        dynamic dataObject = new ExpandoObject();
                        //loop through each item in the report treeview one time checks item
                        foreach (TreeViewItem columnName in oneTimeCheckItem.Items)
                        {
                            switch (columnName.Header)
                            {
                                case "File Photo":
                                    dataObject.FilePhoto = currentChecks.FilePhoto;
                                    break;
                                case "Service Description":
                                    dataObject.ServiceDesc = currentChecks.ServiceDescription;
                                    break;
                                case "Orientation Training":
                                    dataObject.Orientation = currentChecks.OrientTraining;
                                    break;
                                case "Confidence SOU":
                                    if (currentChecks.ConfidenceSOU != null)
                                    {
                                        dataObject.SOU = ((DateTime)currentChecks.ConfidenceSOU).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.SOU = "null";
                                    }
                                    break;
                                case "Service Start Date":
                                    if (currentChecks.ServiceStartDate != null)
                                    {
                                        dataObject.StartDate = ((DateTime)currentChecks.ServiceStartDate).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.StartDate = "null";
                                    }
                                    break;
                                case "NSCHC Check Form":
                                    dataObject.NSCHC = currentChecks.NSCHCCheckForm;
                                    break;
                                case "Background Check":
                                    dataObject.BackgroundCheck = currentChecks.BackgroundCheck;
                                    break;
                                case "ID Copy NSOPW":
                                    if (currentChecks.NSOPW != null)
                                    {
                                        dataObject.NSOPW = ((DateTime)currentChecks.NSOPW).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.NSOPW = "null";
                                    }
                                    break;
                                case "I-Chat":
                                    if(currentChecks.IChat != null)
                                    {
                                        dataObject.IChat = currentChecks.IChat;
                                    }
                                    else
                                    {
                                        dataObject.IChat = "null";  
                                    }
                                    
                                    break;
                                case "True-Screen":
                                    if (currentChecks.TrueScreen != null)
                                    {
                                        dataObject.TrueScreen = ((DateTime)currentChecks.TrueScreen).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.TrueScreen = "null";
                                    }

                                    break;
                                case "Alias F'Print":
                                    if (currentChecks.AliasFingerPrint != null)
                                    {
                                        dataObject.FPrint = ((DateTime)currentChecks.AliasFingerPrint).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.FPrint = "null";
                                    }
                                    break;
                                case "Field-Print Cleared":
                                    if (currentChecks.FieldPrintCleared != null)
                                    {
                                        dataObject.PrintCleared = ((DateTime)currentChecks.FieldPrintCleared).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.PrintCleared = "null";
                                    }
                                    break;
                                case "DHS":
                                    if (currentChecks.DHS != null)
                                    {
                                        dataObject.DHS = ((DateTime)currentChecks.DHS).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.DHS = "null";
                                    }
                                    break;
                                case "TB Shot":
                                    if (currentChecks.TBShot != null)
                                    {
                                        dataObject.TB = ((DateTime)currentChecks.TBShot).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.TB = "null";
                                    }
                                    break;
                                case "Volunteer Name":
                                    dataObject.Volunteer = currentChecks.VolunteerName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                    return dataObjects;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region schoolInformation

        /// <summary>
        /// This method will return a list of dynamic objects that are given properties according to the school information items
        /// in the report treeview
        /// </summary>
        /// <param name="schoolInformationTreeViewItem">the school information report treeview item</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<object>? GetSchoolInformation(TreeViewItem schoolInformationTreeViewItem)
        {
            List<object>? dataObjects = new List<object>(); //list of dynamic objects
            List<SchoolInformationReportModel> schoolInfo = new List<SchoolInformationReportModel>();
            if (schoolInformationTreeViewItem != null)
            {

                schoolInfo = _reportProvider.GetSchoolInformation(chkActive.IsChecked ?? false, chkInactive.IsChecked ?? false, chkCurrent.IsChecked ?? false, chkFormer.IsChecked ?? false);
                if (errorFlag) { errorFlag = false; return null; }

                //loop through each data item returned by the database call
                foreach (var dataModel in schoolInfo)
                {
                    dynamic dataObject = new ExpandoObject();
                    //loop through each item in the school information report structure
                    foreach (TreeViewItem columnName in schoolInformationTreeViewItem.Items)
                    {
                        switch (columnName.Header)
                        {
                            case "School Name":
                                dataObject.Name = dataModel.SchoolName;
                                break;
                            case "Status":
                                if (dataModel.IsActive)
                                {
                                    dataObject.Status = "Active";
                                }
                                else if (dataModel.IsDeleted)
                                {
                                    dataObject.Status = "Deleted";
                                }
                                else
                                {
                                    //default to inactive
                                    dataObject.Status = "Inactive";
                                }
                                break;
                            case "Address":
                                dataObject.Address = dataModel.Address;
                                break;
                            case "Principal":
                                dataObject.Principal = dataModel.Principal;
                                break;
                            case "Secratary":
                                dataObject.Secratary = dataModel.Secratary;
                                break;
                            case "Contact Number":
                                dataObject.Contact = dataModel.Phone;
                                break;
                            case "Days":
                                string? strDays = dataModel.Days;
                                //since days in the db are 1-5 we have to replace them with the proper day here
                                if (strDays != null)
                                {
                                    strDays = ReplaceDays(strDays);
                                    dataObject.Days = strDays;
                                }
                                else
                                {
                                    dataObject.Days = null;
                                }

                                break;
                            case "School Hours":
                                dataObject.Hours = dataModel.Hours;
                                break;
                            default:
                                break;
                        }
                    }
                    dataObjects.Add(dataObject);
                }
                return dataObjects;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region tempInformation

        /// <summary>
        /// This method will return a list of dynamic objects with properties and values corresponding to the report treeview
        /// temporary info selected items. 
        /// </summary>
        /// <param name="tempInfoItem">the report TreeView temporary information item</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<object>? GetTempInfo(TreeViewItem tempInfoItem)
        {
            List<object>? dataObjects = new List<object>(); //list of dynamic objects
            List<TempInfoReportModel> tempInfos = new List<TempInfoReportModel>();
            if (tempInfoItem != null && chkSelectAllVolunteers.IsChecked != null)
            {
                //see if we want to get temp infor for all volunteers or just one.
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                if (blnSelectAllVolunteers)
                {
                    tempInfos = _reportProvider.GetTempInfoForAllVolunteers(chkActive.IsChecked ?? false, chkInactive.IsChecked ?? false, chkCurrent.IsChecked ?? false, chkFormer.IsChecked ?? false);
                    if (errorFlag) { errorFlag = false; return null; }
                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? selectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (selectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? intSelectedVolunteerTuid = selectedVolunteer.Tuid;
                        if (intSelectedVolunteerTuid != null)
                        {
                            tempInfos = _reportProvider.GetTempInforForVolunteer((int)intSelectedVolunteerTuid);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
                if (tempInfos != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            tempInfos = tempInfos.OrderBy(x => x.VolunteerName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            tempInfos = tempInfos.OrderByDescending(x => x.TempInfoValue).ToList();
                        }
                    }
                    //loop through each tempinfo item from the database
                    foreach (TempInfoReportModel tempInfo in tempInfos)
                    {
                        dynamic dataObject = new ExpandoObject();
                        foreach (TreeViewItem columnName in tempInfoItem.Items)
                        {
                            //if the header is in the temp info's items we will add the property to the dynamic object
                            switch (columnName.Header)
                            {
                                case "Field Name":
                                    dataObject.FieldName = tempInfo.TempInfoName;
                                    break;
                                case "Field Value":
                                    DateTime date;
                                    if (DateTime.TryParse(tempInfo.TempInfoValue, out date))
                                    {
                                        dataObject.FieldValue = date.ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.FieldValue = tempInfo.TempInfoValue;
                                    }
                                    break;
                                case "Volunteer Name":
                                    dataObject.VolunteerName = tempInfo.VolunteerName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                    return dataObjects;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }

        #endregion

        #region generalInformation

        /// <summary>
        /// This method will get the general information from the database, and then create a List of dynamic objects
        /// that have properties that correspond with the properties in the report treeview, and assign values from the database
        /// data model
        /// </summary>
        /// <param name="generalInformationTreeViewItem">The report treeview item for general information</param>
        /// <returns>a list of generic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<object>? GetGeneralInformation(TreeViewItem generalInformationTreeViewItem)
        {
            List<object>? dataObjects = new List<object>();
            List<ReportVolunteerGeneralInformationModel> generalInformations = new List<ReportVolunteerGeneralInformationModel>();
            if (generalInformationTreeViewItem != null && chkSelectAllVolunteers.IsChecked != null)
            {
                //see if we are doing all volunteers or just one
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                if (blnSelectAllVolunteers)
                {
                    generalInformations = _reportProvider.GetGeneralInfoAllVolunteer(chkActive.IsChecked ?? false, chkInactive.IsChecked ?? false, chkCurrent.IsChecked ?? false, chkFormer.IsChecked ?? false);
                    if (errorFlag) { errorFlag = false; return null; }
                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? intSelectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (intSelectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? selectedVolunteerTuid = intSelectedVolunteer.Tuid;
                        if (selectedVolunteerTuid != null)
                        {
                            generalInformations = _reportProvider.GetGeneralInfoForVolunteer((int)selectedVolunteerTuid);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
                if (generalInformations != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            generalInformations = generalInformations.OrderBy(x => x.LastName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            generalInformations = generalInformations.OrderByDescending(x => x.StartDate).ToList();
                        }
                    }
                    //loop through each item returned from the database
                    foreach (ReportVolunteerGeneralInformationModel generalInfo in generalInformations)
                    {
                        dynamic dataObject = new ExpandoObject(); //the dynamic object we will add properties to
                        /* loop through each item that is in the report for "General Information" and add the appropriate property to our dynamic object */
                        foreach (TreeViewItem columnName in generalInformationTreeViewItem.Items)
                        {
                            switch (columnName.Header)
                            {
                                case "First Name":
                                    dataObject.FirstName = generalInfo.FirstName;
                                    break;
                                case "Last Name":
                                    dataObject.LastName = generalInfo.LastName;
                                    break;
                                case "Status":
                                    if (generalInfo.IsActive)
                                    {
                                        dataObject.Status = "Active";
                                    }
                                    else
                                    {
                                        //get inactive status
                                        string? strInActiveStatus = _reportProvider.GetReasonSeparated(generalInfo.Tuid);
                                        if (errorFlag) { errorFlag = false; return null; }
                                        if (!string.IsNullOrEmpty(strInActiveStatus))
                                        {
                                            dataObject.Status = strInActiveStatus;
                                        }
                                        else
                                        {
                                            //if we could not resolve a status we will put inactive.
                                            dataObject.Status = "Inactive";
                                        }
                                    }

                                    break;
                                case "Phone":
                                    dataObject.Phone = generalInfo.Phone;
                                    break;
                                case "Address":
                                    dataObject.Address = generalInfo.Address;
                                    break;
                                case "Email":
                                    dataObject.Email = generalInfo.Email;
                                    break;
                                case "Alternate Phone":
                                    dataObject.AltPhone = generalInfo.AltPhone;
                                    break;
                                case "Start Date":
                                    dataObject.StartDate = generalInfo.StartDate.ToString("d");
                                    break;
                                case "Separation Date":
                                    DateTime? endDate = generalInfo.EndDate;
                                    if (endDate != null && endDate != new DateTime()) //this will check that enddate is not the default datetime value
                                    {
                                        dataObject.EndDate = ((DateTime)endDate).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.EndDate = "null";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        dataObjects.Add(dataObject);
                    }
                }
            }
            return dataObjects;

        }

        #endregion

        #region annualChecks
        /// <summary>
        /// This method will get the annual checks from the database for either a single volunteer or all volunteers,
        /// and then it will add properties denoted by the items of the annualCheckTreeViewItems items header's since
        /// these are what are selected in the report builder, and returns a list of dynamic objects
        /// </summary>
        /// <param name="annualCheckTreeViewItem">the TreeViewItem for the annualCheck structure that is in the report</param>
        /// <returns>a list of generic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<object>? GetAnnualChecks(TreeViewItem annualCheckTreeViewItem)
        {
            List<object>? annualChecks = new List<object>(); //the list we will add dynamic objects to
            List<ReportAnnualCheckModel>? databaseAnnualChecks = new List<ReportAnnualCheckModel>();
            if (annualCheckTreeViewItem != null && chkSelectAllVolunteers.IsChecked != null)
            {
                bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                if (blnSelectAllVolunteers)
                {
                    //get annual checks for all volunteers
                    databaseAnnualChecks = _reportProvider.GetAnnualChecksAllVolunteers(chkActive.IsChecked ?? false, chkInactive.IsChecked ?? false, chkCurrent.IsChecked ?? false, chkFormer.IsChecked ?? false);
                    if (errorFlag) { errorFlag = false; return null; }
                }
                else
                {
                    //just one volunteer, make sure the selected volunteer is not a null item
                    VolunteerNameIdModel? selectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                    if (selectedVolunteer != null)
                    {
                        //resolve the tuid for the selected volunteer
                        int? intSelectedVolunteerTuid = selectedVolunteer.Tuid;
                        if (intSelectedVolunteerTuid != null)
                        {
                            databaseAnnualChecks = _reportProvider.GetAnnualCheckForVolunteer((int)intSelectedVolunteerTuid);
                            if (errorFlag) { errorFlag = false; return null; }
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
                if (databaseAnnualChecks != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            databaseAnnualChecks = databaseAnnualChecks.OrderBy(x => x.VolunteerFullName).ToList();
                        }
                    }
                    //loop through all of the items returned from the database
                    foreach (ReportAnnualCheckModel check in databaseAnnualChecks)
                    {
                        dynamic dataObject = new ExpandoObject(); //the object that we are going to dynamically add properties to
                        /* loop through each item of the "Annual Checks" item to see which items are in the report and add them as properties
                         * and use the current annualCheck model to get the data */
                        foreach (TreeViewItem dataColumn in annualCheckTreeViewItem.Items)
                        {
                            switch (dataColumn.Header)
                            {
                                case "Schedule and Photo Release":
                                    if (check.SchedulePhotoRelease != null)
                                    {
                                        dataObject.SchedulePhotoRelease = ((DateTime)check.SchedulePhotoRelease).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.SchedulePhotoRelease = "null";
                                    }

                                    break;
                                case "Emergency Beneficiary Form":
                                    if (check.EmergancyBeneficiaryForm != null)
                                    {
                                        dataObject.EmergencyForm = ((DateTime)check.EmergancyBeneficiaryForm).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.EmergencyForm = "null";
                                    }

                                    break;
                                case "Hippa Release":
                                    if (check.HippaRelease != null)
                                    {
                                        dataObject.HippaRelease = ((DateTime)check.HippaRelease).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.HippRelease = "null";
                                    }

                                    break;
                                case "Physical":
                                    if (check.Physical != null)
                                    {
                                        dataObject.Physical = ((DateTime)check.Physical).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.Physical = "null";
                                    }

                                    break;
                                case "Annual Income Car Insurance":
                                    if (check.AnnualIncomeCarInsurance != null)
                                    {
                                        dataObject.AnnualIncome = ((DateTime)check.AnnualIncomeCarInsurance).ToString("d");
                                    }
                                    else
                                    {
                                        dataObject.AnnualIncome = "null";
                                    }

                                    break;
                                case "Volunteer Name":
                                    dataObject.VolunteerName = check.VolunteerFullName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        annualChecks.Add(dataObject);
                    }
                    return annualChecks;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        #endregion

        #region in-kind
        /// <summary>
        /// This method is used to get all the in kind expenses that fall in the time range of the start and end date datepickers for
        /// the in-kind expense type that inKindExpenseItem represents in its header
        /// </summary>
        /// <param name="inKindExpenseItem">the inKindExpenseItem that we want to get items from the database for.</param>
        /// <returns>a List of generic objects that have properties corresponding to the in kind expense item</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/24/2023</created>
        public List<object>? GetInKindExpenses(TreeViewItem inKindExpenseItem)
        {
            List<object> expenses = new List<object>();
            List<ExpenseEntryModel>? databaseExpenses = null;
            if (inKindExpenseItem != null)
            {
                string? strExpenseTypeName = inKindExpenseItem.Header.ToString();
                DateTime? startDate = dtpStartDate.SelectedDate;
                DateTime? endDate = dtpEndDate.SelectedDate;
                if (strExpenseTypeName != null && startDate != null && endDate != null && chkSelectAllVolunteers.IsChecked != null)
                {
                    bool blnSelectAllVolunteers = (bool)chkSelectAllVolunteers.IsChecked;
                    if (blnSelectAllVolunteers)
                    {
                        //get annual checks for all volunteers
                        databaseExpenses = _reportProvider.GetExpensesForReport(startDate, endDate, strExpenseTypeName, chkActive.IsChecked ?? false,
                            chkInactive.IsChecked ?? false, chkCurrent.IsChecked ?? false, chkFormer.IsChecked ?? false);
                        if (errorFlag) { errorFlag = false; return null; }
                    }
                    else
                    {
                        //just one volunteer, make sure the selected volunteer is not a null item
                        VolunteerNameIdModel? selectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                        if (selectedVolunteer != null)
                        {
                            //resolve the tuid for the selected volunteer
                            int? intSelectedVolunteerTuid = selectedVolunteer.Tuid;
                            if (intSelectedVolunteerTuid != null)
                            {
                                databaseExpenses = _reportProvider.GetExpensesForReportForVolunteer(startDate, endDate, strExpenseTypeName, (int)intSelectedVolunteerTuid);
                                if (errorFlag) { errorFlag = false; return null; }
                            }
                            else
                            {
                                return null;
                            }

                        }
                    }
                }

                //since collections cannot be explicitly cast into object type we have to add each entry from the database list to the list we want to return typed as object
                if (databaseExpenses != null)
                {
                    //check if we need to sort this
                    string? sortBy = cmbSortBy.SelectedItem as string;
                    if (sortBy != null)
                    {
                        if (sortBy.Equals("Volunteer Name"))
                        {
                            databaseExpenses = databaseExpenses.OrderBy(x => x.Volunteer.FullName).ToList();
                        }
                        if (sortBy.Equals("Date"))
                        {
                            databaseExpenses = databaseExpenses.OrderByDescending(x => x.DateOf).ToList();
                        }
                    }
                    foreach (ExpenseEntryModel expense in databaseExpenses)
                    {

                        if (expense != null)
                        {
                            dynamic dataObject = new ExpandoObject();
                            //loop through each item in the ExpenseType treeviewitem to get the selected columns
                            foreach (TreeViewItem columnName in inKindExpenseItem.Items)
                            {
                                string? strColumnName = columnName.Header.ToString();
                                if (!string.IsNullOrEmpty(strColumnName))
                                {
                                    switch (strColumnName)
                                    {
                                        case "Value":
                                            dataObject.Value = expense.Value;
                                            break;
                                        case "Volunteer Name":
                                            dataObject.VolunteerName = expense.Volunteer?.FullName ?? "null";
                                            break;
                                        case "Date":
                                            dataObject.Date = expense.DateOf.ToString("d");
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            expenses.Add(dataObject);
                        }

                    }
                }
                return expenses;
            }
            else
            {
                return null;
            }
        }

        public List<object>? GetDonations(TreeViewItem itemToMatch)
        {
            List<object> dataObjects = new List<object>();
            List<DonationReportModel> donations = new List<DonationReportModel>();
            DateTime? startDate = dtpStartDate.SelectedDate;
            DateTime? endDate = dtpEndDate.SelectedDate;
            if (startDate != null && endDate != null)
            {
                donations = _reportProvider.GetDonations((DateTime)startDate, (DateTime)endDate);
                if (errorFlag) { errorFlag = false; return null; }
            }
            if (donations != null)
            {
                foreach (DonationReportModel donation in donations)
                {
                    dynamic dataObject = new ExpandoObject();
                    foreach (TreeViewItem columnName in itemToMatch.Items)
                    {
                        switch (columnName.Header)
                        {
                            case "Name":
                                dataObject.Name = donation.Name;
                                break;
                            case "Type":
                                dataObject.Type = donation.Type;
                                break;
                            case "Value":
                                dataObject.Value = donation.Value;
                                break;
                            case "Date":
                                dataObject.Date = donation.Date.ToString("d");
                                break;
                            default:
                                break;
                        }
                    }
                    dataObjects.Add(dataObject);
                }
            }
            return dataObjects;

        }
        /// <summary>
        /// This method will check the treeviewItem header of a category that was added into the structure from the database and then return a List of 
        /// appropriate objects
        /// </summary>
        /// <param name="itemToMatch">the item we are trying to find</param>
        /// <returns>a list of dynamic objects</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/25/2023</created>
        public List<object>? SearchConfigurables(TreeViewItem itemToMatch)
        {
            List<ExpenseTypeModel> expenseTypes = _expenseProvider.GetAllExpenseTypes();
            if (errorFlag) { errorFlag = false; return null; }
            if (expenseTypes != null)
            {
                foreach (ExpenseTypeModel type in expenseTypes)
                {
                    if (!string.IsNullOrEmpty(type.Name))
                    {
                        if (type.Name.Equals(itemToMatch.Header))
                        {
                            return GetInKindExpenses(itemToMatch);
                        }
                    }
                }
            }
            //if we make it here check if we are doing donations
            if (itemToMatch.Header.Equals("Donations"))
            {
                return GetDonations(itemToMatch);
            }
            return null;
        }
        #endregion

        #endregion

        #region Report Preset Methods
        /// <summary>
        /// Populates cmbSelectPreset with existing presets
        /// </summary>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        public void PopulatePresets()
        {
            var reportPresets = _reportPresetProvider.GetAllReportPresets();
            if (errorFlag) { errorFlag = false; throw new RefreshDataCustomException(); }
            cmbSelectPreset.ItemsSource = reportPresets;
        }

        /// <summary>
        /// Updates an existing preset
        /// </summary>
        /// <param name="intPresetTuid"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        private void UpdatePreset(int intPresetTuid)
        {
            List<TreeNode> lstNewPreset = BuildTreeNodes(TreeViewReport.Items, "");

            ReportPresetModel reportPresetModel = new ReportPresetModel()
            {
                Tuid = intPresetTuid,
                Preset = lstNewPreset,
                SortBy = cmbSortBy.SelectedValue.ToString(),
                Active = chkActive.IsChecked,
                Inactive = chkInactive.IsChecked,
                Current = chkCurrent.IsChecked,
                Former = chkFormer.IsChecked
            };

            _reportPresetProvider.UpdateReportPreset(reportPresetModel);
        }

        /// <summary>
        /// Creates a new preset and saves it to the database
        /// </summary>
        /// <param name="strPresetName"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        private void CreatePreset(string strPresetName)
        {
            List<TreeNode> lstNewPreset = BuildTreeNodes(TreeViewReport.Items, "");

            ReportPresetModel presetModel = new ReportPresetModel()
            {
                Name = strPresetName,
                Preset = lstNewPreset,
                SortBy = cmbSortBy.SelectedValue.ToString(),
                Active = chkActive.IsChecked,
                Inactive = chkInactive.IsChecked,
                Former = chkFormer.IsChecked,
                Current = chkCurrent.IsChecked
            };

            _reportPresetProvider.CreateReportPreset(presetModel);
        }

        /// <summary>
        /// Loads a preset from the database and updates the TreeViewReport control
        /// </summary>
        /// <param name="intPresetTuid"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        private void LoadPreset(int intPresetTuid)
        {
            //TreeViewReport.Items.Clear();
            report.Clear();

            ReportPresetModel? selectedReportPresetModel = _reportPresetProvider.GetReportPreset(intPresetTuid);

            if(selectedReportPresetModel != null)
            {
                //Sets controls to their presets
                chkActive.IsChecked = selectedReportPresetModel.Active;
                chkCurrent.IsChecked = selectedReportPresetModel.Current;
                chkFormer.IsChecked = selectedReportPresetModel.Former;
                chkInactive.IsChecked = selectedReportPresetModel.Inactive;
                cmbSortBy.SelectedValue = selectedReportPresetModel.SortBy;
                if (selectedReportPresetModel.Preset != null)
                {
                    report = BuildTreeViewItem(selectedReportPresetModel.Preset, "");
                }
                ObservableCollection<TreeViewItem> newList = new ObservableCollection<TreeViewItem>();
                foreach (var item in report)
                {
                    newList.Add(CopyTreeViewItemWithParents(item));
                }
                report.Clear();
                report = newList;
                TreeViewReport.ItemsSource = report;
            }
        }

        /// <summary>
        /// Loads the defualt preset
        /// </summary>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/22/23</created>
        private void DefaultPreset()
        {
            chkActive.IsChecked = false;
            chkFormer.IsChecked = false;
            chkInactive.IsChecked = false;
            chkCurrent.IsChecked = false;
            cmbSortBy.SelectedValue = null;
            report.Clear();
            TreeViewReport.ItemsSource = report;
            chkSelectAllVolunteers.IsChecked = true;
        }

        /// <summary>
        /// Builds a list of TreeViewItems from a list of TreeNodes
        /// </summary>
        /// <param name="treeNodes"></param>
        /// <param name="ParentHeader"></param>
        /// <returns></returns>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        private ObservableCollection<TreeViewItem> BuildTreeViewItem(List<TreeNode> treeNodes, string ParentHeader)
        {
            ObservableCollection<TreeViewItem> treeViewItems = new ObservableCollection<TreeViewItem>();

            foreach (TreeNode node in treeNodes)
            {
                if (node.ParentHeader == ParentHeader)
                {
                    treeViewItems.Add(new TreeViewItem() { Header = node.Header, ItemsSource = BuildTreeViewItem(treeNodes, node.Header) });
                }
            }

            return treeViewItems;
        }

        /// <summary>
        /// Builds a list of TreeNodes from a collection of TreeViewItems
        /// </summary>
        /// <param name="treeViewItems"></param>
        /// <param name="parentHeader"></param>
        /// <returns></returns>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        private List<TreeNode> BuildTreeNodes(ItemCollection treeViewItems, string parentHeader)
        {
            List<TreeNode> treeNodes = new List<TreeNode>();

            foreach (TreeViewItem item in treeViewItems)
            {
                if(item.Header != null)
                {
                    treeNodes.Add(new TreeNode() { Header = item.Header.ToString() ?? "null", ParentHeader = parentHeader });

                    if (item.Items.Count > 0)
                    {
                        treeNodes.AddRange(BuildTreeNodes(item.Items, item.Header.ToString() ?? "null"));
                    }
                }
            }

            return treeNodes;
        }
        #endregion

        #region Control Events
        #region ComboBox Events
        /// <summary>
        /// Is called when the selected item in cmbSelectPreset is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/12/23</created>
        private void cmbSelectPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = cmbSelectPreset.SelectedItem;
            if (selectedItem != null)
            {
                Int32.TryParse(cmbSelectPreset.SelectedValue.ToString(), out int tuid);
                LoadPreset(tuid);
            }
            else
            {
                DefaultPreset();
            }
        }
        #endregion

        #region Button Events
        /// <summary>
        /// Is called when button add is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/21/23</created>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (report.Count == 0)
            {
                ShowGrowlWarning("Cannot add preset without a structure");
                return;
            }
            string? strExistingName = _reportPresetProvider.MatchPresetOnPreset(BuildTreeNodes(TreeViewReport.Items, ""));
            if (errorFlag) { errorFlag = false; return; }
            if (!string.IsNullOrEmpty(strExistingName))
            {
                ShowGrowlWarning("Preset with same structure already exists in database, its name is " + strExistingName);
            }
            else
            {
                AddEditReportPreset dialogWindow = new AddEditReportPreset(_serviceProvider, BuildTreeNodes(TreeViewReport.Items, ""));
                dialogWindow.ShowDialog();
                if(dialogWindow.DialogResult == true)
                {
                    ShowGrowlInfo("Added new preset");
                    try
                    {
                        PopulatePresets();
                    }
                    catch (RefreshDataCustomException ex)
                    {

                    }
                }
                else
                {
                    ShowGrowlInfo("Preset not added");
                }
                //reset the growl parent
                Growl.GrowlPanel = null;    // Reset the Growl Panel to Null. This Locks it onto the current Window

            }

        }

        /// <summary>
        /// Is called when button edit is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/21/23</created>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //make sure there is something in the report
            if(report.Count == 0)
            {
                ShowGrowlWarning("Cannot update preset without a structure");
                return;
            }
            if (cmbSelectPreset.SelectedIndex >= 0 && int.TryParse(cmbSelectPreset.SelectedValue.ToString(), out int presetTuid))
            {
                AddEditReportPreset dialogWindow = new AddEditReportPreset(_serviceProvider, BuildTreeNodes(TreeViewReport.Items, ""), presetTuid);
                dialogWindow.ShowDialog();
                if (dialogWindow.DialogResult == true)
                {
                    ShowGrowlInfo("Successfully updated preset");
                    try
                    {
                        PopulatePresets();
                    }
                    catch(RefreshDataCustomException ex)
                    {

                    }
                    
                }
                else
                {
                    ShowGrowlInfo("Preset not updated");
                }
                Growl.GrowlPanel = null;    // Reset the Growl Panel to Null. This Locks it onto the current Window
            }
            else
            {
                ShowGrowlWarning("Please Select a preset from the presets dropdown!");
            }
        }

        /// <summary>
        /// Is called when button delete is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/21/23</created>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedPreset = cmbSelectPreset.SelectedValue;
            if (selectedPreset != null)
            {
                Growl.Ask("Are you sure you would like to delete this preset?", confirmed =>
                {
                    if (confirmed)
                    {
                        _reportPresetProvider.DeleteReportPreset(int.Parse(selectedPreset.ToString() ?? "-1"));
                        if (errorFlag) { errorFlag = false; return true; }
                        cmbSelectPreset.SelectedIndex = -1;
                        try
                        {
                            PopulatePresets();
                        }
                        catch (RefreshDataCustomException ex)
                        {

                        }
                        Growl.Info(new GrowlInfo
                        {
                            Message = "Preset has been deleted",
                            StaysOpen = false,
                            ShowDateTime = false,
                            WaitTime = 2,
                        });
                    }
                    return true;
                });

            }
            else
            {
                ShowGrowlWarning("Please Select a preset from the presets dropdown!");
            }
        }

        /// <summary>
        /// This method will remove the selected treeview item from the report treeview control.
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the event arguments</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/5/23</created>
        private void btnRemoveAttribute_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = TreeViewReport.SelectedItem as TreeViewItem;
            if (selectedItem != null)
            {
                if (RemoveTreeViewReportItem(selectedItem))
                {
                    TreeViewReport.ItemsSource = report;
                }
            }
            else
            {
                ShowGrowlWarning("No item selected to remove from report");
            }


        }

        /// <summary>
        /// This method will generate an excel report when the generate report button is clicked.
        /// </summary>
        /// <param name="sender">the object that fired this event</param>
        /// <param name="e">the event arguments</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/11/23</created>
        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (report.Count == 0)
            {
                ShowGrowlWarning("Please choose a preset or add items to the right side of the report builder to generate a report");
                return;
            }
            if (ValidateControlFields())
            {
                ExcelFileModel excelFile = ConvertTreeToExcelFileModel();
                if (!ExcelExporter.ExportToExcel(excelFile))
                {
                    Growl.Error(new GrowlInfo
                    {
                        Message = "Unable To Export To Excel. Try Closing other excel files first. If issue persists please contact support",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 2
                    });
                }
            }

        }

        /// <summary>
        /// This method will fire when the '>' button is clicked. It will add the selected item on the structure tree view (left side)
        /// to the report tree view (right side)
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the event arguments</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/5/23</created>
        /// <modification>
        ///     <change> author : Andrew Loesel, Date 3/15/23 
        ///          added null check of selected item to avoid crashing.
        ///     </change>
        /// </modification>
        private void btnAddAttribute_Click(object sender, RoutedEventArgs e)
        {
            AddItemToTreeViewReport();
        }
        #endregion

        #region DateTime Picker Events

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? endDate = dtpEndDate.SelectedDate;
            DateTime? startDate = dtpStartDate.SelectedDate;
            if (startDate != null && endDate != null)
            {
                //check that end date is after start date.
                if (endDate < startDate)
                {
                    ShowGrowlWarning("please select a Start date that is before the end date");
                    dtpStartDate.SelectedDate = null;
                }
            }
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? endDate = dtpEndDate.SelectedDate;
            DateTime? startDate = dtpStartDate.SelectedDate;
            if (startDate != null && endDate != null)
            {
                //check that end date is after start date.
                if (endDate < startDate)
                {
                    ShowGrowlWarning("please select an end date that is after the start date");
                    dtpEndDate.SelectedDate = null;
                }
            }
        }
        #endregion

        #region checkboxEvents
        /// <summary>
        /// This method is called when the current checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void chkCurrent_Checked(object sender, RoutedEventArgs e)
        {
            chkFormer.IsChecked = false;
        }
        /// <summary>
        /// This method is called when the former checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void chkFormer_Checked(object sender, RoutedEventArgs e)
        {
            chkCurrent.IsChecked = false;
            //you cannot be former and active
            chkActive.IsChecked = false;
        }

        /// <summary>
        /// This method is called when the active checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void chkActive_Checked(object sender, RoutedEventArgs e)
        {
            chkInactive.IsChecked = false;
            //you cannot be active and former
            chkFormer.IsChecked = false;
        }

        /// <summary>
        /// This method is called when the inactive checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void chkInactive_Checked(object sender, RoutedEventArgs e)
        {
            chkActive.IsChecked = false;
        }

        /// <summary>
        /// this method fires when the select all volunteer checkbox is checked. We will want to just clear the selected volunter
        /// in cmbSelectVolunteer if one is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAllVolunteers_Checked(object sender, RoutedEventArgs e)
        {
            var selectedVolunteer = cmbSelectVolunteer.SelectedValue;
            if (selectedVolunteer != null)
            {
                cmbSelectVolunteer.SelectedValue = null;
            }
            else
            {

            }
            chkSelectAllVolunteers.IsChecked = true;
            //re-enable the filtering checkboxes
            SetFilterCheckBoxesEnabled(true);
        }
        #endregion



        #endregion

        #region helpers
        private bool ValidateControlFields()
        {
            bool blnHasStartDate = dtpStartDate.SelectedDate != null;
            bool blnHasVolunteerSelection = (chkSelectAllVolunteers.IsChecked != null && (bool)chkSelectAllVolunteers.IsChecked) || cmbSelectVolunteer.SelectedValue != null;

            //search for any mid level items that require a volunteer name
            if (SearchReportByHeader("Volunteer Name", report.ToList()) != null ||
                SearchReportByHeader("First Name", report.ToList()) != null ||
                SearchReportByHeader("Last Name", report.ToList()) != null)
            {
                //we found something in the report that needs a name,
                if (!blnHasVolunteerSelection)
                {
                    ShowGrowlWarning("Items in the report require a volunteer name selection");
                    return false;
                }
            }
            //now check for date fields
            if (SearchReportByHeader("Date", report.ToList()) != null)
            {
                if (!blnHasStartDate)
                {
                    ShowGrowlWarning("Items in the report require a start and end date selection");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method gets the end date from dtpEndDate and appends 11 59 59 pm to the date
        /// </summary>
        /// <returns></returns>
        private DateTime? GetEndDate()
        {
            if (dtpEndDate.SelectedDate == null)
            {
                return null;
            }
            else
            {
                return dtpEndDate.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
        }


        #endregion

        /// <summary>
        /// this method fires when the volunteer combobox selection is changed. If chkSelectAllVolunteers is checked we will want to uncheck it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectVolunteer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chkSelectAllVolunteers.IsChecked == true)
            {
                chkSelectAllVolunteers.IsChecked = false;
            }
            if(cmbSelectVolunteer.SelectedItem != null)
            {
                //we want to disable the filtering checkboxes
                SetFilterCheckBoxesEnabled(false);
            }
            else
            {
                SetFilterCheckBoxesEnabled(true);
            }
        }

        /// <summary>
        /// This method will set the IsEnabled property of the four filtering checkboxes to the passed in boolean value
        /// </summary>
        /// <param name="blnEnabled"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/3/2023</created>
        private void SetFilterCheckBoxesEnabled(bool blnEnabled)
        {
            chkActive.IsEnabled = blnEnabled;
            chkInactive.IsEnabled = blnEnabled;
            chkFormer.IsEnabled = blnEnabled;
            chkCurrent.IsEnabled = blnEnabled;
        }

        /// <summary>
        /// This method shows a growl warning with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlWarning(string strMessage)
        {
            Growl.Warning(new GrowlInfo
            {
                Message = strMessage,
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 2
            });
        }

        /// <summary>
        /// This method shows a growl info with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlInfo(string strMessage)
        {
            Growl.Info(new GrowlInfo
            {
                Message = strMessage,
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 2
            });
        }

        /// <summary>
        /// This method is to be called when data outside of this page has been changed
        /// it refreshes the structure treeview (in case new in-kind types were added), the volunteer list and the presets
        /// </summary>
        private void RefreshData()
        {
            AddTreeViewItemsToTreeViewStructure();
            PopulateVolunteers();
            PopulatePresets();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(ReportsReportBuilderPage));
            btnRefresh.IsEnabled = true;
            GrowlHelpers.Success("Data Refreshed");
        }
    }
}
