   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  &     C_FGMS.UI.ReportsAnnualCheckPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI       N� Title$  ReportsAnnualCheckPage�� N� 
Background
 �����.���  � RowDefinitions �� $��* ��� $	��12* �.+�� $��0�� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ label��$��
Filter By:��$��0��$��Right=�
�������d� -    d�Name$ 
tglCurrent��$��Current Volunteers��$��1�� d� 	IsChecked$ FalseY�   ToggleButtonSwitch#��[  d� -   $ 
tglMissing��$��Missing Documentation��$��2��$ FalseY�#��[  � $��4��$��Right=�$	��Top=� �� $��* ��� $��* �.+��� -    ��Name$ 	btnExport��$��0q�$��0��$��5,5,5,0q�$��0q� � ToolTip$  Export Volunteers to Excel��.���� $!��/Resources/reports-icon.png���� -   $ 
btnRefresh��$��0q�$��1��$��5,5,5,0q�$��0q�$ Refresh Data��.���� $!��\Resources\refresh-icon.png��&   System.Windows.Controls.DataGrid  -   	  Name$	 dtgAnnualCheck��
   CanUserAddRows$
 False��   AutoGenerateColumns$ False�� �� AlternationCount$ 2��   ColumnHeaderHeight$ 40Q�   	RowHeight$ 40Q�$��Center=�   
IsReadOnly$
 True��   GridLinesVisibility$
 None=�   CanUserResizeColumns$ False��$��1��$��4��$��10q�   RowBackground
 �����   SelectionUnit$
 Cell=�   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=���  � ��$ 0 0 2 0q���  � �� �	#FFE0643A   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  �@��TextAlignment$ Center=���  � ��$ 0 0 1 0q���  � �� �	#FFE0E0E0��  � ��$ Center=�   RowStyle �� )  #System.Windows.Controls.DataGridRow"  .�-    �� Triggers P�  P� Property �@ AlternationIndex P� Value$ 1��. ���  � ��
 �����P�  �@ AlternationIndex$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   RowDetailsTemplate �� .R��� 
�������$��0 2 0 0q�$��
10 0 10 10q�._��  �� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��File Photo: ��$��0��$��0��$��Right=�
��� � ��� $
��Left=� �� *	FilePhoto+�� $��Service Description: ��$��0��$��1��$��Right=�
��� � ��� $
��Left=� �� *ServiceDescription+�� $��True-Screen: ��$��0��$��2��$��Right=����	#FFC22626�� $
��Left=�����  �� Path$ 
TrueScreen� �� StringFormat$ d���� $��Confid SOU: ��$��0��$��3��$��Right=�
��� � ��� $
��Left=����� $ 	ConfidSOU�$ d���� $��Service Start Date: ��$��0��$��4��$��Right=����	#FFC22626�� $
��Left=����� $ ServiceStartDate�$ d���� $��NSCHC Checked Form: ��$��1��$��0��$��Right=����	#FFC22626�� $
��Left=� �� *NSCHC+�� $��Background Check: ��$��1��$��1��$��Right=����	#FFC22626�� $
��Left=� �� *BackgroundCheck+�� $��	TB Shot: ��$��1��$��2��$��Right=�
����� ��� $
��Left=����� $ TBShot�$ d���� $��NSOPW: ��$��1��$��3��$��Right=����	#FFC22626�� $
��Left=����� $ NSOPW�$ d���� $��I-Chat: ��$��1��$��4��$��Right=����	#FFC22626�� $
��Left=����� $ IChat�$ d���� $��Orientation Training: ��$��2��$��0��$��Right=�
��� � ��� $
��Left=� �� *OrientationTraining+�� $��	ID Copy: ��$��2��$��1��$��Right=����	#FFC22626�� $
��Left=� �� *IDCopy+�� $��Field-Print Cleared: ��$��2��$��2��$��Right=����	#FFC22626�� $
��Left=����� $ FieldPrintCleared�$ d���� $��Alias F'Print: ��$��2��$��3��$��Right=����	#FFC22626�� $
��Left=����� $ AliasFPrint�$ d���� $��DHS: ��$��2��$��4��$��Right=����	#FFC22626�� $
��Left=����� $	 DHS�$ d��    RowHeaderTemplate  �� .R��� ! ��Name$! RowImage��$	��Top=�$ ��\Resources\expand-icon.png��$��16��$��16��" �� Triggers" �� # �� Value$# Visible��$ �� Binding$ �� *DetailsVisibility+% �� RelativeSource% �� & �� AncestorType& DataGridRow.a��� ' �� 
TargetName$' RowImage�� � ��$" \Resources\collapse-icon.png��(   Columns( 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn)   Width5  /System.Windows.Controls.DataGridLengthConverter$) * *   Header$* Volunteer Name��1  +System.Windows.Controls.DataGridBoundColumn+   Binding+ �� *Name+  $) 2* $ * Schedule and Photo Release��+ �� $ SchedulePhotoRelease�$ d��  $) 2* $ * Emergency Beneficiary Form��+ �� $ EmergancyBeneficiaryForm�$ d��  $) * $* HIPPA Release��+ �� $ HippaRelease�$ d��  $) * $* Physical��+ �� $ Physical�$ d��  $) * $* Ann. Inc. Car Ins.��+ �� $ AnnualIncomeCarInsurance�$ d��