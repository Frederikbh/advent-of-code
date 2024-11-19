using System.Collections.Immutable;

using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day12;

[ProblemName("Hot Springs")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var records = ParseInput(input);

        var result = records
            .Sum(e => Solve(e.Condition, ImmutableStack.CreateRange(e.Sequences), new Dictionary<string, long>()));
        return result;
    }

    public object PartTwo(string input)
    {
        var records = ParseInput(input, true);

        var result = records
            .Sum(e => Solve(e.Condition, ImmutableStack.CreateRange(e.Sequences), new Dictionary<string, long>()));
        return result;
    }

    private long Solve(string pattern, ImmutableStack<int> sequences, Dictionary<string, long> cache)
    {
        var key = $"{pattern},{string.Join(",", sequences)}";

        if(!cache.ContainsKey(key))
            cache[key] = SolveInternal(pattern, sequences, cache);

        return cache[key];
    }

    private long SolveInternal(string pattern, ImmutableStack<int> sequences, Dictionary<string, long> cache)
    {
        return pattern.FirstOrDefault() switch
        {
            '.' => ProcessDot(pattern, sequences, cache),
            '#' => ProcessHash(pattern, sequences, cache),
            '?' => ProcessQuestion(pattern, sequences, cache),
            _ => ProcessEnd(sequences)
        };
    }

    private long ProcessDot(string pattern, ImmutableStack<int> sequences, Dictionary<string, long> cache)
    {
        return Solve(pattern[1..], sequences, cache);
    }

    private long ProcessHash(string pattern, ImmutableStack<int> sequences, Dictionary<string, long> cache)
    {
        if(sequences.IsEmpty)
            return 0;

        sequences = sequences.Pop(out var n);

        var potentiallyDead = pattern.TakeWhile(e => e is '#' or '?').Count();

        if (potentiallyDead < n)
            return 0;
        if (pattern.Length == n)
            return Solve("", sequences, cache);
        if (pattern[n] == '#')
            return 0;

        return Solve(pattern[(n + 1)..], sequences, cache);
    }

    private long ProcessQuestion(string pattern, ImmutableStack<int> sequences, Dictionary<string, long> cache)
    {
        return Solve("." + pattern[1..], sequences, cache) + Solve("#" + pattern[1..], sequences, cache);
    }

    private long ProcessEnd(ImmutableStack<int> sequences)
    {
        return sequences.Any() ? 0 : 1;
    }

    private static List<ConditionRecord> ParseInput(string input, bool unfold = false)
    {
        var lines = input.Split('\n');

        var records = new List<ConditionRecord>();
        foreach (var line in lines)
        {
            var split = line.Split(" ");


            if (unfold)
            {
                split[0] = string.Join('?', Enumerable.Repeat(split[0], 5));
                split[1] = string.Join(',', Enumerable.Repeat(split[1], 5));
            }

            var condition = split[0];
            var sequences = split[1].Split(",").Select(int.Parse).ToList();
            sequences.Reverse();
            records.Add(new ConditionRecord(condition, sequences));
        }

        return records;
    }

    private record ConditionRecord(string Condition, List<int> Sequences);
}
