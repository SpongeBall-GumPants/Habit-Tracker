   M S B A M L   `   `   `  ����  HabitTracker&     HabitTracker.ManageMinutesWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Manage Available Minutes��5   	   $	��600��6*   $	��800��67    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Manage Available Minutes��6!   $��24(�6A   $��0,0,0,20q�6O   � 5   
   $��1��6   $��0,0,0,20q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $	��200 �6#   �� 5      $
��Auto �6#   �� 5      $	��100 �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   $��Date:��6$   $��Center=�64   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ NewDatePicker��5   )   $��1��6   $��5q�6@   �� 5      $��2��6   $��Minutes:��6$   $��Center=�67   �� 5      -   5         ��Name$ NewMinutesTextBox��5   &   $��3��6   $��5q�6A   �� 5       -   5        $��4��5       $	��Add��6%   $��80��63   $��5q�6>   6I   5!      &   System.Windows.Controls.DataGrid  5$   
   -   5          Name$ MinutesGrid��5$       $��2��6      AutoGenerateColumns$ False��65      CanUserAddRows$ False��5%      	   Columns	 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Date��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Key+6K     5(      $
 Minutes��6%   $	 100 6P    66   �� *Value+6P   4  .System.Windows.Controls.DataGridTemplateColumn  5)      $	 100 6)      CellTemplate 5*      �� 5+      .R�5,      �� -   5        $��Delete��5,   %   66   5-      5/      51      �� 54   
   $��3��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�55      .+�56      �� -   5        $��Close��56      $	��100��6%   $��30��61   $��0,0,10,0q�6=   57      �� 58      -   5        $
��Save��58      $	��100��6$   $��30��60   ���#4CAF50
�������59   (   5:      5;      5<      