using System;
using System.IO;

// https://www.spoj.com/problems/MULTQ3/ #divide-and-conquer #lazy #segment-tree
// Does range increments and range queries for numbers divisible by 3.
public sealed class MULTQ3 // v1, same big O as v2 but too much overhead to get AC
{
    private readonly MULTQ3LazySegmentTree _segmentTree;

    // The idea is to store how many numbers in a range have remainders of 0, 1, and 2 when divided
    // by 3. Then we can calculate how range increments to that range affect the total count of
    // numbers divisible by 3. For example, if the range increment is 2, any numbers in the range
    // already having a remainder of 1 will have a remainder of 0 after applying the range increment.
    public MULTQ3(int lightCount)
    {
        _segmentTree = new MULTQ3LazySegmentTree(lightCount);
    }

    public void Increment(int updateStartIndex, int updateEndIndex)
        => _segmentTree.Increment(updateStartIndex, updateEndIndex);

    public int Query(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(queryStartIndex, queryEndIndex);
}

public sealed class MULTQ3LazySegmentTree
{
    private readonly IncrementQueryObject[] _treeArray;

    public MULTQ3LazySegmentTree(int arrayLength)
    {
        _treeArray = new IncrementQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(arrayLength) - 1];
        Build(0, 0, arrayLength - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new IncrementQueryObject(segmentStartIndex);
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
        => Query(0, queryStartIndex, queryEndIndex).Remainder0Count;

    // Instead of returning the children object directly, we have to add on the parent's range addition. The children
    // query object knows the subset of the parent segment it intersects with, and everything in there needs the
    // additions that were applied to the parent segment as a whole. It's kind of weird, any pending range additions
    // specifically for the children object gets brought out and added to the sum when we do .Combine or .Sum, but
    // recursively it makes sense: the children object has a sum but still needs to know about the parent's range additions.
    private IncrementQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
    {
        var parentQueryObject = _treeArray[treeArrayIndex];

        if (parentQueryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
            return parentQueryObject;

        bool leftHalfOverlaps = parentQueryObject.DoesLeftHalfOverlapWith(queryStartIndex, queryEndIndex);
        bool rightHalfOverlaps = parentQueryObject.DoesRightHalfOverlapWith(queryStartIndex, queryEndIndex);
        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        IncrementQueryObject childrenQueryObject;

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

        return new IncrementQueryObject(
            childrenQueryObject.SegmentStartIndex,
            childrenQueryObject.SegmentEndIndex,
            childrenQueryObject.Remainder0Count,
            childrenQueryObject.Remainder1Count,
            childrenQueryObject.Remainder2Count)
        {
            RangeIncrements = parentQueryObject.RangeIncrements
        };
    }

    public void Increment(int incrementStartIndex, int incrementEndIndex)
        => Increment(0, incrementStartIndex, incrementEndIndex);

    private void Increment(int treeArrayIndex, int incrementStartIndex, int incrementEndIndex)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.IsTotallyOverlappedBy(incrementStartIndex, incrementEndIndex))
        {
            ++queryObject.RangeIncrements;
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (queryObject.DoesLeftHalfOverlapWith(incrementStartIndex, incrementEndIndex))
        {
            Increment(leftChildTreeArrayIndex, incrementStartIndex, incrementEndIndex);
        }

        if (queryObject.DoesRightHalfOverlapWith(incrementStartIndex, incrementEndIndex))
        {
            Increment(rightChildTreeArrayIndex, incrementStartIndex, incrementEndIndex);
        }

        queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }

    private sealed class IncrementQueryObject
    {
        public int Remainder0Count
        {
            get
            {
                int rangeIncrementRemainder = RangeIncrements % 3;
                return rangeIncrementRemainder == 0 ? Remainder0CountWithoutRangeIncrements
                    : rangeIncrementRemainder == 1 ? Remainder2CountWithoutRangeIncrements
                    : Remainder1CountWithoutRangeIncrements;
            }
        }

        public int Remainder1Count
        {
            get
            {
                int rangeIncrementRemainder = RangeIncrements % 3;
                return rangeIncrementRemainder == 0 ? Remainder1CountWithoutRangeIncrements
                    : rangeIncrementRemainder == 1 ? Remainder0CountWithoutRangeIncrements
                    : Remainder2CountWithoutRangeIncrements;
            }
        }

        public int Remainder2Count
        {
            get
            {
                int rangeIncrementRemainder = RangeIncrements % 3;
                return rangeIncrementRemainder == 0 ? Remainder2CountWithoutRangeIncrements
                    : rangeIncrementRemainder == 1 ? Remainder1CountWithoutRangeIncrements
                    : Remainder0CountWithoutRangeIncrements;
            }
        }

        private int Remainder0CountWithoutRangeIncrements { get; set; }
        private int Remainder1CountWithoutRangeIncrements { get; set; }
        private int Remainder2CountWithoutRangeIncrements { get; set; }
        public int RangeIncrements { get; set; }

        public int SegmentStartIndex { get; }
        public int SegmentEndIndex { get; }
        public int SegmentLength => SegmentEndIndex - SegmentStartIndex + 1;

        public IncrementQueryObject(int index)
        {
            SegmentStartIndex = index;
            SegmentEndIndex = index;
            Remainder0CountWithoutRangeIncrements = 1;
        }

        public IncrementQueryObject(int segmentStartIndex, int segmentEndIndex,
            int remainder0CountWithoutRangeIncrements,
            int remainder1CountWithoutRangeIncrements,
            int remainder2CountWithoutRangeIncrements)
        {
            SegmentStartIndex = segmentStartIndex;
            SegmentEndIndex = segmentEndIndex;
            Remainder0CountWithoutRangeIncrements = remainder0CountWithoutRangeIncrements;
            Remainder1CountWithoutRangeIncrements = remainder1CountWithoutRangeIncrements;
            Remainder2CountWithoutRangeIncrements = remainder2CountWithoutRangeIncrements;
        }

        public IncrementQueryObject Combine(IncrementQueryObject rightAdjacentObject)
            => new IncrementQueryObject(
                segmentStartIndex: SegmentStartIndex,
                segmentEndIndex: rightAdjacentObject.SegmentEndIndex,
                remainder0CountWithoutRangeIncrements: Remainder0Count + rightAdjacentObject.Remainder0Count,
                remainder1CountWithoutRangeIncrements: Remainder1Count + rightAdjacentObject.Remainder1Count,
                remainder2CountWithoutRangeIncrements: Remainder2Count + rightAdjacentObject.Remainder2Count);

        public void Update(IncrementQueryObject updatedLeftChild, IncrementQueryObject updatedRightChild)
        {
            Remainder0CountWithoutRangeIncrements = updatedLeftChild.Remainder0Count + updatedRightChild.Remainder0Count;
            Remainder1CountWithoutRangeIncrements = updatedLeftChild.Remainder1Count + updatedRightChild.Remainder1Count;
            Remainder2CountWithoutRangeIncrements = updatedLeftChild.Remainder2Count + updatedRightChild.Remainder2Count;
        }

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
        int arrayLength = FastIO.ReadNonNegativeInt();
        var solver = new MULTQ3(arrayLength);

        int operationCount = FastIO.ReadNonNegativeInt();
        for (int o = 0; o < operationCount; ++o)
        {
            int operation = FastIO.ReadNonNegativeInt();

            if (operation == 0)
            {
                solver.Increment(
                    updateStartIndex: FastIO.ReadNonNegativeInt(),
                    updateEndIndex: FastIO.ReadNonNegativeInt());
            }
            else
            {
                FastIO.WriteNonNegativeInt(solver.Query(
                    queryStartIndex: FastIO.ReadNonNegativeInt(),
                    queryEndIndex: FastIO.ReadNonNegativeInt()));
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
