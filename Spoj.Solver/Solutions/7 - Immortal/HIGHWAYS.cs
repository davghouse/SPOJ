using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vertex = WeightedSimpleGraph.Vertex;

// https://www.spoj.com/problems/HIGHWAYS/ #dijkstras #graph-theory #greedy #heap #shortest-path
// Finds the cheapest path between pairs of cities.
public static class HIGHWAYS
{
    // This uses Dijkstra's algorithm: https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm.
    // We return immediately upon visiting the destination city, and we don't initialize the
    // heap with all cities. We only add cities to the heap when reaching one of their neighbor
    // cities. Without a pre-filled heap to rely on, we track what cities have been visited
    // using an array of bools.
    public static int? Solve(WeightedSimpleGraph cityGraph, Vertex startCity, Vertex endCity)
    {
        var pathCosts = new BinaryHeap(startCity);
        bool[] visitedCities = new bool[cityGraph.VertexCount];

        while (!pathCosts.IsEmpty)
        {
            var cheapestPath = pathCosts.Extract();
            var city = cheapestPath.Key;
            int pathCostToCity = cheapestPath.Value;

            if (city == endCity)
                return pathCostToCity;

            foreach (var neighbor in city.Neighbors.Where(n => !visitedCities[n.ID]))
            {
                int pathCostToNeighborThroughCity = pathCostToCity + city.GetEdgeWeight(neighbor);
                int currentPathCostToNeighbor;

                // We know the neighboring city hasn't been visited yet, so we need to maintain its
                // path cost in the heap. If it's already in the heap, see if a cheaper path exists
                // to it through the city we're visiting. If it isn't in the heap yet, add it.
                if (pathCosts.TryGetValue(neighbor, out currentPathCostToNeighbor))
                {
                    if (pathCostToNeighborThroughCity < currentPathCostToNeighbor)
                    {
                        pathCosts.Update(neighbor, pathCostToNeighborThroughCity);
                    }
                }
                else
                {
                    pathCosts.Add(neighbor, pathCostToNeighborThroughCity);
                }
            }

            visitedCities[city.ID] = true;
        }

        return null;
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

        // In this city graph, there can be multiple highways between cities--use the cheapest.
        internal void AddNeighbor(Vertex neighbor, int weight)
        {
            int currentWeight;
            if (!_edges.TryGetValue(neighbor, out currentWeight))
            {
                _edges.Add(neighbor, weight);
            }
            else if (weight < currentWeight)
            {
                _edges[neighbor] = weight;
            }
        }

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
    private readonly List<KeyValuePair<Vertex, int>> _keyValuePairs = new List<KeyValuePair<Vertex, int>>();
    private readonly Dictionary<Vertex, int> _keyIndices = new Dictionary<Vertex, int>();

    public BinaryHeap(Vertex topKey, int topValue = 0)
    {
        _keyValuePairs.Add(new KeyValuePair<Vertex, int>(topKey, topValue));
        _keyIndices.Add(topKey, 0);
    }

    public int Size => _keyValuePairs.Count;
    public bool IsEmpty => Size == 0;
    public KeyValuePair<Vertex, int> Top => _keyValuePairs[0];

    public void Add(Vertex key, int value)
        => Add(new KeyValuePair<Vertex, int>(key, value));

    public void Add(KeyValuePair<Vertex, int> keyValuePair)
    {
        _keyValuePairs.Add(keyValuePair);
        _keyIndices.Add(keyValuePair.Key, _keyValuePairs.Count - 1);
        SiftUp(_keyValuePairs.Count - 1, keyValuePair);
    }

    public KeyValuePair<Vertex, int> Extract()
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

    public int GetValue(Vertex key)
        => _keyValuePairs[_keyIndices[key]].Value;

    public bool TryGetValue(Vertex key, out int value)
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

    public int Update(Vertex key, int value)
        => Update(new KeyValuePair<Vertex, int>(key, value));

    public int Update(KeyValuePair<Vertex, int> keyValuePair)
    {
        int index = _keyIndices[keyValuePair.Key];
        int oldValue = _keyValuePairs[index].Value;
        _keyValuePairs[index] = keyValuePair;

        // If the old value was larger than the updated value, try sifting the updated value up.
        if (oldValue > keyValuePair.Value)
        {
            SiftUp(index, keyValuePair);
        }
        else
        {
            SiftDown(index, keyValuePair);
        }

        return oldValue;
    }

    private void SiftUp(int index, KeyValuePair<Vertex, int> keyValuePair)
    {
        // Stop if we don't have a parent to sift up to.
        if (index == 0) return;

        int parentIndex = (index - 1) / 2;
        var parentKeyValuePair = _keyValuePairs[parentIndex];

        // If the parent is larger, push the parent down and the value up--small rises to the
        // top. We know this is okay (aka heap-preserving) because parent was smaller than the
        // other child, as only one child gets out of order at a time. So both are larger than value.
        if (parentKeyValuePair.Value > keyValuePair.Value)
        {
            _keyValuePairs[index] = parentKeyValuePair;
            _keyIndices[parentKeyValuePair.Key] = index;
            _keyValuePairs[parentIndex] = keyValuePair;
            _keyIndices[keyValuePair.Key] = parentIndex;
            SiftUp(parentIndex, keyValuePair);
        }
    }

    private void SiftDown(int index, KeyValuePair<Vertex, int> keyValuePair)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;

        // If both children exist...
        if (rightChildIndex < _keyValuePairs.Count)
        {
            var leftChildKeyValuePair = _keyValuePairs[leftChildIndex];
            var rightChildKeyValuePair = _keyValuePairs[rightChildIndex];

            // If the left child is smaller than the right child (so left can move above right)...
            if (leftChildKeyValuePair.Value < rightChildKeyValuePair.Value)
            {
                // And the value is greater than its left child, push the left child up and
                // the value down--big falls to the bottom.
                if (keyValuePair.Value > leftChildKeyValuePair.Value)
                {
                    _keyValuePairs[index] = leftChildKeyValuePair;
                    _keyIndices[leftChildKeyValuePair.Key] = index;
                    _keyValuePairs[leftChildIndex] = keyValuePair;
                    _keyIndices[keyValuePair.Key] = leftChildIndex;
                    SiftDown(leftChildIndex, keyValuePair);
                }
            }
            // If the right child is smaller or the same as the left child (so right can move above left)...
            else
            {
                // And the value is greater than its right child, push the right child up and
                // the value down--big falls to the bottom.
                if (keyValuePair.Value > rightChildKeyValuePair.Value)
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

            // And the value is greater than its left child, push the left child up and
            // the value down--big falls to the bottom.
            if (keyValuePair.Value > leftChildKeyValuePair.Value)
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
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int cityCount = FastIO.ReadNonNegativeInt();
            int highwayCount = FastIO.ReadNonNegativeInt();
            int startCityIndex = FastIO.ReadNonNegativeInt() - 1;
            int endCityIndex = FastIO.ReadNonNegativeInt() - 1;
            var cityGraph = new WeightedSimpleGraph(cityCount);

            for (int h = 0; h < highwayCount; ++h)
            {
                cityGraph.AddEdge(
                    firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                    secondVertexID: FastIO.ReadNonNegativeInt() - 1,
                    weight: FastIO.ReadNonNegativeInt());
            }

            int? totalMinutes = HIGHWAYS.Solve(cityGraph,
                startCity: cityGraph.Vertices[startCityIndex],
                endCity: cityGraph.Vertices[endCityIndex]);

            Console.WriteLine(totalMinutes?.ToString() ?? "NONE");
        }
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
