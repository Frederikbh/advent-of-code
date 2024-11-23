using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day10;

[ProblemName("Balance Bots")]
public partial class Solution : ISolver
{
    public object PartOne(string input)
    {
        var bots = ParseBots(input);
        var executions = new BotController(bots).Execute();

        // Find the bot that compares 17 and 61
        return executions.First(e => e is { Low: 17, High: 61 }).Id.Split(' ')[1];
    }

    public object PartTwo(string input)
    {
        var bots = ParseBots(input);
        _ = new BotController(bots).Execute().ToList();

        // Multiply outputs 0, 1, and 2
        return bots["output 0"].Values.Single()
            * bots["output 1"].Values.Single()
            * bots["output 2"].Values.Single();
    }

    private static Dictionary<string, Bot> ParseBots(string input)
    {
        var bots = new Dictionary<string, Bot>();

        foreach (var line in input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            if (AssignRegex()
                    .Match(line) is { Success: true } assignMatch)
            {
                var value = int.Parse(assignMatch.Groups[1].Value);
                var botId = assignMatch.Groups[2].Value;

                GetOrCreateBot(botId)
                    .Values.Add(value);
            }
            else if (BotRegex()
                         .Match(line) is { Success: true } botMatch)
            {
                var sourceBot = botMatch.Groups[1].Value;
                var lowRecipient = botMatch.Groups[2].Value;
                var highRecipient = botMatch.Groups[3].Value;

                var bot = GetOrCreateBot(sourceBot);
                bot.LowRecipient = lowRecipient;
                bot.HighRecipient = highRecipient;

                GetOrCreateBot(lowRecipient); // Ensure low recipient exists
                GetOrCreateBot(highRecipient); // Ensure high recipient exists
            }
        }

        return bots;

        Bot GetOrCreateBot(string id)
        {
            return bots.TryGetValue(id, out var bot)
                ? bot
                : bots[id] = new Bot(id);
        }
    }

    [GeneratedRegex(@"value (\d+) goes to (.+)")]
    public static partial Regex AssignRegex();

    [GeneratedRegex("(.+) gives low to (.+) and high to (.+)")]
    public static partial Regex BotRegex();
}

internal class BotController
{
    private readonly Dictionary<string, Bot> _bots;

    public BotController(Dictionary<string, Bot> bots)
    {
        _bots = bots;
    }

    public IEnumerable<ExecutionRecord> Execute()
    {
        var activeBots = new Queue<Bot>(_bots.Values.Where(b => b.Values.Count == 2));

        while (activeBots.Count > 0)
        {
            var bot = activeBots.Dequeue();

            if (bot.Values.Count < 2 || bot.LowRecipient == null || bot.HighRecipient == null)
            {
                continue;
            }

            var (low, high) = (bot.Values.Min(), bot.Values.Max());
            bot.Values.Clear();

            _bots[bot.LowRecipient].Values.Add(low);
            _bots[bot.HighRecipient].Values.Add(high);

            // Add recipients to queue if they now have 2 values
            if (_bots[bot.LowRecipient].Values.Count == 2)
            {
                activeBots.Enqueue(_bots[bot.LowRecipient]);
            }

            if (_bots[bot.HighRecipient].Values.Count == 2)
            {
                activeBots.Enqueue(_bots[bot.HighRecipient]);
            }

            yield return new ExecutionRecord(bot.Id, low, high);
        }
    }
}

internal record ExecutionRecord(string Id, int Low, int High);

internal record Bot
{
    public string Id { get; }

    public string? LowRecipient { get; set; }

    public string? HighRecipient { get; set; }

    public List<int> Values { get; } = [];

    public Bot(string id)
    {
        Id = id;
    }
}
