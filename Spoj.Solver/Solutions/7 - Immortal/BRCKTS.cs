using System;
using System.Text;

// https://www.spoj.com/problems/BRCKTS/ #divide-and-conquer #segment-tree
// Determines (across frequent updates) if an expression has balanced brackets.
public sealed class BRCKTS
{
    private readonly string _brackets;
    private readonly ArrayBasedSegmentTree _segmentTree;

    public BRCKTS(string brackets)
    {
        _brackets = brackets;
        _segmentTree = new ArrayBasedSegmentTree(brackets);
    }

    public bool IsBalanced()
        => _segmentTree.Query(0, _brackets.Length - 1);

    public void Flip(int index)
        => _segmentTree.Flip(index);
}

// Most guides online cover this approach, but here's one good one:
// https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
public sealed class ArrayBasedSegmentTree
{
    private readonly string _sourceBrackets;
    private readonly BracketBalanceQueryObject[] _treeArray;

    public ArrayBasedSegmentTree(string sourceBrackets)
    {
        _sourceBrackets = sourceBrackets;
        _treeArray = new BracketBalanceQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceBrackets.Length) - 1];
        Build(0, 0, _sourceBrackets.Length - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new BracketBalanceQueryObject(segmentStartIndex, _sourceBrackets[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public bool Query(int queryStartIndex, int queryEndIndex)
        => Query(0, queryStartIndex, queryEndIndex).IsBalanced;

    private BracketBalanceQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
            return queryObject;

        bool leftHalfOverlaps = queryObject.DoesLeftHalfOverlapWith(queryStartIndex, queryEndIndex);
        bool rightHalfOverlaps = queryObject.DoesRightHalfOverlapWith(queryStartIndex, queryEndIndex);
        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (leftHalfOverlaps && rightHalfOverlaps)
            return Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex)
                .Combine(Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex));
        else if (leftHalfOverlaps)
            return Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        else
            return Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex);
    }

    public void Flip(int flipIndex)
        => Flip(flipIndex, flipIndex);

    public void Flip(int flipStartIndex, int flipEndIndex)
        => Flip(0, flipStartIndex, flipEndIndex);

    private void Flip(int treeArrayIndex, int flipStartIndex, int flipEndIndex)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.SegmentStartIndex == queryObject.SegmentEndIndex)
        {
            queryObject.Flip();
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (queryObject.DoesLeftHalfOverlapWith(flipStartIndex, flipEndIndex))
        {
            Flip(leftChildTreeArrayIndex, flipStartIndex, flipEndIndex);
        }

        if (queryObject.DoesRightHalfOverlapWith(flipStartIndex, flipEndIndex))
        {
            Flip(rightChildTreeArrayIndex, flipStartIndex, flipEndIndex);
        }

        queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }
}

// Given a query range, this stores the result of attempting to balance its brackets. For example,
// the range '()' balances to '', so it has 0 right brackets, and 0 left brackets. The range '(()'
// balances to '(', so it has 0 right brackets, and 1 left bracket. After balancing, ranges are
// always zero or more ')' followed by zero or more '('. We know this because if there's ever a
// '()' subrange in a range, those two brackets must(?) pair with each other during balancing. So you
// can imagine removing them from the range, which might create a new '()' subrange, and so on,
// until there's no '()' subrange, meaning no ')' can exist to the right of any '(' after attempting
// to balance. So when we're left with ranges effectively like '))(((' and '))(', it's easy to see
// how to combine them into a new range (which we must be able to do to use a segment tree). The
// ')' from the left and the '(' from the right stick around, but some of the '(' from the
// left and ')' from the right can cancel out, either all the way, or to some number of ')' or '('.
public sealed class BracketBalanceQueryObject
{
    private BracketBalanceQueryObject()
    { }

    private BracketBalanceQueryObject(int segmentStartIndex, int segmentEndIndex)
    {
        SegmentStartIndex = segmentStartIndex;
        SegmentEndIndex = segmentEndIndex;
    }

    public BracketBalanceQueryObject(int index, char bracket)
    {
        SegmentStartIndex = SegmentEndIndex = index;

        if (bracket == ')')
        {
            RightBracketCount = 1;
        }
        else // '('
        {
            LeftBracketCount = 1;
        }
    }

    public void Flip()
    {
        if (RightBracketCount == 1)
        {
            RightBracketCount = 0;
            LeftBracketCount = 1;
        }
        else
        {
            RightBracketCount = 1;
            LeftBracketCount = 0;
        }
    }

    public int SegmentStartIndex { get; }
    public int SegmentEndIndex { get; }

    public bool IsBalanced
        => RightBracketCount == 0 && LeftBracketCount == 0;

    public int RightBracketCount { get; private set; }
    public int LeftBracketCount { get; private set; }

    public BracketBalanceQueryObject Combine(BracketBalanceQueryObject rightAdjacentObject)
    {
        var queryObject = new BracketBalanceQueryObject(SegmentStartIndex, rightAdjacentObject.SegmentEndIndex)
        {
            RightBracketCount = RightBracketCount,
            LeftBracketCount = rightAdjacentObject.LeftBracketCount
        };

        if (LeftBracketCount > rightAdjacentObject.RightBracketCount)
        {
            queryObject.LeftBracketCount += LeftBracketCount - rightAdjacentObject.RightBracketCount;
        }
        else if (LeftBracketCount < rightAdjacentObject.RightBracketCount)
        {
            queryObject.RightBracketCount += rightAdjacentObject.RightBracketCount - LeftBracketCount;
        }

        return queryObject;
    }

    public void Update(BracketBalanceQueryObject updatedLeftChild, BracketBalanceQueryObject updatedRightChild)
    {
        RightBracketCount = updatedLeftChild.RightBracketCount;
        LeftBracketCount = updatedRightChild.LeftBracketCount;

        if (updatedLeftChild.LeftBracketCount > updatedRightChild.RightBracketCount)
        {
            LeftBracketCount += updatedLeftChild.LeftBracketCount - updatedRightChild.RightBracketCount;
        }
        else if (updatedLeftChild.LeftBracketCount < updatedRightChild.RightBracketCount)
        {
            RightBracketCount += updatedRightChild.RightBracketCount - updatedLeftChild.LeftBracketCount;
        }
    }

    // The given range starts before the segment starts and ends after the segment ends.
    public bool IsTotallyOverlappedBy(int startIndex, int endIndex)
        => startIndex <= SegmentStartIndex && endIndex >= SegmentEndIndex;

    // Assumed that some overlap exists, just not necessarily over the left half.
    public bool DoesLeftHalfOverlapWith(int startIndex, int endIndex)
        => startIndex <= (SegmentStartIndex + SegmentEndIndex) / 2;

    // Assumed that some overlap exists, just not necessarily over the right half.
    public bool DoesRightHalfOverlapWith(int startIndex, int endIndex)
        => endIndex > (SegmentStartIndex + SegmentEndIndex) / 2;
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
        for (int t = 1; t <= 10; ++t)
        {
            output.AppendLine($"Test {t}:");

            int bracketCount = int.Parse(Console.ReadLine());
            string brackets = Console.ReadLine().Substring(0, bracketCount);
            int operationCount = int.Parse(Console.ReadLine());

            var solver = new BRCKTS(brackets);
            for (int o = 1; o <= operationCount; ++o)
            {
                int operation = int.Parse(Console.ReadLine());
                if (operation == 0)
                {
                    output.AppendLine(
                        solver.IsBalanced() ? "YES" : "NO");
                }
                else
                {
                    solver.Flip(operation - 1);
                }
            }
        }

        Console.Write(output);
    }
}
