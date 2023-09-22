namespace Tymr.Data.Entities
{
    public class TimeEntry : Entity
    {
        DateTime Date { get; set; }
        int Duration { get; set; }

        protected TimeEntry()
        {
        }
    }
}