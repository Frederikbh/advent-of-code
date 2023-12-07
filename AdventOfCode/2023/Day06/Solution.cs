using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day06;

[ProblemName("Wait For It")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var races = ParseInputPartOne(input);

        var result = 1L;

        foreach (var race in races)
        {
            var min = CalculateMinHold(race.Time, race.Distance);
            var max = CalculateMaxHold(race.Time, race.Distance);
            var combinations = max - min + 1;

            result *= combinations;
        }

        return result;
    }

    public object PartTwo(string input) 
    {
        var race = ParseInputPartTwo(input);
        var min = CalculateMinHold(race.Time, race.Distance);
        var max = CalculateMaxHold(race.Time, race.Distance);
        var combinations = max - min + 1;

        return combinations;
    }

    private long CalculateMinHold(long time, long distance)
    {
        var result = 0.5 * (time - Math.Sqrt(time * time - 4 * distance));

        return (long)Math.Floor(result) + 1;
    }

    private long CalculateMaxHold(long time, long distance)
    {
        var result = 0.5 * (Math.Sqrt(time * time - 4 * distance) + time);

        var rounded = Math.Floor(result);

        return rounded == result
            ? (long)(rounded - 1) 
            : (long)rounded;
    }

    private static List<Race> ParseInputPartOne(string input)
    {
        var lines = input.Split('\n');
        var times = lines[0].Split(':', 2)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var distances = lines[1].Split(':', 2)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        return times
            .Select((t, i) => new Race(t, distances[i]))
            .ToList();
    }

    private static Race ParseInputPartTwo(string input)
    {
        var lines = input.Split('\n');
        
        // remove any non digit characters from lines[0]
        var time = long.Parse(Regex.Replace(lines[0], "[^0-9]", ""));
        var distance = long.Parse(Regex.Replace(lines[1], "[^0-9]", ""));

        return new Race(time, distance);
    }

    private record Race(long Time, long Distance);
}
