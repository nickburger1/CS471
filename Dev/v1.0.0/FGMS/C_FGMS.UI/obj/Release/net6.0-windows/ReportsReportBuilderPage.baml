   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  (    "C_FGMS.UI.ReportsReportBuilderPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  ReportsReportBuilderPage��.��� -    �Name$ grdMain��
������� � ColumnDefinitions �� $��2* ��� $��5* � � RowDefinitions �� $��* ��� $	��10* ��� $��* � � 	Resources    HandyControl.Controls.Card �� 
TargetType   CardBaseStyle �� BasedOn �� Property  EffectShadow1 �� Value&   HandyControl.Controls.DatePicker  DatePickerExtendBaseStyle#  HandyControl.Controls.Divider  DividerBaseStyle��%�  '        7   '�� w     '  �     7  '�� �     '  Z    7  �� "  8   ���  �@J�Effect# �  ��  � ��$ 1q���  � �� �	#FFF7F7F7��  � ��$ 15q��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 5,0,0,5q��� "  8   ���  � ��$ 3q��� " �����  � ��$ LeftToRight=���  � ��$ 5,5,5,5q���  � ��$
 Left=��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q�.+�� $��2��$��Right=� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -   	 ��Name$	 btnAdd��$��0��
 � ToolTip$
 
Add Preset��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/add-icon.png���� -   $	 btnEdit��$
 Edit Preset��$��1��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/edit-icon.png���� -   $	 	btnDelete��$
 Delete selected preset��$��2��$��0q�$��0q�$��5,0,5,0q����� *ConfirmDeleteCommand+.���� $ ��/Resources/delete-icon.png���� -   $	 
btnRefresh��$
 Refresh Data��$��3��$��0q�$��0q�$��5,0,5,0q�.���� $!��\Resources\refresh-icon.png��  $��1��$��0��.����  �� CornerRadius$ 4,4,0,0��._��  �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�  $��0��  $��1��  $��2��  $��3��  $��4��  $��5��  $��6��  $��7��$  HandyControl.Controls.ComboBox  -   $��cmbSelectPreset��$��0��$��0��   AutoComplete$
 True��$
��auto��$
��auto��$��10q�$��Center=�'  !HandyControl.Controls.InfoElement   Placeholder$ Presets�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$
 Name��   ShowClearButton$
 True������ *reportPresets+ �� SelectedValue �� *Tuid+ �� TargetNullValue$  ��  -   $��	cmbSortBy��$��1��$��0��$
 True��$
��auto��$
��auto��$��10q�$��Center=�$ 
Sort By...��$
 True��� $��2��$��0��$��5q�$
��Left=�$��Center=� �� $��* ��� $��* � �� $��* ��� $��* �.+��� -   $��
chkCurrent��$��0��$��0��$��Current�� d� 	IsChecked �� *
chkCurrent+ �� Mode$ TwoWay=��� -	   $��	chkFormer��$��0��$��1��$��Former�� �� *	chkFormer+$ TwoWay=��� -
   $��	chkActive��$��1��$��0��$��Active�� �� *	chkActive+$ TwoWay=��� -   $��chkInactive��$��1��$��1��$��Inactive�� �� *chkInactive+$ TwoWay=��� -   $��chkSelectAllVolunteers��$��3��$��0��$��Select All Volunteers?��$��Bottom=�$��Center=�$��5,10,5,5q�  -   $��cmbSelectVolunteer��$��4��$��0��$
 True��$
��auto��$
��auto��$��10q�$��Center=�$
 Tuid��$ FullName��$ Volunteer Name��$
 True������ *Names+ �� *Tuid+$  ���� $��5��$��0��$��Time Range: ��$��Bottom=�$��5,10,0,5q�� $��6��$��0�� �� $��* ��� $��2* � �� $��* ��� $��* �.+��� $��0��$��0��$��Start Date: ��  -   $��dtpStartDate��$��0��$��1���� $��1��$��0��$��
End Date: ��$��Center=�  -   $��
dtpEndDate��$��1��$��1��  $��0��$��1��$y�Hidden=�.��$  HandyControl.Controls.Transfer  -   $��ReportBuilderTransfer������ *
DataList+���� .R��� ���� *
Key.Header+  $��1��$��1��.���  �� $��3* ��� $��* ��� $��3* �.+�R� -   $��TreeViewStructure��$��0������  �� ItemsSource ��  �� Path$ Value� �� ItemTemplate �� .R��� ���� $
 Name�.R��� $��Center=����� *Key+� $��1�� �� $��4* ��� $��* ��� $��* ��� $��* ��� $��4* �.+��� -   $��btnAddAttribute��$��>��$��1���� -   $��btnRemoveAttribute��$��<��$��3��R� -   $��TreeViewReport��$��2������  �� $ Value� �� .R��� ���� $
 Name�.R��� $��Center=����� *Key+�� -   $	 btnGenerateReport��$��2��$��1��$��Generate Report��$��Right=�$��0,0,14,0q�