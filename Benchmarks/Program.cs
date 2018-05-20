namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            MatrixReverseBenchmark.Run();
            MatrixGaussEliminationBenchmark.Run();
            MatrixEpauFactorizationBenchmark.Run();
        }
    }
}
