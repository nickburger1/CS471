   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5      $    C_FGMS.UI.VolunteerActivityLog   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        N� Title$  VolunteerActivityLog��5
      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   $  HandyControl.Controls.ComboBox  -   5          Name$ cmbSelectVolunteer��5   �    �� DisplayMemberPath$ FullName��5   �    �� SelectedValue$
 Tuid��6�   $��0��6�   $��0��6�      AutoComplete$
 True��6�      ShowClearButton$
 True��6�    �� 
IsEditable$
 True��6  $
��auto��6  $
��auto��6#  (  "HandyControl.Controls.TitleElement	   TitlePlacement$		 Top=�5   "   
   Title$
 Select Volunteer��6F   $��10q�6n    �� Text$	 All��6z   '  !HandyControl.Controls.InfoElement   Placeholder$  Volunteer Name��6�   $��0��6�   ��5      �� *
Volunteers+ �� SelectedItem 69   ��  �� Path$ SelectedVolunteer� �� TargetNullValue$  �� �� Mode$ TwoWay=�5   �   � 5    
   $��1��6   $��Bottom=�6    5!      �� 5"      $��* �6#   �� 5#      $��* �6#    5%      �� 5&      $
��auto �6    .+�5(      &   HandyControl.Controls.DatePicker  $��1��6G   $��0��6T   $
 Start Date:��6d      ShowClearButton$
 True��6�   $��10q�6�   $��1��6�   (  "System.Windows.Controls.DatePicker   SelectedDate 6   �� *SelectedStartDate+6�     5+      $��1��6E   $��1��6R   $
 	End Date:��6b   $
 True��6�   $��10q�6�   $��2��6�    6   �� *SelectedEndDate+6�   5,      � 53   
   $��2��6   $��Right=�6    54      �� 55      $��* �6#   �� 56      $��* �6#   �� 57      $��* �6#   �� 58      $��* �6#   �� 59      $��* �6#   .+�5<      �� -   5         ��Name$ btnAdd��5<      $��0��5=       � ToolTip$	 Add��6!   $��0q�5>      $��0q�6%   $��5,0,5,0q�61   .��5?      �� $��/Resources/add-icon.png��6   6;   5@      �� 5F      -   5        $ btnEdit��5F      $
 Edit��6<   $��1��5G      $��0q�6!   $��0q�65   $��5,0,5,0q�6A   .��5H      �� $��/Resources/edit-icon.png��6   5I      �� 5J      -   5        $ 	btnDelete��5J      $ Delete��6(   $��2��5M      $��0q�6!   $��0q�65   $��5,0,5,0q�6A   ��5K      �� *ConfirmDeleteCommand+.��5N      �� $ ��/Resources/delete-icon.png��6   5O      �� 5Q      -   5        $��	btnExport��5Q      $ Export to Excel��6&   $��0q�6@   $��3��6�   $��0q�6�   $��5,5,5,0q�6�   ��6T   �� *ExportCommand+.��5R      �� $!��\Resources\reports-icon.png��6   5S      �� 5U      -   5        $��
btnRefresh��5U      $ Refresh Data��6'   $��0q�6>   $��4��6R   $��0q�6b   $��5,5,5,0q�6n   .��5V      �� $!��\Resources\refresh-icon.png��6   5W      5X      &   System.Windows.Controls.DataGrid  5Z   
   -   5          Name$ dtgLog��5Z   M      
IsReadOnly$
 True��6   $��10q�6%      AutoGenerateColumns$ False��61   $��2��5[   �   $��0��6�   $��3��5\      $��Center=�64    �� AlternationCount$ 2��6Y   $��Center=�6n   ��5[      �� *ActivityLogs+ 6D   �� $ SelectedActivityLog�$ TwoWay=�   ColumnHeaderStyle 5]      �� 5^       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5_      ��  �� Property � �� �� Value
 �r���63   �� 5`       � ��$ Center=�6C   5a         RowStyle 5c      �� 5d      )  #System.Windows.Controls.DataGridRow"  6      DataGridRowStyle �� BasedOn# [  61   .�5e      ��  � ��
 �����63    �� Triggers 5f      P� 5g        P� Property  �@ AlternationIndex! P� Value$! 1��6>   . �5h      ��  � ��
 �����6;   5i      P� 5j        �@ 
IsSelected$
! True��68   . �5k      ��  � ��
 �r���6;   5l      5n      "   	CellStyle" 5p      �� 5q      *	  $System.Windows.Controls.DataGridCell" 	 6   .�5r      ��  � ��$ 0q�68   �� 5s       � ��
 ���� 6I   �� 5t       � ��$ Stretch=�6M   �� 5u       � �� 5v      �� 5w      # �� 
TargetType"# 	 6.   .R�5x   "   � #��z��6'   .+�5y   &   �� $��Center=�67   $��Center=�6T   5z   #   5{      5}      5~      $   Columns$ 5�      0
  *System.Windows.Controls.DataGridTextColumn
  5�      ,  &System.Windows.Controls.DataGridColumn%   Header$% 	Volunteer��6%   &   Width5  /System.Windows.Controls.DataGridLengthConverter$& * 6~   1  +System.Windows.Controls.DataGridBoundColumn'   Binding' 68   �� *Volunteer.FullName+( �� StringFormat$( {0:MM/dd/yyyy}��6~   
  5�      $
% Date��6%   $& * 6k   ' 63   �� *Date+$( {0:MM/dd/yyyy}��6k   
  5�      $% Initials��6%   $& * 6S   ' 67   �� *	Initial+6S   
  5�      $% Incident��6%   $& * 6T   ' 67   �� *
Incident+6T   
  5�      )   
Visibility$) Hidden=�6%   $& * 6R   ' 69   �� *Tuid+6R   5�      5�      5�      