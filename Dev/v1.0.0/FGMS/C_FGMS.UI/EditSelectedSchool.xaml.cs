using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.Seeders;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.AddressProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static C_FGMS.UI.EditSelectedSchool;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace C_FGMS.UI
{
    /**
     ************************************************************************************************************************
     *                                      File Name : EditSelectedSchool.xaml.cs                                          *
     *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
     ************************************************************************************************************************
     *                                      Created By : Kiefer Thorson                                                     *
     *                                      Date Created :2/26/23                                                           *
     *                                      Additional Contributors : CS471 WI23 Development Team                           *
     *                                      Last Modified :                                                                 *
     *                                      Last Modified By :                                                              *
     ************************************************************************************************************************
     * File Purpose : The Purpose of this file is to edit a given school's information in the database                      *
     ************************************************************************************************************************
     * Modification Log:                                                                                                    *
     * Author:                                                                                                              *
     * Date:                                                                                                                *
     * Description:                                                                                                         *
     ************************************************************************************************************************
     **/
    public partial class EditSelectedSchool : System.Windows.Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISchoolProvider _schoolProvider;
        private readonly IAddressProvider _addressProvider;
        private readonly SchoolModel? SelectedSchool;
        private readonly AddressModel SelectedAddress;
        private readonly bool IsActive;
        private readonly IDialogProvider _dialogProvider;
        private static readonly Regex rxNonDigits = new Regex(@"[^\d]+");
        private bool errorFlag;



        #region Constructor
        // All Good
        /// <summary>
        /// Function Name: EditSelectedSchool
        /// Created By: Kiefer Thorson                     
        /// Date Created: 2/26/2023
        /// Additional Contributors:
        /// Last Modified:         
        /// Last Modified By:                                   
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Initialize the Page
        /// <param name="serviceProvider"></param>
        /// <param name="SchoolName"></param>
        public EditSelectedSchool(IServiceProvider serviceProvider, int intSchoolTuid)
        {

            InitializeComponent();

            _serviceProvider = serviceProvider;
            _schoolProvider = serviceProvider.GetRequiredService<ISchoolProvider>();
            _addressProvider = serviceProvider.GetRequiredService<IAddressProvider>();
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

            _addressProvider.DatabaseError += ErrorHandler;
            _schoolProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            // get selected school
            SelectedSchool = _schoolProvider.GetSchoolByTuid(intSchoolTuid);
            if (errorFlag) { errorFlag = false; return; }
            // get school's address
            if (SelectedSchool == null)
            {
                DialogResult = false;
                this.Close();
            }
            else
            {
                SelectedAddress = _addressProvider.GetAddressByTuid((int?)SelectedSchool.AddressTuid);
                if (errorFlag) { errorFlag = false; return; }
                tglStatus.IsChecked = SelectedSchool.IsActive;
                setStates();
                txtSchoolName.Focus();

                populateExistingSchool();
            }



        }
        #endregion

        #region Error Handlers


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

        //!!!!!!!!!!!!!!!!! THIS SECTION CONTAINS LOGIC FOR BUTTONS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        #region Button Functionality
        //Edit Selected School works saving excel now.Need to clean up and apply to add new school and perschool
        private void btnExcelReport_Click(object sender, RoutedEventArgs e)
        {
            ArrayList errorList = validateInputs();
            int getStatusLayer = errorList.Count - 1;

            // proceed if everything required is filled in, if not send error
            if (errorList[getStatusLayer].Equals(true))
            {
                ExcelExporter.ExportToExcel(CreateNewSchoolReport());
            }
            else
            {
                // Send error: Complete form before saving
                string messageBoxText = "Please complete the following fields before exporting:\n";
                for (int i = 0; i < errorList.Count - 1; i++)
                {
                    messageBoxText = messageBoxText + "\n" + errorList[i];
                }
                Growl.Warning(messageBoxText);
            }
        }





        // Issue here with HOURS - FINISH CHANGES FOR THIS AFTER XAML CHANGES
        /// <summary>
        /// Function Name: btnSave_Click
        /// Created By: Kiefer Thorson                     
        /// Date Created: 2/26/2023
        /// Additional Contributors:
        /// Last Modified:         
        /// Last Modified By:                                   
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Save any changes made to the current school to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ArrayList errorList = validateInputs();
            int getStatusLayer = errorList.Count - 1;

            if (errorList[getStatusLayer].Equals(true))
            {
                // verify that inputs don't already exist in database
                // check school name
                bool schoolExists = _schoolProvider.CheckSchoolNameExists(txtSchoolName.Text, SelectedSchool.Name) ?? true;
                if (errorFlag) { errorFlag = false; return; }

                // check school num
                var phoneNum = txtPhoneNum1.Text + txtPhoneNum2.Text + txtPhoneNum3.Text;
                bool phoneNumExists = _schoolProvider.GetSchoolPhoneNum(phoneNum, SelectedSchool.ContactNumber) ?? true;
                if (errorFlag) { errorFlag = false; return; }

                // check school address
                string createAddressString = txtAddress.Text + txtCity.Text;
                string selectedAddress = SelectedAddress.AddressLine1 + SelectedAddress.City;

                if (schoolExists.Equals(false) && phoneNumExists.Equals(false))
                {
                    SelectedSchool.Name = txtSchoolName.Text;
                    string number = "(" + txtPhoneNum1.Text + ") " + txtPhoneNum2.Text + "-" + txtPhoneNum3.Text;
                    SelectedSchool.ContactNumber = number;
                    SelectedSchool.Principal = txtPrincipal.Text;
                    SelectedSchool.Secretary = txtSecretary.Text;
                    getDays();

                    // toggle button for adding Time editing next to status
                    // if edit time toggled grid containing time pickers appears in front of day pickers
                    //      timepicker values selected update their txt counterparts



                    SelectedSchool.StartTime = tpStart.Text;
                    SelectedSchool.EndTime = tpEnd.Text;


                    SelectedSchool.IsActive = (bool)tglStatus.IsChecked;
                    //-------------------------------------------
                    SelectedAddress.AddressLine1 = txtAddress.Text;
                    SelectedAddress.City = txtCity.Text;
                    SelectedAddress.State = cbxState.SelectedItem.ToString();
                    SelectedAddress.Zipcode = txtZipCode.Text;

                    _addressProvider.UpdateAddress(SelectedAddress);
                    if (errorFlag) { errorFlag = false; return; }
                    SelectedSchool.AddressTuid = SelectedAddress.Tuid;
                    _schoolProvider.UpdateSchool(SelectedSchool);
                    if (errorFlag) { errorFlag = false; return; }
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    // Send Error: Existing Data
                    string errorMessage = "";
                    if (schoolExists)
                    {
                        errorMessage += "School Name Already Exists in Database\n";
                    }
                    if (phoneNumExists)
                    {
                        errorMessage += "Phone Number Already Exists in Database\n";
                    }
                    Growl.Warning(errorMessage);
                }
            }
            else
            {
                // Send error: Complete form before saving
                string messageBoxText = "Please complete the following fields before saving:\n";
                for (int i = 0; i < errorList.Count - 1; i++)
                {
                    messageBoxText = messageBoxText + "\n" + errorList[i];
                }
                Growl.Warning(messageBoxText);
            }

        }

        private void btnSaveTime_Click(object sender, RoutedEventArgs e)
        {
            txtStartTime.Text = tpStart.Text;
            txtEndTime.Text = tpEnd.Text;
        }


        // ALL GOOD
        /// <summary>
        /// Function Name: btnCancel_Click
        /// Created By: Kiefer Thorson                     
        /// Date Created: 2/26/2023
        /// Additional Contributors:
        /// Last Modified: 3/3/2023        
        /// Last Modified By: Kiefer Thorson                                  
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Cancel any changes made (dont save to database and return to main page)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        #endregion

        #region Page Settings
        private void populateExistingSchool()
        {
            txtSchoolName.Text = SelectedSchool.Name;
            txtSchoolName.Foreground = System.Windows.Media.Brushes.Gray;
            txtAddress.Text = SelectedAddress.AddressLine1;
            txtAddress.Foreground = System.Windows.Media.Brushes.Gray;
            txtCity.Text = SelectedAddress.City;
            txtCity.Foreground = System.Windows.Media.Brushes.Gray;
            cbxState.SelectedItem = SelectedAddress.State;
            cbxState.SelectedItem = System.Windows.Media.Brushes.Gray;
            txtZipCode.Text = SelectedAddress.Zipcode;
            txtZipCode.Foreground = System.Windows.Media.Brushes.Gray;
            populatePhoneNum();
            txtPrincipal.Text = SelectedSchool.Principal;
            txtPrincipal.Foreground = System.Windows.Media.Brushes.Gray;
            txtSecretary.Text = SelectedSchool.Secretary;
            txtSecretary.Foreground = System.Windows.Media.Brushes.Gray;
            txtStartTime.Text = SelectedSchool.StartTime;
            txtStartTime.Foreground = System.Windows.Media.Brushes.Gray;
            txtEndTime.Text = SelectedSchool.EndTime;
            txtEndTime.Foreground = System.Windows.Media.Brushes.Gray;
            populateDays();
        }
        // ALL GOOD - Called when populating schools
        private void populatePhoneNum()
        {
            string full = SelectedSchool.ContactNumber;
            full = rxNonDigits.Replace(full, "");
            txtPhoneNum1.Text = full.Substring(0, 3);
            txtPhoneNum1.Foreground = System.Windows.Media.Brushes.Gray;
            txtPhoneNum2.Text = full.Substring(3, 3);
            txtPhoneNum2.Foreground = System.Windows.Media.Brushes.Gray;
            txtPhoneNum3.Text = full.Substring(6);
            txtPhoneNum3.Foreground = System.Windows.Media.Brushes.Gray;
        }

        
        /// <summary>
        /// this method will loop through each character in the day string and check the corresponding
        /// day checkbox
        /// </summary>
        /// <author>Andrew Loesel</author>
        /// <created>4/13/2023</created>
        private void populateDays()
        {
            string? days = SelectedSchool == null ? "" : SelectedSchool.Days;
            if (!string.IsNullOrEmpty(days))
            {
                foreach (char day in days)
                {

                    if (day == '1')
                    {
                        chkMonday.IsChecked = true;
                    }
                    else if (day == '2')
                    {
                        chkTuesday.IsChecked = true;
                    }
                    else if (day == '3')
                    {
                        chkWednesday.IsChecked = true;
                    }
                    else if (day == '4')
                    {
                        chkThursday.IsChecked = true;
                    }
                    else if (day == '5')
                    {
                        chkFriday.IsChecked = true;
                    }
                }
            }
        }
        // ALL GOOD
        private void getDays()
        {
            // configure days
            String SchoolDays = "";
            if (chkMonday.IsChecked == true)
            {
                SchoolDays = "1";
            }
            if (chkTuesday.IsChecked == true)
            {
                SchoolDays += "2";
            }
            if (chkWednesday.IsChecked == true)
            {
                SchoolDays += "3";
            }
            if (chkThursday.IsChecked == true)
            {
                SchoolDays += "4";
            }
            if (chkFriday.IsChecked == true)
            {
                SchoolDays += "5";
            }
            SelectedSchool.Days = SchoolDays;
        }

        private string retDays()
        {
            // configure days
            String SchoolDays = "";
            if (chkMonday.IsChecked == true)
            {
                SchoolDays = "M ";
            }
            if (chkTuesday.IsChecked == true)
            {
                SchoolDays += "T ";
            }
            if (chkWednesday.IsChecked == true)
            {
                SchoolDays += "W ";
            }
            if (chkThursday.IsChecked == true)
            {
                SchoolDays += "Th ";
            }
            if (chkFriday.IsChecked == true)
            {
                SchoolDays += "F ";
            }
            return SchoolDays;
        }

        // ALL GOOD
        private void setStates()
        {
            // load state list into state combobox
            List<string> stateList = new List<string>();
            stateList.Add("Al");
            stateList.Add("AK");
            stateList.Add("AZ");
            stateList.Add("AR");
            stateList.Add("CA");
            stateList.Add("CO");
            stateList.Add("CT");
            stateList.Add("DE");
            stateList.Add("DC");
            stateList.Add("FL");
            stateList.Add("GA");
            stateList.Add("HI");
            stateList.Add("ID");
            stateList.Add("IL");
            stateList.Add("IN");
            stateList.Add("IA");
            stateList.Add("KS");
            stateList.Add("KY");
            stateList.Add("LA");
            stateList.Add("ME");
            stateList.Add("MD");
            stateList.Add("MA");
            stateList.Add("MI");
            stateList.Add("MN");
            stateList.Add("MS");
            stateList.Add("MO");
            stateList.Add("MT");
            stateList.Add("NE");
            stateList.Add("NV");
            stateList.Add("NH");
            stateList.Add("NJ");
            stateList.Add("NM");
            stateList.Add("NY");
            stateList.Add("NC");
            stateList.Add("ND");
            stateList.Add("OH");
            stateList.Add("OK");
            stateList.Add("OR");
            stateList.Add("PA");
            stateList.Add("RI");
            stateList.Add("SC");
            stateList.Add("SD");
            stateList.Add("TN");
            stateList.Add("TX");
            stateList.Add("UT");
            stateList.Add("VT");
            stateList.Add("VA");
            stateList.Add("WA");
            stateList.Add("WV");
            stateList.Add("WI");
            stateList.Add("WY");
            cbxState.ItemsSource = stateList;
        }
        private void tglEditTime_Checked(object sender, RoutedEventArgs e)
        {
            if (tglEditTime.IsChecked == true)
            {
                grdDatePickers.Visibility = Visibility.Hidden;
                grdTimePickers.Visibility = Visibility.Visible;
                txtEditStartTimeLabel.Visibility = Visibility.Visible;
                txtEditEndTimeLabel.Visibility = Visibility.Visible;
                btnSaveTime.Visibility = Visibility.Visible;
            }
            else
            {
                grdDatePickers.Visibility = Visibility.Visible;
                grdTimePickers.Visibility = Visibility.Hidden;
                txtEditStartTimeLabel.Visibility = Visibility.Hidden;
                txtEditEndTimeLabel.Visibility = Visibility.Hidden;
                btnSaveTime.Visibility = Visibility.Hidden;
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            //default form to not close
            e.Cancel = true;

            //if no changes were made or the user accepts the dialog, sets the form to close
            if (checkUnchanged() == true)
            {
                e.Cancel = false;
            }
            else
            {
                bool closeConfirmed = (bool)_dialogProvider.ShowConfirmationDialog("Are you sure you want to exit? Changes won't be saved.", "Confirmation");

                if (closeConfirmed)
                {
                    e.Cancel = false;
                }

                base.OnClosing(e);
            }
        }

        public bool checkUnchanged()
        {
            bool unchanged = true;

            if (txtSchoolName.Text != SelectedSchool.Name)
            {
                unchanged = false;
            }
            // Address
            else if (txtAddress.Text != SelectedAddress.AddressLine1)
            {
                unchanged = false;
            }
            // City
            else if (txtAddress.Text != SelectedAddress.AddressLine1)
            {
                unchanged = false;
            }
            // State
            else if (cbxState.SelectedItem.ToString() != SelectedAddress.State)
            {
                unchanged = false;
            }
            // Zip Code
            else if (txtZipCode.Text != SelectedAddress.Zipcode)
            {
                unchanged = false;
            }
            // Phone Num
            else if ("(" + txtPhoneNum1.Text + ") " + txtPhoneNum2.Text + "-" + txtPhoneNum3.Text != SelectedSchool.ContactNumber)
            {
                unchanged = false;
            }
            // Principal
            else if (txtAddress.Text != SelectedAddress.AddressLine1)
            {
                unchanged = false;
            }
            // Secretary
            else if (txtAddress.Text != SelectedAddress.AddressLine1)
            {
                unchanged = false;
            }
            // Start Time
            else if (txtAddress.Text != SelectedAddress.AddressLine1)
            {
                unchanged = false;
            }
            // End Time
            else if (txtAddress.Text != SelectedAddress.AddressLine1)
            {
                unchanged = false;
            }
            // Day Changed
            else if (txtAddress.Text != SelectedAddress.AddressLine1)
            {
                unchanged = false;
            }


            return unchanged;
        }

        public ExcelFileModel CreateNewSchoolReport()
        {
            var SendInfo = buildRow();
            var status = tglStatus.IsChecked;
            string retStatus;

            if (status == true)
            {
                retStatus = " - ACTIVE";
            }
            else
            {
                retStatus = " - INACTIVE";
            }

            var summaryTable = new ExcelTableModel()
            {
                Title = txtSchoolName.Text + retStatus,
                Headers = new List<string> { txtSchoolName.Text },
                Rows = SendInfo,
            };
            var excelSheetModel = new ExcelSheetModel()
            {
                Title = txtSchoolName.Text,
                Tables = new List<ExcelTableModel> { summaryTable }
            };

            var excelFileModel = new ExcelFileModel()
            {
                FileName = txtSchoolName.Text,
                Sheets = new List<ExcelSheetModel> { excelSheetModel }
            };

            return excelFileModel;
        }

        private List<object> buildRow()
        {
            var address = txtAddress.Text;
            var city = txtCity.Text;
            var state = cbxState.SelectedItem.ToString();
            var zipcode = txtZipCode.Text;
            var phonenum = "(" + txtPhoneNum1.Text + ") " + txtPhoneNum2.Text + "-" + txtPhoneNum3.Text;
            var start = "Start: " + txtStartTime.Text;
            var end = "End: " + txtEndTime.Text;
            var days = retDays() + "\n";
            var SendInfo = new List<object>();

            SendInfo.Add(new { address });
            SendInfo.Add(new { city });
            SendInfo.Add(new { state });
            SendInfo.Add(new { zipcode });
            SendInfo.Add(new { phonenum });

            // not all schools have a principal and secretary selected
            if (txtPrincipal.Text.Length <= 0)
            {
                var error = "- No Principal on File -";
                SendInfo.Add(new { error });
            }
            else
            {
                SendInfo.Add(new { txtPrincipal.Text });
            }
            if (txtSecretary.Text.Length <= 0)
            {
                var error = "- No Secretary on File -";
                SendInfo.Add(new { error });
            }
            else
            {
                SendInfo.Add(new { txtSecretary.Text });
            }

            SendInfo.Add(new { start });
            SendInfo.Add(new { end });
            SendInfo.Add(new { days });

            return SendInfo;
        }

        #endregion

        #region Data Validation
        // Checks to see if all necessary fields are populated
        // Need to add back in time validation
        // Need to add split case once we hear decision 
        private ArrayList validateInputs()
        {

            // first value of errorList will be a boolean, any values after are strings of what form is missing
            ArrayList errorList = new ArrayList();
            bool isGood = true;


            // Verify School Contact Information Populated
            if (txtSchoolName.Text.Length <= 0)
            {
                errorList.Add("School Name");
                isGood = false;
            }
            if (txtAddress.Text.Length <= 0)
            {
                errorList.Add("Street Address");
                isGood = false;
            }
            if (txtCity.Text.Length <= 0)
            {
                errorList.Add("City");
                isGood = false;
            }
            if (cbxState.Text.Length <= 0)
            {
                errorList.Add("State");
                isGood = false;
            }
            if (txtZipCode.Text.Length <= 0)
            {
                errorList.Add("Zip Code");
                isGood = false;
            }
            if (txtPhoneNum1.Text.Length != 3 || txtPhoneNum2.Text.Length != 3 || txtPhoneNum3.Text.Length != 4)
            {
                errorList.Add("Phone Number");
                isGood = false;
            }

            // Verify School Time and Date Populated & Logical
            if (txtStartTime.Text.Length <= 0)
            {
                errorList.Add("Start Time");
                isGood = false;
            }
            if (txtEndTime.Text.Length <= 0)
            {
                errorList.Add("End Time");
                isGood = false;
            }
            if (chkMonday.IsChecked == false && chkTuesday.IsChecked == false && chkWednesday.IsChecked == false && chkThursday.IsChecked == false && chkFriday.IsChecked == false)
            {
                errorList.Add("No Days Selected");
                isGood = false;
            }

            // validate selected times
            /* if (tpStart.SelectedTime >= tpEnd.SelectedTime)
             {
                 errorList.Add("Start Time Can Not Exceed End Time");
             }*/
            /*
            // Validation for Split Times, Re-Enable once we get answer from Tara
            if (tpStartSplit.SelectedTime >= tpEndSplit.SelectedTime)
            {
                errorList.Add("Split Start Time Can Not Exceed Split End Time");
            }
            */

            errorList.Add(isGood);
            return errorList;
        }

        // The following methods have to do with input validation
        // ALL OF THESE GOOD
        private void validateName(object sender, RoutedEventArgs e)
        {
            if (txtSchoolName.Text.Length > 0)
            {
                string error = textOnly(txtSchoolName.Text);
                txtSchoolName.Text = error;
                txtSchoolName.Select(txtSchoolName.Text.Length, 0);
                txtSchoolName.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void validateAddress(object sender, RoutedEventArgs e)
        {
            // Addresses can be composed of anything, just need this to change to black once textbox edited
            txtAddress.Foreground = System.Windows.Media.Brushes.Black;
        }
        private void validateCity(object sender, RoutedEventArgs e)
        {
            if (txtCity.Text.Length > 0)
            {
                string error = textOnly(txtCity.Text);
                txtCity.Text = error;
                txtCity.Select(txtCity.Text.Length, 0);
                txtCity.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void validateZip(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text.Length > 0)
            {
                txtZipCode.MaxLength = 5;
                {
                    string error = numericOnly(txtZipCode.Text);
                    txtZipCode.Text = error;
                }
                txtZipCode.Select(txtZipCode.Text.Length, 0);
                txtZipCode.Foreground = System.Windows.Media.Brushes.Black;
            }

        }
        private void validatePhoneNum1(object sender, RoutedEventArgs e)
        {
            txtPhoneNum1.MaxLength = 3;
            if (txtPhoneNum1.Text.Length > 0)
            {

                string error = numericOnly(txtPhoneNum1.Text);
                txtPhoneNum1.Text = error;
                txtPhoneNum1.Select(txtPhoneNum1.Text.Length, 0);
                txtPhoneNum1.Foreground = System.Windows.Media.Brushes.Black;

                if (txtPhoneNum1.Text.Length == 3)
                {
                    txtPhoneNum2.Focus();
                }
            }
        }
        private void validatePhoneNum2(object sender, RoutedEventArgs e)
        {
            txtPhoneNum2.MaxLength = 3;
            if (txtPhoneNum2.Text.Length > 0)
            {
                string error = numericOnly(txtPhoneNum2.Text);
                txtPhoneNum2.Text = error;
                txtPhoneNum2.Select(txtPhoneNum2.Text.Length, 0);
                txtPhoneNum2.Foreground = System.Windows.Media.Brushes.Black;

                if (txtPhoneNum2.Text.Length == 3)
                {
                    txtPhoneNum3.Focus();
                }
            }
        }
        private void validatePhoneNum3(object sender, RoutedEventArgs e)
        {
            txtPhoneNum3.MaxLength = 4;
            if (txtPhoneNum3.Text.Length > 0)
            {

                string error = numericOnly(txtPhoneNum3.Text);
                txtPhoneNum3.Text = error;
                txtPhoneNum3.Select(txtPhoneNum3.Text.Length, 0);
                txtPhoneNum3.Foreground = System.Windows.Media.Brushes.Black;
                if (txtPhoneNum3.Text.Length == 4)
                {
                    txtPrincipal.Focus();
                }
            }
        }
        private void validatePrincipal(object sender, RoutedEventArgs e)
        {
            if (txtPrincipal.Text.Length > 0)
            {
                string error = textOnly(txtPrincipal.Text);
                txtPrincipal.Text = error;
                txtPrincipal.Select(txtPrincipal.Text.Length, 0);
                txtPrincipal.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void validateSecretary(object sender, RoutedEventArgs e)
        {
            if (txtSecretary.Text.Length > 0)
            {
                string error = textOnly(txtSecretary.Text);
                txtSecretary.Text = error;
                txtSecretary.Select(txtSecretary.Text.Length, 0);
                txtSecretary.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private string numericOnly(string input)
        {
            bool result = Regex.IsMatch(input, @"^[0-9]+$");

            if (result == false)
            {
                // Give an error message an illegal character was entered and remove that character
                Growl.Warning("Only Digits [0-9] Allowed");
                input = input.Substring(0, input.Length - 1);
            }
            return input;
        }
        private string textOnly(string input)
        {
            bool result = Regex.IsMatch(input, @"^[""a-zA-Z\s-/.]+$");

            if (result == false)
            {
                // Give an error message an illegal character was entered and remove that character
                Growl.Warning("Only Letters [A-Z], Characters [- / .] and Spaces Allowed");
                input = input.Substring(0, input.Length - 1);
            }
            return input;
        }

        #endregion

        #region growlSettings
        /// <summary>
        /// Overrites the OnActivated method to set the growl parent to the current window
        /// </summary>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        protected override void OnActivated(EventArgs e)
        {
            Growl.SetGrowlParent(stkGrowl, true);   //Sets the GrowlPanel onto this page
            base.OnActivated(e);
        }

        /// <summary>
        /// Overrites the OnDeactived method to unset the growl parent from the current window
        /// </summary>
        /// <param name="e"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        protected override void OnDeactivated(EventArgs e)
        {
            Growl.SetGrowlParent(stkGrowl, false);
            base.OnDeactivated(e);
        }
        #endregion


    }
}
