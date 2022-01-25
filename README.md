## Summary

### Problem

The challenge was to write a program that generates a list of distinct random numbers from 1 to 10,000 inclusive each time it is run.

### Projects
| **Name**                 | **Description**                                           |
|--------------------------|-----------------------------------------------------------|
| RandomNumbers            | The main executable containing the random list functions. |
| RandomNumbers.Benchmarks | Contains benchmarks for two different approaches.         |
| RandomNumbers.Tests      | Contains unit tests.                                      |

### Methodology

The first approach was to solve the problem in a purely functional manner. The simplest way I could think of was to remove random values from the original list and add them to a new list until there were no values left.

One problem with the first approach is that since lists are immutable we would have to allocate at least 20,000 objects when adding and removing items from each list. I created a second version of the function that uses the regular mutable .NET List object for comparison.

To compare the two, I made a separate project using BenchmarkDotNet that runs each approach with different input list sizes.

Out of curiosity, I found a third very clever approach on StackOverflow and added that as a point of comparison.

### Benchmarks

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000<br>
Intel Core i9-10885H CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores<br>
.NET SDK=6.0.101<br>
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT DEBUG<br>
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT<br>


| Method                      | ListSize |           Mean |         Error |         StdDev |      Gen 0 |      Gen 1 |  Allocated |
|-----------------------------|----------|---------------:|--------------:|---------------:|-----------:|-----------:|-----------:|
| GenerateLength              | 100      |      35.226 us |     0.4072 us |      0.3809 us |    10.2234 |     0.2441 |      84 KB |
| GenerateLengthMutable       | 100      |       6.955 us |     0.0388 us |      0.0363 us |     1.0605 |     0.0229 |       9 KB |
| GenerateLengthStackOverflow | 100      |       6.579 us |     0.0190 us |      0.0158 us |     0.9766 |     0.0229 |       8 KB |
| GenerateLength              | 1000     |   3,225.210 us |    19.5544 us |     18.2912 us |   960.9375 |   164.0625 |   7,862 KB |
| GenerateLengthMutable       | 1000     |      74.721 us |     0.2990 us |      0.2796 us |     9.6436 |     1.5869 |      79 KB |
| GenerateLengthStackOverflow | 1000     |      76.178 us |     0.1879 us |      0.1758 us |     9.5215 |     1.5869 |      78 KB |
| GenerateLength              | 10000    | 360,630.723 us | 7,193.5479 us | 13,511.2266 us | 94000.0000 | 26000.0000 | 771,963 KB |
| GenerateLengthMutable       | 10000    |   1,802.088 us |    12.4509 us |      9.7208 us |   107.4219 |    48.8281 |     882 KB |
| GenerateLengthStackOverflow | 10000    |     888.326 us |     1.7238 us |      1.5281 us |    94.7266 |    44.9219 |     781 KB |

### Conclusion
As the benchmarks show, the mutable approach is both significantly faster, uses far less memory, and causes significantly less Gen 0 and Gen 1 garbage collections.

In a case where performance and system resources were critical, I would choose the mutable approach even though it breaks from the purely functional style. If these were not a factor, I would stick with the purely functional approach.