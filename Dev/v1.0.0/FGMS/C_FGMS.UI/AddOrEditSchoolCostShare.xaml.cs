using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using HandyControl.Controls;
using HandyControl.Data;
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
    /// <summary>
    /// This object contains the logic for adding and editing a school cost share item
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>3/20/2023</created>
    public partial class AddOrEditCostShare : System.Windows.Window
    {
        private readonly IExpenseProvider _expenseProvider;
        private readonly IDialogProvider _dialogProvider;
        private readonly SchoolCostShareModel? _originalItem;
        bool addFlag = false;
        private bool errorFlag;

        /// <summary>
        /// This is the constructor that will be called when we are adding a new School cost share entry
        /// </summary>
        /// <param name="serviceProvider">the object that provides our dependency injection services</param>
        /// <param name="expenseProvider">our expense provider for interacting with expense database entries</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/21/2023</created>
        public AddOrEditCostShare(IServiceProvider serviceProvider, IExpenseProvider expenseProvider)
        {
            _expenseProvider = expenseProvider;
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();

            _expenseProvider.DatabaseError += ErrorHandler;
            errorFlag = false;

            InitializeComponent();
            addFlag = true; //we set this since we are adding an item
            btnDeleteCostShare.IsEnabled = false; //disable the button since we are adding

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
        /// This Constructor will be called when we are opening this window to edit an existing school cost share.
        /// </summary>
        /// <param name="serviceProvider">our dependency injection service provider</param>
        /// <param name="expenseProvider">an instance of the expense provider</param>
        /// <param name="origDate">the original date for this cost share</param>
        /// <param name="origName">the original name of the cost share</param>
        /// <param name="origValue">the original value of the cost share</param>
        /// <param name="origTUID">the tuid of the cost share we are editing</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/21/2023</created>
        public AddOrEditCostShare(IServiceProvider serviceProvider, IExpenseProvider expenseProvider, SchoolCostShareModel originalItem)
        {
            _expenseProvider = expenseProvider;
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();
            _expenseProvider.DatabaseError += ErrorHandler;
            errorFlag = false;
            InitializeComponent();
            _originalItem = originalItem;
            
        }

        #region inputHandlersAndValidation
        /// <summary>
        /// Close if user confirms with yes. (this method is the same as in Tyler's AddActivityLog.xaml.cs page)
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/17/2023</created>
        private void ConfirmClose()
        {
            bool? closeConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to exit? Changes won't be saved.", "Confirmation");

            if (closeConfirmed == true)
            {
                DialogResult = false;
                this.Close();
            }
        }

        /// <summary>
        /// This method will check if any of the controls have been changed from their default values
        /// </summary>
        /// <returns>boolean true -> yes a control was chagned, false -> no control changed</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/20/2023</created>
        private bool ControlsChanged()
        {
            //check if we are adding a new item or editing existing item
            if (addFlag)
            {
                //we need to check that all text areas are empty still
                if(string.IsNullOrEmpty(txtDate.Text) && string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtValue.Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                //edit state, we have the original values so we will check against those
                //first try to parse the value from txtValue
                double dblCurrentValue;
                if (!double.TryParse(txtValue.Text, out dblCurrentValue))
                {
                    //we couldn't parse a double from txtValue, so we should confirm close to be safe
                    return true;
                }
                if(_originalItem != null && (txtDate.SelectedDate != _originalItem.Date || !txtName.Text.Equals(_originalItem.Name) || dblCurrentValue != _originalItem.Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            
        }

        /// <summary>
        /// This method will validate the inputs on our form. for our date control we make sure all three have a value, and that 
        /// txtValue is parsable into a double data type. When we find an invalid input we growl that to notify the user.
        /// </summary>
        /// <returns>true -> valid inputs, false -> invalid input</returns>
        private bool ValidateInputs()
        {
            bool valid = true; //our flag for valid inputs

            double dblValue;
            //check valid date
            if (txtDate.SelectedDate == null)
            {
                ShowGrowlWarning("Please Select a date of billing for this cost share.");
                valid = false;
            }
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text.Length > 45)
            {
                ShowGrowlWarning("Please enter a Name for this cost share, no longer than 45 characters, ex. '1st Billing'.");
                valid = false;
            }
            if (string.IsNullOrEmpty(txtValue.Text))
            {
                ShowGrowlWarning("Please enter a value for this cost share, ex. '5000'.");
                valid = false;
            }
            else if (!double.TryParse(txtValue.Text, out dblValue))
            {
                ShowGrowlWarning("Value must be a decimal number or integer. Do not put '$' in the text field.");
                valid = false;
            }

            return valid;
        }

        #endregion

        /// <summary>
        /// this method shows a growl warning with the provided message
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

        #region EventHandlers     
        /// <summary>
        /// This method will add the new cost share to the database. We first validate the inputs, and if they are valid we
        /// then call the expenseProvider's add cost share method and close the form. If we are unable to parse txtValue into a double
        /// we will growl an error stating that the cost share was not added.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs() && txtDate.SelectedDate != null)
            {
                //check if we are adding
                if(addFlag)
                {
                    DateTime date = (DateTime)txtDate.SelectedDate;
                    string strName = txtName.Text;
                    double dblValue;
                    if (double.TryParse(txtValue.Text, out dblValue))
                    {
                        if(_expenseProvider.AddCostShare(date, strName, dblValue))
                        {
                            if (errorFlag) { errorFlag = false; return; }
                            DialogResult = true;
                            this.Close();
                        }
                        else
                        {
                            if (errorFlag) { errorFlag = false; return; }
                            ShowGrowlWarning("Failed to add the cost share");
                        }
                    }
                    else
                    {
                        Growl.Error("Could not add the cost share");
                    }
                }
                else
                {
                    //we are editing, add a check for the original item so that we do not throw a warning
                    if(txtDate.SelectedDate != null && _originalItem != null)
                    {
                        DateTime date = (DateTime)txtDate.SelectedDate;
                        string strName = txtName.Text;
                        double dblValue;
                        if (double.TryParse(txtValue.Text, out dblValue))
                        {
                            if(_expenseProvider.UpdateCostShare(date, strName, dblValue, _originalItem.Tuid))
                            {
                                if (errorFlag) { errorFlag = false; return; }
                                DialogResult = true;
                                this.Close();
                            }
                            else
                            {
                                if (errorFlag) { errorFlag = false; return; }
                                ShowGrowlWarning("Failed to update the cost share");
                            }
                        }
                        else
                        {

                            ShowGrowlWarning("Please check the value field");
                        }
                    }
                    else
                    {
                        ShowGrowlWarning("Please ensure a date is selected");
                    }
                    
                }
            }
        }

        /// <summary>
        /// This method will check if the control values have been changed, if so we confirm before closing the dialog. If not then we close this dialog right away
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the event arguments</param>
        /// <author>Andrew Loesel</author>
        /// <created>3/20/2023</created>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            if (ControlsChanged())
            {
                ConfirmClose();
            }
            else
            {
                DialogResult = false;
                this.Close();
            }

        }

        /// <summary>
        /// This method first confirms that the user would like to delete the selected cost share and then tries to delete it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        private void btnDeleteCostShare_Click(object sender, RoutedEventArgs e)
        {
            bool? deleteConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to permanately delete this cost share item?", "Confirmation");
            if(deleteConfirmed == true && _originalItem != null)
            {
                if (_expenseProvider.DeleteCostShare(_originalItem.Tuid))
                {
                    if (errorFlag) { errorFlag = false; return; }
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    if (errorFlag) { errorFlag = false; return; }
                    ShowGrowlWarning("Failed to delete the selected cost share");
                }
            }
        }

        #endregion

        
    }
}
