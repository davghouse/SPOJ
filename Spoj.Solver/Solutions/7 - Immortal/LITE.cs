using System;
using System.IO;

// https://www.spoj.com/problems/LITE/ #divide-and-conquer #lazy #segment-tree
// Calculates how many lights in a range have been toggled on.
public sealed class LITE
{
    private readonly LazySumSegmentTree _segmentTree;

    public LITE(int lightCount)
    {
        _segmentTree = new LazySumSegmentTree(lightCount);
    }

    public void Update(int updateStartIndex, int updateEndIndex)
        => _segmentTree.Push(updateStartIndex, updateEndIndex);

    public int Query(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(queryStartIndex, queryEndIndex);
}

public sealed class LazySumSegmentTree
{
    private readonly QueryObject[] _treeArray;

    public LazySumSegmentTree(int arrayLength)
    {
        _treeArray = new QueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(arrayLength) - 1];
        Build(0, 0, arrayLength - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new QueryObject(segmentStartIndex);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
        => Query(0, queryStartIndex, queryEndIndex).LitUpCount;

    // Instead of returning the children object directly, we have to add on the parent's range addition. The children
    // query object knows the subset of the parent segment it intersects with, and everything in there needs the
    // additions that were applied to the parent segment as a whole. It's kind of weird, any pending range additions
    // specifically for the children object gets brought out and added to the sum when we do .Combine or .Sum, but
    // recursively it makes sense: the children object has a sum but still needs to know about the parent's range additions.
    private QueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
    {
        var parentQueryObject = _treeArray[treeArrayIndex];

        if (parentQueryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
            return parentQueryObject;

        bool leftHalfOverlaps = parentQueryObject.DoesLeftHalfOverlapWith(queryStartIndex, queryEndIndex);
        bool rightHalfOverlaps = parentQueryObject.DoesRightHalfOverlapWith(queryStartIndex, queryEndIndex);
        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        QueryObject childrenQueryObject;

        if (leftHalfOverlaps && rightHalfOverlaps)
        {
            childrenQueryObject = Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex)
                .Combine(Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex));
        }
        else if (leftHalfOverlaps)
        {
            childrenQueryObject = Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        }
        else
        {
            childrenQueryObject = Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        }

        return new QueryObject(
            childrenQueryObject.SegmentStartIndex,
            childrenQueryObject.SegmentEndIndex,
            childrenQueryObject.LitUpCount)
        {
            RangePushes = parentQueryObject.RangePushes
        };
    }

    public void Push(int pushStartIndex, int pushEndIndex)
        => Push(0, pushStartIndex, pushEndIndex);

    private void Push(int treeArrayIndex, int pushStartIndex, int pushEndIndex)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.IsTotallyOverlappedBy(pushStartIndex, pushEndIndex))
        {
            ++queryObject.RangePushes;
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (queryObject.DoesLeftHalfOverlapWith(pushStartIndex, pushEndIndex))
        {
            Push(leftChildTreeArrayIndex, pushStartIndex, pushEndIndex);
        }

        if (queryObject.DoesRightHalfOverlapWith(pushStartIndex, pushEndIndex))
        {
            Push(rightChildTreeArrayIndex, pushStartIndex, pushEndIndex);
        }

        queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }

    private sealed class QueryObject
    {
        public int LitUpCount
            => RangePushes % 2 == 0
            ? LitUpCountWithoutRangePushes
            : SegmentLightCount - LitUpCountWithoutRangePushes;

        public int LitUpCountWithoutRangePushes { get; internal set; }
        public int RangePushes { get; internal set; }

        public int SegmentStartIndex { get; }
        public int SegmentEndIndex { get; }
        public int SegmentLightCount => SegmentEndIndex - SegmentStartIndex + 1;

        public QueryObject(int index)
        {
            SegmentStartIndex = index;
            SegmentEndIndex = index;
        }

        public QueryObject(int segmentStartIndex, int segmentEndIndex, int litUpCountWithoutRangePushes)
        {
            SegmentStartIndex = segmentStartIndex;
            SegmentEndIndex = segmentEndIndex;
            LitUpCountWithoutRangePushes = litUpCountWithoutRangePushes;
        }

        public QueryObject Combine(QueryObject rightAdjacentObject)
            => new QueryObject(
                segmentStartIndex: SegmentStartIndex,
                segmentEndIndex: rightAdjacentObject.SegmentEndIndex,
                litUpCountWithoutRangePushes: LitUpCount + rightAdjacentObject.LitUpCount);

        public void Update(QueryObject updatedLeftChild, QueryObject updatedRightChild)
            => LitUpCountWithoutRangePushes = updatedLeftChild.LitUpCount + updatedRightChild.LitUpCount;

        public bool IsTotallyOverlappedBy(int startIndex, int endIndex)
            => startIndex <= SegmentStartIndex && endIndex >= SegmentEndIndex;

        public bool DoesLeftHalfOverlapWith(int startIndex, int endIndex)
            => startIndex <= (SegmentStartIndex + SegmentEndIndex) / 2;

        public bool DoesRightHalfOverlapWith(int startIndex, int endIndex)
            => endIndex > (SegmentStartIndex + SegmentEndIndex) / 2;
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
        int lightCount = FastIO.ReadNonNegativeInt();
        var solver = new LITE(lightCount);

        int operationCount = FastIO.ReadNonNegativeInt();
        for (int o = 0; o < operationCount; ++o)
        {
            int operation = FastIO.ReadNonNegativeInt();

            if (operation == 0)
            {
                solver.Update(
                    updateStartIndex: FastIO.ReadNonNegativeInt() - 1,
                    updateEndIndex: FastIO.ReadNonNegativeInt() - 1);
            }
            else
            {
                FastIO.WriteNonNegativeInt(solver.Query(
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

    public static void WriteNonNegativeInt(int value)
    {
        int digitCount = 0;
        do
        {
            int digit = value % 10;
            _digitsBuffer[digitCount++] = (byte)(digit + _zero);
            value /= 10;
        } while (value > 0);

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
