   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI      C_FGMS.UI.Users   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  Users�� N� FontSize$ 14(�.��� 
�������.+��  � ColumnDefinitions �� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $��* �.+��� $
��Left=�$��Users��$��32(�$	��Top=�$��8,0,0,0q�&   System.Windows.Controls.DataGrid  -   $��dtgUsers��   
IsReadOnly$
 True��$��1��   SelectionMode$ Single=�   AutoGenerateColumns$ False��$��2��$��Center=�   HeadersVisibility$	 All=� �� AlternationCount$ 2��	   GridLinesVisibility$
	 None=�
   CanUserResizeColumns$
 False��   CanUserResizeRows$ False��$��8,0,8,8q�   ColumnHeaderHeight$ 30Q�   	RowHeight$ 40Q�   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �r�����  � ��$ Center=�   RowStyle �� )  #System.Windows.Controls.DataGridRow"     DataGridRowStyle �� BasedOn# [  .���  � ��
 ����� �� Triggers P�  P� Property �@ AlternationIndex P� Value$ 1��. ���  � ��
 �����P�  �@ 
IsSelected$
 True��. ���  � ��
 �r���   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ 0q���  � ��
 ���� ��  � ��$ Stretch=���  � �� ��  �� 
TargetType"  .R�� #��z��.+��� $��Center=�$��Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Employee Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ Employee Email��$ *  �� *Email+  $ Phone Number��$ *  �� *PhoneNumber+4	  .System.Windows.Controls.DataGridCheckBoxColumn	  $ Active�� 	  IsThreeState$ False��$
 0.5*  �� *
IsActive+	  $ Admin��$ False��$
 0.5*  �� *	IsAdmin+� $��2��$��Right=�$��0,0,8,0q� �� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btnAdd��$��0��$��0q�$��0q�$��5,0,5,0q� � ToolTip$ Add New User��.���� $��/Resources/add-icon.png���� -   $ btnEdit��$ Edit Selected User��$��1��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/edit-icon.png���� -   $ 	btnDelete��$ Delete Selected User��$��2��$��0q�$��0q�$��5,0,5,0q�.���� $ ��/Resources/delete-icon.png��