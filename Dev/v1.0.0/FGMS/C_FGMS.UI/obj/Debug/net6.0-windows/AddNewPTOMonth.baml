   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	       C_FGMS.UI.AddNewPTOMonth   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:C_FGMS.UI     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   -   5          �Name$  winAddNewMonth��5   <    � Title$ AddNewMonth��5	   	   $	��600��6   $	��800��6*   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��299* �6   �� 5      $
��501* �6    � RowDefinitions 5   
   �� 5      $��5* �6   �� 5      $��5* �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dtgPTO��5   F      CanUserAddRows$ False��6      AutoGenerateColumns$ False��6*      ColumnHeaderHeight$ 25Q�6V      	RowHeight$ 40Q�6n   
�������	   GridLinesVisibility$
	 None=�5      
   CanUserResizeColumns$
 False��6(      
IsReadOnly$
 True��6E   $��
0,265,0,10q�6W      SelectionMode$ Single=�6�   $��3��6�    �� AlternationCount$ 2��6�      AlternatingRowBackground �	#FFD2C7C7$��2��6�      RowStyle 5'      �� 5(       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� BasedOn# [B 6:    �� Triggers 5.      ]� 5/       ]� 
Conditions 50      �� 51   "    �� Property �@ 
IsSelected �� Value$ False��52      �� 53   "    �@ AlternationIndex$ 1��54      /�56      �� 57   "    �� Property � �� �� Value
 ���� 58      5;      ]� 5=       5>      �� 5?   "    �@ 
IsSelected$
 True��5@      �� 5A   "    �@ AlternationIndex$ 1��5B      /�5D      �� 5E   "    � ��
 �����5F      5H      ]� 5L       5M      �� 5N   "    �@ 
IsSelected$ False��5O      �� 5P   "    �@ AlternationIndex$ 0��5Q      /�5S      �� 5T   "    � ��
 ���� 5U      5W      ]� 5Y       5Z      �� 5[   "    �@ 
IsSelected$
 True��5\      �� 5]   "    �@ AlternationIndex$ 0��5^      /�5`      �� 5a   "    � ��
 �r���5b      5d      5k         	CellStyle 5p      �� 5q      *  $System.Windows.Controls.DataGridCell"  6   # [B 6;    5r      �� 5s       �� Binding 6&   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType"   �� Value True5t      .a�5u      ��  � ��
 ���� 5v      5x      .�5}      ��  � �� 5~      �� 5       �� 
TargetType"  6.   .R�5�   "   � #��z��6'   .+�5�   &   �� $��Center=�67   $��Center=�6T   5�   #   5�      5�      5�         ColumnHeaderStyle 5�      �� 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5�      ��  � ��
 �r���63   �� 5�       � ��$ Center=�6C   �� 5�       � ��$ Center=�6A   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      -   5        colVolunteerName��,  &System.Windows.Controls.DataGridColumn    Width5  /System.Windows.Controls.DataGridLengthConverter$  * 5�   ?   !   Header$! Volunteer Name��6I   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 6a   �� *strFullName+  5�      -   5        colRegHours��$  * 5�   :   $! 	Reg Hours��6D   " 6W   �� *RegularHours+  5�      -   5        
colPtoUsed��$  * 5�   9   $! PTO Used��6C   " 6U   �� *	PtoUsed+4	  .System.Windows.Controls.DataGridTemplateColumn	  5�      $  * 6)   $! Eligibility��63   # 	  CellTemplate# 5�      �� 5�      .R�5�      d�    ToggleButtonSwitch#��[  6P   $
{�True��6|   $ d� 	IsChecked$ 6,   �� *IsPTOEligible+6|   5�      5�      5�      � 5�   
   $��	0,163,0,0q�6   $��2��6"    5�      �� 5�      $	��35* �6#   �� 5�      $
��131* �6#   �� 5�      $
��128* �6#   �� 5�      $
��139* �6#   �� 5�      $
��124* �6#   �� 5�      $
��118* �6#   �� 5�      $
��125* �6#   .+�5�      �� -   5        % ��Name$% lblVolunteer��5�      $��	Volunteer��6*   $��0,10,0,0q�6>   $	��Top=�6P   $��30��6h   $	��120��6t   $��1��6�   ��6�   Z� 6�   �� 5�      -   5        $% lblRegHours��5�      $��Regular Hours��6)   $��0,10,0,0q�6A   $	��Top=�6S   $��30��6k   $	��120��6w   $��2��6�   ��6�   Z� 6�   �� 5�      -   5        $% 
lblPTOUsed��5�      $��PTO Used��6(   $��0,10,0,0q�6;   $	��Top=�6M   $��30��6e   $	��118��6q   $��3��6�   ��6}   Z� 6�   �� 5�      -	   5        & ��Name$& cbobxVolunteer��5�      $��Center=�6/   $��0,55,0,0q�6L   $	��Top=�6^   $	��120��6v   ' �� DisplayMemberPath$' FullName��6�   $��30��6�   $��1��6�   �� 5�      -
   5        ( ��Name$( txtRegularHours��5�      $��Center=�6/   $��0,55,0,0q�6L   ) �� TextWrapping$
) Wrap=�6^   $	��Top=�6r   $	��120��6�   $��30��6�   $��2��6�   6�   �� 5�      -   5        $( 
txtPTOUsed��5�      $��0,55,0,0q�6*   $
) Wrap=�6<   $	��Top=�6P   $��30��6h   $��3��6t   $��Center=�6�   $	��120��6�   6�   �� 5�      -   5        * ��Name$* btnAddPerson��5�      $��Add To Table��6+   $��0,58,0,0q�6B   $	��Top=�6T   $��98��6l   + J� RenderTransformOrigin$+ 0.496,0.567&�6�   $��5��6�   $��30��6�   d� 5�      -   5        , d�Name$, chkIsPtoEligible��5�   '   $��0��5�      $��4��6(   $��Center=�5�   '   $��0,55,0,0q�6D   $	��Top=�6V   $��48��6n   $��30��6�   $ 5�   '   �� *IsPTOEligible+- �� Mode$- TwoWay=���5�   y   1
  +HandyControl.Themes.StaticResourceExtension
  *ToggleButtonSwitch+6�   �� 5�      -   5        $% lblPtoEligibility��5�      $��PTO Eligibility��6/   $��0,10,0,0q�6I   $	��Top=�6[   $��30��6s   $	��118��6   $��4��6�   ��6�   Z� 6�   5�      � 5�   
   $��0,0,0,97q�6   $��2��6!    5�      �� 5�      $��* �6#   �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� $��10,10,10,10q�6   .+�5�      �� -   5        $% lblYear��5�      $
��Year��6)   $��30��68   $��44��6D   ��6O   Z� �� 5�      -   5        $% lblYearNumber��5�      $�� ��6/   ��6:   Z� �� 5�      -   5        $% lblMonth��5�      $��Month��6*   $��30��6:   $��57��6F   ��6Q   Z� ��6g   Z� �� 5�      -   5        $% lblMonthName��5�      $�� ��6.   ��69   Z� 5�      �� 5�      $��10,10,10,10q�6   $��1��6.   .+�5�      � $��36��6   .+�5�      �� -   5        $% 
lblPTORate��5�      $��PTO Rate��60   $��30��6C   $��70��6O   $
��Left=�6�   $	��Top=�6�   ��6Z   Z� ��6q   Z� 6�   �� 5      -   5        $( 
txtPTORate��5      $
) Wrap=�62   $��
75,0,100,0q�6F   ${�False��6Z   5     � 5     $��50��6   .+�5     �� -   5        $% lblCurrentStipendRate��5     $��Current Stipend Rate��6;   $
��Left=�6Z   $��30��6u   $	��133��6�   
���������6�   Z� �� 5     -   5        $( txtCurrentStipendRate��5     $
) Wrap=�6=   $��138,10,10,10q�6Q   ${�False��6g   5     5     �� 5     -   5        $��
btnPTOSave��5     $��0q�6'   $��2��6R   $��0q�6b   $��	0,10,10,0q�6n   $��Right=�6�   $	��Top=�6�   $��35��6�   . � ToolTip$. 	Save Edit��6�   .��5     �� $��\Resources\save-icon.png��6   5     �� 5     -   5        $��btnPtoCancel��5     $��0q�6)   $��0q�6U   $��Right=�6a   $��	0,10,50,0q�6}   $	��Top=�6�   $��2��6�   $. 
Close Page��6�   .��5     �� $ ��\Resources\cancel-icon.png��6   5     5     5$     5%     