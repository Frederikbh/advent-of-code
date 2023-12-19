using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day19;

[ProblemName("Aplenty")]
public class Solution : ISolver
{

    public object PartOne(string input)
    {
        var (workflows, parts) = ParseInput(input);

        var acceptedParts = new List<Part>();

        foreach (var part in parts)
        {
            var current = "in";

            do
            {
                var workflow = workflows[current];

                foreach (var rule in workflow.Rules)
                {
                    if (rule.Category == null)
                    {
                        current = rule.Workflow;
                        break;
                    }
                    var value = part.Values[rule.Category.Value];

                    if (rule.Comparer == '<' && value < rule.Value)
                    {
                        current = rule.Workflow;
                        break;
                    }
                    else if (rule.Comparer == '>' && value > rule.Value)
                    {
                        current = rule.Workflow;
                        break;
                    }
                    else if (rule.Comparer == null)
                    {
                        current = rule.Workflow;
                        break;
                    }
                }
            } while (current != "R" && current != "A");

            if (current == "A")
            {
                acceptedParts.Add(part);
            }
        }

        var result = acceptedParts
            .SelectMany(e => e.Values.Values)
            .Sum();

        return result;
    }

    public object PartTwo(string input)
    {
        var (workflows, parts) = ParseInput(input);

        var ranges = new Dictionary<char, (int Min, int Max)>
        {
            ['x'] = (1, 4000),
            ['m'] = (1, 4000),
            ['a'] = (1, 4000),
            ['s'] = (1, 4000)
        };


        return ProcessRanges("in", ranges, workflows);
    }

    private static long ProcessRanges(string position, Dictionary<char, (int Min, int Max)> ranges, Dictionary<string, Workflow> workflows)
    {
        switch (position)
        {
            case "A":
                return ranges.Values.Aggregate(1L, (current, range) => current * (range.Max - range.Min + 1));
            case "R":
                return 0;
        }

        long result = 0;
        var workflow = workflows[position];

        foreach (var rule in workflow.Rules)
        {
            var (min, max) = rule.Category == null ? (0, 0) : ranges[rule.Category.Value];

            switch (rule.Comparer)
            {
                case '<':
                    if (max < rule.Value)
                    {
                        result += ProcessRanges(rule.Workflow, ranges, workflows);

                        return result;
                    }

                    if (min < rule.Value)
                    {
                        var newRanges = new Dictionary<char, (int Min, int Max)>(ranges)
                        {
                            [rule.Category!.Value] = (min, rule.Value - 1)
                        };

                        result += ProcessRanges(rule.Workflow, newRanges, workflows);
                        ranges[rule.Category.Value] = (rule.Value, max);
                    }

                    break;
                case '>':
                    if (min > rule.Value)
                    {
                        result += ProcessRanges(rule.Workflow, ranges, workflows);
                        return result;
                    }

                    if (max > rule.Value)
                    {
                        var newRanges = new Dictionary<char, (int Min, int Max)>(ranges)
                        {
                            [rule.Category!.Value] = (rule.Value + 1, max)
                        };

                        result += ProcessRanges(rule.Workflow, newRanges, workflows);
                        ranges[rule.Category.Value] = (min, rule.Value);
                    }

                    break;
                default:
                    result += ProcessRanges(rule.Workflow, ranges, workflows);
                    break;
            }
        }

        return result;
    }

    private static (Dictionary<string, Workflow>, List<Part>) ParseInput(string input)
    {
        var split = input.Split("\n\n")
            .Select(e => e.Split('\n'))
            .ToList();

        // Parse workflows
        var workflowRegex = new Regex(@"(\w+)\{(.+)\}");
        var ruleRegex = new Regex(@"(?:([xmas])([<>])(\d*):)?(\w+?)$");

        var workflows = new List<Workflow>();
        foreach (var line in split[0])
        {
            var match = workflowRegex.Match(line);
            var name = match.Groups[1].Value;
            var ruleLines = match.Groups[2].Value.Split(',');

            var rules = new List<Rule>();
            foreach (var ruleSegment in ruleLines)
            {
                var ruleMatch = ruleRegex.Match(ruleSegment);
                char? category = ruleMatch.Groups[1].Length > 0 ? ruleMatch.Groups[1].Value[0] : null;
                var hasValue = int.TryParse(ruleMatch.Groups[3].Value, out var value);
                var comparer = ruleMatch.Groups[2].Value.FirstOrDefault();
                var workflowName = ruleMatch.Groups[4].Value;

                var rule = new Rule(category, comparer, value, workflowName);
                rules.Add(rule);
            }

            var workflow = new Workflow(name, rules);
            workflows.Add(workflow);
        }

        // Parse parts
        var parts = new List<Part>();
        var partsRegex = new Regex(@"\{x=(\d+),m=(\d+),a=(\d+),s=(\d+)\}");

        foreach (var line in split[1])
        {
            var match = partsRegex.Match(line);
            var x = int.Parse(match.Groups[1].Value);
            var m = int.Parse(match.Groups[2].Value);
            var a = int.Parse(match.Groups[3].Value);
            var s = int.Parse(match.Groups[4].Value);

            var values = new Dictionary<char, int>
            {
                ['x'] = x,
                ['m'] = m,
                ['a'] = a,
                ['s'] = s
            };
            var part = new Part(values);
            parts.Add(part);
        }


        return (workflows.ToDictionary(e => e.Name), parts);
    }

    private record Part(Dictionary<char, int> Values);
    private record Workflow(string Name, List<Rule> Rules);
    private record Rule(char? Category, char? Comparer, int Value, string Workflow);
}
