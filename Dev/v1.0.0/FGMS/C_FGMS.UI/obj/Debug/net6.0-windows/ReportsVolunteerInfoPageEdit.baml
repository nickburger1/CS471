   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	   ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters  5   	   ,    &C_FGMS.UI.ReportsVolunteerInfoPageEdit   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:C_FGMS.UI     5   	   5local1"clr-namespace:C_FGMS.UI.Converters     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5	   	     � Title$   Edit Volunteer Information��5
   	   $	��700��6,   $	��800��69    � 
ResizeMode$ CanMinimize=�6F    � 	Resources 5      6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          5   
   6-   5      .��5      �  5   
    �� 
TargetType �� BasedOn1  +HandyControl.Themes.StaticResourceExtension �� Property �� Value��%w   'd�       �� 5      " d�6    6.     *ToggleButtonSwitch+.�5      ��  � ��$ 10q�6+   5      5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5    
   �� 5!      $��* �6   �� 5"      $��* �6   .+�5%   
   � $��1��6   $��Right=�6    5&      �� 5'      $
��auto �6#   �� 5(      $
��auto �6#   .+�5*      �� -   5        	 ��Name$	 btnSave��5*      $��0��6&   $��5,0,5,0q�66   $��Right=�6G   $	��Top=�6c   ��6�   �� *UpdateCommand+{�6�   �� *	HasErrors+
 �� 	Converter#
 [  .��5+      �� $��/resources/save-icon.png��6    � ToolTip$
 Save��6:   5,      �� 5-      -   5        $	 	btnCancel��5-      $	��Top=�6(   $��5,0,5,0q�6@   $��Right=�6Q   $��1��6m   .��5.      �� $ ��/resources/cancel-icon.png��6   $ Cancel��6<   5/      50      � 53   
   $��1��6   $��Stretch=�6    54      �� 55      $��* �6#   �� 56      $��* �6#   .+�58      #  HandyControl.Controls.TextBox  -   5        $��txtFirstName��58   �   $��0��6   $��Stretch=�6)   $��10q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $
��auto��6�   (  "HandyControl.Controls.TitleElement   Title$ First Name: ��6   �� MaxLines$ 1��6&  $��0��63  '  !HandyControl.Controls.InfoElement   Placeholder$ 
First Name��6A  ��6G   �� *	FirstName+ �� UpdateSourceTrigger$ PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��6A    59      -   5        $��txtLastName��59   �   $��1��6   $��Stretch=�6)   $��10q�6�   $
 Wrap=�6�   $	��Top=�6�   $
��auto��6�   $ Last Name: ��6�   $ 1��6"  $��0��6/  $ 	Last Name��6=  ��6G   �� *
LastName+$ PropertyChanged=�$
 True��6=  5:      &   HandyControl.Controls.DatePicker  5<   
   -   5        $��txtDateOfBirth��5<   �   $��2��6   $��0��6%   $��Stretch=�65   $��10q�6�   $	��Top=�6�   $
��auto��6�   $ Select Date of Birth:��6  $��1��63  $ Date of Birth��6A  (  "System.Windows.Controls.DatePicker   SelectedDate 6S   �� *DateOfBirth+$ PropertyChanged=�$
 True��6A  $  HandyControl.Controls.ComboBox  5>   
   -   5          Name$ comboGender��5>      $��3��6+   $��0��68    �� DisplayMemberPath$
 Name��6�    �� SelectedValue$
 Tuid��6�      AutoComplete$
 True��6�    �� 
IsEditable$
 True��6�   $
��auto��6�   $
��auto��6     TitlePlacement$	 Top=�5?   "   $ Gender��6F   $��10q�6d    �� Text$	 All��6p   $ Gender��6{   $��2��6�   ��5>   I   �� *	Genders+ �� SelectedItem 6i   ��  �� Path$ SelectedGender� �� Mode$ TwoWay=�5?   �     5A   
   -   5        $ comboIdentifies��5A      $��4��6/   $��0��6<   $
 Name��6�   $
 Tuid��6�   $
 True��6�   $
 True��6  $
��auto��6  $
��auto��6%  $	 Top=�5B   "   $ Identifies As��6F   $��10q�6k   $	 All��6w   $  Identifies As��6�   $��3��6�   ��5A   M   �� *
Identifies+ 6p   �� $ SelectedIdentifiesAs� �� TargetNullValue$  ��$ TwoWay=�5B   �     5D   
   -   5        $ comboEthnicity��5D      $��5��6.   $��0��6;   $
 Name��6�   $
 Tuid��6�   $
 True��6�   $
 True��6  $
��auto��6  $
��auto��6   $	 Top=�5E   "   $ 	Ethnicity��6F   $��10q�6g   $	 All��6s   $ 	Ethnicity��6~   $��4��6�   ��5D   L   �� *	Ethnicity+ 6n   �� $ SelectedEthnicity�$  ��$ TwoWay=�5E   �     5G   
   -	   5        $ comboRacialGroup��5G      $��6��60   $��0��6=   $
 Name��6�   $
 Tuid��6�   $
 True��6�   $
 True��6  $
��auto��6  $
��auto��6&  $	 Top=�5H   "   $ Racial Group��6F   $��10q�6j   $	 All��6v   $ Racial Group��6�   $��5��6�   ��5G   N   �� *RacialGroup+ 6r   �� $ SelectedRacialGroup�$  ��$ TwoWay=�5H   �   d� 5L   
   -
   5         d�Name$ 
btnVeteran��5M   #   $��1��5L      $��1��6$   $��Veteran��64   $
��Left=�6F   $	 Top=�5M   7   $ Veteran��6[   $��10q�6z   $��6��6�    d� 	IsChecked 6�   �� *	IsVeteran+d� 5N   
   -   5        $ btnFamilyMilitary��5O   #   $��2��5N      $��1��6$   $��Family of Military��64   $
��Left=�6Q   $��7��6l    5O   >   �� *IsFamilyOfMilitary+d� 5P   
   -   5        $ 	btnStatus��5Q   #   $��3��5P      $��1��6$   $��Active��64   $
��Left=�6E   $��8��5Q   6    6C   �� *
IsActive+  5S   
   -   5        $��txtStartDate��5S   �   $��4��6   $��1��6%   $��Stretch=�65   $��10q�6�   $	��Top=�6�   $
��auto��6�   $ Start Date:��6  $��9��6%  $ 
Start Date��62   6S   �� *	StartDate+$ PropertyChanged=�$
 True��62    5T   
   -   5        $��
txtEndDate��5T   �   $��5��6   $��1��6%   $��Stretch=�65   $��10q�6�   $	��Top=�6�   $
��auto��6�   $ 	End Date:��6�   $��10��6      ShowClearButton$
  True��6-  $ End Date��6E   6S   �� *	EndDate+$ PropertyChanged=�$
 True��6E    5U   
   -   5        $ comboSeparated��5U      $��6��6.   $��1��6;   $
 Name��6�   $
 Tuid��6�   $
 True��6�   !   ShowClearButton$
! True��6  $
 True��6'  $
��auto��69  $��60��6F  $	 Top=�5V   "   $ Reason Separated:��6F   $��10q�6o   $  Reason Separated��6{   $��11��6�   ��5U   L   �� *InactiveStatusTypes+ 6x   �� $ SelectedInactiveStatus�$  ��$ TwoWay=�5V   �   5W      5X      