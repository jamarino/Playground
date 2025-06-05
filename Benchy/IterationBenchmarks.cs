using BenchmarkDotNet.Attributes;

namespace Benchy;

[MemoryDiagnoser]
public class IterationBenchmarks
{
    const int Count = 500;

    [Benchmark]
    public int DictionaryOfObjects()
    {
        int result = 0;

        var dict = new Dictionary<Tuple<int, int>, int>();

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
                var key = new Tuple<int, int>(i, j);
                dict[key] = i + j;
            }
        }

        foreach (var key in dict.Keys)
        {
            result = dict[key];
        }

        return result;
    }

    [Benchmark]
    public int DictionaryOfStructs()
    {
        int result = 0;

        var dict = new Dictionary<ValueTuple<int, int>, int>();

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
                var key = new ValueTuple<int, int>(i, j);
                dict[key] = i + j;
            }
        }

        foreach (var key in dict.Keys)
        {
            result = dict[key];
        }

        return result;
    }

    [Benchmark]
    public int ListOfObjects()
    {
        int result = 0;

        var list = new List<Tuple<int, int, int>>(Count * Count);

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
                list.Add(new(i, j, i + j));
            }
        }

        foreach (var (_, _, val) in list)
        {
            result = val;
        }

        return result;
    }

    [Benchmark(Baseline = true)]
    public int ListOfStructs()
    {
        int result = 0;

        var list = new List<ValueTuple<int, int, int>>(Count * Count);

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
                list.Add(new(i, j, i + j));
            }
        }

        foreach (var (_, _, val) in list)
        {
            result = val;
        }

        return result;
    }
}
