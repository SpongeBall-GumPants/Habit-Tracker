using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HabitTracker
{
    public partial class AddHabitWindow : Window
    {
        private readonly DataManager _dataManager;

        public AddHabitWindow(DataManager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;
        }

        private void SaveHabit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var habit = new Habit(
                    name: NameTextBox.Text,
                    description: DescriptionTextBox.Text,
                    lengthMinutes: int.Parse(LengthTextBox.Text),
                    frequency: double.Parse(FrequencyTextBox.Text),
                    importance: double.Parse(ImportanceTextBox.Text),
                    severity: double.Parse(SeverityTextBox.Text),
                    plannedTimePerWeek: double.Parse(PlannedTimeTextBox.Text)
                );

                _dataManager.AddHabit(habit);
                MessageBox.Show("Habit added successfully!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding habit: {ex.Message}");
            }
        }
    }
}
