   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5           C_FGMS.UI.AddOrEditExpense   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      #localclr-namespace:C_FGMS.UI  5      $	��400��5	      $	��450��6     � Title$  Add Or Edit Expense��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � $��Right=�6    5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5       $��* �6#   .+�5"      �� -   5         ��Name$ btnSave��5"      $��0��6&   $��5,0,5,0q�66   $��Right=�6G   $	��Top=�6c   $��0q�6{    � ToolTip$
 Save��6�   .��5#      �� $��/resources/save-icon.png��6   5$      �� 5%      -   5        $ 	btnCancel��5%      $	��Top=�6(   $��5,0,5,0q�6@   $��Right=�6Q   $��1��6m   $��0q�6}   $ Cancel and discard��6�   .��5&      �� $ ��/resources/cancel-icon.png��6   5'      �� 5(      -   5        $��btnDeleteExpense��5(      $��0q�6-   $��2��6a   $��0q�6q   $��5,0,5,0q�6}   $��2��6�   $  Delete selected cost share��6�   .��5)      �� $ ��\Resources\delete-icon.png��6   5*      5+      � 5,   
   $��0��6   $
��Left=�6    5-      �� 5.      $��* �6#   �� 5/      $��* �6#   .+�51      	� -   5        $��rdbDonation��51   J   $��Donation��6   $��0��6-   $	��5,0q�6=   6J   	� 52      -   5        $��rdbOther��52   O   $��Other In-Kind��6   $��1��62   $	��5,0q�6B   6O   53      �� 55   
   $��0��6   $��2��6$   $��2��64    K� ZIndex$
 1000��6E   $��Hidden=�56      $��Right=�6<   $��0,0,-2,0q�6X   .��57      �� -   5         ��Name$ stkGrowl��57   1   $	��Top=�6   $��0,0,-2,0q�6C      EffectShadow1 J� Effect# [  6U   58      � 5<   
   $��2��6   $��Center=�6   .+�5=      &   HandyControl.Controls.DatePicker  -   5        $��txtDate��5>      $��10q�5=      $
��Left=�6(   $	��Top=�6C   $	��410��6[   (  "HandyControl.Controls.TitleElement   Title$ Select Date:��5>   +   $��1��6O   5?      � 5@   
   $��3��6   $��Center=�6   .+�5A      #  HandyControl.Controls.TextBox  -   5        $��txtValue��5B      $��10q�5A      $
��Left=�6%   	 �� TextWrapping$
	 Wrap=�6@   $	��Top=�6T   $	��410��6l   $  A decimal or integer value��6y   $ Expense Value: ��5B   )   '  !HandyControl.Controls.InfoElement
   Placeholder$
 i.e. '5000'��6P    �� MaxLines$ 4��6y   $��2��6�   5C      � 5D   
   $��4��6   $��Center=�6   .+�5E      $  HandyControl.Controls.ComboBox  -	   5        $��cmbExpenseType��5F      $��10q�5E      $
��Left=�6&   $	��Top=�6A   $	��410��6Y   $ Expense Type: ��5F   0    �� SelectedValuePath$
 Tuid��6{    �� DisplayMemberPath$
 Name��5G      $! Expense type for this entry��68   $��3��6^   ��5F   V   �� *expenseTypes+5G   ^     5H      -
   5        $��cmbDonationType��5I      $��10q�5H      $
��Left=�6&   $	��Top=�6A   $	��410��6Y   $ Donation Type: ��5I   1   $
 Tuid��6~   $
 Name��5J      $! Expense type for this entry��68   $��3��6^   ��5I   X   �� *donationTypes+5J   ^   5K      � 5L   
   $��5��6   $��Center=�6   .+�5M        -   5        $��cmbVolunteerName��5N      $��10q�5M      $
��Left=�6&   $	��Top=�6A   $	��410��6Y   $ Volunteer for this entry��6e   $ Volunteer Name: ��5N   2      ShowClearButton$
 True��6Z   $
 Tuid��5O   @   $ FullName��6Y   $��4��6v   ��6   �� *VolunteerList+6v     5P      -   5        $��txtDonorName��5Q      $��10q�5P      $
��Left=�6%   $	��Top=�6@   $	��410��6X   $ Donor for this entry��6d   $ Donor Name: ��5Q   .   $��4��5R      5S      5U      5V      