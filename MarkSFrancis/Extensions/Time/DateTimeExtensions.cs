using System;

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
        /// Get a person's age from their date of birth and the time zone they were born in
        /// </summary>
        /// <param name="dob">The person's date of birth</param>
        /// <param name="dobTimeZone">The time zone that the person was born in</param>
        /// <returns></returns>
        public static int Age(this DateTime dob, TimeZoneInfo dobTimeZone)
        {
            DateTime dobUtc = dob.ToUtc(dobTimeZone);

            return AgeUtc(dobUtc);
        }

        /// <summary>
        /// Get a person's age from their date of birth in UTC time zone
        /// </summary>
        /// <param name="dob">The person's date of birth in UTC time zone</param>
        /// <returns></returns>
        public static int AgeUtc(this DateTime dob)
        {
            DateTime now = DateTime.UtcNow.Date;
            if (now.DayOfYear < dob.DayOfYear)
            {
                return (now.Year + 1) - dob.Year;
            }

            return now.Year - dob.Year;
        }

        /// <summary>
        /// Convert <paramref name="dt"/> to UTC given its <see cref="TimeZoneInfo"/>
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="timeZone">The time zone to convert from</param>
        /// <returns></returns>
        public static DateTime ToUtc(this DateTime dt, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTime(dt, timeZone, TimeZoneInfo.Utc);
        }

        /// <summary>
        /// Convert <paramref name="dt"/> from UTC to a time zone
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="timeZone">The time zone to convert to</param>
        /// <returns></returns>
        public static DateTime ToLocal(this DateTime dt, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.Utc, timeZone);
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a string, displaying only the <see cref="DateTime.Date"/> component
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="numericDate">Whether to show the date in a numeric format (e.g 1/1/2001) or a text format (e.g 1st January 2001)</param>
        /// <returns></returns>
        public static string AsDateString(this DateTime dt, bool numericDate = true)
        {
            return numericDate ? dt.ToString("d") : dt.ToString("D");
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a string, displaying only the <see cref="DateTime.TimeOfDay"/> component
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <param name="includeSeconds">Whether to include seconds in the time format (e.g 00:00 vs 00:00:00)</param>
        /// <returns></returns>
        public static string AsTimeString(this DateTime dt, bool includeSeconds = false)
        {
            return includeSeconds ? dt.ToString("T") : dt.ToString("t");
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> representing the time of the very end of the day that <paramref name="dt"/> is within
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to get the end of the day of</param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day + 1).AddTicks(-1);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> representing the time of the very end of the month that <paramref name="dt"/> is within
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to get the end of the month of</param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month + 1, 1).AddMonths(1).AddTicks(-1);
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
        /// Gets whether <paramref name="dt"/> (localized) is in the future
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <param name="dtTimeZone">The time zone that <paramref name="dt"/> is in</param>
        /// <returns></returns>
        public static bool IsInTheFuture(this DateTime dt, TimeZoneInfo dtTimeZone)
        {
            return IsInTheFutureUtc(dt.ToUtc(dtTimeZone));
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> (UTC) is in the future
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsInTheFutureUtc(this DateTime dt)
        {
            return dt > DateTime.UtcNow;
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> (localized) is in the past
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <param name="dtTimeZone">The time zone that <paramref name="dt"/> is in</param>
        /// <returns></returns>
        public static bool IsInThePast(this DateTime dt, TimeZoneInfo dtTimeZone)
        {
            return IsInThePastUtc(dt.ToUtc(dtTimeZone));
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> (UTC) is in the past
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsInThePastUtc(this DateTime dt)
        {
            return dt < DateTime.UtcNow;
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> (localized) is within today
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <param name="dtTimeZone">The time zone that <paramref name="dt"/> is in</param>
        /// <returns></returns>
        public static bool IsToday(this DateTime dt, TimeZoneInfo dtTimeZone)
        {
            return IsTodayUtc(dt.ToUtc(dtTimeZone));
        }

        /// <summary>
        /// Gets whether <paramref name="dt"/> (UTC) is within today
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to check</param>
        /// <returns></returns>
        public static bool IsTodayUtc(this DateTime dt)
        {
            return dt.Date == DateTime.UtcNow.Date;
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
        public static DateTime SetTime(this DateTime dt, int hour, int minute, int second = 0,
            int millisecond = 0)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, hour, minute, second, millisecond);
        }

        /// <summary>
        /// Converts <paramref name="dt"/> to a <see cref="TimeSpan"/> after <see cref="Epoch"/>
        /// </summary>
        /// <param name="dt">The <see cref="DateTime"/> to convert</param>
        /// <returns></returns>
        public static TimeSpan ToEpochTimeSpan(this DateTime dt)
        {
            return dt.Subtract(Epoch);
        }
    }
}