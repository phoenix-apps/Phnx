using System;
using System.Globalization;

namespace MarkSFrancis.Extensions.Time
{
    /// <summary>
    /// Extensions for <see cref="DateTime"/>
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 1st of January, 1970. Used by JavaScript to represent when time began
        /// </summary>
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Get a person's age from their date of birth
        /// </summary>
        /// <param name="dob">The person's date of birth</param>
        /// <param name="now">The current <see cref="DateTime"/></param>
        /// <returns></returns>
        public static int Age(this DateTime dob, DateTime now)
        {
            int age;

            if (now.DayOfYear < dob.DayOfYear)
            {
                age = (now.Year - 1) - dob.Year;
            }
            else
            {
                age = now.Year - dob.Year;
            }

            return age < 0 ? 0 : age;
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a string, displaying only the <see cref="DateTime.Date"/> component, using the <see cref="CultureInfo.CurrentCulture"/> for formatting
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="shortDate">Whether to show the date in a short format (e.g 1/1/2001) or a long format (e.g 1st January 2001)</param>
        /// <returns></returns>
        public static string AsDateString(this DateTime dt, bool shortDate = true)
        {
            return shortDate ? dt.ToString("d") : dt.ToString("D");
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a string, displaying only the <see cref="DateTime.Date"/> component
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="formatProvider">The format provider to define how the date will be formatted</param>
        /// <param name="shortFormat">Whether to show the date in a short format (e.g 1/1/2001) or a long format (e.g 1st January 2001)</param>
        /// <returns></returns>
        public static string AsDateString(this DateTime dt, IFormatProvider formatProvider, bool shortFormat = true)
        {
            if (formatProvider == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(formatProvider));
            }

            return shortFormat ? dt.ToString("d", formatProvider) : dt.ToString("D", formatProvider);
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a string, displaying only the <see cref="DateTime.TimeOfDay"/> component, using the <see cref="CultureInfo.CurrentCulture"/> for formatting
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="shortFormat">Whether to show the time in a short format (e.g 00:00 vs 00:00:00)</param>
        /// <returns></returns>
        public static string AsTimeString(this DateTime dt, bool shortFormat = true)
        {
            return shortFormat ? dt.ToString("t") : dt.ToString("T");
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a string, displaying only the <see cref="DateTime.Date"/> component
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="formatProvider">The format provider to define how the date will be formatted</param>
        /// <param name="shortFormat">Whether to show the time in a short format (e.g 00:00 vs 00:00:00)</param>
        /// <returns></returns>
        public static string AsTimeString(this DateTime dt, IFormatProvider formatProvider, bool shortFormat = true)
        {
            if (formatProvider == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(formatProvider));
            }

            return shortFormat ? dt.ToString("t", formatProvider) : dt.ToString("T", formatProvider);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> representing the time of the very end of the day that <paramref name="dt"/> is within
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to get the end of the day of</param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day).AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> representing the time of the very end of the month that <paramref name="dt"/> is within
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to get the end of the month of</param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1).AddMonths(1).AddTicks(-1);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> representing the time of the very end of the year that <paramref name="dt"/> is within
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to get the end of the year of</param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year + 1, 1, 1).AddTicks(-1);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> representing the start of the week
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to get the start of the week of</param>
        /// <param name="weekStartMonday">Whether the week starts on a Monday (<see langword="true"/>) or Sunday (<see langword="false"/>)</param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dt, bool weekStartMonday = true)
        {
            int daysToSubtract = (int)dt.DayOfWeek;

            if (weekStartMonday)
            {
                daysToSubtract--;
                if (daysToSubtract < 0)
                {
                    daysToSubtract = 6;
                }
            }

            return dt.AddDays(-daysToSubtract).Date;
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> representing the very end of the week
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to get the end of the week of</param>
        /// <param name="weekStartMonday">Whether the week starts on a Monday (<see langword="true"/>) or Sunday (<see langword="false"/>)</param>
        /// <returns></returns>
        public static DateTime EndOfWeek(this DateTime dt, bool weekStartMonday = true)
        {
            int daysToAdd = 6 - (int)dt.DayOfWeek;

            if (weekStartMonday)
            {
                daysToAdd++;
                if (daysToAdd > 6)
                {
                    daysToAdd = 0;
                }
            }

            return dt.AddDays(daysToAdd + 1).Date.AddTicks(-1);
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> is in the afternoon
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsAfternoon(this DateTime dt)
        {
            return dt.TimeOfDay >= new TimeSpan(12, 0, 0);
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> is in the morning
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsMorning(this DateTime dt)
        {
            return !IsAfternoon(dt);
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> (UTC) is within today
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <param name="sameDayAs">The <see cref="DateTime"/> to check whether <paramref name="dt"/> is on the same day as</param>
        /// <returns></returns>
        public static bool IsSameDay(this DateTime dt, DateTime sameDayAs)
        {
            return dt.Date == sameDayAs.Date;
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> is within a weekend
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime dt)
        {
            var dayOfWeek = dt.DayOfWeek;
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Sets the time of <paramref name="dt"/> to a value
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to change</param>
        /// <param name="hour">The hour to assign</param>
        /// <param name="minute">The minute to assign</param>
        /// <param name="second">The second to assign</param>
        /// <param name="millisecond">The millisecond to assign</param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime dt, int? hour = null, int? minute = null, int? second = null,
            int? millisecond = null)
        {
            return new DateTime(
                dt.Year,
                dt.Month,
                dt.Day,
                hour ?? dt.Hour,
                minute ?? dt.Minute,
                second ?? dt.Second,
                millisecond ?? dt.Millisecond
            );
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a <see cref="TimeSpan"/> after <see cref="Epoch"/>
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <returns></returns>
        public static TimeSpan ToEpochTimeSpan(this DateTime dt)
        {
            return dt - Epoch;
        }
    }
}