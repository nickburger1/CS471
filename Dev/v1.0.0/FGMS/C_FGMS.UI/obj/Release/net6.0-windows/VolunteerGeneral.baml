   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters     C_FGMS.UI.VolunteerGeneral   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  /local1"clr-namespace:C_FGMS.UI.Converters    N� Title$  VolunteerGeneral�� � 	Resources 1  +C_FGMS.UI.Converters.StringToPhoneConverter   StringToPhoneConverter��%   &	          .��� -    �Name$ grdMain��
������� � ColumnDefinitions �� $��1.15* ��� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $
��7.5* �    HandyControl.Controls.Card �� 
TargetType  CardBaseStyle �� BasedOn �� Property  EffectShadow2 �� Value#  HandyControl.Controls.TextBox  	BaseStyle	 �� Triggers
 P� Property P� Value#  HandyControl.Controls.Divider  DividerBaseStyle&   HandyControl.Controls.DatePicker  DatePicker.Small1  +HandyControl.Themes.StaticResourceExtension  ExpanderBaseStyle(  "HandyControl.Controls.ScrollViewer  ScrollViewerBaseStyle$  HandyControl.Controls.ComboBox  ComboBoxPlusBaseStyle(	  "HandyControl.Controls.TitleElement��%�  '        7  '  �     7  '�� �    '  W    7  '  �    7  'd� �    '9� o    7  '�� �    '      7  '  5    7  �� "  8   ���  �@J�Effect# �  ��  � ��$ 0q���  � �� �	#FFF6F6F6��  � ��$ 4q���  � ��$ 14(��� "  8   ���  � ��
 ���� ��  � ��$ 0q���  �@��TextAlignment$ Center=���  � ��$ Center=���  �@��
IsReadOnly$
 True����  �@��TextWrapping$
 Wrap=���  � ��$ 14(�	 P� 
 �@=�HasError$
 True��. ���  � ��$ 4,4,4,20q��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 5,0,0,2q���  � ��$ 14(�	 P� 
 � ��$ 1��. ���  � ��$ Center=��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q�	 P� 
 � ��$ 0��. ���  �@ 
LineStroke
 �   ��� "  8   ���  � ��$ 0,0,4,2q���  � ��$ Center=���  � ��
 ���� ��  � ��$ Center=���  � ��$ Center=���  � ��$ 1����  � |�$ False����  �@J�IsHitTestVisible$ False����  � ��$ 14(��� " d�   *ToggleButtonSwitch+.���  � ��$ 5,0,0,0q���  � y�$ Hidden=���  � ��$
 Left=��� " 9�8   .���  � ��$ 0,0,0,4q��� " ��.���  � ��$ Center=���  � ��$ Center=���  � {�$ False����  � ��$ 1���� "  8   .���  � ��$
 Auto=��� "  8   .���  �@	 TitlePlacement$
 Left=���  � ��$ 4q���  �@ AutoComplete$
 True����  �@��
IsEditable$
 True����  �@ ShowClearButton$
 True����  � ��$ Center=���  � ��$
 auto����  � ��$ 14(�	 P� 
 �@=�HasError$
 True��. ���  � ��$ 4,4,4,20q�.+�  -   $��cmbSelectVolunteer��$��0��$��0��   AutoComplete$
 True�� �� 
IsEditable$
 True��$
��auto��$
��auto��$��10q�   ShowClearButton$
 True�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$ FullName��'
  !HandyControl.Controls.InfoElement 
  Placeholder$ Volunteer Name������ *VolunteerList+ �� SelectedValue �� *VolunteerTuid+� $��0��$��1�� �� $
��auto ��� $��* �.+��� $��Select Year:��  -   $��cmbSelectYear��$��0��$��1��$��10q�$	��100��$ False��$
��Left=� �� *Year+���� *
YearList+�� -    ��Name$ txtEditState��$��1��$��	Edit Mode��$��Center=�$��Center=�$��30(�$
��Bold"�
���r���$y�Hidden=�$��2��� $��2��$��Right=� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnAdd��$��0��$��0q�$��0q�$��5,0,5,0q� � ToolTip$ Add a New Volunteer��.���� $��/Resources/add-icon.png���� -   $ 	btnFinish��$��1��$y�	Collapsed=�$ Save Changes��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/save-icon.png���� -   $ 	btnCancel��$��2��$y�Hidden=�$ Cancel Changes��$��0q�$��2q�$��5,0,5,0q�.���� $ ��/Resources/cancel-icon.png���� -   $ btnEdit��$ Edit Volunteer��$��2��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/edit-icon.png���� -	   $ 	btnDelete��$ Delete Volunteer��$��3��$��0q�$��0q�$��5,0,5,0q�.���� $ ��/Resources/delete-icon.png���� -
   $ 
btnRefresh��$��4��$��0q�$��0q�$��5,0,5,0q�$ Refresh Data��.���� $!��/Resources/refresh-icon.png��  $��1��$��0��.����  �� CornerRadius$ 4,4,0,0��._�  .��� -   $��grdVolunteerGeneral��  	 TextBoxExtend��%3  '        7 	 �� "  8   .���  �@��TextWrapping$
 Wrap=���  � ��$ 1q���  � ��
 ���� ��  � ��$ Center=���  � ��$ 4q���  �@��
IsReadOnly$
 True����  � ��$ 14(���  � ��$ 1��	 P� 
 �@=�HasError$
 True��. ���  � ��$ 4,4,4,20q� �� $
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
��auto �.+��� $��0��$��School:���� $��1��$��Phone:���� $��2��$��Address Line 1:���� $��3��$��Line 2:���� $��4��$��City, State:���� $��5��$
��Zip:���� $��6��$��Email:���� $��7��$��
Alt Phone:���� $��8��$��Active:���� $��9��$��Start Date:���� $��10��$��	End Date:��  -     Name$ 	txtSchool��$ School Name��$��0������ *
SchoolName+  -     Name$ cmbSchoolList��$��1��$
 True��$
 True��$
 True��$
 Tuid��$
 Name��$ School Name��$y�Hidden=����� *
SchoolList+ �� Text �� *
SchoolName+ �� Mode$ OneWayToSource=� �� *
SchoolTuid+$ TwoWay=�  -   $ txtPhone��$ 989-916-9898��$��1�� �� 	MaxLength$ 14������ *Phone+$ TwoWay=� �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=� �� StringFormat$ {0:(###) ###-###}�� �� 	Converter# [    $ 714 Birch Street��$��2��$ 45������ *
Address1+$ TwoWay=�  $ Apt 123 (Optional)��$��3��$ 45������ *
Address2+$ TwoWay=�� $��4��$��1�� �� $��* ��� $
��auto � �� $��* ��� $��* �.+�  -   $ txtCity��$ Saginaw��$��0��$ 45������ *City+$ TwoWay=�  -   $ txtState��$ MI��$��1������ *State+$ TwoWay=�  -   $ cmbState��$��0��$��2��$ Abbreviations��$
 Name��$y�Hidden=�$  State �� �� *State+$ TwoWay=�  -   $ 48604��$��5��$ 5������ *	ZipCode+$ TwoWay=�  $ email@gmail.com��$��6��$ 45������ *Email+$ TwoWay=�  -   $	 N/A��$��7��$ 14������ *
AltPhone+$ TwoWay=�$ PropertyChanged=�$ {0:(###) ###-###}��# [    -   $ 	txtActive��$	 Yes��$��8������ *Active+d� -     d�Name$  	btnStatus��$��8��$��1��! d� 	IsChecked! �� *
IsActive+$ TwoWay=����� *Active+  -   "  Name$" dprStartDate��$��9��$��4q�(  "System.Windows.Controls.DatePicker#   SelectedDate# �� *	StartDate+$ TwoWay=�  -   $" 
dprEndDate��$��10��$��4q�# �� *	EndDate+$ TwoWay=�  $��1��$��1��.���� $ 4,4,0,0��._�  .��� -   $��grdOneTimeChecks�� �� $��* ��� $��* � �� $
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
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��One-Time Checks��$��0��$��2��$ �� TextAlignment$$ Center=�$
��Bold"��� $��File Photo: ��$��1��$��0���� $��Service Descr: ��$��2��$��0���� $��Orient. Training: ��$��3��$��0���� $��Confid SOU: ��$��4��$��0���� $��Service Start Date: ��$��5��$��0���� $��NSCHC Check Form: ��$��6��$��0���� $��Background Check: ��$��7��$��0���� $��	ID Copy: ��$��8��$��0���� $��NSOPW: ��$��9��$��0���� $��I-Chat: ��$��10��$��0���� $��True-Screen: ��$��11��$��0���� $��Alias F'Print: ��$��12��$��0���� $��Field-Print Cleared: ��$��13��$��0���� $��DHS: ��$��14��$��0���� $��	TB Shot: ��$��15��$��0��  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  $��6��  $��7��  $��8��  $��9��  $��10��  $��11��  $��12��  $��13��  $��14���� $��1��! �� *	FilePhoto+$ TwoWay=��� $��2��! �� *ServiceDescription+$ TwoWay=��� $��3��! �� *OrientTraining+$ TwoWay=�  $��4��# �� *ConfidenceSOU+$ TwoWay=�  $��5��# �� *ServiceStartDate+$ TwoWay=��� $��6��! �� *NSCHCCheckForm+$ TwoWay=��� $��7��! �� *BackgroundCheck+$ TwoWay=��� $��8��! �� *IDCopy+$ TwoWay=�  $��9��# �� *NSOPW+$ TwoWay=�  $��10��# �� *IChat+$ TwoWay=�  $��11��# �� *
TrueScreen+$ TwoWay=�  $��12��# �� *AliasFieldPrint+$ TwoWay=�  $��13��# �� *
FieldPrint+$ TwoWay=�  $��14��# �� *DHS+$ TwoWay=�  $��15��# �� *TBShot+$ TwoWay=�  $��1��$��2��.���� $ 4,4,0,0��._��  �� $
��auto ��� $
��auto �.+�9� % 9� 
IsExpanded$
% True��$��0������ $��Annual Checks��$��14(�$
��Bold"�.��� -   $��grdAnnualChecks�� �� $��* ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $ ��Schedule & Photo Release: ��$��1��$��0���� $"��Emergency Beneficiary Form: ��$��2��$��0���� $��Hippa Release: ��$��3��$��0���� $��
Physical: ��$��4��$��0���� $#��Annual Income Car Insurance: ��$��5��$��0��  $��1��  $��2��  $��3��  $��4��  $��5���� $��1��$��1���� $��2��$��1���� $��3��$��1���� $��4��$��1���� $��5��$��1��  $��1��# �� *SchedulePhotoRelease+$ TwoWay=�  $��2��# �� *EmergencyBeneficiary+$ TwoWay=�  $��3��# �� *HippaRelease+$ TwoWay=�  $��4��# �� *
Physical+$ TwoWay=�  $��5��# �� *AnnualIncomeCarInsurance+$ TwoWay=�9� $��1��$
% True������ $��Temporary Information��$��14(�$
��Bold"�.��� -   $��grdTempInfo�� �� $��* ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �