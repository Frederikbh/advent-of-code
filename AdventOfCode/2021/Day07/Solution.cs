using AdventOfCode.Lib;

namespace AdventOfCode._2021.Day07;

[ProblemName("The Treachery of Whales")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var depths = ParseInput(input);
        depths.Sort();

        var median = depths[depths.Count / 2];

        var fuelCost = depths.Select(e => Math.Abs(e - median))
            .Sum();

        return fuelCost;
    }

    public object PartTwo(string input)
    {
        var depths = ParseInput(input);

        var mean = depths.Average();
        var floor = (int)Math.Floor(mean);
        var ceil = (int)Math.Ceiling(mean);

        var fuelCostFloor = depths
            .Select(e => FuelCostFunc(Math.Abs(e - floor)))
            .Sum();
        var fuelCostCeil = depths
            .Select(e => FuelCostFunc(Math.Abs(e - ceil)))
            .Sum();

        return Math.Min(fuelCostFloor, fuelCostCeil);

        static int FuelCostFunc(int consumption)
        {
            return (consumption + 1) * consumption / 2;
        }
    }

    public static List<int> ParseInput(string input) =>
        input.Split(',')
            .Select(int.Parse)
            .ToList();
}
