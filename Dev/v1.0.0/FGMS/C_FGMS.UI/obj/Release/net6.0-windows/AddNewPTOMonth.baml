   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI      C_FGMS.UI.AddNewPTOMonth   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  -     �Name$  winAddNewMonth�� � Title$ AddNewMonth��$	��600��$	��800��.���  � ColumnDefinitions �� $
��299* ��� $
��501* � � RowDefinitions �� $��5* ��� $��5* ��� $��* �.+�&   System.Windows.Controls.DataGrid  -     Name$ dtgPTO��   CanUserAddRows$ False��   AutoGenerateColumns$ False��   ColumnHeaderHeight$ 25Q�   	RowHeight$ 40Q�
�������	   GridLinesVisibility$
	 None=�
   CanUserResizeColumns$
 False��   
IsReadOnly$
 True��$��
0,265,0,10q�   SelectionMode$ Single=�$��3�� �� AlternationCount$ 2��   AlternatingRowBackground �	#FFD2C7C7$��2��   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"   �� BasedOn# [B  �� Triggers ]�  ]� 
Conditions ��  �� Property �@ 
IsSelected �� Value$ False����  �@ AlternationIndex$ 1��/���  �� Property � �� �� Value
 ���� ]�  ��  �@ 
IsSelected$
 True����  �@ AlternationIndex$ 1��/���  � ��
 �����]�  ��  �@ 
IsSelected$ False����  �@ AlternationIndex$ 0��/���  � ��
 ���� ]�  ��  �@ 
IsSelected$
 True����  �@ AlternationIndex$ 0��/���  � ��
 �r���   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  # [B  ��  �� Binding �� *
IsSelected+ �� RelativeSource ��  �� AncestorType"   �� Value True.a���  � ��
 ���� .���  � �� ��  �� 
TargetType"  .R�� #��z��.+��� $��Center=�$��Center=�   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��
 �r�����  � ��$ Center=���  � ��$ Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  -   colVolunteerName��,  &System.Windows.Controls.DataGridColumn    Width5  /System.Windows.Controls.DataGridLengthConverter$  * !   Header$! Volunteer Name��1  +System.Windows.Controls.DataGridBoundColumn"   Binding" �� *strFullName+  -   colRegHours��$  * $! 	Reg Hours��" �� *RegularHours+  -   
colPtoUsed��$  * $! PTO Used��" �� *	PtoUsed+4	  .System.Windows.Controls.DataGridTemplateColumn	  $  * $! Eligibility��# 	  CellTemplate# �� .R�d�    ToggleButtonSwitch#��[  $
{�True��$ d� 	IsChecked$ �� *IsPTOEligible+� $��	0,163,0,0q�$��2�� �� $	��35* ��� $
��131* ��� $
��128* ��� $
��139* ��� $
��124* ��� $
��118* ��� $
��125* �.+��� -   % ��Name$% lblVolunteer��$��	Volunteer��$��0,10,0,0q�$	��Top=�$��30��$	��120��$��1����Z� �� -   $% lblRegHours��$��Regular Hours��$��0,10,0,0q�$	��Top=�$��30��$	��120��$��2����Z� �� -   $% 
lblPTOUsed��$��PTO Used��$��0,10,0,0q�$	��Top=�$��30��$	��118��$��3����Z� �� -	   & ��Name$& cbobxVolunteer��$��Center=�$��0,55,0,0q�$	��Top=�$	��120��' �� DisplayMemberPath$' FullName��$��30��$��1���� -
   ( ��Name$( txtRegularHours��$��Center=�$��0,55,0,0q�) �� TextWrapping$
) Wrap=�$	��Top=�$	��120��$��30��$��2���� -   $( 
txtPTOUsed��$��0,55,0,0q�$
) Wrap=�$	��Top=�$��30��$��3��$��Center=�$	��120���� -   * ��Name$* btnAddPerson��$��Add To Table��$��0,58,0,0q�$	��Top=�$��98��+ J� RenderTransformOrigin$+ 0.496,0.567&�$��5��$��30��d� -   , d�Name$, chkIsPtoEligible��$��0��$��4��$��Center=�$��0,55,0,0q�$	��Top=�$��48��$��30��$ �� *IsPTOEligible+- �� Mode$- TwoWay=���1
  +HandyControl.Themes.StaticResourceExtension
  *ToggleButtonSwitch+�� -   $% lblPtoEligibility��$��PTO Eligibility��$��0,10,0,0q�$	��Top=�$��30��$	��118��$��4����Z� � $��0,0,0,97q�$��2�� �� $��* ��� $��* ��� $��* �.+��� $��10,10,10,10q�.+��� -   $% lblYear��$
��Year��$��30��$��44����Z� �� -   $% lblYearNumber��$�� ����Z� �� -   $% lblMonth��$��Month��$��30��$��57����Z� ��Z� �� -   $% lblMonthName��$�� ����Z� �� $��10,10,10,10q�$��1��.+�� $��36��.+��� -   $% 
lblPTORate��$��PTO Rate��$��30��$��70��$
��Left=�$	��Top=���Z� ��Z� �� -   $( 
txtPTORate��$
) Wrap=�$��
75,0,100,0q�${�False��� $��50��.+��� -   $% lblCurrentStipendRate��$��Current Stipend Rate��$
��Left=�$��30��$	��133��
���������Z� �� -   $( txtCurrentStipendRate��$
) Wrap=�$��138,10,10,10q�${�False���� -   $��
btnPTOSave��$��0q�$��2��$��0q�$��	0,10,10,0q�$��Right=�$	��Top=�$��35��. � ToolTip$. 	Save Edit��.���� $��\Resources\save-icon.png���� -   $��btnPtoCancel��$��0q�$��0q�$��Right=�$��	0,10,50,0q�$	��Top=�$��2��$. 
Close Page��.���� $ ��\Resources\cancel-icon.png��