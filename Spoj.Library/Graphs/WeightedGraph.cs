using System;
using System.Collections.Generic;

namespace Spoj.Library.Graphs
{
    // Undirected, weighted graph with no loops or multiple edges. The graph's vertices are stored in an array
    // and the ID of a vertex (from 0 to vertexCount - 1) corresponds to its index in that array. Using a list
    // instead of a dictionary for a vertex's edges can help avoid TLE for certain problems. Maintaining
    // search state inside of the vertices themselves can also help.
    public sealed class WeightedGraph<T>
    {
        public WeightedGraph(int vertexCount)
        {
            var vertices = new Vertex[vertexCount];
            for (int id = 0; id < vertexCount; ++id)
            {
                vertices[id] = new Vertex(this, id);
            }

            Vertices = vertices;
        }

        // For example, an edge like (0, 1, 4) => there's an edge between vertices 0 and 1 with weight 4.
        public static WeightedGraph<int> CreateFromZeroBasedEdges(int vertexCount, int[,] edges)
        {
            var graph = new WeightedGraph<int>(vertexCount);
            for (int i = 0; i < edges.GetLength(0); ++i)
            {
                graph.AddEdge(edges[i, 0], edges[i, 1], edges[i, 2]);
            }

            return graph;
        }

        // For example, an edge like (1, 2, 4) => there's an edge between vertices 0 and 1 with weight 4.
        public static WeightedGraph<int> CreateFromOneBasedEdges(int vertexCount, int[,] edges)
        {
            var graph = new WeightedGraph<int>(vertexCount);
            for (int i = 0; i < edges.GetLength(0); ++i)
            {
                graph.AddEdge(edges[i, 0] - 1, edges[i, 1] - 1, edges[i, 2]);
            }

            return graph;
        }

        public IReadOnlyList<Vertex> Vertices { get; }
        public int VertexCount => Vertices.Count;

        public void AddEdge(int firstVertexID, int secondVertexID, T weight)
            => AddEdge(Vertices[firstVertexID], Vertices[secondVertexID], weight);

        public void AddEdge(Vertex firstVertex, Vertex secondVertex, T weight)
        {
            firstVertex.AddNeighbor(secondVertex, weight);
            secondVertex.AddNeighbor(firstVertex, weight);
        }

        public bool HasEdge(int firstVertexID, int secondVertexID)
            => HasEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

        public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
            => firstVertex.HasNeighbor(secondVertex);

        public sealed class Vertex : IEquatable<Vertex>
        {
            private readonly WeightedGraph<T> _graph;
            private readonly Dictionary<Vertex, T> _edges = new Dictionary<Vertex, T>();

            internal Vertex(WeightedGraph<T> graph, int ID)
            {
                _graph = graph;
                this.ID = ID;
            }

            public int ID { get; }

            public IReadOnlyCollection<Vertex> Neighbors => _edges.Keys;
            public int Degree => _edges.Count;

            internal void AddNeighbor(int neighborID, T weight)
                => _edges.Add(_graph.Vertices[neighborID], weight);

            internal void AddNeighbor(Vertex neighbor, T weight)
                => _edges.Add(neighbor, weight);

            public bool HasNeighbor(int neighborID)
                => _edges.ContainsKey(_graph.Vertices[neighborID]);

            public bool HasNeighbor(Vertex neighbor)
                => _edges.ContainsKey(neighbor);

            public T GetEdgeWeight(int neighborID)
                => _edges[_graph.Vertices[neighborID]];

            public T GetEdgeWeight(Vertex neighbor)
                => _edges[neighbor];

            public bool TryGetEdgeWeight(int neighborID, out T edgeWeight)
                => _edges.TryGetValue(_graph.Vertices[neighborID], out edgeWeight);

            public bool TryGetEdgeWeight(Vertex neighbor, out T edgeWeight)
                => _edges.TryGetValue(neighbor, out edgeWeight);

            public override bool Equals(object obj)
                => (obj as Vertex)?.ID == ID;

            public bool Equals(Vertex other)
                => other.ID == ID;

            public override int GetHashCode()
                => ID;
        }
    }
}
