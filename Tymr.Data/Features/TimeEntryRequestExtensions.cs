using Tymr.Data.Entities;

namespace Tymr.Data.Features
{
    public static class TimeEntryRequestExtensions
    {
        public static bool IsNotUnique(this TimeEntryRequest entry, IEnumerable<TimeEntry> existingEntries)
        {
            return existingEntries.Any(existingEntry =>
                existingEntry.Date == entry.Date &&
                existingEntry.Begin == entry.Begin &&
                existingEntry.End == entry.End);
        }
    }
}
