## Summary

### Problem

The challenge was to write a program that generates a list of distinct random numbers from 1 to 10,000 inclusive each time it is run.

### Projects
| **Name**                 | **Description**                                   |
|--------------------------|---------------------------------------------------|
| RandomNumbers            | The main executable.                              |
| RandomNumbers.Benchmarks | Contains benchmarks for two different approaches. |
| RandomNumbers.Tests      | Contains unit tests.                              |

### Methodology

The first approach was to solve the problem in a purely functional manner. The simplest way I could think of was to remove random values from the original list and add them to a new list until there were no values left.

One problem with the first approach is that since lists are immutable we would have to allocate at least 20,000 objects when adding and removing items from each list. I created a second version of the function that uses the regular mutable .NET List object for comparison.

To compare the two, I made a separate project using BenchmarkDotNet that runs each approach with different input list sizes.

### Benchmarks

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000<br>
Intel Core i9-10885H CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores<br>
.NET SDK=6.0.101<br>
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT DEBUG<br>
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT<br>


| Method                | ListSize |           Mean |         Error |         StdDev |      Gen 0 |      Gen 1 |  Allocated |
|-----------------------|----------|---------------:|--------------:|---------------:|-----------:|-----------:|-----------:|
| GenerateLength        | 100      |      36.127 us |     0.6608 us |      0.6181 us |    10.2234 |     0.2441 |      84 KB |
| GenerateLengthMutable | 100      |       7.026 us |     0.0267 us |      0.0250 us |     1.0605 |     0.0229 |       9 KB |
| GenerateLength        | 1000     |   3,254.094 us |    18.9854 us |     16.8301 us |   960.9375 |   164.0625 |   7,868 KB |
| GenerateLengthMutable | 1000     |      76.108 us |     0.3363 us |      0.3146 us |     9.6436 |     1.5869 |      79 KB |
| GenerateLength        | 10000    | 357,758.071 us | 7,027.1578 us | 13,870.9240 us | 96000.0000 | 28000.0000 | 786,783 KB |
| GenerateLengthMutable | 10000    |   1,801.670 us |     7.1058 us |      6.6468 us |   107.4219 |    48.8281 |     882 KB |

### Conclusion
As the benchmarks show, the mutable approach is both significantly faster, uses far less memory, and causes significantly less Gen 0 and Gen 1 garbage collections.

In a case where performance and system resources were critical, I would choose the mutable approach even though it breaks from the purely functional style. If these were not a factor, I would stick with the purely functional approach.