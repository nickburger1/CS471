   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI       C_FGMS.UI.AddOrEditExpense   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  #localclr-namespace:C_FGMS.UI  $	��400��$	��450��  � Title$  Add Or Edit Expense��.���  � ColumnDefinitions �� $��* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�� $��Right=� �� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnSave��$��0��$��5,0,5,0q�$��Right=�$	��Top=�$��0q� � ToolTip$
 Save��.���� $��/resources/save-icon.png���� -   $ 	btnCancel��$	��Top=�$��5,0,5,0q�$��Right=�$��1��$��0q�$ Cancel and discard��.���� $ ��/resources/cancel-icon.png���� -   $��btnDeleteExpense��$��0q�$��2��$��0q�$��5,0,5,0q�$��2��$  Delete selected cost share��.���� $ ��\Resources\delete-icon.png��� $��0��$
��Left=� �� $��* ��� $��* �.+�	� -   $��rdbDonation��$��Donation��$��0��$	��5,0q�	� -   $��rdbOther��$��Other In-Kind��$��1��$	��5,0q��� $��0��$��2��$��2�� K� ZIndex$
 1000��$��Hidden=�$��Right=�$��0,0,-2,0q�.���� -    ��Name$ stkGrowl��$	��Top=�$��0,0,-2,0q�   EffectShadow1 J� Effect# [  � $��2��$��Center=�.+�&   HandyControl.Controls.DatePicker  -   $��txtDate��$��10q�$
��Left=�$	��Top=�$	��410��(  "HandyControl.Controls.TitleElement   Title$ Select Date:��$��1��� $��3��$��Center=�.+�#  HandyControl.Controls.TextBox  -   $��txtValue��$��10q�$
��Left=�	 �� TextWrapping$
	 Wrap=�$	��Top=�$	��410��$  A decimal or integer value��$ Expense Value: ��'  !HandyControl.Controls.InfoElement
   Placeholder$
 i.e. '5000'�� �� MaxLines$ 4��$��2��� $��4��$��Center=�.+�$  HandyControl.Controls.ComboBox  -	   $��cmbExpenseType��$��10q�$
��Left=�$	��Top=�$	��410��$ Expense Type: �� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$
 Name��$! Expense type for this entry��$��3������ *expenseTypes+  -
   $��cmbDonationType��$��10q�$
��Left=�$	��Top=�$	��410��$ Donation Type: ��$
 Tuid��$
 Name��$! Expense type for this entry��$��3������ *donationTypes+� $��5��$��Center=�.+�  -   $��cmbVolunteerName��$��10q�$
��Left=�$	��Top=�$	��410��$ Volunteer for this entry��$ Volunteer Name: ��   ShowClearButton$
 True��$
 Tuid��$ FullName��$��4������ *VolunteerList+  -   $��txtDonorName��$��10q�$
��Left=�$	��Top=�$	��410��$ Donor for this entry��$ Donor Name: ��$��4��