   M S B A M L   `   `   `  ����D  @C_FGMS.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:C_FGMS.UI	C_FGMS.UI  5          C_FGMS.UI.ReportsPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:C_FGMS.UI     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        N� Title$  ReportsPage��5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $	��13* �6   .+�5   
   &   HandyControl.Controls.TabControl  -   5          Name$ 
TabControl��5      $��0q�6-      TabControlBaseStyle#���   68   $��0��5   G   $��0q�5      $
��auto��6,    � 	Resources 5       �� 
TargetType �� Property �� Value  tabStyle#  HandyControl.Controls.TabItem  TabItemInLine �� BasedOn��%�   '��       &	 K     7  �� 5      " ��6   .�5      ��  � ��$ Right=�6<   5      �� 5      "  6)   8   �5      �� 5       � ��
 �r���68   5      5      .@�5         �� 
IsSelected$
 True��6   $��Report Builder��6+   #��[ 6C     5       $��Annual Check��6   #��[ 6/     5!      $��Volunteer Info��6   #��[ 61   5"       � 5$   
   -   5        	  �Name$	 reportsMainFrame��5$      
  � NavigationUIVisibility$
 Hidden=�6*   $��1��6J   5%      5&      