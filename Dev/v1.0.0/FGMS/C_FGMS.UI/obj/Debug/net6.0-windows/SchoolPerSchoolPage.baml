   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5      #    C_FGMS.UI.SchoolPerSchoolPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        N� Title$  SchoolPerSchoolPage��5
      .��5      � 
�������.+�5      � .+�5      �� $
��Left=�6   $��14,0,0,0q�67   $
��Wrap=�6I   $��Select School:��6]   $	��Top=�6s   $��17��6�   $  HandyControl.Controls.ComboBox  5      -   5        $��
cbxSchools��5   2   $
��Left=�5      $��	14,20,0,0q�69   $	��Top=�6L   $	��250��6d   $��34��6p      AutoComplete$
 True��5       �� IsTextSearchEnabled$ False��6t    �� 
IsEditable$
 True��5      '  !HandyControl.Controls.InfoElement   Placeholder$ Select School��60     �� IsTextSearchCaseSensitive$ False��6[    �� SelectedValuePath$
 Tuid��5       �� DisplayMemberPath$
 Name��67   ��5   }   �� *	Schools+5   7   �� 5      -   5        $��txtHours��5   �   $
��Left=�6   $��	14,62,0,0q�67   $
��Wrap=�6J   $��School Hours��6^   $	��Top=�6r   $��18��6�   6�   �� 5      -   5        $��txtDays��5   �   $
��Left=�6   $��
139,62,0,0q�67   $
��Wrap=�6K   $��School Days��6_   $	��Top=�6r   $��18��6�   6�   �� 5      $
��Left=�6   $��	293,4,0,0q�67   $
��Wrap=�6J   $��
Principal:��6^   $	��Top=�6p   �� 5      -   5        $��txtPrincipal��5   x   $
��Left=�6   $��
293,29,0,0q�67   $
��Wrap=�6K   $	��Top=�6_   6x   �� 5       -   5        $��
txtAddress��5    �   $
��Left=�6   $��
293,62,0,0q�67   $
��Wrap=�6K   $��Address��6_   $	��Top=�6n   $��18��6�   6�   �� 5#      $
��Left=�6   $��	567,4,0,0q�67   $
��Wrap=�6J   $��
Secretary:��6^   $	��Top=�6p   �� 5$      -   5        $��txtSecretary��5$   w   $
��Left=�6   $��
567,29,0,0q�67   $
��Wrap=�6K   $	��Top=�6_   6w   �� 5%      -   5        $��txtPhoneNum��5%   �   $
��Left=�6   $��
567,62,0,0q�67   $
��Wrap=�6K   $��Phone Number��6_   $	��Top=�6s   $��18��6�   6�   &   System.Windows.Controls.DataGrid  5(      -   5        $��dgVolsPerSchool��5(         
IsReadOnly$
 True��63   $��
10,85,10,0q�6E   	   AutoGenerateColumns$	 False��6Y   
 �� AlternationCount$
 2��5)         ColumnHeaderHeight$ 30Q�60      	RowHeight$ 40Q�6H   $��0��5*      $��Center=�6+      HeadersVisibility$	 All=�6O   $��2��6g   $
��True��5+         CanUserResizeColumns$
 True��6@   ��5)   W   �� *
DataList+   ColumnHeaderStyle 5,      �� 5-       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5.      ��  �� Property � �� �� Value
 �r���67   �� 5/       � ��$ Center=�6G   50         RowStyle 52      �� 53      )  #System.Windows.Controls.DataGridRow"  6      DataGridRowStyle �� BasedOn# [  65   .�54      ��  � ��
 �����67    �� Triggers 55      P� 56       P� Property �@ AlternationIndex P� Value$ 1��6B   . �57   "   ��  � ��
 �����6?   58      P� 59       �@ 
IsSelected$
 True��6<   . �5:   "   ��  � ��
 �r���6?   5;      5=         	CellStyle 5?      �� 5@      *  $System.Windows.Controls.DataGridCell"  6   .�5A      ��  � ��$ 0q�6<   �� 5B       � ��
 ���� 6M   �� 5C       � ��$ Stretch=�6Q   �� 5D       � �� 5E      �� 5F   "    �� 
TargetType"  62   .R�5G   &   � #��z��6+   .+�5H   *   �� $��Center=�6;   $��Center=�6X   5I   '   5J   #   5L      5M         Columns 5O      0  *System.Windows.Controls.DataGridTextColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Volunteer��6)      Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	 6c   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 6<   �� *Volunteer.FullName+6c     5Q      $ Teacher��6)   $ *	 6`    6:   �� *Classroom.Teacher+6`     5R      $ Grade��6)   $ *	 6\    68   �� *Classroom.Grade+6\     5S      $
 Room��6)   $ *	 6Z    67   �� *Classroom.Room+6Z     5T      $ Schedule��6)   $ 2*	 6g    6;   �� *Schedule.StringSchedule+6g     5U      $ Contact��6)   $ *	 6^    6:   �� *Volunteer.Phone+6^   5X      � 5\      $��1��6   $��Right=�6'   $	��Top=�6C    � ColumnDefinitions 5]      �� 5^      $��* �6+   �� 5_      $��* �6+   �� 5`      $��* �6+   �� 5a      $��* �6+   .+�5d      �� -	   5         ��Name$ btnEditSave��5d   b   $��5,5,5,0q�6   $��Right=�6.   $	��Top=�6J   $��0��6�   $��0q�6�   $��0q�6�     � ToolTip$  Edit Selected School��6�   .��5e      �� $��/Resources/edit-icon.png��6    5f      �� 5g      -
   5        $ btnDeleteCancel��5g   5   $	��Top=�6   $��Right=�6g   $��5,5,5,0q�6�   $��1��6�   $��0q�6�   $��0q�6�   $  Delete Selected School��6�   .��5h      �� $ ��/Resources/delete-icon.png��6    5i      �� 5j      -   5        $ btnExcelPerSchool��5j   ^   $	��Top=�6   $��5,5,5,0q�61   $��Right=�6B   $��2��6�   $��0q�6�   $��0q�6�   $  Save To Excel��6�   .��5k      �� $!��/Resources/reports-icon.png��6    5l      �� 5m      -   5        $ 
btnRefresh��5m   b   $	��Top=�6   $��5,5,5,0q�65   $��Right=�6F   $��3��6v   $��0q�6�   $��0q�6�   $  Refresh Data��6�   .��5n      �� $!��\Resources\refresh-icon.png��6    5o      5p      5r      5t      5u      