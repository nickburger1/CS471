using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace C_FGMS.UI
{
    /// <summary>
    /// This page outlines logic for adding or deleting an expense entry
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>3/22/2023</created>
    public partial class AddOrEditExpense : System.Windows.Window
    {

        private readonly IDialogProvider _dialogProvider;
        private readonly IExpenseProvider _expenseProvider;
        private readonly InKindExpenseModel? _originalEntry;
        bool addState = false;
        private bool errorFlag;

        /// <summary>
        /// This is the constructor to be used when adding an expense
        /// </summary>
        /// <param name="serviceProvider">the dependency injection service provider</param>
        /// <param name="expenseProvider">the expense database provider</param>
        /// <param name="volunteerList">the list of volunteers</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/22/2023</created>
        public AddOrEditExpense(IServiceProvider serviceProvider, IExpenseProvider expenseProvider, IEnumerable<VolunteerModel> volunteerList)
        {
            InitializeComponent();
            _expenseProvider = expenseProvider;
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

            _expenseProvider.DatabaseError += ErrorHandler;

            addState = true;
            cmbVolunteerName.ItemsSource = volunteerList;
            rdbDonation.Checked += rdbDonation_Checked;
            rdbOther.Checked += rdbOther_Checked;
            rdbOther.IsChecked = true;
            btnDeleteExpense.IsEnabled = false;
            InitializeControlSources(volunteerList);
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
        /// This constructor is to be used when we are editing a current expense
        /// </summary>
        /// <param name="serviceProvider">the dependency injection service provider</param>
        /// <param name="expenseProvider">the expense database provider</param>
        /// <param name="volunteerList">the list of volunteers</param>
        /// <param name="selectedExpense">the expense that was selected on Finance General page</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/22/2023</created>
        public AddOrEditExpense(IServiceProvider serviceProvider, IExpenseProvider expenseProvider, IEnumerable<VolunteerModel> volunteerList, InKindExpenseModel selectedExpense)
        {

            InitializeComponent();
            _expenseProvider = expenseProvider;
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

            _expenseProvider.DatabaseError += ErrorHandler;


            _originalEntry = selectedExpense;
            txtDate.SelectedDate = selectedExpense.Date;
            txtValue.Text = selectedExpense.Value.ToString();
            rdbDonation.Checked += rdbDonation_Checked;
            rdbOther.Checked += rdbOther_Checked;
            if (selectedExpense.IsDonation)
            {
                rdbDonation.IsChecked = true;
                cmbDonationType.SelectedValue = selectedExpense.TypeTuid;
                txtDonorName.Text = selectedExpense.VolunteerDonorName;
            }
            else
            {
                rdbOther.IsChecked = true;
                cmbExpenseType.SelectedValue = selectedExpense.TypeTuid;
                cmbVolunteerName.SelectedValue = selectedExpense.intVolunteerTuid;
            }
            InitializeControlSources(volunteerList);
        }

        /// <summary>
        /// this method initializes the three comboboxes on this dialog page with their proper collections
        /// </summary>
        /// <param name="volunteerList"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        public void InitializeControlSources(IEnumerable<VolunteerModel> volunteerList)
        {
            var inKindExpenseTypeList = _expenseProvider.GetAllExpenseTypes();
            if (errorFlag) { errorFlag = false; return; }
            var donationTypeList = _expenseProvider.GetDonationTypes();
            if (errorFlag) { errorFlag = false; return; }
            cmbExpenseType.ItemsSource = inKindExpenseTypeList;
            cmbDonationType.ItemsSource = donationTypeList;
            cmbVolunteerName.ItemsSource = volunteerList;
        }

        #region ControlValidation
        /// <summary>
        /// This method will validate the forms data, for date we make sure it is not null. for value make sure it can be parsed into a double,
        /// for Volunteer and expenseType we make sure that an option has been chosen from the comboboxes.
        /// </summary>
        /// <returns>true -> valid data, false -> invalid data</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/22/2023</created>
        private bool ValidateData()
        {

            bool valid = true;
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                ShowGrowlWarning("Please select a date for the expense.");
                valid = false;
            }
            //try parsing the value into a double
            double dblValue;
            if (!double.TryParse(txtValue.Text, out dblValue))
            {
                ShowGrowlWarning("Value must be a decimal number, do not include $.");
                valid = false;
            }
            //now we have to see if they are trying to do a donation or an expense
            if (rdbDonation.IsChecked == true)
            {
                if (cmbDonationType.SelectedItem == null)
                {
                    ShowGrowlWarning("Must choose a donation type");
                    valid = false;
                }
                //here we just validate that the donor name is an acceptable length, empty names are handled in the add/edit functions
                if (txtDonorName.Text.Length > 45)
                {
                    ShowGrowlWarning("Donor name can be at most 45 characters long.");
                    valid = false;
                }
            }
            else
            {
                if (cmbExpenseType.SelectedValue == null)
                {
                    ShowGrowlWarning("Must choose an expense type");
                    valid = false;
                }
                if (cmbVolunteerName.SelectedValue == null)
                {
                    ShowGrowlWarning("Must choose a volunteer for this expense");
                    valid = false;
                }
            }


            return valid;
        }

        /// <summary>
        /// This method will check if the controls have been changed from their initial state, if adding we check if there is content in any of the controls,
        /// if edit we check that the current values are different than the original values
        /// </summary>
        /// <returns>true -> controls have been changed, false -> controls have not been changed</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/22/2023</created>
        private bool ControlsChanged()
        {
            if (addState)
            {
                if (rdbDonation.IsChecked == true)
                {
                    if (cmbDonationType.SelectedItem != null || txtDate.SelectedDate != null || !string.IsNullOrEmpty(txtDonorName.Text) || !string.IsNullOrEmpty(txtValue.Text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (cmbExpenseType.SelectedItem != null || cmbVolunteerName.SelectedItem != null || txtDate.SelectedDate != null || !string.IsNullOrEmpty(txtValue.Text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            else
            {
                if (_originalEntry != null)
                {
                    if (rdbDonation.IsChecked == true)
                    {
                        if ((int)cmbDonationType.SelectedValue != _originalEntry.TypeTuid || txtDate.SelectedDate != _originalEntry.Date ||
                            !txtDonorName.Text.Equals(_originalEntry.VolunteerDonorName) || !txtValue.Text.Equals(_originalEntry.Value.ToString()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if ((int)cmbExpenseType.SelectedValue != _originalEntry.TypeTuid || txtDate.SelectedDate != _originalEntry.Date ||
                            (int)cmbVolunteerName.SelectedValue != _originalEntry.intVolunteerTuid || !txtValue.Text.Equals(_originalEntry.Value.ToString()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        #endregion

        #region DonationCU
        /// <summary>
        /// This method will add a donation with the provided date and value
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="dblValue"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void AddDonation(DateTime selectedDate, double dblValue)
        {
            ExpenseTypeModel? donationType = cmbDonationType.SelectedItem as ExpenseTypeModel;
            if (donationType != null)
            {
                //add the donation
                if (string.IsNullOrEmpty(txtDonorName.Text))
                {
                    //here we just want to confirm that the user wants to continue without a donor name
                    Growl.Ask("Continue without a donor name?", continueConfirm =>
                    {
                        if (continueConfirm == true)
                        {
                            bool success = _expenseProvider.AddDonation(dblValue, selectedDate, txtDonorName.Text, donationType.Tuid);
                            if (errorFlag) { errorFlag = false; return true; }
                            if (!success)
                            {
                                ShowGrowlWarning("Something went wrong, entry not added. Please contact support if issue persists.");
                            }
                            else
                            {
                                this.DialogResult = true;
                                this.Close();
                            }
                        }
                        return true;
                    });
                }
                else
                {
                    bool success = _expenseProvider.AddDonation(dblValue, selectedDate, txtDonorName.Text, donationType.Tuid);
                    if (errorFlag) { errorFlag = false; return; }
                    if (!success)
                    {
                        ShowGrowlWarning("Something went wrong, entry not added. Please contact support if issue persists.");
                    }
                    else
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                }
            }
        }

        /// <summary>
        /// This method will update a donation with the selected date and the provided value
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="dblValue"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void UpdateDonation(DateTime selectedDate, double dblValue)
        {
            ExpenseTypeModel? donationType = cmbDonationType.SelectedItem as ExpenseTypeModel;
            if (donationType != null && _originalEntry != null)
            {
                //we need to check here if the donor name is empty or not and then just confirm that they want to update the donation with no donor name
                if (string.IsNullOrEmpty(txtDonorName.Text))
                {
                    Growl.Ask("Continue without a donor name?", continueConfirm =>
                    {
                        if (continueConfirm == true)
                        {
                            //update the donation
                            bool success = _expenseProvider.UpdateDonation(_originalEntry.dbMirrorTuid, dblValue, selectedDate, donationType.Tuid, txtDonorName.Text);
                            if (errorFlag) { errorFlag = false; return true; }
                            if (!success)
                            {
                                ShowGrowlWarning("Something went wrong, entry not updated. Please contact support if issue persists.");
                            }
                            else
                            {
                                this.DialogResult = true;
                                this.Close();
                            }
                        }
                        return true;
                    });
                }
                else
                {
                    //update the donation
                    bool success = _expenseProvider.UpdateDonation(_originalEntry.dbMirrorTuid, dblValue, selectedDate, donationType.Tuid, txtDonorName.Text);
                    if (errorFlag) { errorFlag = false; return; }
                    if (!success)
                    {
                        ShowGrowlWarning("Something went wrong, entry not updated. Please contact support if issue persists.");
                    }
                    else
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                }
            }
        }
        #endregion

        #region EventHandlers

        /// <summary>
        /// This method fires when the save button is hit, we do nothing and return if data is invalid.
        /// If the add button was clicked we will use the expense provider to add the expense, if the edit button
        /// was clicked we will use the expense provider to update the expense with the original expense's tuid.
        /// </summary>
        /// <param name="sender">the object that fired this event</param>
        /// <param name="e">the event arguments</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/22/2023</created>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                DateTime? selectedDate = txtDate.SelectedDate;
                double dblValue;
                bool validValue = double.TryParse(txtValue.Text, out dblValue);
                if (rdbDonation.IsChecked == true && selectedDate != null && validValue)
                {
                    if (addState)
                    {
                        AddDonation((DateTime)selectedDate, dblValue);
                    }
                    else
                    {
                        //we'll want to see if this was orignally an in-kind expense type or a donation type
                        if (_originalEntry != null && _originalEntry.IsDonation)
                        {
                            UpdateDonation((DateTime)selectedDate, dblValue);
                        }
                        else if (_originalEntry != null)
                        {
                            Growl.Ask("This action will delete the original expense, and add a donation with provided values. Would you like to continue?",
                                confirm =>
                                {
                                    if (confirm)
                                    {
                                        if (_expenseProvider.DeleteExpense(_originalEntry.dbMirrorTuid))
                                        {
                                            if (errorFlag) { errorFlag = false; return true; }
                                            AddDonation((DateTime)selectedDate, dblValue);
                                        }
                                        else
                                        {
                                            if (errorFlag) { errorFlag = false; return true; }
                                            ShowGrowlWarning("Failed update when deleteing prior expense.");
                                        }

                                    }
                                    return true;
                                });
                        }

                    }
                }
                else if (selectedDate != null && validValue)
                {
                    //here we are dealing with expenses
                    if (addState)
                    {
                        _expenseProvider.AddExpense((DateTime)selectedDate, dblValue, (int)cmbExpenseType.SelectedValue, (int)cmbVolunteerName.SelectedValue);
                        if (errorFlag) { errorFlag = false; return; }
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        if (_originalEntry != null && _originalEntry.IsDonation)
                        {
                            //confirm that they want to delete the original donation type item and add an expense type item
                            Growl.Ask("This action will delete the original expense, and add a donation with provided values. Would you like to continue?",
                                confirm =>
                                {
                                    if (confirm)
                                    {
                                        if (_expenseProvider.DeleteDonation(_originalEntry.dbMirrorTuid))
                                        {
                                            if (errorFlag) { errorFlag = false; return true; }
                                            if (_expenseProvider.AddExpense((DateTime)selectedDate, dblValue, (int)cmbExpenseType.SelectedValue, (int)cmbVolunteerName.SelectedValue))
                                            {
                                                if (errorFlag) { errorFlag = false; return true; }
                                                this.DialogResult = true;
                                                this.Close();
                                            }
                                            else
                                            {
                                                if (errorFlag) { errorFlag = false; return true; }
                                                ShowGrowlWarning("Failed to add the new expense.");
                                            }
                                        }
                                        else
                                        {
                                            if (errorFlag) { errorFlag = false; return true; }
                                            ShowGrowlWarning("Failed to delete prior donation entry.");
                                        }
                                    }
                                    return true;
                                });

                        }
                        else if (_originalEntry != null)
                        {
                            _expenseProvider.UpdateExpense((DateTime)selectedDate, dblValue, (int)cmbExpenseType.SelectedValue, (int)cmbVolunteerName.SelectedValue, _originalEntry.dbMirrorTuid);
                            if (errorFlag) { errorFlag = false; return; }
                            this.DialogResult = true;
                            this.Close();
                        }

                    }
                }

            }
        }

        /// <summary>
        /// This method fires when the cancel button is clicked, if there have been changes made we confirm with the user that they want to close.
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the event arguments</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (ControlsChanged())
            {
                Growl.Ask("Changes won't be saved, are you sure you want to exit?", confirm =>
                {
                    if (confirm == true)
                    {
                        this.DialogResult = false;
                        this.Close();
                    }
                    return true;
                });

            }
            else
            {
                this.DialogResult = false;
                this.Close();
            }
        }


        /// <summary>
        /// This method will handle deleting an in-kind expense. We first confirm this is what the user wants to do.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private void btnDeleteExpense_Click(object sender, RoutedEventArgs e)
        {

            Growl.Ask("Are you sure you want to permanately delete this In-Kind Expense entry?", deleteConfirmed =>
            {
                if (deleteConfirmed == true && _originalEntry != null)
                {
                    if (_originalEntry.IsDonation)
                    {
                        _expenseProvider.DeleteDonation(_originalEntry.dbMirrorTuid);
                        if (errorFlag) { errorFlag = false; return true; }
                    }
                    else
                    {
                        _expenseProvider.DeleteExpense(_originalEntry.dbMirrorTuid);
                        if (errorFlag) { errorFlag = false; return true; }
                    }

                    //close window
                    this.DialogResult = true;
                    this.Close();
                }
                return true;
            });

        }

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

        /// <summary>
        /// This method will show a growl warning with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlWarning(string strMessage)
        {
            Growl.Warning(new GrowlInfo
            {
                Message = strMessage,
                StaysOpen = false,
                ShowDateTime = false,
                WaitTime = 2

            });
        }
        /// <summary>
        /// This method will show a growl info with the provided message
        /// </summary>
        /// <param name="strMessage"></param>
        private void ShowGrowlInfo(string strMessage)
        {
            Growl.Info(new GrowlInfo
            {
                Message = strMessage,
                StaysOpen = false,
                ShowDateTime = false,
                WaitTime = 2,
            });
        }

        /// <summary>
        /// This method changes the controls showed to reflect the other in-kind expense types when rdbOther is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        private void rdbOther_Checked(object sender, RoutedEventArgs e)
        {
            //swap name areas
            txtDonorName.Visibility = Visibility.Hidden;
            cmbVolunteerName.Visibility = Visibility.Visible;
            //swap type combos
            cmbDonationType.Visibility = Visibility.Hidden;
            cmbExpenseType.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// This method changes the controls shown to reflect the donation in-kind expense types when rdbDonations is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void rdbDonation_Checked(object sender, RoutedEventArgs e)
        {
            cmbVolunteerName.Visibility = Visibility.Hidden;
            txtDonorName.Visibility = Visibility.Visible;
            //swap type combos
            cmbDonationType.Visibility = Visibility.Visible;
            cmbExpenseType.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
