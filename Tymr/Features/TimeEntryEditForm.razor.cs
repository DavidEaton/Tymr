﻿using Microsoft.AspNetCore.Components;
using Tymr.Data.Features;

namespace Tymr.Client.Features
{
    public partial class TimeEntryEditForm
    {
        public TimeEntryRequest TimeEntry = new();
        private TimeOnly Begin { get; set; }
        private TimeOnly End { get; set; }
        private string Duration { get; set; }

        [Inject]
        private ITimeEntryRepository TimeEntryRepository { get; set; }

        public void UpdateDuration()
        {
            TimeSpan durationTimeSpan = End.ToTimeSpan() - Begin.ToTimeSpan();
            double durationInHours = durationTimeSpan.TotalHours;

            // Handling negative durations if End time is earlier than Begin time
            if (durationInHours < 0)
            {
                durationInHours += 24;
            }

            Duration = $"{durationInHours:F2}"; // Formatted to 2 decimal places
        }

        private void Save()
        {
            TimeEntry.Begin = Begin;
            TimeEntry.End = End;
            TimeEntry.Duration = double.Parse(Duration);

            TimeEntryRepository.AddAsync(TimeEntry);
        }
    }
}
