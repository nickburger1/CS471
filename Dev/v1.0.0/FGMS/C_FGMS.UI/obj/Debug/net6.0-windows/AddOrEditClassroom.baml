   M S B A M L   `   `   `  ����O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullgGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogicB_FGMS.BusinessLogic.Models  5   	   D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI 5   	   "   C_FGMS.UI.AddOrEditClassroom   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   XdataGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogic     5   	   )localclr-namespace:C_FGMS.UI     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5	   	     � Title$  Add New Classroom��5
   	   $	��400��6#   $	��350��60   .��5      � .+�5   
   �� $��0��6   $��2��6$   $��2��64    K� ZIndex$
 1000��6E   $��Visible=�5      $��Right=�6=   $��0,0,-2,0q�6Y   .��5      �� -   5         ��Name$ stkGrowl��5   1   $	��Top=�6   $��0,0,-2,0q�6C      EffectShadow1 J� Effect# [  6U   5         HandyControl.Controls.Card  5   
   -   5          Name$ crdStudentInfo��5   =   $��1��5      $��1��6"   $��4,4,6,4q�6/   $��1q�6@   ���	#FFF6F6F6
���   �.��5      ��  �� CornerRadius$ 4,4,0,0��6   ._�5      � -   5         �Name$ grdStudentInfo��5       � 	Resources 5      #  HandyControl.Controls.TextBox �� 
TargetType  TextBoxExtend	 �� BasedOn
 �� Property �� Value  	Converter1   +B_FGMS.BusinessLogic.Models.ClassroomsModel  NewClassroom��%  '        7  '�� 
    &	 �    &	 �    �� 5      "  6    8	   �5      �� 5   "   
 � ��$ 1q�6D   �� 5   "   
 �@��TextAlignment$ Center=�6B   �� 5   "   
 � ��$ Center=�6F   �� 5   "   
 � ��$ 4q�6;   �� 5   "   
 �@��TextWrapping$
 Wrap=�6A   5       �� 5!      " ��6    �5"      �� 5#   "   
 � ��$ Center=�6F   �� 5$   "   
 � ��$
 Wrap=�6A   �� 5%   "   
 � ��$ 4,0,0,2q�6;   5'      �� 5(      67     5)      6/   5*       � ColumnDefinitions 5+      �� 5,      $
��auto �6+   �� 5-      $��* �6+    � RowDefinitions 5/      �� 50      $
��auto �6(   �� 51      $
��auto �6(   �� 52      $
��auto �6(   �� 53      $
��auto �6(   �� 54      $
��auto �6(   �� 55      $
��auto �6(   �� 56      $
��auto �6(   �� 57      $
��auto �6(   �� 58      $
��auto �6(   �� 59      $
��auto �6(   �� 5:      $
��auto �6(   �� 5;      $
��auto �6(    � DataContext 5>      �� 5?       �� Source# [ 6"   .+�5F      $  HandyControl.Controls.ComboBox  -   5          Name$ CmbAddEditSelectVolunteer��5K   "   $��0��5F   "   $��2��6/      AutoComplete$
 True��6C    �� 
IsEditable$
 True��6W   $
��auto��5G   "   $
��auto��6/   $��5,5,5,5q�6=      ShowClearButton$
 True��6N    �� SelectedValuePath$
 Tuid��5I   "    �� DisplayMemberPath$ FullName��5J   "   '  !HandyControl.Controls.InfoElement   Placeholder$ Volunteer Name��5L   "   ��5H   "   �� *Names+5L   N   �� 5P      $��Room Number:��6    $��2��64   $��0��6A     5Q      -   5          Name$ txtRoomNumber��5Q   >   $��2��6!   $��1��6.   ��6U   �� *
RoomNumber+�� 5R      $��3��6    $��Class Size:��6-     5S      -   5        $ txtClassSize��5S   >   $��3��6!   $��1��6.   ��6T   �� *	ClassSize+�� 5T      $��4��6    $��Grade:��6-     5U      -   5        $ txtGrade��5U   >   $��4��6!   $��1��6.   ��6P   �� *Grade+�� 5V      $��5��6    $��Teacher Name:��6-     5W      -   5        $ txtTeacherName��5W   >   $��5��6!   $��1��6.   ��6V   �� *TeacherName+� 5Z      -	   5        $��grdDatePickers��5Z      $��6��61   $��2��6>    5[      �� 5\      $��* �6/   �� 5]      $��1* �6/   �� 5^      $��1* �6/   �� 5_      $��1* �6/   �� 5`      $��1* �6/   �� 5a      $��1* �6/   .+�5d      �� $��0��6$   $��Center=�64   $��Days: ��6Q   �� 5e      -
   5         ��Name$ 	chkMonday��5e   #   $��M��66   $��1��6B   �� 5f      -   5        $ 
chkTuesday��5f   #   $��T��67   $��2��6C   �� 5g      -   5        $ chkWednesday��5g   #   $��W��69   $��3��6E   �� 5h      -   5        $ chkThursday��5h   #   $��TR��68   $��4��6E   �� 5i      -   5        $ 	chkFriday��5i   #   $��F��66   $��5��6B   5j      � 5l      $��7��6   $��0��6(   $��2��68    5m      �� 5n      $��* �6/   �� 5o      $��2* �6/   �� 5p      $��* �6/   �� 5q      $��2* �6/   .+�5t      �� $��Start:��6$   $	��5,0q�62   $��0��6?     5u      -   5        $ txtStartTime��5u   Z   $��1��6%   $	��5,0q�65   $	��Top=�6B    �� 
IsReadOnly$
 True��6p   &   HandyControl.Controls.TimePicker  5v      -   5          Name$ tpStart��5v   E   $��1��6(   $	��5,0q�68   $y�Hidden=�6W      
TimeFormat$ h:mm tt��6l   �� 5w      $	��5,0q�6$   $
��End:��61   $��2��6=     5x      -   5        $ 
txtEndTime��5x   Z   $��3��6%   $	��5,0q�65   $	��Top=�6B   $
 True��6n     5y      -   5        $ tpEnd��5y   q   $��3��6(   $	��5,0q�68   $	��Top=�6E   $y�Hidden=�6]   $ h:mm tt��6�   5z      �� 5}      -   5         ��Name$ txtEditTime��5}   O   $��8��6    $��0��6-   $��
Edit Time?��6=   6O   d� 5~      -   5         d�Name$ tglEditTime��5~   �   $��8��6#   $��1��60   $
{�True��6p   $
��Left=�6�   $	��Top=�6�   ��6@   1  +HandyControl.Themes.StaticResourceExtension  *ToggleButtonSwitch+6�   � 5�      $��8��6   $��Right=�6(   $��1��6D   $��
0,14,9,-44q�6T    J� RenderTransformOrigin$ 0.478,-1.588&�6h    5�      �� 5�      $��* �6/   �� 5�      $��* �6/   �� 5�      $��* �6/   .+�5�      �� -   5         ��Name$ 	btnFinish��5�   !   $��12��5�      $��1��6'     � ToolTip$  Save Changes��67   $��0q�5�      $��0q�6.   $��5,0,5,0q�6:   .��5�      �� $��/Resources/save-icon.png��6$   6H   5�      �� 5�      -   5        $ 	btnCancel��5�   !   $��12��5�      $��2��6'   $  Cancel Changes��67   $��0q�5�      $��2q�6-   $��5,0,5,0q�69   .��5�      �� $ ��/Resources/cancel-icon.png��6$   6J   5�      5�      5�      5�      5�      5�      5�      