   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5      &     C_FGMS.UI.ReportsAnnualCheckPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:C_FGMS.UI     5        N� Title$  ReportsAnnualCheckPage��5	       N� 
Background
 �����.��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $	��12* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ label��5      $��
Filter By:��6#   $��0��68   $��Right=�6H   
�������6d   d� 5      -   5         d�Name$ 
tglCurrent��5      $��Current Volunteers��60   $��1��6M    d� 	IsChecked$ FalseY�6]      ToggleButtonSwitch#��[  6o   6�   d� 5      -   5        $ 
tglMissing��5      $��Missing Documentation��60   $��2��6P   $ FalseY�6`   #��[  6r   6�   � 5       $��4��6   $��Right=�6#   $	��Top=�6?    5!      �� 5"      $��* �6'   �� 5#      $��* �6'   .+�5&      �� -   5         ��Name$ 	btnExport��5&      $��0q�6,   $��0��6@   $��5,5,5,0q�6P   $��0q�5'       � ToolTip$  Export Volunteers to Excel��6!   .��5(      �� $!��/Resources/reports-icon.png��6   5)      �� 5+      -   5        $ 
btnRefresh��5+      $��0q�6-   $��1��6B   $��5,5,5,0q�6R   $��0q�5,      $ Refresh Data��6!   .��5-      �� $!��\Resources\refresh-icon.png��6   5.      50      51      &   System.Windows.Controls.DataGrid  54   
   -   5        	  Name$	 dtgAnnualCheck��54   F   
   CanUserAddRows$
 False��6      AutoGenerateColumns$ False��6*    �� AlternationCount$ 2��55         ColumnHeaderHeight$ 40Q�6(      	RowHeight$ 40Q�6@   $��Center=�6O      
IsReadOnly$
 True��56         GridLinesVisibility$
 None=�6%      CanUserResizeColumns$ False��6@   $��1��57      $��4��6    $��10q�64      RowBackground
 �����   SelectionUnit$
 Cell=�6V      ColumnHeaderStyle 58      �� 59       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5:      ��  �� Property � �� �� Value
 �r���63   �� 5;       � ��$ Center=�6C   �� 5<       � ��$ 0 0 2 0q�68   �� 5=       � �� �	#FFE0643A64   5>         	CellStyle 5@      �� 5A      *  $System.Windows.Controls.DataGridCell"  6   .�5B      ��  �@��TextAlignment$ Center=�6@   �� 5C       � ��$ 0 0 1 0q�68   �� 5D       � �� �	#FFE0E0E064   �� 5E       � ��$ Center=�6D   5F         RowStyle 5H      �� 5I      )  #System.Windows.Controls.DataGridRow"  6   .�5J      -   5         �� Triggers 5K      P� 5L       P� Property �@ AlternationIndex P� Value$ 1��6>   . �5M      ��  � ��
 �����6;   5N      P� 5O       �@ AlternationIndex$ 1��6>   . �5P      ��  � ��
 �����6;   5Q      P� 5R       �@ 
IsSelected$
 True��68   . �5S      ��  � ��
 �r���6;   5T      5V         RowDetailsTemplate 5Y      �� 5Z      .R�5[      �� 
�������$��0 2 0 0q�60   $��
10 0 10 10q�6J   ._�5\      �  5]      �� 5^   "   $��* �60   �� 5_   "   $��* �60   �� 5`   "   $��* �60    5b      �� 5c   "   $��* �63   �� 5d   "   $��* �63   �� 5e   "   $��* �63   �� 5f   "   $��* �63   �� 5g   "   $��* �63   .+�5i      �� $��File Photo: ��6(   $��0��6<   $��0��6I   $��Right=�6Y   
��� � ��� 5j   "   $
��Left=�6K    6+   �� *	FilePhoto+6h   5k      �� 5l      $��Service Description: ��6(   $��0��6E   $��1��6R   $��Right=�6b   
��� � ��� 5m   "   $
��Left=�6U    6+   �� *ServiceDescription+6r   5n      �� 5o      $��True-Screen: ��6(   $��0��6=   $��2��6J   $��Right=�6Z   ���	#FFC22626�� 5p   "   $
��Left=�6]   ��6,   ��  �� Path$ 
TrueScreen� �� StringFormat$ d��6z   5q      �� 5r      $��Confid SOU: ��6(   $��0��6<   $��3��6I   $��Right=�6Y   
��� � ��� 5s   "   $
��Left=�6\   ��6,   �� $ 	ConfidSOU�$ d��6y   5t      �� 5u      $��Service Start Date: ��6(   $��0��6D   $��4��6Q   $��Right=�6a   ���	#FFC22626�� 5v   "   $
��Left=�6c   ��6,   �� $ ServiceStartDate�$ d��6�   5w      �� 5x      $��NSCHC Checked Form: ��6(   $��1��6D   $��0��6Q   $��Right=�6a   ���	#FFC22626�� 5y   "   $
��Left=�6G    6+   �� *NSCHC+6d   5z      �� 5{      $��Background Check: ��6(   $��1��6B   $��1��6O   $��Right=�6_   ���	#FFC22626�� 5|   "   $
��Left=�6Q    6+   �� *BackgroundCheck+6n   5}      �� 5~      $��	TB Shot: ��6(   $��1��69   $��2��6F   $��Right=�6V   
����� ��� 5   "   $
��Left=�6Y   ��6,   �� $ TBShot�$ d��6v   5�      �� 5�      $��NSOPW: ��6(   $��1��67   $��3��6D   $��Right=�6T   ���	#FFC22626�� 5�   "   $
��Left=�6X   ��6,   �� $ NSOPW�$ d��6u   5�      �� 5�      $��I-Chat: ��6(   $��1��68   $��4��6E   $��Right=�6U   ���	#FFC22626�� 5�   "   $
��Left=�6X   ��6,   �� $ IChat�$ d��6u   5�      �� 5�      $��Orientation Training: ��6(   $��2��6F   $��0��6S   $��Right=�6c   
��� � ��� 5�   "   $
��Left=�6V    6+   �� *OrientationTraining+6s   5�      �� 5�      $��	ID Copy: ��6(   $��2��69   $��1��6F   $��Right=�6V   ���	#FFC22626�� 5�   "   $
��Left=�6H    6+   �� *IDCopy+6e   5�      �� 5�      $��Field-Print Cleared: ��6(   $��2��6E   $��2��6R   $��Right=�6b   ���	#FFC22626�� 5�   "   $
��Left=�6d   ��6,   �� $ FieldPrintCleared�$ d��6�   5�      �� 5�      $��Alias F'Print: ��6(   $��2��6?   $��3��6L   $��Right=�6\   ���	#FFC22626�� 5�   "   $
��Left=�6^   ��6,   �� $ AliasFPrint�$ d��6{   5�      �� 5�      $��DHS: ��6(   $��2��65   $��4��6B   $��Right=�6R   ���	#FFC22626�� 5�   "   $
��Left=�6V   ��6,   �� $	 DHS�$ d��6s   5�      5�      5�      5�          RowHeaderTemplate  5�      �� 5�      .R�5�      �� ! ��Name$! RowImage��6   $	��Top=�6.   $ ��\Resources\expand-icon.png��6F   $��16��6j   $��16��6u   " �� Triggers" 5�      �� 5�      # �� Value$# Visible��6�   $ �� Binding$ 6&   �� *DetailsVisibility+% �� RelativeSource% �� & �� AncestorType& DataGridRow.a�5�      �� ' �� 
TargetName$' RowImage��6%    � ��$" \Resources\collapse-icon.png��6M   5�      5�      (   Columns( 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn)   Width5  /System.Windows.Controls.DataGridLengthConverter$) * 6%   *   Header$* Volunteer Name��6I   1  +System.Windows.Controls.DataGridBoundColumn+   Binding+ 6/   �� *Name+6I     5�      $) 2* 6%   $ * Schedule and Photo Release��6o   + 60   �� $ SchedulePhotoRelease�$ d��6o     5�      $) 2* 6%   $ * Emergency Beneficiary Form��6s   + 60   �� $ EmergancyBeneficiaryForm�$ d��6s     5�      $) * 6%   $* HIPPA Release��6f   + 6/   �� $ HippaRelease�$ d��6f     5�      $) * 6%   $* Physical��6b   + 6/   �� $ Physical�$ d��6b     5�      $) * 6%   $* Ann. Inc. Car Ins.��6r   + 6/   �� $ AnnualIncomeCarInsurance�$ d��6r   5�      5�      5�      