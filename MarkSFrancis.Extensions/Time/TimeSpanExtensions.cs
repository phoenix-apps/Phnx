using System;
using System.Collections.Generic;
using MarkSFrancis.Extensions.Numeric;

namespace MarkSFrancis.Extensions.Time
{
    [Flags]
    public enum TimeComponents
    {
        Milliseconds = 1, // 1
        Seconds = 1 << 1,   // 2
        Minutes = 1 << 2,   // 4
        Hours = 1 << 3,   // 8
        Days = 1 << 4,   // 16
    }

    public static class TimeSpanExtensions
    {
        public static DateTime FromEpochTimeSpan(this TimeSpan span)
        {
            return DateTimeExtensions.Epoch + span;
        }

        public static string AsTimeString(this TimeSpan ts,
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

            List<string> timeSpanComponents = new List<string>();
            string stringPrefix =
                (tsComponents & TimeComponents.Days) != 0 ?
                ts.Days + daysUnit + ", " : "";

            if ((tsComponents & TimeComponents.Hours) != 0)
            {
                timeSpanComponents.Add(ts.Hours.ToString(2) + hoursUnit);
            }

            if ((tsComponents & TimeComponents.Minutes) != 0)
            {
                timeSpanComponents.Add(ts.Minutes.ToString(2) + minutesUnit);
            }

            if ((tsComponents & TimeComponents.Seconds) != 0)
            {
                timeSpanComponents.Add(ts.Seconds.ToString(2) + secondsUnit);
            }

            string stringSuffix =
                (tsComponents & TimeComponents.Milliseconds) != 0 ?
                (longFormat ? ", " : ".") +
                ts.Milliseconds.ToString(3) + millisecondsUnit : "";

            string formattedString =
                stringPrefix +
                string.Join(longFormat ? ", " : ":", timeSpanComponents) +
                stringSuffix;

            return formattedString;
        }
    }
}