   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	   ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters  6/   &     C_FGMS.UI.DatabaseSettingsDialog   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:C_FGMS.UI     5   	   5local1"clr-namespace:C_FGMS.UI.Converters     6/   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	     � Title$  Database Settings��5	   	   $	��340��6#   $	��400��60    � 
ResizeMode$ NoResize=�6<    � WindowStartupLocation$ CenterScreen=�6R   
������� � 	Resources 5
      6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          5   
   6-   5      .��5      � -   5         �Name$ grdSettings��5       � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    5   
   #  HandyControl.Controls.TextBox �� 
TargetType  TextBoxExtend �� BasedOn	 �� Property
 �� Value �� Triggers P� Property P� Value'  !HandyControl.Controls.PasswordBox  PasswordBoxPlusBaseStyle1  +HandyControl.Themes.StaticResourceExtension    C_FGMS.UI.DatabaseSettings  DataSettings��%�  '        7  '  �    7  '�� {    'd� �    &	 i    �� 5      "  6   8   �5      �� 5      	 � ��

 ���� 63   �� 5      	 � ��$
 1q�68   �� 5      	 � ��$
 Center=�6:   �� 5      	 �@��TextWrapping$

 Wrap=�65   �� 5      	 � ��$
 14(�61   �� 5      	 � ��$
 4q�6/    5       P� 5!       �@=�HasError$
 True��6=   . �5"      �� 	 � ��$
 4,4,4,20q�63   5#      5%      �� 5&      "  6   8   �5'      �� 5(      	 � ��

 ���� 63   �� 5)      	 � ��$
 1q�68   �� 5*      	 � ��$
 Center=�6:   �� 5+      	 � ��$
 14(�61   �� 5,      	 � ��$
 4q�6/   5.      �� 5/      " ��6   �50      �� 51      	 � ��$
 Center=�6:   �� 52      	 � ��$

 Wrap=�65   �� 53      	 � ��$
 5,0,0,2q�6/   �� 54      	 � ��$
 14(�61    56      P� 57       � ��$ 1��65   . �58      �� 	 � ��$
 Center=�6@   59      5;      �� 5<      " d�6    6.     *ToggleButtonSwitch+.�5=      �� 	 � ��$
 5,0,0,0q�6+   �� 5>      	 � y�$
 Visible=�6/   �� 5?      	 � ��$

 Left=�68   5@        5A      6%   5B      .+�5D   
   �� $��0��6   $��2��6$   $��2��64    K� ZIndex$
 1000��6E   $��Hidden=�5E      $��Right=�6<   $��0,0,-2,0q�6X   .��5F      �� -   5         ��Name$ stkGrowl��5F   1   $	��Top=�6   $��0,0,-2,0q�6C     EffectShadow1 J� Effect# [ 6U   5G      � 5I   
   -   5        $��grdDatabaseSettings��5I      $��0��6    5K      �� 5L      $
��auto �6#   �� 5M      $��* �6#    5O      �� 5P      $
��auto �6    �� 5Q      $
��auto �6    �� 5R      $
��auto �6    �� 5S      $
��auto �6    �� 5T      $
��auto �6    �� 5U      $
��auto �6     � DataContext 5X      �� 5Y       �� Source# [ 6   .+�5\      �� $��0��6   $��0��6%   $��Server Name: ��65   �� 5]      $��1��6   $��0��6%   $��
Database: ��65   �� 5^      $��2��6   $��0��6%   $��
Username: ��65   �� 5_      $��3��6   $��0��6%   $��
Password: ��65     5a      '  !HandyControl.Controls.InfoElement   Placeholder$ data.host,1443��6   $��0��6E   $��1��6R   ��6b   ��  �� Path$ 
ServerName� �� Mode$ TwoWay=�  5b      $ 
myDatabase��6   $��1��6A   $��1��6N   ��6^   �� $ DatabaseName�$ TwoWay=�  5c      $ johndoe��6   $��2��6>   $��1��6K   ��6[   �� $ Username�$ TwoWay=�6�     5d      -   5        $��txtPassword��5d      $��3��60   $��1��6=   $ Enter Password��6M   � 5f      $��4��6   $��0��6    $��2��60    5g      �� 5h      $
��auto �6'   �� 5i      $��4* �6'   .+�5l      �� -   5         ��Name$ 
lblVeteran��5m   !   $��0��5l      $��0��6)   $y�Visible=�5m   5   $��Windows Authentication: ��5n   !   $��Center=�6A   d� 5o      -   5         d�Name$! btnUseWindowsAuthentication��5o   <   $��0��6   $��1��6,    d� 	IsChecked 6a   �� $ UseWindowsAuthentication�$ TwoWay=�5p      �� 5q      -   5         ��Name$ 
btnMigrate��5q      $��Migrate��6)   $��0��6;   $��6��6K   $��2��6X   $��4q�6l   $
��Left=�6w   {�6�   �� $ 	HasErrors� �� 	Converter# [  6�   5r      � 5t   
   $��2��6   $��10q�6    5u      �� 5v      $��* �6#   �� 5w      $��* �6#   �� 5x      $��* �6#   .+�5z      �� -   5        $ btnClose��5z      $��Close��6'   $��0��67   $��Right=�6^   �� 5{      -	   5        $ btnTest��5{      $��Test Connection��6&   $��1��6@   6P   �� 5|      -
   5        $ btnSave��5|      $
��Save��6&   $��2��65   $
��Left=�6[   {�6v   �� $ 	HasErrors�# [  5}      5~      5      