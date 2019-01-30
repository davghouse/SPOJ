using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/BUGLIFE/ #dfs #graph-theory
// Determines if a set of bugs can be divided into two non-interacting groups.
public static class BUGLIFE
{
    // Check to see if the ants can be divided into two groups, where members of one group
    // group only interact with members of the other group: AKA check bipartiteness.
    public static bool Solve(SimpleGraph interactionGraph)
        => interactionGraph.IsBipartite();
}

// Undirected, unweighted graph with no loops or multiple edges. The graph's vertices are stored
// in an array, with the ID of a vertex (from 0 to vertexCount - 1) corresponding to its index.
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

    // Performs a DFS from some vertex in every connected component of the graph, while
    // attempting a 2-coloring. Don't need the count property from a hash set, so using
    // two parallel bool arrays, one for discovery, one for 2-coloring.
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
                    // If undiscovered, discover it and color it opposite the vertex we're
                    // visiting from (put it in the other set).
                    if (!discoveredVertexIDs[neighbor.ID])
                    {
                        discoveredVertexIDs[neighbor.ID] = true;
                        discoveredVertexColors[neighbor.ID] = !vertexColor;
                        verticesToVisit.Push(neighbor);
                    }
                    // Else, make sure its color isn't the same as the vertex we're visting
                    // from (verify its not in the same set).
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
        var output = new StringBuilder();
        int testCount = FastIO.ReadNonNegativeInt();
        for (int t = 1; t <= testCount; ++t)
        {
            int bugCount = FastIO.ReadNonNegativeInt();
            int interactionCount = FastIO.ReadNonNegativeInt();
            var interactionGraph = new SimpleGraph(bugCount);

            for (int i = 0; i < interactionCount; ++i)
            {
                interactionGraph.AddEdge(
                    firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                    secondVertexID: FastIO.ReadNonNegativeInt() - 1);
            }

            output.AppendLine($"Scenario #{t}:");
            output.AppendLine(BUGLIFE.Solve(interactionGraph)
                ? "No suspicious bugs found!" : "Suspicious bugs found!");
        }

        Console.Write(output);
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _newLine = (byte)'\n';
    private const byte _minusSign = (byte)'-';
    private const byte _zero = (byte)'0';
    private const int _inputBufferLimit = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

    private static byte ReadByte()
    {
        if (_inputBufferIndex == _inputBufferSize)
        {
            _inputBufferIndex = 0;
            _inputBufferSize = _inputStream.Read(_inputBuffer, 0, _inputBufferLimit);
            if (_inputBufferSize == 0)
                return _null; // All input has been read.
        }

        return _inputBuffer[_inputBufferIndex++];
    }

    public static int ReadNonNegativeInt()
    {
        byte digit;

        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        do
        {
            digit = ReadByte();
        }
        while (digit < _minusSign);

        // Build up the integer from its digits, until we run into whitespace or the null byte.
        int result = digit - _zero;
        while (true)
        {
            digit = ReadByte();
            if (digit < _zero) break;
            result = result * 10 + (digit - _zero);
        }

        return result;
    }
}
