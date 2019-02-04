using System;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/RPLN/ #divide-and-conquer #segment-tree
// Finds the minimum score in a range.
public sealed class RPLN
{
    private readonly ArrayBasedSegmentTree _segmentTree;

    public RPLN(int[] scores)
    {
        _segmentTree = new ArrayBasedSegmentTree(scores);
    }

    public int Solve(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(queryStartIndex, queryEndIndex);
}

// Most guides online cover this approach, but here's one good one:
// https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
// This segment tree has some optimizations (compared to say, GSS3) to avoid TLE.
public sealed class ArrayBasedSegmentTree
{
    private readonly int[] _sourceArray;
    private readonly int[] _treeArray;

    public ArrayBasedSegmentTree(int[] sourceArray)
    {
        _sourceArray = sourceArray;
        _treeArray = new int[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Length) - 1];
        Build(0, 0, _sourceArray.Length - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = _sourceArray[segmentStartIndex];
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = Math.Min(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
        => Query(0, 0, _sourceArray.Length - 1, queryStartIndex, queryEndIndex);

    private int Query(
        int treeArrayIndex, int segmentStartIndex, int segmentEndIndex,
        int queryStartIndex, int queryEndIndex)
    {
        if (queryStartIndex <= segmentStartIndex && queryEndIndex >= segmentEndIndex)
            return _treeArray[treeArrayIndex];

        int leftChildTreeArrayIndex = (treeArrayIndex << 1) + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        if (queryStartIndex <= leftChildSegmentEndIndex && queryEndIndex > leftChildSegmentEndIndex)
            return Math.Min(
                Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex),
                Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex));
        else if (queryStartIndex <= leftChildSegmentEndIndex)
            return Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex);
        else
            return Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex);
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
        var output = new StringBuilder();
        int testCount = FastIO.ReadNonNegativeInt();
        for (int t = 1; t <= testCount; ++t)
        {
            int scoreCount = FastIO.ReadNonNegativeInt();
            int queryCount = FastIO.ReadNonNegativeInt();

            int[] scores = new int[scoreCount];
            for (int s = 0; s < scoreCount; ++s)
            {
                scores[s] = FastIO.ReadInt();
            }

            var solver = new RPLN(scores);

            output.AppendLine($"Scenario #{t}:");
            for (int q = 0; q < queryCount; ++q)
            {
                output.Append(solver.Solve(
                    queryStartIndex: FastIO.ReadNonNegativeInt() - 1,
                    queryEndIndex: FastIO.ReadNonNegativeInt() - 1));
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
}
