using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HabitTracker
{
    public partial class MainWindow : Window
    {
        private readonly DataManager _dataManager;
        private readonly Scheduler _scheduler;
        private List<Habit> _habits;

        public MainWindow()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            _habits = _dataManager.LoadHabits();
            _scheduler = new Scheduler(_habits, _dataManager);
            LoadHabits();
        }

        private void LoadHabits()
        {
            HabitsList.ItemsSource = _habits;
        }
        private void AddHabit_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddHabitWindow(_dataManager);
            addWindow.Owner = this;
            addWindow.ShowDialog();
            _habits = _dataManager.LoadHabits();
            LoadHabits();
        }
        private void DeleteHabit_Click(object sender, RoutedEventArgs e)
        {
            var deleteWindow = new DeleteHabitWindow(_dataManager);
            deleteWindow.Owner = this;
            deleteWindow.ShowDialog();
            _habits = _dataManager.LoadHabits();
            LoadHabits();
        }
        private void UpdateHabit_Click(object sender, RoutedEventArgs e)
        {
            var updateWindow = new UpdateHabitWindow(_dataManager, _habits);
            updateWindow.Owner = this;
            updateWindow.ShowDialog();
            _habits = _dataManager.LoadHabits();
            LoadHabits();
        }
        private void MarkDone_Click(object sender, RoutedEventArgs e)
        {
            var markDoneWindow = new MarkDoneWindow(_dataManager);
            markDoneWindow.Owner = this;
            markDoneWindow.ShowDialog();
            _habits = _dataManager.LoadHabits();
            LoadHabits();
        }
        private void ManageMinutes_Click(object sender, RoutedEventArgs e)
        {
            var minutesWindow = new ManageMinutesWindow(_dataManager);
            minutesWindow.Owner = this;
            minutesWindow.ShowDialog();
        }

        

        private void ViewJson_Click(object sender, RoutedEventArgs e)
        {
            var jsonWindow = new ViewJsonWindow(_dataManager);
            jsonWindow.Owner = this;
            jsonWindow.ShowDialog();
        }

        private void ViewSchedule_Click(object sender, RoutedEventArgs e)
        {
                var viewScheduleWindow = new ViewScheduleWindow(_dataManager);
            viewScheduleWindow.ShowDialog();
        }
    }
}