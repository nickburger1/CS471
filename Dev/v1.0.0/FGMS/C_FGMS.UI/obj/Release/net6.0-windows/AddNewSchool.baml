   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI      C_FGMS.UI.AddNewSchool   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  Add New School��$	��500��$	��650��.��� .+��  � RowDefinitions �� $	��43* ��� $
��199* �.+��� $��0��$��2��$��2�� K� ZIndex$
 1000��$��Hidden=�$��Right=�$��0,0,-2,0q�.���� -    ��Name$ stkGrowl��$	��Top=�$��0,0,-2,0q�   EffectShadow1 J� Effect# [  � $��Center=�$	��351��$��0,10,0,0q�$��2��$	��Top=�$	��618��.+��� $
��Left=�$��33,8,0,0q�$
��Wrap=�$��School Name:��$	��Top=�$	��100��$��16���� $
��Left=�$��	33,73,0,0q�$
��Wrap=�$��Address:��$	��Top=�$��16���� $
��Left=�$��
33,148,0,0q�$
��Wrap=�$��City:��$	��Top=�$��16���� $
��Left=�$��
33,220,0,0q�$
��Wrap=�$��State:��$	��Top=�$��16���� $
��Left=�$��
33,290,0,0q�$
��Wrap=�$��	Zip Code:��$	��Top=�$��16���� $
��Left=�$��
355,73,0,0q�$
��Wrap=�$��Phone Number:��$	��Top=�$��16���� $
��Left=�$��355,148,0,0q�$
��Wrap=�$��
Principal:��$	��Top=�$��16���� $
��Left=�$
��Wrap=�$��
Secretary:��$	��Top=�$��16��$��355,220,0,0q��� $
��Left=�$
��Wrap=�$��Status:��$	��Top=�$��16��$��379,285,0,0q��� $
��Left=�$
��Wrap=�$��InActive��$	��Top=�$��16��$��321,318,0,0q��� $
��Left=�$
��Wrap=�$��Active��$	��Top=�$��16��$��435,318,0,0q��� -    ��Name$ txtEditStartTimeLabel��$
��Left=�$
��Wrap=�$��Edit Start Time:��$	��Top=�$��16��$��346,358,0,0q�$y�Hidden=��� -   $ txtEditEndTimeLabel��$
��Left=�$
��Wrap=�$��Edit End Time:��$	��Top=�$��16��$��499,358,0,0q�$y�Hidden=��� -    ��Name$ txtNewSchool��$
��Left=�$��3,30,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��606��$��30���� -   $ 
txtAddress��$
��Left=�$��	3,103,0,0q�$
 Wrap=�$	��Top=�$	��290��$��30���� -   $ txtCity��$
��Left=�$��	3,170,0,0q�$
 Wrap=�$	��Top=�$	��290��$��30���� -    ��Name$ cbxState��$
��Left=�$��30��$��	3,241,0,0q�$	��Top=�$	��145��	 �� SelectedIndex$	 22���� -   $ 
txtZipCode��$
��Left=�$��	3,311,0,0q�$
 Wrap=�$	��Top=�$	��290��$��30���� $
��Left=�$��24��$��321,103,0,0q�$
 Wrap=�$	��1 (��$	��Top=�$��34��${�False���� -	   $ txtPhoneNum1��$
��Left=�$��355,103,0,0q�$
 Wrap=�$	��Top=�$��70��$��30���� $
��Left=�$��24��$��425,103,0,0q�$
 Wrap=�$��)��$	��Top=�$��21��${�False���� -
   $ txtPhoneNum2��$
��Left=�$��446,103,0,0q�$
 Wrap=�$	��Top=�$��70��$��30���� $
��Left=�$��24��$��516,103,0,0q�$
 Wrap=�$��-��$	��Top=�$��21��${�False���� -   $ txtPhoneNum3��$
��Left=�$��537,103,0,0q�$
 Wrap=�$	��Top=�$��74��$��30���� -   $ txtPrincipal��$
��Left=�$��321,170,0,0q�$
 Wrap=�$	��Top=�$	��290��$��30���� -   $ txtSecretary��$
��Left=�$��321,241,0,0q�$
 Wrap=�$	��Top=�$	��290��$��30��d� -   
 d�Name$
 	tglStatus��$
{�True�� d� 	IsChecked$
 TrueY�$
��Left=�$��374,311,0,0q�$	��Top=�$��44��$��30����1  +HandyControl.Themes.StaticResourceExtension  *ToggleButtonSwitch+� $��Center=�$��63��$��	0,289,0,0q�$	��Top=�$	��608��$��1��.+��� $
��Left=�$
��Wrap=�$��Start��$	��Top=�$��	10,32,0,0q��� $
��Left=�$��
164,32,0,0q�$
��Wrap=�$	��End��$	��Top=�&   HandyControl.Controls.TimePicker  -     Name$ tpStart��$
��Left=�$��	39,24,0,0q�$	��Top=�$��29��$	��115��   Text$  ��   
TimeFormat$ HH:mm��  -   $ tpEnd��$
��Left=�$��
187,24,0,0q�$	��Top=�$��30��$	��115��$ HH:mm��� -   $��grdDatePickers��$
��Left=�$��40��$��
317,24,0,0q�$	��Top=�$	��283��.+��� $
��Left=�$��	56,22,0,0q�$
��Wrap=�$��M��$	��Top=��� $
��Left=�$��	99,22,0,0q�$
��Wrap=�$��T��$	��Top=��� $��Center=�$��0,23,0,0q�$
��Wrap=�$��W��$	��Top=��� $
��Left=�$��
175,22,0,0q�$
��Wrap=�$��TH��$	��Top=��� $
��Left=�$��
219,22,0,0q�$
��Wrap=�$��F��$	��Top=��� -    ��Name$ 	chkMonday��$
��Left=�$��54,2,0,0q�$	��Top=��� -   $ 
chkTuesday��$
��Left=�$��94,2,0,0q�$	��Top=��� -   $ chkWednesday��$��Center=�$	��Top=�$��0,2,0,0q��� -   $ chkThursday��$
��Left=�$��	174,2,0,0q�$	��Top=��� -   $ 	chkFriday��$
��Left=�$��	214,2,0,0q�$	��Top=��� $
��Left=�$��28,0,0,0q�$
��Wrap=�$��School Hours:��$	��Top=�$��16��� $��Right=�$	��Top=� � ColumnDefinitions �� $��* ��� $��* ��� $��* �.+��� -    ��Name$ 	btnReport��$��5,5,5,0q�$��Right=�$	��Top=�$��0��$��0q�$��0q� � ToolTip$ Export To Excel��.���� $!��/resources/reports-icon.png���� -   $ btnSave��$��1��$��5,5,5,0q�$��Right=�$	��Top=�$��0q�$��0q�$ Save Changes��.���� $��/resources/save-icon.png���� -   $ 	btnCancel��$	��Top=�$��5,5,5,0q�$��Right=�$��2��$��0q�$��0q�$ Discard Changes��.���� $ ��/resources/cancel-icon.png��