using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;

public class DataManager
{
    private readonly string DATA_FILE_PATH;
    private SchedulerData _data = new();

    public class SchedulerData
    {
        public Dictionary<string, int> DailyAvailableMinutes { get; set; }
        public List<HabitData> Habits { get; set; }

        public SchedulerData()
        {
            DailyAvailableMinutes = new Dictionary<string, int>();
            Habits = new List<HabitData>();
        }
    }

    public class HabitData
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LengthMinutes { get; set; }
        public double Frequency { get; set; }
        public double Importance { get; set; }
        public double Severity { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<DateTime> DoneDates { get; set; } = new();
        public double PlannedTimePerWeek { get; set; }
        public DateTime? LastPerformedDate { get; set; }
        public DateTime? NextDueDate { get; set; }
        public double Age { get; set; }
    }

    public DataManager()
    {
        DATA_FILE_PATH = "C:\\Users\\emree\\source\\repos\\The_System\\The_System\\Data_data.json";
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(DATA_FILE_PATH))
            {
                string jsonString = File.ReadAllText(DATA_FILE_PATH);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };

                // Create new instance instead of modifying existing
                var loadedData = JsonSerializer.Deserialize<SchedulerData>(jsonString, options);
                if (loadedData != null)
                {
                    _data = loadedData;
                    // Initialize collections if null
                    _data.Habits ??= new List<HabitData>();
                    _data.DailyAvailableMinutes ??= new Dictionary<string, int>();
                }
                else
                {
                    InitializeDefaultData();
                }
            }
            else
            {
                InitializeDefaultData();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            InitializeDefaultData();
        }
    }

    private void SaveData()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(_data, options);
            Console.WriteLine("\nSaving data to file...");
            Console.WriteLine("Data being saved:");
            Console.WriteLine(jsonString);

            // Create a backup of the existing file if it exists
            if (File.Exists(DATA_FILE_PATH))
            {
                string backupPath = DATA_FILE_PATH + ".bak";
                File.Copy(DATA_FILE_PATH, backupPath, true);
            }

            // Write the new data
            File.WriteAllText(DATA_FILE_PATH, jsonString);

            // Verify the save
            if (File.Exists(DATA_FILE_PATH))
            {
                string savedContent = File.ReadAllText(DATA_FILE_PATH);
                Console.WriteLine($"\nVerified saved content (length: {savedContent.Length})");
                Console.WriteLine("Save successful!");
            }
            else
            {
                throw new Exception("File was not created after save attempt");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    private void InitializeDefaultData()
    {
        _data = new SchedulerData
        {
            DailyAvailableMinutes = new Dictionary<string, int>
            {
                { "default", 300 } // 5 hours default
            },
            Habits = new List<HabitData>() // Empty list, no default habits
        };
        SaveData();
    }

    public void SetAvailableMinutes(DateTime date, int minutes)
    {
        try
        {
            if (minutes < 0)
                throw new ArgumentException("Minutes cannot be negative");

            string dateKey = date.ToString("yyyy-MM-dd");
            Console.WriteLine($"\nSetting available minutes for {dateKey} to {minutes}");

            _data.DailyAvailableMinutes[dateKey] = minutes;
            SaveData();

            // Verify the update
            LoadData();
            if (_data.DailyAvailableMinutes.TryGetValue(dateKey, out int savedMinutes))
            {
                Console.WriteLine($"Verified: {dateKey} has {savedMinutes} minutes");
            }
            else
            {
                throw new Exception("Failed to verify minutes were saved");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting available minutes: {ex.Message}");
            throw;
        }
    }

    public int GetAvailableMinutes(DateTime date)
    {
        try
        {
            string dateKey = date.ToString("yyyy-MM-dd");
            Console.WriteLine($"\nChecking available minutes for date: {dateKey}");
            Console.WriteLine("Current available minutes entries:");
            foreach (var entry in _data.DailyAvailableMinutes)
            {
                Console.WriteLine($"- {entry.Key}: {entry.Value} minutes");
            }

            // First try to get the specific date's minutes
            if (_data.DailyAvailableMinutes.TryGetValue(dateKey, out int minutes))
            {
                Console.WriteLine($"Found specific minutes for {dateKey}: {minutes}");
                return minutes;
            }

            // If not found, try to get the default value
            if (_data.DailyAvailableMinutes.TryGetValue("default", out int defaultMinutes))
            {
                Console.WriteLine($"Using default minutes: {defaultMinutes}");
                return defaultMinutes;
            }

            // If no default value is set, return 300 as fallback
            Console.WriteLine("No specific or default minutes found, using fallback value: 300");
            return 300;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting available minutes: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return 300; // Return default value in case of error
        }
    }

    public List<Habit> LoadHabits()
    {
        try
        {
            var habits = new List<Habit>();
            foreach (var habitData in _data.Habits)
            {
                Console.WriteLine($"\nConverting habit data: {habitData.Name}");
                var habit = ConvertToHabit(habitData);
                if (!habit.NextDueDate.HasValue || habit.NextDueDate.Value < DateTime.Now)
                {
                    habit.CalculateNextDueDate();
                }
                habits.Add(habit);
            }
            return habits;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in LoadHabits: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public void AddHabit(Habit habit)
    {
        try
        {
            Console.WriteLine($"\nAdding new habit: {habit.Name}");
            ValidateHabit(habit);

            if (habit.Id == Guid.Empty)
            {
                habit.Id = Guid.NewGuid();
            }

            var habitData = ConvertToHabitData(habit);
            _data.Habits.Add(habitData);

            Console.WriteLine($"Saving data with new habit (Total habits: {_data.Habits.Count})");
            SaveData();

            // Verify the addition
            LoadData(); // Reload to verify
            var verificationHabit = _data.Habits.FirstOrDefault(h => h.Id == habit.Id);
            if (verificationHabit != null)
            {
                Console.WriteLine($"Verified addition: {verificationHabit.Name}");
            }
            else
            {
                throw new Exception("Failed to verify habit addition after save");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding habit: {ex.Message}");
            throw;
        }
    }

    public void UpdateHabit(Habit habit)
    {
        try
        {
            Console.WriteLine($"\nUpdating habit: {habit.Name} (ID: {habit.Id})");
            ValidateHabit(habit);

            var existingHabit = _data.Habits.FirstOrDefault(h => h.Id == habit.Id);
            if (existingHabit != null)
            {
                var updatedHabitData = ConvertToHabitData(habit);
                var index = _data.Habits.IndexOf(existingHabit);
                _data.Habits[index] = updatedHabitData;

                Console.WriteLine("Saving updated habit data...");
                SaveData();

                // Verify the update
                LoadData(); // Reload to verify
                var verificationHabit = _data.Habits.FirstOrDefault(h => h.Id == habit.Id);
                if (verificationHabit != null)
                {
                    Console.WriteLine($"Verified update: {verificationHabit.Name} with length {verificationHabit.LengthMinutes}");
                }
                else
                {
                    throw new Exception("Failed to verify habit update after save");
                }
            }
            else
            {
                throw new KeyNotFoundException($"Habit with ID {habit.Id} not found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating habit: {ex.Message}");
            throw;
        }
    }

    public void DeleteHabit(Guid habitId)
    {
        try
        {
            Console.WriteLine($"\nAttempting to delete habit with ID: {habitId}");

            // First verify we have the habit
            var habitToDelete = _data.Habits.FirstOrDefault(h => h.Id == habitId);
            if (habitToDelete == null)
            {
                Console.WriteLine($"No habit found with ID {habitId}");
                return;
            }

            Console.WriteLine($"Found habit to delete: {habitToDelete.Name}");

            // Remove the habit
            _data.Habits.RemoveAll(h => h.Id == habitId);

            // Save immediately after removal
            SaveData();

            // Important: Clear any cached data that might be held in memory
            _data = new SchedulerData
            {
                DailyAvailableMinutes = _data.DailyAvailableMinutes,
                Habits = _data.Habits
            };

            // Verify deletion by reloading and checking
            LoadData();
            var verificationHabit = _data.Habits.FirstOrDefault(h => h.Id == habitId);
            if (verificationHabit != null)
            {
                throw new Exception("Failed to delete habit - habit still exists after reload");
            }

            Console.WriteLine("Habit successfully deleted and verified");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteHabit: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public void MarkHabitDone(Guid habitId, DateTime date)
    {
        try
        {
            if (habitId == Guid.Empty)
            {
                throw new ArgumentException("Invalid habit ID");
            }

            Console.WriteLine($"Attempting to mark habit {habitId} as done for date {date}");

            var habit = _data.Habits.FirstOrDefault(h => h.Id == habitId);
            if (habit == null)
            {
                throw new KeyNotFoundException($"Habit with ID {habitId} not found in data store");
            }

            habit.DoneDates ??= new List<DateTime>();
            var normalizedDate = date.Date;

            if (habit.DoneDates.Any(d => d.Date == normalizedDate))
            {
                Console.WriteLine($"Habit already marked as done for {normalizedDate}");
                return;
            }

            habit.DoneDates.Add(normalizedDate);
            habit.LastPerformedDate = normalizedDate;
            habit.NextDueDate = normalizedDate.AddDays(1.0 / habit.Frequency);
            habit.Age = 0;

            SaveData();

            // Verify the update
            LoadData();
            var verificationHabit = _data.Habits.FirstOrDefault(h => h.Id == habitId);
            if (verificationHabit != null)
            {
                Console.WriteLine($"Verified mark as done: {verificationHabit.LastPerformedDate}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in MarkHabitDone: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public Habit GetHabit(Guid habitId)
    {
        try
        {
            var habitData = _data.Habits.FirstOrDefault(h => h.Id == habitId);
            if (habitData == null)
                throw new KeyNotFoundException($"Habit with ID {habitId} not found");

            return ConvertToHabit(habitData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting habit: {ex.Message}");
            throw;
        }
    }

    public void SaveHabits(List<Habit> habits)
    {
        try
        {
            _data.Habits = habits.Select(h => ConvertToHabitData(h)).ToList();
            SaveData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving habits: {ex.Message}");
            throw;
        }
    }

    private void ValidateHabit(Habit habit)
    {
        if (habit == null)
            throw new ArgumentNullException(nameof(habit));

        if (string.IsNullOrWhiteSpace(habit.Name))
            throw new ArgumentException("Habit name cannot be empty");

        if (habit.LengthMinutes <= 0)
            throw new ArgumentException("Length must be greater than 0");

        if (habit.Frequency <= 0)
            throw new ArgumentException("Frequency must be greater than 0");

        if (habit.Importance < 0 || habit.Importance > 5)
            throw new ArgumentException("Importance must be between 0 and 5");

        if (habit.Severity < 0 || habit.Severity > 5)
            throw new ArgumentException("Severity must be between 0 and 5");

        if (habit.PlannedTimePerWeek < 0)
            throw new ArgumentException("Planned time per week cannot be negative");
    }

    private HabitData ConvertToHabitData(Habit habit)
    {
        return new HabitData
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            LengthMinutes = habit.LengthMinutes,
            Frequency = habit.Frequency,
            Importance = habit.Importance,
            Severity = habit.Severity,
            CreatedDate = habit.CreatedDate,
            DoneDates = habit.DoneDates ?? new List<DateTime>(),
            PlannedTimePerWeek = habit.PlannedTimePerWeek,
            LastPerformedDate = habit.LastPerformedDate,
            NextDueDate = habit.NextDueDate,
            Age = habit.Age
        };
    }

    private Habit ConvertToHabit(HabitData data)
    {
        var habit = new Habit(
            data.Name,
            data.Description,
            data.LengthMinutes,
            data.Frequency,
            data.Importance,
            data.Severity,
            data.PlannedTimePerWeek)
        {
            Id = data.Id,
            CreatedDate = data.CreatedDate,
            DoneDates = data.DoneDates ?? new List<DateTime>(),
            LastPerformedDate = data.LastPerformedDate,
            NextDueDate = data.NextDueDate,
            Age = data.Age
        };
        return habit;
    }

    public void ClearAllData()
    {
        try
        {
            InitializeDefaultData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing data: {ex.Message}");
            throw;
        }
    }

    public void BackupData(string backupPath)
    {
        try
        {
            File.Copy(DATA_FILE_PATH, backupPath, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating backup: {ex.Message}");
            throw;
        }
    }
    public void RestoreData(string backupPath)
    {
        try
        {
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, DATA_FILE_PATH, true);
                LoadData();
            }
            else
            {
                throw new FileNotFoundException("Backup file not found", backupPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error restoring from backup: {ex.Message}");
            throw;
        }
    }

}