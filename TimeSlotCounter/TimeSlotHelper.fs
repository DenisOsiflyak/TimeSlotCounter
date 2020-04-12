namespace TimeSlotCounter

open System

module TimeSlotHelper =
    let isSlotIntoRange range slot = slot.EndDate > range.StartDate && slot.StartDate < range.EndDate
    let isSlotLessThanStartDateAndEndDate range slot = slot.StartDate < range.StartDate && slot.EndDate < range.EndDate
    let isSlotMoreThanStartDateAndEndDate range slot = slot.StartDate > range.StartDate && slot.EndDate > range.EndDate
    let isSlotEndDateMoreThanStartDateAndLessThanEndDate range slot =
        range.EndDate > slot.StartDate && range.EndDate < slot.EndDate

    let greatest firstDate secondDate =
        if firstDate > secondDate then firstDate else secondDate

    let isFirstElement (element: 'a) (arr: list<'a>): bool = element = arr.Head
    let isLastElement (element: 'a) (arr: list<'a>): bool = element = arr.[arr.Length - 1]

    let filter (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list): TimeSlot list =
        let range =
            { StartDate = startDate
              EndDate = endDate }
        slots |> List.filter (fun slot -> isSlotIntoRange range slot)

    let scretch (startDate: DateTimeOffset) (endDate: DateTimeOffset) (slots: TimeSlot list): TimeSlot list =
        let range =
            { StartDate = startDate
              EndDate = endDate }
        slots
        |> List.filter (fun slot -> isSlotIntoRange range slot)
        |> List.map (fun slot ->
            match slot with
            | slot when isSlotLessThanStartDateAndEndDate range slot ->
                { StartDate = startDate
                  EndDate = slot.EndDate }
            | slot when isSlotMoreThanStartDateAndEndDate range slot ->
                { StartDate = slot.StartDate
                  EndDate = endDate }
            | _ -> slot)

    let (+) (addSlot: TimeSlot) (slots: TimeSlot list): TimeSlot list =
        match addSlot with
        | addSlot when addSlot.StartDate < addSlot.EndDate ->
            slots
            |> List.map (fun slot ->
                match isSlotIntoRange addSlot slot with
                | true ->
                    let startDate =
                        match isSlotEndDateMoreThanStartDateAndLessThanEndDate addSlot slot with
                        | true -> addSlot.EndDate
                        | _ -> slot.StartDate

                    let endDate = greatest slot.EndDate addSlot.EndDate
                    { StartDate = startDate
                      EndDate = endDate }
                | _ -> slot)
        | _ -> raise (new ArgumentException("Slot start date can't be more than end date"))

    let calculateTimeSlots (excludeBookingIds: Set<int> option) (space: Space): seq<TimeSlotAvailability> =
        let bookings =
            match excludeBookingIds with
            | Some ids -> space.Bookings |> List.filter (fun booking -> not (Seq.contains (booking.Id) ids))
            | None -> []
        let toAvailiable slot =
            Available({StartDate= fst slot; EndDate= snd slot})
        let toBusy slot =
            Busy({StartDate= fst slot; EndDate= snd slot})

        let rec getAvailiability slots converter =
            match slots with
            | head::tail -> converter head :: (getAvailiability tail converter)
            | [] -> []

        let booked =
            bookings
            |> List.map(fun slot -> (slot.EventSlot.StartDate.AddMinutes(- float space.SetupMinutes), slot.EventSlot.EndDate.AddMinutes(float space.TeardownMinutes)))
        
        let vacant =
            booked
            |> List.map(fun slot -> [fst slot; snd slot])
            |> List.collect(fun slot -> slot)
            |> List.append [DateTimeOffset.MinValue; DateTimeOffset.MaxValue]
            |> List.sort
            |> List.pairwise
            |> List.except booked
            |> List.map(fun time ->
                match time with
                | time when (fst time) = DateTimeOffset.MinValue -> (fst time, (snd time).AddMinutes(- float space.TeardownMinutes))
                | time when (snd time) = DateTimeOffset.MaxValue -> ((fst time).AddMinutes(float space.SetupMinutes), (snd time))
                | _ -> ((fst time).AddMinutes(float space.SetupMinutes), (snd time).AddMinutes(- float space.TeardownMinutes)))

        (getAvailiability booked toBusy)
        |> List.append (getAvailiability vacant toAvailiable)
        |> Seq.ofList