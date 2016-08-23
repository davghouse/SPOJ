using System.Collections.Generic;

namespace Spoj.Library.SegmentTree
{
    // This implementation is mostly educational. Rather than store the segment query values
    // in an array and do annoying little calculations to determine what segment the array index
    // corresponds to, the nodes store that information/structure when built.
    public sealed class NodeBasedSegmentTree<T> : SegmentTree<T> where T : class, ISegmentTreeQueryValue<T>, new()
    {
        private Node _root;

        public NodeBasedSegmentTree(IReadOnlyList<int> sourceArray)
            : base(sourceArray)
        {
            _root = Build(0, _sourceArray.Count - 1);
        }

        private Node Build(int segmentStartIndex, int segmentEndIndex)
            => segmentStartIndex == segmentEndIndex
            ? new Node(
                index: segmentStartIndex,
                value: _sourceArray[segmentStartIndex])
            : new Node(
                leftChild: Build(segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2),
                rightChild: Build((segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex));

        public override T Query(int queryStartIndex, int queryEndIndex)
            => Query(_root, queryStartIndex, queryEndIndex);

        private T Query(Node node, int queryStartIndex, int queryEndIndex)
        {
            if (node.IsOverlappedTotallyBy(queryStartIndex, queryEndIndex))
                return node.QueryValue;

            bool leftChildIsOverlapped = node.LeftChild.IsOverlappedBy(queryStartIndex, queryEndIndex);
            bool rightChildIsOverlapped = node.RightChild.IsOverlappedBy(queryStartIndex, queryEndIndex);

            if (leftChildIsOverlapped && rightChildIsOverlapped)
                return Query(node.LeftChild, queryStartIndex, queryEndIndex)
                    .Combine(Query(node.RightChild, queryStartIndex, queryEndIndex));
            else if (leftChildIsOverlapped)
                return Query(node.LeftChild, queryStartIndex, queryEndIndex);
            else
                return Query(node.RightChild, queryStartIndex, queryEndIndex);
        }

        private class Node
        {
            public Node(int index, int value)
            {
                SegmentStartIndex = SegmentEndIndex = index;
                QueryValue = new T();
                QueryValue.Initialize(value);
            }

            public Node(Node leftChild, Node rightChild)
            {
                SegmentStartIndex = leftChild.SegmentStartIndex;
                SegmentEndIndex = rightChild.SegmentEndIndex;
                LeftChild = leftChild;
                RightChild = rightChild;
                QueryValue = leftChild.QueryValue.Combine(rightChild.QueryValue);
            }

            public T QueryValue { get; }

            public int SegmentStartIndex { get; }
            public int SegmentEndIndex { get; }

            public Node LeftChild { get; }
            public Node RightChild { get; }

            public bool IsOverlappedBy(int queryStartIndex, int queryEndIndex)
                => queryStartIndex <= SegmentEndIndex && queryEndIndex >= SegmentStartIndex;

            public bool IsOverlappedTotallyBy(int queryStartIndex, int queryEndIndex)
                => queryStartIndex <= SegmentStartIndex && queryEndIndex >= SegmentEndIndex;
        }
    }
}
