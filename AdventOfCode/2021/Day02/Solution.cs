using AdventOfCode.Lib;

namespace AdventOfCode._2021.Day02;

[ProblemName("Dive!")]
public class Solution : ISolver
{
    public object PartOne(string input) =>
        ParseInput(input)
            .Aggregate(
                new Position(),
                (position, command) => command.Action switch
                {
                    "forward" => position with
                    {
                        X = position.X + command.Units
                    },
                    "up" => position with
                    {
                        Y = position.Y - command.Units
                    },
                    "down" => position with
                    {
                        Y = position.Y + command.Units
                    },
                    _ => throw new Exception()
                },
                position => position.X * position.Y);

    public object PartTwo(string input) =>
        ParseInput(input)
            .Aggregate(
                new Position(),
                (position, command) => command.Action switch
                {
                    "forward" => position with
                    {
                        X = position.X + command.Units,
                        Y = position.Y + position.Aim * command.Units
                    },
                    "up" => position with
                    {
                        Aim = position.Aim - command.Units
                    },
                    "down" => position with
                    {
                        Aim = position.Aim + command.Units
                    },
                    _ => throw new Exception()
                },
                position => position.X * position.Y);

    private static List<Command> ParseInput(string input) =>
        input.Split('\n')
            .Select(ParseCommand)
            .ToList();

    private static Command ParseCommand(string line)
    {
        var match = line.Split(' ');

        return new Command(match[0], int.Parse(match[1]));
    }
}

internal record Position(int X = 0, int Y = 0, int Aim = 0);

internal record Command(string Action, int Units);
