using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/BRCKTS/
public static class InputVerifier
{
    private static void Main()
    {
        for (int t = 1; t <= 10; ++t)
        {
            int bracketCount = int.Parse(Console.ReadLine());
            if (bracketCount < 1 || bracketCount > 30000)
                throw new ArgumentOutOfRangeException();

            string brackets = Console.ReadLine();
            if (brackets.Length != bracketCount || brackets.Any(c => c != '(' && c != ')'))
                throw new ArgumentException();

            int operationCount = int.Parse(Console.ReadLine());
            for (int o = 1; o <= operationCount; ++o)
            {
                int operation = int.Parse(Console.ReadLine());
                if (operation < 0 || operation > bracketCount)
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
