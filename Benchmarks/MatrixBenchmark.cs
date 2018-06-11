using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using LinalLib;

namespace Benchmarks
{
    [ClrJob(true)]
    [RPlotExporter, RankColumn]
    public class MatrixReverseBenchmark
    {
        private Matrix _m;

        [Params(5, 10, 20, 30, 40, 50)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            Random r = new Random(123);
            _m = new Matrix(N, N, r);
        }

        [Benchmark]
        public void Reverse()
        {
            _m.Reverse(out var _);
        }

        public static Summary Run()
        {
            return BenchmarkRunner.Run<MatrixReverseBenchmark>();
        }
    }

    [ClrJob(true)]
    [RPlotExporter, RankColumn]
    public class MatrixGaussEliminationBenchmark
    {
        private Matrix _m;
        private Matrix _b;

        [Params(5, 10, 20, 30, 40, 50)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            Random r = new Random(123);
            _m = new Matrix(N, N, r);
            _b = new Matrix(N, N, r);
        }

        [Benchmark]
        public void GaussElimination()
        {
            //_m.GaussElimination(_b, out var _);
        }

        public static Summary Run()
        {
            return BenchmarkRunner.Run<MatrixGaussEliminationBenchmark>();
        }
    }

    [ClrJob(true)]
    [RPlotExporter, RankColumn]
    public class MatrixEpauFactorizationBenchmark
    {
        private Matrix _m;

        [Params(5, 10, 20, 30, 40, 50)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            Random r = new Random(123);
            _m = new Matrix(N, N, r);
        }

        public static Summary Run()
        {
            return BenchmarkRunner.Run<MatrixEpauFactorizationBenchmark>();
        }
    }
}
