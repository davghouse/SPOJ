using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// https://www.spoj.com/problems/ORDERSET/ #binary-search #bit #compression #offline #sorting
// Inserts and deletes elements while answering kth smallest and count smaller queries.
public sealed class ORDERSET
{
    private readonly HashSet<int> _possibleValues;
    private readonly int[] _orderedValues;
    private readonly bool[] _valueIsInserted;
    private readonly PURQBinaryIndexedTree _insertionBIT;

    public ORDERSET(HashSet<int> possibleValues)
    {
        _possibleValues = possibleValues;
        _orderedValues = possibleValues.OrderBy(v => v).ToArray();
        _valueIsInserted = new bool[_orderedValues.Length];
        _insertionBIT = new PURQBinaryIndexedTree(_orderedValues.Length);
    }

    public void Insert(int value)
    {
        int index = Array.BinarySearch(_orderedValues, value);
        if (_valueIsInserted[index]) return;

        _insertionBIT.PointUpdate(index, 1);
        _valueIsInserted[index] = true;
    }

    public void Delete(int value)
    {
        int index = Array.BinarySearch(_orderedValues, value);
        if (index < 0 || !_valueIsInserted[index]) return;

        _insertionBIT.PointUpdate(index, -1);
        _valueIsInserted[index] = false;
    }

    public int? GetKthSmallest(int k)
    {
        int? indexOfKthSmallest = BinarySearch.Search(
            start: 0,
            end: _orderedValues.Length - 1,
            // Querying the BIT from 0 to an end index counts the number of inserted values that
            // are <= to the value at the end index (in the ordered values array). The kth smallest
            // value is at the index that makes the BIT query == k. We binary search to find it.
            verifier: queryEndIndex => _insertionBIT.SumQuery(queryEndIndex) >= k,
            mode: BinarySearch.Mode.FalseToTrue);

        return indexOfKthSmallest.HasValue
            ? _orderedValues[indexOfKthSmallest.Value]
            : (int?)null;
    }

    public int CountValuesSmallerThan(int value)
    {
        int index = Array.BinarySearch(_orderedValues, value);
        int queryEndIndex = index > 0
            ? index - 1 // Value was found, smaller values are at the indices to its left.
            : ~index - 1; // Value wasn't found, bitwise complement is index of first value larger than it.

        return _insertionBIT.SumQuery(queryEndIndex);
    }
}

// Point update, range query binary indexed tree. This is the original BIT described
// by Fenwick. There are lots of tutorials online but I'd start with these two videos:
// https://www.youtube.com/watch?v=v_wj_mOAlig, https://www.youtube.com/watch?v=CWDQJGaN1gY.
// Those make the querying part clear but don't really describe the update part very well.
// For that, I'd go and read Fenwick's paper. This is all a lot less intuitive than segment trees.
public sealed class PURQBinaryIndexedTree
{
    private readonly int[] _tree;

    public PURQBinaryIndexedTree(int arrayLength)
    {
        _tree = new int[arrayLength + 1];
    }

    // There's a way to do this in O(n) instead of O(nlogn), apparently.
    public PURQBinaryIndexedTree(IReadOnlyList<int> array)
    {
        _tree = new int[array.Count + 1];

        for (int i = 0; i < array.Count; ++i)
        {
            PointUpdate(i, array[i]);
        }
    }

    // Updates to reflect an addition at an index of the original array (by traversing the update tree).
    public void PointUpdate(int updateIndex, int delta)
    {
        for (++updateIndex;
            updateIndex < _tree.Length;
            updateIndex += updateIndex & -updateIndex)
        {
            _tree[updateIndex] += delta;
        }
    }

    // Computes the sum from the zeroth index through the query index (by traversing the interrogation tree).
    public int SumQuery(int queryEndIndex)
    {
        int sum = 0;
        for (++queryEndIndex;
            queryEndIndex > 0;
            queryEndIndex -= queryEndIndex & -queryEndIndex)
        {
            sum += _tree[queryEndIndex];
        }

        return sum;
    }

    // Computes the sum from the start through the end query index, by removing the part we
    // shouldn't have counted. Fenwick describes a more efficient way to do this, but it's complicated.
    public int SumQuery(int queryStartIndex, int queryEndIndex)
        => SumQuery(queryEndIndex) - SumQuery(queryStartIndex - 1);
}

// This facilitates predicate-based binary searching, where the values being searched on
// satisfy the predicate in an ordered manner, in one of two ways:
// [false false false ... false true ... true true true] (true => anything larger is true)
// [true true true ... true false ... false false false] (true => anything smaller is true)
// In the first, the goal of the search is to locate the smallest value satisfying the predicate.
// In the second, the goal of the search is to locate the largest value satisfying the predicate.
// For more info, see: https://www.topcoder.com/community/data-science/data-science-tutorials/binary-search/.
public static class BinarySearch
{
    public enum Mode
    {
        FalseToTrue,
        TrueToFalse
    };

    public static int? Search(int start, int end, Predicate<int> verifier, Mode mode)
        => mode == Mode.FalseToTrue
        ? SearchFalseToTrue(start, end, verifier)
        : SearchTrueToFalse(start, end, verifier);

    private static int? SearchFalseToTrue(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int mid;

        while (start != end)
        {
            mid = start + (end - start) / 2;

            if (verifier(mid))
            {
                end = mid;
            }
            else
            {
                start = mid + 1;
            }
        }

        return verifier(start) ? start : (int?)null;
    }

    private static int? SearchTrueToFalse(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int mid;

        while (start != end)
        {
            mid = start + (end - start + 1) / 2;

            if (verifier(mid))
            {
                start = mid;
            }
            else
            {
                end = mid - 1;
            }
        }

        return verifier(start) ? start : (int?)null;
    }
}

public struct Operation
{
    public Operation(char operationType, int operationParameter)
    {
        OperationType = operationType;
        OperationParameter = operationParameter;
    }

    public char OperationType { get; }
    public int OperationParameter { get; }
}

public static class Program
{
    private static void Main()
    {
        int operationCount = FastIO.ReadNonNegativeInt();
        var operations = new Operation[operationCount];
        var possibleValues = new HashSet<int>();

        for (int o = 0; o < operationCount; ++o)
        {
            operations[o] = new Operation(
                operationType: FastIO.ReadOperationType(),
                operationParameter: FastIO.ReadInt());

            if (operations[o].OperationType == 'I')
            {
                possibleValues.Add(operations[o].OperationParameter);
            }
        }

        var solver = new ORDERSET(possibleValues);
        var output = new StringBuilder();

        for (int o = 0; o < operationCount; ++o)
        {
            switch (operations[o].OperationType)
            {
                case 'I':
                    solver.Insert(value: operations[o].OperationParameter);
                    break;
                case 'D':
                    solver.Delete(value: operations[o].OperationParameter);
                    break;
                case 'K':
                    output.AppendLine(solver
                        .GetKthSmallest(k: operations[o].OperationParameter)
                        ?.ToString() ?? "invalid");
                    break;
                case 'C':
                    output.AppendLine(solver
                        .CountValuesSmallerThan(value: operations[o].OperationParameter)
                        .ToString());
                    break;
                default: throw new InvalidOperationException();
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

    public static int ReadInt()
    {
        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        byte digit;
        do
        {
            digit = ReadByte();
        }
        while (digit < _minusSign);

        bool isNegative = digit == _minusSign;
        if (isNegative)
        {
            digit = ReadByte();
        }

        // Build up the integer from its digits, until we run into whitespace or the null byte.
        int result = isNegative ? -(digit - _zero) : (digit - _zero);
        while (true)
        {
            digit = ReadByte();
            if (digit < _zero) break;
            result = result * 10 + (isNegative ? -(digit - _zero) : (digit - _zero));
        }

        return result;
    }

    public static char ReadOperationType()
    {
        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        byte operationType;
        do
        {
            operationType = ReadByte();
        } while (operationType < _minusSign);

        return (char)operationType;
    }
}
