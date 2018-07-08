using System;
using System.Collections.Generic;
using System.Text;

// http://www.spoj.com/problems/KGSS/ #divide-and-conquer #research #segment-tree
// Does element updates and second maximum (sum) subrange queries on an array (using a segment tree).
public sealed class KGSS
{
    private readonly ArrayBasedSegmentTree _segmentTree;

    public KGSS(IReadOnlyList<int> sourceArray)
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
    private readonly SecondMaximumQueryObject[] _treeArray;

    public ArrayBasedSegmentTree(IReadOnlyList<int> sourceArray)
    {
        _sourceArray = sourceArray;
        _treeArray = new SecondMaximumQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Count) - 1];
        Build(0, 0, _sourceArray.Count - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new SecondMaximumQueryObject(segmentStartIndex, _sourceArray[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2);
        Build(rightChildTreeArrayIndex, (segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
    {
        var result = Query(0, queryStartIndex, queryEndIndex);
        return result.Maximum + (result.SecondMaximum ?? 0);
    }

    private SecondMaximumQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
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

// Given a query range, this stores the maximum and second maximum.
public sealed class SecondMaximumQueryObject
{
    private SecondMaximumQueryObject()
    { }

    public SecondMaximumQueryObject(int index, int value)
    {
        SegmentStartIndex = SegmentEndIndex = index;
        Maximum = value;
    }

    public void Reinitialize(int value)
        => Maximum = value;

    // 'Readonly' property for the start index of the array range this query object corresponds to.
    public int SegmentStartIndex { get; private set; }

    // 'Readonly' property for the end index of the array range this query object corresponds to.
    public int SegmentEndIndex { get; private set; }

    public int Maximum { get; private set; }
    public int? SecondMaximum { get; private set; }

    public SecondMaximumQueryObject Combine(SecondMaximumQueryObject rightAdjacentObject)
        => new SecondMaximumQueryObject
        {
            SegmentStartIndex = SegmentStartIndex,
            SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
            Maximum = GetCombinedMaximum(this, rightAdjacentObject),
            SecondMaximum = GetCombinedSecondMaximum(this, rightAdjacentObject)
        };

    public void Update(SecondMaximumQueryObject updatedLeftChild, SecondMaximumQueryObject updatedRightChild)
    {
        Maximum = GetCombinedMaximum(updatedLeftChild, updatedRightChild);
        SecondMaximum = GetCombinedSecondMaximum(updatedLeftChild, updatedRightChild);
    }

    private static int GetCombinedMaximum(SecondMaximumQueryObject leftAdjacentObject, SecondMaximumQueryObject rightAdjacentObject)
        => Math.Max(leftAdjacentObject.Maximum, rightAdjacentObject.Maximum);

    private static int GetCombinedSecondMaximum(SecondMaximumQueryObject leftAdjacentObject, SecondMaximumQueryObject rightAdjacentObject)
        => Math.Max(
            Math.Min(leftAdjacentObject.Maximum, rightAdjacentObject.Maximum),
            Math.Max(leftAdjacentObject.SecondMaximum ?? 0, rightAdjacentObject.SecondMaximum ?? 0));

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
        int[] sourceArray = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        var solver = new KGSS(sourceArray);

        var output = new StringBuilder();
        int operationCount = int.Parse(Console.ReadLine());
        for (int o = 0; o < operationCount; ++o)
        {
            string[] operation = Console.ReadLine().Split();

            if (operation[0] == "Q")
            {
                output.Append(solver.Query(
                    queryStartIndex: int.Parse(operation[1]) - 1,
                    queryEndIndex: int.Parse(operation[2]) - 1));
                output.AppendLine();
            }
            else
            {
                solver.Update(
                    updateIndex: int.Parse(operation[1]) - 1,
                    newValue: int.Parse(operation[2]));
            }
        }

        Console.Write(output);
    }
}
