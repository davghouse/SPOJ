using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vertex = RootedTree.Vertex;

// https://www.spoj.com/problems/LCA/ #divide-and-conquer #graph-theory #lca #segment-tree #stack #tree
// Finds the lowest common ancestor of two vertices in a rooted tree.
public sealed class LCA
{
    private readonly RootedTree _tree;
    private readonly Vertex[] _eulerTour;
    private readonly ArrayBasedSegmentTree _segmentTree;

    public LCA(int vertexCount, List<int>[] verticesChildren)
    {
        _tree = RootedTree.CreateFromChildren(vertexCount, 0, verticesChildren);
        _eulerTour = _tree.GetEulerTour();
        _segmentTree = new ArrayBasedSegmentTree(_eulerTour);
    }

    // Here's a good guide: https://www.geeksforgeeks.org/find-lca-in-binary-tree-using-rmq/.
    // First, we have to build a rooted tree. Then, we have to compute its Euler tour. I do
    // this using a stack but it's really easy using recursion, just a little less performant.
    // Then, convince yourself the LCA of two vertices is the vertex of minimum depth between
    // the first occurrences of those two vertices in the Euler tour. We can build a segment
    // tree on top of the Euler tour, with query objects storing the vertex of minimum depth
    // in a range. Then when asked to find the LCA of two vertices, query the segment tree for
    // the minimum depth vertex between the vertices' initial indices in the Euler tour.
    public int Solve(int firstVertexID, int secondVertexID)
    {
        int firstInitialIndex = _tree.Vertices[firstVertexID].EulerTourInitialIndex.Value;
        int secondInitialIndex = _tree.Vertices[secondVertexID].EulerTourInitialIndex.Value;

        return firstInitialIndex < secondInitialIndex
            ? _segmentTree.Query(firstInitialIndex, secondInitialIndex)
            : _segmentTree.Query(secondInitialIndex, firstInitialIndex);
    }
}

// Undirected, connected, rooted graph with no cycles. The tree's vertices are stored in an
// array with the ID of a vertex (from 0 to vertexCount - 1) correpsonding to its index.
public sealed class RootedTree
{
    private RootedTree(int vertexCount, int rootID)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = vertices;
        Root = vertices[rootID];
    }

    // E.g. if verticesChildren[1] = (3, 4, 6), vertices w/ ID 3, 4, 6 are children of vertex w/ ID 1.
    public static RootedTree CreateFromChildren(int vertexCount, int rootID, List<int>[] verticesChildren)
    {
        var tree = new RootedTree(vertexCount, rootID);
        for (int id = 0; id < vertexCount; ++id)
        {
            if (!verticesChildren[id]?.Any() ?? true)
                continue;

            var parent = tree.Vertices[id];
            foreach (int childID in verticesChildren[id])
            {
                tree.Vertices[childID].SetParent(parent);
            }
        }

        return tree;
    }

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public Vertex Root { get; }

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
                vertex.Depth = 1 + (vertex.Parent?.Depth ?? -1);
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
        private readonly RootedTree _tree;
        private readonly List<Vertex> _children = new List<Vertex>();

        internal Vertex(RootedTree tree, int ID)
        {
            _tree = tree;
            this.ID = ID;
        }

        public int ID { get; }

        public Vertex Parent { get; private set; }
        public IReadOnlyList<Vertex> Children => _children;
        public int? Depth { get; internal set; }
        public int? EulerTourInitialIndex { get; internal set; }
        internal int EulerTourChildCounter { get; set; }

        internal void SetParent(Vertex parent)
        {
            Parent = parent;
            Parent._children.Add(this);
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
public sealed class ArrayBasedSegmentTree
{
    private readonly IReadOnlyList<Vertex> _sourceArray;
    private readonly MinimumDepthQueryObject[] _treeArray;

    public ArrayBasedSegmentTree(IReadOnlyList<Vertex> sourceArray)
    {
        _sourceArray = sourceArray;
        _treeArray = new MinimumDepthQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Count) - 1];
        Build(0, 0, _sourceArray.Count - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new MinimumDepthQueryObject(segmentStartIndex, _sourceArray[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        Build(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex);
        Build(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
        => Query(0, queryStartIndex, queryEndIndex).MinimumDepthVertex.ID;

    private MinimumDepthQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
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
}

// Given a query range, this stores the vertex of minimum depth across that range in the Euler tour.
public sealed class MinimumDepthQueryObject
{
    private MinimumDepthQueryObject()
    { }

    public MinimumDepthQueryObject(int index, Vertex value)
    {
        SegmentStartIndex = SegmentEndIndex = index;
        MinimumDepthVertex = value;
    }

    // 'Readonly' property for the start index of the array range this query object corresponds to.
    public int SegmentStartIndex { get; private set; }

    // 'Readonly' property for the end index of the array range this query object corresponds to.
    public int SegmentEndIndex { get; private set; }

    public Vertex MinimumDepthVertex { get; private set; }

    public MinimumDepthQueryObject Combine(MinimumDepthQueryObject rightAdjacentObject)
        => new MinimumDepthQueryObject
        {
            SegmentStartIndex = SegmentStartIndex,
            SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
            MinimumDepthVertex = MinimumDepthVertex.Depth < rightAdjacentObject.MinimumDepthVertex.Depth
                ? MinimumDepthVertex : rightAdjacentObject.MinimumDepthVertex
        };

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
        var verticesChildren = new List<int>[1000];
        for (int i = 0; i < 1000; ++i)
        {
            verticesChildren[i] = new List<int>();
        }

        var output = new StringBuilder();
        int totalTestCases = int.Parse(Console.ReadLine());
        for (int t = 1; t <= totalTestCases; ++t)
        {
            output.AppendLine($"Case {t}:");

            int vertexCount = int.Parse(Console.ReadLine());
            for (int id = 0; id < vertexCount; ++id)
            {
                int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                var children = verticesChildren[id];
                children.Clear();
                for (int i = 1; i < line.Length; ++i)
                {
                    children.Add(line[i] - 1);
                }
            }

            var solver = new LCA(vertexCount, verticesChildren);
            int queryCount = int.Parse(Console.ReadLine());
            for (int q = 0; q < queryCount; ++q)
            {
                string[] line = Console.ReadLine().Split();

                output.Append(solver.Solve(
                    firstVertexID: int.Parse(line[0]) - 1,
                    secondVertexID:  int.Parse(line[1]) - 1) + 1);
                output.AppendLine();
            }
        }

        Console.Write(output);
    }
}
