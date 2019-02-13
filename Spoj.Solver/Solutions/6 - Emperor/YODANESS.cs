using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/YODANESS/ #ad-hoc #binary-search-tree
// Finds the number of word inversions in something Yoda has said.
public static class YODANESS
{
    // We can map Yoda's words to numbers using the ordered statement. Once
    // we do that we need to count the number of inversions in the resulting
    // array, just like CODESPTB or INVCNT.
    public static int Solve(int wordCount, string[] yodaStatement, string[] orderedStatement)
    {
        var wordsOrderedIndices = new Dictionary<string, int>(wordCount);
        for (int w = 0; w < wordCount; ++w)
        {
            wordsOrderedIndices[orderedStatement[w]] = w;
        }

        int[] yodaStatementWordIndices = yodaStatement
            .Select(w => wordsOrderedIndices[w])
            .ToArray();
        var inversionBST = new InversionBST(yodaStatementWordIndices[0]);

        int inversionCount = 0;
        for (int i = 1; i < wordCount; ++i)
        {
            inversionCount += inversionBST.Add(yodaStatementWordIndices[i]);
        }

        return inversionCount;
    }
}

public sealed class InversionBST
{
    private Node _root;

    public InversionBST(int rootValue)
    {
        _root = new Node(rootValue);
    }

    // The specialized Add method counts inversions. Nodes are added into the BST from the input
    // array left to right. The value being added is inverted with any values already in the BST
    // (that is, to its left in the array) that are larger than it. These are the values in
    // the right subtrees (and roots) it passes during the add. Subtree sizes are maintained on
    // each node (acting as a root for their subtree), so we don't need to count them each time.
    public int Add(int value)
    {
        int inversionCount = 0;
        Node node = _root;

        while (true)
        {
            // The value being added is going to the left or right; either way, node's subtree gets bigger.
            ++node.SubtreeSize;

            if (value < node.Value)
            {
                // Going to the left, so inverted with this node and everything in its right subtree.
                inversionCount += 1 + (node.RightChild?.SubtreeSize ?? 0);

                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node(value);
                    return inversionCount;
                }

                node = node.LeftChild;
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new Node(value);
                    return inversionCount;
                }

                node = node.RightChild;
            }
        }
    }

    private sealed class Node
    {
        public Node(int value)
        {
            Value = value;
            SubtreeSize = 1;
        }

        public int Value { get; set; }
        public int SubtreeSize { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int wordCount = int.Parse(Console.ReadLine());
            string[] yodaStatement = Console.ReadLine().Trim().Split();
            string[] orderedStatement = Console.ReadLine().Trim().Split();

            Console.WriteLine(
                YODANESS.Solve(wordCount, yodaStatement, orderedStatement));
        }
    }
}
