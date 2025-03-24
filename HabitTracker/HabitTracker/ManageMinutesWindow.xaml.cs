using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HabitTracker
{
    public partial class ManageMinutesWindow : Window
    {
        private readonly DataManager _dataManager;
        private readonly Dictionary<DateTime, int> _minutesData = new();


        public ManageMinutesWindow(DataManager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;

            // Initialize calendar with next 30 days
            var today = DateTime.Now.Date;
            for (int i = 0; i < 30; i++)
            {
                var date = today.AddDays(i);
                _minutesData[date] = _dataManager.GetAvailableMinutes(date);
            }
            MinutesGrid.ItemsSource = _minutesData.ToList();
        }

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            if (NewDatePicker.SelectedDate.HasValue && int.TryParse(NewMinutesTextBox.Text, out int minutes))
            {
                _minutesData[NewDatePicker.SelectedDate.Value] = minutes;
                MinutesGrid.ItemsSource = _minutesData.ToList();
            }
            else
            {
                MessageBox.Show("Please select a date and input a valid number of minutes.");
            }
        }
        private void DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            if (MinutesGrid.SelectedItem is KeyValuePair<DateTime, int> selectedItem)
            {
                _minutesData.Remove(selectedItem.Key);
                MinutesGrid.ItemsSource = _minutesData.ToList();
            }
            else
            {
                MessageBox.Show("Please select a record to delete");
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var entry in _minutesData)
                {
                    _dataManager.SetAvailableMinutes(entry.Key, entry.Value);
                }
                MessageBox.Show("Available minutes updated successfully!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating minutes: {ex.Message}");
            }
        }
    }
}