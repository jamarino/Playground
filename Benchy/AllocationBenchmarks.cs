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
    public int AllocateObject()
    {
        var result = 0;

        for (int i = 0; i < Count; i++)
        {
            var obj = new MyAdder(1, 2);
            result = obj.Add();
        }

        return result;
    }

    class MyAdder(int a, int b)
    {
        public int Add() => a + b;
    }

    [Benchmark(OperationsPerInvoke = Count)]
    public int UseStatic()
    {
        var result = 0;

        for (int i = 0; i < Count; i++)
        {
            result = MyStaticAdder.Add(1, 2);
        }

        return result;
    }

    static class MyStaticAdder
    {
        public static int Add(int a, int b) => a + b;
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
