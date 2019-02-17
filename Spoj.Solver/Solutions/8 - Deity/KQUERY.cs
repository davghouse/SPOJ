using System;
using System.IO;
using System.Linq;

// https://www.spoj.com/problems/KQUERY/ #bit #offline #sorting
// Finds the number of elements greater than a (variable) value in subranges of an array.
public static class KQUERY
{
    // This problem is similar to DQUERY, but requires less creativity I think. The
    // way to use the BIT was a lot easier to think of, but I'll still rank it as Deity.
    public static int[] SolveOffline(int[] sourceArray, GreaterThanQuery[] queries)
    {
        int[] queryResults = new int[queries.Length];

        // Sort source array values by descending value, but remember their original index.
        var orderedSourceValues = sourceArray
            .Select((v, i) => new ValueSourceIndex(v, i))
            .ToArray();
        Array.Sort(orderedSourceValues, (vi1, vi2) => vi2.Value.CompareTo(vi1.Value));

        // Sort queries by descending k (a query looks for everything in a range > k).
        Array.Sort(queries, (q1, q2) => q2.GreaterThanLowerLimit.CompareTo(q1.GreaterThanLowerLimit));

        int sourceValuesIndex = 0;
        // Set an index in this BIT to 1 once the source array value at that index is greater
        // than the limit that we're considering for our queries. Queries are ordered descendingly
        // by the limit being considered, so once we set it to 1 the first time, it's good
        // for all future queries (since future queries will have even lower limits).
        var greaterThanLimitBIT = new PURQBinaryIndexedTree(sourceArray.Length);
        foreach (var query in queries)
        {
            while (sourceValuesIndex < sourceArray.Length
                && orderedSourceValues[sourceValuesIndex].Value > query.GreaterThanLowerLimit)
            {
                greaterThanLimitBIT.PointUpdate(
                    orderedSourceValues[sourceValuesIndex].SourceIndex, 1);
                ++sourceValuesIndex;
            }

            queryResults[query.ResultIndex] = greaterThanLimitBIT.SumQuery(
                query.QueryStartIndex, query.QueryEndIndex);
        }

        return queryResults;
    }
}

public struct GreaterThanQuery
{
    public GreaterThanQuery(
        int queryStartIndex,
        int queryEndIndex,
        int greaterThanLowerLimit,
        int resultIndex)
    {
        QueryStartIndex = queryStartIndex;
        QueryEndIndex = queryEndIndex;
        GreaterThanLowerLimit = greaterThanLowerLimit;
        ResultIndex = resultIndex;
    }

    public int QueryStartIndex { get; }
    public int QueryEndIndex { get; }
    public int GreaterThanLowerLimit { get; }
    public int ResultIndex { get; }
}

public struct ValueSourceIndex
{
    public ValueSourceIndex(int value, int sourceIndex)
    {
        Value = value;
        SourceIndex = sourceIndex;
    }

    public int Value { get; }
    public int SourceIndex { get; }
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
    private int SumQuery(int queryEndIndex)
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

public static class Program
{
    private static void Main()
    {
        int sourceArrayLength = FastIO.ReadNonNegativeInt();
        int[] sourceArray = new int[sourceArrayLength];
        for (int i = 0; i < sourceArrayLength; ++i)
        {
            sourceArray[i] = FastIO.ReadNonNegativeInt();
        }

        int queryCount = FastIO.ReadNonNegativeInt();
        var queries = new GreaterThanQuery[queryCount];

        for (int q = 0; q < queryCount; ++q)
        {
            queries[q] = new GreaterThanQuery(
                queryStartIndex: FastIO.ReadNonNegativeInt() - 1,
                queryEndIndex: FastIO.ReadNonNegativeInt() - 1,
                greaterThanLowerLimit: FastIO.ReadNonNegativeInt(),
                resultIndex: q);
        }

        int[] queryResults = KQUERY.SolveOffline(sourceArray, queries);
        foreach (int queryResult in queryResults)
        {
            FastIO.WriteNonNegativeInt(queryResult);
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
