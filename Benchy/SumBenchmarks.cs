using BenchmarkDotNet.Attributes;
using NoAlloq;
using System.Numerics;

namespace Benchy;

[MemoryDiagnoser]
public class SumBenchmarks
{
    [Params(100, 10_000, 1_000_000)]
    public int Count = 100;

    public int[] Numbers;

    [GlobalSetup]
    public void Setup()
    {
        Numbers = Enumerable
            .Range(1, Count)
            .Select(i => i % 10)
            .ToArray();
    }

    [Benchmark(Baseline = true)]
    public void Loop()
    {
        var sum = 0;

        for (int i = 0; i < Count; i++)
        {
            sum += Numbers[i];
        }
    }

    [Benchmark]
    public void Simd()
    {
        var vectorSize = Vector<int>.Count;
        int i = 0;
        var acc = new Vector<int>(0);

        // Sum in chunks of vector size
        for (; i <= Numbers.Length - vectorSize; i += vectorSize)
        {
            var v = new Vector<int>(Numbers, i);
            acc += v;
        }

        // Sum the elements of the accumulator vector
        int sum = 0;
        for (int j = 0; j < vectorSize; j++)
        {
            sum += acc[j];
        }

        // Sum any remaining elements
        for (; i < Numbers.Length; i++)
        {
            sum += Numbers[i];
        }
    }

    [Benchmark]
    public void LinqSum()
    {
        _ = Numbers.Sum();
    }

    [Benchmark]
    public void ChunckedParallelSum()
    {
        const int chunkSize = 10 * 1024;
        var sum = 0;

        Parallel.For(0, Numbers.Length / chunkSize + 1, i =>
        {
            Span<int> span = Numbers.AsSpan(i * chunkSize, (i+1) * chunkSize > Numbers.Length ? Numbers.Length % chunkSize : chunkSize);
            var localSum = span.Sum();
            Interlocked.Add(ref sum, localSum);
        });
    }
}
