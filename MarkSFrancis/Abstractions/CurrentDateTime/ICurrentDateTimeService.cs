using System;

namespace MarkSFrancis.Abstractions.CurrentDateTime
{
    /// <summary>
    /// Provides unit-testable ways to get the current date and time
    /// </summary>
    public interface ICurrentDateTimeService
    {
        /// <summary>
        /// Get the current time and date for the UTC timezone
        /// </summary>
        DateTime UtcNow { get; }
    }
}
