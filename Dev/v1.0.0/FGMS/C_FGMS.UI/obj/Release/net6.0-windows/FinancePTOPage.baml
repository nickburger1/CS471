   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI      C_FGMS.UI.FinancePTOPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  FinancePTOPage��.��� 
������� � ShowGridLines$ False�� � ColumnDefinitions �� $��6* ��� $��* � � RowDefinitions �� $
��1.5* ��� $��* ��� $��5* ��� $��* ��� $��2* �.+�   HandyControl.Controls.Card  $��0��$��0��.��� $ False�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $	��.5* ��� $��1.25* �.+��� -   $��
lblPTOYear��$
��Year��$��0q���Z� ��Z� $  HandyControl.Controls.ComboBox  -   $��cbobxPTOYear��$	��Top=�$��5,5,5,0q�$��1�� �� SelectedIndex$ 0���� -    ��Name$ lblPTOMonths��$��Month��$��0q�$��1��  -     Name$ cbobxPTOMonths��$	��Top=�$��1��$��1��$��5,5,5,0q�$ 0���� $��Stretch=�
���   �$��1,1,1,0q�$��4��$��0�����#F0EFEF._��� -   $��lblPTORates��$��0q����#F0EFEF.��Rates�� $��Stretch=�
���   �$��1,0,1,1q�$��4��$��1�����#F0EFEF._�,  &HandyControl.Controls.SimpleStackPanel     Orientation$ Vertical=�$��4��$��2�����#F0EFEF$��Stretch=�$��Center=�.+�  $ 
Horizontal=�$��Center=�.+��� -   $��lblPTOStipendRate��$��0q����#F0EFEF$��Center=�$	��Top=�.��Stipend Rate:�� -   $��txtBoxPTOStipendRate��${�False��$��0q�$
��3.15�� �� TextAlignment$
 Left=����#F0EFEF$	��Top=�'  !HandyControl.Controls.ButtonGroup  �� $��0,0,0,1q����#F0EFEF  $ 
Horizontal=�$��Center=�.+��� -   $��lblPTOMileageRate��$��0q����#F0EFEF$��Center=�$	��Top=�.��	PTO Rate:�� -	   $��txtBoxPTORate��${�False��$��0q�$
��0.12��$
 Left=����#F0EFEF$	��Top=�� $��1��$��Right=�$	��Top=� �� $��* ��� $��* ��� $��* �.+��� -
   $��
btnPTOEdit��$��0q�$��0��$��0q�$��5,5,5,0q�	 � ToolTip$	 Edit PTO Table��.���� $��\Resources\edit-icon.png���� -   $��btnPrintToExcel��$��0q�$��1��$��0q�$��5,5,5,0q�$	 Export To Excel��.���� $!��\Resources\reports-icon.png���� -   $��
btnRefresh��$��0q�$��2��$��0q�$��5,5,5,0q�$	 Refresh Page��.���� $!��\Resources\refresh-icon.png��  $��1��$��0��$��0q�.��� $��Center=� �� $
��154* ��� $
��154* ��� $
��107* ��� $
��356* � �� $
��Auto �.+��� -   $��lblPTOSearchVolunteer��
�������$��0q�.��Search Volunteer�� -   
 ��Name$
 txtPTOSearchVolunteer��$��1��$��Center=�$��Center=�$	��134��  $��0��$��2��$��2��.��&   System.Windows.Controls.DataGrid  -   $��dtgPTO��   CanUserAddRows$ False��   AutoGenerateColumns$ False��   ColumnHeaderHeight$ 25Q�   	RowHeight$ 40Q�
�������   GridLinesVisibility$
 None=�   CanUserResizeColumns$ False��   
IsReadOnly$
 True��$��Center=�$��Center=�   AlternatingRowBackground �	#FFBEBEBE   SelectionMode$ Single=�   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"   �� BasedOn# [B  �� Triggers ]�  ]� 
Conditions ��  �� Property �@ 
IsSelected �� Value$ False����  �@ AlternationIndex$ 1��/���  �� Property � �� �� Value
 ���� ]�  ��  �@ 
IsSelected$
 True����  �@ AlternationIndex$ 1��/���  � ��
 �����]�  ��  �@ 
IsSelected$ False����  �@ AlternationIndex$ 0��/���  � ��
 ���� ]�  ��  �@ 
IsSelected$
 True����  �@ AlternationIndex$ 0��/���  � ��
 �r���   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  # [B  ��  �� Binding �� *
IsSelected+ �� RelativeSource ��   �� AncestorType"   ! �� Value! True.a���  � ��
 ���� .���  � �� �� " �� 
TargetType""  .R�� #��z��.+��� $��Center=�$��Center=�#   ColumnHeaderStyle# �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��
 �r�����  � ��$ Center=���  � ��$ Center=�$   Columns$ 4	  .System.Windows.Controls.DataGridComboBoxColumn	  -   colVolunteerName��,
  &System.Windows.Controls.DataGridColumn% 
  Width5  /System.Windows.Controls.DataGridLengthConverter$% * & 
  Header$& Volunteer Name��' 	  DisplayMemberPath$' strFullName��( 	  TextBinding( �� *strFullName+) �� UpdateSourceTrigger$) PropertyChanged=�* 	  ItemsSource* �� *strFullName+0  *System.Windows.Controls.DataGridTextColumn  -   colRegHours��$% * $& 	Reg Hours��1  +System.Windows.Controls.DataGridBoundColumn+   Binding+ �� *RegularHours+$) PropertyChanged=�  -   
colPtoUsed��$% * $& PTO Used��+ �� *	PtoUsed+$) PropertyChanged=�  -   colPtoEarned��$% * $& 
PTO Earned��, 
  
IsReadOnly$
, True��+ �� *	PtoEarned+$) PropertyChanged=�  -   colPtoStart��$% * $& 	PTO Start��$
, True��+ �� *
PtoStart+$) PropertyChanged=�  -   colEndingPto��$% * $& 
Ending PTO��$
, True��+ �� *PtoEnd+$) PropertyChanged=�  -   colStipendPaid��$% * $& Stipend Paid��$
, True��+ �� *StipendPaid+$) PropertyChanged=�  -   colYTDHours��$% * $& 	YTD Hours��$
, True��+ �� *YearToDateHour+$) PropertyChanged=�  $��0��$��3��$��2��.���  �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��lblPTOTotals��
�������$��0q�$��0��$��Center=�$��Center=�.��	Totals:�� -   $��lblPTORegHoursTotal��
�������$��0q�$��1��$��Center=�$��Center=�.��0�� -   $��lblPTOUsedTotal��
�������$��0q�$��2��$��Center=�$��Center=�.��0�� -   $��lblPTOEarnedTotal��
�������$��0q�$��3��$��Center=�$��Center=�.��0�� -   $��lblPTOStartTotal��
�������$��0q�$��4��$��Center=�$��Center=�.��0�� -   $��lblPTOEndingTotal��
�������$��0q�$��5��.��0�� -   $��lblPTOStipendPaidTotal��
�������$��0q�$��6��.��0�� -   $��lblPTOytdHoursTotal��
�������$��0q�$��7��.��0  $��0��$��4��$��2��.���  �� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* �.+��� -    $��lblPTOytdStipendPaidTotal��
�������$��0q�$��Right=�$��Center=�$��0��$��0��.��YTD Stipends Paid:�� -!   $%��lblPTOytdStipendPaidTotalNumber��
�������$��0q�$
��Left=�$��Center=�$��1��$��0��.��
$10,025.25�� -"   $$��lblPTOytdStipendPaidThisPeriod��
�������$��0q�$��Right=�$��Center=�$��2��$��0��.��Stipends Payed This Period:�� -#   $*��$lblPTOytdStipendPaidThisPeriodNumber��
�������$��0q�$
��Left=�$��Center=�$��3��$��0��.��$1,056�� -$   $��lblPTOBeginningStipend��
�������$��0q�$��Right=�$��Center=�$��0��$��1��.��Beginning Stipends:�� -%   $"��lblPTOBeginningStipendNumber��
�������$��0q�$
��Left=�$��Center=�$��1��$��1��.��$173,940.00�� -&   $��lblPTOStipendRemaining��
�������$��0q�$��Right=�$��Center=�$��2��$��1��.��Stipends Remaining:�� -'   $"��lblPTOStipendRemainingNumber��
�������$��0q�$
��Left=�$��Center=�$��3��$��1��.��$172,884.00