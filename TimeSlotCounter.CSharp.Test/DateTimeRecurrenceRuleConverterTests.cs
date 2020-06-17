using System;
using System.Linq;
using Microsoft.FSharp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimeSlotCounter.CSharp.Test
{
    [TestClass]
    public class DateTimeRecurrenceRuleConverterTests
    {
		#region Daily

		[TestMethod]
		public void Convert_DailyPatternWithOutOccurences_EndDateConsidered()
        {
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Daily,
                1,
				new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 04, 14, 0, 0)),
                null,
                null,
                null);

			var expected = new[]
			{
				new DateTime(2018,10,01),
				new DateTime(2018,10,02),
				new DateTime(2018,10,03),
				new DateTime(2018,10,04)
			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		[TestMethod]
		public void Convert_DailyPatternWithOutOccurencesWithInterval_EndDateConsidered()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Daily,
                2,
                new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 04, 14, 0, 0)),
                null,
                null,
                null);

			var expected = new[]
			{
				new DateTime(2018,10,01),
				new DateTime(2018,10,03),
			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		[TestMethod]
		public void Convert_DailyPatternWithOccurences_EndDateIgnored()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Daily,
                2,
                new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 04, 14, 0, 0)),
                5,
                null,
                null);

			var expected = new[]
			{
				new DateTime(2018,10,01),
					new DateTime(2018,10,02),
					new DateTime(2018,10,03),
					new DateTime(2018,10,04),
					new DateTime(2018,10,05),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		[TestMethod]
		public void Convert_DailyPatternWithOccurencesWithInterval_EndDateIgnored()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Daily,
                2,
                new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 04, 14, 0, 0)),
                2,
                null,
                null);

			var expected = new[]
			{
				new DateTime(2018,10,01),
					new DateTime(2018,10,03)
			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		#endregion

		#region EveryWeekDay
		[TestMethod]
		public void Convert_EveryWeekDayPatternWithOutOccurences_EndDateConsidered()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.EveryWeekDay,
                null,
                new DateTimeOffset(new DateTime(2018, 10, 05, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 08, 14, 0, 0)),
                2,
                null,
                null);

			var expected = new[]
			{
				new DateTime(2018,10,05),
				new DateTime(2018,10,08),
			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		[TestMethod]
		public void Convert_EveryWeekDayPatternWithOccurences_EndDateIgnored()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.EveryWeekDay,
                null,
                new DateTimeOffset(new DateTime(2018, 10, 05, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 07, 14, 0, 0)),
                5,
                null,
                null);

			var expected = new[]
			{
				new DateTime(2018,10,05),
					new DateTime(2018,10,08),
					new DateTime(2018,10,09),
					new DateTime(2018,10,10),
					new DateTime(2018,10,11)
			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		} /**/
		#endregion

		#region Weekly

		[TestMethod]
		public void Convert_WeeklyPatternWithOutOccurences_EndDateConsidered()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Weekly,
                1,
                new DateTimeOffset(new DateTime(2018, 10, 02, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 12, 14, 0, 0)),
                null,
                new DateTimeRecurrenceRuleConverterTypes.WeeklyRecurrencePattern(new[]
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Saturday
                }), 
                null);

			var expected = new[]
			{
				new DateTime(2018,10,03),
					new DateTime(2018,10,05),
					new DateTime(2018,10,06),
					new DateTime(2018,10,08),
					new DateTime(2018,10,10),
					new DateTime(2018,10,12),
			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}

		[TestMethod]
		public void Convert_WeeklyPatternWithOutOccurencesWithInterval_EndDateConsidered()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Weekly,
                3,
                new DateTimeOffset(new DateTime(2018, 10, 02, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 25, 14, 0, 0)),
                null,
                new DateTimeRecurrenceRuleConverterTypes.WeeklyRecurrencePattern(new[]
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Saturday
                }),
                null);

			var expected = new[]
			{
                new DateTime(2018,10,03), 
                new DateTime(2018,10,05),
                new DateTime(2018,10,06),
                new DateTime(2018,10,22),
                new DateTime(2018,10,24),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}

		[TestMethod]
		public void Convert_WeeklyPatternWithOccurences_EndDateIgnored()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Weekly,
                1,
                new DateTimeOffset(new DateTime(2018, 10, 03, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 13, 14, 0, 0)),
                5,
                new DateTimeRecurrenceRuleConverterTypes.WeeklyRecurrencePattern(new[]
                {
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday
                }),
                null);
			var expected = new[]
			{
			   new DateTime(2018,10,03),
				   new DateTime(2018,10,05),
					new DateTime(2018,10,10),
					new DateTime(2018,10,12),
					new DateTime(2018,10,17),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);

		}

		[TestMethod]
		public void Convert_WeeklyPatternWithOccurencesWithInterval_EndDateIgnored()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Weekly,
                3,
                new DateTimeOffset(new DateTime(2018, 10, 03, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 10, 13, 14, 0, 0)),
                5,
                new DateTimeRecurrenceRuleConverterTypes.WeeklyRecurrencePattern(new[]
                {
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday
                }),
                null);

			var expected = new[]
			{
					new DateTime(2018,10,03),
					new DateTime(2018,10,05),
					new DateTime(2018,10,24),
					new DateTime(2018,10,26),
					new DateTime(2018,11,14),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);

		}

		#endregion

		#region Monthly

		[TestMethod]
		public void Convert_MonthlyPatternDayBasedWithOutOccurences_EndDateConsidered()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
                1,
                new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 12, 31, 14, 0, 0)),
                null,
                null,
                new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern(1, null, null));
			var expected = new[]
			{
					new DateTime(2018,10,01),
					new DateTime(2018,11,01),
					new DateTime(2018,12,01),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		[TestMethod]
		public void Convert_MonthlyPatternDayBasedWithOutOccurencesWithInterval_EndDateConsidered()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
                2,
                new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 12, 31, 14, 0, 0)),
                null,
                null,
                new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern(10, null, null));
			var expected = new[]
			{
				new DateTime(2018,10,10),
				new DateTime(2018,12,10),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		[TestMethod]
		public void Convert_MonthlyPatternDayBasedWithOutOccurences31DayRequested_TakeLastDayOfEveryMonth()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
                2,
                new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 12, 31, 14, 0, 0)),
                null,
                null,
                new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern(31, null, null));

			var expected = new[]
			{
					new DateTime(2018,10,31),
					new DateTime(2018,11,30),
					new DateTime(2018,12,31),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		//[TestMethod]
		//public void Convert_MonthlyPatternDayBasedWithOutOccurences30DayRequested_TakeLastDayOfFebruary()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2019, 01, 01, 13, 0, 0),
		//		EndDate = new DateTime(2019, 03, 31, 14, 0, 0),
		//		Interval = 1,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			Day = 30
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2019,01,30),
		//			new DateTime(2019,02,28),
		//			new DateTime(2019,03,30),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}

		//[TestMethod]
		//public void Convert_MonthlyPatternDayBasedWithOutOccurences30DayRequestedWithNoMatchInDateRange_ReturnEmptyList()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2019, 01, 31, 13, 0, 0),
		//		EndDate = new DateTime(2019, 02, 04, 14, 0, 0),
		//		Interval = 1,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			Day = 30
		//		}
		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	Assert.AreEqual(0, result.Length);
		//}

		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOutOccurencesFirstMondayRequested_EndDateConsidered()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 1,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.First,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Monday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2018,10,01),
		//			new DateTime(2018,11,05),
		//			new DateTime(2018,12,03),
		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		[TestMethod]
		public void Convert_MonthlyPatternWeekBasedWithOutOccurencesWithIntervalSecondWednesdayRequested_EndDateConsidered()
		{
            var rule = DateTimeRecurrenceRuleConverterTypes.RecurrenceRule.Create(
                DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
                2,
                new DateTimeOffset(new DateTime(2018, 10, 01, 13, 0, 0)),
                new DateTimeOffset(new DateTime(2018, 12, 31, 14, 0, 0)),
                null,
                null,
                new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern(
                    null,
                    new FSharpOption<DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes>(DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Second),
                    new FSharpOption<DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes>(DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Wednesday)));
			var expected = new[]
			{
					new DateTime(2018,10,10),
					new DateTime(2018,12,12),

			};

			var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

			CollectionAssert.AreEquivalent(expected, result);
		}
		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOutOccurencesLastWeekdayRequested_EndDateConsidered()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 1,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Last,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Weekday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2018,10,31),
		//			new DateTime(2018,11,30),
		//			new DateTime(2018,12,31),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOutOccurencesWithIntervalFourthDayRequested_EndDateConsidered()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 2,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Fourth,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Day
		//		}
		//	};
		//	var expected = new[]
		//	{
		//		new DateTime(2018,10,04),
		//		new DateTime(2018,12,04),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOutOccurencesWithIntervalSecondWeekdayRequested_EndDateConsidered()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 2,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Second,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Weekday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//		new DateTime(2018,10,02),
		//		new DateTime(2018,12,04),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOutOccurencesThirdWeekendDayRequested_EndDateConsidered()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 1,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Third,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.WeekendDay
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2018,10,13),
		//			new DateTime(2018,11,10),
		//			new DateTime(2018,12,08),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOccurencesFirstMondayRequested_EndDateIgnored()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 1,
		//		Occurrences = 5,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.First,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Monday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2018,10,01),
		//			new DateTime(2018,11,05),
		//			new DateTime(2018,12,03),
		//			new DateTime(2019,01,07),
		//			new DateTime(2019,02,04),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}

		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOccurencesWithIntervalSecondWednesdayRequested_EndDateIgnored()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 2,
		//		Occurrences = 4,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Second,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Wednesday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2018,10,10),
		//			new DateTime(2018,12,12),
		//			new DateTime(2019,02,13),
		//			new DateTime(2019,04,10),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOccurencesLastWeekdayRequested_EndDateIgnored()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 1,
		//		Occurrences = 2,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Last,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Weekday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2018,10,31),
		//			new DateTime(2018,11,30),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}

		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOccurencesWithIntervalFourthDayRequested_EndDateIgnored()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 2,
		//		Occurrences = 3,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Fourth,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Day
		//		}
		//	};
		//	var expected = new[]
		//	{
		//			new DateTime(2018,10,04),
		//			new DateTime(2018,12,04),
		//			new DateTime(2019,02,04),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}

		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithSingleOccurencesWithIntervalSecondWeekdayRequested_EndDateIgnored()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 2,
		//		Occurrences = 1,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Second,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Weekday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//		new DateTime(2018,10,02)
		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}

		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithOccurencesThirdWeekendDayRequested_EndDateIgnored()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 01, 13, 0, 0),
		//		EndDate = new DateTime(2018, 12, 31, 14, 0, 0),
		//		Interval = 1,
		//		Occurrences = 6,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Third,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.WeekendDay
		//		}
		//	};
		//	var expected = new[]
		//	{
		//		new DateTime(2018,10,13),
		//		new DateTime(2018,11,10),
		//		new DateTime(2018,12,08),
		//		new DateTime(2019,01,12),
		//		new DateTime(2019,02,09),
		//		new DateTime(2019,03,09),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}

		//[TestMethod]
		//public void Convert_MonthlyPatternWeekBasedWithSkippedSiutableDayThisMonthWithInterval_GapIntervalAfterStartDay()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Monthly,
		//		StartDate = new DateTime(2018, 10, 27, 13, 0, 0),
		//		EndDate = new DateTime(2018, 10, 31, 14, 0, 0),
		//		Interval = 2,
		//		Occurrences = 2,
		//		MonthlyRecurrencePattern = new DateTimeRecurrenceRuleConverterTypes.MonthlyRecurrencePattern()
		//		{
		//			WeekOfMonth = DateTimeRecurrenceRuleConverterTypes.WeekOfMonthTypes.Third,
		//			DayOfWeek = DateTimeRecurrenceRuleConverterTypes.DayOfWeekTypes.Friday
		//		}
		//	};
		//	var expected = new[]
		//	{
		//		new DateTime(2018,12,21),
		//		new DateTime(2019,02,15)

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		//#endregion



		//#region Cross midnight 

		//[TestMethod]
		//public void Convert_DailyPatternMidnight_StartDateConsideredOnly()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Daily,
		//		StartDate = new DateTime(2018, 10, 01, 20, 0, 0),
		//		EndDate = new DateTime(2018, 10, 04, 7, 0, 0),
		//		Interval = 1
		//	};
		//	var expected = new[]
		//	{
		//		new DateTime(2018,10,1),
		//		new DateTime(2018,10,2),
		//		new DateTime(2018,10,3),
		//		new DateTime(2018,10,4),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}

		//[TestMethod]
		//public void Convert_DailyPatternMidnightWithOccurences_StartDateConsideredOnly()
		//{
		//	var rule = new DateTimeRecurrenceRuleConverterTypes.RecurrenceRule()
		//	{
		//		RecurrencePatternType = DateTimeRecurrenceRuleConverterTypes.RecurrencePatternTypes.Daily,
		//		StartDate = new DateTime(2018, 10, 01, 20, 0, 0),
		//		EndDate = new DateTime(2018, 10, 02, 7, 0, 0),
		//		Occurrences = 4,
		//		Interval = 1
		//	};
		//	var expected = new[]
		//	{
		//		new DateTime(2018,10,1),
		//		new DateTime(2018,10,2),
		//		new DateTime(2018,10,3),
		//		new DateTime(2018,10,4),

		//	};

		//	var result = DateTimeRecurrenceRuleConverter.Convert(rule).ToArray();

		//	CollectionAssert.AreEquivalent(expected, result);
		//}
		#endregion
	}
}
