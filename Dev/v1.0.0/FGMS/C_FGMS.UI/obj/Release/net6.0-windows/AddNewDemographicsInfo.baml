   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  &     C_FGMS.UI.AddNewDemographicsInfo   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$"  Add/Delete Demographics Info��$	��400��$	��450��.���  � ColumnDefinitions �� $��4* ��� $��* � � RowDefinitions �� $��* ��� $��* ��� $
��1.5* �.+�,  &HandyControl.Controls.SimpleStackPanel  $��0��$��0��   Orientation$ Vertical=�.+��� $��Choose Category��$
��Left=�$��10q�� -   $��grdCategory��$
��Left=�$��10,0,0,0q� �� $��* ��� $��* � �� $��* ��� $��* � � 	Resources  �� 
TargetType   RadioButtonBaseStyle �� BasedOn �� Property �� Value��%U   '	�       7   �� " 	�8   ���  � ��$ 5,5,20,5q���  � ��$
 Left=�.+�	� -   	 	�Name$	 	rdoGender��$��0��$��0��
 	� 	GroupName$
 grpCategory��.��Gender	� -   $	 rdoIdentifiesAs��$��0��$��1��$
 grpCategory��.��Identifies As	� -   $	 rdoEthnicity��$��1��$��0��$
 grpCategory��$��5,5,20,5q�$
��Left=�.��	Ethnicity	� -   $	 rdoRacialGroup��$��1��$��1��$
 grpCategory��.��Racial Group� $��0��$��1��$��Right=�$��0,10,0,0q�$	��Top=� �� $��* ��� $��* �.+��� -    ��Name$ btnSave��$��0��$��0��$��0q�$��0q�$��5,0,10,0q� � ToolTip$
 Save��.���� $��/Resources/save-icon.png���� -   $ 	btnDelete��$��0��$��1��$��0q�$��0q�$��5,0,10,0q�$ Remove��.���� $ ��/Resources/delete-icon.png��  $��1��$ Vertical=�.+��� $��Add New Item��$��
10,10,10,0q�$
��Left=�  $ 
Horizontal=�$��10q�.+�#  HandyControl.Controls.TextBox  -     Name$ txtItem��$	��300��$��0,0,20,0q�  $��2��$ Vertical=�.+��� $��Delete Selected Item(s)��$��10,0,0,0q�  $ 
Horizontal=�$
��Left=�.+�)  #HandyControl.Controls.CheckComboBox  -	     Name$ cmbSelectItems�� �� SelectedValuePath$
 Tuid�� �� DisplayMemberPath$
 Name��$	��300��$��
10,10,20,0q�+  %HandyControl.Controls.DropDownElement   ConsistentWidth$
 True������ *
DataList+ �� SelectedValue �� *Tuid+ �� TargetNullValue$  ��