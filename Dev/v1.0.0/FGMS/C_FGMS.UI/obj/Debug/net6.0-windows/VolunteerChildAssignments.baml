   M S B A M L   `   `   `  ����O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  5      D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI 5      )   #C_FGMS.UI.VolunteerChildAssignments   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5	        N� Title$  VolunteerChildAssignments��5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��175* �6   �� 5      $
��309* �6   �� 5      $
��262* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   $  HandyControl.Controls.ComboBox  -   5          Name$ CmbSelectVolunteer��5!   "   $��0��5      $��0��6#      AutoComplete$
 True��63    �� 
IsEditable$
 True��6G   $
��auto��5   "   $
��auto��6/   $��10,10,10,10q�6=      ShowClearButton$
 True��6R    �� SelectedValuePath$
 Tuid��5   "    �� DisplayMemberPath$ FullName��5    "   '  !HandyControl.Controls.InfoElement	   Placeholder$	 Volunteer Name��5"   "   ��5   "   �� *Names+5#   "     5%   
   -   5        $ CmbSelectRoom��5*   "   $��0��5%      $��1��6#   $
 True��63   $
 True��6G   $
��auto��5&   "   $
��auto��6/   $��10,10,10,10q�6=   $
 True��6R   $ ClassroomNumber��5(   "   $ DisplayName��5)   "   $	 Room Number��5+   "   ��5'   "   �� *
Classrooms+5+   K   �� 5.   
   -   5        
 ��Name$
 txtEditMode��5.      $��2��6)   $��	Edit Mode��69   $��Center=�5/      $��Center=�6/   $��30(�50      $
��Bold"�6"   
���r���$y�Hidden=�51      $��40��6(   $	��144��64   � 55   
   $��3��6   $��Right=�6    56      �� 57      $��* �6#   �� 58      $��* �6#   �� 59      $��* �6#   �� 5:      $��* �6#   �� 5;      $��* �6#   �� 5<      $��* �6#   .+�5?      �� -   5         ��Name$ btnAdd��5?      $��1��5@      $��0q�5A      $��0q�6%   $��5,0,5,0q�61    � ToolTip$ Add New Child Assignment��5B      .��5C      �� $��/Resources/add-icon.png��6   6;   5D      �� 5E      -   5        $ btnEdit��5E      $ Edit Child Assignment��6<   $��2��5F      $��0q�6!   $��0q�65   $��5,0,5,0q�6A   .��5G      �� $��/Resources/edit-icon.png��6   5H      �� 5I      -   5        $ 	btnDelete��5I      $ Delete Child Assignment��6@   $��3��5J      $��0q�6!   $��0q�65   $��5,0,5,0q�6A   .��5K      �� $ ��/Resources/delete-icon.png��6   5L      �� 5M      -   5        $ 	btnExport��5M      $��4��5N      $��0q�5O      $��0q�6%   $��5,0,5,0q�61   $&  Export Child Assignment to Excel��5P      .��5Q      �� $!��\Resources\reports-icon.png��6   5R      �� 5S      -   5        $ 
btnRefresh��5S      $��5��5T      $��0q�5U      $��0q�6%   $��5,0,5,0q�61   $ Refresh Data��5V      .��5W      �� $!��\Resources\refresh-icon.png��6   5X      5Y         HandyControl.Controls.Card  5\   
   $��1��6   $��0��6   $��0q�6/   $��4,4,4,4q�6C   .��5]      ��  �� CornerRadius$ 4,4,0,0��6   ._�5^      � -	   5         �Name$ grdMain��5^   2   $��Center=�6    � 	Resources 5_      #  HandyControl.Controls.TextBox �� 
TargetType   TextBoxExtend �� BasedOn(  "HandyControl.Controls.TitleElement �� Property �� Value  ComboBoxExtend  CenteredToolTipJ   DB_FGMS.BusinessLogic.Models.Volunteer.VolunteerChildAssignmentsModel  VolunteerChildAssignments��%�  '        7   '  �     7  &	 C    &	 �    �� 5`      "  6    8   .�5a      ��  �@ TitlePlacement$	 Top=�6O   �� 5b       � ��$ 1q�6@   �� 5c       � ��
 ���� 6;   �� 5d       �@��
IsReadOnly$
 True��6;   �� 5e       � ��$ 14(�69   5f      �� 5g      "  6    8   .�5h      ��  �@ TitlePlacement$	 Top=�6O   �� 5i       � ��$ 1q�6@   �� 5j       � ��
 �   �6<   �� 5k       � ��
 ���� 6;   �� 5l       �@��
IsReadOnly$
 True��6;   �� 5m       � {�$ False��6:   �� 5n       � ��$ 14(�69   5o      �� 5p      " _�68   .�5q      ��  � ��$ Center=�6K   �� 5r       � ��$ Center=�6I   5s        5t      6>   5u       5v      �� 5w      $��* �6+    5y      �� 5z      $
��auto �6(   �� 5{      $
��auto �6(   �� 5|      $
��auto �6(   �� 5}      $
��auto �6(   �� 5~      $
��auto �6(   �� 5      $
��auto �6(    � DataContext 5�      �� 5�       �� Source# [ 6"   .+�5�        -
   5          Name$ 	txtSchool��5�   W   $	 Arthur Eddy��6!   $��0��6J      Title$ School:��5�   !   ��6A   ��  �� Path$ School.Name� �� Mode$ TwoWay=�  5�      -   5        $ txtClassroomSize��5�   N   $	 25��6!   $��1��6A   $ Classroom Size:��5�   !   ��6I   �� $ ClassroomSize�$ TwoWay=�  5�      -   5        $ txtKidsAssigned��5�   M   $	 4��6!   $��2��6@   $ Kids Assigned:��5�   !   ��6H   �� $ KidsAssigned�$ TwoWay=�  5�      -   5        $ txtGradeLevel��5�   O   $		 1st��6!   $��3��6B   $ Grade Level:��5�   !   ��6F   �� $ 
GradeLevel�$ TwoWay=�  5�      -   5        $ 
txtAge0to5��5�   M   $	 0��6!   $��4��6@   $ Age 0 to 5:��5�   !   ��6E   �� $ Age0To5�$ TwoWay=�  5�      -   5        $ txtAge6to12��5�   M   $	 4��6!   $��5��6@   $ Age 6 to 12:��5�   !   ��6F   �� $ Age6To12�$ TwoWay=�5�      5�      5�      &   System.Windows.Controls.DataGrid  5�   
   -   5        $��
dtgStudent��5�   V      
IsReadOnly$
 True��6   $��10,10,10,10q�6%      AutoGenerateColumns$ False��6:   $��1��5�   L   $��1��6Y   $��3��6i   $��Center=�5�       �� AlternationCount$ 2��6C   ��5�      �� *childAssignmentsGrid+   ColumnHeaderStyle 5�      �� 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5�      ��  � ��
 �r���63   �� 5�       � ��$ Center=�6C   �� 5�       �@�ShowGridLines$
 True��6;   5�         RowStyle 5�      �� 5�      )	  #System.Windows.Controls.DataGridRow" 	 6     DataGridRowStyle# [ 61   .�5�      ��  � ��
 �����63    �� Triggers 5�      P� 5�        P� Property  �@	 AlternationIndex! P� Value$! 1��6>   . �5�      ��  � ��
 �����6;   5�      P� 5�        �@	 
IsSelected$
! True��68   . �5�      ��  � ��
 �r���6;   5�      5�      "   	CellStyle" 5�      �� 5�      *
  $System.Windows.Controls.DataGridCell" 
 6   .�5�      ��  � ��$ Stretch=�6<   6N   �� 5�       � ��$ Stretch=�6:   6L   �� 5�       � ��$ Stretch=�6A   6S   �� 5�       �@��TextAlignment$ Center=�6@   6Q   �� 5�       � ��$ Center=�6D   6U   �� 5�       � ��
 ���� 6I   �� 5�       � ��$ 0q�68   6D   5�       5�      Q   KB_FGMS.BusinessLogic.Models.Volunteer.VolunteerChildAssignmentDataGridModel  VolunteerGeneralDataGrid��%   &	         5�      6=   5�       5�      �� 5�      # [ 6   #   Columns# 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn$   Header$$ 
Identifier��6%   %   Width5  /System.Windows.Controls.DataGridLengthConverter$% * 6]   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 69   �� $ 
Identifier�6]     5�      $$ 
Conditions��6%   $% * 6\   & 69   �� $ 	Condition�'   ElementStyle' 5�      �� 5�      " ��6    .�5�      ��  �@^�ToolTip 6G   �� *ConditionDescription+5�      5�        5�      $$ Student Needs��6%   $% * 6b   & 6<   �� $ StudentNeeds�' 5�      �� 5�      " ��6    .�5�      ��  �@^�ToolTip 6G   �� *StudentNeedsDescription+5�      5�        5�      $$ Desired Outcome��6%   $% * 6f   & 6>   �� $ DesiredOutcome�6f   5�      5�      5�      