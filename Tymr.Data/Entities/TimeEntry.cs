using CSharpFunctionalExtensions;

namespace Tymr.Data.Entities
{
    public class TimeEntry : Entity
    {
        private static readonly int minimumDuration = 0;
        public DateTime Date { get; private set; }
        public int Duration { get; private set; }
        private TimeEntry(DateTime date, int duration)
        {
            Date = date;
            Duration = duration;
        }

        public static Result<TimeEntry> Create(int duration)
        {
            return duration >= minimumDuration
                ? Result.Success(new TimeEntry(DateTime.Now, duration))
                : Result.Failure<TimeEntry>($"Duration must be greater than {minimumDuration}");
        }

        public Result<DateTime> SetDate(DateTime date)
        {
            return
                date > DateTime.Now
                ? Result.Failure<DateTime>("Date cannot be in the future")
                : Result.Success(Date = date);

        }

        protected TimeEntry()
        {
        }
    }
}