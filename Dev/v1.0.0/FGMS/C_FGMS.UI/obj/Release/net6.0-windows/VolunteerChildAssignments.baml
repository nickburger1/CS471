   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI )   #C_FGMS.UI.VolunteerChildAssignments   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  VolunteerChildAssignments��.��� 
������� � ColumnDefinitions �� $
��auto ��� $
��175* ��� $
��309* ��� $
��262* � � RowDefinitions �� $
��auto ��� $��* �.+�$  HandyControl.Controls.ComboBox  -     Name$ CmbSelectVolunteer��$��0��$��0��   AutoComplete$
 True�� �� 
IsEditable$
 True��$
��auto��$
��auto��$��10,10,10,10q�   ShowClearButton$
 True�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$ FullName��'  !HandyControl.Controls.InfoElement	   Placeholder$	 Volunteer Name������ *Names+  -   $ CmbSelectRoom��$��0��$��1��$
 True��$
 True��$
��auto��$
��auto��$��10,10,10,10q�$
 True��$ ClassroomNumber��$ ClassroomNumber��$	 Room Number������ *
Classrooms+�� -   
 ��Name$
 txtEditMode��$��2��$��	Edit Mode��$��Center=�$��Center=�$��30(�$
��Bold"�
���r���$y�Hidden=�$��40��$	��144��� $��3��$��Right=� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnAdd��$��1��$��0q�$��0q�$��5,0,5,0q� � ToolTip$ Add New Child Assignment��.���� $��/Resources/add-icon.png���� -   $ btnEdit��$ Edit Child Assignment��$��2��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/edit-icon.png���� -   $ 	btnDelete��$ Delete Child Assignment��$��3��$��0q�$��0q�$��5,0,5,0q�.���� $ ��/Resources/delete-icon.png���� -   $ 	btnExport��$��4��$��0q�$��0q�$��5,0,5,0q�$&  Export Child Assignment to Excel��.���� $!��\Resources\reports-icon.png���� -   $ 
btnRefresh��$��5��$��0q�$��0q�$��5,0,5,0q�$ Refresh Data��.���� $!��\Resources\refresh-icon.png��   HandyControl.Controls.Card  $��1��$��0��$��0q�$��4,4,4,4q�.����  �� CornerRadius$ 4,4,0,0��._�� -	    �Name$ grdMain��$��Center=� � 	Resources #  HandyControl.Controls.TextBox �� 
TargetType   TextBoxExtend �� BasedOn(  "HandyControl.Controls.TitleElement �� Property �� Value  ComboBoxExtend  CenteredToolTipJ   DB_FGMS.BusinessLogic.Models.Volunteer.VolunteerChildAssignmentsModel  VolunteerChildAssignments��%�  '        7   '  �     7  &	 m    &	 �    �� "  8   .���  �@ TitlePlacement$	 Top=���  � ��$ 1q���  � ��
 ���� ��  �@��
IsReadOnly$
 True����  � ��$ 14(��� "  8   .���  �@ TitlePlacement$	 Top=���  � ��$ 1q���  � ��
 �   ���  � ��
 ���� ��  �@��
IsReadOnly$
 True����  � {�$ False����  � ��$ 14(��� " _�.���  � ��$ Center=���  � ��$ Center=�   �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � � DataContext ��  �� Source# [ .+�  -
     Name$ 	txtSchool��$	 Arthur Eddy��$��0��   Title$ School:������  �� Path$ School.Name� �� Mode$ TwoWay=�  -   $ txtClassroomSize��$	 25��$��1��$ Classroom Size:������ $ ClassroomSize�$ TwoWay=�  -   $ txtKidsAssigned��$	 4��$��2��$ Kids Assigned:������ $ KidsAssigned�$ TwoWay=�  -   $ txtGradeLevel��$		 1st��$��3��$ Grade Level:������ $ 
GradeLevel�$ TwoWay=�  -   $ 
txtAge0to5��$	 0��$��4��$ Age 0 to 5:������ $ Age0To5�$ TwoWay=�  -   $ txtAge6to12��$	 4��$��5��$ Age 6 to 12:������ $ Age6To12�$ TwoWay=�&   System.Windows.Controls.DataGrid  -   $��
dtgStudent��   
IsReadOnly$
 True��$��10,10,10,10q�   AutoGenerateColumns$ False��$��1��$��1��$��3��$��Center=� �� AlternationCount$ 2������ *childAssignmentsGrid+   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��
 �r�����  � ��$ Center=���  �@�ShowGridLines$
 True��   RowStyle �� )	  #System.Windows.Controls.DataGridRow" 	   DataGridRowStyle# [ .���  � ��
 ����� �� Triggers P�   P� Property  �@	 AlternationIndex! P� Value$! 1��. ���  � ��
 �����P�   �@	 
IsSelected$
! True��. ���  � ��
 �r���"   	CellStyle" �� *
  $System.Windows.Controls.DataGridCell" 
 .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q� Q   KB_FGMS.BusinessLogic.Models.Volunteer.VolunteerChildAssignmentDataGridModel  VolunteerGeneralDataGrid��%   &	          �� # [ #   Columns# 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn$   Header$$ 
Identifier��%   Width5  /System.Windows.Controls.DataGridLengthConverter$% * 1  +System.Windows.Controls.DataGridBoundColumn&   Binding& �� $ 
Identifier�  $$ 
Conditions��$% * & �� $ 	Condition�'   ElementStyle' �� " ��.���  �@^�ToolTip �� *ConditionDescription+  $$ Student Needs��$% * & �� $ StudentNeeds�' �� " ��.���  �@^�ToolTip �� *StudentNeedsDescription+  $$ Desired Outcome��$% * & �� $ DesiredOutcome�