   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  (    "C_FGMS.UI.ReportsVolunteerInfoPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  ReportsVolunteerInfoPage��.��� 
������� � RowDefinitions �� $
��auto ��� $��* ��� $
��auto � � ColumnDefinitions �� $��* ��� $��* ��� $��* �.+�$  HandyControl.Controls.ComboBox   �� DisplayMemberPath$ FullName�� �� SelectedValue$
 Tuid��$��0��$��0��   AutoComplete$
 True��   ShowClearButton$
 True�� �� 
IsEditable$
 True��$
��auto��$
��auto��(  "HandyControl.Controls.TitleElement   TitlePlacement$	 Top=�	   Title$	 Filter by Volunteer��$��10q�
 �� Text$	
 All��'  !HandyControl.Controls.InfoElement   Placeholder$  Volunteer Name��$��0������ *
Volunteers+ �� SelectedItem ��  �� Path$ SelectedVolunteer� �� TargetNullValue$  �� �� Mode$ TwoWay=�  $��0��$��1��$
 True��$
 True��$
��auto��$
��auto��$��10q�$
 True��$ Select Volunteer Status��$	 Top=�$	 Filter by Status������ *
Statuses+ �� $ SelectedStatus�$  ��$ TwoWay=�� $��2��$��Right=� �� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnEdit��$��0��$��0q�$��0q�$��5,0,5,0q� � ToolTip$
 Edit��.���� $��/Resources/edit-icon.png���� -   $ 	btnExport��$��1��$��0q�$��0q�$��5,0,5,0q�$ Export������ *ExportCommand+.���� $!��/Resources/reports-icon.png���� -   $ 
btnRefresh��$��2��$��0q�$��0q�$��5,0,5,0q�$ Refresh Data��.���� $!��\Resources\refresh-icon.png��&   System.Windows.Controls.DataGrid  -     Name$ dtgInfo��   
IsReadOnly$
 True��$��10q�   CanUserAddRows$ False��   AutoGenerateColumns$ False��$	��Top=�   ColumnHeaderHeight$ 25Q�   	RowHeight$ 40Q�$��Center=�
�������$��0��   GridLinesVisibility$
 None=�   CanUserResizeColumns$ False��$��1��$��3��   AlternatingRowBackground
 ����� �� AlternationCount$ 2������ *VolunteerInfo+ �� $ SelectedVolunteerInfo�$ TwoWay=�   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=�    	CellStyle  �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�!   Columns! 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn"   Width5	  /System.Windows.Controls.DataGridLengthConverter$" *	 #   Header$
# Name��1
  +System.Windows.Controls.DataGridBoundColumn$ 
  Binding$ �� *Volunteer.FullName+  $" *	 $# Status��$ �� *Demographics.Status+  $" *	 $# 
Start Date��$ �� *	StartDate+% �� StringFormat$% {0:MM/dd/yyyy}��  $" *	 $# End Date��$ �� *	EndDate+$% {0:MM/dd/yyyy}��  $" *	 $# Reason Seperated��$ �� *InactiveStatusNameAndId.Name+  $" *	 $	# DOB��$ �� *Demographics.DateOfBirth+$% {0:MM/dd/yyyy}��  $" *	 $# Gender��$ �� *Demographics.Gender+  $" *	 $# Identifies As��$ �� *Demographics.IdentifiesAs+  $" *	 $# 	Ethnicity��$ �� *Demographics.Ethnicity+  $" *	 $
# Race��$ �� *Demographics.RacialGroup+  $" *	 $# Veteran��$ �� *Demographics.Veteran+  $" *	 $# Family of Military��$ �� *Demographics.FamilyOfMilitary+� $��2��$��2�� �� $
��auto ��� $
��auto �.+�,  &HandyControl.Controls.SimpleStackPanel  &   Orientation$& 
Horizontal=�$��2��$��Right=�$��0��.+��� $��Total Active:��$��0q�$��2��$��DemiBold"��� -   $��totalActive��$��0q�$��2������ *TotalActive+  $& 
Horizontal=�$��2��$��Right=�$��1��.+��� $��Total Inactive:��$��0q�$��2��$��DemiBold"��� -   $��totalInactive��$��0q�$��2������ *TotalInactive+