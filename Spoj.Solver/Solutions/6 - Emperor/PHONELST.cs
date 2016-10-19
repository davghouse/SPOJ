using System;
using System.Collections.Generic;

// 4033 http://www.spoj.com/problems/PHONELST/ Phone List
// Determines if some phone numbers or consistent--none the prefix of another.
public static class PHONELST
{
    // This uses a modified trie, where the trie's Add method returns a bool indicating
    // whether the string added was a prefix of a string already present, or prefixed
    // by string already preset. In either case, the phone numbers aren't consistent,
    // so we short-circuit and just remember to consume the rest of the input.
    public static bool Solve(int phoneNumberCount, string[] phoneNumbers)
    {
        var trie = new Trie();

        for (int i = 0; i < phoneNumberCount; ++i)
        {
            if (trie.Add(phoneNumbers[i]))
                return false; // Some string prefixed another, so it's not consistent.
        }

        return true; // No string prefixed another, so it's consistent.
    }
}
public class Trie
{
    private Node _root = new Node((char)0);

    // Modified to return true if the string added was a prefix of or was prefixed by an already existing string.
    // Special care would need to be taken to avoid counting the same string added multiple times as not prefixing
    // the other, but the problem wants the opposite behavior apparently. So it simplies the code just a little.
    public bool Add(string s)
    {
        bool isPrefixedByAWord = false;
        bool isPrefixOfAWord = false;

        Node currentNode = _root;
        Node nextNode;
        int i = 0;

        // Traverse down into the trie until we run out of characters or get to a node that needs a new child.
        while (i < s.Length && currentNode.Children.TryGetValue(s[i], out nextNode))
        {
            currentNode = nextNode;
            ++i;

            if (currentNode.IsAWordEnd)
            {
                // The string up through the current node prefixes the string being added. We test below the
                // assignment because the root node can't be a word end (no empty strings in this problem).
                isPrefixedByAWord = true;
            }
        }

        if (i == s.Length)
        {
            if (currentNode.Children.Count != 0)
            {
                // We're done descending, but there's a string that continues down below this one, so our string prefixes it.
                isPrefixOfAWord = true;
            }
        }
        else
        {
            // We've now exhausted the prefix of s already in the trie, so we know all characters from this point
            // forward will need to have nodes created be created and wired up.
            for (; i < s.Length; ++i)
            {
                nextNode = new Node(s[i]);
                currentNode.Children.Add(s[i], nextNode);
                currentNode = nextNode;
            }
        }

        // currentNode is now the node corresponding to the final character in s, so we mark it as a word end.
        currentNode.IsAWordEnd = true;

        return isPrefixedByAWord || isPrefixOfAWord;
    }

    private class Node
    {
        public Node(char value)
        {
            Value = value;
        }

        // Storing this value isn't necessary, it just helps me debug and think clearly about what's going on.
        public char Value { get; }

        public bool IsAWordEnd { get; set; }

        public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        string[] phoneNumbers = new string[10000];

        while (remainingTestCases-- > 0)
        {
            int phoneNumberCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < phoneNumberCount; ++i)
            {
                phoneNumbers[i] = Console.ReadLine();
            }

            Console.WriteLine(
                PHONELST.Solve(phoneNumberCount, phoneNumbers) ? "YES" : "NO");
        }
    }
}
