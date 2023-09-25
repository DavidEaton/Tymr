namespace Tymr.Client.Features
{
    public class TimeEntryRequest
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeOnly Begin { get; set; } = new TimeOnly();
        public TimeOnly End { get; set; } = new TimeOnly();
        public int Duration { get; set; }


        TimeOnly startTime = new TimeOnly(12, 30); // 12:30
        TimeOnly endTime = new TimeOnly(16, 45);  // 16:45

    }
}
