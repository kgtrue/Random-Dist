using BenchmarkDotNet.Attributes;
using Random_Distro.Runners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro
{
    [SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 1, invocationCount: 1)]
    [MemoryDiagnoser]
    [RankColumn]
    public class Benchmarks
    {
        private IRandomDistroService randomDistroService;
        private IWithParallelForEach withParallelForEach;
        private IWithWhile withWhile;
        private IWithParallelForEachMulti withParallelForEachMulti;
        private int randomMatches;


        [Params(1_000_000_00)]
        public int N;

        //[Benchmark]
        public string WithParallelForEach()
        {
            var result = withParallelForEach.GetMaxRandomRepeatingMatches(randomMatches);
            Console.WriteLine($"withParallelForEach: {result}");
            return result;
        }

        [Benchmark]
        public string WithWhile()
        {
            var result = withWhile.GetMaxRandomRepeatingMatches(randomMatches);
            Console.WriteLine($"WithWhile: {result}");
            return result;
        }

        [Benchmark]
        public string WithParallelForEachMulti()
        {
            var result = withParallelForEachMulti.Run(randomMatches, 14);
            Console.WriteLine($"WithWhile: {result}");
            return result;
        }

        [Benchmark]
        public string WithLinq()
        {
            var result = withParallelForEachMulti.Run2(randomMatches, 14);
            Console.WriteLine($"WithWhile: {result}");
            return result;
        }


        [GlobalSetup]
        public void Setup()
        {
            randomDistroService = new RandomDistroService();
            withParallelForEach = new WithParallelForEach(randomDistroService);
            withWhile = new WithWhile(randomDistroService);
            withParallelForEachMulti = new WithParallelForEachMulti();
            randomMatches = N;
        }

    }
}
