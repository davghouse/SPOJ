using System;
using System.IO;
using System.Linq;

// https://www.spoj.com/problems/MKTHNUM/ #binary-search #divide-and-conquer #merge #segment-tree #sorting
// Answers k-th smallest element in a range queries.
public sealed class MKTHNUM
{
    private readonly int _sourceArrayEndIndex;
    private readonly MergeSortSegmentTree _segmentTree;

    public MKTHNUM(int[] sourceArray)
    {
        _sourceArrayEndIndex = sourceArray.Length - 1;
        _segmentTree = new MergeSortSegmentTree(sourceArray);
    }

    public int Query(int queryStartIndex, int queryEndIndex, int k)
        => _segmentTree.Query(0, 0, _sourceArrayEndIndex, queryStartIndex, queryEndIndex, k);
}

// https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/ (basics)
// https://www.geeksforgeeks.org/merge-sort-tree-for-range-order-statistics/ (merge sort tree)
public sealed class MergeSortSegmentTree
{
    private readonly int[] _sourceArray;
    private readonly IndexedValue[] _orderedSourceValues;
    private readonly int[][] _treeArray;

    public MergeSortSegmentTree(int[] sourceArray)
    {
        _sourceArray = sourceArray;
        _orderedSourceValues = sourceArray
            .Select((v, i) => new IndexedValue(v, i))
            .ToArray();
        Array.Sort(_orderedSourceValues, (v1, v2) => v1.Value.CompareTo(v2.Value));
        _treeArray = new int[2 * Helpers.FirstPowerOfTwoEqualOrGreater(sourceArray.Length) - 1][];
        Build(0, 0, sourceArray.Length - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new[] { _orderedSourceValues[segmentStartIndex].SourceIndex };
            return;
        }

        int leftChildTreeArrayIndex = (treeArrayIndex << 1) | 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = Helpers.MergeSortedArrays(
            firstArray: _treeArray[leftChildTreeArrayIndex],
            secondArray: _treeArray[rightChildTreeArrayIndex]);
    }

    public int Query(
        int treeArrayIndex, int segmentStartIndex, int segmentEndIndex,
        int queryStartIndex, int queryEndIndex, int k)
    {
        if (segmentStartIndex == segmentEndIndex)
            return _sourceArray[_treeArray[treeArrayIndex][0]];

        int leftChildTreeArrayIndex = (treeArrayIndex << 1) | 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        // How many indices in the left child array fall within the range being queried?
        // For example, the left child array might look like [0 2 4 6], and the range
        // being queried might be 1 to 5. Then answer would then be 2--indices 2 and 4.
        int leftChildQueryIndexCount = _treeArray[leftChildTreeArrayIndex]
            .CountElementsBetween(queryStartIndex, queryEndIndex);

        // We need the first k indices in the query range. Does the left child have all we need?
        if (leftChildQueryIndexCount >= k)
            return Query(
                leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex,
                queryStartIndex, queryEndIndex, k);

        // The left child didn't have enough of the indices. Recurse to the right child, but
        // remove from k the first however many indices that the left child did have.
        return Query(
            rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex,
            queryStartIndex, queryEndIndex, k - leftChildQueryIndexCount);
    }
}

public static class Helpers
{
    public static int FirstPowerOfTwoEqualOrGreater(int value)
    {
        int result = 1;
        while (result < value)
        {
            result <<= 1;
        }

        return result;
    }

    public static int[] MergeSortedArrays(int[] firstArray, int[] secondArray)
    {
        int[] mergedArray = new int[firstArray.Length + secondArray.Length];
        int mergedArrayIndex = 0;
        int firstArrayIndex = 0;
        int secondArrayIndex = 0;

        while (firstArrayIndex < firstArray.Length)
        {
            if (secondArrayIndex == secondArray.Length)
            {
                Array.Copy(
                    sourceArray: firstArray,
                    sourceIndex: firstArrayIndex,
                    destinationArray: mergedArray,
                    destinationIndex: mergedArrayIndex,
                    length: firstArray.Length - firstArrayIndex);

                return mergedArray;
            }

            if (firstArray[firstArrayIndex] < secondArray[secondArrayIndex])
            {
                mergedArray[mergedArrayIndex++] = firstArray[firstArrayIndex++];
            }
            else
            {
                mergedArray[mergedArrayIndex++] = secondArray[secondArrayIndex++];
            }
        }

        Array.Copy(
            sourceArray: secondArray,
            sourceIndex: secondArrayIndex,
            destinationArray: mergedArray,
            destinationIndex: mergedArrayIndex,
            length: secondArray.Length - secondArrayIndex);

        return mergedArray;
    }

    // NOTE: doesn't support arrays with duplicate values, because .NET's BinarySearch
    // doesn't guarantee anything about the index of the duplicate found. Not an issue
    // for us, because we're using it on arrays of indices (which are of course distinct).
    public static int CountElementsBetween(this int[] sortedArray, int lowerBound, int upperBound)
    {
        // The index of the first value >= lowerBound, or array length if all smaller.
        int rangeStartIndex = Array.BinarySearch(sortedArray, lowerBound);
        rangeStartIndex = rangeStartIndex < 0 ? ~rangeStartIndex : rangeStartIndex;
        if (rangeStartIndex == sortedArray.Length)
            return 0;

        // The index of the last value <= upperBound, or -1 if all larger. This index
        // can be at most 1 less than rangeStartIndex, when the value @ rangeStartIndex
        // is greater than both lowerBound and upperBound. 0 is returned as desired then.
        int rangeEndIndex = Array.BinarySearch(
            // Save some work by starting the search at rangeStartIndex.
            sortedArray, rangeStartIndex, sortedArray.Length - rangeStartIndex, upperBound);
        rangeEndIndex = rangeEndIndex < 0 ? ~rangeEndIndex - 1 : rangeEndIndex;

        return rangeEndIndex - rangeStartIndex + 1;
    }
}

public struct IndexedValue
{
    public IndexedValue(int value, int sourceIndex)
    {
        Value = value;
        SourceIndex = sourceIndex;
    }

    public int Value { get; }
    public int SourceIndex { get; }
}

public static class Program
{
    private static void Main()
    {
        int sourceArrayLength = FastIO.ReadNonNegativeInt();
        int queryCount = FastIO.ReadNonNegativeInt();

        int[] sourceArray = new int[sourceArrayLength];
        for (int i = 0; i < sourceArrayLength; ++i)
        {
            sourceArray[i] = FastIO.ReadInt();
        }

        var solver = new MKTHNUM(sourceArray);

        for (int q = 0; q < queryCount; ++q)
        {
            FastIO.WriteInt(solver.Query(
                queryStartIndex: FastIO.ReadNonNegativeInt() - 1,
                queryEndIndex: FastIO.ReadNonNegativeInt() - 1,
                k: FastIO.ReadNonNegativeInt()));
            FastIO.WriteLine();
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

    public static void WriteInt(int value)
    {
        bool isNegative = value < 0;

        int digitCount = 0;
        do
        {
            int digit = isNegative ? -(value % 10) : (value % 10);
            _digitsBuffer[digitCount++] = (byte)(digit + _zero);
            value /= 10;
        } while (value != 0);

        if (isNegative)
        {
            _digitsBuffer[digitCount++] = _minusSign;
        }

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
