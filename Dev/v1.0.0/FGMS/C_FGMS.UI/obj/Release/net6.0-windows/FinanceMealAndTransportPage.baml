   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  +    %C_FGMS.UI.FinanceMealAndTransportPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$!  FinanceMealAndTransportPage��.��� 
������� � RowDefinitions �� $
��1.5* ��� $��* ��� $��5* ��� $��* ��� $��* � � ColumnDefinitions �� $��3* ��� $��1.25* ��� $��* �.+�   HandyControl.Controls.Card  $��0��$��0��
�������$��0q�.���  �� $��* ��� $
��1.5* ��� $��* ��� $
��1.5* �.+��� -   $(��"lblMealAndTransportSelectYearRange��$��Year Range:��$��Right=�$��Center=�$��0q�$��0��$  HandyControl.Controls.ComboBox  -   $*��$cbobxMealAndTransportSelectYearRange��$��Stretch=�$��Center=�$��1��$��10,10,10,10q�$��0���� -   $$��lblMealAndTransportSelectMonth��$��Month:��$��Right=�$��Center=�$��0q�$��2��  -   $&�� cbobxMealAndTransportSelectMonth��$��Stretch=�$��Center=�$��3��$��10,10,10,10q�$��1��  $��1��$��0��$��Stretch=�.���  �� $��* � �� $	��.5* ��� $��1.25* �.+��� $��1,1,1,0q�$��0��$��0��
���   ����#F0EFEF._��� -   $��lblMealandTransportRates��$��0q����#F0EFEF.��Rates�� $��1,0,1,1q�$��0��$��1��
���   ����#F0EFEF._�,  &HandyControl.Controls.SimpleStackPanel  ���#F0EFEF$��0��$��1��.+�     Orientation$ 
Horizontal=����#F0EFEF$��-8,-2,-15,0q�$	��Top=�$��Center=�.+��� -   $��lblYearlyMealValue��$��0q����#F0EFEF.��Yearly Meal Value:�� -   $��txtBoxYearlyMealValue��${�False��$��0q�$
��4.00�� �� TextAlignment$
 Left=����#F0EFEF  $ 
Horizontal=����#F0EFEF$��0,-2,0,0q�$	��Top=�$��Center=�.+��� -   $��lblCurrentMilageRate��$��0q����#F0EFEF.��Current Mileage Rate:�� -	   $��txtBoxCurrentMilageRate��${�False��$��0q�$
��4.00��$
 Left=����#F0EFEF$��	-8,-2,0,0q�� $��2��$��Right=�$	��Top=� �� $��* ��� $��* ��� $��* �.+��� -
   $��btnMealandTransportEdit��$��0q�$��0��$��0q�$��5,5,5,0q� � ToolTip$f `Edit a Given Months Meal and Transport Information. If new Month sets all volunteers values to 0��.���� $��\Resources\edit-icon.png���� -   $��btnMealandTransportExport��$��0q�$��1��$��0q�$��5,5,5,0q�$ Export to Excel��.���� $!��\Resources\reports-icon.png���� -   $��
btnRefresh��$��0q�$��2��$��0q�$��5,5,5,0q�$ Refresh Data��.���� $!��\Resources\refresh-icon.png��� $��1�� �� $��* ��� $
��1.5* ��� $��* ��� $
��1.5* �.+��� $��Filter Name:��$��0q�$��0��$��Center=�$��Right=�  -   $��cbobxFilterName��   AutoComplete$
 True�� �� 
IsEditable$
 True��   ShowClearButton$
 True��$��1��$��2��$��Center=�$��Stretch=�$��10,10,10,10q�  $��2��$��0��$��3��$��0q�.��&   System.Windows.Controls.DataGrid  -   $��dtgMealAndTransport��	   CanUserAddRows$	 False��
   AutoGenerateColumns$
 False��$��Stretch=�   ColumnHeaderHeight$ 25Q�   	RowHeight$ 40Q�$��Stretch=�
�������   GridLinesVisibility$
 None=�   CanUserResizeColumns$ False��   
IsReadOnly$
 True��   AlternatingRowBackground
 �����   	CellStyle ��  �� 
TargetType*  $System.Windows.Controls.DataGridCell"  .���  �� Property � �� �� Value$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q���  � ��
 ����    ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��
 �r�����  � ��$ Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	    Header$
 Name��1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *strVolunteerName+  $ *	 $ 
Num. Meals�� �� *
numMeals+  $ *	 $ Total Meal Value�� �� *totalMealCost+  $ *	 $ Num. Bus Rides�� �� *numBusRides+  $ *	 $ Mileage�� �� *	Mileage+  $ *	 $ Total Milage Value�� �� *totalMileageCost+  $��3��$��0��$��3��.���  �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��lblMealMonthlyTotals��$��Right=�$��Center=�$��0q�$��0��.��Monthly Totals�� -   $��lblMealNumMeals��$��Center=�$��Center=�$��0q�$��1��.��326�� -   $��lblMealTotalMealValue��$��Center=�$��Center=�$��0q�$��2��.��	$1,304.00�� -   $��lblMealBusRides��$��Center=�$��Center=�$��0q�$��3��.��0�� -   $��lblMealMileage��$��Center=�$��Center=�$��0q�$��4��.��3,321�� -   $��lblMealMileageValue��$��Center=�$��Center=�$��0q�$��5��.��	$1,328.40  $��4��$��0��$��3��.���  �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��lblYTDMonthlyTotals��$��Right=�$��Center=�$��0q�$��0��.��
YTD Totals�� -   $��lblYTDMealNumMeals��$��Center=�$��Center=�$��0q�$��1��.��1025�� -   $��lblYTDMealTotalMealValue��$��Center=�$��Center=�$��0q�$��2��.��	$4,100.00�� -   $��lblYTDMealBusRides��$��Center=�$��Center=�$��0q�$��3��.��0�� -   $��lblYTDMealMileage��$��Center=�$��Center=�$��0q�$��4��.��10,611�� -   $��lblYTDMealMileageValue��$��Center=�$��Center=�$��0q�$��5��.��	$4,244.40