   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  )    #C_FGMS.UI.AddOrEditMealAndTransport   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  AddOrEditMealAndTransport��$	��450��$	��800��.��� 
������� � RowDefinitions �� $
��2.5* ��� $
��2.5* ��� $��5* ��� $��* �.+�� $��0�� � ColumnDefinitions �� $
��2.5* ��� $
��2.5* ��� $��3* ��� $��* �.+�,  &HandyControl.Controls.SimpleStackPanel  $��0��   Orientation$ Vertical=�$��Center=�.+��� -   $&�� lblYearAddOrEditMealAndTransport��
�������$��0q�$��5,5,5,5q�.��Year:�� -   $5��/lblCurrentSelectedYearAddOrEditMealAndTransport��
�������$��0q�.��(Year)  $��1��$ Vertical=�$��Center=�.+��� -   $'��!lblMonthAddOrEditMealAndTransport��
�������$��0q�$��5,5,5,5q�.��Month:�� -   $6��0lblCurrentSelectedMonthAddOrEditMealAndTransport��
�������$��0q�.��	(Month)� $��2�� �� $��* ��� $��* � �� $
��1.5* ��� $��* �.+��� -   $1��+lblMealAndTransportEditYearlyMealValuelabel��$��0��$��0��
�������$��0q�$��Right=�$��Bottom=�.��Yearly Meal Value:�� -   $,��&lblMealAndTransportEditYearlyMealValue��$��1��$��0��
�������$��0q�$��Center=�$��Bottom=�.��$0.00�� -   $4��.lblMealAndTransportEditCurrentMileageRateLabel��$��0��$��1��
�������$��0q�$��Right=�$	��Top=�.��Current Mileage Rate:�� -   $/��)lblMealAndTransportEditCurrentMileageRate��$��1��$��1��
�������$��0q�$��Center=�$	��Top=�.��0.00  $��3��$ 
Horizontal=�$	��Top=�$��Right=�.+��� -	   $��btnMealandTransportSave��$��0q�$��0q�$��5,5,5,0q� � ToolTip$$ Save Data In Table to Database��.���� $��\Resources\save-icon.png���� -
   $��btnMealandTransportCancel��$��0q�$��0q�$��5,5,5,0q�$3 -Cancel Edit Return to Meal And Transport Page��.���� $ ��\Resources\cancel-icon.png��� $��1�� �� $��* ��� $��* ��� $��* ��� $��* ��� $
��.75* � �� $��* ��� $��* �.+��� -   $,��&lblMealAndTransportEditVolunteersLabel��$��0��$��0��
�������$��0q�$��Center=�$��Bottom=�.��	Volunteer$  HandyControl.Controls.ComboBox  -   $)��#cbobxMealAndTransportEditFilterName��   AutoComplete$
 True�� �� 
IsEditable$
 True��   ShowClearButton$
 True��$��0��$��1��$��0��$	��Top=�$��Stretch=�$��5q�$	��200���� -   $*��$lblMealAndTransportEditNumMealsLabel��$��1��$��0��
�������$��0q�$��Center=�$��Bottom=�.��
Num. Meals�� -   $(��"txtboxMealAndTransportEditNumMeals��$��1��$��1��$��5q�$	��Top=�$��75��$��1�� �� SelectionStart$ 4���� -   $(��"lblMealAndTransportEditNumBusRides��$��2��$��0��
�������$��0q�$��Center=�$��Bottom=�.��Num. Bus Rides�� -   $+��%txtboxMealAndTransportEditNumBusRides��$��2��$��1��$��5q�$	��Top=�$��75��$��2��.���� -   $)��#lblMealAndTransportEditMileageLabel��$��3��$��0��
�������$��0q�$��Center=�$��Bottom=�.��	Mileage�� -   $'��!txtboxMealAndTransportEditMileage��$��3��$��1��$��5q�$	��Top=�$��75��$��3���� -   $%��MealAndTransportEditUpdateTable��$��4��$��1��$��Update Table��$��5q�$	��Top=�$��4��$0 *Updates Selected Volunteer Values in Table��&   System.Windows.Controls.DataGrid  -   $��dtgMealAndTransportEdit��	   CanUserAddRows$	 False��
   
IsReadOnly$

 True��$��2��
�������   ColumnHeaderHeight$ 25Q�$��Stretch=�$��10,10,10,10q�   	CellStyle ��  �� 
TargetType*  $System.Windows.Controls.DataGridCell"  .���  �� Property � �� �� Value$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q���  � ��
 ����    RowStyle �� )  #System.Windows.Controls.DataGridRow"  .���  � ��
 ����� �� Triggers P�  P� Property �@ AlternationIndex P� Value$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��
 �r�����  � ��$ Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	    Header$
 Name��1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *strVolunteerName+  $ *	 $ 
Num. Meals�� �� *
numMeals+  $ *	 $ Num. Bus Rides�� �� *numBusRides+  $ *	 $ Mileage�� �� *	Mileage+� $��3�� �� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $/��)lblMealAndTransportEditMonthlyTotalsLabel��$��Center=�$��Center=�$��0q�
�������$��0��.��Monthly Totals:�� -   $,��&lblMealAndTransportEditMonthlyNumMeals��$��Center=�$��Center=�$��0q�
�������$��1��.��0�� -   $/��)lblMealAndTransportEditMonthlyNumBusRides��$��Center=�$��Center=�$��0q�
�������$��2��.��0�� -   $+��%lblMealAndTransportEditMonthlyMileage��$��Center=�$��Center=�$��0q�
�������$��3��.��0