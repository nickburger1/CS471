   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	   ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters  5   	   !    C_FGMS.UI.UpdateActivityLog   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:C_FGMS.UI     5   	   5local1"clr-namespace:C_FGMS.UI.Converters     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	     � Title$  Edit Activity Log��5
   	   $	��400��6#   $	��500��60    � 
ResizeMode$ CanMinimize=�6<    � 	Resources 5      6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          5   
   6-   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � $��Right=�6    5      �� 5       $
��auto �6#   �� 5!      $
��auto �6#   .+�5#      �� -   5         ��Name$ btnSave��5#      $��0��6&   $��5,0,5,0q�66   $��Right=�6G   $	��Top=�6c   ��6�   �� *UpdateCommand+{�6�   �� *	HasErrors+ �� 	Converter# [  .��5$      �� $��/resources/save-icon.png��6    � ToolTip$
 Save��6:   5%      �� 5&      -   5        $ 	btnCancel��5&      $	��Top=�6(   $��5,0,5,0q�6@   $��Right=�6Q   $��1��6m   .��5'      �� $ ��/resources/cancel-icon.png��6   $ Cancel��6<   5(      5)      � 5/   
   $��1��6   $��Center=�6   .+�50      �� $
��Left=�6   $��38,8,0,0q�63   $
��Wrap=�6E   $	��Top=�6|   $	��100��6�   $��16��6�   ��6Y   �� *SelectedVolunteer+6�   51      � 52   
   $��2��6   $��Center=�6   .+�53      &   HandyControl.Controls.DatePicker  -   5        $��txtDate��53   �   $
��Left=�6�   $	��Top=�6�   $	��424��6�   $��10q�6�   (  "HandyControl.Controls.TitleElement   Title$ Select Date:��6�   $��0��6  (  "System.Windows.Controls.DatePicker	   SelectedDate	 6   �� *	NewDate+
 �� UpdateSourceTrigger$
 PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��6  54      � 55   
   $��3��6   $��Center=�6   .+�56      #  HandyControl.Controls.TextBox  -   5        $��
txtInitial��56   �   $
��Left=�6|   $��10q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��424��6�   $ Enter Initials: ��6�    �� MaxLines$ 1��6  $��1��6"  ��6   �� *
NewInitial+$
 PropertyChanged=�$
 True��6"  57      � 58   
   $��4��6   $��Center=�6   .+�59        -   5        $��txtIncident��59   �   $
��Left=�6}   $��10q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��424��6�   $ Enter Incident: ��6�   $ 4��6  $��2��6$  ��6   �� *NewIncident+$
 PropertyChanged=�$
 True��6$  5:      5;      5<      