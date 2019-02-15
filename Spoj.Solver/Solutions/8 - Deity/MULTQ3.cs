using System;
using System.IO;

// https://www.spoj.com/problems/MULTQ3/ #divide-and-conquer #lazy #segment-tree
// Does range increments and range queries for numbers divisible by 3.
public sealed class MULTQ3
{
    private readonly int _arrayEndIndex;
    private readonly MULTQ3LazySegmentTree _segmentTree;

    public MULTQ3(int arrayLength)
    {
        _arrayEndIndex = arrayLength - 1;
        _segmentTree = new MULTQ3LazySegmentTree(arrayLength);
    }

    public void Increment(int incrementStartIndex, int incrementEndIndex)
        => _segmentTree.Increment(0, 0, _arrayEndIndex, incrementStartIndex, incrementEndIndex);

    public int Query(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(0, 0, _arrayEndIndex, queryStartIndex, queryEndIndex).Remainder0Count;
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
            _treeArray[treeArrayIndex] = new IncrementQueryObject { Remainder0Count = 1 };
            return;
        }

        int leftChildTreeArrayIndex = (treeArrayIndex << 1) | 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    // Instead of returning the children object directly, we have to add on the parent's range addition. The children
    // query object knows the subset of the parent segment it intersects with, and everything in there needs the
    // additions that were applied to the parent segment as a whole. It's kind of weird, any pending range additions
    // specifically for the children object gets brought out and added to the sum when we do .Combine or .Sum, but
    // recursively it makes sense: the children object has a sum but still needs to know about the parent's range additions.
    public IncrementQueryObject Query(
        int treeArrayIndex, int segmentStartIndex, int segmentEndIndex,
        int queryStartIndex, int queryEndIndex)
    {
        var parentQueryObject = _treeArray[treeArrayIndex];

        if (queryStartIndex <= segmentStartIndex && queryEndIndex >= segmentEndIndex)
            return parentQueryObject;

        int leftChildTreeArrayIndex = (treeArrayIndex << 1) | 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;
        IncrementQueryObject childrenQueryObject;

        if (queryStartIndex <= leftChildSegmentEndIndex && queryEndIndex > leftChildSegmentEndIndex)
        {
            childrenQueryObject = Query(
                leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex,
                queryStartIndex, queryEndIndex)
                .Combine(Query(
                    rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex,
                    queryStartIndex, queryEndIndex));
        }
        else if (queryStartIndex <= leftChildSegmentEndIndex)
        {
            childrenQueryObject = Query(
                leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex,
                queryStartIndex, queryEndIndex);
        }
        else
        {
            childrenQueryObject = Query(
                rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex,
                queryStartIndex, queryEndIndex);
        }

        var queryObject = new IncrementQueryObject
        {
            RangeIncrements = parentQueryObject.RangeIncrements
        };
        queryObject.SubsumeRemainders(childrenQueryObject);

        return queryObject;
    }

    public void Increment(
        int treeArrayIndex, int segmentStartIndex, int segmentEndIndex,
        int incrementStartIndex, int incrementEndIndex)
    {
        if (incrementStartIndex <= segmentStartIndex && incrementEndIndex >= segmentEndIndex)
        {
            ++_treeArray[treeArrayIndex].RangeIncrements;
            return;
        }

        int leftChildTreeArrayIndex = (treeArrayIndex << 1) | 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        if (incrementStartIndex <= leftChildSegmentEndIndex)
        {
            Increment(
                leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex,
                incrementStartIndex, incrementEndIndex);
        }

        if (incrementEndIndex > leftChildSegmentEndIndex)
        {
            Increment(
                rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex,
                incrementStartIndex, incrementEndIndex);
        }

        _treeArray[treeArrayIndex].Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }

    // The idea is to store how many numbers in a range have remainders of 0, 1, and 2 when divided
    // by 3. Then we can calculate how range increments to that range affect the total count of
    // numbers divisible by 3. For example, if the range increment is 2, any numbers in the range
    // already having a remainder of 1 will have a remainder of 0 after applying the range increment.
    // The tricky part is doing the calculates fast enough to get AC.
    public struct IncrementQueryObject
    {
        public int Remainder0Count
        {
            get
            {
                switch (RangeIncrements % 3)
                {
                    case 0: return _remainder0CountWithoutRangeIncrements;
                    case 1: return _remainder2CountWithoutRangeIncrements;
                    case 2: return _remainder1CountWithoutRangeIncrements;
                    default: return 0;
                }
            }
            set
            {
                _remainder0CountWithoutRangeIncrements = value;
            }
        }

        private int _remainder0CountWithoutRangeIncrements;
        private int _remainder1CountWithoutRangeIncrements;
        private int _remainder2CountWithoutRangeIncrements;
        public int RangeIncrements;

        public IncrementQueryObject Combine(IncrementQueryObject rightAdjacentObject)
        {
            var combinedQueryObject = new IncrementQueryObject();
            combinedQueryObject.SubsumeRemainders(this);
            combinedQueryObject.SubsumeRemainders(rightAdjacentObject);

            return combinedQueryObject;
        }

        public void Update(IncrementQueryObject updatedLeftChild, IncrementQueryObject updatedRightChild)
        {
            // Zero these out first as we want the total of the children's updated contributions.
            _remainder0CountWithoutRangeIncrements
                = _remainder1CountWithoutRangeIncrements
                = _remainder2CountWithoutRangeIncrements = 0;
            SubsumeRemainders(updatedLeftChild);
            SubsumeRemainders(updatedRightChild);
        }

        public void SubsumeRemainders(IncrementQueryObject child)
        {
            switch (child.RangeIncrements % 3)
            {
                case 0:
                    _remainder0CountWithoutRangeIncrements += child._remainder0CountWithoutRangeIncrements;
                    _remainder1CountWithoutRangeIncrements += child._remainder1CountWithoutRangeIncrements;
                    _remainder2CountWithoutRangeIncrements += child._remainder2CountWithoutRangeIncrements;
                    break;
                case 1:
                    _remainder0CountWithoutRangeIncrements += child._remainder2CountWithoutRangeIncrements;
                    _remainder1CountWithoutRangeIncrements += child._remainder0CountWithoutRangeIncrements;
                    _remainder2CountWithoutRangeIncrements += child._remainder1CountWithoutRangeIncrements;
                    break;
                case 2:
                    _remainder0CountWithoutRangeIncrements += child._remainder1CountWithoutRangeIncrements;
                    _remainder1CountWithoutRangeIncrements += child._remainder2CountWithoutRangeIncrements;
                    _remainder2CountWithoutRangeIncrements += child._remainder0CountWithoutRangeIncrements;
                    break;
            }
        }
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
                    incrementStartIndex: FastIO.ReadNonNegativeInt(),
                    incrementEndIndex: FastIO.ReadNonNegativeInt());
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
