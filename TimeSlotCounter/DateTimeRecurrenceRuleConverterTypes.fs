namespace TimeSlotCounter

open System

[<AutoOpen>]
module DateTimeRecurrenceRuleConverterTypes =
    type RecurrencePatternTypes =
    | Daily = 0
    | EveryWeekDay = 1
    | Weekly = 2
    | Monthly = 3
    
    type WeekOfMonthTypes =
    | First | Second | Third | Fourth | Last

    type DayOfWeekTypes =
    | Day | Weekday | WeekendDay | Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday

    type WeeklyRecurrencePattern = {
        DayOfWeek: seq<DayOfWeek>
    }

    type MonthlyRecurrencePattern = {
        Day: int option
        WeekOfMonth: WeekOfMonthTypes option
        DayOfWeek: DayOfWeekTypes option
    }

    type RecurrenceRule = {
        RecurrencePatternType: RecurrencePatternTypes
        Interval: int option
        StartDate: DateTimeOffset option
        EndDate: DateTimeOffset option
        Occurrences: int option
        WeeklyRecurrencePattern: WeeklyRecurrencePattern
        MonthlyRecurrencePattern: MonthlyRecurrencePattern
    } with
        static member Create recurrencePatternType interval startDate endDate occurrences weeklyRecurrencePattern monthlyRecurrencePattern = {
            RecurrencePatternType = recurrencePatternType;
            Interval = interval;
            StartDate = startDate;
            EndDate = endDate;
            Occurrences = occurrences;
            WeeklyRecurrencePattern = weeklyRecurrencePattern;
            MonthlyRecurrencePattern = monthlyRecurrencePattern
        }
