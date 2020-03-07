namespace TimeSlotCounter

open System

module TimeSlotHelper =
    let isSlotBetweenDates firstSlot secondSlot =
        secondSlot.EndDate >= firstSlot.StartDate && secondSlot.StartDate <= firstSlot.EndDate
    let isSlotLessThanStartDateAndLessThanEndDate firstSlot secondSlot =
        secondSlot.StartDate < firstSlot.StartDate && secondSlot.EndDate <= firstSlot.EndDate
    let isSlotMoreThanStartDateAndMoreThanEndDate firstSlot secondSlot =
        secondSlot.StartDate >= firstSlot.StartDate && secondSlot.EndDate > firstSlot.EndDate
    let isSlotsAreEqual firstSlot secondSlot =
        firstSlot.StartDate = secondSlot.StartDate && firstSlot.EndDate = secondSlot.EndDate

    let filter (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list)
        : TimeSlot list =
            let period = { StartDate = startDate; EndDate = endDate }
            slots
                |> List.filter (fun slot -> isSlotBetweenDates period slot)

    let scretch (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list)
        : TimeSlot list =
            let period = { StartDate = startDate; EndDate = endDate }
            slots
                |> List.filter (fun slot -> isSlotBetweenDates period slot)
                |> List.map(fun slot -> match slot with
                                            | slot when not(isSlotBetweenDates period slot)
                                                    -> { StartDate = slot.StartDate; EndDate = slot.EndDate }
                                            | slot when isSlotLessThanStartDateAndLessThanEndDate period slot
                                                    -> { StartDate = startDate; EndDate = slot.EndDate }
                                            | slot when isSlotMoreThanStartDateAndMoreThanEndDate period slot
                                                    -> { StartDate = slot.StartDate; EndDate = endDate }
                                            | _ -> slot)

    let (+) (addSlot: TimeSlot) (slots: TimeSlot list)
        : TimeSlot list =
            match addSlot with
                | addSlot when addSlot.StartDate < addSlot.EndDate -> 
                    slots
                        |> List.filter (fun slot -> not(isSlotsAreEqual addSlot slot))
                | _ -> raise (new ArgumentException("Slot start date can't be more than end date"))
            