   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5      ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters  6-       C_FGMS.UI.LoginPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      #localclr-namespace:C_FGMS.UI  5      /local1"clr-namespace:C_FGMS.UI.Converters  6-     N� Title$  Login��5
       N� 
Background
 ����� � 	Resources 5      6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          5   
   6-   5      .��5      ]� $
��True��6   .+�5   
   �� $��0��6   $��2��6$   $��2��64    K� ZIndex$
 1000��6E   $��Hidden=�5      $��Right=�6<   $��0,0,-2,0q�6X   .��5      �� -   5         ��Name$ stkGrowl��5   1   $	��Top=�6   $��0,0,-2,0q�6C     EffectShadow1 J� Effect# [ 6U   5      � 5   
   $��Bottom=�6   $��Right=�6'   .+�5      �� -   5         ��Name$ btnSettings��5      $��Database Settings��6*   $��8q�6F   6m   5      � 5   
   -   5        $��grdLogin��5      $��Center=�6:    � ColumnDefinitions 5      �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5       $��* �6    �� 5!      $
��auto �6    �� 5"      $
��auto �6    .+�5$      �� $��/Resources/TopLogo.jpg��6   $��Uniform=�64   $��0��6F   $	��590��6S   #  HandyControl.Controls.TextBox  5%      -   5        $��txtEmail��5%      '  !HandyControl.Controls.InfoElement	   Placeholder$	 Enter Email��5'      
�������
���   �$	��378��5*      $��1��5+      $��10q�5,      ��5&      �� *Email+"
 �� ValidatesOnNotifyDataErrors$

 True��5,      '  !HandyControl.Controls.PasswordBox  5-      -   5        $��txtPassword��5-      $	 Enter Password��5.      
�������
���   �$	��378��51      $��2��52      $��10q�53         ShowEyeButton$
 True��54      55      �� 56      -   5        $��Login��57      $	��218��58      $��3��59      $��10q�5:      
���   �$��Stretch=�5>      $��Stretch=�5?      ���	#FF112542
�������{�5<      �� *	HasErrors+ �� 	Converter# [  5?   @   �� 5@      $��Uniform=�6   $��4��6&   $	��270��63   $&�� /Resources/AmeriCorpsSeniors.png��6B   5A      5B      5C      