using System;
using System.Text;

// http://www.spoj.com/problems/HORRIBLE/ #divide-and-conquer #lazy #research #segment-tree
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
    // Using static arrays for performance reasons (okay, maybe premature).
    private static readonly long[] _sourceArray = new long[100000];
    private static readonly QueryObject[] _treeArray = new QueryObject[2 * 1048576 - 1];

    public LazySumSegmentTree(int arrayLength)
    {
        Array.Clear(_sourceArray, 0, arrayLength);
        Build(0, 0, arrayLength - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new QueryObject(segmentStartIndex, _sourceArray[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2);
        Build(rightChildTreeArrayIndex, (segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public long Query(int queryStartIndex, int queryEndIndex)
        => Query(0, queryStartIndex, queryEndIndex).Sum;

    // Instead of returning the children object directly, we have to add on the parent's range addition. The children
    // query object knows the subset of the parent segment it intersects with, and everything in there needs the
    // additions that were applied to the parent segment as a whole. It's kind of weird, any pending range additions
    // specifically for the children object gets brought out and added to the sum when we do .Combine or .Sum, but
    // recursively it makes sense: the children object has a sum but still needs to know about the parent's range additions.
    private QueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
    {
        var parentQueryObject = _treeArray[treeArrayIndex];

        if (parentQueryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
            return parentQueryObject;

        bool isLeftHalfOverlapped = parentQueryObject.IsLeftHalfOverlappedBy(queryStartIndex, queryEndIndex);
        bool isRightHalfOverlapped = parentQueryObject.IsRightHalfOverlappedBy(queryStartIndex, queryEndIndex);
        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        QueryObject childrenQueryObject;

        if (isLeftHalfOverlapped && isRightHalfOverlapped)
        {
            childrenQueryObject = Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex)
                .Combine(Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex));
        }
        else if (isLeftHalfOverlapped)
        {
            childrenQueryObject = Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        }
        else
        {
            childrenQueryObject = Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        }

        return new QueryObject(
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
        QueryObject queryObject = _treeArray[treeArrayIndex];

        if (queryObject.IsTotallyOverlappedBy(updateStartIndex, updateEndIndex))
        {
            queryObject.Update(rangeAddition);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (queryObject.IsLeftHalfOverlappedBy(updateStartIndex, updateEndIndex))
        {
            Update(leftChildTreeArrayIndex, updateStartIndex, updateEndIndex, rangeAddition);
        }

        if (queryObject.IsRightHalfOverlappedBy(updateStartIndex, updateEndIndex))
        {
            Update(rightChildTreeArrayIndex, updateStartIndex, updateEndIndex, rangeAddition);
        }

        queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }

    private sealed class QueryObject
    {
        public long Sum
            => SumWithoutRangeAddition + SumFromRangeAddition;

        public long SumFromRangeAddition
            => RangeAddition * (SegmentEndIndex - SegmentStartIndex + 1);

        public long SumWithoutRangeAddition { get; set; }
        public long RangeAddition { get; set; }

        public int SegmentStartIndex { get; }
        public int SegmentEndIndex { get; }

        public QueryObject(int index, long value)
        {
            SegmentStartIndex = index;
            SegmentEndIndex = index;
            SumWithoutRangeAddition = value;
        }

        public QueryObject(int segmentStartIndex, int segmentEndIndex, long sum)
        {
            SegmentStartIndex = segmentStartIndex;
            SegmentEndIndex = segmentEndIndex;
            SumWithoutRangeAddition = sum;
        }

        public QueryObject Combine(QueryObject rightAdjacentObject)
            => new QueryObject(
                segmentStartIndex: SegmentStartIndex,
                segmentEndIndex: rightAdjacentObject.SegmentEndIndex,
                sum: Sum + rightAdjacentObject.Sum);

        public void Update(long rangeAddition)
            => RangeAddition += rangeAddition;

        public void Update(QueryObject updatedLeftChild, QueryObject updatedRightChild)
            => SumWithoutRangeAddition = updatedLeftChild.Sum + updatedRightChild.Sum;

        public bool IsTotallyOverlappedBy(int startIndex, int endIndex)
            => startIndex <= SegmentStartIndex && endIndex >= SegmentEndIndex;

        public bool IsLeftHalfOverlappedBy(int startIndex, int endIndex)
            => startIndex <= (SegmentStartIndex + SegmentEndIndex) / 2;

        public bool IsRightHalfOverlappedBy(int startIndex, int endIndex)
            => endIndex > (SegmentStartIndex + SegmentEndIndex) / 2;
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
