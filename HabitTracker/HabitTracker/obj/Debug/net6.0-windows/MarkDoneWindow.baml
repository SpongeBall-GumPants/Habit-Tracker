   M S B A M L   `   `   `  ����  HabitTracker!    HabitTracker.MarkDoneWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Mark Habit as Done��5   	   $	��500��6$   $	��600��61    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Mark Habit as Done��6!   $��20(�6;   $��0,0,0,20q�6I   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,20q�6;   .+�5      �� $��Date:��6   $��Center=�6$   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ 
DatePicker��5      $	��200��6-   $��5,0,20,0q�69   5      �� 5   
   -   5         ��Name$ HabitsListView��5       $��2��6    �� View 5      �� 5      .P�5      �� $
��Name��6%    �� Width$	 200��63    �� DisplayMemberBinding 6?   �� *Name+�� 5      $��Status��6%   $	 100��65   	 �� CellTemplate	 5      �� 5      .R�5   "   �� 
 d� 	IsChecked
 6+   �� *IsDone+5      5       5!      5#      �� 5%   
   $��3��6   $ 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�5&      .+�5'      �� -   5        $��Cancel��5'      $	��100��6&   $��30��62   $��0,0,10,0q�6>   5(      �� 5)      -   5        $
��Save��5)      $	��100��6$   $��30��60   ���#4CAF50
�������5*   (   5+      5,      5-      