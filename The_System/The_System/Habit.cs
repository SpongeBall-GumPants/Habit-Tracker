using System;
using System.Collections.Generic;
using System.Linq;

public class Habit
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public int LengthMinutes { get; set; }
    public double Frequency { get; set; }
    public double Importance { get; set; }
    public double Severity { get; set; }
    public DateTime? LastPerformedDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public double Age { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<DateTime> DoneDates { get; set; } = new List<DateTime>();
    public double PlannedTimePerWeek { get; set; }
    public double ActualTimePerWeek { get; set; } = 0;

    public Habit(string name, string description, int lengthMinutes, double frequency, double importance, double severity, double plannedTimePerWeek)
    {
        Name = name;
        Description = description;
        LengthMinutes = lengthMinutes;
        Frequency = frequency;
        Importance = importance;
        Severity = severity;
        PlannedTimePerWeek = plannedTimePerWeek;
        NextDueDate = CreatedDate.AddDays(-1);
    }

    public bool IsDoneOnDate(DateTime date)
    {
        return DoneDates.Any(doneDate => doneDate.Date == date.Date);
    }

    public void ResetAge()
    {
        Age = 0;
    }
    public void UpdateAge(DateTime currentDate)
    {
        // Should only start aging after next due date
        if (LastPerformedDate.HasValue && currentDate > NextDueDate)
        {
            Age = (currentDate - NextDueDate.Value).TotalDays;
        }
    }

    public void MarkDone(DateTime currentDate)
    {
        LastPerformedDate = currentDate;
        DoneDates.Add(DateTime.Now);
    }

    public void CalculateNextDueDate()
    {
        if (LastPerformedDate.HasValue)
        {
            NextDueDate = LastPerformedDate.Value.AddDays(1 / Frequency);

        }
        else
        {
            NextDueDate = CreatedDate.AddDays(1 / Frequency);
        }
    } 



}