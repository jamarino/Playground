using BenchmarkDotNet.Attributes;

namespace Benchy;

[MemoryDiagnoser]
public class MultiDimentionalArrayBenchmarks
{
    public MultiDimentionalArrayBenchmarks()
    {
        matrix = new int[Size, Size, Size];
        jaggedArray = new int[Size][][];

        for (int i = 0; i < Size; i++)
        {
            jaggedArray[i] = new int[Size][];

            for (int j = 0; j < Size; j++)
            {
                jaggedArray[i][j] = new int[Size];

                for (int k = 0; k < Size; k++)
                {
                    jaggedArray[i][j][k] = i + j + k;
                    matrix[i, j, k] = i + j + k;
                }
            }
        }
    }

    const int Size = 500;
    private readonly int[,,] matrix;
    private readonly int[][][] jaggedArray;

    [Benchmark(Baseline = true)]
    public void Matrix()
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
    public void Jagged()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                for (int k = 0; k < Size; k++)
                {
                    _ = jaggedArray[i][j][k];
                }
            }
        }
    }

    [Benchmark]
    public void JaggedLocal()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                var arr = jaggedArray[i][j];

                for (int k = 0; k < Size; k++)
                {
                    _ = arr[k];
                }
            }
        }
    }

    [Benchmark]
    public void JaggedMoreLocal()
    {
        for (int i = 0; i < Size; i++)
        {
            var dim1 = jaggedArray[i];

            for (int j = 0; j < Size; j++)
            {
                var dim2 = dim1[j];

                for (int k = 0; k < Size; k++)
                {
                    _ = dim2[k];
                }
            }
        }
    }
}
