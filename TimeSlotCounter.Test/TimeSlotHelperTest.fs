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

    [<TestMethod>]
    member _.CalculateTimeSlot() =
        let testSpace = {
            Id = 1;
            Bookings = [
                {
                    Id = 1;
                    EventSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 10; Minutes = 00; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 11; Minutes = 15; Seconds = 00 } }
                    ReservedSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 9; Minutes = 50; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 11; Minutes = 30; Seconds = 00 } } 
                }
                {
                    Id = 2;
                    EventSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 14; Minutes = 00; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 15; Minutes = 15; Seconds = 00 } }
                    ReservedSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 13; Minutes = 50; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 15; Minutes = 30; Seconds = 00 } } 
                }
                {
                    Id = 3;
                    EventSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 16; Minutes = 00; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 17; Minutes = 15; Seconds = 00 } }
                    ReservedSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 15; Minutes = 50; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 17; Minutes = 30; Seconds = 00 } } 
                }
                {
                    Id = 4;
                    EventSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 2; Hours = 11; Minutes = 00; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 2; Hours = 12; Minutes = 15; Seconds = 00 } }
                    ReservedSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 2; Hours = 10; Minutes = 50; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 2; Hours = 12; Minutes = 30; Seconds = 00 } } 
                }
                {
                    Id = 5;
                    EventSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 4; Hours = 13; Minutes = 00; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 4; Hours = 14; Minutes = 15; Seconds = 00 } }
                    ReservedSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 4; Hours = 12; Minutes = 50; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 4; Hours = 14; Minutes = 30; Seconds = 00 } } 
                }
                {
                    Id = 6;
                    EventSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 6; Hours = 16; Minutes = 00; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 6; Hours = 16; Minutes = 30; Seconds = 00 } }
                    ReservedSlot = { StartDate = createDate { Year = 2020; Month = 6; Date = 6; Hours = 15; Minutes = 50; Seconds = 00 };
                                    EndDate = createDate { Year = 2020; Month = 6; Date = 6; Hours = 16; Minutes = 45; Seconds = 00 } } 
                }
            ];
            SetupMinutes = 10<Minute>;
            TeardownMinutes = 15<Minute> }
        
        let excludeBukingsId = Some(Set.ofList<int> [2; 3; 5])
        let actual = TimeSlotHelper.calculateTimeSlots excludeBukingsId testSpace |> List.ofSeq

        let expected = [
            Available { StartDate = DateTimeOffset.MinValue;
                            EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 9; Minutes = 50; Seconds = 00 } }
            Busy { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 9; Minutes = 50; Seconds = 00 };
                    EndDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 11; Minutes = 30; Seconds = 00 }}
            Available { StartDate = createDate { Year = 2020; Month = 6; Date = 1; Hours = 11; Minutes = 30; Seconds = 00 };
                    EndDate = createDate { Year = 2020; Month = 6; Date = 2; Hours = 10; Minutes = 50; Seconds = 00 } }
            Busy { StartDate =  createDate { Year = 2020; Month = 6; Date = 2; Hours = 10; Minutes = 50; Seconds = 00 };
                    EndDate = createDate { Year = 2020; Month = 6; Date = 2; Hours = 12; Minutes = 30; Seconds = 00 } }
            Available { StartDate = createDate { Year = 2020; Month = 6; Date = 2; Hours = 12; Minutes = 30; Seconds = 00 };
                    EndDate = createDate { Year = 2020; Month = 6; Date = 6; Hours = 15; Minutes = 50; Seconds = 00 } }
            Busy { StartDate =  createDate { Year = 2020; Month = 6; Date = 6; Hours = 15; Minutes = 50; Seconds = 00 };
                    EndDate = createDate { Year = 2020; Month = 6; Date = 6; Hours = 16; Minutes = 45; Seconds = 00 } }
            Available { StartDate = createDate { Year = 2020; Month = 6; Date = 6; Hours = 16; Minutes = 45; Seconds = 00 };
                    EndDate = DateTimeOffset.MaxValue }
        ]
       
        Assert.AreEqual (expected, actual)
