   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5   	   #    C_FGMS.UI.AddEditReportPreset   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   #localclr-namespace:C_FGMS.UI  5   	     � Title$  AddEditReportPreset��5	   	   $	��225��6%   $	��600��62   $��14(�5
   	   .��5      �  J� RenderTransformOrigin$ 0.5, 0.5&�6   .+�5   
   '  !HandyControl.Controls.ButtonGroup  $��Right=�6   $��40��65   $	��109��6A   .@�5      �� -   5        $��btnSave��5      $��3��6$   $��Right=�64   $��	-96,0,0,0q�6P   $��42��6c   $��50��6o    � ToolTip$
 Save��6�   .��5      �� $��Resources/save-icon.png��6   $��28��69   $��28��6E   6R   5      �� 5      -   5        $��	btnCancel��5      $��3��6>   $��Right=�6N   $��	-96,0,0,0q�6j   $��42��6}   $��50��6�   $ Cancel��6�   .��5      �� $��Resources/cancel-icon.png��6   $��28��6;   $��28��6G   6T   5      5      �� 5   
   $��0��6   $��2��6$   $��2��64    K� ZIndex$
 1000��6E   $��Hidden=�5      $��Right=�6<   $��0,0,-2,0q�6X   .��5      �� -   5         ��Name$ stkGrowl��5   1   $	��Top=�6   $��0,0,-2,0q�6C      EffectShadow1 J� Effect# [  6U   5      �� 5   
   $
��Left=�6   $��	10,10,0,0q�6/   $
��Wrap=�6B   $��Create/Edit Report Preset��6V   $��32(�6w   $	��Top=�6�   �� 5   
   $
��Left=�6   $��	88,93,0,0q�6/   $
��Wrap=�6B   $��Preset Name:��6V    �� TextAlignment$ Right=�6j   $	��Top=�6�   $	��125��6�   �� 5   
   -   5        $��txtName��5   �   $
��Left=�6   $��
230,88,0,0q�6-    �� TextWrapping$
 Wrap=�6A   $�� ��6U   $	��Top=�6]   $	��225��6u   
�������
���   �6�   5       5!      