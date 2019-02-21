using System;
using System.Text;

// https://www.spoj.com/problems/HORRIBLE/ #divide-and-conquer #lazy #segment-tree
// Answers range sum queries and performs range additions.
public sealed class HORRIBLE // v1, using a segment tree
{
    private readonly LazySumSegmentTree _segmentTree;

    public HORRIBLE(int arrayLength)
    {
        _segmentTree = new LazySumSegmentTree(arrayLength);
    }

    public void Update(int updateStartIndex, int updateEndIndex, int rangeAddition)
        => _segmentTree.Update(updateStartIndex, updateEndIndex, rangeAddition);

    public long Query(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(queryStartIndex, queryEndIndex);
}

public sealed class LazySumSegmentTree
{
    private readonly SumQueryObject[] _treeArray;

    public LazySumSegmentTree(int arrayLength)
    {
        _treeArray = new SumQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(arrayLength) - 1];
        Build(0, 0, arrayLength - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new SumQueryObject(segmentStartIndex, 0);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public long Query(int queryStartIndex, int queryEndIndex)
        => Query(0, queryStartIndex, queryEndIndex).Sum;

    // Instead of returning the children object directly, we have to add on the parent's range addition. The
    // children query object knows the subset of the parent segment it intersects with, and everything in
    // there needs the additions that were applied to the parent segment as a whole. It's kind of weird, any
    // pending range additions specifically for the children object gets brought out and added to the sum when
    // we do .Combine or .Sum, but recursively it makes sense: the children object has a sum but still needs
    // to know about the parent's range additions.
    private SumQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
    {
        var parentQueryObject = _treeArray[treeArrayIndex];

        if (parentQueryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
            return parentQueryObject;

        bool leftHalfOverlaps = parentQueryObject.DoesLeftHalfOverlapWith(queryStartIndex, queryEndIndex);
        bool rightHalfOverlaps = parentQueryObject.DoesRightHalfOverlapWith(queryStartIndex, queryEndIndex);
        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        SumQueryObject childrenQueryObject;

        if (leftHalfOverlaps && rightHalfOverlaps)
        {
            childrenQueryObject = Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex)
                .Combine(Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex));
        }
        else if (leftHalfOverlaps)
        {
            childrenQueryObject = Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        }
        else
        {
            childrenQueryObject = Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        }

        return new SumQueryObject(
            childrenQueryObject.SegmentStartIndex,
            childrenQueryObject.SegmentEndIndex,
            childrenQueryObject.Sum)
        {
            RangeAddition = parentQueryObject.RangeAddition
        };
    }

    public void Update(int updateIndex, long rangeAddition)
        => Update(updateIndex, updateIndex, rangeAddition);

    public void Update(int updateStartIndex, int updateEndIndex, long rangeAddition)
        => Update(0, updateStartIndex, updateEndIndex, rangeAddition);

    private void Update(int treeArrayIndex, int updateStartIndex, int updateEndIndex, long rangeAddition)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.IsTotallyOverlappedBy(updateStartIndex, updateEndIndex))
        {
            queryObject.Update(rangeAddition);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (queryObject.DoesLeftHalfOverlapWith(updateStartIndex, updateEndIndex))
        {
            Update(leftChildTreeArrayIndex, updateStartIndex, updateEndIndex, rangeAddition);
        }

        if (queryObject.DoesRightHalfOverlapWith(updateStartIndex, updateEndIndex))
        {
            Update(rightChildTreeArrayIndex, updateStartIndex, updateEndIndex, rangeAddition);
        }

        queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }

    private sealed class SumQueryObject
    {
        public long Sum
            => SumWithoutRangeAddition + RangeAddition * SegmentLength;

        private long SumWithoutRangeAddition { get; set; }
        public long RangeAddition { get; set; }

        public int SegmentStartIndex { get; }
        public int SegmentEndIndex { get; }
        public int SegmentLength => SegmentEndIndex - SegmentStartIndex + 1;

        public SumQueryObject(int index, long value)
        {
            SegmentStartIndex = index;
            SegmentEndIndex = index;
            SumWithoutRangeAddition = value;
        }

        public SumQueryObject(int segmentStartIndex, int segmentEndIndex, long sumWithoutRangeAddition)
        {
            SegmentStartIndex = segmentStartIndex;
            SegmentEndIndex = segmentEndIndex;
            SumWithoutRangeAddition = sumWithoutRangeAddition;
        }

        public SumQueryObject Combine(SumQueryObject rightAdjacentObject)
            => new SumQueryObject(
                segmentStartIndex: SegmentStartIndex,
                segmentEndIndex: rightAdjacentObject.SegmentEndIndex,
                sumWithoutRangeAddition: Sum + rightAdjacentObject.Sum);

        public void Update(long rangeAddition)
            => RangeAddition += rangeAddition;

        public void Update(SumQueryObject updatedLeftChild, SumQueryObject updatedRightChild)
            => SumWithoutRangeAddition = updatedLeftChild.Sum + updatedRightChild.Sum;

        public bool IsTotallyOverlappedBy(int startIndex, int endIndex)
            => startIndex <= SegmentStartIndex && endIndex >= SegmentEndIndex;

        public bool DoesLeftHalfOverlapWith(int startIndex, int endIndex)
            => startIndex <= (SegmentStartIndex + SegmentEndIndex) / 2;

        public bool DoesRightHalfOverlapWith(int startIndex, int endIndex)
            => endIndex > (SegmentStartIndex + SegmentEndIndex) / 2;
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
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int arrayLength = line[0];
            var solver = new HORRIBLE(arrayLength);

            int commandCount = line[1];
            for (int c = 0; c < commandCount; ++c)
            {
                int[] command = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                if (command[0] == 0)
                {
                    solver.Update(
                        updateStartIndex: command[1] - 1,
                        updateEndIndex: command[2] - 1,
                        rangeAddition: command[3]);
                }
                else
                {
                    output.Append(solver.Query(
                        queryStartIndex: command[1] - 1,
                        queryEndIndex: command[2] - 1));
                    output.AppendLine();
                }
            }
        }

        Console.Write(output);
    }
}
