using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/SHPATH/ #dijkstras #graph-theory #greedy #heap #shortest-path
// Finds the cheapest path between pairs of cities.
public static class SHPATH
{
    // This uses Dijkstra's algorithm: https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm.
    // We return immediately upon visiting the destination city, and we don't initialize the
    // heap with all cities. We only add cities to the heap when reaching one of their neighbor
    // cities. Without a pre-filled heap to rely on, we track what cities have been visited
    // using an array of bools.
    public static int Solve(List<KeyValuePair<int, int>>[] cityGraph, int sourceCity, int destinationCity)
    {
        var pathCosts = new BinaryHeap(sourceCity);
        bool[] visitedCities = new bool[cityGraph.Length];

        while (!pathCosts.IsEmpty)
        {
            var cheapestPath = pathCosts.Extract();
            var city = cheapestPath.Key;
            int pathCostToCity = cheapestPath.Value;

            if (city == destinationCity)
                return pathCostToCity;

            var neighboringEdges = cityGraph[city];
            for (int e = 0; e < neighboringEdges.Count; ++e)
            {
                int neighbor = neighboringEdges[e].Key;
                if (visitedCities[neighbor])
                    continue;

                int pathCostToNeighborThroughCity = pathCostToCity + neighboringEdges[e].Value;
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

            visitedCities[city] = true;
        }

        throw new NotSupportedException();
    }
}

public sealed class BinaryHeap
{
    private readonly List<KeyValuePair<int, int>> _keyValuePairs = new List<KeyValuePair<int, int>>();
    private readonly Dictionary<int, int> _keyIndices = new Dictionary<int, int>();

    public BinaryHeap(int topKey, int topValue = 0)
    {
        _keyValuePairs.Add(new KeyValuePair<int, int>(topKey, topValue));
        _keyIndices.Add(topKey, 0);
    }

    public int Size => _keyValuePairs.Count;
    public bool IsEmpty => Size == 0;
    public KeyValuePair<int, int> Top => _keyValuePairs[0];

    public void Add(int key, int value)
        => Add(new KeyValuePair<int, int>(key, value));

    public void Add(KeyValuePair<int, int> keyValuePair)
    {
        _keyValuePairs.Add(keyValuePair);
        _keyIndices.Add(keyValuePair.Key, _keyValuePairs.Count - 1);
        SiftUp(_keyValuePairs.Count - 1, keyValuePair);
    }

    public KeyValuePair<int, int> Extract()
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

    public bool Contains(int key)
        => _keyIndices.ContainsKey(key);

    public int GetValue(int key)
        => _keyValuePairs[_keyIndices[key]].Value;

    public bool TryGetValue(int key, out int value)
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

    public int Update(int key, int value)
        => Update(new KeyValuePair<int, int>(key, value));

    public int Update(KeyValuePair<int, int> keyValuePair)
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

    private void SiftUp(int index, KeyValuePair<int, int> keyValuePair)
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

    private void SiftDown(int index, KeyValuePair<int, int> keyValuePair)
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
        var output = new StringBuilder();
        int testCount = FastIO.ReadNonNegativeInt();
        for (int t = 0; t < testCount; ++t)
        {
            int cityCount = FastIO.ReadNonNegativeInt();
            var cityIndices = new Dictionary<string, int>(cityCount);
            var cityGraph = new List<KeyValuePair<int, int>>[cityCount];

            for (int c = 0; c < cityCount; ++c)
            {
                cityIndices.Add(FastIO.ReadString(), c);
                cityGraph[c] = new List<KeyValuePair<int, int>>();

                int neighborCount = FastIO.ReadNonNegativeInt();
                for (int n = 0; n < neighborCount; ++n)
                {
                    int neighborIndex = FastIO.ReadNonNegativeInt() - 1;
                    int connectionCost = FastIO.ReadNonNegativeInt();

                    cityGraph[c].Add(new KeyValuePair<int, int>(neighborIndex, connectionCost));
                }
            }

            int pathCount = FastIO.ReadNonNegativeInt();
            for (int p = 0; p < pathCount; ++p)
            {
                int sourceCity = cityIndices[FastIO.ReadString()];
                int destinationCity = cityIndices[FastIO.ReadString()];

                output.Append(SHPATH.Solve(cityGraph, sourceCity, destinationCity));
                output.AppendLine();
            }
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
    private const int _stringLengthLimit = 12;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;
    private static readonly char[] _stringBuilder = new char[_stringLengthLimit];

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

    public static string ReadString()
    {
        byte letter;

        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        do
        {
            letter = ReadByte();
        }
        while (letter < _minusSign);

        int stringLength = 0;
        _stringBuilder[stringLength++] = (char)letter;
        while (true)
        {
            letter = ReadByte();
            if (letter < _zero) break;
            _stringBuilder[stringLength++] = (char)letter;
        }

        return new string(_stringBuilder, 0, stringLength);
    }
}
