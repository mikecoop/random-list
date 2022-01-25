
open BenchmarkDotNet.Running
open Benchmarks

[<EntryPoint>]
let Main args =
    BenchmarkRunner.Run<RandomListComparison>() |> ignore
    0
