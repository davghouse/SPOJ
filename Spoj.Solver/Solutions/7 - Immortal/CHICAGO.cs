using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vertex = WeightedSimpleGraph.Vertex;

// https://www.spoj.com/problems/CHICAGO/ #dijkstras #graph-theory #greedy #heap #shortest-path
// Finds the path to Chicago with the highest probability of safety.
public static class CHICAGO
{
    // This uses Dijkstra's algorithm: https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm.
    // We return immediately upon visiting the destination intersection, and we don't initialize
    // the heap with all intersections. We only add intersections to the heap when reaching one
    // of their neighbor intersections. Without a pre-filled heap to rely on, we track what
    // intersections have been visited using an array of bools.
    public static double Solve(WeightedSimpleGraph intersectionGraph)
    {
        var startIntersection = intersectionGraph.Vertices.First();
        var endIntersection = intersectionGraph.Vertices.Last();

        var pathSafetyProbabilities = new BinaryHeap(startIntersection, topValue: 1);
        bool[] visitedIntersections = new bool[intersectionGraph.VertexCount];

        while (!pathSafetyProbabilities.IsEmpty)
        {
            // Instead of choosing the closest neighbor, we choose the safest (highest probability).
            var safestPath = pathSafetyProbabilities.Extract();
            var intersection = safestPath.Key;
            double safetyProbabilityToIntersection = safestPath.Value;

            if (intersection == endIntersection)
                return safetyProbabilityToIntersection * 100;

            foreach (var neighbor in intersection.Neighbors.Where(n => !visitedIntersections[n.ID]))
            {
                double safetyProbabilityToNeighborThroughIntersection = safetyProbabilityToIntersection
                    * (intersection.GetEdgeWeight(neighbor) / (double)100);
                double currentSafetyProbabilityToNeighbor;

                // We know the neighboring intersection hasn't been visited yet, so we need to maintain
                // its safety probability in the heap. If it's already in the heap, see if a safer path
                // exists to it through the intersection we're visiting. If it isn't in the heap yet, add it.
                if (pathSafetyProbabilities.TryGetValue(neighbor, out currentSafetyProbabilityToNeighbor))
                {
                    if (safetyProbabilityToNeighborThroughIntersection > currentSafetyProbabilityToNeighbor)
                    {
                        pathSafetyProbabilities.Update(neighbor, safetyProbabilityToNeighborThroughIntersection);
                    }
                }
                else
                {
                    pathSafetyProbabilities.Add(neighbor, safetyProbabilityToNeighborThroughIntersection);
                }
            }

            visitedIntersections[intersection.ID] = true;
        }

        throw new NotSupportedException();
    }
}

// Undirected, weighted graph with no loops or multiple edges. The graph's vertices are stored
// in an array, with the ID of a vertex (from 0 to vertexCount - 1) corresponding to its index.
public sealed class WeightedSimpleGraph
{
    public WeightedSimpleGraph(int vertexCount)
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

    public void AddEdge(int firstVertexID, int secondVertexID, int weight)
        => AddEdge(Vertices[firstVertexID], Vertices[secondVertexID], weight);

    public void AddEdge(Vertex firstVertex, Vertex secondVertex, int weight)
    {
        firstVertex.AddNeighbor(secondVertex, weight);
        secondVertex.AddNeighbor(firstVertex, weight);
    }

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly WeightedSimpleGraph _graph;
        private readonly Dictionary<Vertex, int> _edges = new Dictionary<Vertex, int>();

        internal Vertex(WeightedSimpleGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public IReadOnlyCollection<Vertex> Neighbors => _edges.Keys;
        public int Degree => _edges.Count;

        internal void AddNeighbor(Vertex neighbor, int weight)
            => _edges.Add(neighbor, weight);

        public bool HasNeighbor(Vertex neighbor)
            => _edges.ContainsKey(neighbor);

        public int GetEdgeWeight(Vertex neighbor)
            => _edges[neighbor];

        public bool TryGetEdgeWeight(Vertex neighbor, out int edgeWeight)
            => _edges.TryGetValue(neighbor, out edgeWeight);

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;
    }
}

public sealed class BinaryHeap
{
    private readonly List<KeyValuePair<Vertex, double>> _keyValuePairs = new List<KeyValuePair<Vertex, double>>();
    private readonly Dictionary<Vertex, int> _keyIndices = new Dictionary<Vertex, int>();

    public BinaryHeap(Vertex topKey, double topValue = 0)
    {
        _keyValuePairs.Add(new KeyValuePair<Vertex, double>(topKey, topValue));
        _keyIndices.Add(topKey, 0);
    }

    public int Size => _keyValuePairs.Count;
    public bool IsEmpty => Size == 0;
    public KeyValuePair<Vertex, double> Top => _keyValuePairs[0];

    public void Add(Vertex key, double value)
        => Add(new KeyValuePair<Vertex, double>(key, value));

    public void Add(KeyValuePair<Vertex, double> keyValuePair)
    {
        _keyValuePairs.Add(keyValuePair);
        _keyIndices.Add(keyValuePair.Key, _keyValuePairs.Count - 1);
        SiftUp(_keyValuePairs.Count - 1, keyValuePair);
    }

    public KeyValuePair<Vertex, double> Extract()
    {
        var top = _keyValuePairs[0];
        _keyIndices.Remove(top.Key);

        if (_keyValuePairs.Count == 1)
        {
            _keyValuePairs.RemoveAt(0);
        }
        else
        {
            var bottom = _keyValuePairs[_keyValuePairs.Count - 1];
            _keyValuePairs.RemoveAt(_keyValuePairs.Count - 1);
            _keyValuePairs[0] = bottom;
            _keyIndices[bottom.Key] = 0;
            SiftDown(0, bottom);
        }

        return top;
    }

    public bool Contains(Vertex key)
        => _keyIndices.ContainsKey(key);

    public double GetValue(Vertex key)
        => _keyValuePairs[_keyIndices[key]].Value;

    public bool TryGetValue(Vertex key, out double value)
    {
        int keyIndex;
        if (_keyIndices.TryGetValue(key, out keyIndex))
        {
            value = _keyValuePairs[keyIndex].Value;
            return true;
        }

        value = default(int);
        return false;
    }

    public double Update(Vertex key, double value)
        => Update(new KeyValuePair<Vertex, double>(key, value));

    public double Update(KeyValuePair<Vertex, double> keyValuePair)
    {
        int index = _keyIndices[keyValuePair.Key];
        double oldValue = _keyValuePairs[index].Value;
        _keyValuePairs[index] = keyValuePair;

        // If the old value was less than the updated value, try sifting the updated value up.
        if (oldValue < keyValuePair.Value)
        {
            SiftUp(index, keyValuePair);
        }
        else
        {
            SiftDown(index, keyValuePair);
        }

        return oldValue;
    }

    private void SiftUp(int index, KeyValuePair<Vertex, double> keyValuePair)
    {
        // Stop if we don't have a parent to sift up to.
        if (index == 0) return;

        int parentIndex = (index - 1) / 2;
        var parentKeyValuePair = _keyValuePairs[parentIndex];

        // If the parent is less, push the parent down and the value up--large rises to the
        // top. We know this is okay (aka heap-preserving) because parent was larger than the
        // other child, as only one child gets out of order at a time. So both are less than value.
        if (parentKeyValuePair.Value < keyValuePair.Value)
        {
            _keyValuePairs[index] = parentKeyValuePair;
            _keyIndices[parentKeyValuePair.Key] = index;
            _keyValuePairs[parentIndex] = keyValuePair;
            _keyIndices[keyValuePair.Key] = parentIndex;
            SiftUp(parentIndex, keyValuePair);
        }
    }

    private void SiftDown(int index, KeyValuePair<Vertex, double> keyValuePair)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;

        // If both children exist...
        if (rightChildIndex < _keyValuePairs.Count)
        {
            var leftChildKeyValuePair = _keyValuePairs[leftChildIndex];
            var rightChildKeyValuePair = _keyValuePairs[rightChildIndex];

            // If the left child is larger than the right child (so left can move above right)...
            if (leftChildKeyValuePair.Value > rightChildKeyValuePair.Value)
            {
                // And the value is less than its left child, push the left child up and
                // the value down--small falls to the bottom.
                if (keyValuePair.Value < leftChildKeyValuePair.Value)
                {
                    _keyValuePairs[index] = leftChildKeyValuePair;
                    _keyIndices[leftChildKeyValuePair.Key] = index;
                    _keyValuePairs[leftChildIndex] = keyValuePair;
                    _keyIndices[keyValuePair.Key] = leftChildIndex;
                    SiftDown(leftChildIndex, keyValuePair);
                }
            }
            // If the right child is larger or the same as the left child (so right can move above left)...
            else
            {
                // And the value is less than its right child, push the right child up and
                // the value down--small falls to the bottom.
                if (keyValuePair.Value < rightChildKeyValuePair.Value)
                {
                    _keyValuePairs[index] = rightChildKeyValuePair;
                    _keyIndices[rightChildKeyValuePair.Key] = index;
                    _keyValuePairs[rightChildIndex] = keyValuePair;
                    _keyIndices[keyValuePair.Key] = rightChildIndex;
                    SiftDown(rightChildIndex, keyValuePair);
                }
            }
        }
        // If only the left child exists (and therefore the left child is the last value)...
        else if (leftChildIndex < _keyValuePairs.Count)
        {
            var leftChildKeyValuePair = _keyValuePairs[leftChildIndex];

            // And the value is less than its left child, push the left child up and
            // the value down--small falls to the bottom.
            if (keyValuePair.Value < leftChildKeyValuePair.Value)
            {
                _keyValuePairs[index] = leftChildKeyValuePair;
                _keyIndices[leftChildKeyValuePair.Key] = index;
                _keyValuePairs[leftChildIndex] = keyValuePair;
                _keyIndices[keyValuePair.Key] = leftChildIndex;
            }
        }
    }
}

public static class Program
{
    private static void Main()
    {
        int intersectionCount;
        while ((intersectionCount = FastIO.ReadNonNegativeInt()) != 0)
        {
            int streetCount = FastIO.ReadNonNegativeInt();
            var intersectionGraph = new WeightedSimpleGraph(intersectionCount);

            for (int i = 0; i < streetCount; ++i)
            {
                intersectionGraph.AddEdge(
                    firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                    secondVertexID: FastIO.ReadNonNegativeInt() - 1,
                    weight: FastIO.ReadNonNegativeInt());
            }

            Console.WriteLine(
                $"{CHICAGO.Solve(intersectionGraph):F6} percent");
        }
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO isn't necessary but it's convenient since I'm copying HIGHWAYS code.
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
