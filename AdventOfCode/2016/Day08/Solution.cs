using System.Text;

using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day08;

[ProblemName("Two-Factor Authentication")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var screen = new Screen();
        var commands = input.Split("\n");

        foreach (var command in commands)
        {
            screen.RunCommand(command);
        }

        return screen.CountLitPixels();
    }

    public object PartTwo(string input)
    {
        var screen = new Screen();
        var commands = input.Split("\n");

        foreach (var command in commands)
        {
            screen.RunCommand(command);
        }

        return screen.ToString()
            .Ocr();
    }
}

internal record Screen
{
    private readonly char[][] _screen;

    public Screen()
    {
        _screen = new char[6][];

        for (var i = 0; i < 6; i++)
        {
            _screen[i] = new char[50];

            for (var j = 0; j < 50; j++)
            {
                _screen[i][j] = '.';
            }
        }
    }

    public void RunCommand(string command)
    {
        if (command.StartsWith("rect"))
        {
            var rectParts = command.Split(' ');
            var dims = rectParts[1]
                .Split('x');
            var a = int.Parse(dims[0]);
            var b = int.Parse(dims[1]);
            Rect(a, b);
        }
        else if (command.StartsWith("rotate row"))
        {
            var rowParts = command.Split(' ');
            var y = int.Parse(
                rowParts[2]
                    .Split('=')[1]);
            var by = int.Parse(rowParts[4]);
            RotateRow(y, by);
        }
        else if (command.StartsWith("rotate column"))
        {
            var colParts = command.Split(' ');
            var x = int.Parse(
                colParts[2]
                    .Split('=')[1]);
            var by = int.Parse(colParts[4]);
            RotateColumn(x, by);
        }
    }

    public void Rect(int a, int b)
    {
        for (var i = 0; i < a; i++)
        {
            for (var j = 0; j < b; j++)
            {
                _screen[j][i] = '#';
            }
        }
    }

    public void RotateRow(int a, int b)
    {
        var row = new char[50];

        for (var i = 0; i < 50; i++)
        {
            row[(i + b) % 50] = _screen[a][i];
        }

        for (var i = 0; i < 50; i++)
        {
            _screen[a][i] = row[i];
        }
    }

    public void RotateColumn(int a, int b)
    {
        var column = new char[6];

        for (var i = 0; i < 6; i++)
        {
            column[(i + b) % 6] = _screen[i][a];
        }

        for (var i = 0; i < 6; i++)
        {
            _screen[i][a] = column[i];
        }
    }

    public int CountLitPixels()
    {
        var count = 0;

        for (var i = 0; i < 6; i++)
        {
            for (var j = 0; j < 50; j++)
            {
                if (_screen[i][j] == '#')
                {
                    count++;
                }
            }
        }

        return count;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (var i = 0; i < 6; i++)
        {
            for (var j = 0; j < 50; j++)
            {
                sb.Append(_screen[i][j]);
            }

            sb.Append("\n");
        }

        return sb.ToString();
    }
}
