   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	       C_FGMS.UI.Users_AddNew   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:C_FGMS.UI     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	     � Title$  Create/edit user��5	   	   $	��500��6"   $	��665��6/   $��14(�5
   	   .��5      �  J� RenderTransformOrigin$ 0.5,0.5&�6   .+�5   
   '  !HandyControl.Controls.ButtonGroup  $��Right=�6   $��40��65   $	��109��6A   .@�5      �� -   5        $��btnSave��5      $��3��6$   $��Right=�64   $��	-96,0,0,0q�6P   $��42��6c   $��50��6o    � ToolTip$
 Save��6�   $��0q�6�   .��5      �� $��Resources/save-icon.png��6   $��28��69   $��28��6E   6R   5      �� 5      -   5        $��	btnDelete��5      $��3��6&   $��Right=�66   $��	-96,0,0,0q�6R   $��42��6e   $��50��6q   $ Cancel��6�   $��0q�6�   .��5      �� $��Resources/cancel-icon.png��6   $��28��6;   $��28��6G   6T   5      5      �� 5   
   -   5        $��txtPageHeader��5   �   $
��Left=�6   $��	10,10,0,0q�6/   $
��Wrap=�6B   $��Create/Edit User��6V   $��32(�6n   $	��Top=�6|   6�   �� 5   
   $
��Left=�6   $��	88,95,0,0q�6/   $
��Wrap=�6B   $��Employee Name:��6V    �� TextAlignment$ Right=�6l   $	��Top=�6�   $	��125��6�   �� 5   
   $
��Left=�6   $��
88,145,0,0q�6/   $
��Wrap=�6C   $��Employee Email:��6W   $ Right=�6n   $	��Top=�6�   $	��125��6�   �� 5   
   $
��Left=�6   $��
63,195,0,0q�6/   $
��Wrap=�6C   $��Employee Password:��6W   $ Right=�6q   $	��Top=�6�   $	��150��6�   �� 5    
   $
��Left=�6   $��
88,245,0,0q�6/   $
��Wrap=�6C   $��Re-Enter Password:��6W   $ Right=�6q   $	��Top=�6�   $	��125��6�   �� 5!   
   $
��Left=�6   $��
88,295,0,0q�6/   $
��Wrap=�6C   $��Phone Number:��6W   $ Right=�6l   $	��Top=�6�   $	��125��6�   �� 5"   
   $
��Left=�6   $��
88,345,0,0q�6/   $
��Wrap=�6C   $��Status:��6W   $ Right=�6f   $	��Top=�6|   $	��125��6�   �� 5#   
   $
��Left=�6   $��
88,388,0,0q�6/   $
��Wrap=�6C   $��User level:��6W   $ Right=�6j   $	��Top=�6�   $	��125��6�   #  HandyControl.Controls.TextBox  5'   
   -   5        $��txtName��5'   �   $
��Left=�6   $��
230,88,0,0q�60    �� TextWrapping$ NoWrap=�6D   $�� ��6Z   $	��Top=�6b   $	��300��6z   
�������
���   �6�   �� 5(   
   -   5        $��txtEmail��5(   �   $
��Left=�6   $��230,138,0,0q�6-   $ NoWrap=�6B   $�� ��6X   $	��Top=�6`   $	��300��6x   
�������
���   �6�   '  !HandyControl.Controls.PasswordBox  5)   
   -   5        $��txtPassword��5)   �   $
��Left=�6   $��230,188,0,0q�64   $	��Top=�6I      ShowEyeButton$
 True��6a   $	��300��6v   
�������
���   �6�     5*   
   -   5        $��txtPassword2��5*   �   $
��Left=�6   $��230,238,0,0q�64   $	��Top=�6I   $
 True��6a   $	��300��6v   
�������
���   �6�   �� 5+   
   -   5        $��txtPhoneNum��5+   �   $
��Left=�6   $��230,288,0,0q�6-   $ NoWrap=�6B   $�� ��6X   $	��Top=�6`   $	��300��6x   
�������
���   �6�   	� 5,   
   -	   5        $��	rdoActive��5,   o   $��Active��6   $
��Left=�6'   $��230,347,0,0q�6B   $	��Top=�6W    d� 	IsChecked$
 TrueY�6�   
���   �6�   	� 5-   
   -
   5        $��rdoInactive��5-   q   $��Inactive��6   $
��Left=�6)   $��300,347,0,0q�6D   $	��Top=�6Y   
���   �6�   �� 5.   
   -   5        $��
chkIsAdmin��5.   }   $��Admin��6   $ FalseY�6#   $��230,390,0,0q�65   $	��Top=�6J   $
��Left=�6b   
���   �6�   �� 50   
   -   5         ��Name$ txtErrorName��50      $��230, 118, 0, 0q�6*   $��NoWrap=�6B   $��Error��6X   
���  ��$	��Top=�6v   �� 51   
   -   5        $ txtErrorEmail��51      $��230,168,0,0q�6+   $��NoWrap=�6@   $��Error��6V   
���  ��$	��Top=�6t   �� 52   
   -   5        $ txtErrorPassword��52      $��230,218,0,0q�6.   $��NoWrap=�6C   $��Error��6Y   
���  ��$	��Top=�6w   �� 53   
   -   5        $ txtErrorPassword2��53      $��230,268,0,0q�6/   $��NoWrap=�6D   $��Error��6Z   
���  ��$	��Top=�6x   �� 54   
   -   5        $ txtErrorPhoneNum��54      $��230,318,0,0q�6.   $��NoWrap=�6C   $��Error��6Y   
���  ��$	��Top=�6w   55      56      