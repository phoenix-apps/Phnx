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
        /// <param name="tsComponents">The <see cref="TimeComponents"/> to include in the outputted string</param>
        /// <param name="longFormat">Whether to display an extended format (such as "3 hours, 5 minutes" or "03:05" </param>
        /// <returns></returns>
        public static string ToString(this TimeSpan ts,
            TimeComponents tsComponents =
                TimeComponents.Hours |
                TimeComponents.Minutes |
                TimeComponents.Seconds,
            bool longFormat = false)
        {
            string
                daysUnit = " days",
                hoursUnit = longFormat ? " hours" : "",
                minutesUnit = longFormat ? " minutes" : "",
                secondsUnit = longFormat ? " seconds" : "",
                millisecondsUnit = longFormat ? " milliseconds" : "";

            string days = string.Empty;
            StringBuilder formattedText = new StringBuilder();

            // Delimiters are placed preceding the element
            void AppendItem(string appendMe, string longDelimiter = ", ", string shortDelimiter = "")
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

            if ((tsComponents & TimeComponents.Days) != 0)
            {
                if (tsComponents != TimeComponents.Days)
                {
                    days = ts.Days + daysUnit + " ";
                }
                else
                {
                    return ts.Days + daysUnit;
                }
            }

            if ((tsComponents & TimeComponents.Hours) != 0)
            {
                AppendItem(ts.Hours.ToString(2) + hoursUnit);
            }

            if ((tsComponents & TimeComponents.Minutes) != 0)
            {
                AppendItem(ts.Minutes.ToString(2) + minutesUnit);
            }

            if ((tsComponents & TimeComponents.Seconds) != 0)
            {
                AppendItem(ts.Seconds.ToString(2) + secondsUnit);
            }

            if ((tsComponents & TimeComponents.Milliseconds) != 0)
            {
                if (tsComponents != TimeComponents.Milliseconds)
                {
                    AppendItem(ts.Milliseconds.ToString(3) + millisecondsUnit, shortDelimiter: ".");
                }
                else
                {
                    return ts.Milliseconds.ToString(3) + millisecondsUnit;
                }
            };

            return days + formattedText.ToString();
        }
    }
}