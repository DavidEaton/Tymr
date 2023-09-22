namespace Tymr.Client.Shared
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            int daysSinceStartOfWeek = (7 + (date.DayOfWeek - startDayOfWeek)) % 7;
            return date.Date.AddDays(-daysSinceStartOfWeek);
        }
    }
}
