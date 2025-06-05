using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchy;

[MemoryDiagnoser]
public class AllocateManyBenchmarks
{
    const int Count = 100_000;
    const int Iterations = 100;

    [Benchmark(OperationsPerInvoke = Iterations)]
    public int AllocateObjects()
    {
        int result = 0;

        for (int n = 0; n < Iterations; n++)
        {
            var values = new List<Tuple<int, int>>(Count);
            for (int i = 0; i < Count; i++)
            {
                values.Add(new Tuple<int, int>(i, i + 1));
            }

            for (int i = 0; i < values.Count; i++)
            {
                Tuple<int, int>? value = values[i];
                result = value.Item1 + value.Item2;
            } 
        }

        return result;
    }

    [Benchmark(OperationsPerInvoke = Iterations)]
    public int AllocateStructs()
    {
        int result = 0;

        for (int n = 0; n < Iterations; n++)
        {
            var values = new List<ValueTuple<int, int>>(Count);
            for (int i = 0; i < Count; i++)
            {
                values.Add(new ValueTuple<int, int>(i, i + 1));
            }

            for (int i = 0; i < values.Count; i++)
            {
                (int, int) value = values[i];
                result = value.Item1 + value.Item2;
            } 
        }

        return result;
    }
}
