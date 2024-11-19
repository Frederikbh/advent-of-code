using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day07;

[ProblemName("Camel Cards")]
public class Solution : ISolver
{

    public object PartOne(string input)
    {
        var hands = ParseInput(input);
        hands.Sort();

        return ScoreHands(hands);
    }

    public object PartTwo(string input)
    {
        var hands = ParseInput(input, true);
        hands.Sort();

        return ScoreHands(hands);
    }

    private static int ScoreHands(IEnumerable<PokerHand> hands)
    {
        return hands
            .Select((t, i) => t.Bet * (i + 1))
            .Sum();
    }

    private static List<PokerHand> ParseInput(string input, bool joker = false)
    {
        return input.Split('\n')
            .Select(line => line.Split(' '))
            .Select(split => new PokerHand(split[0], int.Parse(split[1]), joker))
            .ToList();
    }

    private record PokerHand : IComparable<PokerHand>
    {
        public int Bet { get; }

        private readonly char[] _cardValueOrder;

        private readonly string _hand;

        private readonly int _jokers;

        private readonly Dictionary<char, int> _handDetails;
        private readonly HandType _handType;

        public PokerHand(string hand, int bet, bool joker)
        {
            _hand = hand;
            Bet = bet;

            _cardValueOrder = new[] { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
            _handDetails = _hand
                .GroupBy(e => e)
                .ToDictionary(e => e.Key, e => e.Count());

            if (joker)
            {
                _cardValueOrder = new[] { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

                _jokers = hand.Count(e => e == 'J');
                _handDetails.Remove('J');
            }

            _handType = GetHandType(this);
        }


        public int CompareTo(PokerHand? other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (ReferenceEquals(null, other))
                return 1;

            if (_handType != other._handType)
                return other._handType - _handType;

            for (var i = 0; i < _hand.Length; i++)
            {
                var cardOrder = Array.IndexOf(_cardValueOrder, _hand[i]);
                var otherCardOrder = Array.IndexOf(_cardValueOrder, other._hand[i]);

                if (cardOrder != otherCardOrder)
                    return otherCardOrder - cardOrder;
            }

            return 0;
        }

        private static HandType GetHandType(PokerHand hand)
        {
            if (hand._handDetails.Any(e => e.Value + hand._jokers == 5) || hand._jokers == 5)
                return HandType.FiveOfAKind;
            if (hand._handDetails.Any(e => e.Value + hand._jokers == 4))
                return HandType.FourOfAKind;
            if (hand._handDetails.Any(e => e.Value == 3) && hand._handDetails.Any(e => e.Value == 2) ||
                hand._handDetails.Count(e => e.Value == 2) == 2 && hand._jokers > 0)
                return HandType.FullHouse;
            if (hand._handDetails.Any(e => e.Value + hand._jokers == 3))
                return HandType.ThreeOfAKind;
            if (hand._handDetails.Count(e => e.Value == 2) == 2)
                return HandType.TwoPairs;
            if (hand._handDetails.Any(e => e.Value == 2) || hand._jokers > 0)
                return HandType.OnePair;

            return HandType.HighCard;
        }

        private enum HandType
        {
            FiveOfAKind,
            FourOfAKind,
            FullHouse,
            ThreeOfAKind,
            TwoPairs,
            OnePair,
            HighCard
        }
    }
}
