   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  #    C_FGMS.UI.SchoolPerSchoolPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  SchoolPerSchoolPage��.��� 
�������.+�� .+��� $
��Left=�$��14,0,0,0q�$
��Wrap=�$��Select School:��$	��Top=�$��17��$  HandyControl.Controls.ComboBox  -   $��
cbxSchools��$
��Left=�$��	14,20,0,0q�$	��Top=�$	��250��$��34��   AutoComplete$
 True�� �� IsTextSearchEnabled$ False�� �� 
IsEditable$
 True��'  !HandyControl.Controls.InfoElement   Placeholder$ Select School��  �� IsTextSearchCaseSensitive$ False�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$
 Name������ *	Schools+�� -   $��txtHours��$
��Left=�$��	14,62,0,0q�$
��Wrap=�$��School Hours��$	��Top=�$��18���� -   $��txtDays��$
��Left=�$��
139,62,0,0q�$
��Wrap=�$��School Days��$	��Top=�$��18���� $
��Left=�$��	293,4,0,0q�$
��Wrap=�$��
Principal:��$	��Top=��� -   $��txtPrincipal��$
��Left=�$��
293,29,0,0q�$
��Wrap=�$	��Top=��� -   $��
txtAddress��$
��Left=�$��
293,62,0,0q�$
��Wrap=�$��Address��$	��Top=�$��18���� $
��Left=�$��	567,4,0,0q�$
��Wrap=�$��
Secretary:��$	��Top=��� -   $��txtSecretary��$
��Left=�$��
567,29,0,0q�$
��Wrap=�$	��Top=��� -   $��txtPhoneNum��$
��Left=�$��
567,62,0,0q�$
��Wrap=�$��Phone Number��$	��Top=�$��18��&   System.Windows.Controls.DataGrid  -   $��dgVolsPerSchool��   
IsReadOnly$
 True��$��
10,85,10,0q�	   AutoGenerateColumns$	 False��
 �� AlternationCount$
 2��   ColumnHeaderHeight$ 30Q�   	RowHeight$ 40Q�$��0��$��Center=�   HeadersVisibility$	 All=�$��2��$
��True��   CanUserResizeColumns$
 True������ *
DataList+   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=�   RowStyle �� )  #System.Windows.Controls.DataGridRow"     DataGridRowStyle �� BasedOn# [  .���  � ��
 ����� �� Triggers P�  P� Property �@ AlternationIndex P� Value$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ 0q���  � ��
 ���� ��  � ��$ Stretch=���  � �� ��  �� 
TargetType"  .R�� #��z��.+��� $��Center=�$��Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Volunteer��   Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	 1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *Volunteer.FullName+  $ Teacher��$ *	  �� *Classroom.Teacher+  $ Grade��$ *	  �� *Classroom.Grade+  $
 Room��$ *	  �� *Classroom.Room+  $ Schedule��$ 2*	  �� *Schedule.StringSchedule+  $ Contact��$ *	  �� *Volunteer.Phone+� $��1��$��Right=�$	��Top=� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* �.+��� -	    ��Name$ btnEditSave��$��5,5,5,0q�$��Right=�$	��Top=�$��0��$��0q�$��0q�  � ToolTip$  Edit Selected School��.���� $��/Resources/edit-icon.png���� -
   $ btnDeleteCancel��$	��Top=�$��Right=�$��5,5,5,0q�$��1��$��0q�$��0q�$  Delete Selected School��.���� $ ��/Resources/delete-icon.png���� -   $ btnExcelPerSchool��$	��Top=�$��5,5,5,0q�$��Right=�$��2��$��0q�$��0q�$  Save To Excel��.���� $!��/Resources/reports-icon.png���� -   $ 
btnRefresh��$	��Top=�$��5,5,5,0q�$��Right=�$��3��$��0q�$��0q�$  Refresh Data��.���� $!��\Resources\refresh-icon.png��