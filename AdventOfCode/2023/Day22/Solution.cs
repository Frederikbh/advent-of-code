using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day22;

[ProblemName("Sand Slabs")]
public class Solution : ISolver
{

    public object PartOne(string input)
    {
        var bricks = ParseInput(input);
        bricks = ApplyGravity(bricks);
        var supports = GetSupports(bricks);

        var result = bricks
            .Select(e => Disintegrate(e, supports))
            .Count(e => e == 0);
        
        return result;
    }

    public object PartTwo(string input)
    {
        var bricks = ParseInput(input);
        bricks = ApplyGravity(bricks);
        var supports = GetSupports(bricks);

        var result = bricks
            .Select(e => Disintegrate(e, supports))
            .Sum();

        return result;
    }

    private static int Disintegrate(Brick disintegratedBrick, Supports supports)
    {
        var queue = new Queue<Brick>();
        queue.Enqueue(disintegratedBrick);

        var falling = new HashSet<Brick>();

        while (queue.TryDequeue(out var brick))
        {
            falling.Add(brick);

            var bricksStartFalling = supports.BricksAbove[brick]
                .Where(e => supports.BricksBelow[e].IsSubsetOf(falling));

            foreach (var fallingBrick in bricksStartFalling)
            {
                queue.Enqueue(fallingBrick);
            }
        }

        return falling.Count - 1;
    }

    private static List<Brick> ApplyGravity(List<Brick> bricks)
    {
        // sort them in Z first so that we can work in bottom to top order
        bricks = bricks.OrderBy(block => block.Bottom).ToList();

        for (var i = 0; i < bricks.Count; i++)
        {
            var newBottom = 1;
            for (var j = 0; j < i; j++)
            {
                if (IntersectsXy(bricks[i], bricks[j]))
                {
                    newBottom = Math.Max(newBottom, bricks[j].Top + 1);
                }
            }
            var fall = bricks[i].Bottom - newBottom;
            bricks[i] = bricks[i] with
            {
                Z = new Range(bricks[i].Bottom - fall, bricks[i].Top - fall)
            };
        }
        return bricks;
    }

    private static Supports GetSupports(List<Brick> blocks)
    {
        var blocksAbove = blocks.ToDictionary(b => b, _ => new HashSet<Brick>());
        var blocksBelow = blocks.ToDictionary(b => b, _ => new HashSet<Brick>());
        for (var i = 0; i < blocks.Count; i++)
        {
            for (var j = i + 1; j < blocks.Count; j++)
            {
                var zNeighbours = blocks[j].Bottom == 1 + blocks[i].Top;
                if (zNeighbours && IntersectsXy(blocks[i], blocks[j]))
                {
                    blocksBelow[blocks[j]].Add(blocks[i]);
                    blocksAbove[blocks[i]].Add(blocks[j]);
                }
            }
        }
        return new Supports(blocksAbove, blocksBelow);
    }

    private static bool IntersectsXy(Brick brickA, Brick brickB) =>
        Intersects(brickA.X, brickB.X) && Intersects(brickA.Y, brickB.Y);

    private static bool Intersects(Range r1, Range r2) => r1.Start <= r2.End && r2.Start <= r1.End;

    private static List<Brick> ParseInput(string input)
    {
        var lines = input.Split('\n');
        var bricks = new List<Brick>();

        foreach (var line in lines)
        {
            var numbers = line.Split(',', '~')
                .Select(int.Parse)
                .ToList();

            var x = new Range(numbers[0], numbers[3]);
            var y = new Range(numbers[1], numbers[4]);
            var z = new Range(numbers[2], numbers[5]);
            bricks.Add(new Brick(x, y, z));
        }

        return bricks;
    }

    private record Range(int Start, int End);

    private record Brick(Range X, Range Y, Range Z)
    {
        public int Top => Z.End;
        public int Bottom => Z.Start;
    }

    private record Supports(Dictionary<Brick, HashSet<Brick>> BricksAbove, Dictionary<Brick, HashSet<Brick>> BricksBelow);
}
