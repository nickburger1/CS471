   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI      C_FGMS.UI.Users_AddNew   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  Create/edit user��$	��500��$	��665��$��14(�.���  J� RenderTransformOrigin$ 0.5,0.5&�.+�'  !HandyControl.Controls.ButtonGroup  $��Right=�$��40��$	��109��.@��� -   $��btnSave��$��3��$��Right=�$��	-96,0,0,0q�$��42��$��50�� � ToolTip$
 Save��$��0q�.���� $��Resources/save-icon.png��$��28��$��28���� -   $��	btnDelete��$��3��$��Right=�$��	-96,0,0,0q�$��42��$��50��$ Cancel��$��0q�.���� $��Resources/cancel-icon.png��$��28��$��28���� -   $��txtPageHeader��$
��Left=�$��	10,10,0,0q�$
��Wrap=�$��Create/Edit User��$��32(�$	��Top=��� $
��Left=�$��	88,95,0,0q�$
��Wrap=�$��Employee Name:�� �� TextAlignment$ Right=�$	��Top=�$	��125���� $
��Left=�$��
88,145,0,0q�$
��Wrap=�$��Employee Email:��$ Right=�$	��Top=�$	��125���� $
��Left=�$��
63,195,0,0q�$
��Wrap=�$��Employee Password:��$ Right=�$	��Top=�$	��150���� $
��Left=�$��
88,245,0,0q�$
��Wrap=�$��Re-Enter Password:��$ Right=�$	��Top=�$	��125���� $
��Left=�$��
88,295,0,0q�$
��Wrap=�$��Phone Number:��$ Right=�$	��Top=�$	��125���� $
��Left=�$��
88,345,0,0q�$
��Wrap=�$��Status:��$ Right=�$	��Top=�$	��125���� $
��Left=�$��
88,388,0,0q�$
��Wrap=�$��User level:��$ Right=�$	��Top=�$	��125��#  HandyControl.Controls.TextBox  -   $��txtName��$
��Left=�$��
230,88,0,0q� �� TextWrapping$ NoWrap=�$�� ��$	��Top=�$	��300��
�������
���   ��� -   $��txtEmail��$
��Left=�$��230,138,0,0q�$ NoWrap=�$�� ��$	��Top=�$	��300��
�������
���   �'  !HandyControl.Controls.PasswordBox  -   $��txtPassword��$
��Left=�$��230,188,0,0q�$	��Top=�   ShowEyeButton$
 True��$	��300��
�������
���   �  -   $��txtPassword2��$
��Left=�$��230,238,0,0q�$	��Top=�$
 True��$	��300��
�������
���   ��� -   $��txtPhoneNum��$
��Left=�$��230,288,0,0q�$ NoWrap=�$�� ��$	��Top=�$	��300��
�������
���   �	� -	   $��	rdoActive��$��Active��$
��Left=�$��230,347,0,0q�$	��Top=� d� 	IsChecked$
 TrueY�
���   �	� -
   $��rdoInactive��$��Inactive��$
��Left=�$��300,347,0,0q�$	��Top=�
���   ��� -   $��
chkIsAdmin��$��Admin��$ FalseY�$��230,390,0,0q�$	��Top=�$
��Left=�
���   ��� -    ��Name$ txtErrorName��$��230, 118, 0, 0q�$��NoWrap=�$��Error��
���  ��$	��Top=��� -   $ txtErrorEmail��$��230,168,0,0q�$��NoWrap=�$��Error��
���  ��$	��Top=��� -   $ txtErrorPassword��$��230,218,0,0q�$��NoWrap=�$��Error��
���  ��$	��Top=��� -   $ txtErrorPassword2��$��230,268,0,0q�$��NoWrap=�$��Error��
���  ��$	��Top=��� -   $ txtErrorPhoneNum��$��230,318,0,0q�$��NoWrap=�$��Error��
���  ��$	��Top=�