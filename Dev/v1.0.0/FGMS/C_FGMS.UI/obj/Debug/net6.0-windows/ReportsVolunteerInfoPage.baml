   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5      (    "C_FGMS.UI.ReportsVolunteerInfoPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        N� Title$  ReportsVolunteerInfoPage��5
      .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   $  HandyControl.Controls.ComboBox   �� DisplayMemberPath$ FullName��6�    �� SelectedValue$
 Tuid��6�   $��0��6�   $��0��6�      AutoComplete$
 True��6�      ShowClearButton$
 True��6�    �� 
IsEditable$
 True��6  $
��auto��6  $
��auto��6#  (  "HandyControl.Controls.TitleElement   TitlePlacement$	 Top=�5   "   	   Title$	 Filter by Volunteer��6F   $��10q�6q   
 �� Text$	
 All��6}   '  !HandyControl.Controls.InfoElement   Placeholder$  Volunteer Name��6�   $��0��6�   ��5      �� *
Volunteers+ �� SelectedItem 69   ��  �� Path$ SelectedVolunteer� �� TargetNullValue$  �� �� Mode$ TwoWay=�5   �     5   
   $��0��6�   $��1��6�   $
 True��6�   $
 True��6�   $
��auto��5   "   $
��auto��6/   $��10q�6=   $
 True��6I   $ Select Volunteer Status��6`   $	 Top=�6�   $	 Filter by Status��6�   ��5      �� *
Statuses+ 67   �� $ SelectedStatus�$  ��$ TwoWay=�5   �   � 5   
   $��2��6   $��Right=�6    5       �� 5!      $��* �6#   �� 5"      $��* �6#   �� 5#      $��* �6#   .+�5%      �� -   5         ��Name$ btnEdit��5%      $��0��6&   $��0q�66   $��0q�5&      $��5,0,5,0q�6    � ToolTip$
 Edit��6.   .��5'      �� $��/Resources/edit-icon.png��6   5(      �� 5*      -   5        $ 	btnExport��5*      $��1��6(   $��0q�68   $��0q�5+      $��5,0,5,0q�6!   $ Export��62   ��6C   �� *ExportCommand+.��5,      �� $!��/Resources/reports-icon.png��6   5-      �� 5/      -   5        $ 
btnRefresh��5/      $��2��6)   $��0q�69   $��0q�50      $��5,0,5,0q�6!   $ Refresh Data��62   .��51      �� $!��\Resources\refresh-icon.png��6   52      53      &   System.Windows.Controls.DataGrid  55   
   -   5          Name$ dtgInfo��55   d      
IsReadOnly$
 True��6   $��10q�6%      CanUserAddRows$ False��61      AutoGenerateColumns$ False��6H   $	��Top=�57         ColumnHeaderHeight$ 25Q�67      	RowHeight$ 40Q�6O   $��Center=�6^   
�������$��0��58   2      GridLinesVisibility$
 None=�59         CanUserResizeColumns$ False��6:   $��1��5:       $��3��6-      AlternatingRowBackground
 ����� �� AlternationCount$ 2��6f   ��56      �� *VolunteerInfo+ 69   �� $ SelectedVolunteerInfo�$ TwoWay=�   ColumnHeaderStyle 5;      �� 5<       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5=      ��  �� Property � �� �� Value
 �r���63   �� 5>       � ��$ Center=�6C   5?          	CellStyle  5A      �� 5B      *  $System.Windows.Controls.DataGridCell"  6   .�5C      ��  � ��$ Stretch=�6<   6N   �� 5D       � ��$ Stretch=�6:   6L   �� 5E       � ��$ Stretch=�6A   6S   �� 5F       �@��TextAlignment$ Center=�6@   6Q   �� 5G       � ��$ Center=�6D   6U   �� 5H       � ��
 ���� 6I   �� 5I       � ��$ 0q�68   6D   5J      !   Columns! 5L      0  *System.Windows.Controls.DataGridTextColumn  5M      ,  &System.Windows.Controls.DataGridColumn"   Width5	  /System.Windows.Controls.DataGridLengthConverter$" *	 6%   #   Header$
# Name��6/   1
  +System.Windows.Controls.DataGridBoundColumn$ 
  Binding$ 6=   �� *Volunteer.FullName+  5N      $" *	 6%   $# Status��6/   $ 6?   �� *Demographics.Status+  5O      $" *	 6%   $# 
Start Date��6/   $ 6C   �� *	StartDate+% �� StringFormat$% {0:MM/dd/yyyy}��  5P      $" *	 6%   $# End Date��6/   $ 6A   �� *	EndDate+$% {0:MM/dd/yyyy}��  5Q      $" *	 6%   $# Reason Seperated��6/   $ 6I   �� *InactiveStatusNameAndId.Name+  5R      $" *	 6%   $	# DOB��6/   $ 6<   �� *Demographics.DateOfBirth+$% {0:MM/dd/yyyy}��  5S      $" *	 6%   $# Gender��6/   $ 6?   �� *Demographics.Gender+  5T      $" *	 6%   $# Identifies As��6/   $ 6F   �� *Demographics.IdentifiesAs+  5U      $" *	 6%   $# 	Ethnicity��6/   $ 6B   �� *Demographics.Ethnicity+  5V      $" *	 6%   $
# Race��6/   $ 6=   �� *Demographics.RacialGroup+  5W      $" *	 6%   $# Veteran��6/   $ 6@   �� *Demographics.Veteran+  5X      $" *	 6%   $# Family of Military��6/   $ 6K   �� *Demographics.FamilyOfMilitary+5Z      � 5[   
   $��2��6   $��2��6    5\      �� 5]      $
��auto �6#   �� 5^      $
��auto �6#   .+�5`      ,  &HandyControl.Controls.SimpleStackPanel  &   Orientation$& 
Horizontal=�6"   $��2��6;   $��Right=�6H   $��0��6d   .+�5a      �� $��Total Active:��6   $��0q�60   $��2��6D   $��DemiBold"�6Q   �� 5b      -   5        $��totalActive��5b   8   $��0q�6K   $��2��6_   ��6   �� *TotalActive+6_   5c        5d      $& 
Horizontal=�6"   $��2��6;   $��Right=�6H   $��1��6d   .+�5e      �� $��Total Inactive:��6   $��0q�62   $��2��6F   $��DemiBold"�6S   �� 5f      -   5        $��totalInactive��5f   :   $��0q�6O   $��2��6c   ��6   �� *TotalInactive+6c   5g      5h      5i      5j      