using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HabitTracker
{
    public partial class DeleteHabitWindow : Window
    {
        private readonly DataManager _dataManager;
        private readonly List<Habit> _habits;
        public DeleteHabitWindow(DataManager dataManager)
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            {
                if (HabitsListView.SelectedItem is Habit selectedHabit)
                {
                    try
                    {
                        _dataManager.DeleteHabit(selectedHabit.Id);
                        MessageBox.Show("Habit deleted successfully!");
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting habit: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a habit to delete.");
                }
            }
        }
    }
}