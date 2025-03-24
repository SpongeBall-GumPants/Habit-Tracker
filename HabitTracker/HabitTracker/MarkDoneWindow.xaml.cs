using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HabitTracker
{
    public partial class MarkDoneWindow : Window
    {
        private readonly DataManager _dataManager;
        private readonly List<Habit> _habits;

        public MarkDoneWindow(DataManager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;
            _habits = _dataManager.LoadHabits();
            HabitsListView.ItemsSource = _habits;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (HabitsListView.SelectedItem is Habit selectedHabit)
            {
                try
                {
                    _dataManager.MarkHabitDone(selectedHabit.Id, DateTime.Now.Date);
                    MessageBox.Show($"Habit '{selectedHabit.Name}' marked as done today.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error marking habit as done: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a habit to mark as done.");
            }
        }
    }
}