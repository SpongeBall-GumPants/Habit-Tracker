public class Scheduler
{
    private readonly List<Habit> _habits;
    private readonly DataManager _dataManager;
    public readonly Dictionary<int, int> _dailyAvailableHours;

    // Existing weights...
    private const double IMPORTANCE_WEIGHT = 3.0;
    private const double SEVERITY_WEIGHT = 2.5;
    private const double AGE_WEIGHT = 3.0;
    private const double URGENCY_WEIGHT = 4.0;
    private const double BALANCE_WEIGHT = 3.5;
    private const double COMPLETION_RATE_WEIGHT = 2.0;

    public Scheduler(List<Habit> habits, DataManager dataManager)
    {
        _habits = habits;
        _dataManager = dataManager;
        _dailyAvailableHours = new Dictionary<int, int>();
        SyncAvailableHours();
    }

    private void SyncAvailableHours()
    {
        // Initialize the dictionary for all days
        for (int i = 1; i <= 30; i++)
        {
            // Convert minutes to hours, rounding down
            var minutes = _dataManager.GetAvailableMinutes(DateTime.Now.AddDays(i - 1));
            _dailyAvailableHours[i] = minutes / 60;
        }
    }

    public void UpdateAvailableHours(int day, int hours)
    {
        if (day >= 1 && day <= 30)
        {
            _dailyAvailableHours[day] = hours;
            // Sync back to DataManager (convert hours to minutes)
            _dataManager.SetAvailableMinutes(DateTime.Now.AddDays(day - 1), hours * 60);
        }
        else
        {
            Console.WriteLine($"Invalid day value: {day}. Day must be between 1 and 30.");
        }
    }

    private double CalculateBalanceScore(Habit habit, DateTime currentDate)
    {
        var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
        var totalMinutesThisWeek = habit.DoneDates
            .Where(d => d >= startOfWeek && d <= currentDate)
            .Sum(_ => habit.LengthMinutes);

        // Modified to consider both under and over scheduling
        var completionRate = totalMinutesThisWeek / habit.PlannedTimePerWeek;
        return Math.Max(0, 1.0 - completionRate);
    }

    private double CalculateCompletionRate(Habit habit, DateTime currentDate)
    {
        var daysFromStart = (currentDate - habit.CreatedDate).TotalDays;
        if (daysFromStart <= 0) return 1.0;

        var expectedCompletions = daysFromStart * habit.Frequency;
        var actualCompletions = habit.DoneDates.Count;

        return actualCompletions / expectedCompletions;
    }

    private double CalculateUrgencyScore(Habit habit, DateTime currentDate)
    {
        if (!habit.NextDueDate.HasValue) return 0;

        var daysOverdue = (currentDate - habit.NextDueDate.Value).TotalDays;
        if (daysOverdue <= 0) return 0;

        // Modified urgency calculation to be more responsive
        return Math.Min(5.0, Math.Log(daysOverdue + 1, 2));
    }

    private double CalculatePriority(Habit habit, DateTime currentDate)
    {
        var balanceScore = CalculateBalanceScore(habit, currentDate);
        var urgencyScore = CalculateUrgencyScore(habit, currentDate);
        var completionRate = CalculateCompletionRate(habit, currentDate);
        var frequencyScore = Math.Min(3.0, 1.0 / habit.Frequency);

        var priority = (habit.Importance * IMPORTANCE_WEIGHT) + 
                      (habit.Severity * SEVERITY_WEIGHT) +
                      (habit.Age * AGE_WEIGHT) +
                      (urgencyScore * URGENCY_WEIGHT) +
                      (balanceScore * BALANCE_WEIGHT) +
                      ((1 - completionRate) * COMPLETION_RATE_WEIGHT) +
                      (frequencyScore * 2.0);

        // Add randomization factor to prevent static scheduling
        return priority * (0.95 + (new Random().NextDouble() * 0.1));
    }

    public Dictionary<DateTime, List<(Habit habit, int timeSlotStart, int timeSlotEnd)>> GenerateMonthlySchedule(DateTime monthStart, int monthLength)
    {
        var schedule = new Dictionary<DateTime, List<(Habit habit, int timeSlotStart, int timeSlotEnd)>>();
        var habitLastScheduledDate = new Dictionary<Guid, DateTime>();
        var random = new Random();

        for (int i = 0; i < monthLength; i++)
        {
            var currentDate = monthStart.AddDays(i).Date;
            schedule[currentDate] = new List<(Habit habit, int timeSlotStart, int timeSlotEnd)>();

            int dayOfMonth = ((currentDate.Day - 1) % 30) + 1;
            if (!_dailyAvailableHours.TryGetValue(dayOfMonth, out int availableHours)) continue;

            var availableMinutes = availableHours * 60;
            var scheduledMinutes = 0;

            // Keep track of habits scheduled today to ensure variety
            var habitsScheduledToday = new HashSet<Guid>();

            while (scheduledMinutes < availableMinutes)
            {
                var eligibleHabits = _habits.Where(h =>
                    !habitsScheduledToday.Contains(h.Id) &&
                    !h.IsDoneOnDate(currentDate) &&
                    h.LengthMinutes <= (availableMinutes - scheduledMinutes) &&
                    (!habitLastScheduledDate.ContainsKey(h.Id) ||
                     (currentDate - habitLastScheduledDate[h.Id]).TotalDays >= (1.0 / h.Frequency)))
                    .ToList();

                if (!eligibleHabits.Any()) break;

                // Select top 3 habits by priority and randomly choose one to add variety
                var topHabits = eligibleHabits
                    .OrderByDescending(h => CalculatePriority(h, currentDate))
                    .Take(3)
                    .ToList();

                var selectedHabit = topHabits[random.Next(Math.Min(3, topHabits.Count))];

                schedule[currentDate].Add((selectedHabit, scheduledMinutes, scheduledMinutes + selectedHabit.LengthMinutes));
                habitLastScheduledDate[selectedHabit.Id] = currentDate;
                habitsScheduledToday.Add(selectedHabit.Id);

                scheduledMinutes += selectedHabit.LengthMinutes;
            }

            UpdateHabitsAges(currentDate);
        }

        return schedule;
    }

    private void UpdateHabitsAges(DateTime currentDate)
    {
        foreach (var habit in _habits)
        {
            if (habit.NextDueDate.HasValue && currentDate > habit.NextDueDate.Value)
            {
                habit.Age = (currentDate - habit.NextDueDate.Value).TotalDays;
            }
        }
    }
}