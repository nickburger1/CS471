   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullgGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogicB_FGMS.BusinessLogic.Models  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI #   C_FGMS.UI.VolunteerClassrooms   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     XdataGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  VolunteerClassrooms��.��� 
������� � ColumnDefinitions �� $
��auto ��� $
��215* ��� $
��481* � � RowDefinitions �� $
��auto ��� $��* �.+�$  HandyControl.Controls.ComboBox  -     Name$ CmbSelectVolunteer��$��0��$��0��   AutoComplete$
 True�� �� 
IsEditable$
 True��$
��auto��$
��auto��$��10,10,10,10q�   ShowClearButton$
 True�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$ FullName��'  !HandyControl.Controls.InfoElement	   Placeholder$	 Volunteer Name������ *Names+  -   $ CmbSelectSchool��$��0��$��1��$
 True��$
 True��$
��auto��$
��auto��$��10,10,10,10q�$
 True��$
 Tuid��$
 Name��$	 School Name������ *	Schools+�� -   
 ��Name$
 txtEditMode��$��2��$��	Edit Mode��$	��Top=�$
��Left=�$��30(�$
��Bold"�
���r���$y�Hidden=�$��40��$	��145��$��95,8,0,0q� J� RenderTransformOrigin$ -0.833,0.451&�� $��3��$��Right=� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnAdd��$��1��$��0q�$��0q�$��5,0,5,0q� � ToolTip$ Add Classroom��.���� $��/Resources/add-icon.png���� -   $ btnEdit��$ Edit Classroom��$��2��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/edit-icon.png���� -   $ 	btnDelete��$ Delete Classroom��$��3��$��0q�$��0q�$��5,0,5,0q�.���� $ ��/Resources/delete-icon.png���� -   $ 	btnExport��$��4��$��0q�$��0q�$��5,0,5,0q�$ Export Classroom to Excel��.���� $!��\Resources\reports-icon.png���� -   $ 
btnRefresh��$��5��$��0q�$��0q�$��5,0,5,0q�$ Refresh Data��.���� $!��\Resources\refresh-icon.png��&   System.Windows.Controls.DataGrid  -	   $��dtgClassrooms��   
IsReadOnly$
 True��$��10,10,10,10q�   AutoGenerateColumns$ False��$��1��$��0��$��3��$��Center=�$��Center=� �� AlternationCount$ 2������ *ClassroomsDataGrid+   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=���  �@�ShowGridLines$
 True��   RowStyle �� )  #System.Windows.Controls.DataGridRow"     DataGridRowStyle �� BasedOn# [  .���  � ��
 ����� �� Triggers P�  P� Property �@ AlternationIndex P� Value$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q� � 	Resources 1   +B_FGMS.BusinessLogic.Models.ClassroomsModel  
Classrooms��%   &	          � DataContext ��  �� Source# [    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 	Volunteer��  	  Width5
  /System.Windows.Controls.DataGridLengthConverter$  *
 1  +System.Windows.Controls.DataGridBoundColumn!   Binding! �� " �� Path$" Volunteer.FormattedName�  $ School��$  *
 ! �� $" School.Name�  $
 Room��$  *
 ! �� $" ClassroomNumber�  $ 
Class Size��$  *
 ! �� $" ClassroomSize�  $ Grade��$  *
 ! �� $" 
GradeLevel�  $ Teacher��$  *
 ! �� $" TeacherName�  $
 Days��$  *
 ! �� $" Schedule.Day�  $ 
Start Time��$  *
 ! �� $" Schedule.StartTime�  $ End Time��$  *
 ! �� $" Schedule.EndTime�