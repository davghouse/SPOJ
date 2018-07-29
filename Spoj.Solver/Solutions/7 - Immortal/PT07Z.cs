using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/PT07Z/ #bfs #graph-theory #longest-path #proof #tree
// Finds the longest path in a tree.
public static class PT07Z
{
    // See image for details: http://i.imgur.com/hWnw1N9.jpg.
    public static int Solve(int nodeCount, int[,] edges)
    {
        if (nodeCount == 1)
            return 0;

        SimpleGraph graph = SimpleGraph.CreateFromOneBasedEdges(nodeCount, edges);

        var firstVertex = graph.Vertices.First(v => v.Degree == 1);
        var secondVertex = graph.FindFurthestVertex(firstVertex).Item1;
        int longestPathLength = graph.FindFurthestVertex(secondVertex).Item2;

        return longestPathLength;
    }
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

        Vertices = vertices;
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

    public Tuple<Vertex, int> FindFurthestVertex(int startVertexID)
        => FindFurthestVertex(Vertices[startVertexID]);

    // This finds a furthest vertex from the start vertex, that is, a vertex whose (shortest) path
    // to the start is the longest, and returns that vertex and its distance (path length) from the start.
    public Tuple<Vertex, int> FindFurthestVertex(Vertex startVertex)
    {
        bool[] discoveredVertexIDs = new bool[VertexCount];
        var verticesToVisit = new Queue<Vertex>();
        discoveredVertexIDs[startVertex.ID] = true;
        verticesToVisit.Enqueue(startVertex);

        Vertex furthestVertex = null;
        int furthestDistance = -1;

        // We visit vertices in waves, where all vertices in the same wave are the same distance
        // from the start vertex, which BFS makes convenient. This allows us to avoid storing
        // distances to the start vertex at the level of individual vertices.
        while (verticesToVisit.Count > 0)
        {
            // We don't care which furthest vertex we get from this wave, so we just choose the first.
            furthestVertex = verticesToVisit.Peek();
            ++furthestDistance;

            int waveSize = verticesToVisit.Count;
            for (int i = 0; i < waveSize; ++i)
            {
                var vertex = verticesToVisit.Dequeue();

                foreach (var neighbor in vertex.Neighbors)
                {
                    if (!discoveredVertexIDs[neighbor.ID]) 
                    {
                        discoveredVertexIDs[neighbor.ID] = true;
                        verticesToVisit.Enqueue(neighbor);
                    }
                }
            }
        }

        return Tuple.Create(furthestVertex, furthestDistance);
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
        int nodeCount = int.Parse(Console.ReadLine());
        int edgeCount = nodeCount - 1;

        int[,] edges = new int[edgeCount, 2];
        for (int i = 0; i < edgeCount; ++i)
        {
            int[] edge = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            edges[i, 0] = edge[0];
            edges[i, 1] = edge[1];
        }

        Console.WriteLine(
            PT07Z.Solve(nodeCount, edges));
    }
}
