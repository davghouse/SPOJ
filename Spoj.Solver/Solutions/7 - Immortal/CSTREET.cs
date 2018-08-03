using System;
using System.Collections.Generic;
using System.Text;
using Vertex = WeightedGraph.Vertex;

// https://www.spoj.com/problems/CSTREET/ #graph-theory #greedy #heap #mst #prims
// Finds the cheapest way to pave streets from any building to any building.
public static class CSTREET
{
    // This uses Prim's algorithm: "https://en.wikipedia.org/wiki/Prim's_algorithm. We don't actually need
    // to build the MST, just need the total cost of the streets that compose it. The heap itself can be
    // used to keep track of which vertices are in the MST. Arbitrarily choose the building/vertex with ID 0
    // to begin creating the MST from. Using int.MaxValue as a sentinel value to represent the street cost
    // for a building into the growing MST when that building actually has no street into the MST. Hopefully
    // that doesn't lead to problems, otherwise would need nullable ints w/ a custom comparer, or something.
    public static long Solve(int buildingCount, int[,] streets)
    {
        var buildingGraph = WeightedGraph.CreateFromOneBasedEdges(buildingCount, streets);
        var connectionCosts = new BinaryHeap(buildingGraph, buildingGraph.Vertices[0]);
        long totalStreetCost = 0;

        while (!connectionCosts.IsEmpty)
        {
            var cheapestConnection = connectionCosts.Extract();
            var building = cheapestConnection.Key;
            int costToConnectBuilding = cheapestConnection.Value;
            totalStreetCost += costToConnectBuilding;

            foreach (var neighbor in building.Neighbors)
            {
                int costToConnectNeighborFromBuilding = building.GetEdgeWeight(neighbor);
                int currentCostToConnectNeighbor;

                // The neighboring building may still be in the heap, or it might already be in the MST. If
                // it's still in the heap, check to see if we can improve its cost to get into the MST by
                // utilizing its street to the building just added.
                if (connectionCosts.TryGetValue(neighbor, out currentCostToConnectNeighbor))
                {
                    if (costToConnectNeighborFromBuilding < currentCostToConnectNeighbor)
                    {
                        connectionCosts.Update(neighbor, costToConnectNeighborFromBuilding);
                    }
                }
            }
        }

        return totalStreetCost;
    }
}

// Undirected, weighted graph with no loops or multiple edges. The graph's vertices are stored in an array
// and the ID of a vertex (from 0 to vertexCount - 1) corresponds to its index in that array. Using a list
// instead of a dictionary for a vertex's edges can help avoid TLE for certain problems. Maintaining
// search state inside of the vertices themselves can also help.
public sealed class WeightedGraph
{
    public WeightedGraph(int vertexCount)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = vertices;
    }

    // For example, an edge like (1, 2, 4) => there's an edge between vertices 0 and 1 with weight 4.
    public static WeightedGraph CreateFromOneBasedEdges(int vertexCount, int[,] edges)
    {
        var graph = new WeightedGraph(vertexCount);
        for (int i = 0; i < edges.GetLength(0); ++i)
        {
            graph.AddEdge(edges[i, 0] - 1, edges[i, 1] - 1, edges[i, 2]);
        }

        return graph;
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
        private readonly WeightedGraph _graph;
        private readonly Dictionary<Vertex, int> _edges = new Dictionary<Vertex, int>();

        internal Vertex(WeightedGraph graph, int ID)
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
    private List<KeyValuePair<Vertex, int>> _keyValuePairs = new List<KeyValuePair<Vertex, int>>();
    private Dictionary<Vertex, int> _keyIndices = new Dictionary<Vertex, int>();

    public BinaryHeap(WeightedGraph graph, Vertex topKey, int topValue = 0)
    {
        _keyValuePairs.Add(new KeyValuePair<Vertex, int>(topKey, topValue));
        _keyIndices.Add(topKey, 0);

        foreach (var vertex in graph.Vertices)
        {
            if (vertex.Equals(topKey))
                continue;

            _keyValuePairs.Add(new KeyValuePair<Vertex, int>(vertex, int.MaxValue));
            _keyIndices.Add(vertex, _keyValuePairs.Count - 1);
        }
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

        // If the parent is larger, push the parent down and the value up--small rises to the top. We know this is okay (aka heap-preserving)
        // because parent was smaller than the other child, as only one child gets out of order at a time. So both are larger than value.
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

            // If the left child is smaller than the right child (so left can move above right, no problem)...
            if (leftChildKeyValuePair.Value < rightChildKeyValuePair.Value)
            {
                // And the value is greater than its left child, push the left child up and the value down--big falls to the bottom.
                if (keyValuePair.Value > leftChildKeyValuePair.Value)
                {
                    _keyValuePairs[index] = leftChildKeyValuePair;
                    _keyIndices[leftChildKeyValuePair.Key] = index;
                    _keyValuePairs[leftChildIndex] = keyValuePair;
                    _keyIndices[keyValuePair.Key] = leftChildIndex;
                    SiftDown(leftChildIndex, keyValuePair);
                }
            }
            // If the right child is smaller or the same as the left child (so right can move above left, no problem)...
            else
            {
                // And the value is greater than its right child, push the right child up and the value down--big falls to the bottom.
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

            // And the value is greater than its left child, push the left child up and the value down--big falls to the bottom.
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
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int pricePerFurlong = int.Parse(Console.ReadLine());
            int buildingCount = int.Parse(Console.ReadLine());
            int streetCount = int.Parse(Console.ReadLine());

            var streets = new int[streetCount, 3];
            for (int s = 0; s < streetCount; ++s)
            {
                int[] street = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                streets[s, 0] = street[0];
                streets[s, 1] = street[1];
                streets[s, 2] = street[2] * pricePerFurlong; // length in furlongs * price per furlong = cost.
            }

            output.Append(
                CSTREET.Solve(buildingCount, streets));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
