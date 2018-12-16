using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// https://www.spoj.com/problems/QTREE/ #divide-and-conquer #graph-theory #hld #lca #segment-tree #tree
// Finds the edge of maximum weight between two vertices in a tree w/ updatable weights.
public sealed class QTREE
{
    private readonly int[,] _edges;
    private readonly WeightedRootedTree _tree;

    public QTREE(int vertexCount, int[,] edges)
    {
        _edges = edges;
        _tree = WeightedRootedTree.CreateFromEdges(vertexCount, rootID: 0, edges: edges);
    }

    public void Change(int edgeIndex, int weight)
    { }

    public int Query(int firstVertexID, int secondVertexID)
        => 1;
}

// Undirected, connected, rooted, weighted graph with no cycles. The tree's vertices are stored
// in an array with the ID of a vertex (from 0 to vertexCount - 1) corresponding to its index.
public sealed class WeightedRootedTree
{
    private WeightedRootedTree(int vertexCount, int rootID)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = vertices;
        Root = vertices[rootID];
    }

    // For example, edges like (0, 1, 99), (1, 2, -3) => there's an edge between vertices 0 and 1 and 1 and 2.
    public static WeightedRootedTree CreateFromEdges(int vertexCount, int rootID, int[,] edges)
    {
        var tree = new WeightedRootedTree(vertexCount, rootID);
        for (int i = 0; i < vertexCount - 1; ++i)
        {
            var firstVertex = tree.Vertices[edges[i, 0]];
            var secondVertex = tree.Vertices[edges[i, 1]];
            int weight = edges[i, 2];
            firstVertex.AddNeighbor(secondVertex, weight);
            secondVertex.AddNeighbor(firstVertex, weight);
        }

        // Now we need to wire up the parent-child relationships. We have to DFS just like
        // InitializeDepthsAndSubtreeSizes, so we might as well initialize depths & subtree sizes too.
        var verticesToVisit = new Stack<Vertex>();
        verticesToVisit.Push(tree.Root);

        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Peek();

            // If Depth is null, it's the first time we're visiting the vertex. Visit its children.
            if (!vertex.Depth.HasValue)
            {
                vertex.Depth = 1 + (vertex.Parent?.Depth ?? -1);

                foreach (var childEdge in vertex.NeighborEdges.Where(ne => ne.Key != vertex.Parent))
                {
                    childEdge.Key.SetParent(vertex, childEdge.Value);
                    verticesToVisit.Push(childEdge.Key);
                }
            }
            // At this point, we know all the children have been visited, so we're good to pop.
            else
            {
                verticesToVisit.Pop();
                vertex.SubtreeSize = 1 + vertex.Children.Sum(c => c.SubtreeSize);
            }
        }

        return tree;
    }

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public Vertex Root { get; }

    public Vertex[] GetEulerTour()
    {
        // For all n - 1 edges, we take the edge down to its child and then, eventually, back up
        // to its parent. So each edge contributes 2 vertices to the tour, and we get the root
        // initially without using any edges, so that's 2*(n - 1) + 1 = 2n - 1 vertices.
        var eulerTour = new Vertex[2 * VertexCount - 1];
        int eulerTourIndex = -1;
        var verticesToVisit = new Stack<Vertex>();
        verticesToVisit.Push(Root);

        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Peek();
            eulerTour[++eulerTourIndex] = vertex;

            // If the EulerTourInitialIndex is null, it's the first time we're visiting the vertex.
            if (!vertex.EulerTourInitialIndex.HasValue)
            {
                vertex.Depth = 1 + (vertex.Parent?.Depth ?? -1);
                vertex.EulerTourInitialIndex = eulerTourIndex;
            }

            // Only pop the vertex once we've visited all its children. At first I was popping
            // no matter what, and then re-pushing for each child, like follows. This degrades
            // performance by about a factor of 2, and is even slower than the recursive solution.
            //        foreach (var child in vertex.Children)
            //        {
            //            verticesToVisit.Push(vertex);
            //            verticesToVisit.Push(child);
            //        }
            if (vertex.EulerTourChildCounter == vertex.Children.Count)
            {
                verticesToVisit.Pop();
            }
            else
            {
                verticesToVisit.Push(vertex.Children[vertex.EulerTourChildCounter++]);
            }
        }

        return eulerTour;
    }

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly WeightedRootedTree _tree;
        private readonly List<KeyValuePair<Vertex, int>> _neighborEdges = new List<KeyValuePair<Vertex, int>>();
        private readonly List<Vertex> _children = new List<Vertex>();

        internal Vertex(WeightedRootedTree tree, int ID)
        {
            _tree = tree;
            this.ID = ID;
        }

        public int ID { get; }

        public Vertex Parent { get; private set; }
        public int Weight { get; private set; } // Weight of the edge to the parent.
        internal IReadOnlyList<KeyValuePair<Vertex, int>> NeighborEdges => _neighborEdges;
        public IReadOnlyList<Vertex> Children => _children;
        public int? Depth { get; internal set; }
        public int? SubtreeSize { get; internal set; }
        public int? EulerTourInitialIndex { get; internal set; }
        internal int EulerTourChildCounter { get; set; }

        internal void AddNeighbor(Vertex neighbor, int weight)
            => _neighborEdges.Add(new KeyValuePair<Vertex, int>(neighbor, weight));

        internal void SetParent(Vertex parent, int weight)
        {
            Parent = parent;
            Parent._children.Add(this);
            Weight = weight;
        }

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;

        public override string ToString()
            => $"{ID}";
    }
}

public static class Program
{
    private static void Main()
    {
        int[,] edges = new int[9999, 3];
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int vertexCount = FastIO.ReadNonNegativeInt();

            for (int i = 0; i < vertexCount - 1; ++i)
            {
                edges[i, 0] = FastIO.ReadNonNegativeInt() - 1; // first vertex ID
                edges[i, 1] = FastIO.ReadNonNegativeInt() - 1; // second vertex ID
                edges[i, 2] = FastIO.ReadNonNegativeInt(); // weight
            }

            var solver = new QTREE(vertexCount, edges);

            char instruction;
            while ((instruction = FastIO.ReadInstruction()) != 'D')
            {
                if (instruction == 'C')
                {
                    solver.Change(
                        edgeIndex: FastIO.ReadNonNegativeInt() - 1,
                        weight: FastIO.ReadNonNegativeInt());
                }
                else
                {
                    FastIO.WriteNonNegativeInt(solver.Query(
                        firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                        secondVertexID: FastIO.ReadNonNegativeInt() - 1));
                    FastIO.WriteLine();
                }
            }
        }

        FastIO.Flush();
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
    private const byte _A = (byte)'A';
    private const int _inputBufferLimit = 8192;
    private const int _outputBufferLimit = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

    private static readonly Stream _outputStream = Console.OpenStandardOutput();
    private static readonly byte[] _outputBuffer = new byte[_outputBufferLimit];
    private static readonly byte[] _digitsBuffer = new byte[11];
    private static int _outputBufferSize = 0;

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

    public static char ReadInstruction()
    {
        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        byte firstInstructionChar;
        do
        {
            firstInstructionChar = ReadByte();
        } while (firstInstructionChar < _minusSign);

        // Consume and discard instruction characters (their ASCII codes are all uppercase).
        byte throwawayInstructionChar;
        do
        {
            throwawayInstructionChar = ReadByte();
        } while (throwawayInstructionChar >= _A);

        return (char)firstInstructionChar;
    }

    public static void WriteNonNegativeInt(int value)
    {
        int digitCount = 0;
        do
        {
            int digit = value % 10;
            _digitsBuffer[digitCount++] = (byte)(digit + _zero);
            value /= 10;
        } while (value > 0);

        if (_outputBufferSize + digitCount > _outputBufferLimit)
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        while (digitCount > 0)
        {
            _outputBuffer[_outputBufferSize++] = _digitsBuffer[--digitCount];
        }
    }

    public static void WriteLine()
    {
        if (_outputBufferSize == _outputBufferLimit) // else _outputBufferSize < _outputBufferLimit.
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        _outputBuffer[_outputBufferSize++] = _newLine;
    }

    public static void Flush()
    {
        _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
        _outputBufferSize = 0;
        _outputStream.Flush();
    }
}
