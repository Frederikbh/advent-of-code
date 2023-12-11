namespace AdventOfCode.Y2022.Day06;

[ProblemName("Tuning Trouble")]
public class Solution : ISolver 
{

    public object? PartOne(string input)
    {
        for (var i = 3; i < input.Length; i++)
        {
            if (CountWindow(input, i - 3, 4) == 4)
                return i + 1;
        }

        return 0;
    }

    private int CountWindow(string input, int position, int size)
    {
        return input.AsSpan()
            .Slice(position, size)
            .ToArray()
            .Distinct()
            .Count();
    }

    public object? PartTwo(string input) 
    {
        for (var i = 13; i < input.Length; i++)
        {
            if (CountWindow(input, i - 13, 14) == 14)
                return i + 1;
        }

        return 0;
    }

    private int Bit(char c)
    {
        return 1 << (c - 'a');
    }
}
