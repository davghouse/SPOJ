using System;
using System.IO;

// https://www.spoj.com/problems/FREQUENT/ #divide-and-conquer #segment-tree
// Finds the count of the most frequent value in subranges of an ordered array.
public sealed class FREQUENT
{
    private readonly ArrayBasedSegmentTree _segmentTree;

    public FREQUENT(int[] sourceArray)
    {
        _segmentTree = new ArrayBasedSegmentTree(sourceArray);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(queryStartIndex, queryEndIndex).MostFrequentValueCount;
}

// Most guides online cover this approach, but here's one good one:
// https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
// This segment tree has some minor optimizations (compared to say, GSS3) to avoid TLE.
public sealed class ArrayBasedSegmentTree
{
    private readonly int[] _sourceArray;
    private readonly MostFrequentValueQueryObject[] _treeArray;

    public ArrayBasedSegmentTree(int[] sourceArray)
    {
        _sourceArray = sourceArray;
        _treeArray = new MostFrequentValueQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Length) - 1];
        Build(0, 0, _sourceArray.Length - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new MostFrequentValueQueryObject(_sourceArray[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public MostFrequentValueQueryObject Query(int queryStartIndex, int queryEndIndex)
        => Query(0, 0, _sourceArray.Length - 1, queryStartIndex, queryEndIndex);

    private MostFrequentValueQueryObject Query(
        int treeArrayIndex, int segmentStartIndex, int segmentEndIndex,
        int queryStartIndex, int queryEndIndex)
    {
        if (queryStartIndex <= segmentStartIndex && queryEndIndex >= segmentEndIndex)
            return _treeArray[treeArrayIndex];

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        if (queryStartIndex <= leftChildSegmentEndIndex && queryEndIndex > leftChildSegmentEndIndex)
            return Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex)
                .Combine(Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex));
        else if (queryStartIndex <= leftChildSegmentEndIndex)
            return Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex);
        else
            return Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex);
    }
}

public sealed class MostFrequentValueQueryObject
{
    private MostFrequentValueQueryObject()
    { }

    public MostFrequentValueQueryObject(int value)
    {
        MostFrequentValue = value;
        MostFrequentValueCount = 1;
        LeftStartingValue = value;
        LeftStartingValueCount = 1;
        RightStartingValue = value;
        RightStartingValueCount = 1;
    }

    private int MostFrequentValue { get; set; }
    public int MostFrequentValueCount { get; set; }
    private int LeftStartingValue { get; set; }
    private int LeftStartingValueCount { get; set; }
    private int RightStartingValue { get; set; }
    private int RightStartingValueCount { get; set; }

    public MostFrequentValueQueryObject Combine(MostFrequentValueQueryObject rightAdjacentValue)
    {
        var combinedValue = new MostFrequentValueQueryObject();

        // Either the most frequent value is entirely in the left or the right subrange...
        if (MostFrequentValueCount > rightAdjacentValue.MostFrequentValueCount)
        {
            combinedValue.MostFrequentValue = MostFrequentValue;
            combinedValue.MostFrequentValueCount = MostFrequentValueCount;
        }
        else
        {
            combinedValue.MostFrequentValue = rightAdjacentValue.MostFrequentValue;
            combinedValue.MostFrequentValueCount = rightAdjacentValue.MostFrequentValueCount;
        }

        // ...or it crosses over between the left and right subranges.
        if (RightStartingValue == rightAdjacentValue.LeftStartingValue)
        {
            int intersectingValue = RightStartingValue;
            int intersectingValueCount = RightStartingValueCount
                + rightAdjacentValue.LeftStartingValueCount;

            if (intersectingValueCount > combinedValue.MostFrequentValueCount)
            {
                combinedValue.MostFrequentValue = intersectingValue;
                combinedValue.MostFrequentValueCount = intersectingValueCount;
            }
        }

        // The left starting value starts with the left range...
        combinedValue.LeftStartingValue = LeftStartingValue;
        combinedValue.LeftStartingValueCount = LeftStartingValueCount;
        // ...and it might cross over into the right range:
        if (LeftStartingValue == rightAdjacentValue.LeftStartingValue)
        {
            combinedValue.LeftStartingValueCount += rightAdjacentValue.LeftStartingValueCount;
        }

        // The right starting value starts with the right range...
        combinedValue.RightStartingValue = rightAdjacentValue.RightStartingValue;
        combinedValue.RightStartingValueCount = rightAdjacentValue.RightStartingValueCount;
        // ...and it might cross over into the right range:
        if (RightStartingValue == rightAdjacentValue.RightStartingValue)
        {
            combinedValue.RightStartingValueCount += RightStartingValueCount;
        }

        return combinedValue;
    }
}

public static class MathHelper
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
}

public static class Program
{
    private static void Main()
    {
        int arrayLength;
        while ((arrayLength = FastIO.ReadNonNegativeInt()) != 0)
        {
            int queryCount = FastIO.ReadNonNegativeInt();
            int[] sourceArray = new int[arrayLength];
            for (int i = 0; i < arrayLength; ++i)
            {
                sourceArray[i] = FastIO.ReadInt();
            }

            var solver = new FREQUENT(sourceArray);

            for (int q = 0; q < queryCount; ++q)
            {
                FastIO.WriteInt(solver.Query(
                    queryStartIndex: FastIO.ReadNonNegativeInt() - 1,
                    queryEndIndex: FastIO.ReadNonNegativeInt() - 1));
                FastIO.WriteLine();
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
