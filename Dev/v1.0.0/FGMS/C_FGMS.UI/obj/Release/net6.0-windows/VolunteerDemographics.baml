   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI %   C_FGMS.UI.VolunteerDemographics   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  VolunteerDemographics�� N� 
Background
 �����.��� -   $��grdContainerDemographics�� � ColumnDefinitions �� $��* ��� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $��* � � 	Resources    HandyControl.Controls.Card �� 
TargetType   CardBaseStyle �� BasedOn �� Property  EffectShadow2 �� Value#  HandyControl.Controls.TextBox  	BaseStyle#  HandyControl.Controls.Divider  DividerBaseStyle	 �� Triggers
 P� Property P� Value&   HandyControl.Controls.DatePicker  DatePicker.Small$  HandyControl.Controls.ComboBox  ComboBoxPlusBaseStyle(  "HandyControl.Controls.TitleElement(  "HandyControl.Controls.ScrollViewer  ScrollViewerBaseStyle��%Q  '        7   '  �     7  '�� l    '  �    7  '  y    7  '  e    7  '  �    7  �� "  8   ���  �@J�Effect# �  ��  � ��$ 0q���  � �� �	#FFF6F6F6��  � ��$ 4q���  � ��$ 14(��� "  8   ���  � ��
 ���� ��  � ��$ 0q���  �@��TextAlignment$ Center=���  � ��$ Center=���  �@��
IsReadOnly$
 True����  �@��TextWrapping$
 Wrap=���  � ��$ 14(��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 5,0,0,2q���  � ��$ 14(��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q�	 P� 
 � ��$ 0��. ���  �@ 
LineStroke
 �   ��� "  8   ���  � ��$ 0,0,4,2q���  � ��$ Center=���  � ��
 ���� ��  � ��$ Center=���  � ��$ Center=���  � ��$ 1����  � |�$ False����  �@J�IsHitTestVisible$ False���� "  8   .���  �@ TitlePlacement$
 Left=���  � ��$ 4q���  �@ AutoComplete$
 True����  �@��
IsEditable$
 True����  �@ ShowClearButton$
 True����  � y�$ Hidden=���  � ��$ Center=���  � ��$
 auto����  � ��$ 14(�	 P� 
 �@=�HasError$
 True��. ���  � ��$ 4,4,4,20q��� "  8   .���  � ��$
 Auto=�.+�  -   $��cmbVolunteerName��$��0��$��0��   AutoComplete$
 True��$y�Visible=�$
��auto��$
��auto��$��10q� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$ FullName��   ShowClearButton$
 True��'  !HandyControl.Controls.InfoElement   Placeholder$ Volunteer Name��$��Center=����� *VolunteerList+ �� SelectedItem �� *SelectedVolunteer+ �� SelectedValue �� *VolunteerTuid+ �� Mode$ TwoWay=��� -    ��Name$ txtEditMode��$��0��$��1��$��	Edit Mode��$��Center=�$��Center=�$��30(�$
��Bold"�
���r���$y�Hidden=�� $��0��$��3��$��Right=�$	��Top=�$��0,10,0,0q� �� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnEdit��$��2��$��0q�$��0q�$��5,0,5,0q� � ToolTip$
 Edit��.���� $��/Resources/edit-icon.png���� -   $ 	btnFinish��$��1��$y�Hidden=�$��0q�$��0q�$��5,0,5,0q�$
 Save��.���� $��/Resources/save-icon.png���� -   $ 	btnCancel��$��2��$y�Hidden=�$��0q�$��2q�$��5,0,5,0q�$ Cancel��.���� $ ��/Resources/cancel-icon.png���� -   $ 
btnRefresh��$��3��$��0q�$��0q�$��5,0,5,0q�$ Refresh Data��.���� $!��/Resources/refresh-icon.png��  $��1��$��0��$
��Auto=�.����  �� CornerRadius$ 4,4,0,0��._�  .��� -   $(��"grdVolunteerDemographicInformation��   TextBoxExtend1	  +HandyControl.Themes.StaticResourceExtension��%.  '        7  'd� ;    '  �    '�� �    �� "  8   .���  �@��TextWrapping$
 Wrap=���  � ��$ 4,4,0,0q���  �@ TitlePlacement$
 Left=���  �@ VerticalAlignment$ Stretch=���  � ��$ 1q���  � ��
 ���� ��  � ��$ Center=���  � ��$ 4q���  �@��
IsReadOnly$
 True����  � ��$ 14(��� " d� 	  *ToggleButtonSwitch+.���  � ��$ 9,0,0,0q���  � y�$ Hidden=���  � ��$
 Left=��� "   	  *DatePickerExtend+.���  � ��$ 4,4,0,0q���  � ��$ 4q���  � ��
 ���� ��  �@J�IsHitTestVisible$ False����  �@ TitlePlacement$
 Left=���  � ��$ 14(��� " ��.���  � ��$
 Wrap=���  �@��Padding$ 4,4,0,0q���  � ��$ 4q���  � ��$ 14(� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��Personal��$��0��$��2�� �� TextAlignment$ Center=�$
��Bold"�  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  $��6��  $��7��  $��8��  $��9��  $��1��$��0��   Title$ Last Updated:��$ 1/1/2020������ *LastUpdated+ �� StringFormat$ g��  -	     Name$ dprDateOfBirth��$��2��$��0��$ Date of Birth: ��$ 1/1/1960��(
  "System.Windows.Controls.DatePicker 
  Text �� *DateOfBirth+$ TwoWay=�  $��3��$��0��$ Age: ��$ 60������ *Age+$ TwoWay=�  -
     Name$ 	txtGender��$��4��$��0��$ Gender: ��$
 Male������ *
GenderName+  -     Name$ 	cmbGender��$��4��$��0��$
 Tuid��$
 Name��$ Gender: ��$  -- Select Gender --������ *
GenderList+ �� *
GenderTuid+$ TwoWay=� �� Text �� *
GenderName+$ OneWayToSource=�  -   $ txtIdentifiesAs��$��5��$��0��$ Identifies As: ��$
 Male������ *IdentityName+  -   $ cmbIdentifiesAs��$��5��$��0��$
 Tuid��$
 Name��$ Identifies As: ��$  -- Select Identity --������ *IdentityList+ �� *IdentityTuid+$ TwoWay=� �� *IdentityName+$ OneWayToSource=�  -   $ txtEthnicity��$��6��$��0��$ Ethnicity: ��$ Hispanic������ *EthnicityName+  -   $ cmbEthnicity��$��6��$��0��$
 Tuid��$
 Name��$ Ethnicity: ��$  -- Select Ethnicity --������ *EthnicityList+ �� *EthnicityTuid+$ TwoWay=� �� *EthnicityName+$ OneWayToSource=�  -   $ txtRacialGroup��$��7��$��0��$ Racial Group: ��$ White������ *
RacialName+  -   $ cmbRacialGroup��$��7��$��0��$
 Tuid��$
 Name��$ Racial Group: ��$   -- Select Racial Group --������ *
RacialList+ �� *
RacialTuid+$ TwoWay=� �� *
RacialName+$ OneWayToSource=�� $��8��$��0�� �� $
��auto ��� $��4* �.+�  -   $ 
txtVeteran��$��0��$��0��$��2��$ 	Veteran: ��$	 Yes������ *	Veteran+�� -   $ 
lblVeteran��$��0��$��0��$y�Hidden=�$��	Veteran: ��  �� Padding$  0q�$��0,4,4,4q�$��Center=�d� -   ! d�Name$! 
btnVeteran��$��0��$��1��" d� 	IsChecked" �� *	IsVeteran+$ TwoWay=�� $��9��$��0�� �� $
��auto ��� $��* �.+�  -   $ txtFamilyOfMilitary��$��0��$��0��$��2��$ Family of Military: ��$	 Yes������ *FamilyOfMilitary+�� -   $ lblFamilyofMilitary��$��0��$��0��$y�Hidden=�$��Family of Military: ��$  0q�$��0,4,4,4q�$��Center=�d� -   $! btnFamilyOfMilitary��$��0��$��2��" �� *IsFamilyOfMilitary+$ TwoWay=�  $��1��$��1��.���� $ 4,4,0,0��._�� -   $'��!grdVolunteerDemographicStatusInfo�� ��%�  '        7  '      7  'd� �    '  %    '�� �    �� "  8   .���  � ��$ 4,4,0,0q���  �@ TitlePlacement$
 Left=���  �@ VerticalAlignment$ Stretch=���  � ��$ 1q���  � ��
 ���� ��  � ��$ Center=���  � ��$ 4q���  �@��
IsReadOnly$
 True����  � ��$ 14(��� "  8   .���  � ��$ 4,4,0,0q���  �@ TitlePlacement$
 Left=���  � ��$ 4q���  �@ AutoComplete$
 True����  �@��
IsEditable$
 True����  �@ ShowClearButton$
 True����  � y�$ Hidden=���  � ��$ Center=���  � ��$
 auto����  � ��$ 14(�	 P� 
 �@=�HasError$
 True��. ���  � ��$ 4,4,4,20q��� " d� 	  *ToggleButtonSwitch+.���  � ��$ 9,0,0,0q���  � y�$ Hidden=���  � ��$
 Left=���  � ��$ 14(��� "   	  *DatePickerExtend+.���  � ��$ 4,4,0,0q���  � ��$ 4q���  � ��
 ���� ��  �@J�IsHitTestVisible$ False����  �@ TitlePlacement$
 Left=���  � ��$ 14(��� " ��.���  �@��Padding$ 4,4,0,0q���  � ��$ 4q���  � ��$ 14(� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��Status��$��0��$ Center=�$
��Bold"�  $��0��  $��1��  $��2��  $��3��� $��1��$��0�� �� $
��auto ��� $��* �.+�  -   $ 	txtStatus��$��0��$��0��$��2��$ Status: ��$ Active������ *Active+�� -   $ 	lblStatus��$��0��$��0��$y�Hidden=�$��Status: ��$  0q�$��0,4,4,4q�$��Center=�d� -   $! 	btnStatus��$��0��$��1��" �� *
IsActive+$ TwoWay=�  -   $ dprSeparationDate��$��2��$��0��$ Separation Date: ��$	 N/A��# 
  SelectedDate# �� *SeparationDate+$ TwoWay=�  -   $ txtReasonsSeparated��$��3��$��0��$ Reason: ��$	 N/A������ *ReasonSeparated+  -   $ cmbReasonsSeparated��$��3��$��0��$
 Tuid��$
 Name��$ �� 
IsReadOnly$
$ True��$ Reason: ��$$  -- Select Reason Separated --������ *ReasonsList+ �� *SeparationTuid+ �� *ReasonSeparated+$ OneWayToSource=�9� -   % 9�Name$% 
expVolName��& 9� 
IsExpanded$& False��$��4��$y�Visible=�' J� IsHitTestVisible$' False������ $��Update Name��$��14(�$
��Bold"�.���  �� $��* � �� $
��auto ��� $
��auto �.+�  $��0��$ First Name:��
�������( �� 
IsReadOnly$( False������ *	FirstName+  $��1��$ 
Last Name:��
�������$( False������ *
LastName+