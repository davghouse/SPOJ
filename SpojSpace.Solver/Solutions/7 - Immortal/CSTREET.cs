using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// http://www.spoj.com/problems/CSTREET/ #graph-theory #greedy #heap #mst #research
// Finds the cheapest way to pave streets from any building to any building.
public static class CSTREET
{
    // This uses Prim's algorithm: "https://en.wikipedia.org/wiki/Prim's_algorithm. We don't actually need
    // to build the MST, just get the total cost of the streets that compose it. The heap itself can be
    // used to keep track of which vertices are in the MST. Arbitrarily choose the building/vertex with ID 0
    // to begin created the MST from. Using int.MaxValue as a sentinel value to represent the street cost
    // for a building into the growing MST when that building actually has no street into the MST. Hopefully
    // that doesn't lead to problems, otherwise would need nullable ints w/ a custom comparer, or something.
    public static long Solve(int buildingCount, int[,] streets)
    {
        var buildingGraph = WeightedGraph.CreateFromOneBasedEdges(buildingCount, streets);
        var streetCostHeap = new BinaryHeap<WeightedGraph.Vertex, int>();

        foreach (var building in buildingGraph.Vertices.Skip(1))
        {
            int streetCostToFirstBuilding;
            if (building.TryGetEdgeWeight(0, out streetCostToFirstBuilding))
            {
                streetCostHeap.Add(building, streetCostToFirstBuilding);
            }
            else
            {
                streetCostHeap.Add(building, int.MaxValue);
            }
        }

        long totalStreetCost = 0;
        while (!streetCostHeap.IsEmpty)
        {
            var buildingAndCostAdded = streetCostHeap.Extract();
            var buildingAdded = buildingAndCostAdded.Key;
            totalStreetCost += buildingAndCostAdded.Value;

            foreach (var neighboringBuildingAndCost in buildingAdded.Neighbors)
            {
                var neighboringBuilding = neighboringBuildingAndCost.Key;
                int costToBuildingAdded = neighboringBuildingAndCost.Value;
                int currentCost;

                // The neighboring building may still be in the heap, or it might already be in the MST. If
                // it's still in the heap, check to see if we can improve its cost to get into the MST by
                // utilizing its street to the building just added.
                if (streetCostHeap.TryGetValue(neighboringBuilding, out currentCost))
                {
                    if (costToBuildingAdded < currentCost)
                    {
                        streetCostHeap.Update(neighboringBuilding, costToBuildingAdded);
                    }
                }
            }
        }

        return totalStreetCost;
    }
}

// Undirected, weighted graph with no loops or multiple edges. The graph's vertices are stored in an array
// and the ID of a vertex (from 0 to vertexCount - 1) corresponds to its index in that array. Hash sets in
// the vertices slow things down (use list for some problems).
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

    public bool HasEdge(int firstVertexID, int secondVertexID)
        => HasEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
        => firstVertex.HasNeighbor(secondVertex);

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly WeightedGraph _graph;
        private readonly Dictionary<Vertex, int> _neighbors = new Dictionary<Vertex, int>();

        internal Vertex(WeightedGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public IReadOnlyDictionary<Vertex, int> Neighbors => _neighbors;
        public int Degree => _neighbors.Count;

        internal void AddNeighbor(int neighborID, int weight)
            => _neighbors.Add(_graph.Vertices[neighborID], weight);

        internal void AddNeighbor(Vertex neighbor, int weight)
            => _neighbors.Add(neighbor, weight);

        public bool HasNeighbor(int neighborID)
            => _neighbors.ContainsKey(_graph.Vertices[neighborID]);

        public bool HasNeighbor(Vertex neighbor)
            => _neighbors.ContainsKey(neighbor);

        public int GetEdgeWeight(int neighborID)
            => _neighbors[_graph.Vertices[neighborID]];

        public int GetEdgeWeight(Vertex neighbor)
            => _neighbors[neighbor];

        public bool TryGetEdgeWeight(int neighborID, out int edgeWeight)
            => _neighbors.TryGetValue(_graph.Vertices[neighborID], out edgeWeight);

        public bool TryGetEdgeWeight(Vertex neighbor, out int edgeWeight)
            => _neighbors.TryGetValue(neighbor, out edgeWeight);

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;
    }
}

public sealed class BinaryHeap<TKey, TValue>
{
    private List<KeyValuePair<TKey, TValue>> _keyValuePairs;
    private Dictionary<TKey, int> _keyIndices;
    private IComparer<TValue> _comparer;

    public BinaryHeap(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs = null, IComparer<TValue> comparer = null)
    {
        keyValuePairs = keyValuePairs ?? new KeyValuePair<TKey, TValue>[0];
        _keyValuePairs = new List<KeyValuePair<TKey, TValue>>((keyValuePairs as ICollection<KeyValuePair<TKey, TValue>>)?.Count + 0 ?? 0);
        _keyIndices = new Dictionary<TKey, int>(_keyValuePairs.Count);
        _comparer = comparer ?? Comparer<TValue>.Default;

        foreach (var keyValuePair in keyValuePairs)
        {
            Add(keyValuePair);
        }
    }

    public int Size => _keyValuePairs.Count;
    public bool IsEmpty => Size == 0;
    public KeyValuePair<TKey, TValue> Top => _keyValuePairs[0];

    public void Add(TKey key, TValue value)
        => Add(new KeyValuePair<TKey, TValue>(key, value));

    public void Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
        _keyValuePairs.Add(keyValuePair);
        _keyIndices.Add(keyValuePair.Key, _keyValuePairs.Count - 1);
        SiftUp(_keyValuePairs.Count - 1, keyValuePair);
    }

    public KeyValuePair<TKey, TValue> Extract()
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

    public KeyValuePair<TKey, TValue> Replace(TKey key, TValue value)
        => Replace(new KeyValuePair<TKey, TValue>(key, value));

    public KeyValuePair<TKey, TValue> Replace(KeyValuePair<TKey, TValue> keyValuePair)
    {
        var top = _keyValuePairs[0];
        _keyIndices.Remove(top.Key);
        _keyValuePairs[0] = keyValuePair;
        _keyIndices.Add(keyValuePair.Key, 0);
        SiftDown(0, keyValuePair);

        return top;
    }

    public bool Contains(TKey key)
        => _keyIndices.ContainsKey(key);

    public TValue GetValue(TKey key)
        => _keyValuePairs[_keyIndices[key]].Value;

    public bool TryGetValue(TKey key, out TValue value)
    {
        int keyIndex;
        if (_keyIndices.TryGetValue(key, out keyIndex))
        {
            value = _keyValuePairs[keyIndex].Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public TValue Update(TKey key, TValue value)
        => Update(new KeyValuePair<TKey, TValue>(key, value));

    public TValue Update(KeyValuePair<TKey, TValue> keyValuePair)
    {
        int index = _keyIndices[keyValuePair.Key];
        TValue oldValue = _keyValuePairs[index].Value;
        _keyValuePairs[index] = keyValuePair;

        // If the old value was larger than the updated value, try sifting the updated value up.
        if (_comparer.Compare(oldValue, keyValuePair.Value) > 0)
        {
            SiftUp(index, keyValuePair);
        }
        else
        {
            SiftDown(index, keyValuePair);
        }

        return oldValue;
    }

    private void SiftUp(int index, KeyValuePair<TKey, TValue> keyValuePair)
    {
        // Stop if we don't have a parent to sift up to.
        if (index == 0) return;

        int parentIndex = (index - 1) / 2;
        var parentKeyValuePair = _keyValuePairs[parentIndex];

        // If the parent is larger, push the parent down and the value up--small rises to the top. We know this is okay (aka heap-preserving)
        // because parent was smaller than the other child, as only one child gets out of order at a time. So both are larger than value.
        if (_comparer.Compare(parentKeyValuePair.Value, keyValuePair.Value) > 0)
        {
            _keyValuePairs[index] = parentKeyValuePair;
            _keyIndices[parentKeyValuePair.Key] = index;
            _keyValuePairs[parentIndex] = keyValuePair;
            _keyIndices[keyValuePair.Key] = parentIndex;
            SiftUp(parentIndex, keyValuePair);
        }
    }

    private void SiftDown(int index, KeyValuePair<TKey, TValue> keyValuePair)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;

        // If both children exist...
        if (rightChildIndex < _keyValuePairs.Count)
        {
            var leftChildKeyValuePair = _keyValuePairs[leftChildIndex];
            var rightChildKeyValuePair = _keyValuePairs[rightChildIndex];

            // If the left child is smaller than the right child (so left can move above right, no problem)...
            if (_comparer.Compare(leftChildKeyValuePair.Value, rightChildKeyValuePair.Value) < 0)
            {
                // And the value is greater than its left child, push the left child up and the value down--big falls to the bottom.
                if (_comparer.Compare(keyValuePair.Value, leftChildKeyValuePair.Value) > 0)
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
                if (_comparer.Compare(keyValuePair.Value, rightChildKeyValuePair.Value) > 0)
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
            if (_comparer.Compare(keyValuePair.Value, leftChildKeyValuePair.Value) > 0)
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
