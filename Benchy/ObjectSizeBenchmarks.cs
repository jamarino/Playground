using BenchmarkDotNet.Attributes;

namespace Benchy;

[MemoryDiagnoser]
public class ObjectSizeBenchmarks
{
    const int Count = 500;
    private MySlimObject[] slimObjects;
    private MyThiccObject[] thiccObjects;

    public ObjectSizeBenchmarks()
    {
        slimObjects = new MySlimObject[Count];
        thiccObjects = new MyThiccObject[Count];

        for (int i = 0; i < Count; i++)
        {
            slimObjects[i] = new MySlimObject() { A = 1 };
            thiccObjects[i] = new MyThiccObject() { A = 1, U = 2 };
        }
    }

    [Benchmark(Baseline = true)]
    public long Slim()
    {
        long result = 0;

        for (int i = 0; i < Count; i++)
        {
            result = slimObjects[i].A;
        }

        return result;
    }

    [Benchmark()]
    public long ThiccA()
    {
        long result = 0;

        for (int i = 0; i < Count; i++)
        {
            result = thiccObjects[i].A;
        }

        return result;
    }

    [Benchmark()]
    public long ThiccB()
    {
        long result = 0;

        for (int i = 0; i < Count; i++)
        {
            result = thiccObjects[i].U;
        }

        return result;
    }

    public class MySlimObject
    {
        public long A;
        public long B;
        public long C;
    }

    public class MyThiccObject
    {
        public long A;
        public long B;
        public long C;
        public long D;
        public long E;
        public long F;
        public long G;
        public long H;
        public long I;
        public long J;
        public long K;
        public long L;
        public long M;
        public long N;
        public long O;
        public long P;
        public long Q;
        public long R;
        public long S;
        public long T;
        public long U;
    }
}
