   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5      "    C_FGMS.UI.AddOrEditCostShare   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      $	��400��5	      $	��450��6     � Title$  Add School Cost Share��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � $��Right=�6    5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5       �� -   5         ��Name$ btnSave��5       $��0��6&   $��5,0,5,0q�66   $��Right=�6G   $	��Top=�6c   $��0q�6{    � ToolTip$
 Save��6�   $��4��6�   .��5!      �� $��/resources/save-icon.png��6   5"      �� 5#      -   5        $ 	btnCancel��5#      $	��Top=�6(   $��5,0,5,0q�6@   $��Right=�6Q   $��1��6m   $��0q�6}   $ Cancel and discard��6�   $��5��6�   .��5$      �� $ ��/resources/cancel-icon.png��6   5%      �� 5&      -   5        $��btnDeleteCostShare��5&      $��0q�6/   $��2��6d   $��0q�6t   $��5,0,5,0q�6�   $  Delete selected cost share��6�   $��6��6�   .��5'      �� $ ��\Resources\delete-icon.png��6   5(      5)      � 5.   
   $��2��6   $��Center=�6   .+�5/      &   HandyControl.Controls.DatePicker  -   5        $��txtDate��50      $��10q�5/      $
��Left=�6(   $	��Top=�6C   $	��410��6[   (  "HandyControl.Controls.TitleElement   Title$ Select Date:��50   +   $��1��6O   51      � 52   
   $��3��6   $��Center=�6   .+�53      #  HandyControl.Controls.TextBox  -   5        $��txtName��54      $��10q�53      $
��Left=�6%    �� TextWrapping$
 Wrap=�6@   $	��Top=�6T   $	��410��6m   $ Describe Cost Share: ��54   (   '  !HandyControl.Controls.InfoElement   Placeholder$ i.e. '1st Billing'��6U    �� MaxLines$ 1��6�   $��2��6�   55      � 56   
   $��4��6   $��Center=�6   .+�57        -   5        $��txtValue��58      $��10q�57      $
��Left=�6&   $
 Wrap=�6A   $	��Top=�6U   $	��410��6m   $ Cost Share Value: ��58   )   $ i.e. '5000'��6S   $ 4��6|   $��3��6�   59      5:      5;      