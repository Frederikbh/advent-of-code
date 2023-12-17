namespace AdventOfCode.Y2023.Day16;

[ProblemName("The Floor Will Be Lava")]
public class Solution : ISolver
{

    public object PartOne(string input)
    {
        var grid = input
            .Split("\n")
            .Select(x => x.ToCharArray())
            .ToArray();

        return EnergizeGrid(grid, new Ray(-1, 0, 1, 0));
    }

    public object PartTwo(string input)
    {
        var validStartPoints = new List<Ray>();
        var grid = input
            .Split("\n")
            .Select(x => x.ToCharArray())
            .ToArray();

        for (var i = 0; i < grid.Length; i++)
        {
            validStartPoints.Add(new Ray(i, -1, 0, 1));
            validStartPoints.Add(new Ray(i, grid[0].Length, 0, -1));
            validStartPoints.Add(new Ray(-1, i, 1, 0));
            validStartPoints.Add(new Ray(grid.Length, i, -1, 0));
        }

        EnergizeGrid(grid, new Ray(10, 9, -1, 0));

        var energizeResults = validStartPoints
            .Select(e => EnergizeGrid(grid, e))
            .ToList();

        return energizeResults.Max();
    }

    private static int EnergizeGrid(char[][] grid, Ray startPoint)
    {
        var seenRays = new HashSet<Ray>();
        var energizedTiles = new HashSet<Point>();

        var rays = new Queue<Ray>();
        rays.Enqueue(startPoint);

        while (rays.Count > 0)
        {
            var ray = rays.Dequeue();

            var location = new Point(ray.X + ray.DX, ray.Y + ray.DY);

            if (location.X < 0 || location.X >= grid[0].Length || location.Y < 0 ||
                location.Y >= grid.Length)
            {
                continue;
            }

            energizedTiles.Add(location);

            var tile = grid[location.Y][location.X];

            var nextRays = new List<Ray>();
            if (tile is '.')
            {
                nextRays.Add(new Ray(location.X, location.Y, ray.DX, ray.DY));

            }
            else if (tile == '\\')
            {
                if (ray.DX > 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, 0, 1));
                }
                else if (ray.DX < 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, 0, -1));
                }
                else if (ray.DY > 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, 1, 0));
                }
                else
                {
                    nextRays.Add(new Ray(location.X, location.Y, -1, 0));
                }
            }
            else if (tile == '/')
            {
                if (ray.DX > 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, 0, -1));
                }
                else if (ray.DX < 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, 0, 1));
                }
                else if (ray.DY > 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, -1, 0));
                }
                else
                {
                    nextRays.Add(new Ray(location.X, location.Y, 1, 0));
                }
            }
            else if (tile == '|')
            {
                if (ray.DY != 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, ray.DX, ray.DY));
                }
                else
                {
                    nextRays.Add(new Ray(location.X, location.Y, 0, 1));
                    nextRays.Add(new Ray(location.X, location.Y, 0, -1));
                }
            }
            else if (tile == '-')
            {
                if (ray.DX != 0)
                {
                    nextRays.Add(new Ray(location.X, location.Y, ray.DX, ray.DY));
                }
                else
                {
                    nextRays.Add(new Ray(location.X, location.Y, 1, 0));
                    nextRays.Add(new Ray(location.X, location.Y, -1, 0));
                }
            }

            foreach (var nextRay in nextRays)
            {
                if (seenRays.Add(nextRay))
                {
                    rays.Enqueue(nextRay);
                }
            }
        }

        return energizedTiles.Count;
    }

    private record struct Ray(int X, int Y, int DX, int DY);

    private record struct Point(int X, int Y);
}
