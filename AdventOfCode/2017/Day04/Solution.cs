using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day04;

[ProblemName("High-Entropy Passphrases")]
public class Solution : ISolver
{
    public object PartOne(string input) =>
        input.Split('\n')
            .Select(e => e.Split(' '))
            .Count(
                e => e.Distinct()
                    .Count() == e.Length);

    public object PartTwo(string input) =>
        input.Split('\n')
            .Select(
                e =>
                {
                    var words = e.Split(' ');
                    var orderedWords = words.Select(w => string.Concat(w.Order()));

                    return new
                    {
                        Words = words,
                        OrderedWords = orderedWords.Distinct()
                            .ToArray()
                    };
                })
            .Count(e => e.Words.Length == e.OrderedWords.Length);
}
