   M S B A M L   `   `   `  ���� O  KB_FGMS.BusinessLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Qclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic%B_FGMS.BusinessLogic.Models.Volunteer  gGclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogicB_FGMS.BusinessLogic.Models  D @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI %   C_FGMS.UI.AddNewChildAssignment   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     bdataQclr-namespace:B_FGMS.BusinessLogic.Models.Volunteer;assembly=B_FGMS.BusinessLogic     Ydata2Gclr-namespace:B_FGMS.BusinessLogic.Models;assembly=B_FGMS.BusinessLogic     )localclr-namespace:C_FGMS.UI     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  $	��425��$	��450��  � Title$  AddNewChildAssignment��.��� .+��� $��0��$��2��$��2�� K� ZIndex$
 1000��$��Visible=�$��Right=�$��0,0,-2,0q�.���� -    ��Name$ stkGrowl��$	��Top=�$��0,0,-2,0q�   EffectShadow1 J� Effect# [  �� $��
10,12,10,8q�  TabControlBaseStyle#���   � 	Resources  �� 
TargetType �� Property �� Value��%0   '��       �� " ��.���  � ��$ Right=�.@���  �� 
IsSelected$
 True��$��Add Assignment��$��30��$	��Top=�.��   HandyControl.Controls.Card  -   	  Name$	 crdStudentInfo��$��1��$��1��$��4,4,6,4q�$��1q����	#FFF6F6F6
���   �.���� 
 �� CornerRadius$
 4,4,0,0��._�� -    �Name$ grdStudentInfo�� #  HandyControl.Controls.TextBox  TextBoxExtend �� BasedOn)  #HandyControl.Controls.CheckComboBox  CheckComboBoxExtend#  HandyControl.Controls.Divider  DividerBaseStyle  	ConverterJ   DB_FGMS.BusinessLogic.Models.Volunteer.VolunteerChildAssignmentsModel  NewChildAssignment��%K  '        7  '�� �     '  �     7  '�� -    '  o    7  &	 �    &	 �    �� "  8   ���  � ��$ 1q���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q���  �@��TextWrapping$
 Wrap=��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 4,0,0,2q��� "  8   ���  � ��$ 4,2,4,2q��� " ��.���  � ��$ Center=���  � ��$ 4,0,0,0q��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q���  �@ 
LineStroke
 �   ���    � ColumnDefinitions �� $
��auto ��� $��* � � RowDefinitions �� $
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
��auto � � DataContext ��  �� Source# [ .+��� $��Identifier (Optional):��$��0��$��0��  -     Name$ txtIdentifier��$��0��$��1������ *
Identifier+$  HandyControl.Controls.ComboBox  -     Name$ CmbAddSelectRoom��$��1��$��2��   AutoComplete$
 True�� �� 
IsEditable$ False��$
��auto��$
��auto��$��4,2,4,2q�   ShowClearButton$
 True�� �� SelectedValuePath$ ClassroomNumber�� �� DisplayMemberPath$ ClassroomNumber��'  !HandyControl.Controls.InfoElement   Placeholder$ Room Number������ *
Classrooms+  -   $ cmbAge��$��2��$��2��$	 Age��$��4,2,4,2q�.@��� $	��0-5���� $
��6-12��  -     Name$ cmbConditions��$��3��$��2��$ 
Conditions��$
 Tuid��$ Acronym������ *
Conditions+ �� SelectedValue �� *Tuid+ �� TargetNullValue$  �� �� Mode$ TwoWay=�  -   $ cmbNeeds��$��4��$��2��$ Needs��$
 Tuid��$ Acronym������ *StudentNeeds+ �� *Tuid+$  ��$ TwoWay=��� $��5��$��Desired Outcome:��  -	   $ txtDesiredOutcome��$��5��$��1������ *DesiredOutcome+� $��7��$��Right=�$��1��$��
0,14,9,-44q� J� RenderTransformOrigin$ 0.478,-1.588&� �� $��* ��� $��* ��� $��* �.+��� -
    ��Name$ 	btnFinish��$��12��$��1�� � ToolTip$ Save Changes��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/save-icon.png���� -   $ 	btnCancel��$��12��$��2��$ Cancel Changes��$��0q�$��2q�$��5,0,5,0q�.���� $ ��/Resources/cancel-icon.png���� $��Add Conditon��$��30��$	��Top=�.��  -   $	 crdAddCondition��$��1��$��1��$��4,4,6,4q�$��1q����	#FFF6F6F6
���   �.���� $
 4,4,0,0��._�� -   $ grdAddCondition�� ��%<  '        7  '�� �     '  �     7  '�� -    '  o    7  &	 �    �� "  8   ���  � ��$ 1q���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q���  �@��TextWrapping$
 Wrap=��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 4,0,0,2q��� "  8   ���  � ��$ 4,2,4,2q��� " ��.���  � ��$ Center=���  � ��$ 4,0,0,0q��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q���  �@ 
LineStroke
 �   ���  �� $
��auto ��� $��* � �� $
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
��auto �.+��� $��1��$��
Condition:��  -   $ txtNewCondition��$��1��$��1��$ Attention Difficulties���� $��2��$��Abbreviation:��  -   $! txtNewConditionAbbreviation��$��2��$��1��  �� 	MaxLength$  4��$ AD��� $��3��$��Right=�$��1��$��
0,69,4,-69q� �� $��* ��� $��* ��� $��* �.+��� -   $ btnAddCondition��$��1��$��1��$ Save Changes��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/save-icon.png���� -   $ btnCancelCondition��$��1��$��2��$ Cancel Changes��$��0q�$��2q�$��5,0,5,0q�.���� $ ��/Resources/cancel-icon.png���� $
 True��$��Add Need��$��30��$	��Top=�.��  -   $	 
crdAddNeed��$��1��$��1��$��4,4,6,4q�$��1q����	#FFF6F6F6
���   �.���� $
 4,4,0,0��._�� -   $ 
grdAddNeed�� ��%<  '        7  '�� �     '  �     7  '�� -    '  o    7  &	 �    �� "  8   ���  � ��$ 1q���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q���  �@��TextWrapping$
 Wrap=��� " �����  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 4,0,0,2q��� "  8   ���  � ��$ 4,2,4,2q��� " ��.���  � ��$ Center=���  � ��$ 4,0,0,0q��� "  8   ���  � ��$ 2����  � ��$ Bottom=���  � ��$ 2,0,2,0q���  �@ 
LineStroke
 �   ���  �� $
��auto ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��1��$��Need:��  -   $ 
txtNewNeed��$��1��$��1��$ Nurturing/Comfort���� $��2��$��Abbreviation:��  -   $ txtNewNeedAbbreviation��$��2��$��1��$  4��$ A��� $��3��$��Right=�$��1��$��
0,69,4,-69q� �� $��* ��� $��* ��� $��* �.+��� -   $ 
btnAddNeed��$��1��$��1��$ Save Changes��$��0q�$��0q�$��5,0,5,0q�.���� $��/Resources/save-icon.png���� -   $ btnCancelNeed��$��1��$��2��$ Cancel Changes��$��0q�$��2q�$��5,0,5,0q�.���� $ ��/Resources/cancel-icon.png��