using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day01;

[ProblemName("Inverse Captcha")]
public class Solution : ISolver
{
    public object PartOne(string input) => Solve(input, 1);

    public object PartTwo(string input) => Solve(input, input.Length / 2);

    private static int Solve(string input, int mod)
    {
        var sum = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == input[(i + mod) % input.Length])
            {
                sum += input[i] - '0';
            }
        }

        return sum;
    }
}
