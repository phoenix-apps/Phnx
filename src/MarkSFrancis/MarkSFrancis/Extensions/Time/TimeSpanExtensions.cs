using MarkSFrancis.Extensions.Numeric;
using System;
using System.Text;

namespace MarkSFrancis.Extensions.Time
{
    /// <summary>
    /// A collection of <see cref="TimeSpan"/> components, used for formatting in <see cref="TimeSpanExtensions.ToString"/>
    /// </summary>
    [Flags]
    public enum TimeComponents
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Milliseconds = 1, // 1
        Seconds = 1 << 1,   // 2
        Minutes = 1 << 2,   // 4
        Hours = 1 << 3,   // 8
        Days = 1 << 4,   // 16
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Extensions for <see cref="TimeSpan"/>
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Create a new <see cref="DateTime"/> from a <see cref="TimeSpan"/> after <see cref="DateTimeExtensions.Epoch"/>
        /// </summary>
        /// <param name="span">The time difference between now an <see cref="DateTimeExtensions.Epoch"/></param>
        /// <returns></returns>
        public static DateTime TimeSinceEpochToDateTime(this TimeSpan span)
        {
            return DateTimeExtensions.Epoch + span;
        }

        /// <summary>
        /// Represent a <see cref="TimeSpan"/> as a <see cref="string"/>, formatting it with only certain parts <see cref="TimeSpan"/>, such as "3 days 04:13:22"
        /// </summary>
        /// <param name="ts">The <see cref="TimeSpan"/> to represent</param>
        /// <param name="timeComponents">The <see cref="TimeComponents"/> to include in the outputted string</param>
        /// <param name="longFormat">Whether to display an extended format (such as "3 hours, 5 minutes" or "03:05" </param>
        /// <exception cref="ArgumentException">Only applies when <paramref name="longFormat"/> is <see langword="false"/>. <paramref name="timeComponents"/> represents an unclear time.
        /// These include the following:
        /// Days, Hours, Seconds
        /// Days, Hours, Milliseconds
        /// Days, Minutes, Milliseconds
        /// Hours, Seconds
        /// Hours, Milliseconds
        /// Minutes, Milliseconds
        /// </exception>
        /// <returns></returns>
        public static string ToString(this TimeSpan ts,
            TimeComponents timeComponents =
                TimeComponents.Hours |
                TimeComponents.Minutes |
                TimeComponents.Seconds,
            bool longFormat = false)
        {
            if (!longFormat)
            {
                // Check for invalid component combinations
                switch (timeComponents)
                {
                    case TimeComponents.Days | TimeComponents.Hours | TimeComponents.Seconds:
                    case TimeComponents.Days | TimeComponents.Hours | TimeComponents.Milliseconds:
                    case TimeComponents.Days | TimeComponents.Minutes | TimeComponents.Milliseconds:
                    case TimeComponents.Hours | TimeComponents.Seconds:
                    case TimeComponents.Hours | TimeComponents.Milliseconds:
                    case TimeComponents.Minutes | TimeComponents.Milliseconds:
                        throw ErrorFactory.Default.ArgumentException("Time Components are invalid. They cannot be only " + timeComponents.ToString() + " and in a short format", nameof(timeComponents)).Create();
                }
            }

            string days = string.Empty;
            StringBuilder formattedText = new StringBuilder();

            // Delimiters are placed preceding the element
            void AppendItem(string appendMe, string longDelimiter = ", ", string shortDelimiter = ":")
            {
                if (formattedText.Length > 0)
                {
                    formattedText.Append((longFormat ? longDelimiter : shortDelimiter) + appendMe);

                }
                else
                {
                    formattedText.Append(appendMe);
                }
            }

            if ((timeComponents & TimeComponents.Days) != 0)
            {
                string daysUnit = ts.Days == 1 ? " day" : " days";

                if (timeComponents != TimeComponents.Days)
                {
                    days = ts.Days + daysUnit + ", ";
                }
                else
                {
                    return ts.Days + daysUnit;
                }
            }

            if ((timeComponents & TimeComponents.Hours) != 0)
            {
                string hours = longFormat ? ts.Hours.ToString() : ts.Hours.ToString(2);

                if (longFormat)
                {
                    hours += ts.Hours == 1 ? " hour" : " hours";
                }

                AppendItem(hours);
            }

            if ((timeComponents & TimeComponents.Minutes) != 0)
            {
                string minutes = longFormat ? ts.Minutes.ToString() : ts.Minutes.ToString(2);

                if (longFormat)
                {
                    minutes += ts.Minutes == 1 ? " minute" : " minutes";
                }

                AppendItem(minutes);
            }

            if ((timeComponents & TimeComponents.Seconds) != 0)
            {
                string seconds = longFormat ? ts.Seconds.ToString() : ts.Seconds.ToString(2);

                if (longFormat)
                {
                    seconds += ts.Seconds == 1 ? " second" : " seconds";
                }

                AppendItem(seconds);
            }

            if ((timeComponents & TimeComponents.Milliseconds) != 0)
            {
                string milliseconds = longFormat ? ts.Milliseconds.ToString() : ts.Milliseconds.ToString(3);

                if (longFormat)
                {
                    milliseconds += ts.Milliseconds == 1 ? " millisecond" : " milliseconds";
                }

                if (timeComponents != TimeComponents.Milliseconds)
                {
                    AppendItem(milliseconds, shortDelimiter: ".");
                }
                else
                {
                    return milliseconds;
                }
            };

            return days + formattedText.ToString();
        }
    }
}