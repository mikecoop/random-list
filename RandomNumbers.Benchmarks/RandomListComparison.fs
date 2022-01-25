module Benchmarks

open BenchmarkDotNet.Attributes
open RandomList

[<MemoryDiagnoser>]
type RandomListComparison () =

    [<Params(100,1000,10000)>]
    member val ListSize : int = 0 with get, set

    [<Benchmark>]
    member self.GenerateLength() = generateLength self.ListSize

    [<Benchmark>]
    member self.GenerateLengthMutable() = generateLengthMutable self.ListSize
