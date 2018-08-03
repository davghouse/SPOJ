using System;
using System.Collections.Generic;
using System.Linq;
using Vertex = WeightedGraph.Vertex;

// https://www.spoj.com/problems/MICEMAZE/ #dijkstras #graph-theory #greedy #heap #shortest-path
// Finds the number of mice that can reach the end of a maze in time.
public static class MICEMAZE
{
    // We use Dijkstra's algorithm from the exit cell. It's like starting at our destination and
    // then running time backwards to see where we could've gotten there from. Connection directions
    // need to be reversed. Like, we can spend forward time to go in one direction, or backwards
    // time to go in the opposite direction. A --> B --> C becomes A <-- B <-- C. As a little
    // optimization we don't initialize the heap w/ all cells. Once the best path time is over our
    // limit, we can just break out of the loop w/o ever putting later cells onto the heap.
    public static int Solve(int cellCount, int exitCell, int timeLimit, int connectionCount, int[,] connections)
    {
        var graph = new WeightedGraph(cellCount);
        for (int c = 0; c < connectionCount; ++c)
        {
            graph.AddEdge(connections[c, 1], connections[c, 0], connections[c, 2]);
        }

        int mouseCount = 0;
        var pathTimes = new BinaryHeap(graph.Vertices[exitCell]);
        bool[] visitedCells = new bool[cellCount];

        while (!pathTimes.IsEmpty)
        {
            var closestPath = pathTimes.Extract();
            var cell = closestPath.Key;
            int pathTimeFromCell = closestPath.Value;

            if (pathTimeFromCell > timeLimit)
                break;

            ++mouseCount;

            foreach (var neighbor in cell.Neighbors.Where(n => !visitedCells[n.ID]))
            {
                int pathTimeFromNeighborThroughCell = pathTimeFromCell + cell.GetEdgeWeight(neighbor);
                int currentPathTimeFromNeighbor;

                // We know the neighboring cell hasn't been visited yet, so we need to maintain its
                // path cost in the heap. If it's already in the heap, see if a cheaper path exists
                // to it through the cell we're visiting. If it isn't in the heap yet, add it.
                if (pathTimes.TryGetValue(neighbor, out currentPathTimeFromNeighbor))
                {
                    if (pathTimeFromNeighborThroughCell < currentPathTimeFromNeighbor)
                    {
                        pathTimes.Update(neighbor, pathTimeFromNeighborThroughCell);
                    }
                }
                else
                {
                    pathTimes.Add(neighbor, pathTimeFromNeighborThroughCell);
                }
            }

            visitedCells[cell.ID] = true;
        }

        return mouseCount;
    }
}

// Directed, weighted graph with no loops or multiple edges. The graph's vertices are stored in an array
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

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public void AddEdge(int firstVertexID, int secondVertexID, int weight)
        => AddEdge(Vertices[firstVertexID], Vertices[secondVertexID], weight);

    public void AddEdge(Vertex firstVertex, Vertex secondVertex, int weight)
        => firstVertex.AddNeighbor(secondVertex, weight);

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
        int cellCount = int.Parse(Console.ReadLine());
        int exitCell = int.Parse(Console.ReadLine()) - 1;
        int timeLimit = int.Parse(Console.ReadLine());
        int connectionCount = int.Parse(Console.ReadLine());

        int[,] connections = new int[connectionCount, 3];
        for (int c = 0; c < connectionCount; ++c)
        {
            string[] line = Console.ReadLine().Split();
            int startCell = int.Parse(line[0]) - 1;
            int endCell = int.Parse(line[1]) - 1;
            int timeCost = int.Parse(line[2]);
            connections[c, 0] = startCell;
            connections[c, 1] = endCell;
            connections[c, 2] = timeCost;
        }

        Console.Write(
            MICEMAZE.Solve(cellCount, exitCell, timeLimit, connectionCount, connections));
    }
}
