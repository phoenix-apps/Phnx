using System;

namespace MarkSFrancis.Abstractions.CurrentDateTime
{
    /// <summary>
    /// Provides a way to get the current date and time
    /// </summary>
    public class CurrentDateTimeService : ICurrentDateTimeService
    {
        /// <summary>
        /// Get the current time and date for the UTC timezone
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Get the current time and date in the system's local timezone
        /// </summary>
        public DateTime LocalNow => DateTime.Now;
    }
}
