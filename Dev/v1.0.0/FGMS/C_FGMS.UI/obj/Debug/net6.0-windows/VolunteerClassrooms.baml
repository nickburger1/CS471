   M S B A M L   `   `   `  ����O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullgGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogicB_FGMS.BusinessLogic.Models  5      D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI 5      #   C_FGMS.UI.VolunteerClassrooms   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      XdataGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogic     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5	        N� Title$  VolunteerClassrooms��5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��215* �6   �� 5      $
��481* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   $  HandyControl.Controls.ComboBox  -   5          Name$ CmbSelectVolunteer��5   "   $��0��5      $��0��6#      AutoComplete$
 True��63    �� 
IsEditable$
 True��6G   $
��auto��5   "   $
��auto��6/   $��10,10,10,10q�6=      ShowClearButton$
 True��6R    �� SelectedValuePath$
 Tuid��5   "    �� DisplayMemberPath$ FullName��5   "   '  !HandyControl.Controls.InfoElement	   Placeholder$	 Volunteer Name��5    "   ��5   "   �� *Names+5!   "     5#   
   -   5        $ CmbSelectSchool��5(   "   $��0��5#      $��1��6#   $
 True��63   $
 True��6G   $
��auto��5$   "   $
��auto��6/   $��10,10,10,10q�6=   $
 True��6R   $
 Tuid��5&   "   $
 Name��5'   "   $	 School Name��5)   "   ��5%   "   �� *	Schools+5)   K   �� 5,   
   -   5        
 ��Name$
 txtEditMode��5,      $��2��6)   $��	Edit Mode��69   $	��Top=�5-      $
��Left=�6,   $��30(�5.      $
��Bold"�6"   
���r���$y�Hidden=�5/      $��40��6(   $	��145��64   $��95,8,0,0q�6@    J� RenderTransformOrigin$ -0.833,0.451&�6R   � 51   
   $��3��6   $��Right=�6    52      �� 53      $��* �6#   �� 54      $��* �6#   �� 55      $��* �6#   �� 56      $��* �6#   �� 57      $��* �6#   �� 58      $��* �6#   .+�5;      �� -   5         ��Name$ btnAdd��5;      $��1��5<      $��0q�5=      $��0q�6%   $��5,0,5,0q�61    � ToolTip$ Add Classroom��5>      .��5?      �� $��/Resources/add-icon.png��6   6;   5@      �� 5A      -   5        $ btnEdit��5A      $ Edit Classroom��6<   $��2��5B      $��0q�6!   $��0q�65   $��5,0,5,0q�6A   .��5C      �� $��/Resources/edit-icon.png��6   5D      �� 5E      -   5        $ 	btnDelete��5E      $ Delete Classroom��6@   $��3��5F      $��0q�6!   $��0q�65   $��5,0,5,0q�6A   .��5G      �� $ ��/Resources/delete-icon.png��6   5H      �� 5I      -   5        $ 	btnExport��5I      $��4��5J      $��0q�5K      $��0q�6%   $��5,0,5,0q�61   $ Export Classroom to Excel��5L      .��5M      �� $!��\Resources\reports-icon.png��6   5N      �� 5O      -   5        $ 
btnRefresh��5O      $��5��5P      $��0q�5Q      $��0q�6%   $��5,0,5,0q�61   $ Refresh Data��5R      .��5S      �� $!��\Resources\refresh-icon.png��6   5T      5U      &   System.Windows.Controls.DataGrid  5X   
   -	   5        $��dtgClassrooms��5X   V      
IsReadOnly$
 True��6   $��10,10,10,10q�6%      AutoGenerateColumns$ False��6:   $��1��5Y   J   $��0��6W   $��3��6g   $��Center=�5Z      $��Center=�6C    �� AlternationCount$ 2��6e   ��5Y      �� *ClassroomsDataGrid+   ColumnHeaderStyle 5\      �� 5]       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5^      ��  �� Property � �� �� Value
 �r���63   �� 5_       � ��$ Center=�6C   �� 5`       �@�ShowGridLines$
 True��6;   5a         RowStyle 5d      �� 5e      )  #System.Windows.Controls.DataGridRow"  6      DataGridRowStyle �� BasedOn# [  61   .�5f      ��  � ��
 �����63    �� Triggers 5g      P� 5h       P� Property �@ AlternationIndex P� Value$ 1��6>   . �5i      ��  � ��
 �����6;   5j      P� 5k       �@ 
IsSelected$
 True��68   . �5l      ��  � ��
 �r���6;   5m      5o         	CellStyle 5r      �� 5s      *  $System.Windows.Controls.DataGridCell"  6   .�5t      ��  � ��$ Stretch=�6<   6N   �� 5u       � ��$ Stretch=�6:   6L   �� 5v       � ��$ Stretch=�6A   6S   �� 5w       �@��TextAlignment$ Center=�6@   6Q   �� 5x       � ��$ Center=�6D   6U   �� 5y       � ��
 ���� 6I   �� 5z       � ��$ 0q�68   6D   5{       � 	Resources 5~      1   +B_FGMS.BusinessLogic.Models.ClassroomsModel  
Classrooms��%   &	         5      6'   5�       � DataContext 5�      �� 5�       �� Source# [ 6      Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 	Volunteer��6%     	  Width5
  /System.Windows.Controls.DataGridLengthConverter$  *
 6i   1  +System.Windows.Controls.DataGridBoundColumn!   Binding! 68   �� " �� Path$" Volunteer.FormattedName�6i     5�      $ School��6%   $  *
 6Z   ! 65   �� $" School.Name�6Z     5�      $
 Room��6%   $  *
 6\   ! 63   �� $" ClassroomNumber�6\     5�      $ 
Class Size��6%   $  *
 6`   ! 69   �� $" ClassroomSize�6`     5�      $ Grade��6%   $  *
 6X   ! 64   �� $" 
GradeLevel�6X     5�      $ Teacher��6%   $  *
 6[   ! 66   �� $" TeacherName�6[     5�      $
 Days��6%   $  *
 6Y   ! 63   �� $" Schedule.Day�6Y     5�      $ 
Start Time��6%   $  *
 6e   ! 69   �� $" Schedule.StartTime�6e     5�      $ End Time��6%   $  *
 6a   ! 67   �� $" Schedule.EndTime�6a   5�      5�      5�      