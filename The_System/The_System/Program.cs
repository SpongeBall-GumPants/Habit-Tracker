using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        /*var dataManager = new DataManager();
        Console.WriteLine($"\n\nCurrent Directory: {Environment.CurrentDirectory}\n\n\n\n");

        // Add debug output to verify loaded habits

        Console.WriteLine("\nLoaded Habits:");
        var habits = dataManager.LoadHabits();
        foreach (var habit in habits)
        {
            Console.WriteLine($"- {habit.Name} (Length: {habit.LengthMinutes} min, Frequency: {habit.Frequency}/day)");
        }

        var scheduler = new Scheduler(habits);
        var monthStart = DateTime.Now.Date;
        var monthLength = 30;
        dataManager.MarkHabitDone(new Guid("44444444-4444-4444-4444-444444444444"), DateTime.Now.AddDays(10));

        // Debug output for available minutes
        Console.WriteLine("\nAvailable Minutes per Day:");
        for (int i = 1; i <= monthLength; i++)
        {
            var date = monthStart.AddDays(i - 1);
            var availableMinutes = dataManager.GetAvailableMinutes(date);
            Console.WriteLine($"- {date:yyyy-MM-dd}: {availableMinutes} minutes");
            scheduler.UpdateAvailableHours(i, availableMinutes / 60);
        }

        var schedule = scheduler.GenerateMonthlySchedule(monthStart, monthLength);
        DebugHabitsData(habits, "After Initial Load");

        // Display schedule with more detailed information
        Console.WriteLine("\nGenerated Schedule:");
        foreach (var daySchedule in schedule.OrderBy(s => s.Key))
        {
            if (daySchedule.Value.Any())
            {
                Console.WriteLine($"\nSchedule for {daySchedule.Key:yyyy-MM-dd}:");
                foreach (var item in daySchedule.Value.OrderBy(x => x.timeSlotStart))
                {
                    Console.WriteLine($"   - {item.habit.Name}");
                    Console.WriteLine($"     Time: {item.timeSlotStart}-{item.timeSlotEnd} min");
                }
            }
        }

        // ============= ADDING NEW TEST CODE BELOW ================
        Console.WriteLine("\n\n=== STARTING HABIT MODIFICATION TESTS ===\n");

        // Test 1: Modify existing habit
        Console.WriteLine("=== MODIFYING EXISTING HABIT ===");
        var targetHabitId = new Guid("44444444-4444-4444-4444-444444444444");
        var existingHabit = habits.FirstOrDefault(h => h.Id == targetHabitId);

        if (existingHabit != null)
        {
            Console.WriteLine($"\nFound existing habit: {existingHabit.Name}");
            Console.WriteLine("Current values:");
            Console.WriteLine($"- Name: {existingHabit.Name}");
            Console.WriteLine($"- Length: {existingHabit.LengthMinutes}");
            Console.WriteLine($"- Frequency: {existingHabit.Frequency}");
            Console.WriteLine($"- Importance: {existingHabit.Importance}");
            Console.WriteLine($"- Severity: {existingHabit.Severity}");
            Console.WriteLine($"- Planned Time: {existingHabit.PlannedTimePerWeek}");
            Console.WriteLine($"- Age: {existingHabit.Age}");

            Console.WriteLine("\nApplying changes...");
            existingHabit.Name = "Learn coding";
            existingHabit.LengthMinutes = 50;
            existingHabit.Frequency = 2;
            existingHabit.Importance = 4;
            existingHabit.Severity = 3;
            existingHabit.CreatedDate = DateTime.Now.AddDays(1);
            existingHabit.PlannedTimePerWeek = 300;
            existingHabit.Age = 1;

            Console.WriteLine("Updating habit in database...");
            dataManager.UpdateHabit(existingHabit);

            // Verify changes
            var updatedHabit = dataManager.GetHabit(targetHabitId);
            Console.WriteLine("\nVerified updated values:");
            Console.WriteLine($"- Name: {updatedHabit.Name}");
            Console.WriteLine($"- Length: {updatedHabit.LengthMinutes}");
            Console.WriteLine($"- Frequency: {updatedHabit.Frequency}");
            Console.WriteLine($"- Importance: {updatedHabit.Importance}");
            Console.WriteLine($"- Severity: {updatedHabit.Severity}");
            Console.WriteLine($"- Planned Time: {updatedHabit.PlannedTimePerWeek}");
            Console.WriteLine($"- Age: {updatedHabit.Age}");
        }
        else
        {
            Console.WriteLine("ERROR: Could not find the target habit to modify!");
        }

        // Test 2: Add new habit
        Console.WriteLine("\n=== ADDING NEW TEST HABIT ===");
        var newHabit = new Habit(
            name: "Test Habit",
            description: "A test habit to verify adding functionality",
            lengthMinutes: 30,
            frequency: 1,
            importance: 2,
            severity: 2,
            plannedTimePerWeek: 120
        );


        Console.WriteLine("Adding new habit with these details:");
        Console.WriteLine($"- Name: {newHabit.Name}");
        Console.WriteLine($"- Description: {newHabit.Description}");
        Console.WriteLine($"- Length: {newHabit.LengthMinutes} minutes");
        Console.WriteLine($"- Frequency: {newHabit.Frequency}/day");
        Console.WriteLine($"- Importance: {newHabit.Importance}");
        Console.WriteLine($"- Severity: {newHabit.Severity}");
        Console.WriteLine($"- Planned Time: {newHabit.PlannedTimePerWeek} minutes/week");

        dataManager.AddHabit(newHabit);


        // Print current habits
        Console.WriteLine("\nBefore deletion - current habits:");
        var currentHabits = dataManager.LoadHabits();
        foreach (var h in currentHabits)
        {
            Console.WriteLine($"- {h.Name} (ID: {h.Id})");
        }

        // Try to delete the habit
        dataManager.DeleteHabit(newHabit.Id);

        // Verify deletion
        Console.WriteLine("\nAfter deletion - current habits:");
        var remainingHabits = dataManager.LoadHabits();
        foreach (var h in remainingHabits)
        {
            Console.WriteLine($"- {h.Name} (ID: {h.Id})");
        }





        // Display final state
        Console.WriteLine("\n=== FINAL STATE OF ALL HABITS ===");
        var finalHabits = dataManager.LoadHabits();
        foreach (var habit in finalHabits)
        {
            Console.WriteLine($"\nHabit: {habit.Name}");
            Console.WriteLine($"- ID: {habit.Id}");
            Console.WriteLine($"- Length: {habit.LengthMinutes} minutes");
            Console.WriteLine($"- Frequency: {habit.Frequency}/day");
            Console.WriteLine($"- Importance: {habit.Importance}");
            Console.WriteLine($"- Severity: {habit.Severity}");
            Console.WriteLine($"- Created: {habit.CreatedDate}");
            Console.WriteLine($"- Planned Time: {habit.PlannedTimePerWeek} minutes/week");
            Console.WriteLine($"- Age: {habit.Age}");
        }

        Console.WriteLine("\n=== TEST COMPLETE ===");
    }

    public static void DebugHabitsData(List<Habit> habits, string checkpoint)
    {
        Console.WriteLine($"\nDEBUG CHECK - {checkpoint}");
        Console.WriteLine($"Number of habits: {habits.Count}");
        foreach (var habit in habits)
        {
            Console.WriteLine($"Habit Details:");
            Console.WriteLine($"  ID: {habit.Id}");
            Console.WriteLine($"  Name: {habit.Name}");
            Console.WriteLine($"  Description: {habit.Description}");
            Console.WriteLine($"  Length: {habit.LengthMinutes} minutes");
            Console.WriteLine($"  Frequency: {habit.Frequency}/day");
            Console.WriteLine($"  Created: {habit.CreatedDate}");
            Console.WriteLine("  ----------------------");
        }
    */
    }
}