   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters  ,    &C_FGMS.UI.ReportsVolunteerInfoPageEdit   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     5local1"clr-namespace:C_FGMS.UI.Converters     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$   Edit Volunteer Information��$	��700��$	��800�� � 
ResizeMode$ CanMinimize=� � 	Resources 6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          .���   �� 
TargetType �� BasedOn1  +HandyControl.Themes.StaticResourceExtension �� Property �� Value��%M   'd�       �� " d�   *ToggleButtonSwitch+.���  � ��$ 10q� � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � � ColumnDefinitions �� $��* ��� $��* �.+�� $��1��$��Right=� �� $
��auto ��� $
��auto �.+��� -   	 ��Name$	 btnSave��$��0��$��5,0,5,0q�$��Right=�$	��Top=����� *UpdateCommand+{��� *	HasErrors+
 �� 	Converter#
 [  .���� $��/resources/save-icon.png�� � ToolTip$
 Save���� -   $	 	btnCancel��$	��Top=�$��5,0,5,0q�$��Right=�$��1��.���� $ ��/resources/cancel-icon.png��$ Cancel��� $��1��$��Stretch=� �� $��* ��� $��* �.+�#  HandyControl.Controls.TextBox  -   $��txtFirstName��$��0��$��Stretch=�$��10q� �� TextWrapping$
 Wrap=�$	��Top=�$
��auto��(  "HandyControl.Controls.TitleElement   Title$ First Name: �� �� MaxLines$ 1��$��0��'  !HandyControl.Controls.InfoElement   Placeholder$ 
First Name������ *	FirstName+ �� UpdateSourceTrigger$ PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��  -   $��txtLastName��$��1��$��Stretch=�$��10q�$
 Wrap=�$	��Top=�$
��auto��$ Last Name: ��$ 1��$��0��$ 	Last Name������ *
LastName+$ PropertyChanged=�$
 True��&   HandyControl.Controls.DatePicker  -   $��txtDateOfBirth��$��2��$��0��$��Stretch=�$��10q�$	��Top=�$
��auto��$ Select Date of Birth:��$��1��$ Date of Birth��(  "System.Windows.Controls.DatePicker   SelectedDate �� *DateOfBirth+$ PropertyChanged=�$
 True��$  HandyControl.Controls.ComboBox  -     Name$ comboGender��$��3��$��0�� �� DisplayMemberPath$
 Name�� �� SelectedValue$
 Tuid��   AutoComplete$
 True�� �� 
IsEditable$
 True��$
��auto��$
��auto��   TitlePlacement$	 Top=�$ Gender��$��10q� �� Text$	 All��$ Gender��$��2������ *	Genders+ �� SelectedItem ��  �� Path$ SelectedGender� �� Mode$ TwoWay=�  -   $ comboIdentifies��$��4��$��0��$
 Name��$
 Tuid��$
 True��$
 True��$
��auto��$
��auto��$	 Top=�$ Identifies As��$��10q�$	 All��$  Identifies As��$��3������ *
Identifies+ �� $ SelectedIdentifiesAs� �� TargetNullValue$  ��$ TwoWay=�  -   $ comboEthnicity��$��5��$��0��$
 Name��$
 Tuid��$
 True��$
 True��$
��auto��$
��auto��$	 Top=�$ 	Ethnicity��$��10q�$	 All��$ 	Ethnicity��$��4������ *	Ethnicity+ �� $ SelectedEthnicity�$  ��$ TwoWay=�  -	   $ comboRacialGroup��$��6��$��0��$
 Name��$
 Tuid��$
 True��$
 True��$
��auto��$
��auto��$	 Top=�$ Racial Group��$��10q�$	 All��$ Racial Group��$��5������ *RacialGroup+ �� $ SelectedRacialGroup�$  ��$ TwoWay=�d� -
    d�Name$ 
btnVeteran��$��1��$��1��$��Veteran��$
��Left=�$	 Top=�$ Veteran��$��10q�$��6�� d� 	IsChecked �� *	IsVeteran+d� -   $ btnFamilyMilitary��$��2��$��1��$��Family of Military��$
��Left=�$��7�� �� *IsFamilyOfMilitary+d� -   $ 	btnStatus��$��3��$��1��$��Active��$
��Left=�$��8�� �� *
IsActive+  -   $��txtStartDate��$��4��$��1��$��Stretch=�$��10q�$	��Top=�$
��auto��$ Start Date:��$��9��$ 
Start Date�� �� *	StartDate+$ PropertyChanged=�$
 True��  -   $��
txtEndDate��$��5��$��1��$��Stretch=�$��10q�$	��Top=�$
��auto��$ 	End Date:��$��10��    ShowClearButton$
  True��$ End Date�� �� *	EndDate+$ PropertyChanged=�$
 True��  -   $ comboSeparated��$��6��$��1��$
 Name��$
 Tuid��$
 True��!   ShowClearButton$
! True��$
 True��$
��auto��$��60��$	 Top=�$ Reason Separated:��$��10q�$  Reason Separated��$��11������ *InactiveStatusTypes+ �� $ SelectedInactiveStatus�$  ��$ TwoWay=�