   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem "   C_FGMS.UI.AddVolunteerWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     6sys&clr-namespace:System;assembly=mscorlib     S	 OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol 	   � Title$  New Volunteer��$	��560��$
��1000�� � WindowStartupLocation$ CenterOwner=�.��� -    �Name$ grdMain��
������� � ColumnDefinitions �� $��* ��� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $
��7.5* � � 	Resources   	 HandyControl.Controls.Card �� 
TargetType   CardBaseStyle �� BasedOn �� Property  EffectShadow2	 �� Value# 	 HandyControl.Controls.TextBox  TextBoxExtend
 �� Triggers P� Property P� Value# 	 HandyControl.Controls.Divider  DividerBaseStyle& 	  HandyControl.Controls.DatePicker  DatePickerPlusBaseStyle( 	 "HandyControl.Controls.TitleElement1 	 +HandyControl.Themes.StaticResourceExtension  ExpanderBaseStyle( 	 "HandyControl.Controls.ScrollViewer  ScrollViewerBaseStyle$ 	 HandyControl.Controls.ComboBox  ComboBoxPlusBaseStyle��%  '        7   '  v     7  '�� �    '  I    7  '  �    7  'd� �    '9� p    7  '�� �    '      7  '  5    7  �� "  8   ���  �@J�Effect#	 �  ��  � ��$	 0q���  � ��	 �	#FFF6F6F6��  � ��$	 4q��� "  8   .���  �@��TextWrapping$
	 Wrap=���  � ��$	 1q���  � ��
	 �������  � ��$	 Center=���  � ��$	 4,4,4,4q���  �@��
IsReadOnly$	 False����  � ��$	 14(���  � ��$	 1��
 P�  �@=�HasError$
 True��. ���  � ��$	 4,4,4,20q��� " �����  � ��$	 Center=���  � ��$
	 Wrap=���  � ��$	 5,0,0,2q���  � ��$	 14(�
 P�  � ��$ 1��. ���  � ��$	 Center=��� "  8   ���  � ��$	 2����  � ��$	 Bottom=���  � ��$	 2,0,2,0q�
 P�  � ��$ 0��. ���  �@ 
LineStroke
	 �   ��� "  8   ���  � ��$	 0,0,4,2q���  � ��$	 Center=���  � ��
	 �������  � ��$	 Center=���  � ��$	 1����  � ��$	 14(���  �@ TitlePlacement$
	 Left=�
 P�  �@=�HasError$
 True��. ���  � ��$	 4,4,4,20q��� " d�   *ToggleButtonSwitch+.���  � ��$	 5,0,0,0q���  � y�$	 Visible=���  � ��$
	 Left=��� " 9�8   .���  � ��$	 0,0,0,4q��� " ��.���  � ��$	 Center=���  � ��$	 Center=���  � {�$
	 True����  � ��$	 1���� "  8   .���  � ��$
	 Auto=��� "  8   .���  � ��$	 4,4,0,0q���  �@ TitlePlacement$		 Top=���  � ��$	 4q���  �@ AutoComplete$
	 True����  �@��
IsEditable$
	 True����  �@ ShowClearButton$	 False����  � ��$	 Center=���  � ��$
	 auto����  � ��$	 14(�
 P�  �@=�HasError$
 True��. ���  � ��$	 4,4,4,20q�.+��� $��0��$��2��$��2�� K� ZIndex$
 1000��$��Hidden=�$��Right=�$��0,0,-2,0q�.���� -    ��Name$ stkGrowl��$	��Top=�$��0,0,-2,0q�  EffectShadow1 J� Effect# [ �� $��1��$��Center=�$��Center=�$��30(�$
��Bold"�
���r���$y�Hidden=�� $��3��$��Right=� �� $��* ��� $��* �.+��� -    ��Name$ 	btnFinish��$��0�� � ToolTip$ Save Changes��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/save-icon.png���� -   $ 	btnCancel��$��1��$ Cancel Changes��$��0q�$��2q�$��5,0,5,0q�.���� $ ��/Resources/cancel-icon.png��  $��1��$��0��.����  �� CornerRadius$ 4,4,0,0��._�� -   $��grdVolunteerGeneral�� �� $
��auto ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��$��2��$��Center=�$��General��$
��Bold"��� $��1��$��First Name:���� $��2��$��
Last Name:���� $��3��$��Address Line 1:���� $��4��$��Address Line 2:���� $��5��$��City:���� $��6��$��State:���� $��7��$
��Zip:���� $��8��$��Start Date:��  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  $��6��  $��7��  $��8��  '	 	 !HandyControl.Controls.InfoElement 	  Placeholder$ David��$��1������ *	FirstName+  $ Burch��$��2������ *
LastName+  $ 1234 Houston Rd��$��3������ *
Address1+  $ Apt 456 (Optional)��$��4������ *
Address2+  $ Saginaw��$��5������ *City+  -     Name$ cmbState��$��6��$��1�� �� SelectedValuePath$ Abbreviations�� �� DisplayMemberPath$
 Name��$  -- Select State --�� �� SelectedValue �� *State+  -   $ 48604��$��7�� �� 	MaxLength$ 5������ *	ZipCode+  -     Name$ dprStartDate��$��8��$��1��$ 8/1/2020��$��4q�(
  "System.Windows.Controls.DatePicker 
  SelectedDate �� *	StartDate+  $��1��$��1��.���� $ 4,4,0,0��._�� -	   $ grdDemographics�� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��Personal��$��0��$��2�� �� TextAlignment$ Center=�$
��Bold"�  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  $��6��  $��7��  -
   $ dprDateOfBirth��$��1��$ 1/1/1960��   Title$ Date of Birth:��$��4q�
 �� Now 
  DisplayDateEnd# Z  �� *DateOfBirth+  -   $ 	cmbGender��$��2��$��0��$
 Tuid��$
 Name��$ Gender: ��$  -- Select Gender --�� �� *
GenderTuid+  -   $ cmbIdentifiesAs��$��3��$��0��$
 Tuid��$
 Name��$ Identifies As: ��$  -- Select Identity --�� �� *IdentifiesAsTuid+  -   $ cmbEthnicity��$��4��$��0��$
 Tuid��$
 Name��$ Ethnicity: ��$  -- Select Ethnicity --�� �� *EthnicityTuid+  -   $ cmbRacialGroup��$��5��$��0��$
 Tuid��$
 Name��$ Racial Group: ��$   -- Select Racial Group --�� �� *RacialGroupTuid+� $��6��$��0�� �� $
��auto ��� $��4* �.+��� -    ��Name$ 
lblVeteran��$��0��$��0��$y�Visible=�$��	Veteran: ��  �� Padding$  0q�$��0,4,4,4q�$��Center=�d� -   ! d�Name$! 
btnVeteran��$��0��$��1��" d� 	IsChecked" �� *	Veteran+� $��7��$��0�� �� $
��auto ��� $��* �.+��� -   $ lblFamilyofMilitary��$��0��$��0��$y�Visible=�$��Family of Military: ��$  0q�$��0,4,4,4q�$��Center=�d� -   $! btnFamilyOfMilitary��$��0��$��2��" �� *FamilyOfMilitary+  $��1��$��2��.���� $ 4,4,0,0��._�  .��� -   $��grdOneTimeChecks��  	 DatePicker.Small��%  '        7 	 �� "  8   ���  � ��$	 0,0,4,2q���  � ��$	 Center=���  � ��
	 �������  � ��$	 Center=���  � ��$	 Center=���  � ��$	 1����  � |�$
	 True����  �@J�IsHitTestVisible$
	 True����  � ��$	 14(� �� $��* ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��	Checklist��$��0��$��2��$ Center=�$
��Bold"��� $��File Photo: ��$��1��$��0���� $��Service Descr: ��$��2��$��0���� $��Orient. Training: ��$��3��$��0���� $��Confid SOU: ��$��4��$��0���� $��Service Start Date: ��$��5��$��0���� $��Background Check: ��$��6��$��0���� $��	ID Copy: ��$��7��$��0���� $#��Annual Income Car Insurance: ��$��8��$��0��  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  $��6��  $��7��  $��8���� $��1��" �� *	FilePhoto+�� $��2��" �� *ServiceDescription+�� $��3��" �� *OrientTraining+  -   $ dprConfidSOU��$��4�� �� *ConfidenceSOU+  -   $ dprServiceStartDate��$��5�� �� *ServiceStartDate+�� $��6��" �� *BackgroundCheck+�� $��7��" �� *IDCopy+  -   $ dprCarIncome��$��8�� �� *AnnualIncomeCarInsurance+