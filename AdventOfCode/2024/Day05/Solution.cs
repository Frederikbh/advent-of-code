using AdventOfCode.Lib;

namespace AdventOfCode._2024.Day05;

[ProblemName("Print Queue")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var (orders, updates) = ParseInput(input);
        var orderSet = new HashSet<Order>(orders);

        return updates.Sum(e => GetMiddleDigit(e, orderSet, true));
    }

    public object PartTwo(string input)
    {
        var (orders, updates) = ParseInput(input);
        var orderSet = new HashSet<Order>(orders);

        return updates.Sum(e => GetMiddleDigit(e, orderSet, false));
    }

    private static int GetMiddleDigit(
        List<int> update,
        HashSet<Order> orders,
        bool countValidUpdates)
    {
        var comparer = new UpdateComparer(orders);
        var ordered = update.Order(comparer)
            .ToList();

        return countValidUpdates switch
        {
            true when update.SequenceEqual(ordered) => update[update.Count / 2],
            false when !update.SequenceEqual(ordered) => ordered[update.Count / 2],
            _ => 0
        };
    }

    private static (List<Order> Orders, List<List<int>> Updates) ParseInput(string input)
    {
        var parts = input.Split("\n\n");
        var orders = parts[0]
            .Split('\n')
            .Select(
                e => e.Split('|')
                    .Select(int.Parse)
                    .ToList())
            .Select(e => new Order(e[0], e[1]))
            .ToList();

        var updates = parts[1]
            .Split('\n')
            .Select(
                e => e.Split(',')
                    .Select(int.Parse)
                    .ToList())
            .ToList();

        return (orders, updates);
    }

    private class UpdateComparer : IComparer<int>
    {
        private readonly HashSet<Order> _orders;

        public UpdateComparer(HashSet<Order> orders)
        {
            _orders = orders;
        }

        public int Compare(int x, int y)
        {
            if (_orders.Contains(new Order(x, y)))
            {
                return -1;
            }

            if (_orders.Contains(new Order(y, x)))
            {
                return 1;
            }

            return 0;
        }
    }

    private record struct Order(int First, int Second);
}
