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
        let rec getAvailability existingBookings =
            match existingBookings with
            | head::tail when isLastElement head existingBookings ->
                [Busy({StartDate= head.StartDate; EndDate= head.EndDate})] :: getAvailability tail
            | head::tail ->
                [Busy({StartDate= head.StartDate; EndDate= head.EndDate});
                Available({StartDate= head.EndDate.AddMinutes(+ float space.SetupMinutes); EndDate= tail.Head.StartDate.AddMinutes(- float space.TeardownMinutes)})]
                :: getAvailability tail
            | [] -> []

        let busySlots =
            match excludeBookingIds with
            | Some ids ->
                space.Bookings
                |> Seq.filter (fun booking -> not (Seq.contains (booking.Id) ids))
                |> Seq.sort
                |> Seq.map(fun booking -> { StartDate= booking.ReservedSlot.StartDate; EndDate= booking.ReservedSlot.EndDate})
                |> List.ofSeq
            | None -> []

        
        (getAvailability busySlots) |> List.collect(fun x -> x) |> Seq.ofList



