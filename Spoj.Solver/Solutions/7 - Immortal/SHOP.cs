using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/SHOP/ #dijkstras #graph-theory #greedy #heap #shortest-path
// Finds the fastest path through a crowded computer shop.
public sealed class SHOP
{
    private readonly int _height;
    private readonly int _width;
    private readonly char[,] _shopGrid;
    private readonly int?[] _times;
    private readonly int _startSquareID;
    private readonly int _destinationSquareID;

    public SHOP(int height, int width, char[,] shopGrid)
    {
        _height = height;
        _width = width;
        _shopGrid = shopGrid;
        _times = new int?[_height * _width];

        for (int r = 0; r < _height; ++r)
        {
            for (int c = 0; c < _width; ++c)
            {
                int squareID = GetSquareID(r, c);
                char squareValue = _shopGrid[r, c];

                if (squareValue == 'S')
                {
                    _startSquareID = squareID;
                }
                else if (squareValue == 'D')
                {
                    _destinationSquareID = squareID;
                    _times[_destinationSquareID] = 0;
                }
                else if (squareValue != 'X')
                {
                    _times[squareID] = squareValue - '0';
                }
            }
        }
    }

    private int GetSquareID(int row, int column)
        =>  row * _width + column;

    private bool TryGetSquareID(int row, int column, out int squareID)
    {
        squareID = GetSquareID(row, column);
        return row >= 0 && row < _height && column >= 0 && column < _width;
    }

    private IEnumerable<int> GetNeighboringSquares(int squareID)
    {
        int row = squareID / _width;
        int column = squareID % _width;
        int neighborSquareID;

        if (TryGetSquareID(row - 1, column, out neighborSquareID)) yield return neighborSquareID;
        if (TryGetSquareID(row + 1, column, out neighborSquareID)) yield return neighborSquareID;
        if (TryGetSquareID(row, column - 1, out neighborSquareID)) yield return neighborSquareID;
        if (TryGetSquareID(row, column + 1, out neighborSquareID)) yield return neighborSquareID;
    }

    // This uses Dijkstra's algorithm: https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm.
    // We return immediately upon visiting the destination city, and we don't initialize the
    // heap with all cities. We only add cities to the heap when reaching one of their neighbor
    // cities. Without a pre-filled heap to rely on, we track what cities have been visited
    // using an array of bools.
    public int Solve()
    {
        var timeTotals = new BinaryHeap(_startSquareID);
        bool[] visitedSquares = new bool[_height * _width];

        while (!timeTotals.IsEmpty)
        {
            var fastestTime = timeTotals.Extract();
            int squareID = fastestTime.Key;
            int timeToSquare = fastestTime.Value;

            if (squareID == _destinationSquareID)
                return timeToSquare;

            foreach (int neighborSquareID in GetNeighboringSquares(squareID))
            {
                if (visitedSquares[neighborSquareID])
                    continue;

                int? timeToNeighbor = _times[neighborSquareID];
                if (!timeToNeighbor.HasValue)
                    continue;

                int timeToNeighborThroughSquare = timeToSquare + timeToNeighbor.Value;
                int currentTimeToNeighbor;

                // We know the neighboring square hasn't been visited yet, so we need to maintain its
                // total time in the heap. If it's already in the heap, see if a cheaper path exists
                // to it through the city we're visiting. If it isn't in the heap yet, add it.
                if (timeTotals.TryGetValue(neighborSquareID, out currentTimeToNeighbor))
                {
                    if (timeToNeighborThroughSquare < currentTimeToNeighbor)
                    {
                        timeTotals.Update(neighborSquareID, timeToNeighborThroughSquare);
                    }
                }
                else
                {
                    timeTotals.Add(neighborSquareID, timeToNeighborThroughSquare);
                }
            }

            visitedSquares[squareID] = true;
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
        string[] line;
        char[,] shopGrid = new char[25, 25];
        while ((line = Console.ReadLine().Split())[0] != "0")
        {
            int height = int.Parse(line[1]);
            int width = int.Parse(line[0]);

            for (int r = 0; r < height; ++r)
            {
                string row = Console.ReadLine();
                for (int c = 0; c < width; ++c)
                {
                    shopGrid[r, c] = row[c];
                }
            }
            Console.ReadLine();

            var solver = new SHOP(height, width, shopGrid);

            output.Append(
                solver.Solve());
            output.AppendLine();
        }

        Console.Write(output);
    }
}
