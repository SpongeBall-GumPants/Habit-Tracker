## Table of Contents

- [Features](#features)
    
- [Project Structure](#project-structure)
    
- [Technologies Used](#technologies-used)
    
- [Installation](#installation)
    
- [Usage](#usage)
    
    - [Running the Application](#running-the-application)
        
    - [Managing Habits](#managing-habits)
        
- [Architecture](#architecture)
    
    - [Core Library: The_System](#core-library-the_system)
        
    - [Scheduling Algorithm](#scheduling-algorithm)
        
- [Data Files](#data-files)
    
- [Contributing](#contributing)
    
- [License](#license)
    

## Features

- **Dynamic Habit Prioritization:** Uses a priority-based CPU scheduling approach to rank habits by Importance and Severity, with an aging mechanism to prevent starvation [Wikipedia](https://en.wikipedia.org/wiki/Aging_%28scheduling%29?utm_source=chatgpt.com)[GeeksforGeeks](https://www.geeksforgeeks.org/starvation-and-aging-in-operating-systems/?utm_source=chatgpt.com).
    
- **Persistent JSON Storage:** All habits are loaded from and saved to `Data_data.json`, ensuring data persists across sessions [GitHub](https://github.com/SpongeBall-GumPants/Habit-Tracker/commit/887dab620416edb8b01bfb096b8226a4f6ec4009).
    
- **WPF-based User Interface:** Provides windows to add, delete, update, mark as done, manage time allocations, view the generated schedule, and inspect the raw JSON [GitHub](https://github.com/SpongeBall-GumPants/Habit-Tracker/commit/887dab620416edb8b01bfb096b8226a4f6ec4009).
    

## Project Structure

`Habit-Tracker/
`├── The_System/                # Core scheduling & data logic
`│   ├── The_System/           
`│   │   ├── DataManager.cs    
`│   │   ├── Habit.cs          
`│   │   ├── Scheduler.cs      
`│   │   ├── Program.cs        
`│   │   ├── Data_data.json    
`│   │   └── scheduler_data.json 
`│   └── The_System.sln        
`└── HabitTracker/              # UI application
 `   └── HabitTracker/
  `      ├── MainWindow.xaml(.cs)
   `     ├── AddHabitWindow.xaml(.cs)
   `     ├── DeleteHabitWindow.xaml(.cs)
   `     ├── UpdateHabitWindow.xaml(.cs)
   `     ├── MarkDoneWindow.xaml(.cs)
   `     ├── ManageMinutesWindow.xaml(.cs)
   `     ├── HabitTracker.csproj
   `     └── App.xaml(.cs)


All code is implemented in C# with .NET 6.0 and WPF.
## Technologies Used

- **Language:** C# (100% of codebase)
    
- **Framework:** .NET 6.0 (WPF)
    
- **Data Format:** JSON for habit definitions and schedule logs
    

## Installation

1. **Clone the repository**
    
    `git clone https://github.com/SpongeBall-GumPants/Habit-Tracker.git`
    
2. **Open the solution**  
    Launch `The_System/The_System.sln` in Visual Studio.
    
3. **Restore NuGet packages** and build the solution.
    

## Usage

### Running the Application

- Set **HabitTracker** as the startup project and press **F5** (or click **Run**).
    
- The main window (`MainWindow.xaml`) will appear, showcasing the habit list and menu buttons.
    

### Managing Habits

- **Add Habit:** Opens **AddHabitWindow**, collect Name, Description, Length, Frequency, Importance (0–5), Severity (0–5), Planned Time/Week, then save.
    
- **Delete Habit:** Opens **DeleteHabitWindow**, select a habit and confirm deletion.
    
- **Update Habit:** Opens **UpdateHabitWindow**, modify existing habit fields, and save changes.
    
- **Mark as Done:** Opens **MarkDoneWindow** to record completion for the selected habit that day, update last performed date.
    
- **Manage Minutes:** Opens **ManageMinutesWindow** to log time spent on a habit from calendar, if more/less time spared for habits it can be managed by this window.
    
- **View Schedule:** Generates and displays a prioritized list of habits based on the scheduling algorithm.
    
- **View JSON:** Loads and displays the raw `Data_data.json` content for debugging or manual edits.
    

## Architecture

### Core Library: The_System

- **`DataManager`** handles loading and saving the habit list to `Data_data.json`, manages unique IDs, and records scheduling history.
    
- **`Habit`** encapsulates properties: `Id`, `Name`, `Description`, `LengthMinutes`, `Frequency`, `Importance`, `Severity`, `PlannedTimePerWeek`.
    
- **`Scheduler`** implements a priority-based scheduling algorithm, including an aging technique to ensure low-priority habits eventually receive CPU time [Wikipedia](https://en.wikipedia.org/wiki/Aging_%28scheduling%29?utm_source=chatgpt.com)[Testbook](https://testbook.com/operating-system/aging-in-os?utm_source=chatgpt.com).
    

### Scheduling Algorithm

The scheduling algorithm follows a **priority-based** model where each habit’s initial priority is derived from its Importance and Severity, modulated by Frequency. An **aging** mechanism gradually increases the priority of habits that wait longer, preventing indefinite starvation as described in operating-system scheduling literature [GeeksforGeeks](https://www.geeksforgeeks.org/advantages-and-disadvantages-of-various-cpu-scheduling-algorithms/?utm_source=chatgpt.com)[Wikipedia](https://en.wikipedia.org/wiki/Multilevel_feedback_queue?utm_source=chatgpt.com).

- **Aging Principle:** Incrementally boost the priority of waiting habits over time, ensuring equitable scheduling [Wikipedia](https://en.wikipedia.org/wiki/Aging_%28scheduling%29?utm_source=chatgpt.com)[GeeksforGeeks](https://www.geeksforgeeks.org/starvation-and-aging-in-operating-systems/?utm_source=chatgpt.com).
    
- **Reference Implementations:** See examples in CPU scheduling repositories such as yousefkotp/CPU-Scheduling-Algorithms [GitHub](https://github.com/yousefkotp/CPU-Scheduling-Algorithms?utm_source=chatgpt.com) and academic notes on CPU schedulingturn10search2turn10search3.
    

## Data Files

- **`Data_data.json`**: Stores the active habit list.
    
- **`scheduler_data.json`**: Logs each run of the scheduling algorithm for analysis.
    

## Contributing

Contributions are welcome! Please fork the repo, create a new branch for your feature or bugfix, and submit a pull request. Ensure code follows C# conventions and passes existing unit tests.

## License

This project is licensed under the **MIT License**. Feel free to copy, modify, and distribute under the terms of MIT.
