   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  "    C_FGMS.UI.FinanceGeneralPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  FinanceGeneralPage��.��� 
������� � ColumnDefinitions �� $��4* ��� $��* � � RowDefinitions �� $��* ��� $��4* ��� $��* ��� $��6* �.+�   HandyControl.Controls.Card  $��0��$��0��$��0q�.��� $��Center=� �� $��* ��� $��* ��� $��4* �.+��� $��Select Year:��$��Stretch=�$��0q�$��0��$  HandyControl.Controls.ComboBox  -   $��cmbSchoolCostShareYear��$��Stretch=�$
��auto��$��1��$��0�� � ToolTip$ Fiscal Range, 10/1 - 9/30���� $��School Cost Share��$��0q�$
��Bold"�$��0��$��0��$��3��� $��1��$��Right=�$	��Top=� �� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnAdd��$��0��$��0q�$��0q�$��5,5,5,0q�$ Add Cost Share��$��1��.���� $��/Resources/add-icon.png���� -   $��btnFinancialGeneralEdit��$��0q�$��1��$��0q�$��5,5,5,0q�$��2��$ Edit selected cost share��.���� $��\Resources\edit-icon.png���� -   $��btnFinancialGeneralExport��$��0q�$��2��$��0q�$��5,5,5,0q�$��3��$$ Export displayed data to excel��.���� $!��\Resources\reports-icon.png���� -   $��
btnRefresh��$��0q�$��3��$��0q�$��5,5,5,0q�$��3��$ Refresh Data��.���� $!��\Resources\refresh-icon.png��  $��1��$��0��.��&   System.Windows.Controls.DataGrid  -   $��dtgCostShare��   CanUserAddRows$ False��   AutoGenerateColumns$ False��$��Stretch=�   ColumnHeaderHeight$ 25Q�   	RowHeight$ 40Q�$��Stretch=�
�������	   GridLinesVisibility$
	 None=�
   CanUserResizeColumns$
 False�� �� AlternationCount$ 2��   
IsReadOnly$
 True��   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=�   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�   RowStyle �� )  #System.Windows.Controls.DataGridRow"     DataGridRowStyle �� BasedOn# [  .���  � ��
 ����� �� Triggers P�  P� Property �@ AlternationIndex P� Value$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ 2*	    Header$ Cost Share Name��1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *Name+  $ 2*	 $ Date Billed�� �� *Date+ �� StringFormat$ d��  $ 3*	 $ Value�� �� *Value+$ c��  $��0��$��2��$��0q�.��� $��Center=� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $
��auto �.+��� $��Filter Name��$��0q�$��0��$��Center=�  -   $��cmbFilterName��$��Stretch=�   AutoComplete$
 True�� �� 
IsEditable$
 True��$��Center=�   ShowClearButton$
 True��$��1��$��4�� �� SelectedValuePath$
 Tuid��  �� DisplayMemberPath$  FullName������ *VolunteerList+�� $��3��$��0q�$��Right=�$��DemiBold"�.��Cost Share Total:�� -   $��lblCostShareTotal��$��4��$��0q�$
��Left=�.��$0  $��0��$��3��$��0q�.���  �� $	��13* ��� $��2* �.+�� $��0��.+�  -	   $��dtgDonations��$ False��$��Stretch=�$ False��$ 25Q�$ 40Q�$��Stretch=�
�������$
	 None=�$
 False��$ 2��$
 True�� �� "  .���  � ��
 �r�����  � ��$ Center=� �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q� �� "  # [  .���  � ��
 ����� P�  �@ AlternationIndex$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   $ 2*	 $ 
Donor Name�� �� *VolunteerDonorName+  $ 2*	 $ Expense Type�� �� *ExpenseTypeName+  $ 2*	 $
 Date�� �� *Date+$ d��  $ 3*	 $ Value�� �� *Value+$ c��� $��1�� �� $��3* ��� $��* ��� $��* �.+��� $��1��$��In-Kind Total: ��$��0q�$��Right=�$��DemiBold"��� -
   $��lblDonationTotal��$��2��$
��Left=�$��$0��$��0q�� $��2��$��1��$��Right=�$	��Top=� �� $��* ��� $��* ��� $��* �.+��� -   $ btnAddExpense��$	��Top=�$��Right=�$��0q�$��0q�$��5,0,0,0q�$ Add Expense��$��0��$��8��.���� $��/Resources/add-icon.png���� -   $��btnFinanceExpenseEdit��$��0q�$��1��$��0q�$��5,0,5,0q�$��9��$ Edit selected Expense��$	��Top=�$��Right=�.���� $��\Resources\edit-icon.png��  $��2��$��3��$��2��$��0q�$��5q�.��� $	��Top=� �� $��* � �� $��3* ��� $��3* ��� $��3* ��� $��3* ��� $��3* ��� $��3* ��� $��* �.+��� $��In-Kind Expense Type��$��0q�$��0��  -   $��cmbExpenseType��$��0,5,0,0q�$��1��$
 Tuid��$
  Name��$��5��$ Expense Type to be shown������ *expenseTypes+�� $��
Year Range��$��0q�$��0,10,0,0q�$��2��  -   $��cmbYearRange��$��0,5,0,0q�$��3��$��6��$4 .Date range dependent on Grant/Fiscal selection���� $��Grant/Fiscal��$��0q�$��0,10,0,0q�$��4��  -   $��cmbYearType��$��0,5,0,0q�$��5��$��7��$&  The year type for expense table.��