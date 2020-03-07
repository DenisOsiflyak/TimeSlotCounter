namespace TimeSlotCounter.Test

module Validation = 
    let partOfDateInt (num: int) (notLessThenNum: int) (notMoreThenNum: int): Option<int> =
        match num with
            | num when num < notLessThenNum && num > notMoreThenNum -> None
            | num -> Some num
    
module YearInt =
    let create (x: int) =
        match Validation.partOfDateInt x 2000 2100 with
            | Some x -> x
            | None -> raise (new System.ArgumentException("Invalid year value"))

module MonthInt =
    let create (x: int) =
        match Validation.partOfDateInt x 0 12 with
            | Some x -> x
            | None -> raise (new System.ArgumentException("Invalid month value"))

module DateInt =
    let create (x: int) =
        match Validation.partOfDateInt x 0 31 with
            | Some x -> x
            | None -> raise (new System.ArgumentException("Invalid date value"))

module HoursInt =
    let create (x: int) =
        match Validation.partOfDateInt x 0 24 with
            | Some x -> x
            | None -> raise (new System.ArgumentException("Invalid value value"))

module MinuteSecondsInt =
    let create (x: int) =
        match Validation.partOfDateInt x 0 60 with
            | Some x -> x
            | None -> raise (new System.ArgumentException("Invalid minute seconds value"))

[<Struct>]
type DateTimePart = {
    Year: int
    Month: int
    Date: int
    Hours: int
    Minutes: int
    Seconds: int
}
