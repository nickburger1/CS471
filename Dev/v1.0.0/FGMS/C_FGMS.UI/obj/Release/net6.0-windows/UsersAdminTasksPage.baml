   M S B A M L   `   `   `  ���� D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  #    C_FGMS.UI.UsersAdminTasksPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  UsersAdminTasksPage��.��� -   $��grdContainerAdmin�� � ColumnDefinitions �� $��* ��� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $��2* ��� $��5 � � 	Resources    HandyControl.Controls.Card �� 
TargetType   CardBaseStyle �� BasedOn �� Property  EffectShadow2 �� Value#  HandyControl.Controls.Divider  DividerBaseStyle �� Triggers	 P� Property
 P� Value��%�  '        7   '�� }     '  �     7  �� "  8   ���  �@J�Effect# �  ��  � ��$ 0q���  � �� �	#FFF6F6F6��  � ��$ 4,10,4,0q��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 5,0,0,2q��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q� P� 	 � ��$
 0��. ���  �@ 
LineStroke
 �   �.+�  $��1��$��0��$
��auto��$��Stretch=�.����  �� CornerRadius$ 4,4,0,0��._�� $��2��$��0�� �� $��* ��� $��* � �� $
��auto ��� $
��auto �$	��158���� .+��� $ ��Add/Delete Type Categories��$��0��$��2�� �� TextAlignment$ Center=�$
��Bold"�$��0,4,4,4q�,  &HandyControl.Controls.SimpleStackPanel  $��1��$��0��   Orientation$ Vertical=�$��2��$��0,0,0, 0q�.+��� $��Choose Category��$
��Left=�$��10q�� -   $��grdCategory��$
��Left=�$��	10,0,0,-5q�$	��251�� �� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* �   RadioButtonBaseStyle��%U   '	�       7  �� " 	�8   ���  � ��$ 5,5,20,5q���  � ��$
 Left=�.+�	� -    	�Name$ 	rdoGender��$��0��$��0�� d� 	IsChecked$
 TrueY� 	� 	GroupName$ grpCategory��.��Gender	� -   $ rdoIdentifiesAs��$��0��$��1��$ grpCategory��$ FalseY�.��Identifies As	� -   $ rdoEthnicity��$��1��$��0��$ grpCategory��$��5,5,20,5q�$
��Left=�$ FalseY�.��	Ethnicity	� -   $ rdoRacialGroup��$��1��$��1��$ grpCategory��$ FalseY�.��Racial Group	� -   $ 
rdoExpense��$��2��$��0��$ FalseY�$ grpCategory��.��	Expense	� -   $ 	rdoReason��$��2��$��1��$ FalseY�$ grpCategory��.��Depart Reason	� -	   $ rdoTempInfo��$��3��$��0��$ FalseY�$ grpCategory��.��	Temp Info��  �� Orientation$ 
Horizontal=�.+��� $��Center=�$��17��
Add New:�� -
    ��Name$ 	txtAddNew��$��5q�(  "HandyControl.Controls.TitleElement   Title$ 	Add New: ��$	��147������ *CurrentStipendRate+ �� Mode$ TwoWay=��� -   $	��Add��� $��2��$��	10,0,0,-5q� �� $��* ��� $��* � ��%L   '	�       �� " 	����  � ��$ 5,5,20,5q���  � ��$
 Left=�.+�	� -   $ rdoDate��$
��Date��$ grpTempType��$��0������ " 	�.���  � y�$ 	Collapsed=� ��  �� Binding �� *	IsChecked+ �� ElementName$ rdoTempInfo�� �� Value True.a���  � y�$ Visible=�	� -   $ rdoCheckBox��$��CheckBox��$ grpTempType��$��1������ " 	�.���  � y�$ 	Collapsed=� ��  �� *	IsChecked+$ rdoTempInfo�� True.a���  � y�$ Visible=�&   System.Windows.Controls.DataGrid  -   $��dtgDemographics��$��2��$��0��$��2��   CanUserAddRows$ False��   AutoGenerateColumns$ False��   ColumnHeaderHeight$ 25Q�   	RowHeight$ 40Q�
�������   GridLinesVisibility$
 None=�   CanUserResizeColumns$ False��   AlternatingRowBackground
 ����� �� AlternationCount$ 2��    
IsReadOnly$
  True��$��0,27,0,1q�!   ColumnHeaderStyle! �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��
 �r�����  � ��$ Center=�"   	CellStyle" �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�#   Columns# 0  *System.Windows.Controls.DataGridTextColumn  ,	  &System.Windows.Controls.DataGridColumn$ 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$$ *
 -   colName��% 	  Header$% Gender��1  +System.Windows.Controls.DataGridBoundColumn&   Binding& �� *Name+4  .System.Windows.Controls.DataGridTemplateColumn  $% Action��$	$ .5*
 '   CellTemplate' �� .R��� -   .���� $ ��/Resources/delete-icon.png��  $��1��$��1��$
��auto��$��Stretch=�.���� $ 4,4,0,0��._�� $��2��$��0��( J� RenderTransformOrigin$( 0.499,1.014&� �� $��* � �� $
��auto ��� $��* ��� $
��auto ��� $��* ��� $
��auto ��� $��* � ��%    .+��� $��Deleted Volunteers��$ Center=�$
��Bold"�$��0,2,4,0q�$��16��  -   $��
dtgDeleted��$��1��$��0��$ False��$ False��$ 25Q�$ 40Q�
�������$
 None=�$ False��
 �����$ 2��$
  True��$��0,0,0,0q�! �� "  .���  � ��
 �r�����  � ��$ Center=�" �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�#   $$ *
 $
% Name��& �� *
FullName+  $$ *
 $	% DOB��& �� *
Birthday+  $% Action��$	$ .5*
 ' �� .R��� -   .���� $��/Resources/add-icon.png���� $��2��$��Student Needs��$ Center=�$
��Bold"�$��0,2,4,0q�$��16��  -   $��dtgNeeds��$��3��$��0��$ False��$ False��$ 25Q�$ 40Q�
�������$
 None=�$ False��
 �����$ 2��$
  True��$��0,0,0,0q����� *StudentNeeds+) �� SelectedItem) �� *SelectedNeed+* � DataContext* �� ! �� "  .���  � ��
 �r�����  � ��$ Center=�" �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�#   $$ *
 $% Acronym��& �� *	Acronym+  $$ *
 $% Description��& �� *Description+  $% Actions��$$ *
 ' �� .R��� $ 
Horizontal=�$��Center=�.+��� + � ToolTip$+ Delete������ *" DataContext.ConfirmDeleteCommand+, �� RelativeSource, �� - �� AncestorType"-  .���� $ ��/Resources/delete-icon.png���� -   $
+ Edit��$��1,0,0,0q�.���� $��/Resources/edit-icon.png���� $��4��$��Student Conditions��$ Center=�$
��Bold"�$��0,2,4,0q�$��16��  -   $��dtgCondtions��$��5��$��0��$ False��$ False��$ 25Q�$ 40Q�
�������$
 None=�$ False��
 �����$ 2��$
  True��$��0,0,0,0q����� *StudentConditions+) �� *SelectedCondition+* �� ! �� "  .���  � ��
 �r�����  � ��$ Center=�" �� "  .���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q�#   $$ *
 $% Acronym��& �� *	Acronym+  $$ *
 $% Description��& �� *Description+  $% Actions��$$ *
 ' �� .R��� $ 
Horizontal=�$��Center=�.+��� $+ Delete������ *" DataContext.ConfirmDeleteCommand+, �� "-  .���� $ ��/Resources/delete-icon.png���� -   $
+ Edit��$��1,0,0,0q�.���� $��/Resources/edit-icon.png��  $��1��$��2��.���� $ 4,4,0,0��._�� $��2��$��0�� �� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* � #  HandyControl.Controls.TextBox  TextBoxExtend��%�  '        7  '��     �� "  8   .���  �@��TextWrapping$
 Wrap=���  � ��$ 4,4,0,0q���  �@ TitlePlacement$
 Left=���  �@ VerticalAlignment$ Stretch=���  � ��$ 1q���  � ��
 �������  � ��$ Center=���  � ��$ 0,4,4,4q��� " ��.���  � ��$
 Wrap=���  �@��Padding$ 4,4,0,0q���  � ��$ 0,4,4,4q�.+��� $��Modify Rates��$��0��$��2��$ Center=�$
��Bold"�  $��1��  $��4��  $��8��  $��11��  -   .  Name$. txtCurrentStipendRate��$��2��$��0��$��1��$ Stipend Rate: ��$��	52,5,58,0q�  -   $. txtCurrentPTORate��$��3��$��0��$ 
PTO Rate: ��$��	71,5,57,0q��� -   / ��Name$/ btnSavePTOStipend��$��4��$��	5, 5,10,5q�.��Save  -   $. txtYearlyMealValue��$��5��$��0��$ Meal Value: ��$��	61,5,57,0q�  -   $. txtCurrentMileageValue��$��6��$��0��$ Mileage Value: ��$��	45,5,57,0q�  -   $. txtCurrentBusValue��$��7��$��0��$ Bus Milage Rate: ��$��	34,5,57,0q��� -   $/ btnSaveTransportRates��$��8��$��	5, 5,10,5q�.��Save�� $��9��$ 
Horizontal=�$��0, 5,0,0q�$��Center=�.+��� $	��114��$��	0,0,-25,0q�Effective Date:(  "System.Windows.Controls.DatePicker  -   0  Name$0 dpGrantDate��$	��100��  -   $. txtGrantAmount��$��10��$ Grant Stipend: ��$��5,5,5,0q�$	��Top=��� -    $/ btnAddGrant��$��11��$��	5, 5,10,5q�.��Set Total Grant Stipend�� -!   $/ btnReset��$��12��$��	5, 5,10,5q�
���  ��.��RESET CHILD ASSIGNMENTS