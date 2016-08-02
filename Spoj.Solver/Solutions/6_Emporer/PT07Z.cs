using System;
using System.Collections.Generic;
using System.Linq;

// 1437 http://www.spoj.com/problems/PT07Z/ Longest path in a tree
// Finds the longest path in a tree.
public static class PT07Z
{
    // See image for details: http://i.imgur.com/hWnw1N9.jpg.
    public static int Solve(int nodeCount, int[,] edges)
    {
        if (nodeCount == 1)
            return 0;

        var graph = SimpleGraph.CreateFromOneBasedEdges(nodeCount, edges);

        // Proof relies on starting at a leaf, but I have a suspicion that's not necessary.
        var firstVertex = graph.Vertices.First(v => v.Degree == 1);
        var secondVertex = graph.FindFurthestVertex(firstVertex).Item1;
        int longestPathLength = graph.FindFurthestVertex(secondVertex).Item2;

        return longestPathLength;
    }
}

// Undirected, unweighted graph with no loops or multiple edges: http://mathworld.wolfram.com/SimpleGraph.html.
// The graph's vertices are stored in an array and the ID of a vertex (from 0 to vertexCount - 1)
// corresponds to its index in said array. Immutable so far but at least mutable edges later on probably.
// Not bothering to throw exceptions in the case where vertices from other graphs are passed in.
public class SimpleGraph
{
    protected readonly Vertex[] _vertices;

    protected SimpleGraph(int vertexCount)
    {
        _vertices = new Vertex[vertexCount];
    }

    // For example, edges like (0, 1), (1, 2) => there's an edge between vertices 0 and 1 and 1 and 2.
    public static SimpleGraph CreateFromZeroBasedEdges(int vertexCount, int[,] edges)
    {
        var graph = new SimpleGraph(vertexCount);

        for (int id = 0; id < vertexCount; ++id)
        {
            graph._vertices[id] = new Vertex(graph, id);
        }

        for (int i = 0; i < edges.GetLength(0); ++i)
        {
            graph.AddEdge(edges[i, 0], edges[i, 1]);
        }

        return graph;
    }

    // For example, edges like (1, 2), (2, 3) => there's an edge between vertices 0 and 1 and 1 and 2.
    public static SimpleGraph CreateFromOneBasedEdges(int vertexCount, int[,] edges)
    {
        var graph = new SimpleGraph(vertexCount);

        for (int id = 0; id < vertexCount; ++id)
        {
            graph._vertices[id] = new Vertex(graph, id);
        }

        for (int i = 0; i < edges.GetLength(0); ++i)
        {
            graph.AddEdge(edges[i, 0] - 1, edges[i, 1] - 1);
        }

        return graph;
    }

    public int VertexCount
        => _vertices.Length;

    public IReadOnlyList<Vertex> Vertices
        => Array.AsReadOnly(_vertices);

    protected void AddEdge(int firstVertexID, int secondVertexID)
        => AddEdge(_vertices[firstVertexID], _vertices[secondVertexID]);

    protected void AddEdge(Vertex firstVertex, Vertex secondVertex)
    {
        firstVertex.AddNeighbor(secondVertex);
        secondVertex.AddNeighbor(firstVertex);
    }

    public bool HasEdge(int firstVertexID, int secondVertexID)
        => HasEdge(_vertices[firstVertexID], _vertices[secondVertexID]);

    public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
        => firstVertex.HasNeighbor(secondVertex);

    // This performs a DFS from an arbitrary start vertex, to determine if the whole graph is reachable from it.
    public bool IsConnected()
    {
        var arbitraryStartVertex = _vertices[VertexCount / 2];
        var discoveredVertexIDs = new HashSet<int> { arbitraryStartVertex.ID };
        var verticesToVisit = new Stack<Vertex>();
        verticesToVisit.Push(arbitraryStartVertex);

        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Pop();

            foreach (var neighbor in vertex.Neighbors)
            {
                bool neighborWasDiscoveredForTheFirstTime = discoveredVertexIDs.Add(neighbor.ID);
                if (neighborWasDiscoveredForTheFirstTime)
                {
                    verticesToVisit.Push(neighbor);
                }
            }
        }

        return discoveredVertexIDs.Count == VertexCount;
    }

    public Tuple<Vertex, int> FindFurthestVertex(int startVertexID)
        => FindFurthestVertex(_vertices[startVertexID]);

    // This finds a furthest vertex from the start vertex, that is, a vertex whose (shortest) path
    // to the start is the longest, and returns that vertex and its distance (path length) from the start.
    public Tuple<Vertex, int> FindFurthestVertex(Vertex startVertex)
    {
        var discoveredVertexIDs = new HashSet<int> { startVertex.ID };
        var verticesToVisit = new Queue<Vertex>();
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
                    bool neighborWasDiscoveredForTheFirstTime = discoveredVertexIDs.Add(neighbor.ID);
                    if (neighborWasDiscoveredForTheFirstTime)
                    {
                        verticesToVisit.Enqueue(neighbor);
                    }
                }
            }
        }

        return Tuple.Create(furthestVertex, furthestDistance);
    }

    public class Vertex
    {
        protected readonly SimpleGraph _graph;
        protected readonly HashSet<Vertex> _neighbors = new HashSet<Vertex>();

        protected internal Vertex(SimpleGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public int Degree
            => _neighbors.Count;

        public IEnumerable<Vertex> Neighbors
            => _neighbors.Skip(0);

        protected internal void AddNeighbor(int neighborID)
            => AddNeighbor(_graph._vertices[neighborID]);

        protected internal void AddNeighbor(Vertex neighbor)
            => _neighbors.Add(neighbor);

        public bool HasNeighbor(int neighborID)
            => HasNeighbor(_graph._vertices[neighborID]);

        public bool HasNeighbor(Vertex neighbor)
            => _neighbors.Contains(neighbor);
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

        Console.WriteLine(PT07Z.Solve(nodeCount, edges));
    }
}
