using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

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
