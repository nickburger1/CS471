   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5          C_FGMS.UI.FinancePTOPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        N� Title$  FinancePTOPage��5
      .��5      � 
������� � ShowGridLines$ False��6    � ColumnDefinitions 5   
   �� 5      $��6* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $��* �6   �� 5      $��5* �6   �� 5      $��* �6   �� 5      $��2* �6   .+�5   
      HandyControl.Controls.Card  $��0��6   $��0��6   .��5      � $ False��6    5      �� 5       $��* �6'   �� 5!      $��* �6'   �� 5"      $��* �6'   �� 5#      $��* �6'   �� 5$      $��* �6'    5'      �� 5(      $	��.5* �6$   �� 5)      $��1.25* �6$   .+�5+      �� -   5        $��
lblPTOYear��5+      $
��Year��6*   $��0q�6:   ��6N   Z� ��6e   Z� $  HandyControl.Controls.ComboBox  5,      -   5        $��cbobxPTOYear��5,      $	��Top=�63   $��5,5,5,0q�6K   $��1��6\    �� SelectedIndex$ 0��6i   �� 5.      -   5         ��Name$ lblPTOMonths��5.      $��Month��6.   $��0q�6?   $��1��6S     5/      -   5          Name$ cbobxPTOMonths��5/      $	��Top=�67   $��1��6P   $��1��6`   $��5,5,5,0q�6m   $ 0��6~   �� 52      $��Stretch=�6   
���   �$��1,1,1,0q�6I   $��4��6d   $��0��6t   ���#F0EFEF._�53      �� -   5        $��lblPTORates��53      $��0q�6/   ���#F0EFEF.��6X   Rates6_   54      �� 55      $��Stretch=�6   
���   �$��1,0,1,1q�6I   $��4��6d   $��1��6t   ���#F0EFEF._�57      ,  &HandyControl.Controls.SimpleStackPanel     Orientation$ Vertical=�6*   $��4��6A   $��2��6Q   ���#F0EFEF$��Stretch=�6s   $��Center=�6�   .+�59        $ 
Horizontal=�6.   $��Center=�6G   .+�5:      �� -   5        $��lblPTOStipendRate��5:   8   $��0q�6$   ���#F0EFEF$��Center=�6f   $	��Top=�6�   .��6�   Stipend Rate:6�   �� 5;      -   5        $��txtBoxPTOStipendRate��5;   &   ${�False��6B   $��0q�6T   $
��3.15��6h    �� TextAlignment$
 Left=�6t   ���#F0EFEF$	��Top=�6�   '  !HandyControl.Controls.ButtonGroup  5<      5=      �� 5>      $��0,0,0,1q�6$   ���#F0EFEF65     5?      $ 
Horizontal=�6.   $��Center=�6G   .+�5@      �� -   5        $��lblPTOMileageRate��5@   8   $��0q�6$   ���#F0EFEF$��Center=�6f   $	��Top=�6�   .��6�   	PTO Rate:6�   �� 5A      -	   5        $��txtBoxPTORate��5A   &   ${�False��6;   $��0q�6M   $
��0.12��6a   $
 Left=�6m   ���#F0EFEF$	��Top=�6�   5B      5D      5E      5G      5H      � 5K   
   $��1��6   $��Right=�6   $	��Top=�6;    5L      �� 5M      $��* �6#   �� 5N      $��* �6#   �� 5O      $��* �6#   .+�5R      �� -
   5        $��
btnPTOEdit��5R      $��0q�6'   $��0��6U   $��0q�6e   $��5,5,5,0q�6q   	 � ToolTip$	 Edit PTO Table��6�   .��5S      �� $��\Resources\edit-icon.png��6   5T      �� 5V      -   5        $��btnPrintToExcel��5V      $��0q�6,   $��1��6A   $��0q�6Q   $��5,5,5,0q�6]   $	 Export To Excel��6�   .��5W      �� $!��\Resources\reports-icon.png��6   5X      �� 5Z      -   5        $��
btnRefresh��5Z      $��0q�6'   $��2��6<   $��0q�6L   $��5,5,5,0q�6X   $	 Refresh Page��6�   .��5[      �� $!��\Resources\refresh-icon.png��6   5\      5^        5a   
   $��1��6   $��0��6   $��0q�6/   .��5b      � $��Center=�6    5c      �� 5d      $
��154* �6'   �� 5e      $
��154* �6'   �� 5f      $
��107* �6'   �� 5g      $
��356* �6'    5j      �� 5k      $
��Auto �6$   .+�5n      �� -   5        $��lblPTOSearchVolunteer��5n      
�������$��0q�6H   .��6\   Search Volunteer6n   �� 5o      -   5        
 ��Name$
 txtPTOSearchVolunteer��5o      $��1��69   $��Center=�6I   $��Center=�6f   $	��134��6�   6�   5p      5q        5t   
   $��0��6   $��2��6"   $��2��6/   .��5u      &   System.Windows.Controls.DataGrid  -   5        $��dtgPTO��5u   J      CanUserAddRows$ False��6      AutoGenerateColumns$ False��6.      ColumnHeaderHeight$ 25Q�6X      	RowHeight$ 40Q�6p   
�������   GridLinesVisibility$
 None=�5v   +      CanUserResizeColumns$ False��6F      
IsReadOnly$
 True��6c   $��Center=�6u   $��Center=�6�      AlternatingRowBackground �	#FFBEBEBE   SelectionMode$ Single=�6�      RowStyle 5z      �� 5{       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� BasedOn# [B 6>    �� Triggers 5�      ]� 5�       ]� 
Conditions 5�   "   �� 5�   &    �� Property �@ 
IsSelected �� Value$ False��5�      �� 5�   &    �@ AlternationIndex$ 1��5�      /�5�   "   �� 5�   &    �� Property � �� �� Value
 ���� 5�      5�      ]� 5�       5�   "   �� 5�   &    �@ 
IsSelected$
 True��5�      �� 5�   &    �@ AlternationIndex$ 1��5�      /�5�   "   �� 5�   &    � ��
 �����5�      5�      ]� 5�       5�   "   �� 5�   &    �@ 
IsSelected$ False��5�      �� 5�   &    �@ AlternationIndex$ 0��5�      /�5�   "   �� 5�   &    � ��
 ���� 5�      5�      ]� 5�       5�   "   �� 5�   &    �@ 
IsSelected$
 True��5�      �� 5�   &    �@ AlternationIndex$ 0��5�      /�5�   "   �� 5�   &    � ��
 �r���5�      5�      5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6   # [B 6?    5�      �� 5�       �� Binding 6*   �� *
IsSelected+ �� RelativeSource ��   �� AncestorType"   ! �� Value! True5�      .a�5�   "   ��  � ��
 ���� 5�      5�      .�5�      ��  � �� 5�      �� 5�   "   " �� 
TargetType""  62   .R�5�   &   � #��z��6+   .+�5�   *   �� $��Center=�6;   $��Center=�6X   5�   '   5�   #   5�      5�      #   ColumnHeaderStyle# 5�      �� 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5�      ��  � ��
 �r���67   �� 5�       � ��$ Center=�6G   �� 5�       � ��$ Center=�6E   5�      $   Columns$ 5      4	  .System.Windows.Controls.DataGridComboBoxColumn	  5     -   5        colVolunteerName��,
  &System.Windows.Controls.DataGridColumn% 
  Width5  /System.Windows.Controls.DataGridLengthConverter$% * 5  G   & 
  Header$& Volunteer Name��6Q   ' 	  DisplayMemberPath$' strFullName��6�   ( 	  TextBinding( 6i   �� *strFullName+) �� UpdateSourceTrigger$) PropertyChanged=�* 	  ItemsSource* 6�   �� *strFullName+5     0  *System.Windows.Controls.DataGridTextColumn  5     -   5        colRegHours��$% * 5  >   $& 	Reg Hours��6H   1  +System.Windows.Controls.DataGridBoundColumn+   Binding+ 6[   �� *RegularHours+$) PropertyChanged=�  5     -   5        
colPtoUsed��$% * 5  =   $& PTO Used��6G   + 6Y   �� *	PtoUsed+$) PropertyChanged=�  5	     -   5        colPtoEarned��$% * 5	  ?   $& 
PTO Earned��6I   , 
  
IsReadOnly$
, True��6�   + 6]   �� *	PtoEarned+$) PropertyChanged=�6�     5
     -   5        colPtoStart��$% * 5
  >   $& 	PTO Start��6H   $
, True��6�   + 6[   �� *
PtoStart+$) PropertyChanged=�6�     5     -   5        colEndingPto��$% * 5  ?   $& 
Ending PTO��6I   $
, True��6�   + 6]   �� *PtoEnd+$) PropertyChanged=�6�     5     -   5        colStipendPaid��$% * 5  A   $& Stipend Paid��6K   $
, True��6�   + 6a   �� *StipendPaid+$) PropertyChanged=�6�     5     -   5        colYTDHours��$% * 5  >   $& 	YTD Hours��6H   $
, True��6�   + 6[   �� *YearToDateHour+$) PropertyChanged=�6�   5     5       5  
   $��0��6   $��3��6"   $��2��6/   .��5     �  5     �� 5     $��* �6'   �� 5     $��* �6'   �� 5     $��* �6'   �� 5     $��* �6'   �� 5     $��* �6'   �� 5     $��* �6'   �� 5      $��* �6'   �� 5!     $��* �6'   .+�5$     �� -   5        $��lblPTOTotals��5$     
�������$��0q�6?   $��0��6S   $��Center=�6c   $��Center=�6~   .��6�   	Totals:6�   �� 5%     -   5        $��lblPTORegHoursTotal��5%     
�������$��0q�6F   $��1��6Z   $��Center=�6j   $��Center=�6�   .��6�   06�   �� 5&     -   5        $��lblPTOUsedTotal��5&     
�������$��0q�6B   $��2��6V   $��Center=�6f   $��Center=�6�   .��6�   06�   �� 5'     -   5        $��lblPTOEarnedTotal��5'     
�������$��0q�6D   $��3��6X   $��Center=�6h   $��Center=�6�   .��6�   06�   �� 5(     -   5        $��lblPTOStartTotal��5(     
�������$��0q�6C   $��4��6W   $��Center=�6g   $��Center=�6�   .��6�   06�   �� 5)     -   5        $��lblPTOEndingTotal��5)     
�������$��0q�6D   $��5��6X   .��6h   06k   �� 5*     -   5        $��lblPTOStipendPaidTotal��5*     
�������$��0q�6I   $��6��6]   .��6m   06p   �� 5+     -   5        $��lblPTOytdHoursTotal��5+     
�������$��0q�6F   $��7��6Z   .��6j   06m   5,     5-       51  
   $��0��6   $��4��6"   $��2��6/   .��52     �  53     �� 54     $��* �6'   �� 55     $��* �6'   �� 56     $��* �6'   �� 57     $��* �6'    5:     �� 5;     $��* �6$   �� 5<     $��* �6$   .+�5?     �� -    5        $��lblPTOytdStipendPaidTotal��5?     
�������$��0q�6L   $��Right=�6`   $��Center=�6|   $��0��6�   $��0��6�   .��6�   YTD Stipends Paid:6�   �� 5@     -!   5        $%��lblPTOytdStipendPaidTotalNumber��5@     
�������$��0q�6R   $
��Left=�6f   $��Center=�6�   $��1��6�   $��0��6�   .��6�   
$10,025.256�   �� 5A     -"   5        $$��lblPTOytdStipendPaidThisPeriod��5A     
�������$��0q�6Q   $��Right=�6e   $��Center=�6�   $��2��6�   $��0��6�   .��6�   Stipends Payed This Period:6�   �� 5B     -#   5        $*��$lblPTOytdStipendPaidThisPeriodNumber��5B     
�������$��0q�6W   $
��Left=�6k   $��Center=�6�   $��3��6�   $��0��6�   .��6�   $1,0566�   �� 5C     -$   5        $��lblPTOBeginningStipend��5C     
�������$��0q�6I   $��Right=�6]   $��Center=�6y   $��0��6�   $��1��6�   .��6�   Beginning Stipends:6�   �� 5D     -%   5        $"��lblPTOBeginningStipendNumber��5D     
�������$��0q�6O   $
��Left=�6c   $��Center=�6~   $��1��6�   $��1��6�   .��6�   $173,940.006�   �� 5E     -&   5        $��lblPTOStipendRemaining��5E     
�������$��0q�6I   $��Right=�6]   $��Center=�6y   $��2��6�   $��1��6�   .��6�   Stipends Remaining:6�   �� 5F     -'   5        $"��lblPTOStipendRemainingNumber��5F     
�������$��0q�6O   $
��Left=�6c   $��Center=�6~   $��3��6�   $��1��6�   .��6�   $172,884.006�   5G     5I     5L     5N     