namespace VerstaTask.Interfaces
{
    using System;

    public interface ITimeHelper
    {
        public DateTime GetUtcDateTime(DateTime sourceDate);
    }
}
