using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/DSUBSEQ/ #dynamic-programming #memoization #mod-math
// Finds the number of distinct subsequences of a string.
public static class DSUBSEQ
{
    // First, consider the sequence where all the characters are distinct, like ABCD.
    // 1 sequence ends in A--just A. How many end in B? 1--just B itself, plus however many
    // end in A. How many end in C? 1--just C itself, plus however many end in A, plus however
    // many end in B. So it's like this: A:1, B:2, C:4, D:8, for 15 + 1 (empty subsequence) = 16
    // total. This makes sense if you think about binary and that any combination of choices
    // of letters corresponds to a different subsequence, since all letters are different.
    // Now, for when letters can repeat, consider ABCDACA. Up to ABCD it's the same as
    // last time. Then for the first A repeat, it adds the same as it normally would,
    // minus however many subsequences ending in A it'd be redundant with--which is
    // the number of subsequences ending in A from the A before it. Similar for C.
    // Then for the second A, it's what it'd normally be (sum of everything before it + 1),
    // minus the number of subsequences ending A from the two A's before it.
    // So it's like this: A:1, B:2, C:4, D:8, A:15+1-1=15, C:30+1-4=27, A:57+1-15-1=42, for
    // a total of 99 + 1 (empty subsequence) = 100.
    public static int Solve(string @string)
    {
        var subsequencesEndingWithLetter = new Dictionary<char, int>();
        int subsequences = 0;

        foreach (char letter in @string)
        {
            int existingSubsequencesEndingWithLetter;
            subsequencesEndingWithLetter.TryGetValue(letter, out existingSubsequencesEndingWithLetter);

            int newSubsequencesEndingWithLetter = subsequences + 1 - existingSubsequencesEndingWithLetter;
            if (newSubsequencesEndingWithLetter < 0)
            {
                newSubsequencesEndingWithLetter += 1000000007;
            }

            subsequencesEndingWithLetter[letter] =
                (existingSubsequencesEndingWithLetter + newSubsequencesEndingWithLetter) % 1000000007;

            subsequences = (subsequences + newSubsequencesEndingWithLetter) % 1000000007;
        }

        return subsequences + 1 /* don't forget the empty subsequence */;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                DSUBSEQ.Solve(Console.ReadLine()));
        }
    }
}
