using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using B_FGMS.BuisinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using B_FGMS.BusinessLogic.Models.Volunteer;
using Bogus.DataSets;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using TextBox = HandyControl.Controls.TextBox;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Globalization;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using DocumentFormat.OpenXml.Office.CustomUI;
using HandyControl.Data;
using A_FGMS.DataLayer.Entities;
using System.DirectoryServices;
using A_FGMS.DataLayer.EventBroker;
using C_FGMS.UI.Helpers;
using B_FGMS.BusinessLogic.Events;

namespace C_FGMS.UI
{
    /// <summary>
    /// File Purpose : The Purpose of this file is to provide the interaction logic for the VolunteerFinancials.xaml file.
    /// 
    /// </summary>
    /// <author>Jon Maddocks</author>
    /// <additionContributors>CS471 WI23 Development Team</additionContributors>
    /// <date>1/20/23 </date>
    public partial class VolunteerFinancials : Page
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDialogProvider _dialogProvider;
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly DataRefreshEventBroker _refreshEventBroker;
        private clsGeneralBusinessLogic g_GeneralBusinessLogic = new clsGeneralBusinessLogic();
        private bool errorFlag;

        /// <summary>        
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: VolunteerFinancials class that runs when the page first starts. It initalizes all components
        /// and then populates all the volunteers finanaial data.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public VolunteerFinancials(IServiceProvider serviceProvider, IDialogProvider dialogProvider, DataRefreshEventBroker refreshEventBroker)
        {
            _serviceProvider = serviceProvider;
            _dialogProvider = dialogProvider;
            _volunteerProvider = _serviceProvider.GetRequiredService<IVolunteerProvider>();
            _refreshEventBroker = refreshEventBroker;

            _volunteerProvider.DatabaseError += ErrorHandler;

            errorFlag = false;
            InitializeComponent();

            _refreshEventBroker.Subscribe((args, x) =>
            {
                // Check if user is editing or doing something that might get interrupted if a refresh were to happen
                RefreshData();
            });

            PopulateMonthDropDown();
            PopulateYearDropDown();
            PopulateVolunteerDropDown();
            PopulateRates();
        }

        #region Error Handler
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

        #region Data Logic

        /// <summary>
        /// Function Name: RefreshData
        /// 
        /// Purpose: The purpose of this function is to repopulate the page with any new volunteer data.
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <date>April 4, 2023</date>
        private void RefreshData()
        {
            //If not in Edit mode, repopulate page
            if (btnEdit.Visibility == Visibility.Visible)
            {
                try
                {
                    PopulateMonthDropDown();
                    PopulateYearDropDown();
                    PopulateVolunteerDropDown();
                    PopulateRates();
                    GrowlHelpers.Success("Data Refreshed");
                }
                catch (Exception ex)
                {
                    _dialogProvider.ShowAlertDialog($"Could not retrieve volunteer financials. Please contact contact support if issue persists.\n{ex}", "Error");
                }
            } else
            {
                GrowlHelpers.Warning("Data could not be refreshed. Exit editing mode on Volunteer Financials to refresh data.");
            }
        }

        /// <summary>
        /// Function Name: PopulateYearDropDown
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Populates the volunteer dropdown on the page by calling the database and passing the data to the
        /// ItemsSource of the cmbSelectMonth combobox. 
        /// </summary>
        public void PopulateMonthDropDown()
        {
            List<string> lstMonths = new List<string>();
            lstMonths = g_GeneralBusinessLogic.GetMonths();
            cmbSelectMonth.ItemsSource = lstMonths;
            cmbSelectMonth.SelectedIndex = g_GeneralBusinessLogic.GetCurrentMonthIndex();
        }
        /// <summary>
        /// Function Name: PopulateYearDropDown
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Populates the volunteer dropdown on the page by calling the database and passing the data to the
        /// ItemsSource of the cmbSelectYear combobox. 
        /// </summary>
        public void PopulateYearDropDown()
        {
            var Years = _volunteerProvider.GetVolunteerFinancialsYears();
            if (errorFlag) { errorFlag = false; return; }
            if (!Years.Contains(DateTime.Now.Year))
            {
                Years = Years.Append(DateTime.Now.Year);
            }
            cmbSelectYear.ItemsSource = Years;

            if (Years != null)
            {
                cmbSelectYear.SelectedIndex = Years.Count() - 1;
            }
        }

        /// <summary>
        /// Function Name: PopulateVolunteerDropDown
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Populates the volunteer dropdown on the page by calling the database and passing the data to the
        /// ItemsSource of the cmbSelectVolunteer combobox.
        /// </summary>
        public void PopulateVolunteerDropDown()
        {
            var Names = _volunteerProvider.GetVolunteerNameAndId();
            if (errorFlag) { errorFlag = false; return; }
            cmbSelectVolunteer.ItemsSource = Names;
        }

        /// <summary>
        /// Function Name: PopulateVolunteerFinancials
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Populates the textboxes on the volunteers financials tab that is NOT the rates. It gets the data from the database
        /// and assignes it to the DataContext of each setion of the financals tab.
        /// </summary>
        public void PopulateVolunteerFinancials()
        {
            if(cmbSelectVolunteer.SelectedValue == null)
            {
                return;
            }
            DateTime monthYear = new DateTime((int)cmbSelectYear.SelectedItem, (int)cmbSelectMonth.SelectedIndex + 1, 1);
            bool verifyPtoStipend = _volunteerProvider.VerifyPtoStipendRecord((int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }
            bool verifyMealTransport = _volunteerProvider.VerifyMealTransportRecord((int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }

            //If no PTOStipend information exists, populate page with empty data
            if (verifyPtoStipend)
            {
                _volunteerProvider.CalculateVolunterFinancialsSingle((int)cmbSelectVolunteer.SelectedValue, monthYear);
                if (errorFlag) { errorFlag = false; return; }
                var clsVolunteerFinancialHours = _volunteerProvider.GetVolunteerFinancialsHours((int)cmbSelectVolunteer.SelectedValue, monthYear);
                if (errorFlag) { errorFlag = false; return; }
                var clsVolunteerFinancialPtoStipendModel = _volunteerProvider.GetVolunteersFinancialsPtoStipend((int)cmbSelectVolunteer.SelectedValue, monthYear);
                if (errorFlag) { errorFlag = false; return; }

                grdHours.DataContext = clsVolunteerFinancialHours;
                grdPTO.DataContext = clsVolunteerFinancialPtoStipendModel;
            } else if(!verifyPtoStipend)
            {
                PopulateEmptyFinancialData();
            }


            if (verifyMealTransport){
                //Populate financial meals information if exists
                var clsVolunteerFinancialMealTransportModel = _volunteerProvider.GetVolunteerFinancialsMealMileage((int)cmbSelectVolunteer.SelectedValue, monthYear);
                if (errorFlag) { errorFlag = false; return; }
                grdMealTransport.DataContext = clsVolunteerFinancialMealTransportModel;
            } else
            {
                PopulateEmptyMealTransportData();
            }
        }

        /// <summary>
        /// Function Name: PopulateRates
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: The purpose of this function is to populate the rates from the database to the volunteers financials page
        /// in the associated textboxes on that page.
        /// /// </summary>
        public void PopulateRates()
        {
            //get selected month and year
            if (cmbSelectMonth.SelectedIndex != null)
            {
                DateTime monthYear = new DateTime((int)cmbSelectYear.SelectedItem, (int)cmbSelectMonth.SelectedIndex + 1, 1);
                //Get data from VolunteerDatabaseProvider
                VolunteersFinancialsRatesModel? allFinancialRates = _volunteerProvider
                    .GetAllVolunteerFinancialRates(
                    _volunteerProvider.GetVolunteerFinancalsPtoStipendRates(monthYear)
                    , _volunteerProvider.GetVolunteerFinancialsMealTransportRates(monthYear));
                if (errorFlag) { errorFlag = false; return; }

                grdVolunteersFinancialsRates.DataContext = allFinancialRates;
            }
        }

        /// <summary>
        /// Function Name: cmbSelectYear_SelectionChanged
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: This function listens for when the selected Year changes and populates the financial data onto the page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSelectVolunteer.SelectedValue != null)
            {
                PopulateVolunteerFinancials();
                PopulateRates();
            }
        }

        /// <summary>
        /// Function Name: cmbSelectMonth_SelectionChanged
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: This function listens for when the selected Month changes and populates the financial data onto the page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSelectVolunteer.SelectedValue != null)
            {
                PopulateVolunteerFinancials();
                PopulateRates();
            }
        }

        /// <summary>
        /// Function Name: cmbSelectVolunteer_SelectionChanged
        /// Created By: Christopher Washburn
        /// Last Modified: 4/6/23
        /// Last Modified By: Isabelle Johns 
        /// 
        /// Purpose: This function listens for when the selected volunteer changes and populates the financial data onto the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectVolunteer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Persist the Volunteer Selection
            Application.Current.Properties["VolunteerTuid"] = cmbSelectVolunteer.SelectedIndex;

            if (cmbSelectVolunteer.SelectedValue != null)
            {
                PopulateVolunteerFinancials();
                PopulateRates();
            }
            else if (cmbSelectVolunteer.SelectedValue == null)
            {
                //Volunteer Name field has been cleared. Clear the form
                btnPtoEligible.IsChecked= false;
                foreach (HandyControl.Controls.TextBox tb in FindChildrenControls<HandyControl.Controls.TextBox>(grdContainerFinancials))
                {
                    if (!tb.Name.Contains("Rate"))
                    {
                        tb.Clear();
                    }
                }
            }
        }
        #endregion

        #region UI Events
        /// <summary>
        /// Function Name: BtnEdit_Click
        /// 
        /// Purpose: This function is responsbile for the click event of the Edit button on 
        /// the Volunteer Financials UI page. 
        /// 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Event specific that is being handled. </param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSelectVolunteer.SelectedValue == null)
            {
                GrowlHelpers.Warning("Please select a valid volunteer");
                return;
            }

            //Reset all tags to false
            foreach (HandyControl.Controls.TextBox tb in FindChildrenControls<HandyControl.Controls.TextBox>(grdContainerFinancials))
            {
                tb.Tag = false;
            }
            btnPtoEligible.Tag = "";

            if (string.IsNullOrEmpty(cmbSelectVolunteer.Text))
            {
                GrowlHelpers.Warning("Please select a volunteer");
            }
            else
            {
                SetToggleState(true);
            }
        }


        /// <summary>
        /// Function Name: BtnCancel_Click
        /// 
        /// Purpose: This function is responsible for the click event of the Cancel    
        /// button for the Volunteer Financials UI page.                        
        ///                                                                                                                                                                      
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Event specific that is being handled.</param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //Loop through all editable textboxes and verify whether or not any text changes were made
            bool cancelCheck = true;
            foreach (HandyControl.Controls.TextBox tb in FindChildrenControls<HandyControl.Controls.TextBox>(grdContainerFinancials))
            {
                if (tb.IsReadOnly != true && (bool)tb.Tag == true)
                {
                    cancelCheck = false;
                }
            }

            //Check if the PTO button was modified prior
            if(btnPtoEligible.Tag.Equals("true"))
            {
                cancelCheck = false;
            }

            //Changes were made, ask for confirmation on losing changes
            if(!cancelCheck)
            {
                bool? closeConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to cancel? Changes won't be saved.", "Confirmation");

                if (closeConfirmed == true)
                {
                    //User cancels changes, repopulate fields back to their record values
                    PopulateVolunteerFinancials();
                    SetToggleState(false);
                    GrowlHelpers.Info("Cancelled Changes");
                }
            } else
            {
                //No changes were made anywhere, reset toggle state without confirmation
                SetToggleState(false);
                GrowlHelpers.Info("Cancelled Changes");
            }
        }


        /// <summary>
        /// Function Name: BtnFinish_Click
        /// 
        /// Purpose: This function is responsible for the click event of the Finish/Save    
        /// button for the Volunteer Financials UI page.                        
        ///                                                                                                                                                                      
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Event specific that is being handled.</param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            //Validate before saving to the database
            List<bool> validationCheckList = new List<bool>();
            foreach (HandyControl.Controls.TextBox tb in FindChildrenControls<HandyControl.Controls.TextBox>(grdContainerFinancials))
            {
                //Any textbox that is not readOnly, assumes that textbox is editable with data
                if (tb.IsReadOnly != true)
                {
                    //Add the bool state returned
                    validationCheckList.Add(ValidateData(tb));
                }
            }
            //Check separately if PTO Used is valid
           validationCheckList.Add(ValidatePtoUsed());

            //Check if PTO End will have a negative balance 
           validationCheckList.Add(ValidatePtoEnd());

            //If any textbox fails the validation check, do not continue
            if (validationCheckList.Contains(false))
            {
                return;
            }
            // DATA IS VALID // 
            //Push data to the database and reset toggle state
            DateTime monthYear = new DateTime((int)cmbSelectYear.SelectedItem, (int)cmbSelectMonth.SelectedIndex + 1, 1);
            _volunteerProvider.PushVolunteerFinancialsHours(grdHours.DataContext as VolunteerFinancialsHoursModel, (int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }
            _volunteerProvider.PushVolunteerFinancialsMealTransport(grdMealTransport.DataContext as VolunteerFinancialsMealTransportModel, (int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }
            _volunteerProvider.PushVolunteerFinancialsPtoStipend(grdPTO.DataContext as VolunteerFinancialsPtoStipendModel, (int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }
            _volunteerProvider.CalculateVolunteerFinancialsYear((int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }

            //Data was saved to DB. Refresh data
            PopulateVolunteerFinancials();
            SetToggleState(false);
            Growl.Clear();
            GrowlHelpers.Success("Saved Changes");
        }


        /// <summary>
        /// Function Name: ValidDecimalNumber_PreviewTextInput
        /// 
        /// Purpose: The purpose of this function is to preview the key input that the user is typing. User is only
        ///     allowed to enter numbers. The user is allowed one decimal point as well. 
        ///     
        ///     If the user attempts to enter bad data, the keystroke is ignored and a Growl Warning is displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 25, 2023</date>
        private void ValidDecimalNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Get the current culture's decimal separator
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            TextBox textbox = (TextBox)sender;

            // Check if the input is a digit, a decimal separator, or a negative sign
            bool isDigit = char.IsDigit(e.Text, 0);
            bool isDecimalSeparator = e.Text == decimalSeparator || e.Text == ".";
            bool isEnterKey = e.Text == "\r" || e.Text == "\n";

            //If Enter is pressed, do nothing
            if (isEnterKey)
            {
                return;
            }

            // Allow digits, decimal separators
            if (isDigit || isDecimalSeparator)
            {
                // If a decimal separator is already present, disallow another one
                if (isDecimalSeparator && textbox.Text.Contains(decimalSeparator))
                {
                    e.Handled = true;
                    textbox.BorderBrush = Brushes.Red;
                    Growl.Clear();
                    GrowlHelpers.Warning("Only one decimal point is valid");
                }
                else
                {
                    textbox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));
                    // Input is valid, allow the event
                    e.Handled = false;
                }
            }
            else
            {
                // Input is not valid, disallow the event
                e.Handled = true;
                textbox.BorderBrush = Brushes.Red;
                Growl.Clear();
                GrowlHelpers.Warning("Please input only valid numbers");
            }
        }

        /// <summary>
        /// Function Name: ValidWholeNumber_PreviewTextInput
        /// 
        /// Purpose: The purpose of this function is to preview the key input that the user is typing. User is only
        ///     allowed to enter whole numbers.
        ///     
        ///     If the user attempts to enter bad data, the keystroke is ignored and a Growl Warning is displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 25, 2023</date>
        private void ValidWholeNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textbox = (TextBox)sender;   
            decimal result;
            bool isEnterKey = e.Text == "\r" || e.Text == "\n";

            //If Enter is pressed, do nothing
            if (isEnterKey)
            {
                return;
            }

            if (!decimal.TryParse(e.Text, out result))
            {
                // Input is not valid, disallow the event, else allow
                e.Handled = true;
                textbox.BorderBrush = Brushes.Red;
                Growl.Clear();
                GrowlHelpers.Warning("Please input only valid numbers");
            } else
            {
                textbox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));
            }
        }

        /// <summary>
        /// Function Name: btnPtoEligible_PreviewMouseDown
        /// 
        /// Purpose: The purpose of this function is to display a warning dialog message when the user is attempting to switch the
        ///     PTO Eligibilty from Checked to Unchecked. Confirming will not change database information, only until Save is verified. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 26, 2023</date>
        private void btnPtoEligible_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Only fire event when the user is attempting to change the state from Checked to Unchecked
            if (btnPtoEligible.IsChecked == true && e.ChangedButton == MouseButton.Left)
            {
                e.Handled = true;

                //Get current name of volunteer 
                VolunteerNameIdModel? selectedVolunteer = cmbSelectVolunteer.SelectedItem as VolunteerNameIdModel;
                if (selectedVolunteer != null)
                {
                    //Get confirmation
                    bool? closeConfirmed = _dialogProvider.ShowConfirmationDialog($"{selectedVolunteer.FullName} will lose all PTO Eligibility going forward from {cmbSelectMonth.Text} {cmbSelectYear.Text}." +
                    $" Resulting in losing PTO Earned and PTO Used during those times.\n\nThis action cannot be undone. Are you sure you want to continue?", "Confirmation");

                    if (closeConfirmed == true)
                    {
                        //Confirmation is Yes, uncheck button
                        btnPtoEligible.IsChecked = false;
                        e.Handled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Function Name: cmbSelectVolunteer_Loaded
        /// 
        /// Purpose: The purpose of this function is to load-in the current volunteer index for the volunteer ComboBox. This is meant
        ///     to keep consistency throughout the Volunteer pages. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 26, 2023</date>
        private void cmbSelectVolunteer_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnEdit.Visibility == Visibility.Hidden)  //in edit
            {
                return;
            }

            // Persist Volunteer Selection
            int? selectedVolunteer = (int?)Application.Current.Properties["VolunteerTuid"];

            if (selectedVolunteer.HasValue)
            {
                cmbSelectVolunteer.SelectedIndex = selectedVolunteer.Value;
            }
            else
            {
                cmbSelectVolunteer.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Function Name: TextBox_TextChanged
        /// 
        /// Purpose: The purpose of this function is to set the Tag value to true for whenever text box has it's content changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 26, 2023</date>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Tag = true;
        }

        /// <summary>
        /// Function Name: btnPtoEligible_Checked
        /// 
        /// Purpose: The purpose of this function is to set the Tag value to true for whenever the PTO Eligible toggle button was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 28, 2023</date>
        private void btnPtoEligible_Checked(object sender, RoutedEventArgs e)
        {
            btnPtoEligible.Tag = "true";
        }

        /// <summary>
        /// Function Name: btnPtoEligible_Unchecked
        /// 
        /// Purpose: The purpose of this function is to set the Tag value to true for whenever the PTO Eligible toggle button was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 28, 2023</date>
        private void btnPtoEligible_Unchecked(object sender, RoutedEventArgs e)
        {
            btnPtoEligible.Tag = "true";
        }
        #endregion

        #region UI States
        /// <summary>
        /// This function is responsible for returning all of the children    
        /// controls from the parent.Such as, the function will be seeking
        /// all TextBoxes on the page when given the parent.                  
        /// </summary>
        /// <typeparam name="T">Arbitrary generic type.</typeparam>
        /// <param name="depObj">Represents an object that participates in the 
        ///  dependency property system.</param>
        /// <returns></returns>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private static IEnumerable<T> FindChildrenControls<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindChildrenControls<T>(ithChild)) yield return childOfChild;
            }
        }

        /// <summary>
        /// Function Name: SetToggleState
        /// This function is responsible for settings the toggle status for   
        /// the page.The user can change the toggle status by clicking on
        /// either the Edit, Cancel, or Finish button.                        
        ///
        /// Edit will enable TextBoxes and show Toggle Switch controls.       
        /// Cancel & Finish will disable Textboxes and hide Toggle Switch     
        /// controls.                                                         
        /// </summary>
        /// <param name="blnEditState">boolean representing if the user is in Edit mode.</param>
        /// <param name="visButtonState">Visibility status for the Toggle Switch buttons</param>
        /// <param name="visTextState">Visibility status for the TextBoxes</param>
        /// <author>Jon Maddocks</author>
        /// <dateCreated>January 26, 2023</dateCreated>
        private void SetToggleState(bool blnEditState)
        {
            if (blnEditState)
            {
                btnEdit.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Visible;
                btnFinish.Visibility = Visibility.Visible;
                txtEditMode.Visibility = Visibility.Visible;

                UIEditState(false, Brushes.White);
            }
            else
            {
                btnEdit.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Hidden;
                btnFinish.Visibility = Visibility.Hidden;
                txtEditMode.Visibility = Visibility.Hidden;

                UIEditState(true, Brushes.Transparent);
            }
        }

        /// <summary>
        /// Function Name: UIEditState
        /// 
        /// Purpose: Set the read only state for each text box and comboboxes. Set the background and border brush color
        ///     for each text box. 
        /// </summary>
        /// <param name="readOnlyState">Read only boolean state</param>
        /// <param name="colorState">Color background for text field</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 19, 2023</date>
        private void UIEditState(bool readOnlyState, Brush colorState)
        {
            btnPtoEligible.IsEnabled = !readOnlyState;
            cmbSelectVolunteer.IsEnabled = readOnlyState;
            cmbSelectMonth.IsEnabled = readOnlyState;
            cmbSelectYear.IsEnabled = readOnlyState;

            txtRegHours.IsReadOnly = readOnlyState;
            txtRegHours.Background = colorState;
            txtRegHours.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));

            txtPTOUsed.IsReadOnly = readOnlyState;
            txtPTOUsed.Background = colorState;
            txtPTOUsed.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));
            
            txtSiteMeals.IsReadOnly = readOnlyState;
            txtSiteMeals.Background = colorState;
            txtSiteMeals.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));

            txtMileage.IsReadOnly = readOnlyState;
            txtMileage.Background = colorState;
            txtMileage.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));

            txtBusRides.IsReadOnly = readOnlyState;
            txtBusRides.Background = colorState;
            txtBusRides.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));
        }

        /// <summary>
        /// Function Name: PopulateEmptyFinancialData
        /// 
        /// Purpose: The purpose of this function is to insert a new record into the database. This occurs when no record is found
        ///     for a volunteer in the database. 
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <date>March 25, 2023</date>
        private void PopulateEmptyFinancialData()
        {
            DateTime monthYear = new DateTime((int)cmbSelectYear.SelectedItem, (int)cmbSelectMonth.SelectedIndex + 1, 1);
            VolunteerFinancialsHoursModel clsVolunteerFinancialHours = new VolunteerFinancialsHoursModel();
            VolunteerFinancialsPtoStipendModel clsVolunteerFinancialPtoStipendModel = new VolunteerFinancialsPtoStipendModel();

            //Set all the data context's to 0
            grdHours.DataContext = clsVolunteerFinancialHours;
            grdPTO.DataContext = clsVolunteerFinancialPtoStipendModel;

            ////Check the PTO eligibilty for volunteer. Check if they're eligible, else uncheck. 
            if (_volunteerProvider.GetPreviousMonthPTOEligibility((int)cmbSelectVolunteer.SelectedValue, monthYear))
            {
                if (errorFlag) { errorFlag = false; return; }
                btnPtoEligible.IsChecked = true;
            }
            else
            {
                if (errorFlag) { errorFlag = false; return; }
                btnPtoEligible.IsChecked = false;
            }

            _volunteerProvider.PushVolunteerFinancialsHours(grdHours.DataContext as VolunteerFinancialsHoursModel, (int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }
            if (btnPtoEligible.IsChecked == true)
            {
                _volunteerProvider.PushVolunteerFinancialsPtoStipend(grdPTO.DataContext as VolunteerFinancialsPtoStipendModel, (int)cmbSelectVolunteer.SelectedValue, monthYear);
                if (errorFlag) { errorFlag = false; return; }
            }

            _volunteerProvider.CalculateVolunterFinancialsSingle((int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }

            //Data was saved to DB. Refresh data
            PopulateVolunteerFinancials();
            SetToggleState(false);
            Growl.Clear();
            GrowlHelpers.Success($"No record found for {cmbSelectMonth.SelectedValue} {cmbSelectYear.SelectedValue}. New record added to database.");
        }

        /// <summary>
        /// Function Name: PopulateEmptyMealTransportData
        /// 
        /// Purpose: The purpose of this function is to insert a new meal transport record into the database. This occurs when no record is found
        ///     for a volunteer in the database. 
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <date>April 6, 2023</date>
        private void PopulateEmptyMealTransportData()
        {
            DateTime monthYear = new DateTime((int)cmbSelectYear.SelectedItem, (int)cmbSelectMonth.SelectedIndex + 1, 1);
            VolunteerFinancialsMealTransportModel clsVolunteerFinancialsMealTransportModel = new VolunteerFinancialsMealTransportModel();
            grdMealTransport.DataContext = clsVolunteerFinancialsMealTransportModel;
            _volunteerProvider.PushVolunteerFinancialsMealTransport(grdMealTransport.DataContext as VolunteerFinancialsMealTransportModel, (int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }
            _volunteerProvider.CalculateVolunterFinancialsSingle((int)cmbSelectVolunteer.SelectedValue, monthYear);
            if (errorFlag) { errorFlag = false; return; }

            PopulateVolunteerFinancials();
            SetToggleState(false);
            Growl.Clear();
            GrowlHelpers.Success($"No record found for {cmbSelectMonth.SelectedValue} {cmbSelectYear.SelectedValue}. New record added to database.");
        }
        #endregion

        #region Error Validation
        /// <summary>
        /// Function Name: ValidateData
        /// 
        /// Purpose: The purpose of this function is to return a boolean check on whether or not a textbox passes the validation
        ///     check. This includes invalid data (empty) and a fixed number range of 1000. 
        /// </summary>
        /// <param name="tb">Textbox to be validated</param>
        /// <returns>Boolean check if the textbox passes</returns>
        /// <author>Jon Maddocks</author>
        /// <date>March 26, 2023</date>
        private bool ValidateData(TextBox tb)
        {
            decimal number;
            tb.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0E0E0"));
            //Check for empty data
            if (!Decimal.TryParse(tb.Text, out number))
            {
                Growl.Warning($"{tb.Name[3..]} does not contain valid data.\nEnter 0 if no data exists.");
                tb.BorderBrush = Brushes.Red;
                return false;
            }

            //Ensure number is not greater than 1000
            if (number > 1000)
            {
                GrowlHelpers.Warning($"Please keep number in {tb.Name[3..]} between 0-999");
                tb.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Function Name: ValidatePtoUsed
        /// 
        /// Purpose: The purpose of this function is to specifically validate PTO Used. The PTO Used amount should not
        ///     exceed the PTO Earned amount. 
        /// </summary>
        /// <returns>Boolean check if the PTO Used textbox passes</returns>
        /// <author>Jon Maddocks</author>
        /// <date>March 26, 2023</date>
        private bool ValidatePtoUsed()
        {
            decimal.TryParse(txtPTOUsed.Text, out decimal ptoUsed);
            decimal.TryParse(txtPTOStart.Text, out decimal ptoStart);
            decimal.TryParse(txtPTOEarned.Text, out decimal ptoEarned);

            decimal totalPtoAmount = ptoStart + ptoEarned;
            //Ensure that PTO used is not greater than the PTO earned
            if (ptoUsed > totalPtoAmount)
            {
                GrowlHelpers.Warning($"PTO Used cannot be greater than PTO Started + PTO Earned for the month. Please try again.");
                txtPTOUsed.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Function Name: ValidatePtoEnd
        /// 
        /// Function Name: The purpose of this function is validate the PTO End. This validation may occur when the regular hours is modified and PTO used will exceed the total
        ///     amount the volunteer has generated. This would cause the PTO End value to become a negative balance. This check ensures the user must modify the PTO used or
        ///     regular hours in order to avoid this negative balance. 
        /// </summary>
        /// <returns>Boolean check for the validation</returns>
        /// <author>Jon Maddocks</author>
        /// <date>April 11, 2023</date>
        private bool ValidatePtoEnd()
        {
            decimal.TryParse(txtPTOUsed.Text, out decimal ptoUsed);
            decimal.TryParse(txtPTOStart.Text, out decimal ptoStart);

            decimal.TryParse(txtRegHours.Text, out decimal regHours);
            decimal.TryParse(txtCurrentPTORate.Text, out decimal currentPTORate);

            decimal ptoEarned = regHours * currentPTORate;
            decimal ptoEnd = Math.Round(ptoStart - ptoUsed + ptoEarned, 0, MidpointRounding.AwayFromZero);
            if (ptoEnd < 0)
            {
                GrowlHelpers.Warning($"PTO End will contain a negative balance. Please reassign PTO Used or Regular Hours");
                txtPTOUsed.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }
        #endregion
        /// <summary>
        /// Function Name: btnRefresh_Click
        /// 
        /// Purpose: The purpose of this function is to refresh the page for new volunteer information. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Richard Nader</author>
        /// <date>April 5, 2023</date>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnRefresh.IsEnabled = false;
            _refreshEventBroker.Publish(nameof(VolunteerFinancials));
            btnRefresh.IsEnabled = true;
        }
    }
}