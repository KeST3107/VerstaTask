namespace VerstaTask.Services
{
    using System;
    using VerstaTask.Interfaces;

    public class TimeHelper : ITimeHelper
    {
        public DateTime GetUtcDateTime(DateTime sourceDate)
        {
            var dateUtc = new DateTime(sourceDate.Year, sourceDate.Month, sourceDate.Day, sourceDate.Hour,
                sourceDate.Minute, sourceDate.Second, DateTimeKind.Utc);
            return dateUtc;
        }
    }
}
