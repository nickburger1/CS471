   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  $    C_FGMS.UI.VolunteerActivityLog   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  VolunteerActivityLog��.��� 
������� � ColumnDefinitions �� $��* ��� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $��* �.+�$  HandyControl.Controls.ComboBox  -     Name$ cmbSelectVolunteer�� �� DisplayMemberPath$ FullName�� �� SelectedValue$
 Tuid��$��0��$��0��   AutoComplete$
 True��   ShowClearButton$
 True�� �� 
IsEditable$
 True��$
��auto��$
��auto��(  "HandyControl.Controls.TitleElement	   TitlePlacement$		 Top=�
   Title$
 Select Volunteer��$��10q� �� Text$	 All��'  !HandyControl.Controls.InfoElement   Placeholder$  Volunteer Name��$��0������ *
Volunteers+ �� SelectedItem ��  �� Path$ SelectedVolunteer� �� TargetNullValue$  �� �� Mode$ TwoWay=�� $��1��$��Bottom=� �� $��* ��� $��* � �� $
��auto �.+�&   HandyControl.Controls.DatePicker  $��1��$��0��$
 Start Date:��   ShowClearButton$
 True��$��10q�$��1��(  "System.Windows.Controls.DatePicker   SelectedDate �� *SelectedStartDate+  $��1��$��1��$
 	End Date:��$
 True��$��10q�$��2�� �� *SelectedEndDate+� $��2��$��Right=� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnAdd��$��0�� � ToolTip$	 Add��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/add-icon.png���� -   $ btnEdit��$
 Edit��$��1��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/edit-icon.png���� -   $ 	btnDelete��$ Delete��$��2��$��0q�$��0q�$��5,0,5,0q����� *ConfirmDeleteCommand+.���� $ ��/Resources/delete-icon.png���� -   $��	btnExport��$ Export to Excel��$��0q�$��3��$��0q�$��5,5,5,0q����� *ExportCommand+.���� $!��\Resources\reports-icon.png���� -   $��
btnRefresh��$ Refresh Data��$��0q�$��4��$��0q�$��5,5,5,0q�.���� $!��\Resources\refresh-icon.png��&   System.Windows.Controls.DataGrid  -     Name$ dtgLog��   
IsReadOnly$
 True��$��10q�   AutoGenerateColumns$ False��$��2��$��0��$��3��$��Center=�   AlternatingRowBackground
 ����� �� AlternationCount$ 2��$��Center=����� *ActivityLogs+ �� $ SelectedActivityLog�$ TwoWay=�   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=�   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�   Columns 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumn  
  Header$  	Volunteer��! 
  Width5  /System.Windows.Controls.DataGridLengthConverter$! * 1  +System.Windows.Controls.DataGridBoundColumn"   Binding" �� *Volunteer.FullName+# �� StringFormat$# {0:MM/dd/yyyy}��	  $
  Date��$! * " �� *Date+$# {0:MM/dd/yyyy}��	  $  Initials��$! * " �� *	Initial+	  $  Incident��$! * " �� *
Incident+	  $ 
  
Visibility$$ Hidden=�$! * " �� *Tuid+