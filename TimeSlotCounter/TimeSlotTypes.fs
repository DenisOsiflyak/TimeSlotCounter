namespace TimeSlotCounter

open System

[<Struct>]
type TimeSlot = {
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
}