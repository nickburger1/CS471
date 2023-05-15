using B_FGMS.BusinessLogic.Models.Volunteer;
using B_FGMS.BusinessLogic.Services.DialogProvider;
using B_FGMS.BusinessLogic.Services.VolunteerProviders;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddNewDemographicsInfo.xaml
    /// </summary>
    public partial class AddNewDemographicsInfo : System.Windows.Window
    {        
        private readonly IVolunteerProvider _volunteerProvider;
        private readonly IDialogProvider _dialogProvider;

        public AddNewDemographicsInfo(IServiceProvider serviceProvider)
        {
            _volunteerProvider = serviceProvider.GetRequiredService<IVolunteerProvider>();
            _dialogProvider = serviceProvider.GetRequiredService<IDialogProvider>();
            InitializeComponent();
        }

        /// <summary>
        /// Function Name: rdoGender_Checked
        /// 
        /// Purpose: Fill Combobox items with GenderTypeItems when gender radio button is clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void rdoGender_Checked(object sender, RoutedEventArgs e)
        {
            SetGenderItems();
        }

        /// <summary>
        /// Function Name: rdoIdentifiesAs_Checked
        /// 
        /// Purpose: Fill Combobox items with IdentifiesAsTypeItems when identifies as radio button is clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void rdoIdentifiesAs_Checked(object sender, RoutedEventArgs e)
        {
            SetIdentifiesAsItems();
        }

        /// <summary>
        /// Function Name: rdoEthnicity_Checked
        /// 
        /// Purpose: Fill Combobox items with EthnicityTypeItems when ethnicity radio button is clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void rdoEthnicity_Checked(object sender, RoutedEventArgs e)
        {
            SetEthnicityItems();
        }

        /// <summary>
        /// Function Name: rdoRacialGroup_Checked
        /// 
        /// Purpose: Fill Combobox items with RacialGroupTypeItems when racial group radio button is clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void rdoRacialGroup_Checked(object sender, RoutedEventArgs e)
        {
            SetRacialGroupItems();
        }

        /// <summary>
        /// Function Name: btnSave_Click
        /// 
        /// Purpose: Attempt to add new item to the respective category that is currently selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                if (rdoGender.IsChecked.Value)
                {
                    SaveNewGenderItem();
                } 
                else if (rdoEthnicity.IsChecked.Value)
                {
                    SaveNewEthnicityItem();
                } 
                else if (rdoIdentifiesAs.IsChecked.Value)
                {
                    SaveNewIdentifiesAsItem();
                } 
                else if (rdoRacialGroup.IsChecked.Value)
                {
                    SaveNewRacialGroupItem();
                }
            }
            else
            {
                Growl.Error("Please ensure text field is not empty");
            }
        }

        /// <summary>
        /// Function Name: btnDelete_Click
        /// 
        /// Purpose: Attempt to delete all items that is currently selected in the CheckComboBox. These items
        ///     correlate to the respective category that is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = cmbSelectItems.SelectedItems;
            if(selectedItems.Count == 0)
            {
                Growl.Warning("Please ensure delete field is not empty");
            } else
            {
                bool? result = _dialogProvider.ShowConfirmationDialog("Are you sure you want to delete?", "Confirmation");
                if(result == true)
                {
                    //Check which category is selected and begin deletion process
                    if (rdoGender.IsChecked.Value)
                    {
                        foreach (GenderNameIdModel item in selectedItems)
                        {
                            DeleteGenderItems(item);
                        }
                        SetGenderItems();
                    }
                    else if (rdoEthnicity.IsChecked.Value)
                    {
                        foreach(EthnicityNameIdModel item in selectedItems)
                        {
                            DeleteEthnicityItems(item);
                        }
                        SetEthnicityItems();
                    }
                    else if (rdoIdentifiesAs.IsChecked.Value)
                    {
                        foreach (IdentifiesAsNameIdModel item in selectedItems)
                        {
                            DeleteIdentifiesAsItems(item);
                        }
                        SetIdentifiesAsItems();
                    }
                    else if (rdoRacialGroup.IsChecked.Value)
                    {
                        foreach (RacialGroupNameIdModel item in selectedItems)
                        {
                            DeleteRacialGroupItems(item);
                        }
                        SetRacialGroupItems();
                    }
                }
            }
        }

        /// <summary>
        /// Function Name: SetGenderItems
        /// 
        /// Purpose: Set item source to gender combobox. Fill and/or refresh combobox with GenderTypeItems.
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SetGenderItems()
        {
            var genders = _volunteerProvider.GetGenderNameAndId(false);
            cmbSelectItems.ItemsSource = genders;
        }

        /// <summary>
        /// Function Name: SetIdentifiesAsItems
        /// 
        /// Purpose: Set item source to identifies as combobox. Fill and/or refresh combobox with IdentifiesAsTypeItems.
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SetIdentifiesAsItems()
        {
            var identifiesAs = _volunteerProvider.GetIdentifiesAsNameAndId(false);
            cmbSelectItems.ItemsSource = identifiesAs;
        }

        /// <summary>
        /// Function Name: SetEthnicityItems
        /// 
        /// Purpose: Set item source to ethnicty combobox. Fill and/or refresh combobox with EthnicityTypeItems.
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SetEthnicityItems()
        {
            var ethnicities = _volunteerProvider.GetEthnityNameAndId(false);
            cmbSelectItems.ItemsSource = ethnicities;
        }

        /// <summary>
        /// Function Name: SetRacialGroupItems
        /// 
        /// Purpose: Set item source to racial group combobox. Fill and/or refresh combobox with RacialGroupTypeItems.
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SetRacialGroupItems()
        {
            var racialGroup = _volunteerProvider.GetRacialGroupNameAndId(false);
            cmbSelectItems.ItemsSource = racialGroup;
        }

        /// <summary>
        /// Function Name: SaveNewGenderItem
        /// 
        /// Purpose: Add new item to GenderTypeItems database table. 
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SaveNewGenderItem()
        {
            _volunteerProvider.AddGenderItem(txtItem.Text);
            txtItem.Clear();
            SetGenderItems();
        }

        /// <summary>
        /// Function Name: SetIdentifiesAsItems
        /// 
        /// Purpose: Add new item to IdentifiesAsTypeItems database table. 
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SaveNewIdentifiesAsItem()
        {
            _volunteerProvider.AddIdentifiesAsItem(txtItem.Text);
            txtItem.Clear();
            SetIdentifiesAsItems();
        }

        /// <summary>
        /// Function Name: SaveNewEthnicityItem
        /// 
        /// Purpose: Add new item to EthnicityTypeItems database table. 
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SaveNewEthnicityItem()
        {
            _volunteerProvider.AddEthnicityItem(txtItem.Text);
            txtItem.Clear();
            SetEthnicityItems();
        }

        /// <summary>
        /// Function Name: SaveNewRacialGroupItem
        /// 
        /// Purpose: Add new item to RacialGroupTypeItems database table. 
        /// </summary>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void SaveNewRacialGroupItem()
        {
            _volunteerProvider.AddRacialGroupItem(txtItem.Text);
            txtItem.Clear();
            SetRacialGroupItems();
        }


        /// <summary>
        /// Function Name: DeleteGenderItems
        /// 
        /// Purpose: Delete all selected items in the CheckComboBox from the GenderTypeItems database table. 
        /// </summary>
        /// <param name="item">Gender item to be deleted</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void DeleteGenderItems(GenderNameIdModel item)
        {
            //Get every potential volunteer with the specific gender item
            var volunteersWithItem = _volunteerProvider.GetVolunteersWithGender(item.Tuid);
            //If no volunteer has this item. Delete item from it's respective category
            if (!volunteersWithItem.Any())
            {
                //Deletion successful
                _volunteerProvider.DeleteGenderItem(item);
            }
            else
            {
                //Iterate and detail each volunteer that currently has this gender
                foreach (var vol in volunteersWithItem)
                {
                    Growl.Error($"Cannot delete: {item.Name}\n{vol.FullName} is currently {item.Name}");
                }
            }
        }

        /// <summary>
        /// Function Name: DeleteIdentifiesAsItems
        /// 
        /// Purpose: Delete all selected items in the CheckComboBox from the IdentifiesAsTypeItems database table. 
        /// </summary>
        /// <param name="item">Identifies As item to be deleted</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void DeleteIdentifiesAsItems(IdentifiesAsNameIdModel item)
        {
            var volunteersWithItem = _volunteerProvider.GetVolunteersWithIdentifiesAs(item.Tuid);
            //If no volunteer has this item. Delete item from it's respective category
            if (!volunteersWithItem.Any())
            {
                //Deletion successful
                _volunteerProvider.DeleteIdentifiesAsItem(item);
            }
            else
            {
                //Iterate and detail each volunteer that currently has this gender
                foreach (var vol in volunteersWithItem)
                {
                    Growl.Error($"Cannot delete: {item.Name}\n{vol.FullName} is currently {item.Name}");
                }
            }
            
        }

        /// <summary>
        /// Function Name: DeleteEthnicityItems
        /// 
        /// Purpose: Delete all selected items in the CheckComboBox from the EthnicityTypeItems database table. 
        /// </summary>
        /// <param name="item">Ethnicity item to be deleted</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void DeleteEthnicityItems(EthnicityNameIdModel item)
        {
            var volunteersWithItem = _volunteerProvider.GetVolunteersWithEthnicity(item.Tuid);
            //If no volunteer has this item. Delete item from it's respective category
            if (!volunteersWithItem.Any())
            {
                //Deletion successful
                _volunteerProvider.DeleteEthnicityItem(item);
            }
            else
            {
                //Iterate and detail each volunteer that currently has this gender
                foreach (var vol in volunteersWithItem)
                {
                    Growl.Error($"Cannot delete: {item.Name}\n{vol.FullName} is currently {item.Name}");
                }
            }
        }

        /// <summary>
        /// Function Name: DeleteRacialGroupItems
        /// 
        /// Purpose: Delete all selected items in the CheckComboBox from the RacialGroupTypeItems database table. 
        /// </summary>
        /// <param name="item">Racial group item to be deleted</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        private void DeleteRacialGroupItems(RacialGroupNameIdModel item)
        {
            var volunteersWithItem = _volunteerProvider.GetVolunteersWithRacialGroup(item.Tuid);
            //If no volunteer has this item. Delete item from it's respective category
            if (!volunteersWithItem.Any())
            {
                //Deletion successful
                _volunteerProvider.DeleteRacialGroupItem(item);
            }
            else
            {
                //Iterate and detail each volunteer that currently has this gender
                foreach (var vol in volunteersWithItem)
                {
                    Growl.Error($"Cannot delete: {item.Name}\n{vol.FullName} is currently {item.Name}");
                }
            }
        }
    }
}
