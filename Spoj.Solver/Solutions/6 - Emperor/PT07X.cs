using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vertex = SimpleGraph.Vertex;

// https://www.spoj.com/problems/PT07X/ #graph-theory #greedy
// Finds the vertex set of minimum size covering all edges in a tree.
public static class PT07X
{
    // The only way to cover a leaf's edge is by choosing the leaf, or by choosing
    // its parent. Choosing its parent is better since that'll cover other edges
    // as well. This solution chooses leaf parents, and removes from the tree any
    // edges the leaf parent covers. As this removal process happens, new leaves
    // get created and set aside to work on. Once there are no more leaves left,
    // we're done. We know this works because assume we missed part of the tree. The
    // part is also a tree, so it has a leaf. This leaf wasn't one of the initial leaves,
    // otherwise we would've seen it already. So to be a leaf now it must've had an
    // edge removed, so it must've been a neighbor of one of the parent leaves,
    // so we must've seen it, contradiction.
    public static int Solve(SimpleGraph tree)
    {
        var leaves = new HashSet<Vertex>(tree.Vertices.Where(v => v.Degree == 1));
        int vertexCoverSize = 0;
        while (leaves.Any())
        {
            var leaf = leaves.First();
            var parentLeaf = leaf.Neighbors.First();

            var parentLeafNeighbors = parentLeaf.Neighbors.ToArray();
            foreach (var parentLeafNeighor in parentLeafNeighbors)
            {
                tree.RemoveEdge(parentLeaf, parentLeafNeighor);

                if (parentLeafNeighor.Degree == 1)
                {
                    leaves.Add(parentLeafNeighor);
                }
                else if (parentLeafNeighor.Degree == 0)
                {
                    leaves.Remove(parentLeafNeighor);
                }
            }

            // Parent may have been a leaf, and its degree is 0 now, so try removing.
            leaves.Remove(parentLeaf); 

            ++vertexCoverSize;
        }

        return vertexCoverSize;
    }
}

// Undirected, unweighted graph with no loops or multiple edges. The graph's vertices are
// stored in an array and the ID of a vertex (from 0 to vertexCount - 1) corresponds to its
// index in that array.
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

    public void RemoveEdge(int firstVertexID, int secondVertexID)
        => RemoveEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public void RemoveEdge(Vertex firstVertex, Vertex secondVertex)
    {
        firstVertex.RemoveNeighbor(secondVertex);
        secondVertex.RemoveNeighbor(firstVertex);
    }

    public bool HasEdge(int firstVertexID, int secondVertexID)
        => HasEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
        => firstVertex.HasNeighbor(secondVertex);

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

        internal void RemoveNeighbor(int neighborID)
            => _neighbors.Remove(_graph.Vertices[neighborID]);

        internal void RemoveNeighbor(Vertex neighbor)
            => _neighbors.Remove(neighbor);

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
        int treeSize = FastIO.ReadNonNegativeInt();
        var tree = new SimpleGraph(treeSize);
        for (int e = 0; e < treeSize - 1; ++e)
        {
            int firstVertexID = FastIO.ReadNonNegativeInt() - 1;
            int secondVertexID = FastIO.ReadNonNegativeInt() - 1;
            tree.AddEdge(firstVertexID, secondVertexID);
        }

        Console.Write(PT07X.Solve(tree));
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but seems like large input.
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _newLine = (byte)'\n';
    private const byte _minusSign = (byte)'-';
    private const byte _zero = (byte)'0';
    private const int _inputBufferLimit = 8192;
    private const int _outputBufferLimit = 8192;

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
