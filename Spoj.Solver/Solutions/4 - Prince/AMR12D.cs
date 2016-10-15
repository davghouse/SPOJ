using System;

// 13043 http://www.spoj.com/problems/AMR12D/ The Mirror of Galadriel
// Determines if the reverse of every substring exists in a string.
public static class AMR12D
{
    // One substring is the entire string. For the reverse of the entire string
    // to be in the string, the reverse would need to be the same as the string,
    // since the string is the only substring long enough to match the reversed string.
    // But given it's a palindrome, the reverse of all other substrings also exist.
    // To see this, just imagine flipping a substring across the midpoint, the match
    // always there on the other side (but in reverse) since the string is the same
    // forwards and backwards.
    public static bool Solve(string s)
    {
        for (int i = 0, j = s.Length - 1; i < j; ++i, --j)
        {
            if (s[i] != s[j])
                return false;
        }

        return true;
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
                AMR12D.Solve(Console.ReadLine()) ? "YES" : "NO");
        }
    }
}
