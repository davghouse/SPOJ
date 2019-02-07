using System;
using System.IO;

// https://www.spoj.com/problems/CODESPTB/ #binary-search-tree #bit #sorting
// Finds the number of swaps necessary when performing insertion sort.
public static class CODESPTB
{
    private const int _elementLimit = 1000000;

    // Insertion sort looks like this: [sorted part]k[unsorted part]. When adding the
    // kth element to the sorted part, we swap it with however many larger elements
    // there are to its left. The number of larger elements to its left doesn't change
    // as those elements get sorted, so this problem is equivalent to INVCNT. The array
    // size is limited to 100k elements, but that could be 100k * (100k - 1) / 2
    // inversions, so we need to use long when counting. I tried using same code as
    // INVCNT but got TLE. Hints pointed at BIT, and reviewing DQUERY led to the idea.

    // Element values in the array are limited to <= 1 million. Say we're at the ith
    // index in the array. We want to use the BIT to figure out how many values greater
    // than a[i] have already been seen. All we need to do is make sure we've incremented
    // the BIT for each a[j], j before i. Then we can do a range query from a[i]+1 to
    // the limit of a million. For example, say the array is [9 6 9 4 5 1 2]. The BIT
    // goes up to a million. By the time we get the value 5, 9 has been incremented twice,
    // 6 has been incremented once, and 4 has been incremented once. We then sum from
    // 6 (one more than 5) to a million (the limit), and see that 5 is inverted with
    // 3 values to its left (9, 9 and 6, but not 4).
    // ...But what would we do if the limit were much higher? Self-balancing BST?
    public static long Solve(int[] array)
    {
        var elementBIT = new PURQBinaryIndexedTree(
            // Max value (1 million) correponds to max index => array length is +1 of that.
            arrayLength: _elementLimit + 1);
        long inversionCount = 0;

        for (int i = 1; i < array.Length; ++i)
        {
            elementBIT.PointUpdate(array[i - 1], 1);
            inversionCount += elementBIT.SumQuery(array[i] + 1, _elementLimit);
        }

        return inversionCount;
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
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int[] array = new int[FastIO.ReadNonNegativeInt()];
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = FastIO.ReadNonNegativeInt();
            }

            Console.WriteLine(
                CODESPTB.Solve(array));
        }
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but seems like large input.
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
}
