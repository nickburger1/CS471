   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI #   C_FGMS.UI.VolunteerFinancials   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  VolunteerFinancials�� N� 
Background
 �����.��� -   $��grdContainerFinancials�� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $��* � � 	Resources    HandyControl.Controls.Card �� 
TargetType   CardBaseStyle �� BasedOn �� Property  EffectShadow2 �� Value#  HandyControl.Controls.TextBox  	BaseStyle#  HandyControl.Controls.Divider  DividerBaseStyle	 �� Triggers
 P� Property P� Value��%�  '        7   '  v     7  '�� V    '  �    7  �� "  8   ���  �@J�Effect# �  ��  � ��$ 0q���  � �� �	#FFF6F6F6��  � ��$ 4q��� "  8   ���  � ��
 ���� ��  � ��$ 0q���  �@��TextAlignment$ Center=���  � ��$ Center=���  �@��
IsReadOnly$
 True����  �@��TextWrapping$
 Wrap=���  � ��$ 14(��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 5,0,0,2q���  � ��$ 14(��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q�	 P� 
 � ��$ 0��. ���  �@ 
LineStroke
 �   �.+�� $��0��$��3��$��Right=�$��0,10,0,0q�$	��Top=� �� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnEdit��$��0��$��1��$��0q�$��0q�$��5,0,5,0q� � ToolTip$
 Edit��$��-1��.���� $��/Resources/edit-icon.png���� -   $ 	btnFinish��$��0��$��0��$y�Hidden=�$��0q�$��0q�$��5,0,5,0q�$ Save Changes��$��-1��.���� $��/Resources/save-icon.png���� -   $ 	btnCancel��$��0��$��1��$y�Hidden=�$��0q�$��2q�$��5,0,5,0q�$ Cancel��$��-1��.���� $ ��/Resources/cancel-icon.png���� -   $ 
btnRefresh��$��0��$��2��$��0q�$��2q�$��5,0,5,0q�$ Refresh Data��$��-1��.���� $!��/Resources/refresh-icon.png���� -    ��Name$ txtEditMode��$��0��$��2��$��3��$��	Edit Mode��$��Center=�$��Center=�$��30(�$
��Bold"�
���r���$y�Hidden=�� $��0��$��0��$��3�� �� $��* � �� $
��auto ��� $��* � $  HandyControl.Controls.ComboBox  ComboBoxPlusBaseStyle(  "HandyControl.Controls.TitleElement��%#  '        7   '  |     7  '�� s    '      7  '  �    7  �� "  8   ���  �@J�Effect# �  ��  � ��$ 0q���  � �� �	#FFF6F6F6��  � ��$ 4,4,4,0q��� "  8   ���  � ��
 ���� ��  � ��$ 0q���  �@��TextAlignment$ Center=���  � ��$ Center=���  �@��
IsReadOnly$
 True����  �@��TextWrapping$ NoWrap=���  � ��$ 4q���  � ��$ 14(��� " �����  � ��$ Center=���  � ��$ NoWrap=���  � ��$ 5,0,0,2q���  � ��$ 14(���  �@��Padding$ 4q��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q�	 P� 
 � ��$ 0��. ���  �@ 
LineStroke
 �   ��� "  8   .���  � ��$ 4,4,0,0q���  �@ TitlePlacement$	 Top=���  � ��$ 4,4,4,4q���  �@ AutoComplete$
 True����  �@��
IsEditable$ False����  �@ ShowClearButton$
 True����  � y�$ Visible=���  � ��$ Center=���  � ��$
 auto����  � ��$ 14(�.+�,  &HandyControl.Controls.SimpleStackPanel     Orientation$ 
Horizontal=�.+�  -   $��cmbSelectVolunteer�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$ FullName�� �� 
IsEditable$
 True��   AutoComplete$
 True��$��1��$��0��$��3��$��8,4,10,4q�$	��200��   Title$ Select Volunteer��'  !HandyControl.Controls.InfoElement   Placeholder$ Volunteer Name��$��1��  -   $��cmbSelectYear��$��1��$��1��$ Select Year��$��4,4,10,4q�$	��150��   ShowClearButton$ False��$��2������  �� Path$ 
SelectYear�  -	   $��cmbSelectMonth��$��1��$��2��$��4,4,10,4q�$	��150��$ Select Month��$ False��$��3������ $ SelectMonth�  $��1��$��0��.����  �� CornerRadius$ 4,4,0,0��._�� -
   $��grdHours��$��2��$��0�� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $��* �   TextBoxExtend  txtRegHours �� Self �� RelativeSourceI   CB_FGMS.BusinessLogic.Models.Volunteer.VolunteerFinancialsHoursModel  VolunteerFinancialsHours��%U  '        7  &	 7    '�� �    &	 $    �� "  8   .���  �@��TextWrapping$ NoWrap=���  � ��$ 4q���  �@ TitlePlacement$
 Left=���  �@ VerticalAlignment$ Stretch=���  � ��$ 1q���  � ��
 ���� ��  � ��$ Center=���  � ��$ 4q���  �@��
IsReadOnly$
 True����  � ��$ 14(��� " ��	 P� 
 �@=�HasError$
 true��. ���  �@�ToolTip �� # Z $&  (Validation.Errors)/ErrorContent��� " ��.���  � ��$ NoWrap=���  �@��Padding$ 4q���  � ��$ 4q���  � ��$ 14(�   � DataContext ��  �� Source# [ .+��� $��Hours��$��0��$��1�� �� TextAlignment$ Center=�$
��Bold"�  $��0��  $��1��  $��2��  -     Name$ txtRegHours��$��1��$��0��$ Reg Hours: ��$��3��$* $Amount of hours worked for the month������ $ RegHours� �� Mode$ TwoWay=�  -   $ txtYTDHours��$��2��$��0��$ YTD Hours: ��$��-1��$2 ,Amount of hours accrued for the current year������ $ YtdHours�$ TwoWay=�  $��1��$��1��.���� $ 4,4,0,0��._�� -   $��grdPTO��$��2��$��0�� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* � 1	  +HandyControl.Themes.StaticResourceExtensionN
   HB_FGMS.BusinessLogic.Models.Volunteer.VolunteerFinancialsPtoStipendModel ! VolunteerFinancialsPtoStipend��%P  'd�       '  y     7  '�� �    &	     �� " d� 	  *ToggleButtonSwitch+.���  � ��$ 5,0,0,0q���  � ��$
 Left=���  � {�$ False���� "  8   .���  �@��TextWrapping$ NoWrap=���  � ��$ 4q���  �@ TitlePlacement$
 Left=���  �@ VerticalAlignment$ Stretch=���  � ��$ 1q���  � ��
 ���� ��  � ��$ Center=���  � ��$ 4q���  �@��
IsReadOnly$
 True����  � ��$ 14(��� " ��.���  � ��$ NoWrap=���  �@��Padding$ 4q���  � ��$ 4q���  � ��$ 14(�
   �� # [ .+��� $��PTO & Stipends��$��0��$��1��$ Center=�$
��Bold"�  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  $��6��  $��1��$��0��$��2��$��0,4,4,4q�$ 
Horizontal=�.+��� $��PTO Eligibility��  �� Padding$  0q�d� -   ! d�Name$! btnPtoEligible��$��0��$��1��$! Set the eligibility for PTO��" d� 	IsChecked" �� *IsPTOEligible+$ TwoWay=�  -   $ txtPTOEarned��$��2��$��0��$ PTO Earned: ��$��-1��$0 *Amount of PTO earned for the current month������ $ 	PtoEarned�$ TwoWay=�  -   $ 
txtPTOUsed��$��3��$��0��$ 
PTO Used: ��$��6��$N HAmount of PTO used for the current month. Cannot exceed PTO Start+Earned������ $ PtoUsed�$ TwoWay=�  -   $ txtPTOStart��$��4��$��0��$ PTO Start: ��$��-1��$N HAmount of PTO to start the current month based on previous month PTO End������ $ PtoStart�$ TwoWay=�  -   $ txtPTOEnded��$��5��$��0��$ PTO Ended: ��$��-1��$D >Amount of PTO to end the current month (PTO Start-Used+Earned)������ $ PtoEnded�$ TwoWay=�  -   $ txtStipendPaid��$��6��$��0��$ Stipend Paid: ��$��-1��$. (Amount compensated for the current month������ $ StipendPaid�$ TwoWay=�# �� StringFormat$# {0:C}��  $��1��$��2��.���� $ 4,4,0,0��._�� -   $��grdMealTransport��$��2��$��0�� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � Q   KB_FGMS.BusinessLogic.Models.Volunteer.VolunteerFinancialsMealTransportModel $	  VolunteerFinancialsMealTransport��%�  '        7  '�� 7    &		 �    �� "  8   .���  �@��TextWrapping$ NoWrap=���  � ��$ 4q���  �@ TitlePlacement$
 Left=���  �@ VerticalAlignment$ Stretch=���  � ��$ 1q���  � ��
 ���� ��  � ��$ Center=���  � ��$ 4q���  �@��
IsReadOnly$
 True����  � ��$ 14(��� " ��.���  � ��$ NoWrap=���  �@��Padding$ 4q���  � ��$ 4q���  � ��$ 14(�   �� # [	 .+��� $��Meal and Transport��$��0��$��1��$ Center=�$
��Bold"�  $��0��  $��1��  $��2��  $��3��  $��4��$   
LineStroke
$ �   �$��0,20,0,0q�  $��6��  $��7��  $��8��  -   $ txtSiteMeals��$��1��$��0��$ Site Meals: ��$��7��$3 -Amount of meals on site for the current month������ $ 	SiteMeals�$ TwoWay=�  -   $ 
txtMileage��$��2��$��0��$ 	Mileage: ��$��8��$3 -Amount of miles accrued for the current month������ $ Mileage�$ TwoWay=�  -   $ txtBusRides��$��3��$��0��$ Bus Rides: ��$��9��$/ )Amount of bus rides for the current month������ $ BusRides�$ TwoWay=��� $��Totals��$��5��$��1��$ Center=�$
��Bold"�  $��5��
$ �   �  -   $ txtTotalMealValue��$��6��$��0��$ Total Meal Value: ��$��-1��$ Total value for the meals������ $ TotalMealValue�$ TwoWay=�$# {0:C}��  -   $ txtTotalMileageValue��$��7��$��0��$ Total Mileage Value: ��$��-1��$ Total value for the miles������ $ TotalMileageValue�$ TwoWay=�$# {0:C}��  -   $ txtTotalBusRideValue��$��8��$��0��$ Total Bus Ride Value: ��$��-1��$# Total value for the bus rides������ $ TotalBusRidesValue�$ TwoWay=�$# {0:C}��  $��1��$��3��.���� $ 4,4,0,0��._�� -   $"��grdVolunteersFinancialsRates��$��0��$��0�� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* � J   DB_FGMS.BusinessLogic.Models.Volunteer.VolunteersFinancialsRatesModel 
 VolunteersFinancialsRates��%�  '        7  '�� 7    &	
 �    �� "  8   .���  �@��TextWrapping$ NoWrap=���  � ��$ 4q���  �@ TitlePlacement$
 Left=���  �@ VerticalAlignment$ Stretch=���  � ��$ 1q���  � ��
 ���� ��  � ��$ Center=���  � ��$ 4q���  �@��
IsReadOnly$
 True����  � ��$ 14(��� " ��.���  � ��$ NoWrap=���  �@��Padding$ 4q���  � ��$ 4q���  � ��$ 14(�   �� # [
 .+��� $��Rates��$��0��$��1��$ Center=�$
��Bold"�  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  -   $ txtCurrentPTORate��$��1��$��0��$ Current PTO Rate: ��$��-1��$- 'PTO Rate for the current month and year������ $ CurrentPtoRate�$ TwoWay=�  -   $ txtCurrentStipendRate��$��2��$��0��$ Current Stipend Rate: ��$��-1��$1 +Stipend Rate for the current month and year������ $ CurrentStipendRate�$ TwoWay=�  -   $ txtYearlyMealRate��$��3��$��0��$ Current Meal Rate: ��$��-1��$. (Meal Rate for the current month and year������ $ YearlyMealValue�$ TwoWay=�  -   $ txtCurrentMileageRate��$��4��$��0��$ Current Mileage Rate: ��$��-1��$1 +Mileage Rate for the current month and year������ $ CurrentMileageRate�$ TwoWay=�  -    $ txtCurrentBusRideRate��$��5��$��0��$ Current Bus Rate: ��$��-1��$- 'Bus Rate for the current month and year������ $ CurrentBusRideRate�$ TwoWay=�