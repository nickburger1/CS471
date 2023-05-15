using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Excel;
using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.AddressProviders;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.SchoolProviders;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;

/**
 ************************************************************************************************************************
 *                                      File Name : AddNewSchool.xaml.cs                                                *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson                                                     *
 *                                      Date Created : 1/24/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 2/25/23                                                         *
 *                                      Last Modified By : Kiefer Thorson                                               *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to open a window where a new school can be added to the database          *
 *                                                                                                                      *
 * Modification Log:                                                                                                    *
 * Author: Kiefer Thorson                                                                                               *
 * Date: 2/25/2023                                                                                                      *
 * Description: Connect database to add new school once save button is pressed                                          *
 ************************************************************************************************************************
 **/
namespace C_FGMS.UI
{
    /// <summary>
    /// Interaction logic for addNewSchool.xaml
    /// </summary>
    public partial class AddNewSchool : System.Windows.Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISchoolProvider _schoolProvider;
        private readonly IAddressProvider _addressProvider;
        private readonly IDialogProvider _dialogProvider;
        private bool savePressed;
        private bool errorFlag;

        #region Constructor
        public AddNewSchool(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _schoolProvider = serviceProvider.GetRequiredService<ISchoolProvider>();
            _addressProvider = serviceProvider.GetRequiredService<IAddressProvider>();
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

            _addressProvider.DatabaseError += ErrorHandler;
            _schoolProvider.DatabaseError += ErrorHandler;

            savePressed = false;
            errorFlag = false;
            setStates();
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

        #region Button Functionality
        //!!!!!!!!!!!!!!!!! THIS SECTION CONTAINS LOGIC FOR BUTTONS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

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

        /**
         ************************************************************************************************************************
         *                                      Function Name : btnSave_Click                                                   *
         ************************************************************************************************************************
         *                                      Created By : Kiefer Thorson                                                     *
         *                                      Date Created : 1/27/23                                                          *
         *                                      Additional Contributors : CS471 WI23 Development Team                           *
         *                                      Last Modified : 1/27/23                                                         *
         *                                      Last Modified By : Kiefer Thorson                                               *
         ************************************************************************************************************************
         * Function Purpose : The Purpose of this function is to save the entered information to the database. When pressed the *
         * window should close, displaying the Schools Page.                                                                    *
         ************************************************************************************************************************
         **/
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            // errorList determines if we have all the requried info to save a new school - if not send an error
            ArrayList errorList = validateInputs();
            int getStatusLayer = errorList.Count - 1;

            // proceed if everything required is filled in, if not send error
            if (errorList[getStatusLayer].Equals(true))
            {
                // verify that inputs don't already exist in database
                // check school name
                bool schoolExists = _schoolProvider.CheckSchoolNameExists(txtNewSchool.Text) ?? false;
                if (errorFlag) { errorFlag = false; return; }

                // check school num
                var phoneNum = txtPhoneNum1.Text + txtPhoneNum2.Text + txtPhoneNum3.Text;
                bool phoneNumExists = _schoolProvider.GetSchoolPhoneNum(phoneNum) ?? false;
                if (errorFlag) { errorFlag = false; return; }

                // check school address
                string createAddressString = txtAddress.Text + txtCity.Text;

                if (schoolExists.Equals(false) && phoneNumExists.Equals(false))
                {
                    SchoolModel newSchool = new SchoolModel();
                    AddressModel associatedAddress = new AddressModel();

                    // changes for School items
                    newSchool.Name = txtNewSchool.Text;
                    string number = "(" + txtPhoneNum1.Text + ") " + txtPhoneNum2.Text + "-" + txtPhoneNum3.Text;
                    newSchool.ContactNumber = number;
                    newSchool.Principal = txtPrincipal.Text;
                    newSchool.Secretary = txtSecretary.Text;
                    newSchool.Days = getDays();

                    // configure time
                    newSchool.StartTime = tpStart.ToString();
                    newSchool.EndTime = tpEnd.ToString();
                    // Set School Status to Active by default
                    newSchool.IsActive = (bool)tglStatus.IsChecked;

                    // save address
                    associatedAddress.AddressLine1 = txtAddress.Text;
                    associatedAddress.City = txtCity.Text;
                    associatedAddress.State = cbxState.SelectedItem.ToString();
                    associatedAddress.Zipcode = txtZipCode.Text;

                    // write new address to database
                    _addressProvider.AddNewAddress(associatedAddress);
                    if (errorFlag) { errorFlag = false; return; }
                    // once we have the address added to database link the Tuid to the school entry
                    newSchool.AddressTuid = associatedAddress.Tuid;
                    // add the new school to database
                    _schoolProvider.AddNewSchool(newSchool);
                    if (errorFlag) { errorFlag = false; return; }

                    // update savePressed so we don't get a closing message
                    savePressed = true;
                    DialogResult = true;
                    // close add new school window
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

        #endregion

        #region Page Settings
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

        protected override void OnClosing(CancelEventArgs e)
        {
            //default form to not close
            e.Cancel = true;

            //if no changes were made or the user accepts the dialog, sets the form to close
            if (savePressed == true)
            {
                e.Cancel = false;
            }
            else if (checkUnchanged() == true)
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

        private bool checkUnchanged()
        {
            bool unchanged = true;

            if (txtNewSchool.Text.Length > 0)
            {
                unchanged = false;
            }
            else if (txtAddress.Text.Length > 0)
            {
                unchanged = false;
            }
            // City
            else if (txtCity.Text.Length > 0)
            {
                unchanged = false;
            }
            // State is skipped because we automatically populate textbox with michigan
            // Zip Code
            else if (txtZipCode.Text.Length > 0)
            {
                unchanged = false;
            }
            // Phone Num
            else if (txtPhoneNum1.Text.Length > 0)
            {
                unchanged = false;
            }
            else if (txtPhoneNum2.Text.Length > 0)
            {
                unchanged = false;
            }
            else if (txtPhoneNum3.Text.Length > 0)
            {
                unchanged = false;
            }
            // Principal
            else if (txtPrincipal.Text.Length > 0)
            {
                unchanged = false;
            }
            // Secretary
            else if (txtSecretary.Text.Length > 0)
            {
                unchanged = false;
            }
            // Hous Start
            else if (tpStart.Text.Length > 0)
            {
                unchanged = false;
            }
            // Hours End
            else if (tpEnd.Text.Length > 0)
            {
                unchanged = false;
            }
            // Date Picker
            else if (chkMonday.IsChecked == true || chkTuesday.IsChecked == true || chkWednesday.IsChecked == true || chkThursday.IsChecked == true || chkFriday.IsChecked == true)
            {
                unchanged = false;
            }
            return unchanged;
        }

        private string getDays()
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
            return SchoolDays;
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
                Title = txtNewSchool.Text + retStatus,
                Headers = new List<string> { txtNewSchool.Text },
                Rows = SendInfo,
            };
            var excelSheetModel = new ExcelSheetModel()
            {
                Title = txtNewSchool.Text,
                Tables = new List<ExcelTableModel> { summaryTable }
            };

            var excelFileModel = new ExcelFileModel()
            {
                FileName = txtNewSchool.Text,
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
            var start = "Start: " + tpStart.Text;
            var end = "End: " + tpEnd.Text;
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
        private ArrayList validateInputs()
        {

            // first value of errorList will be a boolean, any values after are strings of what form is missing
            ArrayList errorList = new ArrayList();
            bool isGood = true;


            // Verify School Contact Information Populated
            if (txtNewSchool.Text.Length <= 0)
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
            if (txtZipCode.Text.Length <= 0 || txtZipCode.Text.Length != 5)
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
            if (tpStart.Text.Length <= 0)
            {
                errorList.Add("Start Time");
                isGood = false;
            }
            if (tpEnd.Text.Length <= 0)
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
            if (tpStart.SelectedTime >= tpEnd.SelectedTime)
            {
                errorList.Add("Start Time Can Not Exceed End Time");
            }
            
            errorList.Add(isGood);

            return errorList;
        }

        private void validateName(object sender, RoutedEventArgs e)
        {
            if (txtNewSchool.Text.Length > 0)
            {
                string error = textOnly(txtNewSchool.Text);
                txtNewSchool.Text = error;
                txtNewSchool.Select(txtNewSchool.Text.Length, 0);
            }
        }
        private void validateCity(object sender, RoutedEventArgs e)
        {
            if (txtCity.Text.Length > 0)
            {
                string error = textOnly(txtCity.Text);
                txtCity.Text = error;
                txtCity.Select(txtCity.Text.Length, 0);
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
            }
        }
        private void validateSecretary(object sender, RoutedEventArgs e)
        {
            if (txtSecretary.Text.Length > 0)
            {
                string error = textOnly(txtSecretary.Text);
                txtSecretary.Text = error;
                txtSecretary.Select(txtSecretary.Text.Length, 0);
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
