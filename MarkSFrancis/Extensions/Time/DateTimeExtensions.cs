using System;

namespace MarkSFrancis.Extensions.Time
{
    public static class DateTimeExtensions
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public static int Age(this DateTime dob, TimeZoneInfo dobTimeZone)
        {
            DateTime dobUtc = dob.ToUtc(dobTimeZone);

            return AgeUtc(dobUtc);
        }

        public static int AgeUtc(this DateTime dob)
        {
            DateTime now = DateTime.UtcNow.Date;
            if (now.DayOfYear < dob.DayOfYear)
            {
                return (now.Year + 1) - dob.Year;
            }
            
            return now.Year - dob.Year;
        }

        public static DateTime ToUtc(this DateTime dt, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTime(dt, timeZone, TimeZoneInfo.Utc);
        }

        public static DateTime ToLocal(this DateTime dt, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.Utc, timeZone);
        }

        public static string AsDateString(this DateTime dt, bool numericDate = true)
        {
            return numericDate ? dt.ToString("d") : dt.ToString("D");
        }

        public static string AsTimeString(this DateTime dt, bool includeSeconds = false)
        {
            return includeSeconds ? dt.ToString("T") : dt.ToString("t");
        }

        public static TimeSpan Elapsed(this DateTime dt, DateTime dt2)
        {
            return dt - dt2;
        }

        public static DateTime EndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day + 1).Subtract(new TimeSpan(1));
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month + 1, 1).AddMonths(1).Subtract(new TimeSpan(1));
        }

        public static DateTime EndOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year + 1, 1, 1).Subtract(new TimeSpan(1));
        }

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

            return new DateTime(dt.Year, dt.Month, dt.Day - daysToSubtract);
        }

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
            return new DateTime(dt.Year, dt.Month, dt.Day + daysToAdd);
        }

        public static bool IsAfternoon(this DateTime dt)
        {
            return dt.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }

        public static bool IsMorning(this DateTime dt)
        {
            return !IsAfternoon(dt);
        }

        public static bool IsInTheFuture(this DateTime dt, TimeZoneInfo dtTimeZone)
        {
            return IsInTheFutureUtc(dt.ToUtc(dtTimeZone));
        }

        public static bool IsInTheFutureUtc(this DateTime dt)
        {
            return dt > DateTime.UtcNow;
        }

        public static bool IsInThePast(this DateTime dt, TimeZoneInfo dtTimeZone)
        {
            return IsInThePastUtc(dt.ToUtc(dtTimeZone));
        }

        public static bool IsInThePastUtc(this DateTime dt)
        {
            return dt < DateTime.UtcNow;
        }

        public static bool IsToday(this DateTime dt, TimeZoneInfo dtTimeZone)
        {
            return IsTodayUtc(dt.ToUtc(dtTimeZone));
        }

        public static bool IsTodayUtc(this DateTime dt)
        {
            return dt.Date == DateTime.UtcNow.Date;
        }

        public static bool IsWeekend(this DateTime dt)
        {
            var dayOfWeek = dt.DayOfWeek;
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }

        public static DateTime SetTime(this DateTime dt, int hour = 0, int minute = 0, int second = 0,
            int millisecond = 0)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, hour, minute, second, millisecond);
        }

        public static TimeSpan ToEpochTimeSpan(this DateTime dt)
        {
            return dt.Subtract(Epoch);
        }

        public static DateTime Tomorrow(this DateTime dt)
        {
            return dt.Date.AddDays(1);
        }

        public static DateTime Yesterday(this DateTime dt)
        {
            return dt.Date.AddDays(-1);
        }
    }
}