namespace TimeSlotCounter

open System

module TimeSlotHelper =
    let isSlotIntoRange range slot =
        slot.EndDate > range.StartDate && slot.StartDate < range.EndDate
    let isSlotLessThanStartDateAndEndDate range slot =
        slot.StartDate < range.StartDate && slot.EndDate < range.EndDate
    let isSlotMoreThanStartDateAndEndDate range slot =
        slot.StartDate > range.StartDate && slot.EndDate > range.EndDate
    let greatest firstDate secondDate = 
        if firstDate > secondDate then firstDate else secondDate

    let filter (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list)
        : TimeSlot list =
            let range = { StartDate = startDate; EndDate = endDate }
            slots
                |> List.filter (fun slot -> isSlotIntoRange range slot)

    let scretch (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list)
        : TimeSlot list =
            let range = { StartDate = startDate; EndDate = endDate }
            slots
                |> List.filter (fun slot -> isSlotIntoRange range slot)
                |> List.map(fun slot -> match slot with
                                            | slot when not(isSlotIntoRange range slot)
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
                          |> List.map (fun slot -> match slot with 
                                                          | slot when isSlotIntoRange addSlot slot 
                                                                  ->
                                                                    let startDate =
                                                                        if addSlot.EndDate > slot.StartDate && addSlot.EndDate < slot.EndDate
                                                                            then addSlot.EndDate
                                                                        else slot.StartDate
                                                                    let endDate = greatest slot.EndDate addSlot.EndDate
                                                                    { StartDate = startDate; EndDate = endDate }
                                                          | _ -> slot)
                | _ -> raise (new ArgumentException("Slot start date can't be more than end date"))
            