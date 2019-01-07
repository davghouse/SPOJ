using System;
using System.IO;

// https://www.spoj.com/problems/CAM5/ #dfs #disjoint-sets
// Determines the number of friend components in a graph of peers.
public sealed class CAM5
{
    private readonly DisjointSets _disjointSets;

    // This can be solved by DFSing to find the number of connected components in the
    // peer graph, but I went with a disjoint sets solution for the experience.
    public CAM5(int peerCount)
    {
        _disjointSets = new DisjointSets(peerCount);
    }

    public void AddFriendAssociation(int firstPeer, int secondPeer)
        => _disjointSets.UnionSets(firstPeer, secondPeer);

    public int Solve()
        => _disjointSets.DisjointSetCount;
}

// https://en.wikipedia.org/wiki/Disjoint-set_data_structure
// https://www.youtube.com/watch?v=gcmjC-OcWpI
public sealed class DisjointSets
{
    // Can be easily split into two arrays, but let's go with this huge name instead.
    private readonly int[] _elementsParentsOrNegatedSubsetSizes;

    public DisjointSets(int elementCount)
    {
        _elementsParentsOrNegatedSubsetSizes = new int[elementCount];
        for (int i = 0; i < elementCount; ++i)
        {
            _elementsParentsOrNegatedSubsetSizes[i] = -1;
        }

        ElementCount = DisjointSetCount = elementCount;
    }

    public int ElementCount { get; }
    public int DisjointSetCount { get; private set; }

    // Consider the following example:
    // elements:                                   0  1 2  3 4
    // elements' parents or negated subset sizes: -1 -3 1 -1 2
    // Elements with negative numbers are the roots of their sets. The value of the number
    // is the size of the set--so 0 is the root of a set of size 1 (itself), and 1 is the
    // root of a set of size 3. Elements with positive numbers are pointing towards their
    // root. 2 points towards 1 (which we know is the root), and 4 points towards 2, etc.
    private int FindRoot(int element)
    {
        int parentOrNegatedSubsetSize = _elementsParentsOrNegatedSubsetSizes[element];

        return parentOrNegatedSubsetSize >= 0
            // Follow the path towards the parent, and compress. If we run FindParent(4)
            // from the above example, 4 goes to 2 which goes to 1 which returns itself.
            // We compress by setting 4's parent to 1, so it finds it directly next time.
            ? _elementsParentsOrNegatedSubsetSizes[element] = FindRoot(parentOrNegatedSubsetSize)
            // If negative (so a negated subset size), that means it is its own parent.
            : element;
    }

    public void UnionSets(int firstElement, int secondElement)
    {
        int firstRoot = FindRoot(firstElement);
        int secondRoot = FindRoot(secondElement);

        if (firstRoot == secondRoot)
            return;

        int firstSetSize = -1 * _elementsParentsOrNegatedSubsetSizes[firstRoot];
        int secondSetSize = -1* _elementsParentsOrNegatedSubsetSizes[secondRoot];

        int biggerRoot = firstSetSize > secondSetSize ? firstRoot : secondRoot;
        int smallerRoot = firstSetSize > secondSetSize ? secondRoot : firstRoot;

        _elementsParentsOrNegatedSubsetSizes[smallerRoot] = biggerRoot;
        _elementsParentsOrNegatedSubsetSizes[biggerRoot] = -1 * (firstSetSize + secondSetSize);

        --DisjointSetCount;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            var solver = new CAM5(peerCount: FastIO.ReadNonNegativeInt());

            int friendCount = FastIO.ReadNonNegativeInt();
            for (int i = 0; i < friendCount; ++i)
            {
                solver.AddFriendAssociation(
                    firstPeer: FastIO.ReadNonNegativeInt(),
                    secondPeer: FastIO.ReadNonNegativeInt());
            }

            FastIO.WriteNonNegativeInt(solver.Solve());
            FastIO.WriteLine();
        }

        FastIO.Flush();
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but the problem came with an IO warning.
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
