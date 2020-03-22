namespace TimeSlotCounter

open System

[<Measure>]
type Minute

[<Struct>]
type TimeSlot = {
    StartDate: DateTimeOffset
    EndDate: DateTimeOffset
}

type TimeSlotAvailability = 
    | Available of TimeSlot
    | Busy of TimeSlot

[<Struct>]
type Booking = {
    Id: int
    EventSlot: TimeSlot
    ReservedSlot: TimeSlot
}

[<Struct>]
type Space = {
    Id: int
    Bookings: Booking list
    SetupMinutes: int<Minute>
    TeardownMinutes: int<Minute>
}