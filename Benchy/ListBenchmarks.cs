using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchy;

[MemoryDiagnoser]
public class ListBenchmarks
{
    const int Count = 1000;

    [Benchmark(Baseline = true)]
    public void FillArray()
    {
        var array = new int[Count];
        for (int i = 0; i < Count; i++)
        {
            array[i] = i;
        }
    }

    [Benchmark]
    public void FillList()
    {
        var list = new List<int>();
        for (int i = 0; i < Count; i++)
        {
            list.Add(i);
        }
    }
    
    [Benchmark]
    public void FillListWithCapacity()
    {
        var list = new List<int>(Count);
        for (int i = 0; i < Count; i++)
        {
            list.Add(i);
        }
    }
}
