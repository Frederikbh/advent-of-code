namespace AdventOfCode.Y2023.Day01;

[ProblemName("Trebuchet?!")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var lines = input.Split("\n");

        return lines.Sum(Calibrator);

        static int Calibrator(string line) => int.Parse($"{line.First(char.IsNumber)}{line.Last(char.IsNumber)}");
    }

    private static readonly Dictionary<string, int> SpelledDigits = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    public object? PartTwo(string input)
    {
        var lines = input.Split("\n");

        var sum = 0;
        foreach (var line in lines)
        {
            var digits = new List<int>();

            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsNumber(line[i]))
                {
                    digits.Add(line[i] - '0');
                    continue;
                }

                foreach (var (spelled, digit) in SpelledDigits)
                {
                    if (i + spelled.Length <= line.Length && line[i..(i + spelled.Length)] == spelled)
                    {
                        digits.Add(digit);
                        break;
                    }
                }
            }

            sum += 10 * digits.First();
            sum += digits.Last();
        }

        return sum;
    }
}
