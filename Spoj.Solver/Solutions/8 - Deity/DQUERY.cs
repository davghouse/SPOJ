using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://www.spoj.com/problems/DQUERY/ #bit #offline #research #sorting
// Finds the number of distinct elements in a subrange of an array.
// See DQUERY.cpp--this solution was submitted using C++ because C# is too slow. Benchmarking a 200k/30k case shows
// it's almost as good as the C++ solution on my local machine, and within the time limits of the problem, so oh well.
public static class DQUERY
{
    // Needed hints to solve this one, this technique is kind of explained in lots of places but never clearly.
    // After looking through some links I was effectively solving with the following hints:
    // - Consider how to solve the problem where the queries always extend to the array's end.
    // - You can do that using a BIT/segment tree with same cardinality as the source array, with the standard sum operation.
    // - For the general case, you can read all the input at once (offline algorithm), and sort it in a specific way, then
    // leverage the update operation on the BIT between query phases.

    // If you still need help after thinking about that a lot, consider querying and updating 0s and 1s in the BIT, where 1s
    // signify the latest occurrence (or first, depending on how you traverse & order things) of the value at that index
    // in the source array. How will you order the queries? How/when will you update the BIT (just a small update each time)?
    public static int[] SolveOffline(int[] sourceArray, DistinctCountQuery[] queries)
    {
        int[] queryResults = new int[queries.Length];

        // Queries are performed in phases, a phase for each of the sourceArray.Length possible query end indices.
        // The query start index doesn't matter, just the fact that all queries in a phase share an end index.
        // The phases will proceed in ascending order of query end indices, which is why the query objects
        // were sorted that way below. A PURQ BIT is queried within phases and updated between them. For
        // any given phase, the PURQ BIT is always in a state such that it can only answer distinct count queries
        // which have an end index equal to the phase's end index. The BIT's underlying array has 0s and 1s,
        // where a 1 at an index means the value there is the latest occurrence of the value up to the phase's
        // end index. The BIT returns sums like normal, but with this construction the sums correspond to the
        // distinct count of values within the queried range. That's because for a given phase, all queries extend
        // up to the phase's end index, so for any value known to be within the queried range, the latest occurrence of
        // the value up to the phase's end index is definitely within the range, and its underlying BIT value accounts
        // for a single 1 added to the returned sum. After a phase is complete, we increment the query end index
        // for the next phase, update the BIT so the value there has a 1 (it's last, so definitely the latest for its
        // value), and turn off any earlier value marked with a 1, since it's no longer the latest.
        Array.Sort(queries);
        var purqBinaryIndexedTree = new PURQBinaryIndexedTree(sourceArray.Length);
        var valuesLatestOccurrenceIndices = new Dictionary<int, int>(sourceArray.Length);
        int queryIndex = 0;

        for (int phaseEndIndex = 0;
            phaseEndIndex < sourceArray.Length && queryIndex < queries.Length;
            ++phaseEndIndex)
        {
            int endValue = sourceArray[phaseEndIndex];
            int endValuesPreviousLatestOccurrenceIndex;
            if (valuesLatestOccurrenceIndices.TryGetValue(endValue, out endValuesPreviousLatestOccurrenceIndex))
            {
                purqBinaryIndexedTree.PointUpdate(endValuesPreviousLatestOccurrenceIndex, -1);
            }
            purqBinaryIndexedTree.PointUpdate(phaseEndIndex, 1);
            valuesLatestOccurrenceIndices[endValue] = phaseEndIndex;

            DistinctCountQuery query;
            while (queryIndex < queries.Length
                && (query = queries[queryIndex]).QueryEndIndex == phaseEndIndex)
            {
                queryResults[query.ResultIndex] = purqBinaryIndexedTree.SumQuery(query.QueryStartIndex, phaseEndIndex);
                ++queryIndex;
            }
        }

        return queryResults;
    }

    public static int[] SolveSlowly(int[] sourceArray, DistinctCountQuery[] queries)
    {
        int[] results = new int[queries.Length];

        foreach (DistinctCountQuery query in queries)
        {
            int[] subrangeArray = new int[query.QueryEndIndex - query.QueryStartIndex + 1];
            Array.Copy(sourceArray, query.QueryStartIndex, subrangeArray, 0, subrangeArray.Length);

            results[query.ResultIndex] = subrangeArray.Distinct().Count();
        }

        return results;
    }
}

public struct DistinctCountQuery : IComparable<DistinctCountQuery>
{
    public DistinctCountQuery(int queryStartIndex, int queryEndIndex, int resultIndex)
    {
        QueryStartIndex = queryStartIndex;
        QueryEndIndex = queryEndIndex;
        ResultIndex = resultIndex;
    }

    public int QueryStartIndex { get; }
    public int QueryEndIndex { get; }
    public int ResultIndex { get; }

    // Sort by ascending query end indices, which is what the weird looking subtraction achieves.
    public int CompareTo(DistinctCountQuery other)
        => QueryEndIndex - other.QueryEndIndex;
}

// Point update, range query binary indexed tree. This is the original BIT described
// by Fenwick. There are lots of tutorials online but I'd start with these two videos:
// https://www.youtube.com/watch?v=v_wj_mOAlig, https://www.youtube.com/watch?v=CWDQJGaN1gY.
// Those make the querying part clear but don't really describe the update part very well.
// For that, I'd go and read Fenwick's paper. This is all a lot less intuitive than segment trees.
public sealed class PURQBinaryIndexedTree
{
    private readonly int[] _tree;

    public PURQBinaryIndexedTree(int arrayLength)
    {
        _tree = new int[arrayLength + 1];
    }

    // There's a way to do this in O(n) instead of O(nlogn), apparently.
    public PURQBinaryIndexedTree(IReadOnlyList<int> array)
    {
        _tree = new int[array.Count + 1];

        for (int i = 0; i < array.Count; ++i)
        {
            PointUpdate(i, array[i]);
        }
    }

    // Updates to reflect an addition at an index of the original array (by traversing the update tree).
    public void PointUpdate(int updateIndex, int delta)
    {
        for (++updateIndex;
            updateIndex < _tree.Length;
            updateIndex += updateIndex & -updateIndex)
        {
            _tree[updateIndex] += delta;
        }
    }

    // Computes the sum from the zeroth index through the query index (by traversing the interrogation tree).
    private int SumQuery(int queryEndIndex)
    {
        int sum = 0;
        for (++queryEndIndex;
            queryEndIndex > 0;
            queryEndIndex -= queryEndIndex & -queryEndIndex)
        {
            sum += _tree[queryEndIndex];
        }

        return sum;
    }

    // Computes the sum from the start through the end query index, by removing the part we
    // shouldn't have counted. Fenwick describes a more efficient way to do this, but it's complicated.
    public int SumQuery(int queryStartIndex, int queryEndIndex)
        => SumQuery(queryEndIndex) - SumQuery(queryStartIndex - 1);
}

public static class Program
{
    private static void Main()
    {
        Console.ReadLine();
        int[] sourceArray = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);

        int queryCount = int.Parse(Console.ReadLine());
        var queries = new DistinctCountQuery[queryCount];

        for (int q = 0; q < queryCount; ++q)
        {
            string[] queryIndices = Console.ReadLine().Split();
            queries[q] = new DistinctCountQuery(
                queryStartIndex: int.Parse(queryIndices[0]) - 1,
                queryEndIndex: int.Parse(queryIndices[1]) - 1,
                resultIndex: q);
        }

        int[] queryResults = DQUERY.SolveOffline(sourceArray, queries);

        var output = new StringBuilder();
        foreach (int queryResult in queryResults)
        {
            output.AppendLine(queryResult.ToString());
        }

        Console.Write(output);
    }
}
