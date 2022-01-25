/// Generates random lists
module RandomList

open System
open System.Collections.Generic

let private rand = Random()

/// Gets a random index from the given list.
let private randomIndex lst = rand.Next(Seq.length lst);

/// Randomizes the given list.
let randomize values =
    let rec randomize' original randomized =
        match original with
        | [] -> randomized
        | _ ->
            let i = randomIndex original
            let value = original.[i]
            randomize' (original |> List.removeAt i) (value :: randomized)
    randomize' values [ ]

/// Randomizes the given list.
let randomizeMutable (values : 'a list) : 'a list =
    let rec randomize' (original : List<'a>) (randomized : List<'a>) =
        match original.Count with
        | 0 -> randomized
        | _ ->
            let i = randomIndex original
            let value = original.[i]
            original.RemoveAt(i)
            randomized.Add(value)
            randomize' original randomized
    randomize' (new List<'a>(values)) (new List<'a>()) |> List.ofSeq


/// Generates a list of random values from 1 to a given value.
let generateLength n = randomize [ 1 .. n ]

/// Generates a list of random values from 1 to a given value.
let generateLengthMutable n = randomizeMutable [ 1 .. n ]

/// Generates a list of random values from 1 to 10,000.
let generate() = randomize [ 1 .. 10_000 ]

/// Generates a list of random values from 1 to 10,000.
let generateMutable() = randomizeMutable [ 1 .. 10_000 ]
