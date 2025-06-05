using BenchmarkDotNet.Attributes;

namespace Benchy;

[MemoryDiagnoser]
public class MemoryLocalityBenchmarks
{
    public MemoryLocalityBenchmarks()
    {
        matrix = new int[Size, Size, Size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                for (int k = 0; k < Size; k++)
                {
                    matrix[i, j, k] = i + j + k;
                }
            }
        }
    }

    const int Size = 500;
    private readonly int[,,] matrix;

    [Benchmark(Baseline = true)]
    public void Correct()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                for (int k = 0; k < Size; k++)
                {
                    _ = matrix[i, j, k];
                }
            }
        }
    }

    [Benchmark]
    public void HalfCorrect()
    {
        for (int j = 0; j < Size; j++)
        {
            for (int k = 0; k < Size; k++)
            {
                for (int i = 0; i < Size; i++)
                {
                    _ = matrix[i, j, k];
                }
            }
        }
    }

    [Benchmark]
    public void Incorrect()
    {
        for (int k = 0; k < Size; k++)
        {
            for (int j = 0; j < Size; j++)
            {
                for (int i = 0; i < Size; i++)
                {
                    _ = matrix[i, j, k];
                }
            }
        }
    }
}
