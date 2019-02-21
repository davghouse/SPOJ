using System;
using System.IO;

// https://www.spoj.com/problems/EKO/ #binary-search #sorting
// Finds the maximum height for a sawblade while still getting enough wood.
public static class EKO
{
    // Order the trees by height and get the running height sums. Given a tree, you can then figure
    // out (in constant time) if cutting it is necessary or not. If cutting a tree isn't necessary,
    // then setting the blade at exactly the tree's height (so none of the tree gets cut), and
    // cutting down the tops of all the trees to the tree's right will get us enough wood. How much
    // wood is there above the top of a tree? It's the total height of all the trees, minus the tree's
    // height and the height of all the trees shorter than it, minus the height of the tree times the
    // number of trees to its right (those trees only have their tops cut off--their bottoms up the
    // to tree's height aren't cut). If a tree doesn't need to be cut, then neither do any of the
    // trees shorter than it, so we can binary search for the last tree that doesn't need to be cut.
    public static int Solve(int treeCount, int requiredLength, int[] treeHeights)
    {
        Array.Sort(treeHeights);

        long totalHeight = 0;
        long[] runningHeightSums = new long[treeCount];
        for (int t = 0; t < treeCount; ++t)
        {
            totalHeight += treeHeights[t];
            runningHeightSums[t] = totalHeight;
        }

        int? lastTreeNotNeedingCut = BinarySearch.Search(
            start: 0,
            end: treeCount - 1,
            verifier: t => GetLengthAboveTree(
                totalHeight, runningHeightSums, treeHeights, t) >= requiredLength,
            mode: BinarySearch.Mode.TrueToFalse);

        if (lastTreeNotNeedingCut.HasValue)
        {
            long lengthAboveTree = GetLengthAboveTree(
                totalHeight, runningHeightSums, treeHeights, lastTreeNotNeedingCut.Value);
            int treesAfterTree = treeHeights.Length - lastTreeNotNeedingCut.Value - 1;
            long excessLengthAboveTree = lengthAboveTree - requiredLength;
            int treeHeight = treeHeights[lastTreeNotNeedingCut.Value];

            // Set the blade to be the height of the tree, and then move it up in integer increments
            // until moving it up any further wouldn't cut enough wood. Since this is the last tree
            // not needing cut, we know we won't move it up above any trees after this tree. So no
            // matter how much we move it up, we'll lose wood = the move * # trees to the right.
            return treeHeight + (int)(excessLengthAboveTree / treesAfterTree);
        }
        else
        {
            long excessLengthAboveGround = totalHeight - requiredLength;

            // Similar to above, except every tree needs to be cut, so we start from the ground.
            return 0 + (int)(excessLengthAboveGround / treeCount);
        }
    }

    private static long GetLengthAboveTree(
        long totalHeight, long[] runningHeightSums, int[] treeHeights, int tree)
    {
        long lengthAfterTree = totalHeight - runningHeightSums[tree];
        int treesAfterTree = treeHeights.Length - tree - 1;
        long lengthAfterAndUnderTree = treeHeights[tree] * (long)treesAfterTree;

        return lengthAfterTree - lengthAfterAndUnderTree;
    }
}

// This facilitates predicate-based binary searching, where the values being searched on
// satisfy the predicate in an ordered manner, in one of two ways:
// [false false false ... false true ... true true true] (true => anything larger is true)
// [true true true ... true false ... false false false] (true => anything smaller is true)
// In the first, the goal of the search is to locate the smallest value satisfying the predicate.
// In the second, the goal of the search is to locate the largest value satisfying the predicate.
// For more info, see: https://www.topcoder.com/community/data-science/data-science-tutorials/binary-search/.
public static class BinarySearch
{
    public enum Mode
    {
        FalseToTrue,
        TrueToFalse
    };

    public static int? Search(int start, int end, Predicate<int> verifier, Mode mode)
        => mode == Mode.FalseToTrue
        ? SearchFalseToTrue(start, end, verifier)
        : SearchTrueToFalse(start, end, verifier);

    private static int? SearchFalseToTrue(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int mid;

        while (start != end)
        {
            mid = start + (end - start) / 2;

            if (verifier(mid))
            {
                end = mid;
            }
            else
            {
                start = mid + 1;
            }
        }

        return verifier(start) ? start : (int?)null;
    }

    private static int? SearchTrueToFalse(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int mid;

        while (start != end)
        {
            mid = start + (end - start + 1) / 2;

            if (verifier(mid))
            {
                start = mid;
            }
            else
            {
                end = mid - 1;
            }
        }

        return verifier(start) ? start : (int?)null;
    }
}

public static class Program
{
    private static void Main()
    {
        int treeCount = FastIO.ReadNonNegativeInt();
        int requiredLength = FastIO.ReadNonNegativeInt();
        int[] treeHeights = new int[treeCount];
        for (int t = 0; t < treeCount; ++t)
        {
            treeHeights[t] = FastIO.ReadNonNegativeInt();
        }

        FastIO.WriteNonNegativeInt(
            EKO.Solve(treeCount, requiredLength, treeHeights));

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
