using AdventOfCode.Lib;

namespace AdventOfCode._2022.Day08;

[ProblemName("Treetop Tree House")]
public class Solution : ISolver
{
    private static readonly Direction Left = new(0, -1);
    private static readonly Direction Right = new(0, 1);
    private static readonly Direction Up = new(-1, 0);
    private static readonly Direction Down = new(1, 0);

    public object PartOne(string input)
    {
        var forest = Parse(input);

        return forest.Trees().Count(tree =>
            forest.IsTallest(tree, Left) || forest.IsTallest(tree, Right) ||
            forest.IsTallest(tree, Up) || forest.IsTallest(tree, Down)
        );
    }

    public object PartTwo(string input)
    {
        var forest = Parse(input);

        return forest.Trees().Select(tree =>
            forest.ViewDistance(tree, Left) * forest.ViewDistance(tree, Right) *
            forest.ViewDistance(tree, Up) * forest.ViewDistance(tree, Down)
        ).Max();
    }

    private static Forest Parse(string input)
    {
        var items = input.Split("\n");
        var (col, row) = (items[0].Length, items.Length);
        return new Forest(items, row, col);
    }

}


internal record Direction(int DirRow, int DirCol);
internal record Tree(int Height, int TreeRow, int TreeCol);

internal record Forest(string[] Items, int ForestRow, int ForestCol)
{

    public IEnumerable<Tree> Trees() =>
        from row in Enumerable.Range(0, ForestRow)
        from col in Enumerable.Range(0, ForestCol)
        select new Tree(Items[row][col], row, col);

    public int ViewDistance(Tree tree, Direction dir) =>
        IsTallest(tree, dir) ? TreesInDirection(tree, dir).Count()
            : SmallerTrees(tree, dir).Count() + 1;

    public bool IsTallest(Tree tree, Direction dir) =>
        TreesInDirection(tree, dir).All(treeT => treeT.Height < tree.Height);

    public IEnumerable<Tree> SmallerTrees(Tree tree, Direction dir) =>
        TreesInDirection(tree, dir).TakeWhile(treeT => treeT.Height < tree.Height);

    public IEnumerable<Tree> TreesInDirection(Tree tree, Direction dir)
    {
        var (first, row, col) = (true, row: tree.TreeRow, col: tree.TreeCol);
        while (row >= 0 && row < ForestRow && col >= 0 && col < ForestCol)
        {
            if (!first)
            {
                yield return new Tree(Height: Items[row][col], TreeRow: row, TreeCol: col);
            }
            (first, row, col) = (false, row + dir.DirRow, col + dir.DirCol);
        }
    }
}