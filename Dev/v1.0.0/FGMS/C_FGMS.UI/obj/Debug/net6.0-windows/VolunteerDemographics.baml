   M S B A M L   `   `   `  ����O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  5      D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI 5      %   C_FGMS.UI.VolunteerDemographics   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5	        N� Title$  VolunteerDemographics��5       N� 
Background
 �����.��5      � -   5        $��grdContainerDemographics��5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5       $��* �6    � 	Resources 5$   
      HandyControl.Controls.Card �� 
TargetType   CardBaseStyle �� BasedOn �� Property  EffectShadow2 �� Value#  HandyControl.Controls.TextBox  	BaseStyle#  HandyControl.Controls.Divider  DividerBaseStyle	 �� Triggers
 P� Property P� Value&   HandyControl.Controls.DatePicker  DatePicker.Small$  HandyControl.Controls.ComboBox  ComboBoxPlusBaseStyle(  "HandyControl.Controls.TitleElement(  "HandyControl.Controls.ScrollViewer  ScrollViewerBaseStyle��%�  '        7   '  �     7  '�� T    '      7  '  1    7  '  �    7  '  �    7  �� 5%      "  6   8   �5&      �� 5'       �@J�Effect# �  6/   �� 5(       � ��$ 0q�68   �� 5)       � �� �	#FFF6F6F663   �� 5*       � ��$ 4q�6/   �� 5+       � ��$ 14(�61   5-      �� 5.      "  6   8   �5/      �� 50       � ��
 ���� 63   �� 51       � ��$ 0q�68   �� 52       �@��TextAlignment$ Center=�66   �� 53       � ��$ Center=�6:   �� 54       �@��
IsReadOnly$
 True��63   �� 55       �@��TextWrapping$
 Wrap=�65   �� 56       � ��$ 14(�61   58      �� 59      " ��6   �5:      �� 5;       � ��$ Center=�6:   �� 5<       � ��$
 Wrap=�65   �� 5=       � ��$ 5,0,0,2q�6/   �� 5>       � ��$ 14(�61   5@      �� 5A      "  6   8   �5B      �� 5C       � ��$ 2��68   �� 5D       � ��$ Bottom=�6:   �� 5E       � ��$ 2,0,2,0q�6/   	 5G      P� 5H      
 � ��$ 0��62   . �5I      ��  �@ 
LineStroke
 �   �67   5J      5L      �� 5M      "  6   8   �5N      �� 5O       � ��$ 0,0,4,2q�6/   �� 5P       � ��$ Center=�6:   �� 5Q       � ��
 ���� 63   �� 5R       � ��$ Center=�6A   �� 5S       � ��$ Center=�6C   �� 5T       � ��$ 1��64   �� 5U       � |�$ False��62   �� 5V       �@J�IsHitTestVisible$ False��69   5X      �� 5Y      "  6   8   .�5Z      ��  �@ TitlePlacement$
 Left=�6C   �� 5[       � ��$ 4q�6+   �� 5\       �@ AutoComplete$
 True��61   �� 5]       �@��
IsEditable$
 True��6/   �� 5^       �@ ShowClearButton$
 True��64   �� 5_       � y�$ Hidden=�6/   �� 5`       � ��$ Center=�66   �� 5a       � ��$
 auto��6*   �� 5b       � ��$ 14(�6-   	 5c      P� 5d      
 �@=�HasError$
 True��6=   . �5e      ��  � ��$ 4,4,4,20q�63   5f      5h      �� 5i      "  6   8   .�5j      ��  � ��$
 Auto=�6@   5k      5l      .+�5o   
     -   5        $��cmbVolunteerName��5v   "   $��0��5o      $��0��6#      AutoComplete$
 True��63   $y�Visible=�6G   $
��auto��5p   "   $
��auto��6/   $��10q�6=    �� SelectedValuePath$
 Tuid��5t   "    �� DisplayMemberPath$ FullName��5u   "      ShowClearButton$
 True��5v   :   '  !HandyControl.Controls.InfoElement   Placeholder$ Volunteer Name��5w   "   $��Center=�6�   ��5q   "   �� *VolunteerList+ �� SelectedItem 5r   "   �� *SelectedVolunteer+ �� SelectedValue 5s   "   �� *VolunteerTuid+ �� Mode$ TwoWay=�5w   �   �� 5z   
   -   5         ��Name$ txtEditMode��5z      $��0��5{      $��1��6!   $��	Edit Mode��61   $��Center=�5|      $��Center=�6/   $��30(�5}      $
��Bold"�6"   
���r���$y�Hidden=�5~      � 5�   
   $��0��6   $��3��6   $��Right=�6,   $	��Top=�6H   $��0,10,0,0q�6`    5�      �� 5�      $��* �6#   �� 5�      $��* �6#   �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� -   5         ��Name$ btnEdit��5�      $��2��5�      $��0q�5�      $��0q�6%   $��5,0,5,0q�61    � ToolTip$
 Edit��5�      .��5�      �� $��/Resources/edit-icon.png��6   6<   5�      �� 5�      -   5        $ 	btnFinish��5�      $��1��5�      $y�Hidden=�6!   $��0q�5�      $��0q�6%   $��5,0,5,0q�61   $
 Save��5�      .��5�      �� $��/Resources/save-icon.png��6   6<   5�      �� 5�      -   5        $ 	btnCancel��5�      $��2��5�      $y�Hidden=�6!   $��0q�5�      $��2q�6%   $��5,0,5,0q�61   $ Cancel��5�      .��5�      �� $ ��/Resources/cancel-icon.png��6   6>   5�      �� 5�      -   5        $ 
btnRefresh��5�      $��3��5�      $��0q�5�      $��0q�6%   $��5,0,5,0q�61   $ Refresh Data��5�      .��5�      �� $!��/Resources/refresh-icon.png��6   5�      5�        5�   
   $��1��6   $��0��6   $
��Auto=�6/   .��5�      ��  �� CornerRadius$ 4,4,0,0��6   ._�5�        .��5�      � -   5        $(��"grdVolunteerDemographicInformation��5�       5�        TextBoxExtend1	  +HandyControl.Themes.StaticResourceExtension��%�  '        7  'd� �    '  �    '�� �    �� 5�      "  6$   8   .�5�   "   ��  �@��TextWrapping$
 Wrap=�6A   �� 5�   "    � ��$ 4,4,0,0q�6<   �� 5�   "    �@ TitlePlacement$
 Left=�6S   �� 5�   "    �@ VerticalAlignment$ Stretch=�6V   �� 5�   "    � ��$ 1q�6D   �� 5�   "    � ��
 ���� 6?   �� 5�   "    � ��$ Center=�6F   �� 5�   "    � ��$ 4q�6;   �� 5�   "    �@��
IsReadOnly$
 True��6?   �� 5�   "    � ��$ 14(�6=   5�      �� 5�      " d�6$    6>   	  *ToggleButtonSwitch+.�5�   "   ��  � ��$ 9,0,0,0q�6;   �� 5�   "    � y�$ Hidden=�6?   �� 5�   "    � ��$
 Left=�6H   5�      �� 5�      "  6$    6?   	  *DatePickerExtend+.�5�   "   ��  � ��$ 4,4,0,0q�6<   �� 5�   "    � ��$ 4q�6;   �� 5�   "    � ��
 ���� 6?   �� 5�   "    �@J�IsHitTestVisible$ False��6E   �� 5�   "    �@ TitlePlacement$
 Left=�6S   �� 5�   "    � ��$ 14(�6=   5�      �� 5�      " ��6$   .�5�   "   ��  � ��$
 Wrap=�6A   �� 5�   "    �@��Padding$ 4,4,0,0q�6<   �� 5�   "    � ��$ 4q�6;   �� 5�   "    � ��$ 14(�6=   5�      5�       5�      �� 5�      $��* �6/    5�      �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   �� 5�      $
��auto �6,   .+�5�      �� $��Personal��6$   $��0��64   $��2��6A    �� TextAlignment$ Center=�6U   $
��Bold"�6l     5�      $��0��6%     5�      $��1��6%     5�      $��2��6%     5�      $��3��6%     5�      $��4��6%     5�      $��5��6%     5�      $��6��6%     5�      $��7��6%     5�      $��8��6%     5�      $��9��6%     5�      $��1��6%   $��0��62      Title$ Last Updated:��5�      $ 1/1/2020��5�      ��5�      �� *LastUpdated+ �� StringFormat$ g��  5�      -	   5          Name$ dprDateOfBirth��5�   %   $��2��5�   (   $��0��65   $ Date of Birth: ��5�   %   $ 1/1/1960��5�   %   (
  "System.Windows.Controls.DatePicker 
  Text 5�   %   �� *DateOfBirth+$ TwoWay=�  5�      $��3��6%   $��0��62   $ Age: ��5�      $ 60��5�      ��5�      �� *Age+$ TwoWay=�  5�      -
   5          Name$ 	txtGender��5      $��4��5�   %   $��0��62   $ Gender: ��5�      $
 Male��5     ��5�      �� *
GenderName+5       5     -   5          Name$ 	cmbGender��5	  "   $��4��5  &   $��0��63   $
 Tuid��5  "   $
 Name��5  "   $ Gender: ��5
  "   $  -- Select Gender --��5  "   ��5  "   �� *
GenderList+ 5  "   �� *
GenderTuid+$ TwoWay=� �� Text 5  "   �� *
GenderName+$ OneWayToSource=�5  "     5     -   5        $ txtIdentifiesAs��5     $��5��5  %   $��0��62   $ Identifies As: ��5     $
 Male��5     ��5     �� *IdentityName+5       5     -   5        $ cmbIdentifiesAs��5  "   $��5��5  &   $��0��63   $
 Tuid��5  "   $
 Name��5  "   $ Identifies As: ��5  "   $  -- Select Identity --��5  "   ��5  "   �� *IdentityList+ 5  "   �� *IdentityTuid+$ TwoWay=� 5  "   �� *IdentityName+$ OneWayToSource=�5  "     5      -   5        $ txtEthnicity��5#     $��6��5   %   $��0��62   $ Ethnicity: ��5!     $ Hispanic��5$     ��5"     �� *EthnicityName+5$       5&     -   5        $ cmbEthnicity��5,  "   $��6��5&  &   $��0��63   $
 Tuid��5(  "   $
 Name��5)  "   $ Ethnicity: ��5-  "   $  -- Select Ethnicity --��5.  "   ��5'  "   �� *EthnicityList+ 5*  "   �� *EthnicityTuid+$ TwoWay=� 5+  "   �� *EthnicityName+$ OneWayToSource=�5.  "     51     -   5        $ txtRacialGroup��54     $��7��51  %   $��0��62   $ Racial Group: ��52     $ White��55     ��53     �� *
RacialName+55       56     -   5        $ cmbRacialGroup��5<  "   $��7��56  &   $��0��63   $
 Tuid��58  "   $
 Name��59  "   $ Racial Group: ��5=  "   $   -- Select Racial Group --��5>  "   ��57  "   �� *
RacialList+ 5:  "   �� *
RacialTuid+$ TwoWay=� 5;  "   �� *
RacialName+$ OneWayToSource=�5>  "   � 5B     $��8��6   $��0��6,    5C     �� 5D  "   $
��auto �63   �� 5E  "   $��4* �63   .+�5H       -   5        $ 
txtVeteran��5J  !   $��0��5H  )   $��0��66   $��2��6F   $ 	Veteran: ��5I  !   $	 Yes��5L  !   ��5K  !   �� *	Veteran+5L  !   �� 5N     -   5        $ 
lblVeteran��5O  !   $��0��5N  (   $��0��65   $y�Hidden=�5O  5   $��	Veteran: ��5P  !     �� Padding$  0q�62   $��0,4,4,4q�6>   $��Center=�6O   d� 5Q     -   5        ! d�Name$! 
btnVeteran��5R  !   $��0��5Q  +   $��1��68   " d� 	IsChecked" 6H   �� *	IsVeteran+$ TwoWay=�5R  !   5S     � 5V     $��9��6   $��0��6,    5W     �� 5X  "   $
��auto �63   �� 5Y  "   $��* �63   .+�5\       -   5        $ txtFamilyOfMilitary��5^  %   $��0��5\  )   $��0��66   $��2��6F   $ Family of Military: ��5]  %   $	 Yes��5`  %   ��5_  %   �� *FamilyOfMilitary+5`  %   �� 5b     -   5        $ lblFamilyofMilitary��5c  !   $��0��5b  (   $��0��65   $y�Hidden=�5c  >   $��Family of Military: ��5d  !   $  0q�6=   $��0,4,4,4q�6I   $��Center=�6Z   d� 5e     -   5        $! btnFamilyOfMilitary��5f  !   $��0��5e  +   $��2��68   " 6H   �� *IsFamilyOfMilitary+$ TwoWay=�5f  !   5g     5h     5i     5j     5k       5n  
   $��1��6   $��1��6   .��5o     �� $ 4,4,0,0��6   ._�5p     � -   5        $'��!grdVolunteerDemographicStatusInfo��5p      5r     ��%�  '        7  '  �    7  'd� �    '  �    '�� )    �� 5s     "  6    8   .�5t     ��  � ��$ 4,4,0,0q�68   �� 5u      �@ TitlePlacement$
 Left=�6O   �� 5v      �@ VerticalAlignment$ Stretch=�6R   �� 5w      � ��$ 1q�6@   �� 5x      � ��
 ���� 6;   �� 5y      � ��$ Center=�6B   �� 5z      � ��$ 4q�67   �� 5{      �@��
IsReadOnly$
 True��6;   �� 5|      � ��$ 14(�69   5}     �� 5~     "  6    8   .�5     ��  � ��$ 4,4,0,0q�68   �� 5�      �@ TitlePlacement$
 Left=�6O   �� 5�      � ��$ 4q�67   �� 5�      �@ AutoComplete$
 True��6=   �� 5�      �@��
IsEditable$
 True��6;   �� 5�      �@ ShowClearButton$
 True��6@   �� 5�      � y�$ Hidden=�6;   �� 5�      � ��$ Center=�6B   �� 5�      � ��$
 auto��66   �� 5�      � ��$ 14(�69   	 5�     P� 5�  "   
 �@=�HasError$
 True��6I   . �5�  &   ��  � ��$ 4,4,4,20q�6?   5�  #   5�     �� 5�     " d�6     6:   	  *ToggleButtonSwitch+.�5�     ��  � ��$ 9,0,0,0q�67   �� 5�      � y�$ Hidden=�6;   �� 5�      � ��$
 Left=�6D   �� 5�      � ��$ 14(�69   5�     �� 5�     "  6     6;   	  *DatePickerExtend+.�5�     ��  � ��$ 4,4,0,0q�68   �� 5�      � ��$ 4q�67   �� 5�      � ��
 ���� 6;   �� 5�      �@J�IsHitTestVisible$ False��6A   �� 5�      �@ TitlePlacement$
 Left=�6O   �� 5�      � ��$ 14(�69   5�     �� 5�     " ��6    .�5�     ��  �@��Padding$ 4,4,0,0q�68   �� 5�      � ��$ 4q�67   �� 5�      � ��$ 14(�69   5�     5�      5�     �� 5�     $��* �6+    5�     �� 5�     $
��auto �6(   �� 5�     $
��auto �6(   �� 5�     $
��auto �6(   �� 5�     $
��auto �6(   �� 5�     $
��auto �6(   .+�5�     �� $��Status��6    $��0��6.   $ Center=�6;   $
��Bold"�6R     5�     $��0��6!     5�     $��1��6!     5�     $��2��6!     5�     $��3��6!   � 5�     $��1��6   $��0��6(    5�     �� 5�     $
��auto �6/   �� 5�     $��* �6/   .+�5�       -   5        $ 	txtStatus��5�  %   $��0��5�  %   $��0��62   $��2��6B   $ Status: ��5�  %   $ Active��5�  %   ��5�  %   �� *Active+5�  %   �� 5�     -   5        $ 	lblStatus��5�  $   $��0��5�  $   $��0��61   $y�Hidden=�5�  7   $��Status: ��5�  $   $  0q�64   $��0,4,4,4q�6@   $��Center=�6Q   d� 5�     -   5        $! 	btnStatus��5�  #   $��0��5�  '   $��1��64   " 6D   �� *
IsActive+$ TwoWay=�5�  6   5�       5�     -   5        $ dprSeparationDate��5�  $   $��2��5�  $   $��0��61   $ Separation Date: ��5�  $   $	 N/A��5�  $   # 
  SelectedDate# 5�  $   �� *SeparationDate+$ TwoWay=�  5�     -   5        $ txtReasonsSeparated��5�     $��3��5�  !   $��0��6.   $ Reason: ��5�     $	 N/A��5�     ��5�     �� *ReasonSeparated+5�       5�     -   5        $ cmbReasonsSeparated��5�  "   $��3��5�  "   $��0��6/   $
 Tuid��5�  "   $
 Name��5�  "   $ �� 
IsReadOnly$
$ True��5�  "   $ Reason: ��5�  "   $$  -- Select Reason Separated --��5�  "   ��5�  "   �� *ReasonsList+ 5�  "   �� *SeparationTuid+ 5�  "   �� *ReasonSeparated+$ OneWayToSource=�5�  "   9� 5�     -   5        % 9�Name$% 
expVolName��5�     & 9� 
IsExpanded$& False��63   $��4��6F   $y�Visible=�6S   ' J� IsHitTestVisible$' False��5�  >   ��5�     �� 5�     $��Update Name��6(   $��14(�6;   $
��Bold"�6I   .��5�     �  5�     �� 5�  "   $��* �63    5�     �� 5�  "   $
��auto �60   �� 5�  "   $
��auto �60   .+�5�       $��0��6)   $ First Name:��66   
�������( �� 
IsReadOnly$( False��5�  )   ��6<   �� *	FirstName+  5�     $��1��6)   $ 
Last Name:��66   
�������$( False��5�  )   ��6<   �� *
LastName+5�     5�     5�     5�     5�     5�     5      