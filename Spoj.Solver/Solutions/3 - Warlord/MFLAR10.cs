using System;
using System.Linq;

// https://www.spoj.com/problems/MFLAR10/ #ad-hoc
// Determines if all words in a sentence start with the same character.
public static class MFLAR10
{
    public static bool Solve(string sentence)
    {
        string[] words = sentence.Split(default(char[]),
            // Just in case there's more than 1 space between words.
            StringSplitOptions.RemoveEmptyEntries);
        char firstChar = char.ToUpperInvariant(words[0][0]);

        return words
            .Skip(1)
            .Select(w => char.ToUpperInvariant(w[0]))
            .All(c => c == firstChar);
    }
}

public static class Program
{
    private static void Main()
    {
        string sentence;
        while ((sentence = Console.ReadLine()) != "*")
        {
            if (string.IsNullOrWhiteSpace(sentence))
                continue; // Just in case.

            Console.WriteLine(
                MFLAR10.Solve(sentence) ? "Y" : "N");
        }
    }
}
