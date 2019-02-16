using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://www.spoj.com/problems/WORDS1/ #euler-path #graph-theory
// Determines if words can be arranged in sequence s.t. end/start letters match.
public static class WORDS1
{
    // Tricky part is noticing that instead of words being vertices, words can be edges in
    // a graph whose vertices represent letters. Then there's an edge between two vertices
    // for each word that starts with the start vertex's letter and ends with the end vertex's
    // letter. For example, cat => edge between c and t because if you reach a word ending in c,
    // you can ride the edge representing the word cat to any word starting with t, like
    // ...c [cat] t... There are fast algorithms for determing Euler path (use all edges once),
    // but not for determining Hamiltonian path (uses all vertices once), so this transformation
    // really helps. Intuitively I guess the transformation helps so much because it's not the
    // words that matter (which words as vertices approach doesn't realize), it's just their
    // starting and ending letters (which words as edges approach does realize).
    public static bool Solve(string[] words)
    {
        // For convenience, map the words' starting and ending letters to arbitrary IDs
        // between [0, the number of distinct starting and ending letters).
        var startAndEndingLetterIDs = words.Select(w => w[0])
            .Union(words.Select(w => w[w.Length - 1]))
            .Select((l, i) => new { letter = l, ID = i })
            .ToDictionary(p => p.letter, p => p.ID);
        var directedLetterGraph = new DirectedGraph(startAndEndingLetterIDs.Count);
        foreach (string word in words)
        {
            directedLetterGraph.AddEdge(
                startVertexID: startAndEndingLetterIDs[word[0]],
                endVertexID: startAndEndingLetterIDs[word[word.Length - 1]]);
        }

        /* According to Wikipedia: A directed graph has an Eulerian path if and only if at most one
         * vertex has (out-degree) − (in-degree) = 1, at most one vertex has (in-degree) − (out-degree) = 1,
         * every other vertex has equal in-degree and out-degree, and all of its vertices with nonzero
         * degree belong to a single connected component of the underlying undirected graph... 
           And it's a path, not a cycle that we're looking for--don't need to return to start word. */

        if (directedLetterGraph.Vertices.Count(v => v.OutDegree - v.InDegree == 1) > 1
            || directedLetterGraph.Vertices.Count(v => v.InDegree - v.OutDegree == 1) > 1
            || directedLetterGraph.Vertices
                .Where(v => v.OutDegree - v.InDegree != 1)
                .Where(v => v.InDegree - v.OutDegree != 1)
                .Any(v => v.OutDegree != v.InDegree))
            return false;

        var undirectedLetterGraph = new UndirectedGraph(startAndEndingLetterIDs.Count);
        foreach (string word in words)
        {
            undirectedLetterGraph.AddEdge(
                firstVertexID: startAndEndingLetterIDs[word[0]],
                secondVertexID: startAndEndingLetterIDs[word[word.Length - 1]]);
        }

        return undirectedLetterGraph.IsConnected();
    }
}

public sealed class DirectedGraph
{
    public DirectedGraph(int vertexCount)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = vertices;
    }

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public void AddEdge(int startVertexID, int endVertexID)
        => AddEdge(Vertices[startVertexID], Vertices[endVertexID]);

    public void AddEdge(Vertex startVertex, Vertex endVertex)
        => startVertex.AddNeighbor(endVertex);

    public bool HasEdge(int startVertexID, int endVertexID)
        => HasEdge(Vertices[startVertexID], Vertices[endVertexID]);

    public bool HasEdge(Vertex startVertex, Vertex endVertex)
        => startVertex.HasNeighbor(endVertex);

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly DirectedGraph _graph;
        private readonly List<Vertex> _neighbors = new List<Vertex>();

        internal Vertex(DirectedGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public IReadOnlyCollection<Vertex> Neighbors => _neighbors;
        public int OutDegree => _neighbors.Count;
        public int InDegree { get; private set; }

        internal void AddNeighbor(int neighborID)
            => _neighbors.Add(_graph.Vertices[neighborID]);

        internal void AddNeighbor(Vertex neighbor)
        {
            _neighbors.Add(neighbor);
            ++neighbor.InDegree;
        }

        public bool HasNeighbor(int neighborID)
            => _neighbors.Contains(_graph.Vertices[neighborID]);

        public bool HasNeighbor(Vertex neighbor)
            => _neighbors.Contains(neighbor);

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;
    }
}

public sealed class UndirectedGraph
{
    public UndirectedGraph(int vertexCount)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = vertices;
    }

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public void AddEdge(int firstVertexID, int secondVertexID)
        => AddEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public void AddEdge(Vertex firstVertex, Vertex secondVertex)
    {
        firstVertex.AddNeighbor(secondVertex);
        secondVertex.AddNeighbor(firstVertex);
    }

    public bool HasEdge(int firstVertexID, int secondVertexID)
        => HasEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
        => firstVertex.HasNeighbor(secondVertex);

    // DFSes from an arbitrary start vertex, to determine if the whole graph is reachable from it.
    public bool IsConnected()
    {
        var arbitraryStartVertex = Vertices[VertexCount / 2];
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

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly UndirectedGraph _graph;
        private readonly HashSet<Vertex> _neighbors = new HashSet<Vertex>();

        internal Vertex(UndirectedGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public IReadOnlyCollection<Vertex> Neighbors => _neighbors;
        public int Degree => _neighbors.Count;

        internal void AddNeighbor(int neighborID)
            => _neighbors.Add(_graph.Vertices[neighborID]);

        internal void AddNeighbor(Vertex neighbor)
            => _neighbors.Add(neighbor);

        public bool HasNeighbor(int neighborID)
            => _neighbors.Contains(_graph.Vertices[neighborID]);

        public bool HasNeighbor(Vertex neighbor)
            => _neighbors.Contains(neighbor);

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;
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
            int wordCount = int.Parse(Console.ReadLine());
            string[] words = new string[wordCount];
            for (int w = 0; w < wordCount; ++w)
            {
                words[w] = Console.ReadLine().Trim();
            }

            output.AppendLine(WORDS1.Solve(words)
                ? "Ordering is possible."
                : "The door cannot be opened.");
        }

        Console.Write(output);
    }
}
