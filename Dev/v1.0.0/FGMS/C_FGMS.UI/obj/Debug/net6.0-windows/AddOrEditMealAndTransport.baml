   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	   )    #C_FGMS.UI.AddOrEditMealAndTransport   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:C_FGMS.UI     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	     � Title$  AddOrEditMealAndTransport��5	   	   $	��450��6+   $	��800��68   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��2.5* �6   �� 5      $
��2.5* �6   �� 5      $��5* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��2.5* �6#   �� 5      $
��2.5* �6#   �� 5      $��3* �6#   �� 5      $��* �6#   .+�5      ,  &HandyControl.Controls.SimpleStackPanel  $��0��6"      Orientation$ Vertical=�62   $��Center=�6I   .+�5      �� -   5        $&�� lblYearAddOrEditMealAndTransport��5   P   
�������$��0q�6+   $��5,5,5,5q�6?   .��6x   Year:6   �� 5      -   5        $5��/lblCurrentSelectedYearAddOrEditMealAndTransport��5   ?   
�������$��0q�6+   .��6v   (Year)6   5        5       $��1��6"   $ Vertical=�62   $��Center=�6I   .+�5!      �� -   5        $'��!lblMonthAddOrEditMealAndTransport��5!   P   
�������$��0q�6+   $��5,5,5,5q�6?   .��6y   Month:6�   �� 5"      -   5        $6��0lblCurrentSelectedMonthAddOrEditMealAndTransport��5"   ?   
�������$��0q�6+   .��6w   	(Month)6�   5#      � 5'      $��2��6    5(      �� 5)      $��* �6$   �� 5*      $��* �6$    5,      �� 5-      $
��1.5* �6'   �� 5.      $��* �6'   .+�50      �� -   5        $1��+lblMealAndTransportEditYearlyMealValuelabel��50   \   $��0��6   $��0��6(   
�������$��0q�6H   $��Right=�6�   $��Bottom=�6�   .��6�   Yearly Meal Value:6�   �� 51      -   5        $,��&lblMealAndTransportEditYearlyMealValue��51   \   $��1��6   $��0��6(   
�������$��0q�6H   $��Center=�6�   $��Bottom=�6�   .��6�   $0.006�   �� 52      -   5        $4��.lblMealAndTransportEditCurrentMileageRateLabel��52   \   $��0��6   $��1��6(   
�������$��0q�6H   $��Right=�6�   $	��Top=�6�   .��6�   Current Mileage Rate:6�   �� 53      -   5        $/��)lblMealAndTransportEditCurrentMileageRate��53   \   $��1��6   $��1��6(   
�������$��0q�6H   $��Center=�6�   $	��Top=�6�   .��6�   0.006�   54        58      $��3��6"   $ 
Horizontal=�62   $	��Top=�6K   $��Right=�6c   .+�59      �� -	   5        $��btnMealandTransportSave��59      $��0q�68   $��0q�6L   $��5,5,5,0q�6X    � ToolTip$$ Save Data In Table to Database��5:   ;   .��5;      �� $��\Resources\save-icon.png��6   5<      �� 5>      -
   5        $��btnMealandTransportCancel��5>      $��0q�6:   $��0q�6w   $��5,5,5,0q�6�   $3 -Cancel Edit Return to Meal And Transport Page��6�   .��5?      �� $ ��\Resources\cancel-icon.png��6   5@      5A      5B      � 5E   
   $��1��6    5F      �� 5G      $��* �6#   �� 5H      $��* �6#   �� 5I      $��* �6#   �� 5J      $��* �6#   �� 5K      $
��.75* �6#    5N      �� 5O      $��* �6    �� 5P      $��* �6    .+�5S      �� -   5        $,��&lblMealAndTransportEditVolunteersLabel��5S   X   $��0��6   $��0��6$   
�������$��0q�6D   $��Center=�6�   $��Bottom=�6�   .��6�   	Volunteer6�   $  HandyControl.Controls.ComboBox  5T      -   5        $)��#cbobxMealAndTransportEditFilterName��5U         AutoComplete$
 True��5T       �� 
IsEditable$
 True��6/      ShowClearButton$
 True��5U   I   $��0��6a   $��1��6q   $��0��6~   $	��Top=�6�   $��Stretch=�6�   $��5q�6�   $	��200��5V   d   �� 5X      -   5        $*��$lblMealAndTransportEditNumMealsLabel��5X   X   $��1��6   $��0��6$   
�������$��0q�6D   $��Center=�6�   $��Bottom=�6�   .��6�   
Num. Meals6�   �� 5Y      -   5        $(��"txtboxMealAndTransportEditNumMeals��5Y   >   $��1��6   $��1��6&   $��5q�63   $	��Top=�6i   $��75��6�   $��1��6�    �� SelectionStart$ 4��5Z   >   6S   �� 5\      -   5        $(��"lblMealAndTransportEditNumBusRides��5\   X   $��2��6   $��0��6$   
�������$��0q�6D   $��Center=�6�   $��Bottom=�6�   .��6�   Num. Bus Rides6�   �� 5]      -   5        $+��%txtboxMealAndTransportEditNumBusRides��5]   >   $��2��6   $��1��6&   $��5q�63   $	��Top=�6l   $��75��5^      $��2��6$   .��6Y   6\   �� 5`      -   5        $)��#lblMealAndTransportEditMileageLabel��5`   X   $��3��6   $��0��6$   
�������$��0q�6D   $��Center=�6�   $��Bottom=�6�   .��6�   	Mileage6�   �� 5a      -   5        $'��!txtboxMealAndTransportEditMileage��5a   >   $��3��6   $��1��6&   $��5q�63   $	��Top=�6h   $��75��6�   $��3��5b      6\   �� 5d      -   5        $%��MealAndTransportEditUpdateTable��5d   T   $��4��6   $��1��6%   $��Update Table��62   $��5q�6I   $	��Top=�5e   C   $��4��6[   $0 *Updates Selected Volunteer Values in Table��6h   6�   5f      &   System.Windows.Controls.DataGrid  5i   
   -   5        $��dtgMealAndTransportEdit��5i      	   CanUserAddRows$	 False��63   
   
IsReadOnly$

 True��6J   $��2��6\   
�������   ColumnHeaderHeight$ 25Q�6~   $��Stretch=�5j      $��10,10,10,10q�61      	CellStyle 5o      �� 5p       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5r      ��  �� Property � �� �� Value$ Stretch=�6<   6N   �� 5s       � ��$ Stretch=�6:   6L   �� 5t       � ��$ Stretch=�6A   6S   �� 5u       �@��TextAlignment$ Center=�6@   6Q   �� 5v       � ��$ Center=�6D   6U   �� 5w       � ��
 ���� 6I   �� 5x       � ��$ 0q�68   6D   �� 5y       � ��
 ���� 6J   5z         RowStyle 5}      �� 5~      )  #System.Windows.Controls.DataGridRow"  6   .�5      ��  � ��
 �����63    �� Triggers 5�      P� 5�       P� Property �@ AlternationIndex P� Value$ 1��6>   . �5�      ��  � ��
 �����6;   5�      P� 5�       �@ 
IsSelected$
 True��68   . �5�      ��  � ��
 �r���6;   5�      5�         ColumnHeaderStyle 5�      �� 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5�      ��  � ��
 �r���63   �� 5�       � ��$ Center=�6C   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	 6%      Header$
 Name��6/   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 6=   �� *strVolunteerName+  5�      $ *	 6%   $ 
Num. Meals��6/    6C   �� *
numMeals+  5�      $ *	 6%   $ Num. Bus Rides��6/    6G   �� *numBusRides+  5�      $ *	 6%   $ Mileage��6/    6@   �� *	Mileage+5�      � 5�   
   $��3��6    5�      �� 5�      $��* �6#   �� 5�      $��* �6#   �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� -   5        $/��)lblMealAndTransportEditMonthlyTotalsLabel��5�      $��Center=�6E   $��Center=�6`   $��0q�6}   
�������$��0��6�   .��6�   Monthly Totals:6�   �� 5�      -   5        $,��&lblMealAndTransportEditMonthlyNumMeals��5�      $��Center=�6B   $��Center=�6]   $��0q�6z   
�������$��1��6�   .��6�   06�   �� 5�      -   5        $/��)lblMealAndTransportEditMonthlyNumBusRides��5�      $��Center=�6E   $��Center=�6`   $��0q�6}   
�������$��2��6�   .��6�   06�   �� 5�      -   5        $+��%lblMealAndTransportEditMonthlyMileage��5�      $��Center=�6A   $��Center=�6\   $��0q�6y   
�������$��3��6�   .��6�   06�   5�      5�      5�      