using System.ComponentModel;

using AdventOfCode.Lib;

namespace AdventOfCode._2022.Day02;

[ProblemName("Rock Paper Scissors")]
public class Solution : ISolver {
    private enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    private enum Outcome
    {
        Win = 6,
        Draw = 3,
        Loss = 0
    }

    public object? PartOne(string input) {
        var lines = input.Split('\n');

        var sum = 0;

        foreach (var line in lines)
        {
            var opponent = GetSelection(line[0]);
            var player = GetSelection(line[2]);

            sum += (int)player;
            sum += (int)CalculateOutcome(opponent, player);
        }

        return sum;
    }

    public object? PartTwo(string input) {
        var lines = input.Split('\n');
        var sum = 0;

        foreach (var line in lines)
        {
            var opponent = GetSelection(line[0]);
            var outcome = GetOutcome(line[2]);

            sum += (int)outcome;
            sum += (int)CalculateSelection(opponent, outcome);
        }

        return sum;
    }

    private static Choice GetSelection(char input)
    {
        return input switch
        {
            'A' or 'X' => Choice.Rock,
            'B' or 'Y' => Choice.Paper,
            'C' or 'Z' => Choice.Scissors,
            _ => throw new InvalidEnumArgumentException()
        };
    }

    private static Outcome GetOutcome(char input)
    {
        return input switch
        {
            'X' => Outcome.Loss,
            'Y' => Outcome.Draw,
            'Z' => Outcome.Win,
            _ => throw new InvalidEnumArgumentException()
        };
    }

    private static Outcome CalculateOutcome(Choice opponent, Choice player)
    {
        return (opponent, player) switch
        {
            (Choice.Rock, Choice.Paper) => Outcome.Win,
            (Choice.Rock, Choice.Scissors) => Outcome.Loss,
            (Choice.Rock, Choice.Rock) => Outcome.Draw,

            (Choice.Paper, Choice.Paper) => Outcome.Draw,
            (Choice.Paper, Choice.Scissors) => Outcome.Win,
            (Choice.Paper, Choice.Rock) => Outcome.Loss,

            (Choice.Scissors, Choice.Paper) => Outcome.Loss,
            (Choice.Scissors, Choice.Scissors) => Outcome.Draw,
            (Choice.Scissors, Choice.Rock) => Outcome.Win,

            _ => throw new InvalidOperationException()
        };
    }

    private static Choice CalculateSelection(Choice opponent, Outcome outcome)
    {
        return (opponent, outcome) switch
        {
            (Choice.Rock, Outcome.Draw) => Choice.Rock,
            (Choice.Rock, Outcome.Win) => Choice.Paper,
            (Choice.Rock, Outcome.Loss) => Choice.Scissors,

            (Choice.Paper, Outcome.Draw) => Choice.Paper,
            (Choice.Paper, Outcome.Win) => Choice.Scissors,
            (Choice.Paper, Outcome.Loss) => Choice.Rock,

            (Choice.Scissors, Outcome.Draw) => Choice.Scissors,
            (Choice.Scissors, Outcome.Win) => Choice.Rock,
            (Choice.Scissors, Outcome.Loss) => Choice.Paper,

            _ => throw new InvalidOperationException()
        };
    }
}
