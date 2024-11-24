using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day07;

[ProblemName("Recursive Circus")]
public partial class Solution : ISolver
{
    public object PartOne(string input)
    {
        var tree = ParseNodes(input);

        return GetRoot(tree)
            .Name;
    }

    public object PartTwo(string input)
    {
        var tree = ParseNodes(input);
        var root = GetRoot(tree);

        CalculateWeights(tree, root);

        var bogus = BogusChild(root, tree)!;
        var desiredWeight = tree[root.Children.First(childId => childId != bogus.Name)].TotalWeight;
        var result = Fix(bogus, desiredWeight, tree);

        return result;
    }

    private static Node GetRoot(Dictionary<string, Node> nodes)
    {
        var allChildren = nodes.Values.SelectMany(e => e.Children)
            .ToHashSet();

        return nodes.First(e => !allChildren.Contains(e.Key))
            .Value;
    }

    private static int CalculateWeights(Dictionary<string, Node> tree, Node node)
    {
        node.TotalWeight = node.Weight + node.Children.Select(e => CalculateWeights(tree, tree[e]))
            .Sum();

        return node.TotalWeight;
    }

    private static Node? BogusChild(Node node, Dictionary<string, Node> tree)
    {
        var w =
            node.Children.Select(
                    childId => new
                    {
                        childId,
                        child = tree[childId]
                    })
                .GroupBy(e => e.child.TotalWeight, e => e.child)
                .OrderBy(childrenByTreeWeight => childrenByTreeWeight.Count())
                .ToArray();

        return w.Length == 1
            ? null
            : w[0]
                .Single();
    }

    private static int Fix(Node node, int desiredWeight, Dictionary<string, Node> tree)
    {
        while (true)
        {
            if (node.Children.Length < 2)
            {
                throw new Exception();
            }

            var bogusChild = BogusChild(node, tree);

            if (bogusChild == null)
            {
                return desiredWeight - node.TotalWeight + node.Weight;
            }

            desiredWeight = desiredWeight - node.TotalWeight + bogusChild.TotalWeight;
            node = bogusChild;
        }
    }

    private static Dictionary<string, Node> ParseNodes(string input)
    {
        var lines = input.Split('\n');
        var nodes = new Dictionary<string, Node>();

        var nodeRegex = NodeRegex();

        foreach (var line in lines)
        {
            var match = nodeRegex.Match(line);

            var name = match.Groups[1].Value;
            var weight = int.Parse(match.Groups[2].Value);
            var children = match.Groups[3]
                .Value.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            var node = new Node(name, weight, children);

            nodes.Add(node.Name, node);
        }

        return nodes;
    }

    [GeneratedRegex(@"(\w+) \((\d+)\)(?: -> (.+))?")]
    public static partial Regex NodeRegex();
}

internal record Node(string Name, int Weight, string[] Children)
{
    public int TotalWeight { get; set; }
}
