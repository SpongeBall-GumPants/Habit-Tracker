   M S B A M L   `   `   `  ����  HabitTracker$    HabitTracker.UpdateHabitWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Update Habit��5   	   $	��600��6   $	��800��6+    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5	      $��2* �6   .+�5   
   �� $��0��6   $��0,0,20,0q�6%   .+�5      �� $��Select Habit to Update:��6   $
��Bold"�67   $��0,0,0,10q�6I   �� 5      -   5         ��Name$ 
HabitsList��5      $	��450��6*    �� DisplayMemberPath$
 Name��5      5      � 5   
   $��1��6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5#      �� $��0��6   $��0,0,0,10q�6&   .+�5$      �� $��Name:��6   �� 5%      -   5         ��Name$ NameTextBox��5%      $��25��6/   5&      �� 5)      $��1��6   $��0,0,0,10q�6&   .+�5*      �� $��Description:��6   �� 5+      -   5        $ DescriptionTextBox��5+      $��50��66    �� TextWrapping$
 Wrap=�6B    �� AcceptsReturn$
 True��6V   5,      �� 5/      $��2��6   $��0,0,0,10q�6&   .+�50      �� $��Length (minutes):��6   �� 51      -   5        $ LengthTextBox��51      $��25��61   52      �� 55      $��3��6   $��0,0,0,10q�6&   .+�56      �� $��Frequency (per day):��6   �� 57      -   5        $ FrequencyTextBox��57      $��25��64   58      �� 5;      $��4��6   $��0,0,0,10q�6&   .+�5<      �� $��Importance (1-5):��6   �� 5=      -   5        	 ��Name$	 ImportanceSlider��5=      
 � Minimum$
 1Q�63    � Maximum$ 5Q�6?    �� TickFrequency$ 1Q�5>       �� IsSnapToTickEnabled$
 True��6+    �� TickPlacement$ BottomRight=�5?      5@      �� 5C      $��5��6   $��0,0,0,10q�6&   .+�5D      �� $��Severity (1-5):��6   �� 5E      -   5        $	 SeveritySlider��5E      $
 1Q�61   $ 5Q�6=   $ 1Q�5F      $
 True��6+   $ BottomRight=�5G      5H      �� 5K      $��6��6   $��0,0,0,10q�6&   .+�5L      �� $&�� Planned Time Per Week (minutes):��6   �� 5M      -   5        $ PlannedTimeTextBox��5M      $��25��66   5N      �� 5Q      $��8��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   $��0,20,0,0q�6[   .+�5R      �� -	   5        $��Update��5R      $	��100��6*   $��30��66   $��0,0,10,0q�6B   6T   �� 5S      -
   5        $��Cancel��5S      $	��100��6*   $��30��66   6B   5T      5U      5V      5W      