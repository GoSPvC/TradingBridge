// <copyright file="DateTimeConverter.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using System.Globalization;
using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.Core.Common.Converters;

/// <summary>
/// Various date time converters.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "01.01.2026")]
public static class DateTimeConverter
{
    /// <summary>
    /// Converts an ISO8601-String (yyyy-MM-ddTHH:mm:ss.fffZ) into DateTime format.
    /// </summary>
    /// <param name="iso8601String">ISO8601-String (yyyy-MM-ddTHH:mm:ss.fffZ).</param>
    /// <returns>DateTime.</returns>
    public static DateTime ISO8601StringToDateTime(string iso8601String)
    {
        var dateTime = DateTime.Parse(iso8601String, null, DateTimeStyles.RoundtripKind);
        return dateTime;
    }

    /// <summary>
    /// Converts DateTime format into an ISO8601-String (yyyy-MM-ddTHH:mm:ss.fffZ).
    /// </summary>
    /// <param name="dateTime">DateTime.</param>
    /// <returns>ISO8601-String (yyyy-MM-ddTHH:mm:ss.fffZ).</returns>
    public static string DateTimeToISO8601String(DateTime dateTime)
    {
        var iso8601String = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        return iso8601String;
    }

    /// <summary>
    /// Converts a DateTime-String (yyyy-MM-dd HH:mm:ss) into DateTime format.
    /// </summary>
    /// <param name="dateTimeString">DateTime-String (yyyy-MM-dd HH:mm:ss).</param>
    /// <returns>DateTime.</returns>
    public static DateTime DateTimeStringToDateTime(string dateTimeString)
    {
        var dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        return dateTime;
    }

    /// <summary>
    /// Converts DateTime format into a DateTime-String (yyyy-MM-dd HH:mm:ss).
    /// </summary>
    /// <param name="dateTime">DateTime.</param>
    /// <returns>DateTime-String (yyyy-MM-dd HH:mm:ss).</returns>
    public static string DateTimeToDateTimeString(DateTime dateTime)
    {
        var dateTimeString = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        return dateTimeString;
    }

    /// <summary>
    /// Converts Milliseconds (UNIX-Time) into DateTime format.
    /// </summary>
    /// <param name="milliSeconds">Milliseconds (UNIX-Time).</param>
    /// <returns>DateTime.</returns>
    public static DateTime MilliSecondsToDateTime(long milliSeconds)
    {
        var timeSpan = TimeSpan.FromMilliseconds(milliSeconds);
        var dateTimeStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTime = dateTimeStart.Add(timeSpan);
        return dateTime;
    }

    /// <summary>
    /// Converts DateTime format into Milliseconds (UNIX-Time).
    /// </summary>
    /// <param name="dateTime">DateTime.</param>
    /// <returns>Milliseconds (UNIX-Time).</returns>
    public static long DateTimeToMilliseconds(DateTime dateTime)
    {
        var millisecondsDateTime = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        return millisecondsDateTime;
    }

    /// <summary>
    /// Returns a DateTime.Now() in Milliseconds (UNIX-Time) format.
    /// </summary>
    /// <returns>Milliseconds (UNIX-Time).</returns>
    public static long MilliSecondsNow()
    {
        var millisecondsNow = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        return millisecondsNow;
    }

    /// <summary>
    /// Converts Seconds (UNIX-Time) into DateTime format.
    /// </summary>
    /// <param name="seconds">Seconds (UNIX-Time).</param>
    /// <returns>DateTime.</returns>
    public static DateTime SecondsToDateTime(long seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        var dateTimeStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTime = dateTimeStart.Add(timeSpan);
        return dateTime;
    }

    /// <summary>
    /// Converts DateTime format into Seconds (UNIX-Time).
    /// </summary>
    /// <param name="dateTime">DateTime.</param>
    /// <returns>Seconds (UNIX-Time).</returns>
    public static long DateTimeToSeconds(DateTime dateTime)
    {
        var unixDateTime = new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        return unixDateTime;
    }

    /// <summary>
    /// Returns a DateTime.Now() in Seconds (UNIX-Time) format.
    /// </summary>
    /// <returns>Seconds (UNIX-Time).</returns>
    public static long SecondsNow()
    {
        var unixTimeNow = DateTimeOffset.Now.ToUnixTimeSeconds();
        return unixTimeNow;
    }

    /// <summary>
    /// Returns the nearest DateTime from a list of DateTimes.
    /// </summary>
    /// <param name="dateTime">DateTime.</param>
    /// <param name="dateTimes">List of DateTimes.</param>
    /// <returns>Nearest DateTime.</returns>
    public static DateTime GetNearestDate(DateTime dateTime, params DateTime[] dateTimes)
    {
        return dateTime.Add(dateTimes.Min(dateTimeItem => (dateTimeItem - dateTime).Duration()));
    }

    /// <summary>
    /// Returns a DateTime for each day betweet two given DateTimes.
    /// </summary>
    /// <param name="from">From DateTime.</param>
    /// <param name="thru">Thru DateTime.</param>
    /// <returns>DateTimes.</returns>
    public static IEnumerable<IEquatable<DateTime>> EachDay(DateTime from, DateTime thru)
    {
        for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
        {
            yield return day;
        }
    }

    /// <summary>
    /// Returns a DateTime for each month betweet two given DateTimes.
    /// </summary>
    /// <param name="from">From DateTime.</param>
    /// <param name="thru">Thru DateTime.</param>
    /// <returns>DateTimes.</returns>
    public static IEnumerable<IEquatable<DateTime>> EachMonth(DateTime from, DateTime thru)
    {
        for (var month = from.Date; month.Date <= thru.Date; month = month.AddMonths(1))
        {
            yield return month;
        }
    }
}
