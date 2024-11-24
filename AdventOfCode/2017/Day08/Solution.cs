using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day08;

[ProblemName("I Heard You Like Registers")]
public partial class Solution : ISolver
{
    public object PartOne(string input)
    {
        var operations = ParseInput(input);
        var registries = new Dictionary<string, int>();

        foreach (var operation in operations)
        {
            ExecuteOperation(operation, registries);
        }

        return registries.Values.Max();
    }

    public object PartTwo(string input)
    {
        var operations = ParseInput(input);
        var registries = new Dictionary<string, int>();

        var maxSeen = int.MinValue;

        foreach (var operation in operations)
        {
            ExecuteOperation(operation, registries);
            maxSeen = Math.Max(maxSeen, registries.GetValueOrDefault(operation.Registry));
        }

        return maxSeen;
    }

    private static void ExecuteOperation(Operation operation, Dictionary<string, int> registries)
    {
        var compareValue = registries.GetValueOrDefault(operation.CompareRegistry, 0);

        if (Compare(compareValue, operation.CompareCommand, operation.CompareParam))
        {
            var value = registries.GetValueOrDefault(operation.Registry, 0);
            registries[operation.Registry] = operation.Command switch
            {
                OperationCommand.Inc => value + operation.Param,
                OperationCommand.Dec => value - operation.Param,
                _ => throw new NotSupportedException()
            };
        }
    }

    private static bool Compare(int val1, CompareCommand command, int val2) =>
        command switch
        {
            CompareCommand.Eq => val1 == val2,
            CompareCommand.Ne => val1 != val2,
            CompareCommand.Lt => val1 < val2,
            CompareCommand.Lte => val1 <= val2,
            CompareCommand.Gt => val1 > val2,
            CompareCommand.Gte => val1 >= val2,
            _ => throw new NotSupportedException()
        };

    private static List<Operation> ParseInput(string input)
    {
        var lines = input.Split('\n');
        var result = new List<Operation>();

        var opRegex = OperationRegex();

        foreach (var line in lines)
        {
            var match = opRegex.Match(line);

            var registry = match.Groups[1].Value;
            var command = Enum.Parse<OperationCommand>(match.Groups[2].Value, true);
            var param = int.Parse(match.Groups[3].Value);
            var compareRegistry = match.Groups[4].Value;
            var compareCommand = match.Groups[5].Value switch
            {
                "==" => CompareCommand.Eq,
                "!=" => CompareCommand.Ne,
                "<" => CompareCommand.Lt,
                ">" => CompareCommand.Gt,
                ">=" => CompareCommand.Gte,
                "<=" => CompareCommand.Lte,
                _ => throw new NotSupportedException()
            };
            var compareParam = int.Parse(match.Groups[6].Value);

            var operation = new Operation(registry, command, param, compareRegistry, compareCommand, compareParam);
            result.Add(operation);
        }

        return result;
    }

    [GeneratedRegex(@"(\w+) (\w+) (.+) if (\w+) (.+) (.+)")]
    public static partial Regex OperationRegex();
}

internal enum OperationCommand
{
    Inc,
    Dec
};

internal enum CompareCommand
{
    Eq,
    Ne,
    Lt,
    Lte,
    Gt,
    Gte
}

internal record struct Operation(
    string Registry,
    OperationCommand Command,
    int Param,
    string CompareRegistry,
    CompareCommand CompareCommand,
    int CompareParam);
