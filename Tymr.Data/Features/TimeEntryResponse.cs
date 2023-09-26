namespace Tymr.Data.Features
{
    public class TimeEntryResponse
    {
        public DateTime Date { get; set; } = DateTime.MinValue;
        public TimeOnly Begin { get; set; } = new TimeOnly();
        public TimeOnly End { get; set; } = new TimeOnly();
        public TimeSpan Duration => End - Begin;
    }
}
