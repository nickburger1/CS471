   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  !    C_FGMS.UI.SchoolsAllSchools   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  SchoolsAllSchools�� N� 
Background
 �����.���  � RowDefinitions  � ColumnDefinitions �� $
��582* ��� $
��217* �.+�&   System.Windows.Controls.DataGrid  -     Name$ dtgAllSchool��   CanUserAddRows$ False��   AutoGenerateColumns$ False�� �� AlternationCount$ 2��   ColumnHeaderHeight$ 25Q�	   	RowHeight$	 40Q�$��Center=�
�������
   GridLinesVisibility$

 None=�   CanUserResizeColumns$ False��$��0,0,0,0q�$��0��   
IsReadOnly$
 True��   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=�   RowStyle �� )  #System.Windows.Controls.DataGridRow"     DataGridRowStyle �� BasedOn# [  .���  � ��
 ����� �� Triggers P�  P� Property �@ AlternationIndex P� Value$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$ School Name��-   
schoolName��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ * $ Principal Name�� �� *	Principal+  $ * $ Phone Number�� �� *ContactNumber+4  .System.Windows.Controls.DataGridTemplateColumn  $ * $ Status��   CellTemplate �� .R�d�   ToggleButtonSwitch#��[ ${�False�� d� 	IsChecked �� *
IsActive+� $��1�� �� $��* ��� $��2* ��� $��4* ��� $��4* �.+�� $��1��$��1��$
��Left=� �� $��9* ��� $��2* �.+��� $��Show only active schools ��$��Right=�$��0��$��0q��� -   $��	chkActive��$
��Left=�$��1��$	��5,0q�� $��1��$��2��$��0��$��Right=�$	��Top=� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� -    ��Name$ btnAdd��$��0��$��0q�$��0q�$��5,5,5,0q� � ToolTip$ Add a new School��.���� $��/Resources/add-icon.png���� -   $ btnEdit��$��1��$��0q�$��0q�$��5,5,5,0q�$ Edit selected school��.���� $��/Resources/edit-icon.png���� -   $ 	btnExport��$��2��$��0q�$��0q�$��5,5,5,0q�$! Export all Schools to Excel��.���� $!��\Resources\reports-icon.png���� -   $ btnPrint��$��3��$��0q�$��0q�$��5,5,5,0q�$% Export Site Visit Sheet to Word��.���� $��\Resources\print-icon.png���� -   $ 
btnRefresh��$��4��$��0q�$��0q�$��5,5,5,0q�$ Refresh Data��.���� $!��\Resources\refresh-icon.png��� $��2��$��4��$��1��$��2��$
��Left=�$	��Top=� �� $��* ��� $��* ��� $��* ��� $��* � �� $��9* ��� $��2* �.+��� -	    ��Name$ totalStudentsClassroom��$$��Total Students in Classrooms: ��$��0��$��0q�$��0��$��Right=��� -
   $ totalStudetnAssigned��$��0��$��Total Student Assigned: ��$��1��$��0q�$��Right=��� -   $ totalStudentAge��$��Total Students Ages 0-5: ��$��2��$��0��$��0q�$��Right=��� -   $ totalStudenttAge��$��0��$ ��Total Students Ages 6-12: ��$��3��$��0q�$��Right=��� -   $$ NumberofTotalStudentsClassroom��$��1��$��-��$��0��$��0q�$��Right=��� -   $# NumberofTotalStudentsAssigned��$��1��$��-��$��1��$��0q�$��Right=��� -   $! NumberofTotalStudentsAges05��$��1��$��-��$��2��$��0q�$��Right=��� -   $" NumberofTotalStudentsAges612��$��1��$��-��$��3��$��0q�$��Right=�