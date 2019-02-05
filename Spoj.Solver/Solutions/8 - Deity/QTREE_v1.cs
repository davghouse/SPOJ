using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vertex = WeightedRootedTree.Vertex;

// https://www.spoj.com/problems/QTREE/ #divide-and-conquer #graph-theory #hld #lca #segment-tree #tree
// Finds the edge of maximum weight between two vertices in a tree w/ updatable weights.
public sealed class QTREE // v1, same big O as v2 but too much overhead to get AC
{
    private readonly int[,] _edges;
    private readonly WeightedRootedTree _tree;
    private readonly Vertex[] _eulerTour;
    private readonly ArrayBasedSegmentTree<MinimumDepthQueryObject, Vertex> _eulerTourSegmentTree;
    private readonly ArrayBasedSegmentTree<MaximumWeightQueryObject, Vertex> _hldBaseArraySegmentTree;

    // https://blog.anudeep2011.com/heavy-light-decomposition/
    // https://www.geeksforgeeks.org/heavy-light-decomposition-set-2-implementation/
    public QTREE(int vertexCount, int[,] edges)
    {
        _edges = edges;
        _tree = WeightedRootedTree.CreateFromEdges(vertexCount, rootID: 0, edges: edges);
        _eulerTour = _tree.GetEulerTour();
        _eulerTourSegmentTree = new ArrayBasedSegmentTree<MinimumDepthQueryObject, Vertex>(_eulerTour);
        _tree.RunHLD();
        _hldBaseArraySegmentTree = new ArrayBasedSegmentTree<MaximumWeightQueryObject, Vertex>(_tree.HLDBaseArray);
    }

    public int Query(int firstVertexID, int secondVertexID)
    {
        var firstVertex = _tree.Vertices[firstVertexID];
        var secondVertex = _tree.Vertices[secondVertexID];
        var lcaVertex = GetLeastCommonAncestor(firstVertex, secondVertex);

        return Math.Max(
            QueryUp(firstVertex, lcaVertex) ?? 0,
            QueryUp(secondVertex, lcaVertex) ?? 0);
    }

    public void Change(int edgeID, int weight)
    {
        var firstVertex = _tree.Vertices[_edges[edgeID, 0]];
        var secondVertex = _tree.Vertices[_edges[edgeID, 1]];

        if (firstVertex.Parent == secondVertex)
        {
            firstVertex.Weight = weight;
            _hldBaseArraySegmentTree.Update(firstVertex.HLDBaseArrayIndex.Value);
        }
        else // Second vertex's parent must be the first vertex.
        {
            secondVertex.Weight = weight;
            _hldBaseArraySegmentTree.Update(secondVertex.HLDBaseArrayIndex.Value);
        }
    }

    // Here's a good guide: https://www.geeksforgeeks.org/find-lca-in-binary-tree-using-rmq/.
    private Vertex GetLeastCommonAncestor(Vertex firstVertex, Vertex secondVertex)
    {
        int firstInitialIndex = firstVertex.EulerTourInitialIndex.Value;
        int secondInitialIndex = secondVertex.EulerTourInitialIndex.Value;

        return firstInitialIndex < secondInitialIndex
            ? _eulerTourSegmentTree.Query(firstInitialIndex, secondInitialIndex)
            : _eulerTourSegmentTree.Query(secondInitialIndex, firstInitialIndex);
    }

    // Finds the edge of maximum weight on the path up the tree from the descendant vertex
    // to the ancestor vertex. The HLD segment tree allows for log(n) querying when vertices
    // are within the same HLD chain. Some chain hopping may be necessary, but as the links
    // above mention, there are no more than log(n) chains.
    private int? QueryUp(Vertex descendantVertex, Vertex ancestorVertex)
    {
        int? pathMaximumEdgeWeight = null;

        while (true)
        {
            if (descendantVertex.HLDChainIndex == ancestorVertex.HLDChainIndex)
            {
                if (descendantVertex == ancestorVertex)
                    return pathMaximumEdgeWeight; // Could still be null if initial vertices were equal.

                // Consider the following tree, rooted at V0: V0 --- V1 -- V2 -- V3.
                // Say we're querying from V3 (descendant) to V1 (ancestor). Along that path we need
                // to consider V3's edge to V2, and V2's edge to V1. In the HLD segment tree, vertices
                // correspond with the edge to their parent. So we need to query the range between the
                // descendant and one before the ancestor, V3 to V2. (The base array for the segment tree
                // has ancestors appearing before descendants, explaining the start & end indices below.)
                int chainMaximumEdgeWeight = _hldBaseArraySegmentTree.Query(
                    ancestorVertex.HLDBaseArrayIndex.Value + 1,
                    descendantVertex.HLDBaseArrayIndex.Value).Weight.Value;

                return Math.Max(pathMaximumEdgeWeight ?? 0, chainMaximumEdgeWeight);
            }
            else
            {
                var descendantChainHead = _tree.HLDChainHeads[descendantVertex.HLDChainIndex.Value];

                // Query through the descendant's chain head, which considers all the edges from the
                // descendant to the chain head, plus the edge from the chain head to the next chain.
                int descendantChainMaximumEdgeWeight = _hldBaseArraySegmentTree.Query(
                    descendantChainHead.HLDBaseArrayIndex.Value,
                    descendantVertex.HLDBaseArrayIndex.Value).Weight.Value;
                pathMaximumEdgeWeight = Math.Max(pathMaximumEdgeWeight ?? 0, descendantChainMaximumEdgeWeight);

                // Advance to the bottom of the next chain.
                descendantVertex = descendantChainHead.Parent;
            }
        }
    }
}

// Undirected, connected, rooted, weighted graph with no cycles. The tree's vertices are stored
// in an array with the ID of a vertex (from 0 to vertexCount - 1) corresponding to its index.
public sealed class WeightedRootedTree
{
    private WeightedRootedTree(int vertexCount, int rootID)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = vertices;
        Root = vertices[rootID];
    }

    // For example, edges like (0, 1, 99), (1, 2, -3) => there's an edge between vertices 0 and 1 and 1 and 2.
    public static WeightedRootedTree CreateFromEdges(int vertexCount, int rootID, int[,] edges)
    {
        var tree = new WeightedRootedTree(vertexCount, rootID);
        for (int i = 0; i < vertexCount - 1; ++i)
        {
            var firstVertex = tree.Vertices[edges[i, 0]];
            var secondVertex = tree.Vertices[edges[i, 1]];
            int weight = edges[i, 2];
            firstVertex.AddNeighbor(secondVertex, weight);
            secondVertex.AddNeighbor(firstVertex, weight);
        }

        // Now we need to wire up the parent-child relationships. We have to DFS, so we
        // might as well initialize depths and subtree sizes at the same time.
        var verticesToVisit = new Stack<Vertex>();
        verticesToVisit.Push(tree.Root);

        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Peek();

            // If Depth is null, it's the first time we're visiting the vertex. Visit its children.
            if (!vertex.Depth.HasValue)
            {
                vertex.Depth = 1 + (vertex.Parent?.Depth ?? -1);

                foreach (var childEdge in vertex.NeighborEdges.Where(ne => ne.Key != vertex.Parent))
                {
                    childEdge.Key.SetParent(vertex, childEdge.Value);
                    verticesToVisit.Push(childEdge.Key);
                }
            }
            // At this point, we know all the children have been visited, so we're good to pop.
            else
            {
                verticesToVisit.Pop();
                vertex.SubtreeSize = 1 + vertex.Children.Sum(c => c.SubtreeSize);
            }
        }

        return tree;
    }

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public Vertex Root { get; }

    private List<Vertex> _hldChainHeads;
    public IReadOnlyList<Vertex> HLDChainHeads => _hldChainHeads;

    private List<Vertex> _hldBaseArray;
    public IReadOnlyList<Vertex> HLDBaseArray => _hldBaseArray;

    public void RunHLD()
    {
        _hldChainHeads = new List<Vertex>();
        _hldBaseArray = new List<Vertex>(VertexCount);
        RunHLD(Root, startsNewChain: true);
    }

    private void RunHLD(Vertex vertex, bool startsNewChain)
    {
        if (startsNewChain)
        {
            _hldChainHeads.Add(vertex);
        }
        vertex.HLDChainIndex = _hldChainHeads.Count - 1;

        _hldBaseArray.Add(vertex);
        vertex.HLDBaseArrayIndex = _hldBaseArray.Count - 1;

        if (vertex.Children.Any())
        {
            var heaviestChild = vertex.Children[0];
            for (int i = 1; i < vertex.Children.Count; ++i)
            {
                if (vertex.Children[i].SubtreeSize > heaviestChild.SubtreeSize)
                {
                    heaviestChild = vertex.Children[i];
                }
            }

            RunHLD(heaviestChild, startsNewChain: false);

            for (int i = 0; i < vertex.Children.Count; ++i)
            {
                if (vertex.Children[i] != heaviestChild)
                {
                    RunHLD(vertex.Children[i], startsNewChain: true);
                }
            }
        }
    }

    public Vertex[] GetEulerTour()
    {
        // For all n - 1 edges, we take the edge down to its child and then, eventually, back up
        // to its parent. So each edge contributes 2 vertices to the tour, and we get the root
        // initially without using any edges, so that's 2*(n - 1) + 1 = 2n - 1 vertices.
        var eulerTour = new Vertex[2 * VertexCount - 1];
        int eulerTourIndex = -1;
        var verticesToVisit = new Stack<Vertex>();
        verticesToVisit.Push(Root);

        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Peek();
            eulerTour[++eulerTourIndex] = vertex;

            // If the EulerTourInitialIndex is null, it's the first time we're visiting the vertex.
            if (!vertex.EulerTourInitialIndex.HasValue)
            {
                vertex.EulerTourInitialIndex = eulerTourIndex;
            }

            if (vertex.EulerTourChildCounter == vertex.Children.Count)
            {
                verticesToVisit.Pop();
            }
            else
            {
                verticesToVisit.Push(vertex.Children[vertex.EulerTourChildCounter++]);
            }
        }

        return eulerTour;
    }

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly WeightedRootedTree _tree;
        private readonly List<KeyValuePair<Vertex, int>> _neighborEdges = new List<KeyValuePair<Vertex, int>>();
        private readonly List<Vertex> _children = new List<Vertex>();

        internal Vertex(WeightedRootedTree tree, int ID)
        {
            _tree = tree;
            this.ID = ID;
        }

        public int ID { get; }

        public Vertex Parent { get; private set; }
        public int? Weight { get; set; } // Weight of the edge to the parent.
        internal IReadOnlyList<KeyValuePair<Vertex, int>> NeighborEdges => _neighborEdges;
        public IReadOnlyList<Vertex> Children => _children;
        public int? Depth { get; internal set; }
        public int? SubtreeSize { get; internal set; }
        public int? HLDChainIndex { get; internal set; }
        public int? HLDBaseArrayIndex { get; internal set; }
        public int? EulerTourInitialIndex { get; internal set; }
        internal int EulerTourChildCounter { get; set; }

        internal void AddNeighbor(Vertex neighbor, int weight)
            => _neighborEdges.Add(new KeyValuePair<Vertex, int>(neighbor, weight));

        internal void SetParent(Vertex parent, int weight)
        {
            Parent = parent;
            Parent._children.Add(this);
            Weight = weight;
        }

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;

        public override string ToString()
            => $"{ID}";
    }
}

// Most guides online cover this approach, but here's one good one:
// https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
public sealed class ArrayBasedSegmentTree<TQueryObject, TQueryValue>
    where TQueryObject : SegmentTreeQueryObject<TQueryObject, TQueryValue>, new()
{
    private readonly IReadOnlyList<TQueryValue> _sourceArray;
    private readonly TQueryObject[] _treeArray;

    public ArrayBasedSegmentTree(IReadOnlyList<TQueryValue> sourceArray)
    {
        _sourceArray = sourceArray;
        _treeArray = new TQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Count) - 1];
        Build(0, 0, _sourceArray.Count - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new TQueryObject();
            _treeArray[treeArrayIndex].Initialize(segmentStartIndex, _sourceArray[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public TQueryValue Query(int queryStartIndex, int queryEndIndex)
        => Query(0, queryStartIndex, queryEndIndex).QueryValue;

    private TQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
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

    public void Update(int updateIndex)
        => Update(updateIndex, updateIndex);

    public void Update(int updateStartIndex, int updateEndIndex)
        => Update(0, updateStartIndex, updateEndIndex);

    private void Update(int treeArrayIndex, int updateStartIndex, int updateEndIndex)
    {
        var queryObject = _treeArray[treeArrayIndex];

        if (queryObject.SegmentStartIndex == queryObject.SegmentEndIndex)
            return; // Edge weight has already been updated in Change.

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        if (queryObject.DoesLeftHalfOverlapWith(updateStartIndex, updateEndIndex))
        {
            Update(leftChildTreeArrayIndex, updateStartIndex, updateEndIndex);
        }

        if (queryObject.DoesRightHalfOverlapWith(updateStartIndex, updateEndIndex))
        {
            Update(rightChildTreeArrayIndex, updateStartIndex, updateEndIndex);
        }

        queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
    }
}

// Given a query range, this stores the vertex of minimum depth across that range in the Euler tour.
public sealed class MinimumDepthQueryObject
    : SegmentTreeQueryObject<MinimumDepthQueryObject, Vertex>
{
    public override Vertex QueryValue
    {
        get { return MinimumDepthVertex; }
        protected set { MinimumDepthVertex = value; }
    }

    private Vertex MinimumDepthVertex { get; set; }

    public override MinimumDepthQueryObject Combine(
        MinimumDepthQueryObject rightAdjacentObject)
        => new MinimumDepthQueryObject
        {
            SegmentStartIndex = SegmentStartIndex,
            SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
            MinimumDepthVertex = MinimumDepthVertex.Depth < rightAdjacentObject.MinimumDepthVertex.Depth
                ? MinimumDepthVertex : rightAdjacentObject.MinimumDepthVertex
        };

    public override void Update(
        MinimumDepthQueryObject updatedLeftChild,
        MinimumDepthQueryObject updatedRightChild)
    {
        throw new NotImplementedException();
    }
}

// Given a query range, this stores the vertex of maximum edge weight (to parent) in an HLD chain.
public sealed class MaximumWeightQueryObject
    : SegmentTreeQueryObject<MaximumWeightQueryObject, Vertex>
{
    public override Vertex QueryValue
    {
        get { return MaximumWeightVertex; }
        protected set { MaximumWeightVertex = value; }
    }

    private Vertex MaximumWeightVertex { get; set; }

    public override MaximumWeightQueryObject Combine(
        MaximumWeightQueryObject rightAdjacentObject)
        => new MaximumWeightQueryObject
        {
            SegmentStartIndex = SegmentStartIndex,
            SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
            MaximumWeightVertex = MaximumWeightVertex.Weight > rightAdjacentObject.MaximumWeightVertex.Weight
                ? MaximumWeightVertex : rightAdjacentObject.MaximumWeightVertex
        };

    public override void Update(
        MaximumWeightQueryObject updatedLeftChild,
        MaximumWeightQueryObject updatedRightChild)
        => MaximumWeightVertex
        = updatedLeftChild.MaximumWeightVertex.Weight > updatedRightChild.MaximumWeightVertex.Weight
        ? updatedLeftChild.MaximumWeightVertex : updatedRightChild.MaximumWeightVertex;
}

// Generic TQueryObject is kind of like the familiar EquatableObject : IEquatable<EquatableObject>.
// For more information see: https://blogs.msdn.microsoft.com/ericlippert/2011/02/03/curiouser-and-curiouser/
// Query objects know about their segment, but don't know about their 'children'--that's a segment tree's job.
public abstract class SegmentTreeQueryObject<TQueryObject, TQueryValue>
    where TQueryObject : SegmentTreeQueryObject<TQueryObject, TQueryValue>, new()
{
    // A query object wraps a query value, of the same type as in the source array and
    // the data type of the answer we're looking for when querying.
    public abstract TQueryValue QueryValue { get; protected set; }

    // 'Readonly' property for the start index of the array range this query object corresponds to.
    public int SegmentStartIndex { get; protected set; }

    // 'Readonly' property for the end index of the array range this query object corresponds to.
    public int SegmentEndIndex { get; protected set; }

    // For constructing leaves, given an index of the source array and the corresponding value.
    // It would be nice if we could have some actual constructors but we're using these objects
    // as generic type parameters so it'd be annoying to support that (activator or factory).
    public void Initialize(int index, TQueryValue value)
    {
        SegmentStartIndex = SegmentEndIndex = index;
        QueryValue = value;
    }

    // A query object can combine into a new object with query objects from segments adjacent and to the right.
    public abstract TQueryObject Combine(TQueryObject rightAdjacentObject);

    // A query object needs to update itself when the source array gets updated, given its children
    // after they've been updated. Like Combine, but from a different perspective.
    public abstract void Update(TQueryObject updatedLeftChild, TQueryObject updatedRightChild);

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
        int[,] edges = new int[9999, 3];
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int vertexCount = FastIO.ReadNonNegativeInt();

            for (int i = 0; i < vertexCount - 1; ++i)
            {
                edges[i, 0] = FastIO.ReadNonNegativeInt() - 1; // first vertex ID
                edges[i, 1] = FastIO.ReadNonNegativeInt() - 1; // second vertex ID
                edges[i, 2] = FastIO.ReadNonNegativeInt(); // weight
            }

            var solver = new QTREE(vertexCount, edges);

            char instruction;
            while ((instruction = FastIO.ReadInstruction()) != 'D')
            {
                if (instruction == 'Q')
                {
                    FastIO.WriteNonNegativeInt(solver.Query(
                        firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                        secondVertexID: FastIO.ReadNonNegativeInt() - 1));
                    FastIO.WriteLine();
                }
                else
                {
                    solver.Change(
                        edgeID: FastIO.ReadNonNegativeInt() - 1,
                        weight: FastIO.ReadNonNegativeInt());
                }
            }
        }

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
    private const byte _A = (byte)'A';
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

    public static char ReadInstruction()
    {
        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        byte firstInstructionChar;
        do
        {
            firstInstructionChar = ReadByte();
        } while (firstInstructionChar < _minusSign);

        // Consume and discard instruction characters (their ASCII codes are all uppercase).
        byte throwawayInstructionChar;
        do
        {
            throwawayInstructionChar = ReadByte();
        } while (throwawayInstructionChar >= _A);

        return (char)firstInstructionChar; // Q for QUERY, C for CHANGE, D for DONE.
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
