using System.Security.Cryptography;
using System.Text;

using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day17;

[ProblemName("Two Steps Forward")]
public class Solution : ISolver
{
    public object PartOne(string input) => FindPath(input, findLongest: false);

    public object PartTwo(string input) => FindPath(input, findLongest: true);

    private static object FindPath(string input, bool findLongest)
    {
        var queue = new Queue<Position>();
        queue.Enqueue(new Position(0, 0, input));
        var longestPath = string.Empty;

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();
            if (position is { X: 3, Y: 3 })
            {
                var path = position.Pass[8..];
                if (findLongest)
                {
                    if (path.Length > longestPath.Length)
                    {
                        longestPath = path;
                    }
                }
                else
                {
                    return path;
                }
                continue;
            }

            var hash = Hash(position.Pass);
            EnqueueIfValid(queue, position, hash[0], 0, -1, 'U');
            EnqueueIfValid(queue, position, hash[1], 0, 1, 'D');
            EnqueueIfValid(queue, position, hash[2], -1, 0, 'L');
            EnqueueIfValid(queue, position, hash[3], 1, 0, 'R');
        }

        return findLongest ? longestPath.Length : string.Empty;
    }

    private static void EnqueueIfValid(Queue<Position> queue, Position position, char hashChar, int dx, int dy, char direction)
    {
        if ("bcdef".Contains(hashChar))
        {
            var newX = position.X + dx;
            var newY = position.Y + dy;
            if (newX is >= 0 and <= 3 && newY is >= 0 and <= 3)
            {
                queue.Enqueue(new Position(newX, newY, position.Pass + direction));
            }
        }
    }

    private static string Hash(string input)
    {
        var hash = MD5.HashData(Encoding.ASCII.GetBytes(input));
        return string.Concat(hash.Select(b => b.ToString("x2")).Take(2));
    }
}

internal record struct Position(int X, int Y, string Pass);
