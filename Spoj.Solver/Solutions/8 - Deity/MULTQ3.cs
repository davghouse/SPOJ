using System;
using System.IO;

// https://www.spoj.com/problems/MULTQ3/ #divide-and-conquer #lazy #segment-tree
// Does range increments and range queries for numbers divisible by 3.
public sealed class MULTQ3LazySegmentTree
{
    private readonly int _sourceArrayLength;
    private readonly IncrementQueryObject[] _treeArray;

    // This solution required a lot of weird optimizations. After struggling on my own,
    // I based the optimizations off of https://github.com/cacophonix/SPOJ/blob/master/MULTQ3.cpp.
    // Here's my original, cleaner code with the same runtime complexity:
    // https://gist.github.com/davghouse/9823c61bee36232cd161475e124195fe.
    // For the problem the idea is to store how many numbers in a range have remainders of 0,
    // 1, and 2 when divided by 3. Then we can calculate how range increments to that range
    // affect the total count of numbers divisible by 3. For example, if the range has been
    // incremented 4 times, any numbers in the range with a remainder of 2 will have a remainder
    // of 0 after the range is incremented.
    public MULTQ3LazySegmentTree(int arrayLength)
    {
        _sourceArrayLength = arrayLength;
        _treeArray = new IncrementQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(arrayLength) - 1];
        Build(0, 0, _sourceArrayLength - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new IncrementQueryObject();
            _treeArray[treeArrayIndex].Remainder0Count = 1;
            return;
        }

        int leftChildTreeArrayIndex = (treeArrayIndex << 1) | 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex].Remainder0Count
            = _treeArray[leftChildTreeArrayIndex].Remainder0Count
            + _treeArray[rightChildTreeArrayIndex].Remainder0Count;
    }

    public int Query(
        int treeArrayIndex, int segmentStartIndex, int segmentEndIndex,
        int queryStartIndex, int queryEndIndex, int rangeIncrements)
    {
        if (queryStartIndex <= segmentStartIndex && queryEndIndex >= segmentEndIndex)
        {
            switch (rangeIncrements % 3)
            {
                case 0: return _treeArray[treeArrayIndex].Remainder0Count;
                case 1: return _treeArray[treeArrayIndex].Remainder2Count;
                case 2: return _treeArray[treeArrayIndex].Remainder1Count;
            }
        }

        rangeIncrements += _treeArray[treeArrayIndex].RangeIncrements;
        int leftChildTreeArrayIndex = (treeArrayIndex << 1) | 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) >> 1;

        if (queryStartIndex <= leftChildSegmentEndIndex && queryEndIndex > leftChildSegmentEndIndex)
            return Query(
                leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex,
                queryStartIndex, queryEndIndex, rangeIncrements)
                + Query(
                    rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex,
                    queryStartIndex, queryEndIndex, rangeIncrements);
        else if (queryStartIndex <= leftChildSegmentEndIndex)
            return Query(
                leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex,
                queryStartIndex, queryEndIndex, rangeIncrements);
        else
            return Query(
                rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex,
                queryStartIndex, queryEndIndex, rangeIncrements);
    }

    public void Increment(
        int treeArrayIndex, int segmentStartIndex, int segmentEndIndex,
        int incrementStartIndex, int incrementEndIndex)
    {
        if (incrementStartIndex <= segmentStartIndex && incrementEndIndex >= segmentEndIndex)
        {
            _treeArray[treeArrayIndex].RangeIncrements++;
            _treeArray[treeArrayIndex].Swap0And1RemainderCounts();
            _treeArray[treeArrayIndex].Swap0And2RemainderCounts();
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

        _treeArray[treeArrayIndex].Remainder0Count =
            _treeArray[leftChildTreeArrayIndex].Remainder0Count + _treeArray[rightChildTreeArrayIndex].Remainder0Count;
        _treeArray[treeArrayIndex].Remainder1Count =
            _treeArray[leftChildTreeArrayIndex].Remainder1Count + _treeArray[rightChildTreeArrayIndex].Remainder1Count;
        _treeArray[treeArrayIndex].Remainder2Count =
            _treeArray[leftChildTreeArrayIndex].Remainder2Count + _treeArray[rightChildTreeArrayIndex].Remainder2Count;
        switch (_treeArray[treeArrayIndex].RangeIncrements % 3)
        {
            case 1:
                _treeArray[treeArrayIndex].Swap0And1RemainderCounts();
                _treeArray[treeArrayIndex].Swap0And2RemainderCounts();
                break;
            case 2:
                _treeArray[treeArrayIndex].Swap0And1RemainderCounts();
                _treeArray[treeArrayIndex].Swap1And2RemainderCounts();
                break;
        }
    }

    public struct IncrementQueryObject
    {
        public int Remainder0Count { get; set; }
        public int Remainder1Count { get; set; }
        public int Remainder2Count { get; set; }
        public int RangeIncrements { get; set; }

        public void Swap0And1RemainderCounts()
        {
            int oldRemainder1Count = Remainder1Count;
            Remainder1Count = Remainder0Count;
            Remainder0Count = oldRemainder1Count;
        }

        public void Swap0And2RemainderCounts()
        {
            int oldRemainder2Count = Remainder2Count;
            Remainder2Count = Remainder0Count;
            Remainder0Count = oldRemainder2Count;
        }

        public void Swap1And2RemainderCounts()
        {
            int oldRemainder2Count = Remainder2Count;
            Remainder2Count = Remainder1Count;
            Remainder1Count = oldRemainder2Count;
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
        int arrayLengthMinus1 = arrayLength - 1;
        var solver = new MULTQ3LazySegmentTree(arrayLength);

        int operationCount = FastIO.ReadNonNegativeInt();
        for (int o = 0; o < operationCount; ++o)
        {
            int operation = FastIO.ReadNonNegativeInt();

            if (operation == 0)
            {
                solver.Increment(
                    treeArrayIndex: 0, segmentStartIndex: 0, segmentEndIndex: arrayLengthMinus1,
                    incrementStartIndex: FastIO.ReadNonNegativeInt(),
                    incrementEndIndex: FastIO.ReadNonNegativeInt());
            }
            else
            {
                FastIO.WriteNonNegativeInt(solver.Query(
                    treeArrayIndex: 0, segmentStartIndex: 0, segmentEndIndex: arrayLengthMinus1,
                    queryStartIndex: FastIO.ReadNonNegativeInt(),
                    queryEndIndex: FastIO.ReadNonNegativeInt(),
                    rangeIncrements: 0));
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
