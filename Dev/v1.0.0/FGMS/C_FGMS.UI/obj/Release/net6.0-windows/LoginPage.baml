   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters      C_FGMS.UI.LoginPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  #localclr-namespace:C_FGMS.UI  /local1"clr-namespace:C_FGMS.UI.Converters    N� Title$  Login�� N� 
Background
 ����� � 	Resources 6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          .��]� $
��True��.+��� $��0��$��2��$��2�� K� ZIndex$
 1000��$��Hidden=�$��Right=�$��0,0,-2,0q�.���� -    ��Name$ stkGrowl��$	��Top=�$��0,0,-2,0q�  EffectShadow1 J� Effect# [ � $��Bottom=�$��Right=�.+��� -    ��Name$ btnSettings��$��Database Settings��$��8q�� -   $��grdLogin��$��Center=� � ColumnDefinitions �� $��* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto ��� $
��auto �.+��� $��/Resources/TopLogo.jpg��$��Uniform=�$��0��$	��590��#  HandyControl.Controls.TextBox  -   $��txtEmail��'  !HandyControl.Controls.InfoElement	   Placeholder$	 Enter Email��
�������
���   �$	��378��$��1��$��10q����� *Email+"
 �� ValidatesOnNotifyDataErrors$

 True��'  !HandyControl.Controls.PasswordBox  -   $��txtPassword��$	 Enter Password��
�������
���   �$	��378��$��2��$��10q�   ShowEyeButton$
 True���� -   $��Login��$	��218��$��3��$��10q�
���   �$��Stretch=�$��Stretch=����	#FF112542
�������{��� *	HasErrors+ �� 	Converter# [  �� $��Uniform=�$��4��$	��270��$&�� /Resources/AmeriCorpsSeniors.png��