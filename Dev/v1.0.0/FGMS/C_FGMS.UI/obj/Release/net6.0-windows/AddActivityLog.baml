   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  ;"clr-namespace:C_FGMS.UI.ConvertersC_FGMS.UI.Converters      C_FGMS.UI.AddActivityLog   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     5local1"clr-namespace:C_FGMS.UI.Converters     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  Add Activity Log��$	��400��$	��500�� � 
ResizeMode$ CanMinimize=� � 	Resources 6   0C_FGMS.UI.Converters.BoolToOppositeBoolConverter   BoolToOppositeBoolConverter��%   &	          .���  � ColumnDefinitions �� $��* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�� $��Right=� �� $
��auto ��� $
��auto �.+��� -    ��Name$ btnSave��$��0��$��5,0,5,0q�$��Right=�$	��Top=����� *
AddCommand+{��� *	HasErrors+ �� 	Converter# [  .���� $��/resources/save-icon.png�� � ToolTip$
 Save���� -   $ 	btnCancel��$	��Top=�$��5,0,5,0q�$��Right=�$��1��.���� $ ��/resources/cancel-icon.png��$ Cancel��� $��1��$��Center=�.+��� $
��Left=�$��5q�$
��Wrap=�$	��Top=�$	��100��$��16������ *SelectedVolunteer.FullName+� $��2��$��Center=�.+�&   HandyControl.Controls.DatePicker  -   $��txtDate��$��10q�$
��Left=�$	��Top=�$	��424��(  "HandyControl.Controls.TitleElement   Title$ Select Date:��$��0��(  "System.Windows.Controls.DatePicker	   SelectedDate	 �� *	NewDate+
 �� UpdateSourceTrigger$
 PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��� $��3��$��Center=�.+�#  HandyControl.Controls.TextBox  -   $��
txtInitial��$��10q�$
��Left=� �� TextWrapping$
 Wrap=�$	��Top=�$	��424��$ Enter Initials: �� �� MaxLines$ 1��$��1������ *
NewInitial+$
 PropertyChanged=�$
 True��� $��4��$��Center=�.+�  -   $��txtIncident��$��10q�$
��Left=�$
 Wrap=�$	��Top=�$	��424��$ Enter Incident: ��$ 4��$��2������ *NewIncident+$
 PropertyChanged=�$
 True��