   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullgGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogicB_FGMS.BusinessLogic.Models  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI "   C_FGMS.UI.AddOrEditClassroom   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     XdataGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  Add New Classroom��$	��400��$	��350��.��� .+��� $��0��$��2��$��2�� K� ZIndex$
 1000��$��Visible=�$��Right=�$��0,0,-2,0q�.���� -    ��Name$ stkGrowl��$	��Top=�$��0,0,-2,0q�   EffectShadow1 J� Effect# [     HandyControl.Controls.Card  -     Name$ crdStudentInfo��$��1��$��1��$��4,4,6,4q�$��1q����	#FFF6F6F6
���   �.����  �� CornerRadius$ 4,4,0,0��._�� -    �Name$ grdStudentInfo�� � 	Resources #  HandyControl.Controls.TextBox �� 
TargetType  TextBoxExtend	 �� BasedOn
 �� Property �� Value  	Converter1   +B_FGMS.BusinessLogic.Models.ClassroomsModel  NewClassroom��%5  '        7  '�� �     &	 �     &	     �� "  8	   ��� 
 � ��$ 1q��� 
 �@��TextAlignment$ Center=��� 
 � ��$ Center=��� 
 � ��$ 4q��� 
 �@��TextWrapping$
 Wrap=��� " ����� 
 � ��$ Center=��� 
 � ��$
 Wrap=��� 
 � ��$ 4,0,0,2q���    � ColumnDefinitions �� $
��auto ��� $��* � � RowDefinitions �� $
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
��auto � � DataContext ��  �� Source# [ .+�$  HandyControl.Controls.ComboBox  -     Name$ CmbAddEditSelectVolunteer��$��0��$��2��   AutoComplete$
 True�� �� 
IsEditable$
 True��$
��auto��$
��auto��$��5,5,5,5q�   ShowClearButton$
 True�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$ FullName��'  !HandyControl.Controls.InfoElement   Placeholder$ Volunteer Name������ *Names+�� $��Room Number:��$��2��$��0��  -     Name$ txtRoomNumber��$��2��$��1������ *
RoomNumber+�� $��3��$��Class Size:��  -   $ txtClassSize��$��3��$��1������ *	ClassSize+�� $��4��$��Grade:��  -   $ txtGrade��$��4��$��1������ *Grade+�� $��5��$��Teacher Name:��  -   $ txtTeacherName��$��5��$��1������ *TeacherName+� -	   $��grdDatePickers��$��6��$��2�� �� $��* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� $��0��$��Center=�$��Days: ���� -
    ��Name$ 	chkMonday��$��M��$��1���� -   $ 
chkTuesday��$��T��$��2���� -   $ chkWednesday��$��W��$��3���� -   $ chkThursday��$��TR��$��4���� -   $ 	chkFriday��$��F��$��5��� $��7��$��0��$��2�� �� $��* ��� $��2* ��� $��* ��� $��2* �.+��� $��Start:��$	��5,0q�$��0��  -   $ txtStartTime��$��1��$	��5,0q�$	��Top=� �� 
IsReadOnly$
 True��&   HandyControl.Controls.TimePicker  -     Name$ tpStart��$��1��$	��5,0q�$y�Hidden=�   
TimeFormat$ h:mm tt���� $	��5,0q�$
��End:��$��2��  -   $ 
txtEndTime��$��3��$	��5,0q�$	��Top=�$
 True��  -   $ tpEnd��$��3��$	��5,0q�$	��Top=�$y�Hidden=�$ h:mm tt���� -    ��Name$ txtEditTime��$��8��$��0��$��
Edit Time?��d� -    d�Name$ tglEditTime��$��8��$��1��$
{�True��$
��Left=�$	��Top=���1  +HandyControl.Themes.StaticResourceExtension  *ToggleButtonSwitch+� $��8��$��Right=�$��1��$��
0,14,9,-44q� J� RenderTransformOrigin$ 0.478,-1.588&� �� $��* ��� $��* ��� $��* �.+��� -    ��Name$ 	btnFinish��$��12��$��1��  � ToolTip$  Save Changes��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/save-icon.png���� -   $ 	btnCancel��$��12��$��2��$  Cancel Changes��$��0q�$��2q�$��5,0,5,0q�.���� $ ��/Resources/cancel-icon.png��