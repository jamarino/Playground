using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchy;

[MemoryDiagnoser]
public class AllocationBenchmarks
{
    const int Count = 1000;

    [Benchmark(OperationsPerInvoke = Count)]
    public int UseObject()
    {
        var result = 0;

        for (int i = 0; i < Count; i++)
        {
            var values = new Tuple<int, int>(1, 2);
            result = values.Item1 + values.Item2;
        }

        return result;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public int UseStruct()
    {
        var result = 0;

        for (int i = 0; i < Count; i++)
        {
            var values = new ValueTuple<int, int>(1, 2);
            result = values.Item1 + values.Item2;
        }

        return result;
    }

    [Benchmark(OperationsPerInvoke = Count, Baseline = true)]
    public int UseLocal()
    {
        var result = 0;

        for (int i = 0; i < Count; i++)
        {
            int a = 1;
            int b = 2;
            result = a + b;
        }

        return result;
    }
}
