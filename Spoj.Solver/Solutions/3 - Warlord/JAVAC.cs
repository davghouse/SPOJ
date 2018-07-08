using System;
using System.Linq;
using System.Text;

// https://www.spoj.com/problems/JAVAC/ #ad-hoc #strings
// Turns Java-like identifiers into C++-like identifiers, and vice versa.
public static class JAVAC
{
    public static string Solve(string identifier)
    {
        bool isJavaIdentifier = char.IsLower(identifier[0])
            && identifier.All(c => char.IsLetter(c));

        // Just doing !isJavaIdentifier to save work here; technically the identifier could be both.
        bool isCppIdentifier = !isJavaIdentifier
            && char.IsLower(identifier[0])
            && char.IsLower(identifier[identifier.Length - 1]);
        for (int i = 1; i < identifier.Length - 1 && isCppIdentifier; ++i)
        {
            char l = identifier[i];
            char r = identifier[i + 1];

            // Either the character is lower, or it's an underscore with a word beginning immediately after.
            isCppIdentifier = char.IsLower(l) || (l == '_' && char.IsLower(r));
        }

        var output = new StringBuilder();
        if (isJavaIdentifier)
        {
            for (int i = 0; i < identifier.Length; ++i)
            {
                char c = identifier[i];
                if (char.IsLower(c))
                {
                    output.Append(c);
                }
                else // It's an uppercase, so replace U with _u.
                {
                    output.Append('_');
                    output.Append(char.ToLower(c));
                }
            }
        }
        else if (isCppIdentifier)
        {
            for (int i = 0; i < identifier.Length; ++i)
            {
                char c = identifier[i];
                if (char.IsLower(c))
                {
                    output.Append(c);
                }
                else // It's an underscore, so replace _u with U.
                {
                    output.Append(char.ToUpper(identifier[++i]));
                }
            }
        }
        else
        {
            output.Append("Error!");
        }

        return output.ToString();
    }
}

public static class Program
{
    private static void Main()
    {
        string identifier;
        while (!string.IsNullOrEmpty(identifier = Console.ReadLine()))
        {
            Console.WriteLine(
                JAVAC.Solve(identifier));
        }
    }
}
