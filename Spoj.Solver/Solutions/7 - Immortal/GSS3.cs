using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/GSS3/ #divide-and-conquer #segment-tree
// Does element updates and maximum sum subrange queries on an array.
public sealed class GSS3
{
    private readonly ArrayBasedSegmentTree _segmentTree;

    public GSS3(IReadOnlyList<int> sourceArray)
    {
        _segmentTree = new ArrayBasedSegmentTree(sourceArray);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(queryStartIndex, queryEndIndex);

    public void Update(int updateIndex, int newValue)
        => _segmentTree.Update(updateIndex, newValue);
}

// Most guides online cover this approach, but here's one good one:
// https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
public sealed class ArrayBasedSegmentTree
{
    private readonly IReadOnlyList<int> _sourceArray;
    private readonly MaximumSumQueryObject[] _treeArray;

    public ArrayBasedSegmentTree(IReadOnlyList<int> sourceArray)
    {
        _sourceArray = sourceArray;
        _treeArray = new MaximumSumQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Count) - 1];
        Build(0, 0, _sourceArray.Count - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new MaximumSumQueryObject(segmentStartIndex, _sourceArray[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2);
        Build(rightChildTreeArrayIndex, (segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
        => Query(0, queryStartIndex, queryEndIndex).MaximumSum;

    private MaximumSumQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
            return queryObject;

        bool isLeftHalfOverlapped = queryObject.IsLeftHalfOverlappedBy(queryStartIndex, queryEndIndex);
        bool isRightHalfOverlapped = queryObject.IsRightHalfOverlappedBy(queryStartIndex, queryEndIndex);
        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (isLeftHalfOverlapped && isRightHalfOverlapped)
            return Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex)
                .Combine(Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex));
        else if (isLeftHalfOverlapped)
            return Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        else
            return Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex);
    }

    public void Update(int updateIndex, int newValue)
        => Update(0, updateIndex, newValue);

    private void Update(int treeArrayIndex, int updateIndex, int newValue)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.SegmentStartIndex == queryObject.SegmentEndIndex)
        {
            queryObject.Reinitialize(newValue);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (queryObject.IsLeftHalfOverlappedBy(updateIndex, updateIndex))
        {
            Update(leftChildTreeArrayIndex, updateIndex, newValue);
        }
        else // Some overlap must exist, so it's over the right half.
        {
            Update(rightChildTreeArrayIndex, updateIndex, newValue);
        }

        queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }
}

// Given a query range, this stores the maximum sum for any contiguous subrange.
public sealed class MaximumSumQueryObject
{
    private MaximumSumQueryObject()
    { }

    public MaximumSumQueryObject(int index, int value)
    {
        SegmentStartIndex = SegmentEndIndex = index;
        Sum = MaximumSum = MaximumLeftStartingSum = MaximumRightStartingSum = value;
    }

    public void Reinitialize(int value)
        => Sum = MaximumSum = MaximumLeftStartingSum = MaximumRightStartingSum = value;

    // 'Readonly' property for the start index of the array range this query object corresponds to.
    public int SegmentStartIndex { get; private set; }

    // 'Readonly' property for the end index of the array range this query object corresponds to.
    public int SegmentEndIndex { get; private set; }

    private int Sum { get; set; }
    public int MaximumSum { get; private set; }
    private int MaximumLeftStartingSum { get; set; }  // [-> ... ]
    private int MaximumRightStartingSum { get; set; } // [ ... <-]

    public MaximumSumQueryObject Combine(MaximumSumQueryObject rightAdjacentObject)
        => new MaximumSumQueryObject()
        {
            SegmentStartIndex = SegmentStartIndex,
            SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
            Sum = GetCombinedSum(this, rightAdjacentObject),
            MaximumSum = GetCombinedMaximumSum(this, rightAdjacentObject),
            MaximumLeftStartingSum = GetCombinedMaximumLeftStartingSum(this, rightAdjacentObject),
            MaximumRightStartingSum = GetCombinedMaximumRightStartingSum(this, rightAdjacentObject)
        };

    public void Update(MaximumSumQueryObject updatedLeftChild, MaximumSumQueryObject updatedRightChild)
    {
        Sum = GetCombinedSum(updatedLeftChild, updatedRightChild);
        MaximumSum = GetCombinedMaximumSum(updatedLeftChild, updatedRightChild);
        MaximumLeftStartingSum = GetCombinedMaximumLeftStartingSum(updatedLeftChild, updatedRightChild);
        MaximumRightStartingSum = GetCombinedMaximumRightStartingSum(updatedLeftChild, updatedRightChild);
    }

    private static int GetCombinedSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
        // The sum is just the sum of both.
        => leftAdjacentObject.Sum + rightAdjacentObject.Sum;

    private static int GetCombinedMaximumSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
        // The maximum sum either intersects both segments, or is entirely in one.
        => Math.Max(
            leftAdjacentObject.MaximumRightStartingSum + rightAdjacentObject.MaximumLeftStartingSum,
            Math.Max(leftAdjacentObject.MaximumSum, rightAdjacentObject.MaximumSum));

    private static int GetCombinedMaximumLeftStartingSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
        // The maximum left starting sum starts at the left, and may or may not cross into the right.
        => Math.Max(
            leftAdjacentObject.Sum + rightAdjacentObject.MaximumLeftStartingSum,
            leftAdjacentObject.MaximumLeftStartingSum);

    private static int GetCombinedMaximumRightStartingSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
        // The maximum right starting sum starts at the right, and may or may not cross into the left.
        => Math.Max(
            rightAdjacentObject.Sum + leftAdjacentObject.MaximumRightStartingSum,
            rightAdjacentObject.MaximumRightStartingSum);

    // The given range starts before the segment starts and ends after the segment ends.
    public bool IsTotallyOverlappedBy(int startIndex, int endIndex)
        => startIndex <= SegmentStartIndex && endIndex >= SegmentEndIndex;

    // Assumed that some overlap exists, just not necessarily over the left half.
    public bool IsLeftHalfOverlappedBy(int startIndex, int endIndex)
        => startIndex <= (SegmentStartIndex + SegmentEndIndex) / 2;

    // Assumed that some overlap exists, just not necessarily over the right half.
    public bool IsRightHalfOverlappedBy(int startIndex, int endIndex)
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
        int arrayLength = int.Parse(Console.ReadLine());
        int[] sourceArray = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
        var solver = new GSS3(sourceArray);

        var output = new StringBuilder();
        int operationCount = int.Parse(Console.ReadLine());
        for (int o = 0; o < operationCount; ++o)
        {
            int[] operation = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);

            if (operation[0] == 0)
            {
                solver.Update(
                    updateIndex: operation[1] - 1,
                    newValue: operation[2]);
            }
            else
            {
                output.Append(solver.Query(
                    queryStartIndex: operation[1] - 1,
                    queryEndIndex: operation[2] - 1));
                output.AppendLine();
            }
        }

        Console.Write(output);
    }
}
