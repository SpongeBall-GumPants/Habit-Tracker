   M S B A M L   `   `   `  ����  HabitTracker$    HabitTracker.DeleteHabitWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Delete Habit��5   	   $	��450��6   $	��400��6+    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Select Habit to Delete��6!   $��20(�6?   $��0,0,0,20q�6M   �� 5   
   -   5         ��Name$ HabitsListView��5       $��1��6   $��0,0,0,20q�68   ��5      �� 5      .R�5      �� .+�5      �� $
��Bold"�6:   ��6$   �� *Name+6:   �� 5      $
��Wrap=�6A   ��6$   �� *Description+6A   5      5      5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5      �� -   5        $��Cancel��5      $	��100��6&   $��30��62   $��0,0,10,0q�6>   5      �� 5      -   5        $��Delete��5      $	��100��6&   $��30��62   ���#FF5252
�������5   (   5       5!      5"      