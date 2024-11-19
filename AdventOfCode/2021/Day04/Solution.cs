      using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

using AdventOfCode.Lib;        

namespace AdventOfCode._2021.Day04;

[ProblemName("Giant Squid")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var (numbers, boards) = ParseInput(input);

        foreach (var number in numbers)
        {
            foreach (var board in boards)
            {
                board.DrawNumber(number);
                if(board.HasCompleteColumn() || board.HasCompleteRow())
                {
                    return board.CalculateScore(number);
                }
            }
        }

        return 0;
    }

    public object PartTwo(string input) 
    {
        var (numbers, boards) = ParseInput(input);

        foreach (var number in numbers)
        {
            foreach (var board in boards)
            {
                board.DrawNumber(number);
            }

            if (boards.Count == 1 && (boards[0].HasCompleteColumn() || boards[0].HasCompleteRow()))
            {
                return boards[0].CalculateScore(number);
            }

            boards = boards.Where(x => !x.HasCompleteColumn() && !x.HasCompleteRow()).ToList();
        }

        return 0;
    }

    private static (List<int>, List<Board>) ParseInput(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var numbers = lines[0].Split(",").Select(int.Parse).ToList();

        var boards = new List<Board>();
        var boardChunks = lines[1..]
            .Chunk(5);

        foreach (var chunk in boardChunks)
        {
            var rows = chunk
                .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            boards.Add(new Board(rows));
        }

        return (numbers, boards);
    }
}

internal record Board
{
    private readonly int[][] _numbers;
    private readonly bool[][] _drawnNumbers;
    private readonly Dictionary<int, List<(int, int)>> _numberPositions;

    public Board(int[][] numbers)
    {
        _numbers = numbers;
        _drawnNumbers = new bool[numbers.Length][];
        _numberPositions = new Dictionary<int, List<(int, int)>>();

        for (var i = 0; i < numbers.Length; i++)
        {
            _drawnNumbers[i] = new bool[numbers[i].Length];
            for (var j = 0; j < numbers[i].Length; j++)
            {
                if (!_numberPositions.TryGetValue(numbers[i][j], out var value))
                {
                    value = [];
                    _numberPositions[numbers[i][j]] = value;
                }

                value.Add((i, j));
            }
        }
    }

    public void DrawNumber(int number)
    {
        if (!_numberPositions.TryGetValue(number, out var positions))
        {
            return;
        }

        foreach (var (i, j) in positions)
        {
            _drawnNumbers[i][j] = true;
        }
    }

    public bool HasCompleteColumn()
    {
        for (var j = 0; j < _numbers[0].Length; j++)
        {
            var complete = true;
            for (var i = 0; i < _numbers.Length; i++)
            {
                if (!_drawnNumbers[i][j])
                {
                    complete = false;
                    break;
                }
            }
            if (complete)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasCompleteRow()
    {
        for (var i = 0; i < _numbers.Length; i++)
        {
            var complete = true;
            for (var j = 0; j < _numbers[i].Length; j++)
            {
                if (!_drawnNumbers[i][j])
                {
                    complete = false;
                    break;
                }
            }
            if (complete)
            {
                return true;
            }
        }
        return false;
    }

    public int CalculateScore(int drawnNumber)
    {
        var sum = 0;
        for (var i = 0; i < _numbers.Length; i++)
        {
            for (var j = 0; j < _numbers[i].Length; j++)
            {
                if (!_drawnNumbers[i][j])
                {
                    sum += _numbers[i][j];
                }
            }
        }

        return sum * drawnNumber;
    }
}
