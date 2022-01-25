module RandomListTests

open System
open Xunit
open FsUnit
open RandomList

let values = [ 1 .. 100 ]

[<Fact>]
let ``randomize returns correct count`` () =
    randomize values |> List.length |> should equal 100

[<Fact>]
let ``randomize returns distinct values`` () =
    randomize values |> Set.ofList |> Set.count |> should equal 100

[<Fact>]
let ``randomize returns randomized values`` () =
    randomize values |> should not' (equal [ 1 .. 100 ])

[<Fact>]
let ``randomizeMutable returns correct count`` () =
    randomizeMutable values |> Seq.length |> should equal 100

[<Fact>]
let ``randomizeMutable returns distinct values`` () =
    randomizeMutable values |> Set.ofSeq |> Set.count |> should equal 100

[<Fact>]
let ``randomizeMutable returns randomized values`` () =
    randomizeMutable values |> List.ofSeq |> should not' (equal [ 1 .. 100 ])
