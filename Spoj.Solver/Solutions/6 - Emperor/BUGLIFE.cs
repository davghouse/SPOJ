using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://www.spoj.com/problems/BUGLIFE/ #dfs #graph-theory
// Determines if a set of bugs can be divided into two non-interacting groups.
public static class BUGLIFE
{
    // Best we can do is see if the ants can be divided into two groups, where members of one group
    // group only interact with members of the other group. Same as checking bipartiteness. Not the
    // actual code submitted. Had to strip everything down and have vertices store their own search
    // state but the biggest part was using a List instead of a HashSet to store neighbors (not
    // making use of the HashSet functionality for this problem, and even if removing it leads to
    // creating a multigraph (a pair of ants going on more than one date), doesn't matter). I observed
    // no time difference when swapping search start position from first to last vertex. TLE when
    // using BFS instead of DFS. And this solution gets AC and TLE across different submits so...
    // Here's a sketchy one-off submission that actually passed:
    // https://gist.github.com/davghouse/9e9be6dbaa60037c02bd46730f3e4851
    public static bool Solve(int bugCount, int[,] interactions)
        => SimpleGraph
        .CreateFromOneBasedEdges(bugCount, interactions)
        .IsBipartite();
}

// Undirected, unweighted graph with no loops or multiple edges: http://mathworld.wolfram.com/SimpleGraph.html.
// The graph's vertices are stored in an array and the ID of a vertex (from 0 to vertexCount - 1) corresponds to
// its index in that array.
public sealed class SimpleGraph
{
    public SimpleGraph(int vertexCount)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = Array.AsReadOnly(vertices);
    }

    // For example, edges like (1, 2), (2, 3) => there's an edge between vertices 0 and 1 and 1 and 2.
    public static SimpleGraph CreateFromOneBasedEdges(int vertexCount, int[,] edges)
    {
        var graph = new SimpleGraph(vertexCount);
        for (int i = 0; i < edges.GetLength(0); ++i)
        {
            graph.AddEdge(edges[i, 0] - 1, edges[i, 1] - 1);
        }

        return graph;
    }

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public void AddEdge(int firstVertexID, int secondVertexID)
        => AddEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public void AddEdge(Vertex firstVertex, Vertex secondVertex)
    {
        firstVertex.AddNeighbor(secondVertex);
        secondVertex.AddNeighbor(firstVertex);
    }

    public bool HasEdge(int firstVertexID, int secondVertexID)
        => HasEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
        => firstVertex.HasNeighbor(secondVertex);

    // Performs a DFS from some vertex in every connected component of the graph, while attempting a 2-coloring.
    // Don't need the count property from a hash set, so using two parallel bool arrays, one for discovery, one for 2-coloring.
    public bool IsBipartite()
    {
        bool[] discoveredVertexIDs = new bool[VertexCount];
        bool[] discoveredVertexColors = new bool[VertexCount];
        var verticesToVisit = new Stack<Vertex>();

        for (int i = 0; i < VertexCount; ++i)
        {
            if (discoveredVertexIDs[i])
                continue; // Already explored this component.

            discoveredVertexIDs[i] = true;
            discoveredVertexColors[i] = true;
            verticesToVisit.Push(Vertices[i]);

            while (verticesToVisit.Count > 0)
            {
                var vertex = verticesToVisit.Pop();
                bool vertexColor = discoveredVertexColors[vertex.ID];

                foreach (var neighbor in vertex.Neighbors)
                {
                    // If undiscovered, discover it and color it opposite the vertex we're visiting from (put it in the other set).
                    if (!discoveredVertexIDs[neighbor.ID])
                    {
                        discoveredVertexIDs[neighbor.ID] = true;
                        discoveredVertexColors[neighbor.ID] = !vertexColor;
                        verticesToVisit.Push(neighbor);
                    }
                    // Else, make sure its color isn't the same as the vertex we're visting from (verify its not in the same set).
                    else if (discoveredVertexColors[neighbor.ID] == vertexColor)
                        return false;
                }
            }
        }

        return true;
    }

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly SimpleGraph _graph;
        private readonly HashSet<Vertex> _neighbors = new HashSet<Vertex>();

        internal Vertex(SimpleGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public IEnumerable<Vertex> Neighbors => _neighbors;
        public int Degree => _neighbors.Count;

        internal void AddNeighbor(int neighborID)
            => _neighbors.Add(_graph.Vertices[neighborID]);

        internal void AddNeighbor(Vertex neighbor)
            => _neighbors.Add(neighbor);

        public bool HasNeighbor(int neighborID)
            => _neighbors.Contains(_graph.Vertices[neighborID]);

        public bool HasNeighbor(Vertex neighbor)
            => _neighbors.Contains(neighbor);

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int totalTestCases = int.Parse(Console.ReadLine());
        for (int t = 1; t <= totalTestCases; ++t)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int bugCount = line[0];
            int interactionCount = line[1];

            int[,] interactions = new int[interactionCount, 2];
            for (int i = 0; i < interactionCount; ++i)
            {
                line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                interactions[i, 0] = line[0];
                interactions[i, 1] = line[1];
            }

            output.AppendLine($"Scenario #{t}:");
            output.AppendLine(
                BUGLIFE.Solve(bugCount, interactions) ? "No suspicious bugs found!" : "Suspicious bugs found!");
        }

        Console.Write(output);
    }
}
