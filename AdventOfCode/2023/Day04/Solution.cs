namespace AdventOfCode.Y2023.Day04;

[ProblemName("Scratchcards")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var cards = ParseInput(input);

        var result = cards.Select(ScorePartOne).Sum();

        return result;
    }

    public object PartTwo(string input)
    {
        var cards = ParseInput(input)
            .ToDictionary(e => e.Number);

        var cardCount = cards.ToDictionary(e => e.Key, _ => 1);

        var max = cards.MaxBy(e => e.Key).Key;

        for (var i = 1; i <= max; i++)
        {
            var intersects = cards[i].WinningNumbers
                .Intersect(cards[i].SelectedNumbers)
                .Count();

            for (var k = i + 1; k <= max && k <= intersects + i; k++)
            {
                cardCount[k] += cardCount[i];
            }
        }

        return cardCount.Sum(e => e.Value);
    }

    private static int ScorePartOne(Card card)
    {
        var intersectCount = card.WinningNumbers
            .Intersect(card.SelectedNumbers)
            .Count();

        if (intersectCount < 2)
            return intersectCount;

        return (int)Math.Pow(2, intersectCount - 1);
    }

    private static List<Card> ParseInput(string input)
    {
        var lines = input.Split("\n");

        var cards = new List<Card>();
        foreach (var line in lines)
        {
            var trimmed = line.Split(':');
            // Remove last character of trimmed[0]
            var numberText = trimmed[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Last();
            var number = int.Parse(numberText);

            var numbersSplit = trimmed[1].Split('|');

            var winningNumbers = numbersSplit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .Select(int.Parse)
                .ToHashSet();
            var selectedNumbers = numbersSplit[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .Select(int.Parse)
                .ToHashSet();

            cards.Add(new Card(number, winningNumbers, selectedNumbers));
        }

        return cards;
    }

    private record Card(int Number, HashSet<int> WinningNumbers, HashSet<int> SelectedNumbers);
}
