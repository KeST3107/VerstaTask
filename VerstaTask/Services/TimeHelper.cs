namespace VerstaTask.Services
{
    using System;
    using VerstaTask.Interfaces;

    public class TimeHelper : ITimeHelper
    {
        public DateTime GetUtcDateTime(DateTime sourceDate)
        {
            var dateUtc = new DateTimeOffset(sourceDate, TimeSpan.Zero);
            return dateUtc.UtcDateTime;
        }
    }
}
