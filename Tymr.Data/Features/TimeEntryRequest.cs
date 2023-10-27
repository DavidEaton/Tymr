namespace Tymr.Data.Features
{
    public class TimeEntryRequest
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeOnly Begin { get; set; } = new TimeOnly();
        public TimeOnly End { get; set; } = new TimeOnly();
        public double Duration { get; set; }
    }
}
