using System.Text;
using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day22;

[ProblemName("Grid Computing")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var nodes = ParseNodes(input);
        var viablePairs = 0;

        foreach (var nodeA in nodes)
        {
            if (nodeA.Used > 0)
            {
                foreach (var nodeB in nodes)
                {
                    var isDifferentNode = nodeA.Row != nodeB.Row || nodeA.Col != nodeB.Col;
                    var canAccommodate = nodeB.Available >= nodeA.Used;

                    if (isDifferentNode && canAccommodate)
                    {
                        viablePairs++;
                    }
                }
            }
        }

        return viablePairs;
    }

    public object PartTwo(string input)
    {
        var nodes = ParseNodes(input);
        var grid = new Grid(nodes);

        // Move the empty node to the top row
        while (grid.EmptyRow != 0)
        {
            if (!grid.IsWall(grid.EmptyRow - 1, grid.EmptyCol))
            {
                grid.MoveEmpty(-1, 0);
            }
            else
            {
                grid.MoveEmpty(0, -1);
            }
        }

        // Move the empty node to the rightmost column
        while (grid.EmptyCol != grid.TotalCols - 1)
        {
            grid.MoveEmpty(0, 1);
        }

        // Move the goal data to the target position
        while (!nodes[0, 0].IsGoal)
        {
            grid.MoveEmpty(1, 0);
            grid.MoveEmpty(0, -1);
            grid.MoveEmpty(0, -1);
            grid.MoveEmpty(-1, 0);
            grid.MoveEmpty(0, 1);
        }

        return grid.MoveCount;
    }

    private static Node[,] ParseNodes(string input)
    {
        var nodeLines = input.Split('\n')
            .Skip(2);

        var nodeList = nodeLines.Select(
                line =>
                {
                    var numbers = Regex.Matches(line, @"(\d+)")
                        .Select(m => int.Parse(m.Groups[1].Value))
                        .ToArray();

                    return new Node
                    {
                        Row = numbers[1],
                        Col = numbers[0],
                        Size = numbers[2],
                        Used = numbers[3]
                    };
                })
            .ToArray();

        var maxRow = nodeList.Max(node => node.Row) + 1;
        var maxCol = nodeList.Max(node => node.Col) + 1;
        var grid = new Node[maxRow, maxCol];

        foreach (var node in nodeList)
        {
            grid[node.Row, node.Col] = node;
        }

        // Mark the goal node
        grid[0, maxCol - 1].IsGoal = true;

        return grid;
    }

    private class Grid
    {
        public int EmptyRow { get; private set; }

        public int EmptyCol { get; private set; }

        private Node[,] Nodes { get; }

        public int MoveCount { get; private set; }

        private int TotalRows => Nodes.GetLength(0);

        public int TotalCols => Nodes.GetLength(1);

        public Grid(Node[,] nodes)
        {
            Nodes = nodes;
            FindEmptyNode();
        }

        private void FindEmptyNode()
        {
            foreach (var node in Nodes)
            {
                if (node.Used == 0)
                {
                    EmptyRow = node.Row;
                    EmptyCol = node.Col;

                    break;
                }
            }
        }

        public bool IsWall(int row, int col) => Nodes[row, col].Used > Nodes[EmptyRow, EmptyCol].Size;

        public void MoveEmpty(int deltaRow, int deltaCol)
        {
            if (Math.Abs(deltaRow) + Math.Abs(deltaCol) != 1)
            {
                throw new ArgumentException("Invalid move. Must move one step in either row or column.");
            }

            var targetRow = EmptyRow + deltaRow;
            var targetCol = EmptyCol + deltaCol;

            // Validate boundaries
            if (targetRow < 0 || targetRow >= TotalRows || targetCol < 0 || targetCol >= TotalCols)
            {
                throw new InvalidOperationException("Move out of grid boundaries.");
            }

            var targetNode = Nodes[targetRow, targetCol];
            var emptyNode = Nodes[EmptyRow, EmptyCol];

            // Check if the move is possible
            if (targetNode.Used > emptyNode.Size)
            {
                throw new InvalidOperationException("Not enough space to move the data.");
            }

            // Perform the move
            emptyNode.Used = targetNode.Used;
            emptyNode.IsGoal = targetNode.IsGoal;

            // Update empty node position
            EmptyRow = targetRow;
            EmptyCol = targetCol;
            targetNode.Used = 0;
            targetNode.IsGoal = false;

            MoveCount++;
        }

        // Optional: Method to visualize the grid (for debugging)
        public void DisplayGrid()
        {
            var sb = new StringBuilder();

            for (var row = 0; row < TotalRows; row++)
            {
                for (var col = 0; col < TotalCols; col++)
                {
                    if (Nodes[row, col].IsGoal)
                    {
                        sb.Append("G");
                    }
                    else if (row == 0 && col == 0)
                    {
                        sb.Append("x");
                    }
                    else if (Nodes[row, col].Used == 0)
                    {
                        sb.Append("E");
                    }
                    else if (IsWall(row, col))
                    {
                        sb.Append("#");
                    }
                    else
                    {
                        sb.Append(".");
                    }
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }

    private class Node
    {
        public bool IsGoal { get; set; }

        public int Row { get; init; }

        public int Col { get; init; }

        public int Size { get; init; }

        public int Used { get; set; }

        public int Available => Size - Used;
    }
}
