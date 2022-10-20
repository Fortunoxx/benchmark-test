namespace benchmark_test;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using BenchmarkDotNet.Attributes;

public static class EnumExtensions
{
    public static string TargetName(this Enum value)
        => value.GetType().
            GetRuntimeField(value.ToString())?.
            GetCustomAttributes(typeof(EnumMemberAttribute), false).
            SingleOrDefault() is not EnumMemberAttribute attribute 
                ? value.ToString() 
                : attribute.Value;
}

public enum EntityMetaDataName
{
    [EnumMember(Value = "Invalid")]
    Invalid,
    [EnumMember(Value = "TheStatus")]
    Status,
    [EnumMember(Value = "TheText")]
    Text,
}

[MemoryDiagnoser]
public class EnumBenchmark
{
    [Benchmark]
    public IList<string> GetNameToString()
        => Enum.GetValues(typeof(EntityMetaDataName)).Cast<EntityMetaDataName>().
            Select(x => x.ToString()).ToList();

    [Benchmark]
    public IList<string> GetNameStringInterpolation()
        => Enum.GetValues(typeof(EntityMetaDataName)).Cast<EntityMetaDataName>().
            Select(x => $"{x}").ToList();

    [Benchmark]
    public IList<string> GetNameStringExtension()
        => Enum.GetValues(typeof(EntityMetaDataName)).Cast<EntityMetaDataName>().
            Select(x => x.TargetName()).ToList();
}