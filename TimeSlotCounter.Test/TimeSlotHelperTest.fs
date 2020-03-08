namespace TimeSlotCounter.Test

open System
open TimeSlotCounter
open TimeSlotCounter.Test
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass () =
    let createDate (dateTimePart: DateTimePart) =
        DateTimeOffset(
            new DateTime(
                YearInt.create dateTimePart.Year,
                MonthInt.create dateTimePart.Month,
                DateInt.create dateTimePart.Date,
                HoursInt.create dateTimePart.Hours,
                MinuteSecondsInt.create dateTimePart.Minutes,
                MinuteSecondsInt.create dateTimePart.Seconds))
    
    let startDate = { Year = 2019; Month = 1; Date = 1; Hours = 00; Minutes = 00; Seconds = 00 } // 2019.01.01 00:00:00
    let endDate = { startDate with Date = 15 } // 2019.01.15 00:00:00
    let changeMonthByDatePart (datePart: DateTimePart) (month: int)
        = createDate { datePart with Month = MonthInt.create month }

    //create test time slots
    let testTimeSlots = [
        for i in 1..12 do 
            yield {
                StartDate = changeMonthByDatePart startDate i;
                EndDate = changeMonthByDatePart endDate i }
        ]

    let startTimeSlotDate = createDate { startDate with Month = 6; Date = 10 }
    let endTimeSlotDate = createDate  { startDate with Month = 9; Date = 10 }

    [<TestMethod>]
    member _.FilterReturnSlots() =
        let expected = [
            { StartDate = createDate { startDate with Month = 6; Date = 1 };
                EndDate = createDate { startDate with Month = 6; Date = 15 } }
            { StartDate = createDate { startDate with Month = 7; Date = 1 };
                EndDate = createDate { startDate with Month = 7; Date = 15 } }
            { StartDate = createDate { startDate with Month = 8; Date = 1 };
                EndDate = createDate { startDate with Month = 8; Date = 15 } }
            { StartDate = createDate { startDate with Month = 9; Date = 1 };
                EndDate = createDate { startDate with Month = 9; Date = 15 } }]
        let actual = TimeSlotHelper.filter startTimeSlotDate endTimeSlotDate testTimeSlots
        Assert.AreEqual (expected, actual)

    [<TestMethod>]
    member _.FilterScretchTimeSlots() =
        let expexted = [
            { StartDate = createDate { startDate with Month = 6; Date = 10 };
                EndDate = createDate { startDate with Month = 6; Date = 15 } }
            { StartDate = createDate { startDate with Month = 7; Date = 1 };
                EndDate = createDate { startDate with Month = 7; Date = 15 } }
            { StartDate = createDate { startDate with Month = 8; Date = 1 };
                EndDate = createDate { startDate with Month = 8; Date = 15 } }
            { StartDate = createDate { startDate with Month = 9; Date = 1 };
                EndDate = createDate { startDate with Month = 9; Date = 10 } }]
        let actual = TimeSlotHelper.scretch startTimeSlotDate endTimeSlotDate testTimeSlots
        Assert.AreEqual (expexted, actual)

    [<TestMethod>]
    member _.AddTimeSlot() =
        let expexted = [
            { StartDate = createDate { startDate with Month = 1; Date = 1 };
                EndDate = createDate { startDate with Month = 1; Date = 15 } }
            { StartDate = createDate { startDate with Month = 2; Date = 1 };
                EndDate = createDate { startDate with Month = 2; Date = 15 } }
            { StartDate = createDate { startDate with Month = 3; Date = 1 };  //here
                EndDate = createDate { startDate with Month = 4; Date = 5 } } //here
            { StartDate = createDate { startDate with Month = 4; Date = 5 };  //here
                EndDate = createDate { startDate with Month = 4; Date = 15 } }//here
            { StartDate = createDate { startDate with Month = 5; Date = 1 };
                EndDate = createDate { startDate with Month = 5; Date = 15 } }
            { StartDate = createDate { startDate with Month = 6; Date = 1 };
                EndDate = createDate { startDate with Month = 6; Date = 15 } }
            { StartDate = createDate { startDate with Month = 7; Date = 1 };
                EndDate = createDate { startDate with Month = 7; Date = 15 } }
            { StartDate = createDate { startDate with Month = 8; Date = 1 };
                EndDate = createDate { startDate with Month = 8; Date = 15 } }
            { StartDate = createDate { startDate with Month = 9; Date = 1 };
                EndDate = createDate { startDate with Month = 9; Date = 15 } }
            { StartDate = createDate { startDate with Month = 10; Date = 1 };
                EndDate = createDate { startDate with Month = 10; Date = 15 } }
            { StartDate = createDate { startDate with Month = 11; Date = 1 };
                EndDate = createDate { startDate with Month = 11; Date = 15 } }
            { StartDate = createDate { startDate with Month = 12; Date = 1 };
                EndDate = createDate { startDate with Month = 12; Date = 15 } }]
        let timeSlotToAdd = {
            StartDate = createDate { startDate with Month = 3; Date = 1 }; //2019.03.01
            EndDate = createDate { startDate with Month = 4; Date = 5 } } //2019.03.20
        let actual = TimeSlotHelper.(+) timeSlotToAdd testTimeSlots
        Assert.AreEqual (expexted, actual)