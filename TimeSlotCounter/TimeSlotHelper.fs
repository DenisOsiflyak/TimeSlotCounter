namespace TimeSlotCounter

open System

module TimeSlotHelper =
    let isSlotBetweenRange range slot =
        slot.EndDate > range.StartDate && slot.StartDate < range.EndDate
    let isSlotLessThanStartDateAndEndDate range slot =
        slot.StartDate < range.StartDate && slot.EndDate < range.EndDate
    let isSlotMoreThanStartDateAndEndDate range slot =
        slot.StartDate > range.StartDate && slot.EndDate > range.EndDate
    let isSlotsAreEqual firstSlot secondSlot =
        firstSlot.StartDate = secondSlot.StartDate && firstSlot.EndDate = secondSlot.EndDate

    let filter (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list)
        : TimeSlot list =
            let range = { StartDate = startDate; EndDate = endDate }
            slots
                |> List.filter (fun slot -> isSlotBetweenRange range slot)

    let scretch (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list)
        : TimeSlot list =
            let range = { StartDate = startDate; EndDate = endDate }
            slots
                |> List.filter (fun slot -> isSlotBetweenRange range slot)
                |> List.map(fun slot -> match slot with
                                            | slot when not(isSlotBetweenRange range slot)
                                                    -> { StartDate = slot.StartDate; EndDate = slot.EndDate }
                                            | slot when isSlotLessThanStartDateAndEndDate range slot
                                                    -> { StartDate = startDate; EndDate = slot.EndDate }
                                            | slot when isSlotMoreThanStartDateAndEndDate range slot
                                                    -> { StartDate = slot.StartDate; EndDate = endDate }
                                            | _ -> slot)

    let (+) (addSlot: TimeSlot) (slots: TimeSlot list)
        : TimeSlot list =
            match addSlot with
                | addSlot when addSlot.StartDate < addSlot.EndDate -> 
                    slots
                        |> List.filter (fun slot -> not(isSlotsAreEqual addSlot slot))
                | _ -> raise (new ArgumentException("Slot start date can't be more than end date"))
            