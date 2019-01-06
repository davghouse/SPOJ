using System;
using System.Collections.Generic;

namespace Spoj.Library.SegmentTrees
{
    // This implementation is almost as fast as array-based. Rather than store the segment query objects
    // in an array and do little calculations to determine where a segment's children are, the nodes
    // store that information/structure when built.
    public sealed class NodeBasedSegmentTree<TQueryObject, TQueryValue> : SegmentTree<TQueryObject, TQueryValue>
        where TQueryObject : SegmentTreeQueryObject<TQueryObject, TQueryValue>, new()
    {
        private Node _root;

        public NodeBasedSegmentTree(IReadOnlyList<TQueryValue> sourceArray)
            : base(sourceArray)
        {
            _root = Build(0, _sourceArray.Count - 1);
        }

        private Node Build(int segmentStartIndex, int segmentEndIndex)
            => segmentStartIndex == segmentEndIndex
            ? new Node( // Corresponds to a single index, so it's a leaf node.
                index: segmentStartIndex,
                value: _sourceArray[segmentStartIndex])
            : new Node( // Corresponds to a range of indices, so it has two children, each corresponding to half the range.
                leftChild: Build(segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2),
                rightChild: Build((segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex));

        public override TQueryValue Query(int queryStartIndex, int queryEndIndex)
            => Query(_root, queryStartIndex, queryEndIndex).QueryValue;

        // It's always the case that when we enter this method, the node is overlapped at least partially by the query
        // range. Either one or both of its children are overlapped, the base case where it's overlapped totally (a leaf always is).
        // Unless we're querying the whole range, if it's totally overlapped it's being called recursively from higher up.
        private TQueryObject Query(Node node, int queryStartIndex, int queryEndIndex)
        {
            if (node.QueryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
                return node.QueryObject;

            bool leftHalfOverlaps = node.QueryObject.DoesLeftHalfOverlapWith(queryStartIndex, queryEndIndex);
            bool rightHalfOverlaps = node.QueryObject.DoesRightHalfOverlapWith(queryStartIndex, queryEndIndex);

            if (leftHalfOverlaps && rightHalfOverlaps)
                return Query(node.LeftChild, queryStartIndex, queryEndIndex)
                    .Combine(Query(node.RightChild, queryStartIndex, queryEndIndex));
            else if (leftHalfOverlaps)
                return Query(node.LeftChild, queryStartIndex, queryEndIndex);
            else
                return Query(node.RightChild, queryStartIndex, queryEndIndex);
        }

        public override void Update(int updateIndex, Func<TQueryValue, TQueryValue> updater)
            => Update(updateIndex, updateIndex, updater);

        public override void Update(int updateStartIndex, int updateEndIndex, Func<TQueryValue, TQueryValue> updater)
            => Update(_root, updateStartIndex, updateEndIndex, updater);

        // If node is a leaf then we update its query object's value by using the updater, with its current value as input.
        // Otherwise, update recursively the overlapped children and update node, the parent, after those updates finish.
        // It's the nature of the recursion that (for non-leaf nodes) either one or both of the children will be updated.
        private void Update(Node node, int updateStartIndex, int updateEndIndex, Func<TQueryValue, TQueryValue> updater)
        {
            if (node.IsLeaf)
            {
                node.QueryObject.Reinitialize(updater);
                return;
            }

            if (node.QueryObject.DoesLeftHalfOverlapWith(updateStartIndex, updateEndIndex))
            {
                Update(node.LeftChild, updateStartIndex, updateEndIndex, updater);
            }

            if (node.QueryObject.DoesRightHalfOverlapWith(updateStartIndex, updateEndIndex))
            {
                Update(node.RightChild, updateStartIndex, updateEndIndex, updater);
            }

            node.QueryObject.Update(node.LeftChild.QueryObject, node.RightChild.QueryObject);
        }

        private sealed class Node
        {
            public Node(int index, TQueryValue value)
            {
                QueryObject = new TQueryObject();
                QueryObject.Initialize(index, value);
            }

            public Node(Node leftChild, Node rightChild)
            {
                LeftChild = leftChild;
                RightChild = rightChild;
                QueryObject = leftChild.QueryObject.Combine(rightChild.QueryObject);
            }

            public TQueryObject QueryObject { get; }

            public Node LeftChild { get; }
            public Node RightChild { get; }

            // If it's not a leaf it has both children (segment's at least 2 in size), but null check both for completeness.
            public bool IsLeaf =>
                LeftChild == null && RightChild == null;
        }
    }
}
