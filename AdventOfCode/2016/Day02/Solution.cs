using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day02;

[ProblemName("Bathroom Security")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var commands = ParseInput(input);
        const string Keypad = "123\n456\n789";

        var pos = (1, 1);

        return CrackCode(commands, pos, Keypad);
    }

    public object PartTwo(string input)
    {
        var commands = ParseInput(input);
        const string Keypad = "  1  \n 234 \n56789\n ABC \n  D  ";

        var pos = (2, 0);

        return CrackCode(commands, pos, Keypad);
    }

    private static string CrackCode(List<char[]> commands, (int, int) pos, string keypadStr)
    {
        var keypad = keypadStr.Split('\n')
            .Select(line => line.ToCharArray())
            .ToArray();

        var code = string.Empty;

        foreach (var command in commands)
        {
            foreach (var move in command)
            {
                var (modY, modX) = move switch
                {
                    'U' => (-1, 0),
                    'D' => (1, 0),
                    'L' => (0, -1),
                    'R' => (0, 1),
                    _ => throw new Exception()
                };

                var (newY, newX) = (pos.Item1 + modY, pos.Item2 + modX);

                if (newY >= 0 && newY < keypad.Length &&
                    newX >= 0 && newX < keypad[newY].Length &&
                    keypad[newY][newX] != ' ')
                {
                    pos = (newY, newX);
                }
            }

            code += keypad[pos.Item1][pos.Item2 % keypad[pos.Item1].Length];
        }

        return code;
    }

    public static List<char[]> ParseInput(string input) =>
        input.Split('\n')
            .Select(line => line.ToCharArray())
            .ToList();
}
