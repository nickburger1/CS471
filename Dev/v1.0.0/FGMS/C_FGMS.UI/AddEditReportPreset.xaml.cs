using B_FGMS.BusinessLogic.Events;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.ReportProviders;
using DocumentFormat.OpenXml.Drawing;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AddEditReportPreset.xaml
    /// </summary>
    public partial class AddEditReportPreset : System.Windows.Window
    {
        #region Variables
        private int intID;
        private List<TreeNode> _reportStructure;
        private string? strName;
        private readonly IDialogProvider _dialogProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportPresetProvider _presetProvider;
        private bool errorFlag;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for AddEditReportPreset
        /// </summary>
        /// <param name="strName"></param>        
        /// <author>Nathan VanSnepson</author>
        /// <created>3/21/23</created>
        public AddEditReportPreset(IServiceProvider serviceProvider, List<TreeNode> reportStructure, int intID = -1)
        {
            InitializeComponent();
            _reportStructure = reportStructure;
            _serviceProvider = serviceProvider;
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();
            _presetProvider = serviceProvider.GetRequiredService<IReportPresetProvider>();

            errorFlag = false;

            this.intID = intID;

            if (intID > -1)
            {
                var preset = _presetProvider.GetReportPreset(intID);
                if (errorFlag) { errorFlag = false; return; }
                if (preset != null)
                {
                    strName = preset.Name;
                    txtName.Text = strName;
                }
            }
            else
            {
                strName = "";
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

        #region Button Events
        /// <summary>
        /// Is called when button add is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>3/21/23</created>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            if(intID > -1) 
            {
                //edit a preset
                ReportPresetModel? reportPresetModel = _presetProvider.GetReportPreset(intID);
                if (errorFlag) { errorFlag = false; return; }

                if (reportPresetModel != null)
                {
                    //see if the name text is the same as the preset text
                    if (reportPresetModel.Name == null ? false : reportPresetModel.Name.Equals(txtName.Text))
                    {
                        reportPresetModel.Preset = _reportStructure;
                        _presetProvider.UpdateReportPreset(reportPresetModel);
                        if (errorFlag) { errorFlag = false; return; }
                        DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        //check that the new name is not already taken
                        if (_presetProvider.MatchPresetOnName(txtName.Text))
                        {
                            if (errorFlag) { errorFlag = false; return; }
                            Growl.Warning(new GrowlInfo
                            {
                                Message = "This name is already used in the database by another preset. Please use another name.",
                                ShowDateTime = false,
                                StaysOpen = false,
                                WaitTime = 2,
                            });
                            return;
                        }
                        else
                        {
                            reportPresetModel.Name = txtName.Text;
                            _presetProvider.UpdateReportPreset(reportPresetModel);
                            if (errorFlag) { errorFlag = false; return; }
                            DialogResult = true;
                            this.Close();
                        }
                        if (errorFlag) { errorFlag = false; return; }
                    }
                }
            }
            else
            {
                string? existingName = _presetProvider.MatchPresetOnPreset(_reportStructure);
                if (errorFlag) { errorFlag = false; return; }

                //check that the new name is not already taken
                if (_presetProvider.MatchPresetOnName(txtName.Text))
                {
                    if (errorFlag) { errorFlag = false; return; }
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "This name is already used in the database by another preset. Please use another name.",
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 2,
                    });
                    return;
                }else if (!string.IsNullOrEmpty(existingName))
                {
                    if (errorFlag) { errorFlag = false; return; }

                    //if a preset already exists with the same structure tell the user that presets name
                    Growl.Warning(new GrowlInfo
                    {
                        Message = "A preset with the same structure already exists. Its name is " + existingName,
                        ShowDateTime = false,
                        StaysOpen = false,
                        WaitTime = 2,
                    });
                }
                else
                {
                    if (errorFlag) { errorFlag = false; return; }

                    //create a preset
                    ReportPresetModel reportPresetModel = new ReportPresetModel()
                    {
                        Name = txtName.Text,
                        Active = false,
                        Current = false,
                        Former = false,
                        Inactive = false,
                        LastUpdated = DateTime.Now,
                        Preset = _reportStructure,
                        SortBy = ""
                    };
                    _presetProvider.CreateReportPreset(reportPresetModel);
                    if (errorFlag) { errorFlag = false; return; }
                    DialogResult = true;
                    this.Close();
                }
                
            }
            
        }



        #endregion

        #region Overrides

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!txtName.Text.Equals(strName) && intID > -1)
            {
                bool? closeConfirmed = _dialogProvider.ShowConfirmationDialog("Are you sure you want to exit? Changes won't be saved.", "Confirmation");

                if (closeConfirmed != null && (bool)!closeConfirmed)
                {
                    e.Cancel = true;
                }
            }
            base.OnClosing(e);
        }
        #endregion

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Growl.Ask("Changes won't be saved, are you sure you would like to exit?", confirm =>
                {
                    if (confirm)
                    {
                        DialogResult = false;
                        this.Close();
                    }
                    return true;
                });
            }
            else
            {
                DialogResult = false;
                this.Close();
            }
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
    }
}
