   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters  &     C_FGMS.UI.DatabaseSettingsDialog   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     5local1"clr-namespace:C_FGMS.UI.Converters     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  Database Settings��$	��340��$	��400�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�
������� � 	Resources 6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          .��� -    �Name$ grdSettings�� � ColumnDefinitions �� $��* � � RowDefinitions �� $
��auto ��� $��* ��� $
��auto � #  HandyControl.Controls.TextBox �� 
TargetType  TextBoxExtend �� BasedOn	 �� Property
 �� Value �� Triggers P� Property P� Value'  !HandyControl.Controls.PasswordBox  PasswordBoxPlusBaseStyle1  +HandyControl.Themes.StaticResourceExtension    C_FGMS.UI.DatabaseSettings  DataSettings��%�  '        7  '  �     7  '�� s    'd�     &	 �    �� "  8   ��� 	 � ��

 ���� �� 	 � ��$
 1q��� 	 � ��$
 Center=��� 	 �@��TextWrapping$

 Wrap=��� 	 � ��$
 14(��� 	 � ��$
 4q� P�  �@=�HasError$
 True��. ��� 	 � ��$
 4,4,4,20q��� "  8   ��� 	 � ��

 ���� �� 	 � ��$
 1q��� 	 � ��$
 Center=��� 	 � ��$
 14(��� 	 � ��$
 4q��� " ����� 	 � ��$
 Center=��� 	 � ��$

 Wrap=��� 	 � ��$
 5,0,0,2q��� 	 � ��$
 14(� P�  � ��$ 1��. ��� 	 � ��$
 Center=��� " d�   *ToggleButtonSwitch+.��� 	 � ��$
 5,0,0,0q��� 	 � y�$
 Visible=��� 	 � ��$

 Left=�  .+��� $��0��$��2��$��2�� K� ZIndex$
 1000��$��Hidden=�$��Right=�$��0,0,-2,0q�.���� -    ��Name$ stkGrowl��$	��Top=�$��0,0,-2,0q�  EffectShadow1 J� Effect# [ � -   $��grdDatabaseSettings��$��0�� �� $
��auto ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � � DataContext ��  �� Source# [ .+��� $��0��$��0��$��Server Name: ���� $��1��$��0��$��
Database: ���� $��2��$��0��$��
Username: ���� $��3��$��0��$��
Password: ��  '  !HandyControl.Controls.InfoElement   Placeholder$ data.host,1443��$��0��$��1������  �� Path$ 
ServerName� �� Mode$ TwoWay=�  $ 
myDatabase��$��1��$��1������ $ DatabaseName�$ TwoWay=�  $ johndoe��$��2��$��1������ $ Username�$ TwoWay=�  -   $��txtPassword��$��3��$��1��$ Enter Password��� $��4��$��0��$��2�� �� $
��auto ��� $��4* �.+��� -    ��Name$ 
lblVeteran��$��0��$��0��$y�Visible=�$��Windows Authentication: ��$��Center=�d� -    d�Name$! btnUseWindowsAuthentication��$��0��$��1�� d� 	IsChecked �� $ UseWindowsAuthentication�$ TwoWay=��� -    ��Name$ 
btnMigrate��$��Migrate��$��0��$��6��$��2��$��4q�$
��Left=�{��� $ 	HasErrors� �� 	Converter# [  � $��2��$��10q� �� $��* ��� $��* ��� $��* �.+��� -   $ btnClose��$��Close��$��0��$��Right=��� -	   $ btnTest��$��Test Connection��$��1���� -
   $ btnSave��$
��Save��$��2��$
��Left=�{��� $ 	HasErrors�# [  