using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/NAKANJ/ #bfs
// Finds the minimum number of knight moves from one chess square to another.
public static class NAKANJ
{
    private static readonly SimpleGraph _knightMoveGraph;
    private static readonly Tuple<int, int>[] _knightMoveTransformations = new[]
    {
        Tuple.Create(-1, -2), Tuple.Create(-2, -1), /* top left of diagram */
        Tuple.Create(-2, 1), Tuple.Create(-1, 2), /* top right of diagram */
        Tuple.Create(1, 2), Tuple.Create(2, 1), /* bottom right of diagram */
        Tuple.Create(2, -1), Tuple.Create(1, -2) /* bottom left of diagram */
    };

    static NAKANJ()
    {
        _knightMoveGraph = new SimpleGraph(vertexCount: 64);

        // The problem's chessboard has rows from 8 to 1 and columns from a to h,
        // starting in the upper left corner. This one will have rows from 0 to 7
        // and columns from 0 to 7, starting in the upper left hand corner.
        for (int r = 0; r < 8; ++r)
        {
            for (int c = 0; c < 8; ++c)
            {
                int thisVertexID = GetVertexID(r, c);

                foreach (var knightMoveTransformation in _knightMoveTransformations)
                {
                    int rowTransformation = knightMoveTransformation.Item1;
                    int columnTransformation = knightMoveTransformation.Item2;

                    int movedToVertexID;
                    if (TryGetVertexID(r + rowTransformation, c + columnTransformation, out movedToVertexID))
                    {
                        _knightMoveGraph.AddEdge(thisVertexID, movedToVertexID);
                    }
                }
            }
        }
    }

    private static int GetVertexID(int r, int c)
        => r * 8 + c;

    private static bool TryGetVertexID(int r, int c, out int vertexID)
    {
        vertexID = GetVertexID(r, c);
        return r >= 0 && r < 8 && c >= 0 && c < 8;
    }

    private static int GetVertexID(string square)
    {
        int r = '8' - square[1];
        int c = square[0] - 'a';
        return GetVertexID(r, c);
    }

    public static int Solve(string startSquare, string endSquare)
        => _knightMoveGraph.GetShortestPathLength(
            startVertexID: GetVertexID(startSquare),
            endVertexID: GetVertexID(endSquare));
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

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public void AddEdge(int firstVertexID, int secondVertexID)
        => AddEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public void AddEdge(Vertex firstVertex, Vertex secondVertex)
    {
        firstVertex.AddNeighbor(secondVertex);
        secondVertex.AddNeighbor(firstVertex);
    }

    public int GetShortestPathLength(int startVertexID, int endVertexID)
        => GetShortestPathLength(Vertices[startVertexID], Vertices[endVertexID]);

    public int GetShortestPathLength(Vertex startVertex, Vertex endVertex)
    {
        if (startVertex == endVertex) return 0;

        bool[] discoveredVertexIDs = new bool[VertexCount];
        var verticesToVisit = new Queue<Vertex>();
        discoveredVertexIDs[startVertex.ID] = true;
        verticesToVisit.Enqueue(startVertex);

        int distance = 1;

        // We visit vertices in waves, where all vertices in the same wave are the same distance
        // from the start vertex, which BFS makes convenient. This allows us to avoid storing
        // distances to the start vertex at the level of individual vertices. To save work we
        // don't check the wave vertices for endVertex equality, but rather their neighbors.
        // So that's why the distance starts off as one rather than zero.
        while (verticesToVisit.Count > 0)
        {
            int waveSize = verticesToVisit.Count;
            for (int i = 0; i < waveSize; ++i)
            {
                var vertex = verticesToVisit.Dequeue();

                foreach (var neighbor in vertex.Neighbors)
                {
                    if (!discoveredVertexIDs[neighbor.ID]) 
                    {
                        if (neighbor == endVertex)
                            return distance;

                        discoveredVertexIDs[neighbor.ID] = true;
                        verticesToVisit.Enqueue(neighbor);
                    }
                }
            }
            ++distance;
        }

        return -1;
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

        public IReadOnlyCollection<Vertex> Neighbors => _neighbors;
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
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();

            Console.WriteLine(
                NAKANJ.Solve(line[0], line[1]));
        }
    }
}
