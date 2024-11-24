using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day21;

[ProblemName("Scrambled Letters and Hash")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var scramble = ParseInstructions(input);
        const string InitialString = "abcdefgh";
        var scrambled = scramble(InitialString);

        return scrambled;
    }

    public object PartTwo(string input)
    {
        var scramble = ParseInstructions(input);
        const string Target = "fbgdceah";

        var original = Permutations("abcdefgh".ToCharArray())
            .First(permutation => scramble(new string(permutation)) == Target);

        return new string(original);
    }

    private static IEnumerable<char[]> Permutations(char[] array) => Permute(array, 0);

    private static IEnumerable<char[]> Permute(char[] array, int start)
    {
        if (start == array.Length)
        {
            yield return (char[])array.Clone();
        }
        else
        {
            for (var i = start; i < array.Length; i++)
            {
                Swap(ref array[start], ref array[i]);

                foreach (var perm in Permute(array, start + 1))
                {
                    yield return perm;
                }

                Swap(ref array[start], ref array[i]);
            }
        }
    }

    private Func<string, string> ParseInstructions(string input)
    {
        var commands = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseCommand)
            .ToList();

        return inputString =>
        {
            var chars = inputString.ToCharArray();

            foreach (var command in commands)
            {
                command(chars);
            }

            return new string(chars);
        };
    }

    private Action<char[]> ParseCommand(string line) =>
        Match(
            line,
            @"swap position (\d+) with position (\d+)",
            groups =>
            {
                var x = int.Parse(groups[0]);
                var y = int.Parse(groups[1]);

                return chars => SwapPosition(chars, x, y);
            })
        ?? Match(
            line,
            @"swap letter (\w) with letter (\w)",
            groups =>
            {
                var x = groups[0][0];
                var y = groups[1][0];

                return chars => SwapLetter(chars, x, y);
            })
        ?? Match(
            line,
            @"rotate left (\d+) step",
            groups =>
            {
                var steps = int.Parse(groups[0]);

                return chars => RotateLeft(chars, steps);
            })
        ?? Match(
            line,
            @"rotate right (\d+) step",
            groups =>
            {
                var steps = int.Parse(groups[0]);

                return chars => RotateRight(chars, steps);
            })
        ?? Match(
            line,
            @"rotate based on position of letter (\w)",
            groups =>
            {
                var x = groups[0][0];

                return chars => RotateBasedOnPosition(chars, x);
            })
        ?? Match(
            line,
            @"reverse positions (\d+) through (\d+)",
            groups =>
            {
                var x = int.Parse(groups[0]);
                var y = int.Parse(groups[1]);

                return chars => Reverse(chars, x, y);
            })
        ?? Match(
            line,
            @"move position (\d+) to position (\d+)",
            groups =>
            {
                var from = int.Parse(groups[0]);
                var to = int.Parse(groups[1]);

                return chars => MovePosition(chars, from, to);
            })
        ?? throw new InvalidOperationException($"Cannot parse instruction: {line}");

    private static Action<char[]>? Match(string line, string pattern, Func<string[], Action<char[]>> actionBuilder)
    {
        var match = Regex.Match(line, pattern);

        if (match.Success)
        {
            var groups = match.Groups.Cast<Group>()
                .Skip(1)
                .Select(g => g.Value)
                .ToArray();

            return actionBuilder(groups);
        }

        return null;
    }

    private static void SwapPosition(char[] chars, int x, int y) => Swap(ref chars[x], ref chars[y]);

    private static void SwapLetter(char[] chars, char x, char y)
    {
        for (var i = 0; i < chars.Length; i++)
        {
            if (chars[i] == x)
            {
                chars[i] = y;
            }
            else if (chars[i] == y)
            {
                chars[i] = x;
            }
        }
    }

    private static void RotateLeft(char[] chars, int steps)
    {
        steps %= chars.Length;
        Rotate(chars, steps, true);
    }

    private static void RotateRight(char[] chars, int steps)
    {
        steps %= chars.Length;
        Rotate(chars, steps, false);
    }

    private static void RotateBasedOnPosition(char[] chars, char x)
    {
        var index = Array.IndexOf(chars, x);
        var steps = index >= 4
            ? index + 2
            : index + 1;
        steps %= chars.Length;
        RotateRight(chars, steps);
    }

    private static void Reverse(char[] chars, int start, int end)
    {
        while (start < end)
        {
            Swap(ref chars[start], ref chars[end]);
            start++;
            end--;
        }
    }

    private static void MovePosition(char[] chars, int from, int to)
    {
        var charToMove = chars[from];

        // Shift characters to fill the gap
        if (from < to)
        {
            Array.Copy(chars, from + 1, chars, from, to - from);
        }
        else
        {
            Array.Copy(chars, to, chars, to + 1, from - to);
        }

        chars[to] = charToMove;
    }

    private static void Rotate(char[] array, int steps, bool left)
    {
        var rotated = new char[array.Length];

        for (var i = 0; i < array.Length; i++)
        {
            var newIndex = left
                ? (i - steps + array.Length) % array.Length
                : (i + steps) % array.Length;
            rotated[newIndex] = array[i];
        }

        Array.Copy(rotated, array, array.Length);
    }

    private static void Swap(ref char a, ref char b) => (a, b) = (b, a);
}
