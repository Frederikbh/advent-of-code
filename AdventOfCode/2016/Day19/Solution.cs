using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day19;

[ProblemName("An Elephant Named Joseph")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var elves = Elves(int.Parse(input));

        return Solve(
            elves[0],
            elves[1],
            elves.Length,
            (elfVictim, count) => elfVictim.Next.Next);
    }

    public object PartTwo(string input)
    {
        var elves = Elves(int.Parse(input));

        return Solve(
            elves[0],
            elves[elves.Length / 2],
            elves.Length,
            (elfVictim, count) => count % 2 == 1
                ? elfVictim.Next
                : elfVictim.Next.Next);
    }

    private static int Solve(Elf elf, Elf elfVictim, int elfCount, Func<Elf, int, Elf> nextVictim)
    {
        while (elfCount > 1)
        {
            elfVictim.Prev.Next = elfVictim.Next;
            elfVictim.Next.Prev = elfVictim.Prev;
            elf = elf.Next;
            elfCount--;
            elfVictim = nextVictim(elfVictim, elfCount);
        }

        return elf.Id;
    }

    private static Elf[] Elves(int count)
    {
        var elves = Enumerable.Range(0, count)
            .Select(x => new Elf { Id = x + 1 })
            .ToArray();

        for (var i = 0; i < count; i++)
        {
            elves[i].Prev = elves[(i - 1 + count) % count];
            elves[i].Next = elves[(i + 1) % count];
        }

        return elves;
    }

    private class Elf
    {
        public int Id;
        public Elf Prev;
        public Elf Next;
    }
}
