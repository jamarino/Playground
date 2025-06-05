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
    public int Correct()
    {
        int result = 0;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                for (int k = 0; k < Size; k++)
                {
                    result = matrix[i, j, k]; 
                }
            }
        }

        return result;
    }

    [Benchmark]
    public int Incorrect()
    {
        int result = 0;

        for (int k = 0; k < Size; k++)
        {
            for (int j = 0; j < Size; j++)
            {
                for (int i = 0; i < Size; i++)
                {
                    result = matrix[i, j, k]; 
                }
            }
        }

        return result;
    }
}
