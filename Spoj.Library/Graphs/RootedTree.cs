using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.Graphs
{
    // Undirected, connected, rooted graph with no cycles. The tree's vertices are stored in an array
    // and the ID of a vertex (from 0 to vertexCount - 1) corresponds to its index in that array.
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

        // For example, if children[1] = (3, 4, 6) then vertices w/ ID 3, 4, 6 are the children of vertex w/ ID 1.
        public static RootedTree CreateFromExplicitChildren(int vertexCount, int rootID, List<int>[] children)
        {
            var tree = new RootedTree(vertexCount, rootID);
            for (int id = 0; id < vertexCount; ++id)
            {
                if (!children[id]?.Any() ?? true)
                    continue;

                var parent = tree.Vertices[id];
                foreach (int childID in children[id])
                {
                    tree.Vertices[childID].SetParent(parent);
                }
            }

            return tree;
        }

        public void InitializeDepthsAndSubtreeSizes()
        {
            var verticesToVisit = new Stack<Vertex>();
            verticesToVisit.Push(Root);

            while (verticesToVisit.Count > 0)
            {
                var vertex = verticesToVisit.Peek();

                // If Depth is null, it's the first time we're visiting the vertex. Visit its children.
                if (!vertex.Depth.HasValue)
                {
                    vertex.Depth = 1 + (vertex.Parent?.Depth ?? -1);

                    foreach (var child in vertex.Children)
                    {
                        verticesToVisit.Push(child);
                    }
                }
                // At this point, we know all the children have been visited, so we're good to pop.
                else
                {
                    verticesToVisit.Pop();
                    vertex.SubtreeSize = 1 + vertex.Children.Sum(c => c.SubtreeSize);
                }
            }
        }

        public IReadOnlyList<Vertex> Vertices { get; }
        public int VertexCount => Vertices.Count;

        public Vertex Root { get; private set; }

        public Vertex[] GetEulerTourUsingStack()
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

                // Only pop the vertex once we've visited all its children. At first I was popping
                // no matter what, and then re-pushing for each child, like follows. This degrades
                // performance by about a factor of 2, and is even slower than the recursive solution.
                //        foreach (var child in vertex.Children)
                //        {
                //            verticesToVisit.Push(vertex);
                //            verticesToVisit.Push(child);
                //        }
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

        public Vertex[] GetEulerTourUsingRecursion()
        {
            var eulerTour = new Vertex[2 * VertexCount - 1];
            int eulerTourIndex = -1;
            GetEulerTourUsingRecursion(eulerTour, Root, ref eulerTourIndex);
            return eulerTour;
        }

        private void GetEulerTourUsingRecursion(Vertex[] eulerTour, Vertex vertex, ref int eulerTourIndex)
        {
            eulerTour[++eulerTourIndex] = vertex;
            vertex.Depth = 1 + (vertex.Parent?.Depth ?? -1);
            vertex.EulerTourInitialIndex = eulerTourIndex;

            foreach (var child in vertex.Children)
            {
                GetEulerTourUsingRecursion(eulerTour, child, ref eulerTourIndex);
                eulerTour[++eulerTourIndex] = vertex;
            }
        }

        public sealed class Vertex : IEquatable<Vertex>
        {
            private readonly RootedTree _tree;
            private List<Vertex> _children = new List<Vertex>();

            internal Vertex(RootedTree tree, int ID)
            {
                _tree = tree;
                this.ID = ID;
            }

            public int ID { get; }

            public Vertex Parent { get; private set; }
            public IReadOnlyList<Vertex> Children => _children;
            public int? Depth { get; internal set; }
            public int? SubtreeSize { get; internal set; }
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
}
