   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	   &     C_FGMS.UI.AddNewDemographicsInfo   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:C_FGMS.UI     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	     � Title$"  Add/Delete Demographics Info��5	   	   $	��400��6.   $	��450��6;   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��1.5* �6   .+�5   
   ,  &HandyControl.Controls.SimpleStackPanel  $��0��6   $��0��6+      Orientation$ Vertical=�6;   .+�5      �� $��Choose Category��6   $
��Left=�6/   $��10q�6J   � 5      -   5        $��grdCategory��5      $
��Left=�6&   $��10,0,0,0q�6A    5      �� 5      $��* �6'   �� 5      $��* �6'    5      �� 5      $��* �6$   �� 5      $��* �6$    � 	Resources 5!       �� 
TargetType   RadioButtonBaseStyle �� BasedOn �� Property �� Value��%�   '	�       7   �� 5"      " 	�6   8   �5#      �� 5$       � ��$ 5,5,20,5q�67   �� 5%       � ��$
 Left=�6D   5'      5(      .+�5*      	� -   5        	 	�Name$	 	rdoGender��5*      $��0��61   $��0��6>   
 	� 	GroupName$
 grpCategory��5+      .��6N   Gender6V   	� 5,      -   5        $	 rdoIdentifiesAs��5,      $��0��67   $��1��6D   $
 grpCategory��5-      .��6T   Identifies As6c   	� 5.      -   5        $	 rdoEthnicity��5.      $��1��64   $��0��6A   $
 grpCategory��5/      $��5,5,20,5q�50      $
��Left=�6-   .��6H   	Ethnicity6S   	� 51      -   5        $	 rdoRacialGroup��51      $��1��66   $��1��6C   $
 grpCategory��52      .��6S   Racial Group6a   53      54      � 56   
   $��0��6   $��1��6   $��Right=�6,   $��0,10,0,0q�6H   $	��Top=�6Z    57      �� 58      $��* �6#   �� 59      $��* �6#   .+�5;      �� -   5         ��Name$ btnSave��5;      $��0��5<      $��0��6"   $��0q�62   $��0q�5=      $��5,0,10,0q�6!    � ToolTip$
 Save��5>      .��5?      �� $��/Resources/save-icon.png��6   6@   5@      �� 5A      -   5        $ 	btnDelete��5A      $��0��5B      $��1��6&   $��0q�66   $��0q�5C      $��5,0,10,0q�6%   $ Remove��5D      .��5E      �� $ ��/Resources/delete-icon.png��6   6>   5F      5G        5J   
   $��1��6   $ Vertical=�6+   .+�5K      �� $��Add New Item��6   $��
10,10,10,0q�6,   $
��Left=�6@     5L      $ 
Horizontal=�6"   $��10q�6;   .+�5M      #  HandyControl.Controls.TextBox  -   5          Name$ txtItem��5M      $	��300��6.   $��0,0,20,0q�6:   5O      5P        5S   
   $��2��6   $ Vertical=�6+   .+�5T      �� $��Delete Selected Item(s)��6   $��10,0,0,0q�67     5U      $ 
Horizontal=�6"   $
��Left=�6;   .+�5V      )  #HandyControl.Controls.CheckComboBox  -	   5          Name$ cmbSelectItems��5V   #    �� SelectedValuePath$
 Tuid��5W       �� DisplayMemberPath$
 Name��5X      $	��300��5Y   Q   $��
10,10,20,0q�5Z      +  %HandyControl.Controls.DropDownElement   ConsistentWidth$
 True��62   ��5V   ;   �� *
DataList+ �� SelectedValue 5Y      �� *Tuid+ �� TargetNullValue$  ��5[      5]      5`      5a      5b      