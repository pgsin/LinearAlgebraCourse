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
    //[ClrJob(true), CoreJob, MonoJob]
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
        public void ReverseE()
        {
            _m.ReverseE(out var _);
        }

        [Benchmark]
        public void ReverseL()
        {
            _m.ReverseL(out var _);
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
        public void GaussEliminationE()
        {
            _m.GaussEliminationE(_b, out var _);
        }

        [Benchmark]
        public void GaussEliminationL()
        {
            _m.GaussEliminationL(_b, out var _);
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

        [Benchmark]
        public void EpauFactorizationE()
        {
            _m.EPAU_factorizationE(out var _, out var _, out var _);
        }

        [Benchmark]
        public void EpauFactorizationL()
        {
            _m.EPAU_factorizationL(out var _, out var _, out var _);
        }

        public static Summary Run()
        {
            return BenchmarkRunner.Run<MatrixEpauFactorizationBenchmark>();
        }
    }
}
