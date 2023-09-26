using CSharpFunctionalExtensions;

namespace Tymr.Data.Entities
{
    public class TimeEntry
    {
        private static readonly TimeSpan minimumDuration = TimeSpan.FromMinutes(0);
        public DateTime Date { get; private set; }
        public TimeOnly Begin { get; private set; }
        public TimeOnly End { get; private set; }
        public TimeSpan Duration => End - Begin;

        private TimeEntry(DateTime date, TimeOnly begin, TimeOnly end)
        {
            Date = date;
            Begin = begin;
            End = end;
        }

        public static Result<TimeEntry> Create(TimeOnly begin, TimeOnly end)
        {
            if (begin > end)
                return Result.Failure<TimeEntry>("Begin time must be earlier than end time");

            return (end - begin) >= minimumDuration
            ? Result.Success(new TimeEntry(DateTime.Now, begin, end))
            : Result.Failure<TimeEntry>($"Duration must be greater than {minimumDuration}");
        }

        public Result<DateTime> SetDate(DateTime date)
        {
            return
                date > DateTime.Now
                ? Result.Failure<DateTime>("Date cannot be in the future")
                : Result.Success(Date = date);
        }
    }
}