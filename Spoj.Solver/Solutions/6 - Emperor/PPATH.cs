using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

// https://www.spoj.com/problems/PPATH/ #graph-theory #primes #sieve
// Finds the shortest path to travel between primes, along primes, in one-digit swaps.
public static class PPATH
{
    private static SimpleGraph _primeGraph;

    static PPATH()
    {
        // 10000 because zero-based indices. This isn't great as we don't need 10000
        // (as most #s don't represent primes), but hopefully it doesn't matter. Edges
        // exist between primes (a vertex whose index is prime) and other primes,
        // when there's a one-digit swap to go between them (swaps are reversible).
        _primeGraph = new SimpleGraph(10000);
        var primeDecider = new SieveOfEratosthenesDecider(9999);

        // If n is a prime, connect to the primes greater than it, within a one-digit swap. Only
        // greater than because lesser primes were already connected to it earlier in the loop.
        for (int n = 1001; n <= 9999; n += 2)
        {
            if (!primeDecider.IsOddPrime(n))
                continue;

            int nSwapped = n + 1000;
            while (nSwapped % 10000 > n)
            {
                if (primeDecider.IsOddPrime(nSwapped))
                {
                    _primeGraph.AddEdge(n, nSwapped);
                }

                nSwapped += 1000;
            }

            nSwapped = n + 100;
            while (nSwapped % 1000 > n % 1000)
            {
                if (primeDecider.IsOddPrime(nSwapped))
                {
                    _primeGraph.AddEdge(n, nSwapped);
                }

                nSwapped += 100;
            }

            nSwapped = n + 10;
            while (nSwapped % 100 > n % 100)
            {
                if (primeDecider.IsOddPrime(nSwapped))
                {
                    _primeGraph.AddEdge(n, nSwapped);
                }

                nSwapped += 10;
            }

            nSwapped = n + 2;
            while (nSwapped % 10 > n % 10)
            {
                if (primeDecider.IsOddPrime(nSwapped))
                {
                    _primeGraph.AddEdge(n, nSwapped);
                }

                nSwapped += 2;
            }
        }
    }

    public static int Solve(int startPrime, int endPrime)
        => _primeGraph.GetShortestPathLength(startPrime, endPrime);
}

// Undirected, unweighted graph with no loops or multiple edges: http://mathworld.wolfram.com/SimpleGraph.html.
// The graph's vertices are stored in an array and the ID of a vertex (from 0 to vertexCount - 1) corresponds to
// its index in that array.
public sealed class SimpleGraph
{
    public SimpleGraph(int vertexCount)
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

    public int GetShortestPathLength(int startVertexID, int endVertexID)
        => GetShortestPathLength(Vertices[startVertexID], Vertices[endVertexID]);

    public int GetShortestPathLength(Vertex startVertex, Vertex endVertex)
    {
        if (startVertex == endVertex) return 0;

        bool[] discoveredVertexIDs = new bool[VertexCount];
        var verticesToVisit = new Queue<Vertex>();
        discoveredVertexIDs[startVertex.ID] = true;
        verticesToVisit.Enqueue(startVertex);

        int distance = 1;

        // We visit vertices in waves, where all vertices in the same wave are the same distance
        // from the start vertex, which BFS makes convenient. This allows us to avoid storing
        // distances to the start vertex at the level of individual vertices. To save work we
        // don't check the wave vertices for endVertex equality, but rather their neighbors.
        // So that's why the distance start off as one rather than zero.
        while (verticesToVisit.Count > 0)
        {
            int waveSize = verticesToVisit.Count;
            for (int i = 0; i < waveSize; ++i)
            {
                var vertex = verticesToVisit.Dequeue();

                foreach (var neighbor in vertex.Neighbors)
                {
                    if (!discoveredVertexIDs[neighbor.ID]) 
                    {
                        if (neighbor == endVertex)
                            return distance;

                        discoveredVertexIDs[neighbor.ID] = true;
                        verticesToVisit.Enqueue(neighbor);
                    }
                }
            }
            ++distance;
        }

        return -1;
    }

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly SimpleGraph _graph;
        private readonly HashSet<Vertex> _neighbors = new HashSet<Vertex>();

        internal Vertex(SimpleGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public IEnumerable<Vertex> Neighbors => _neighbors;
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

// This sieve has some optimizations to avoid storing results for even integers; the result for an odd
// integer n is stored at index n / 2. IsOddPrime is supplied for convenience (input n assumed to be odd).
public sealed class SieveOfEratosthenesDecider
{
    private readonly IReadOnlyList<bool> _sieve;

    public SieveOfEratosthenesDecider(int limit)
    {
        Limit = limit;

        bool[] sieve = new bool[(Limit + 1) >> 1];
        sieve[0] = true; // 1 (which maps to index [1 / 2] == [0]) is not a prime, so sieve it out.

        // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
        // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
        for (int n = 3; n * n <= Limit; n += 2)
        {
            // If we haven't sieved it yet then it's a prime, so sieve its multiples.
            if (!sieve[n >> 1])
            {
                // Multiples of n less than n * n were already sieved from lower primes. Add twice
                // n for each iteration, as otherwise it's odd + odd = even.
                for (int nextPotentiallyUnsievedMultiple = n * n;
                    nextPotentiallyUnsievedMultiple <= Limit;
                    nextPotentiallyUnsievedMultiple += (n << 1))
                {
                    sieve[nextPotentiallyUnsievedMultiple >> 1] = true;
                }
            }
        }
        _sieve = sieve;
    }

    public int Limit { get; }

    public bool IsPrime(int n)
        => (n & 1) == 0 ? n == 2 : IsOddPrime(n);

    public bool IsOddPrime(int n)
        => !_sieve[n >> 1];
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] primes = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int result = PPATH.Solve(primes[0], primes[1]);
            output.AppendLine(result >= 0 ? result.ToString() : "Impossible");
        }

        Console.Write(output);
    }
}
