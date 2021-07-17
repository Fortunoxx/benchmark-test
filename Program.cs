namespace benchmark_test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;

    [MemoryDiagnoser]
    public class StructVsClass
    {
        private string _initValue;

        public StructVsClass() => _initValue = $"{System.Guid.NewGuid()}{System.Guid.NewGuid()}{System.Guid.NewGuid()}{System.Guid.NewGuid()}{System.Guid.NewGuid()}";

        struct TestStruct
        {
            internal string Value { get; init; }
        }

        public class TestClass
        {
            internal string Value { get; init; }
        }

        [Benchmark]
        public List<string> GetListStruct()
            => Enumerable.Range(0, 10)
            .Select(x => new TestStruct { Value = System.Guid.NewGuid().ToString() })
            .Select(x => x.Value)
            .ToList();

        [Benchmark]
        public List<string> GetListClass()
            => Enumerable.Range(0, 10)
            .Select(x => new TestClass { Value = System.Guid.NewGuid().ToString() })
            .Select(x => x.Value)
            .ToList();
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var summary = BenchmarkRunner.Run<YABenchmark>();
        }
    }
}
