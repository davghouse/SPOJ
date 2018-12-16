using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.Graphs
{
    // Undirected, connected, rooted, weighted graph with no cycles. The tree's vertices are stored
    // in an array with the ID of a vertex (from 0 to vertexCount - 1) corresponding to its index.
    public sealed class WeightedRootedTree<T>
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

        // For example, if children[1] = ((3, 99) (4, -3)) then vertices w/ ID 3, 4 are the children of vertex w/ ID 1.
        // Creating from explicit children doesn't initialize depths or subtree sizes; do that separately if needed.
        public static WeightedRootedTree<int> CreateFromExplicitChildren(
            int vertexCount, int rootID, List<KeyValuePair<int, int>>[] childEdges)
        {
            var tree = new WeightedRootedTree<int>(vertexCount, rootID);
            for (int id = 0; id < vertexCount; ++id)
            {
                if (!childEdges[id]?.Any() ?? true)
                    continue;

                var parent = tree.Vertices[id];
                foreach (var childEdge in childEdges[id])
                {
                    tree.Vertices[childEdge.Key].SetParent(parent, childEdge.Value);
                }
            }

            return tree;
        }

        // For example, edges like (0, 1, 99), (1, 2, -3) => there's an edge between vertices 0 and 1 and 1 and 2.
        // Creating from edges also initializes depths and subtree sizes, because we have to DFS regardless.
        public static WeightedRootedTree<int> CreateFromEdges(int vertexCount, int rootID, int[,] edges)
        {
            var tree = new WeightedRootedTree<int>(vertexCount, rootID);
            for (int i = 0; i < vertexCount - 1; ++i)
            {
                var firstVertex = tree.Vertices[edges[i, 0]];
                var secondVertex = tree.Vertices[edges[i, 1]];
                int weight = edges[i, 2];
                firstVertex.AddNeighbor(secondVertex, weight);
                secondVertex.AddNeighbor(firstVertex, weight);
            }

            // Now we need to wire up the parent-child relationships. We have to DFS just like
            // InitializeDepthsAndSubtreeSizes, so we might as well initialize depths & subtree sizes too.
            var verticesToVisit = new Stack<WeightedRootedTree<int>.Vertex>();
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

        public sealed class Vertex : IEquatable<Vertex>
        {
            private readonly WeightedRootedTree<T> _tree;
            private readonly List<KeyValuePair<Vertex, T>> _neighborEdges = new List<KeyValuePair<Vertex, T>>();
            private readonly List<Vertex> _children = new List<Vertex>();

            internal Vertex(WeightedRootedTree<T> tree, int ID)
            {
                _tree = tree;
                this.ID = ID;
            }

            public int ID { get; }

            public Vertex Parent { get; private set; }
            public T Weight { get; private set; } // Weight of the edge to the parent.
            internal IReadOnlyList<KeyValuePair<Vertex, T>> NeighborEdges => _neighborEdges;
            public IReadOnlyList<Vertex> Children => _children;
            public int? Depth { get; internal set; }
            public int? SubtreeSize { get; internal set; }
            public int? EulerTourInitialIndex { get; internal set; }
            internal int EulerTourChildCounter { get; set; }

            internal void AddNeighbor(Vertex neighbor, T weight)
                => _neighborEdges.Add(new KeyValuePair<Vertex, T>(neighbor, weight));

            internal void SetParent(Vertex parent, T weight)
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
}
