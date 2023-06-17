using System;
using System.Globalization;
using System.Text;

namespace MadRatz.Extensions
{
	public static class DateTimeExtension
	{
		const int EveningEnds = 2;
		const int MorningEnds = 12;
		const int AfternoonEnds = 18;
		static readonly DateTime Date1970 = new DateTime(1970, 1, 1);
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


		public static double UtcOffset => DateTime.Now.Subtract(DateTime.UtcNow).TotalHours;

		#region Age

		public static int CalculateAge(this DateTime dateTime)
		{
			var currentDateTime = DateTime.Now;
			return dateTime.CalculateAge(currentDateTime);
		}

		public static int CalculateAge(this DateTime dateTime, DateTime currentDateTime)
		{
			var years = currentDateTime.Year - dateTime.Year;

			if (currentDateTime.Month < dateTime.Month ||
			    (currentDateTime.Month == dateTime.Month && currentDateTime.Day < dateTime.Day))
			{
				--years;
			}

			return years;
		}

		#endregion

		#region Day

		public static int GetDays(this DateTime dateTime, DateTime toDate)
		{
			var result = Convert.ToInt32(toDate.Subtract(dateTime).TotalDays);
			return result;
		}

		// morning(2-12)/afternoon(13-18)/evening(18-2))
		public static string GetPeriodOfDay(this DateTime dateTime)
		{
			var hour = dateTime.Hour;

			if (hour < EveningEnds)
			{
				return "evening";
			}

			if (hour < MorningEnds)
			{
				return "morning";
			}

			return hour < AfternoonEnds ? "afternoon" : "evening";
		}

		public static bool IsToday(this DateTime dateTime)
		{
			var result = dateTime.Date == DateTime.Today;
			return result;
		}

		public static DateTime IsTomorrow(this DateTime dateTime)
		{
			var result = dateTime.AddDays(1);
			return result;
		}

		public static DateTime IsYesterday(this DateTime dateTime)
		{
			var result = dateTime.AddDays(-1);
			return result;
		}

		#endregion

		#region Week

		public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
		{
			var result = dateTime.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
			return result;
		}

		public static DateTime GetFirstDayOfWeek(this DateTime dateTime, CultureInfo cultureInfo)
		{
			cultureInfo = (cultureInfo ?? CultureInfo.CurrentCulture);
			var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

			while (dateTime.DayOfWeek != firstDayOfWeek)
			{
				dateTime = dateTime.AddDays(-1);
			}

			return dateTime;
		}

		public static DateTime GetLastDayOfWeek(this DateTime dateTime)
		{
			var result = dateTime.GetLastDayOfWeek(CultureInfo.CurrentCulture);
			return result;
		}

		public static DateTime GetLastDayOfWeek(this DateTime dateTime, CultureInfo cultureInfo)
		{
			var result = dateTime.GetFirstDayOfWeek(cultureInfo).AddDays(6);
			return result;
		}

		public static DateTime GetNextWeekday(this DateTime dateTime, DayOfWeek weekday)
		{
			while (dateTime.DayOfWeek != weekday)
			{
				dateTime = dateTime.AddDays(1);
			}

			return dateTime;
		}

		public static DateTime GetPreviousWeekday(this DateTime dateTime, DayOfWeek weekday)
		{
			while (dateTime.DayOfWeek != weekday)
			{
				dateTime = dateTime.AddDays(-1);
			}

			return dateTime;
		}

		public static bool IsWeekend(this DateTime dateTime)
		{
			var result = dateTime.DayOfWeek.EqualsAny(DayOfWeek.Saturday, DayOfWeek.Sunday);
			return result;
		}

		public static bool IsWorkDay(this DateTime dateTime)
		{
			var result = !dateTime.IsWeekend();
			return result;
		}

		public static int GetWeekOfYear(this DateTime dateTime)
		{
			var result = GetWeekOfYear(dateTime, CultureInfo.CurrentCulture);
			return result;
		}

		public static int GetWeekOfYear(this DateTime dateTime, CultureInfo cultureInfo)
		{
			var calendar = cultureInfo.Calendar;
			var dateTimeFormat = cultureInfo.DateTimeFormat;
			var result =
				calendar.GetWeekOfYear(dateTime, dateTimeFormat.CalendarWeekRule, dateTimeFormat.FirstDayOfWeek);
			return result;
		}

		#endregion

		#region Month

		public static int GetDaysOfMonth(this DateTime dateTime)
		{
			var nextMonth = dateTime.AddMonths(1);
			var result = new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
			return result;
		}

		public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
		{
			var result = new DateTime(dateTime.Year, dateTime.Month, 1);
			return result;
		}

		public static DateTime GetFirstDayOfMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
		{
			var dt = dateTime.GetFirstDayOfMonth();

			while (dt.DayOfWeek != dayOfWeek)
			{
				dt = dt.AddDays(1);
			}

			return dt;
		}

		public static DateTime GetLastDayOfMonth(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, GetDaysOfMonth(dateTime));
		}

		public static DateTime GetLastDayOfMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
		{
			var dt = dateTime.GetLastDayOfMonth();

			while (dt.DayOfWeek != dayOfWeek)
			{
				dt = dt.AddDays(-1);
			}

			return dt;
		}

		#endregion

		#region Timestamp

		public static long GetTimestamp(this DateTime datetime)
		{
			var ts = datetime.Subtract(Date1970);
			var result = (long)ts.TotalMilliseconds;
			return result;
		}

		#endregion

		#region Equal

		public static bool IsDateEqual(this DateTime dateTime, DateTime dateToCompare)
		{
			var result = dateTime.Date == dateToCompare.Date;
			return result;
		}

		public static bool IsTimeEqual(this DateTime dateTime, DateTime timeToCompare)
		{
			var result = dateTime.TimeOfDay == timeToCompare.TimeOfDay;
			return result;
		}

		public static bool IsBefore(this DateTime dateTime, DateTime other)
		{
			var result = dateTime.CompareTo(other) < 0;
			return result;
		}

		public static bool IsAfter(this DateTime dateTime, DateTime other)
		{
			var result = dateTime.CompareTo(other) > 0;
			return result;
		}

		#endregion

		#region DateTimeOffset

		public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime)
		{
			return dateTime.ToDateTimeOffset(null);
		}

		public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime, TimeZoneInfo localTimeZone)
		{
			localTimeZone = (localTimeZone ?? TimeZoneInfo.Local);

			if (dateTime.Kind != DateTimeKind.Unspecified)
			{
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
			}

			var result = TimeZoneInfo.ConvertTimeToUtc(dateTime, localTimeZone);
			return result;
		}

		#endregion

		#region Festival

		public static bool IsEaster(this DateTime dateTime)
		{
			var y = dateTime.Year;
			var a = y % 19;
			var b = y / 100;
			var c = y % 100;
			var d = b / 4;
			var e = b % 4;
			var f = (b + 8) / 25;
			var g = (b - f + 1) / 3;
			var h = (19 * a + b - d - g + 15) % 30;
			var i = c / 4;
			var k = c % 4;
			var l = (32 + 2 * e + 2 * i - h - k) % 7;
			var m = (a + 11 * h + 22 * l) / 451;
			var month = (h + l - 7 * m + 114) / 31;
			var day = ((h + l - 7 * m + 114) % 31) + 1;
			var dtEasterSunday = new DateTime(y, month, day);
			var result = dateTime == dtEasterSunday;
			return result;
		}

		#endregion

		#region Format

		// Today, 3:33 PM
		public static string ToFriendlyDateString(this DateTime dateTime)
		{
			var result = ToFriendlyDateString(dateTime, CultureInfo.CurrentCulture);
			return result;
		}

		public static string ToFriendlyDateString(this DateTime dateTime, CultureInfo cultureInfo)
		{
			var sbFormattedDate = new StringBuilder();

			if (dateTime.Date == DateTime.Today)
			{
				sbFormattedDate.Append("Today");
			}
			else if (dateTime.Date == DateTime.Today.AddDays(-1))
			{
				sbFormattedDate.Append("Yesterday");
			}
			else if (dateTime.Date > DateTime.Today.AddDays(-6))
			{
				// Weekday
				sbFormattedDate.Append(dateTime.ToString("dddd").ToString(cultureInfo));
			}
			else
			{
				sbFormattedDate.Append(dateTime.ToString("MMMM dd, yyyy").ToString(cultureInfo));
			}

			// am / pm
			sbFormattedDate.Append(" at ").Append(dateTime.ToString("t").ToLower());
			return sbFormattedDate.ToString();
		}

		#endregion

		#region Comparisons

		public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
		{
			if (beginDate.Year > 2022)
			{
				return (currentDate >= beginDate && currentDate <= endDate);
			}
			else
			{
				return false;
			}
		}

		public static bool IsLessThan(this DateTime currentDate, DateTime referenceDate)
		{
			return (currentDate <= referenceDate);
		}

		public static bool IsGreaterThan(this DateTime currentDate, DateTime referenceDate)
		{
			return (currentDate >= referenceDate);
		}

		// Example
		//     var monday = DateTime.Now.ThisWeekMonday();
		// var friday = DateTime.Now.ThisWeekFriday();
		//
		// If (DateTime.Now.IsInRange(monday, friday) {
		//     ...do something...
		// }

		#endregion

		#region StringConversions

		public static DateTime? ToDateTime(this string s)
		{
			DateTime dtr;
			var tryDtr = DateTime.TryParse(s, out dtr);
			return (tryDtr) ? dtr : new DateTime?();
		}

		//     Example
		// you can test the result like this:
		//         var dt = TextBox1.Text.ToDateTime();
		//     if (dt == null) {
		//         throw new Exception("Your datetime was invalid");
		//     }
		//     else {
		// this method takes a normal/non-nullable DateTime
		// as its parameter.
		//         DoSomething(dt.Value);
		//     }

		#endregion

		#region TimeSpans

		/// <summary>
		/// Get the elapsed time since the input DateTime
		/// </summary>
		/// <param name="input">Input DateTime</param>
		/// <returns>Returns a TimeSpan value with the elapsed time since the input DateTime</returns>
		/// <example>
		/// TimeSpan elapsed = dtStart.Elapsed();
		/// </example>
		/// <seealso cref="ElapsedSeconds()"/>
		public static TimeSpan Elapsed(this DateTime input)
		{
			return DateTime.Now.Subtract(input);
		}

		public static DateTime AddTime(this DateTime date, int hour, int minutes)
		{
			return date + new TimeSpan(hour, minutes, 0);
		}

		public static TimeSpan TimeElapsed(this DateTime date)
		{
			return DateTime.Now - date;
		}

		public static string LengthOfTimeShort(this DateTime date)
		{
			TimeSpan lengthOfTime = DateTime.Now.Subtract(date);
			if (lengthOfTime.Minutes == 0)
				return lengthOfTime.Seconds.ToString() + "s";
			else if (lengthOfTime.Hours == 0)
				return lengthOfTime.Minutes.ToString() + "m";
			else if (lengthOfTime.Days == 0)
				return lengthOfTime.Hours.ToString() + "h";
			else
				return lengthOfTime.Days.ToString() + "d";
		}

		public static string LengthOfTimeTillShort(this DateTime date)
		{
			TimeSpan lengthOfTime = date.Subtract(DateTime.Now); //DateTime.Now.Subtract(date);
			if (lengthOfTime.Minutes == 0)
				return lengthOfTime.Seconds.ToString() + "s";
			else if (lengthOfTime.Hours == 0)
				return lengthOfTime.Minutes.ToString() + "m";
			else if (lengthOfTime.Days == 0)
				return lengthOfTime.Hours.ToString() + "h";
			else
				return lengthOfTime.Days.ToString() + "d";
		}

		public static string LengthOfTimeTill(this DateTime date)
		{
			TimeSpan lengthOfTime = date.Subtract(DateTime.Now); //DateTime.Now.Subtract(date);
			string timeString = "";


			if (lengthOfTime.Days > 0)
			{
				//timeString = lengthOfTime.ToString(@"dd\.hh\:mm");
				//timeString = $"D{lengthOfTime.Days} {lengthOfTime.Hours}:{lengthOfTime.Minutes}";
				timeString = $"{lengthOfTime.Days}D {lengthOfTime.Hours}H:{lengthOfTime.Minutes}M";
			}
			else if (lengthOfTime.Days <= 0)
			{
				//timeString = lengthOfTime.ToString(@"hh\:mm\:ss");
				// timeString = $"{lengthOfTime.Hours}:{lengthOfTime.Minutes}:{lengthOfTime.Seconds}";
				timeString = $"{lengthOfTime.Hours}H {lengthOfTime.Minutes}M:{lengthOfTime.Seconds}S";
			}

			return timeString;
		}

		#region DateTime Floor, Mid, Ceiling

		public static DateTime DateTimeFloor(this DateTime dt, TimeInterval Interval)
		{
			return WorkMethod(dt, 0L, Interval);
		}

		public static DateTime DateTimeMidpoint(this DateTime dt, TimeInterval Interval)
		{
			return WorkMethod(dt, 2L, Interval);
		}

		public static DateTime DateTimeCeiling(this DateTime dt, TimeInterval Interval)
		{
			return WorkMethod(dt, 1L, Interval);
		}

		public static DateTime DateTimeCeilingUnbounded(this DateTime dt, TimeInterval Interval)
		{
			return WorkMethod(dt, 1L, Interval).AddTicks(-1);
		}

		public static DateTime DateTimeRound(this DateTime dt, TimeInterval Interval)
		{
			if (dt >= WorkMethod(dt, 2L, Interval))
				return WorkMethod(dt, 1L, Interval);
			else
				return WorkMethod(dt, 0L, Interval);
		}

		public enum TimeInterval : long
		{
			YearFromJanuary = 120L,
			YearFromFebruary = 121L,
			YearFromMarch = 122L,
			YearFromApril = 123L,
			YearFromMay = 124L,
			YearFromJune = 125L,
			YearFromJuly = 126L,
			YearFromAugust = 127L,
			YearFromSeptember = 128L,
			YearFromOctober = 129L,
			YearFromNovember = 130L,
			YearFromDecember = 131L,
			HalfYearFromJanuary = 60L,
			HalfYearFromFebruary = 61L,
			HalfYearFromMarch = 62L,
			HalfYearFromApril = 63L,
			HalfYearFromMay = 64L,
			HalfYearFromJune = 65L,
			QuarterYearFromJanuary = 30L,
			QuarterYearFromFebruary = 31L,
			QuarterYearFromMarch = 32L,
			BiMonthlyFromJanuary = 20L,
			BiMonthlyFromFebruary = 21L,
			OneMonth = 10L,
			OneWeekFromSunday = 1L,
			OneWeekFromMonday = 2L,
			OneWeekFromTuesday = 3L,
			OneWeekFromWednesday = 4L,
			OneWeekFromThursday = 5L,
			OneWeekFromFriday = 6L,
			OneWeekFromSaturday = 7L,
			OneDay = TimeSpan.TicksPerDay,
			TwelveHour = TimeSpan.TicksPerHour * 12L,
			SixHour = TimeSpan.TicksPerHour * 6L,
			ThreeHour = TimeSpan.TicksPerHour * 3L,
			OneHour = TimeSpan.TicksPerHour,
			HalfHour = TimeSpan.TicksPerMinute * 30L,
			QuarterHour = TimeSpan.TicksPerMinute * 15L,
			OneMinute = TimeSpan.TicksPerMinute,
			HalfMinute = TimeSpan.TicksPerSecond * 30L,
			QuarterMinute = TimeSpan.TicksPerSecond * 15L,
			OneSecond = TimeSpan.TicksPerSecond,
			TenthOfASecond = TimeSpan.TicksPerSecond / 10L,
			HundrethOfASecond = TimeSpan.TicksPerSecond / 100L,
			ThousandthOfASecond = TimeSpan.TicksPerSecond / 1000L
		}

		private static DateTime WorkMethod(DateTime dt, long ReturnType, TimeInterval Interval)
		{
			long Interval1 = (long)Interval;
			long TicksFromFloor = 0L;
			int IntervalFloor = 0;
			int FloorOffset = 0;
			int IntervalLength = 0;
			DateTime floorDate;
			DateTime ceilingDate;

			if (Interval1 > 132L) //Set variables to calculate date for time interval less than one day.
			{
				floorDate = new DateTime(dt.Ticks - (dt.Ticks % Interval1), dt.Kind);
				if (ReturnType != 0L)
					TicksFromFloor = Interval1 / ReturnType;
			}
			else if (Interval1 < 8L) //Set variables to calculate date for time interval of one week.
			{
				IntervalFloor = (int)(Interval1) - 1;
				FloorOffset = (int)dt.DayOfWeek * -1;
				floorDate = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, dt.Kind).AddDays(
					-(IntervalFloor > FloorOffset ? FloorOffset + 7 - IntervalFloor : FloorOffset - IntervalFloor));
				if (ReturnType != 0L)
					TicksFromFloor = TimeSpan.TicksPerDay * 7L / ReturnType;
			}
			else //Set variables to calculate date for time interval one month or greater.
			{
				IntervalLength = Interval1 >= 130L ? 12 : (int)(Interval1 / 10L);
				IntervalFloor = (int)(Interval1 % IntervalLength);
				FloorOffset = (dt.Month - 1) % IntervalLength;
				floorDate = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, dt.Kind).AddMonths(-(IntervalFloor > FloorOffset
					? FloorOffset + IntervalLength - IntervalFloor
					: FloorOffset - IntervalFloor));

				if (ReturnType != 0L)
				{
					ceilingDate = floorDate.AddMonths(IntervalLength);
					TicksFromFloor = (long)ceilingDate.Subtract(floorDate).Ticks / ReturnType;
				}
			}

			return floorDate.AddTicks(TicksFromFloor);
		}

		#endregion

		public static int CompareWithoutMinutes(this DateTime source, DateTime toCompare)
		{
			source = new DateTime(source.Year, source.Month, source.Day, source.Hour, 0, 0);
			toCompare = new DateTime(toCompare.Year, toCompare.Month, toCompare.Day, toCompare.Hour, 0, 0);

			return source.CompareTo(toCompare);
		}

		/// <summary>
		/// Gest the elapsed seconds since the input DateTime
		/// </summary>
		/// <param name="input">Input DateTime</param>
		/// <returns>Returns a Double value with the elapsed seconds since the input DateTime</returns>
		/// <example>
		/// Double elapsed = dtStart.ElapsedSeconds();
		/// </example>
		/// <seealso cref="Elapsed()"/>
		public static double ElapsedSeconds(this DateTime input)
		{
			return DateTime.Now.Subtract(input).TotalSeconds;
		}


		public static string GetRemainingTime(this DateTime date, DateTime endTime)
		{
			TimeSpan lengthOfTime = endTime.Subtract(DateTime.Now);
			string timeString = "";


			if (lengthOfTime.Days > 0)
			{
				timeString = $"{lengthOfTime.Days}D {lengthOfTime.Hours}H"; //:{lengthOfTime.Minutes}M";
			}
			else if (lengthOfTime.Days <= 0 && !(lengthOfTime.Hours <= 0))
			{
				timeString = $"{lengthOfTime.Hours}H {lengthOfTime.Minutes}M"; //:{lengthOfTime.Seconds}S";
			}
			else if (lengthOfTime.Hours <= 0)
			{
				timeString = $"{lengthOfTime.Minutes}M {lengthOfTime.Seconds}S";
			}
			else if (lengthOfTime.Seconds <= 0 && lengthOfTime.Minutes <= 0 && lengthOfTime.Hours <= 0)
			{
				timeString = "{00}M {00}S";
			}

			return timeString;
		}

		#endregion

		#region EPOCH

		public static int ToEpoch(this DateTime dateTime)
		{
			TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1);
			int secondsSinceEpoch = (int)timeSpan.TotalSeconds;
			//Console.WriteLine(secondsSinceEpoch);
			return secondsSinceEpoch;
		}

		public static DateTime FromUnixTime(long unixTime)
		{
			return epoch.AddSeconds(unixTime);
		}

		#endregion
	}
}