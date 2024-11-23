using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day09;

[ProblemName("Explosives in Cyberspace")]
public class Solution : ISolver
{
    public object PartOne(string input) => DecompressedLength(input, false);

    public object PartTwo(string input) => DecompressedLength(input, true);

    private static long DecompressedLength(ReadOnlySpan<char> input, bool recurse)
    {
        var totalLength = 0L;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                // Find the end of the marker
                var endOfMarker = input[i..]
                    .IndexOf(')');
                var markerStart = i + 1;
                var markerEnd = i + endOfMarker;

                // Parse marker (length x repeat)
                var xPosition = input[markerStart..markerEnd]
                    .IndexOf('x') + markerStart;
                var length = int.Parse(input[markerStart..xPosition]);
                var repeat = int.Parse(input[(xPosition + 1)..markerEnd]);

                if (recurse)
                {
                    var toRepeat = input[(markerEnd + 1)..(markerEnd + 1 + length)];
                    totalLength += repeat * DecompressedLength(toRepeat, true);
                }
                else
                {
                    totalLength += length * repeat;
                }

                i += endOfMarker + length;
            }
            else
            {
                totalLength++;
            }
        }

        return totalLength;
    }
}
