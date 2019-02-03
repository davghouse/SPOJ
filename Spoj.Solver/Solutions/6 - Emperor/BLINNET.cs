using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/BLINNET/ #disjoint-sets #mst
// Finds the cheapest way to connect some cities together.
public static class BLINNET
{
    public static long Solve(int cityCount, List<Edge> edges)
    {
        edges.Sort((e1, e2) => e1.Cost.CompareTo(e2.Cost));
        var citySets = new DisjointSets(cityCount);
        long totalEdgeCost = 0;

        for (int e = 0; e < edges.Count; ++e)
        {
            if (!citySets.AreInSameSet(edges[e].SourceCity, edges[e].DestinationCity))
            {
                citySets.UnionSets(edges[e].SourceCity, edges[e].DestinationCity);
                totalEdgeCost += edges[e].Cost;
            }
        }

        return totalEdgeCost;
    }
}

// https://en.wikipedia.org/wiki/Disjoint-set_data_structure
// https://www.youtube.com/watch?v=gcmjC-OcWpI
public sealed class DisjointSets
{
    // Can be easily split into two arrays, but let's go with this huge name instead.
    private readonly int[] _elementsParentsOrNegatedSubsetSizes;

    public DisjointSets(int elementCount)
    {
        _elementsParentsOrNegatedSubsetSizes = new int[elementCount];
        for (int i = 0; i < elementCount; ++i)
        {
            _elementsParentsOrNegatedSubsetSizes[i] = -1;
        }

        ElementCount = DisjointSetCount = elementCount;
    }

    public int ElementCount { get; }
    public int DisjointSetCount { get; private set; }

    // Consider the following example:
    // elements:                                   0  1 2  3 4
    // elements' parents or negated subset sizes: -1 -3 1 -1 2
    // Elements with negative numbers are the roots of their sets. The value of the number
    // is the size of the set--so 0 is the root of a set of size 1 (itself), and 1 is the
    // root of a set of size 3. Elements with positive numbers are pointing towards their
    // root. 2 points towards 1 (which we know is the root), and 4 points towards 2, etc.
    private int FindRoot(int element)
    {
        int parentOrNegatedSubsetSize = _elementsParentsOrNegatedSubsetSizes[element];

        return parentOrNegatedSubsetSize >= 0
            // Follow the path towards the parent, and compress. If we run FindParent(4)
            // from the above example, 4 goes to 2 which goes to 1 which returns itself.
            // We compress by setting 4's parent to 1, so it finds it directly next time.
            ? _elementsParentsOrNegatedSubsetSizes[element] = FindRoot(parentOrNegatedSubsetSize)
            // If negative (so a negated subset size), that means it is its own parent.
            : element;
    }

    public void UnionSets(int firstElement, int secondElement)
    {
        int firstRoot = FindRoot(firstElement);
        int secondRoot = FindRoot(secondElement);

        if (firstRoot == secondRoot)
            return;

        int firstSetSize = -1 * _elementsParentsOrNegatedSubsetSizes[firstRoot];
        int secondSetSize = -1* _elementsParentsOrNegatedSubsetSizes[secondRoot];

        int biggerRoot = firstSetSize > secondSetSize ? firstRoot : secondRoot;
        int smallerRoot = firstSetSize > secondSetSize ? secondRoot : firstRoot;

        _elementsParentsOrNegatedSubsetSizes[smallerRoot] = biggerRoot;
        _elementsParentsOrNegatedSubsetSizes[biggerRoot] = -1 * (firstSetSize + secondSetSize);

        --DisjointSetCount;
    }

    public bool AreInSameSet(int firstElement, int secondElement)
        => FindRoot(firstElement) == FindRoot(secondElement);
}

public struct Edge
{
    public Edge(int sourceCity, int destinationCity, int cost)
    {
        SourceCity = sourceCity;
        DestinationCity = destinationCity;
        Cost = cost;
    }

    public int SourceCity { get; }
    public int DestinationCity { get; }
    public int Cost { get; }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int cityCount = FastIO.ReadNonNegativeInt();
            var edges = new List<Edge>(capacity: 2 * cityCount);

            for (int c = 0; c < cityCount; ++c)
            {
                FastIO.ConsumeString(); // Discard city name.

                int neighborCount = FastIO.ReadNonNegativeInt();
                for (int n = 0; n < neighborCount; ++n)
                {
                    edges.Add(new Edge(
                        sourceCity: c,
                        destinationCity: FastIO.ReadNonNegativeInt() - 1,
                        cost: FastIO.ReadNonNegativeInt()));
                }
            }

            output.Append(
                BLINNET.Solve(cityCount, edges));
            output.AppendLine();
        }

        Console.Write(output);
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
    private const int _inputBufferLimit = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

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

    public static void ConsumeString()
    {
        byte letter;

        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        do
        {
            letter = ReadByte();
        }
        while (letter < _minusSign);

        //int stringLength = 0;
        //_stringBuilder[stringLength++] = (char)letter;
        while (true)
        {
            letter = ReadByte();
            if (letter < _zero) break;
            //_stringBuilder[stringLength++] = (char)letter;
        }

        //return new string(_stringBuilder, 0, stringLength);
    }
}
