using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/PHONELST/ #strings #trie
// Determines if some phone numbers are consistent--none the prefix of another.
public static class PHONELST
{
    // This uses a modified trie, where the trie's Add method returns a bool indicating
    // whether the string added was a prefix of a string already present, or prefixed
    // by a string already present. In either case, the phone numbers aren't consistent,
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

// Here's a good video on tries: https://www.youtube.com/watch?v=AXjmTQ8LEoI.
public sealed class Trie
{
    private Node _root = new Node((char)0);

    // Modified to return true if the string added was a prefix of or was prefixed by an
    // already existing string. Special care would need to be taken to avoid counting the
    // same string added multiple times as not prefixing the other, but the problem wants
    // the opposite behavior apparently. So it simplies the code just a little.
    public bool Add(string word)
    {
        bool isPrefixedByAWord = false;
        bool isPrefixOfAWord = false;

        Node currentNode = _root;
        Node nextNode;
        int index = 0;

        // Traverse down into the trie until running out of characters or getting to a
        // node that needs a new child.
        while (index < word.Length && currentNode.Children.TryGetValue(word[index], out nextNode))
        {
            currentNode = nextNode;
            ++index;

            if (currentNode.IsAWordEnd)
            {
                // The string up through the current node prefixes the string being added.
                // We test below the assignment because the root node can't be a word end
                // (no empty strings in this problem).
                isPrefixedByAWord = true;
            }
        }

        if (index == word.Length)
        {
            if (currentNode.Children.Count != 0)
            {
                // We're done descending, but there's a string that continues down below
                // this one, so our string prefixes it.
                isPrefixOfAWord = true;
            }
        }
        else
        {
            // The prefix of the word already in the trie has been exhausted, so we know all
            // characters from this point forward will need to have nodes created and wired up.
            while (index < word.Length)
            {
                nextNode = new Node(word[index]);
                currentNode.Children.Add(word[index], nextNode);
                currentNode = nextNode;
                ++index;
            }
        }

        // currentNode is now the node corresponding to the final character in the word,
        // so mark it as a word end.
        currentNode.IsAWordEnd = true;

        return isPrefixedByAWord || isPrefixOfAWord;
    }

    private sealed class Node
    {
        public Node(char value)
        {
            Value = value;
        }

        // Storing Value isn't necessary, it just helps me debug/think clearly.
        public char Value { get; }
        public bool IsAWordEnd { get; set; }
        public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();
    }
}

public static class Program
{
    private static void Main()
    {
        string[] phoneNumbers = new string[10000];
        int remainingTestCases = int.Parse(Console.ReadLine());
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
