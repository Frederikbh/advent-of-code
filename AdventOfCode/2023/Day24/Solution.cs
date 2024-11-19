using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day24;

[ProblemName("Never Tell Me The Odds")]
public class Solution : ISolver 
{
    public object PartOne(string input)
    {
        var particles = Project(ParseParticles(input), v => (v.X0, v.X1));

        var res = 0;
        for (var i = 0; i < particles.Length; i++)
        {
            for (var j = i + 1; j < particles.Length; j++)
            {
                var pos = Intersection(particles[i], particles[j]);
                if (pos != null &&
                    InRange(pos.X0) &&
                    InRange(pos.X1) &&
                    InFuture(particles[i], pos) &&
                    InFuture(particles[j], pos)
                )
                {
                    res++;
                }
            }
        }
        return res;

        bool InFuture(Particle2 p, Vec2 pos) => Math.Sign(pos.X0 - p.Pos.X0) == Math.Sign(p.Vel.X0);

        bool InRange(decimal d) => d is >= 2e14m and <= 4e14m;
    }

    public object PartTwo(string input)
    {
        var particles = ParseParticles(input);
        var stoneXy = Solve2D(Project(particles, vec => (vec.X0, vec.X1)));
        var stoneXz = Solve2D(Project(particles, vec => (vec.X0, vec.X2)));
        return Math.Round(stoneXy.X0 + stoneXy.X1 + stoneXz.X1);
    }

    private static Vec2 Solve2D(Particle2[] particles)
    {
        // We try to guess the speed of our stone (a for loop), then supposing 
        // that it is the right velocity we create a new reference frame that 
        // moves with that speed. The stone doesn't move in this frame, it has 
        // some fixed unknown coordinates. Now transform each particle into 
        // this reference frame as well. Since the stone is not moving, if we 
        // properly guessed the speed, we find that each particle meets at the 
        // same point. This must be the stone's location.

        var s = 500; //arbitrary limits for the brute force that worked for me.
        for (var v1 = -s; v1 < s; v1++)
        {
            for (var v2 = -s; v2 < s; v2++)
            {
                var vel = new Vec2(v1, v2);

                // p0 and p1 are linearly independent (for me) => stone != null
                var stone = Intersection(
                    TranslateV(particles[0], vel),
                    TranslateV(particles[1], vel)
                );

                if(stone == null) 
                    continue;

                if (particles.All(p => Hits(TranslateV(p, vel), stone)))
                {
                    return stone;
                }
            }
        }
        throw new Exception();

        Particle2 TranslateV(Particle2 p, Vec2 vel) => new(p.Pos, new Vec2(p.Vel.X0 - vel.X0, p.Vel.X1 - vel.X1));
    }

    private static bool Hits(Particle2 p, Vec2 pos)
    {
        var d = (pos.X0 - p.Pos.X0) * p.Vel.X1 - (pos.X1 - p.Pos.X1) * p.Vel.X0;
        return Math.Abs(d) < (decimal)0.0001;
    }

    private static Vec2? Intersection(Particle2 p1, Particle2 p2)
    {
        // this would look way better if I had a matrix library at my disposal.
        var determinant = p1.Vel.X0 * p2.Vel.X1 - p1.Vel.X1 * p2.Vel.X0;
        if (determinant == 0)
        {
            return null; //particles don't meet
        }

        var b0 = p1.Vel.X0 * p1.Pos.X1 - p1.Vel.X1 * p1.Pos.X0;
        var b1 = p2.Vel.X0 * p2.Pos.X1 - p2.Vel.X1 * p2.Pos.X0;

        return new Vec2(
             (p2.Vel.X0 * b0 - p1.Vel.X0 * b1) / determinant,
             (p2.Vel.X1 * b0 - p1.Vel.X1 * b1) / determinant
         );
    }

    private Particle3[] ParseParticles(string input) => [..
        from line in input.Split('\n')
        let v = ParseNum(line)
        select new Particle3(new(v[0], v[1], v[2]), new(v[3], v[4], v[5]))
    ];

    private decimal[] ParseNum(string l) => [..
        from m in Regex.Matches(l, @"-?\d+") select decimal.Parse(m.Value)
    ];

    // Project particles to a 2D plane:
    private Particle2[] Project(Particle3[] ps, Func<Vec3, (decimal, decimal)> proj) => [..
        from p in ps
        select new Particle2(
            new Vec2(proj(p.Pos).Item1, proj(p.Pos).Item2),
            new Vec2(proj(p.Vel).Item1, proj(p.Vel).Item2)
        )
    ];

    private record Vec2(decimal X0, decimal X1);

    private record Vec3(decimal X0, decimal X1, decimal X2);

    private record Particle2(Vec2 Pos, Vec2 Vel);

    private record Particle3(Vec3 Pos, Vec3 Vel);
}
