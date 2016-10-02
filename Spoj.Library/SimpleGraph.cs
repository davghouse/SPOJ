using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library
{
    // Undirected, unweighted graph with no loops or multiple edges: http://mathworld.wolfram.com/SimpleGraph.html.
    // The graph's vertices are stored in an array and the ID of a vertex (from 0 to vertexCount - 1)
    // corresponds to its index in said array. Immutable so far but at least mutable edges later on probably.
    // Not bothering to throw exceptions in the case where vertices from other graphs are passed in.
    public sealed class SimpleGraph
    {
        private readonly Vertex[] _vertices;

        private SimpleGraph(int vertexCount)
        {
            _vertices = new Vertex[vertexCount];
        }

        // For example, edges like (0, 1), (1, 2) => there's an edge between vertices 0 and 1 and 1 and 2.
        public static SimpleGraph CreateFromZeroBasedEdges(int vertexCount, int[,] edges)
        {
            var graph = new SimpleGraph(vertexCount);

            for (int id = 0; id < vertexCount; ++id)
            {
                graph._vertices[id] = new Vertex(graph, id);
            }

            for (int i = 0; i < edges.GetLength(0); ++i)
            {
                graph.AddEdge(edges[i, 0], edges[i, 1]);
            }

            return graph;
        }

        // For example, edges like (1, 2), (2, 3) => there's an edge between vertices 0 and 1 and 1 and 2.
        public static SimpleGraph CreateFromOneBasedEdges(int vertexCount, int[,] edges)
        {
            var graph = new SimpleGraph(vertexCount);

            for (int id = 0; id < vertexCount; ++id)
            {
                graph._vertices[id] = new Vertex(graph, id);
            }

            for (int i = 0; i < edges.GetLength(0); ++i)
            {
                graph.AddEdge(edges[i, 0] - 1, edges[i, 1] - 1);
            }

            return graph;
        }

        public int VertexCount
            => _vertices.Length;

        public IReadOnlyList<Vertex> Vertices
            => Array.AsReadOnly(_vertices);

        private void AddEdge(int firstVertexID, int secondVertexID)
            => AddEdge(_vertices[firstVertexID], _vertices[secondVertexID]);

        private void AddEdge(Vertex firstVertex, Vertex secondVertex)
        {
            firstVertex.AddNeighbor(secondVertex);
            secondVertex.AddNeighbor(firstVertex);
        }

        public bool HasEdge(int firstVertexID, int secondVertexID)
            => HasEdge(_vertices[firstVertexID], _vertices[secondVertexID]);

        public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
            => firstVertex.HasNeighbor(secondVertex);

        // This performs a DFS from an arbitrary start vertex, to determine if the whole graph is reachable from it.
        public bool IsConnected()
        {
            var arbitraryStartVertex = _vertices[VertexCount / 2];
            var discoveredVertexIDs = new HashSet<int> { arbitraryStartVertex.ID };
            var verticesToVisit = new Stack<Vertex>();
            verticesToVisit.Push(arbitraryStartVertex);

            while (verticesToVisit.Count > 0)
            {
                var vertex = verticesToVisit.Pop();

                foreach (var neighbor in vertex.Neighbors)
                {
                    bool neighborWasDiscoveredForTheFirstTime = discoveredVertexIDs.Add(neighbor.ID);
                    if (neighborWasDiscoveredForTheFirstTime)
                    {
                        verticesToVisit.Push(neighbor);
                    }
                }
            }

            return discoveredVertexIDs.Count == VertexCount;
        }

        // Performs a DFS from some vertex in every connected component of the graph, while attempting a 2-coloring.
        // Don't need the count property from a hashset, so using two parallel bit arrays, one for discovery, one for 2-coloring.
        public bool IsBipartite()
        {
            var discoveredVertexIDs = new BitArray(VertexCount);
            var discoveredVertexColors = new BitArray(VertexCount);
            var verticesToVisit = new Stack<Vertex>();

            for (int i = 0; i < _vertices.Length; ++i)
            {
                if (discoveredVertexIDs[i])
                    continue; // Already explored this component.

                discoveredVertexIDs[i] = true;
                discoveredVertexColors[i] = true;
                verticesToVisit.Push(_vertices[i]);

                while (verticesToVisit.Count > 0)
                {
                    var vertex = verticesToVisit.Pop();
                    bool vertexColor = discoveredVertexColors[vertex.ID];

                    foreach (var neighbor in vertex.Neighbors)
                    {
                        // If undiscovered, discover it and color it opposite the vertex we're visiting from (put it in the other set).
                        if (!discoveredVertexIDs[neighbor.ID])
                        {
                            discoveredVertexIDs[neighbor.ID] = true;
                            discoveredVertexColors[neighbor.ID] = !vertexColor;
                            verticesToVisit.Push(neighbor);
                        }
                        // Else, make sure its color isn't the same as the vertex we're visting from (verify its not in the same set).
                        else if (discoveredVertexColors[neighbor.ID] == vertexColor)
                            return false;
                    }
                }
            }

            return true;
        }

        public Tuple<Vertex, int> FindFurthestVertex(int startVertexID)
            => FindFurthestVertex(_vertices[startVertexID]);

        // This finds a furthest vertex from the start vertex, that is, a vertex whose (shortest) path
        // to the start is the longest, and returns that vertex and its distance (path length) from the start.
        public Tuple<Vertex, int> FindFurthestVertex(Vertex startVertex)
        {
            var discoveredVertexIDs = new HashSet<int> { startVertex.ID };
            var verticesToVisit = new Queue<Vertex>();
            verticesToVisit.Enqueue(startVertex);

            Vertex furthestVertex = null;
            int furthestDistance = -1;

            // We visit vertices in waves, where all vertices in the same wave are the same distance
            // from the start vertex, which BFS makes convenient. This allows us to avoid storing
            // distances to the start vertex at the level of individual vertices.
            while (verticesToVisit.Count > 0)
            {
                // We don't care which furthest vertex we get from this wave, so we just choose the first.
                furthestVertex = verticesToVisit.Peek();
                ++furthestDistance;

                int waveSize = verticesToVisit.Count;
                for (int i = 0; i < waveSize; ++i)
                {
                    var vertex = verticesToVisit.Dequeue();

                    foreach (var neighbor in vertex.Neighbors)
                    {
                        bool neighborWasDiscoveredForTheFirstTime = discoveredVertexIDs.Add(neighbor.ID);
                        if (neighborWasDiscoveredForTheFirstTime)
                        {
                            verticesToVisit.Enqueue(neighbor);
                        }
                    }
                }
            }

            return Tuple.Create(furthestVertex, furthestDistance);
        }

        public sealed class Vertex
        {
            private readonly SimpleGraph _graph;
            private readonly HashSet<Vertex> _neighbors = new HashSet<Vertex>();

            internal Vertex(SimpleGraph graph, int ID)
            {
                _graph = graph;
                this.ID = ID;
            }

            public int ID { get; }

            public int Degree
                => _neighbors.Count;

            public IEnumerable<Vertex> Neighbors
                => _neighbors.Skip(0);

            internal void AddNeighbor(int neighborID)
                => AddNeighbor(_graph._vertices[neighborID]);

            internal void AddNeighbor(Vertex neighbor)
                => _neighbors.Add(neighbor);

            public bool HasNeighbor(int neighborID)
                => HasNeighbor(_graph._vertices[neighborID]);

            public bool HasNeighbor(Vertex neighbor)
                => _neighbors.Contains(neighbor);
        }
    }
}
