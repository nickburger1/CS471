   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  O KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�aclr-namespace:B_FGMS.BusinessLogic.ViewModels.FiscalGrantViewModels;assembly=B_FGMS.BusinessLogic5B_FGMS.BusinessLogic.ViewModels.FiscalGrantViewModels #    C_FGMS.UI.FinanceYearPageBase   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  }fiscalgrantviewmodelsaclr-namespace:B_FGMS.BusinessLogic.ViewModels.FiscalGrantViewModels;assembly=B_FGMS.BusinessLogic    N� Title$  FinanceYearPage��.��� .+�� -   $��GridNormalView�� � ShowGridLines$ False�� � RowDefinitions �� $��* ��� $��6* ��� $��6* � � ColumnDefinitions �� $	��12* ��� $��* ��� $	��12* �.+�   HandyControl.Controls.Card  $��0��$��0��$��2��$��0q�.���  �� $��2* ��� $��2* ��� $��* �.+��� -    ��Name$ labelYearDateRange��$��Select Date Range��$��Stretch=�$��0q�$��0��$  HandyControl.Controls.ComboBox  -   $��cmbYearDateRange��$��Stretch=�$��Center=�$
��auto��$��1������ *
YearRanges+ �� SelectedItem �� *SelectedDateRange+  $��0��$��3��$��0q�.��� $��1��$��Right=�$	��Top=� �� $��* ��� $��* ��� $��* �.+��� -   $��btnFinanceExport��$��0q�$��1�� � ToolTip$ Export to Excel��$��0q�$��5,5,5,0q����� *ExportCommand+.���� $!��\Resources\reports-icon.png���� -   $��
btnRefresh��$��0q�$ Refresh Data��$��2��$��0q�$��5,5,5,0q�.���� $!��\Resources\refresh-icon.png��  $��1��$��0��.���  �� $��2* ��� $	��10* ��� $��* �.+�  $��0��.��� .+��� $��Meal-In-Kind��$��0q��� -   $��0q�$	��Top=�$��Right=�$ View as full window��.���� $!��\Resources\magnify-icon.png��&   System.Windows.Controls.DataGrid  -   $��dtgYearMealInKind��   AutoGenerateColumns$ False��$��1��   
IsReadOnly$
 True��	   AlternatingRowBackground
	 �����
 �� AlternationCount$
 2������ *MealTransportMainPage+   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=���  � ��$ 20��   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    CanUserResize$ False��   Header$ Quarter��1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� *
strQuarter+  $ * $ False��$
 Date�� �� *	strDate+  $ * $ False��$ Value�� �� *strMealValue+,
  &HandyControl.Controls.SimpleStackPanel
   
  Orientation$ 
Horizontal=�$��2��$��Right=�$��0,0,10,0q�.+��� $��Total:��$��0q�$��2��$��DemiBold"�$��Center=��� -   $��lblYearMealInKindTotal��$��2��$��Center=����� *	MealTotal+  $��2��$��0��.���  �� $��2* ��� $	��10* ��� $��* �.+�  $��0��.��� .+��� $��In-Kind��$��0q�$��0���� -	   $��1��$��0q�$	��Top=�$��Right=�$ View as full window��.���� $!��\Resources\magnify-icon.png��  -
   $��dtgYearToDate��$ False��$��1��$
 True��
	 �����$
 2������ *YearToDateList+ �� "  .���  � ��
 �r�����  � ��$ Center=���  � ��$ 20�� �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�   $ * $ False��$
 Type�� �� *Name+  $ * $ False��$ Value�� �� *Value+ �� StringFormat$ c��
  $ 
Horizontal=�$��2��$��Right=�$��0,0,10,0q�.+��� $��Total:��$��0q�$��2��$��DemiBold"�$��Center=��� -   $��lblYearToDateTotal��$��2��$��Center=����� *InKindTotal+  $��2��$��1��.���  �� $��2* ��� $	��10* ��� $��* �.+�  $��0��.��� .+��� $��Volunteer Mileage��$��0q��� -   $��0q�$��Right=�$	��Top=�$ View as full window��.���� $!��\Resources\magnify-icon.png��  -   $��dtgYearVolunteerMileage��$��1��$ False��$
 True��
	 �����$
 2������ *MealTransportMainPage+ �� "  .���  � ��
 �r�����  � ��$ Center=���  � ��$ 20�� �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�   $ * $ False��$ Quarter�� �� *
strQuarter+  $ * $ False��$
 Date�� �� *	strDate+  $ * $ False��$ Reimbursement�� �� *strMileageValue+
  $ 
Horizontal=�$��2��$��Right=�$��0,0,10,0q�.+��� $��Total:��$��0q�$��2��$��DemiBold"�$��Center=��� -   $"��lblYearVolunteerMileageTotal��$��2��$��Center=����� *MileageTotal+  $��2��$��2��.���  �� $��2* ��� $	��10* ��� $��* �.+�  $��0��.��� .+��� $��Bus Transportation��$��0q��� -   $��0q�$��Right=�$	��Top=�$ View as full window��.���� $!��\Resources\magnify-icon.png��  -   $��dtgYearBusTransportation��$��1��$ False��$
 True��
	 �����$
 2������ *MealTransportMainPage+ �� "  .���  � ��
 �r�����  � ��$ Center=���  � ��$ 20�� �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�   $ * $ False��$ Quarter�� �� *
strQuarter+  $ * $ False��$
 Date�� �� *	strDate+  $ * $ False��$ Billings�� �� *strBusValue+
  $ 
Horizontal=�$��2��$��Right=�$��0,0,10,0q�.+��� $��Total:��$��0q�$��2��$��DemiBold"�$��Center=��� -   $#��lblYearBusTransportationTotal��$��Center=����� *
BusTotal+� -   $��GridYearFocusedView��$y�Hidden=� �� $��* ��� $	��11* �.+�  $��0��$��0q�$��Center=�.���  �� $��* ��� $��* ��� $��* ��� $��* ��� $��4* � �� $
��auto �.+��� $��
Start Date��$��0��$��0��$��Center=�$��Center=�&   HandyControl.Controls.DatePicker  -   $��dtpFocusStartDate��$��0��   ShowClearButton$
 True��$��1��(  "System.Windows.Controls.DatePicker   SelectedDate �� *SelectedFocusStartDate+�� $��End Date��$��2��$��0��$��Center=�$��Center=�  -   $��dtpFocusEndDate��$��0��$��3��$
 True�� �� *SelectedFocusEndDate+  $��0��$��7��$��0q�.��� $��1��$��Right=�$	��Top=� �� $��* ��� $��* �.+��� -   $��btnBack��$��0��$ Minimize Table��.���� $"��\Resources\minimize-icon.png���� -   $��btnFinanceFocusedExport��$��0q�$ Export to Excel��$��1��$��0q�$��5,5,5,0q����� *ExportCommand+.���� $!��\Resources\reports-icon.png��  $��1��.���  �� $��* ��� $	��11* ��� $��* � �� $��* ��� $��4* �.+��� -   $��tbkTableName��$��0��$��2��$��0��$��Center=�$��Center=����� *CurrentTitle+  -   $��dtgFocusTable��$ False��$��1��   ColumnWidth$ * $��0��$��2��   CanUserResizeColumns$ False��$
 True��
	 �����$
 2������ *MealTransportMileageList+ �� "  .���  � ��
 �r�����  � ��$ Center=���  � ��$ 20�� �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�   $ * $ False��$ 	Volunteer�� �� *Name+  -   tbkQuarterColumn��$ * $ False��$ Quarter�� �� *	Quarter+  -   tbkDateColumn��$ * $ False��$
 Date�� �� *Date+  -   tbkRateColumn��$ * $ False��$
 Rate�� �� *Rate+$ c��  -   tbkCountColumn��$ * $ False��$ Count�� �� *Count+  $ * $ False��$ Value�� �� *Value+$ c��� $��2��$��7��.+�
  $ 
Horizontal=�$��Right=�$��0,0,10,0q�.+��� $��Total: ��$��DemiBold"�$��0q�$��Center=��� -   $��lblFocusTotal��$��Center=����� *
FocusTotal+