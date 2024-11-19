using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day25;

[ProblemName("Snowverload")]
public class Solution : ISolver 
{
    public object PartOne(string input) 
    {
        var r = new Random(25);

        // run Karger's algorithm until it finds a cut with 3 edges
        var (cutSize, c1, c2) = FindCut(input, r);
        while (cutSize != 3)
        {
            (cutSize, c1, c2) = FindCut(input, r);
        }
        return c1 * c2;
    }

    private static (int size, int c1, int c2) FindCut(string input, Random r)
    {
        var graph = Parse(input);
        var componentSize = graph.Keys.ToDictionary(k => k, _ => 1);

        for (var id = 0; graph.Count > 2; id++)
        {
            // decrease the the number of nodes by one. First select two nodes u 
            // and v connected with an edge. Introduce a new node that inherits 
            // every edge going out of these (excluding the edges between them). 
            // Set the new nodes' component size to the sum of the component 
            // sizes of u and v. Remove u and v from the graph.
            var u = graph.Keys.ElementAt(r.Next(graph.Count));
            var v = graph[u][r.Next(graph[u].Count)];

            var merged = "merge-" + id;
            graph[merged] = [
                .. from n in graph[u] where n != v select n,
                .. from n in graph[v] where n != u select n
            ];
            Rebind(u, merged);
            Rebind(v, merged);

            componentSize[merged] = componentSize[u] + componentSize[v];

            graph.Remove(u);
            graph.Remove(v);
        }

        // two nodes remain with some edges between them, the number of those 
        // edges equals to the size of the cut. Component size tells the number 
        // of nodes in the two sides created by the cut.
        var nodeA = graph.Keys.First();
        var nodeB = graph.Keys.Last();
        return (graph[nodeA].Count(), componentSize[nodeA], componentSize[nodeB]);

        // updates backreferences of oldNode to point to newNode
        void Rebind(string oldNode, string newNode)
        {
            foreach (var n in graph[oldNode])
            {
                while (graph[n]
                       .Remove(oldNode))
                {
                    graph[n]
                        .Add(newNode);
                }
            }
        }
    }

    private static Dictionary<string, List<string>> Parse(string input)
    {
        var graph = new Dictionary<string, List<string>>();

        foreach (var line in input.Split('\n'))
        {
            var parts = line.Split(": ");
            var u = parts[0];
            var nodes = parts[1].Split(' ');
            foreach (var v in nodes)
            {
                RegisterEdge(u, v);
                RegisterEdge(v, u);
            }
        }
        return graph;

        void RegisterEdge(string u, string v)
        {
            if (!graph.ContainsKey(u))
            {
                graph[u] = new List<string>();
            }

            graph[u].Add(v);
        }
    }
}
