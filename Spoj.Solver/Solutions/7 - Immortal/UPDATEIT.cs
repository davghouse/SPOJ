using System;
using System.IO;

// https://www.spoj.com/problems/UPDATEIT/ #bit
// Does a bunch of range updates followed by a bunch of point queries.
public sealed class UPDATEIT
{
    private readonly RUPQBinaryIndexedTree _binaryIndexedTree;

    // I'm using a RUPQ BIT for this, but perhaps there's something better to use
    // since we know the queries come after all the updates?
    public UPDATEIT(int arrayLength)
    {
        _binaryIndexedTree = new RUPQBinaryIndexedTree(arrayLength);
    }

    public void Update(int updateStartIndex, int updateEndIndex, int delta)
        => _binaryIndexedTree.RangeUpdate(updateStartIndex, updateEndIndex, delta);

    public int Query(int queryIndex)
        => _binaryIndexedTree.ValueQuery(queryIndex);
}

// See PURQ before trying to understand this. RUPQ just reinterprets what PURQ is doing without
// actually changing any of the code. We are bound by what PURQ is doing: when a single value is
// updated, the queried value from that index onward is affected (increases by the value added).
// That's because PURQ calculates cumulative sums. Here, we don't want cumulative sums, we want
// to add (the same) value to a range of elements, and query for the total value that's been added
// to a single element. Updating a single element doesn't affect the elements after it. But we must
// find an operation that does, since we're bound by how PURQ works. PURQ is saying "give me an
// index to update and all queries from that index onward will be increased by the same amount."
// RUPQ says "okay, here's an index and a value. We interpret this as ALL elements from that index
// TO THE END of the array get increased by this value, so what you're saying about how the query
// results are affected makes sense, thanks! Luckily, this interpretation allows us to do range updates."
public sealed class RUPQBinaryIndexedTree
{
    private readonly int[] _tree;

    public RUPQBinaryIndexedTree(int arrayLength)
    {
        _tree = new int[arrayLength + 1];
    }

    private void RangeUpdate(int updateStartIndex, int delta)
    {
        for (++updateStartIndex;
            updateStartIndex < _tree.Length;
            updateStartIndex += updateStartIndex & -updateStartIndex)
        {
            _tree[updateStartIndex] += delta;
        }
    }

    // We know conceptually what update is doing; the second line undoes it for the part of the array
    // after the update range that shouldn't have been affected.
    public void RangeUpdate(int updateStartIndex, int updateEndIndex, int delta)
    {
        RangeUpdate(updateStartIndex, delta);
        RangeUpdate(updateEndIndex + 1, -delta);
    }

    public int ValueQuery(int queryIndex)
    {
        int value = 0;
        for (++queryIndex;
            queryIndex > 0;
            queryIndex -= queryIndex & -queryIndex)
        {
            value += _tree[queryIndex];
        }

        return value;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int arrayLength = FastIO.ReadNonNegativeInt();
            int updateCount = FastIO.ReadNonNegativeInt();

            var solver = new UPDATEIT(arrayLength);

            for (int u = 0; u < updateCount; ++u)
            {
                solver.Update(
                    updateStartIndex: FastIO.ReadNonNegativeInt(),
                    updateEndIndex: FastIO.ReadNonNegativeInt(),
                    delta: FastIO.ReadNonNegativeInt());
            }

            int queryCount = FastIO.ReadNonNegativeInt();
            for (int q = 0; q < queryCount; ++q)
            {
                FastIO.WriteNonNegativeInt(solver.Query(
                    queryIndex: FastIO.ReadNonNegativeInt()));
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
