using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/SHPATH/
public static class InputVerifier
{
    private static void Main()
    {
        int testCount = int.Parse(Console.ReadLine());
        if (testCount < 1 || testCount > 10)
            throw new ArgumentOutOfRangeException();

        for (int t = 0; t < testCount; ++t)
        {
            int cityCount = int.Parse(Console.ReadLine());
            if (cityCount < 1 || cityCount > 10000)
                throw new ArgumentOutOfRangeException();

            var cities = new HashSet<string>();
            for (int c = 0; c < cityCount; ++c)
            {
                string city = Console.ReadLine();
                if (!city.All(Char.IsLower) || city.Length < 1 || city.Length > 10)
                    throw new ArgumentException();
                if (!cities.Add(city))
                    throw new ArgumentException();

                int neighborCount = int.Parse(Console.ReadLine());
                if (neighborCount < 0 || neighborCount > cityCount - 1)
                    throw new ArgumentOutOfRangeException();

                var neighborIndices = new HashSet<int>();
                for (int n = 0; n < neighborCount; ++n)
                {
                    string[] line = Console.ReadLine().Split();

                    int neighborIndex = int.Parse(line[0]);
                    if (neighborIndex < 1 || neighborIndex > cityCount)
                        throw new ArgumentOutOfRangeException();

                    if (neighborIndex == c + 1)
                        throw new ArgumentException(); // No self-loops?

                    if (!neighborIndices.Add(neighborIndex))
                        throw new ArgumentException();

                    int cost = int.Parse(line[1]);
                    if (cost < 1 || cost > 200000)
                        throw new ArgumentOutOfRangeException();
                }
            }

            int pathCount = int.Parse(Console.ReadLine());
            if (pathCount < 1 || pathCount > 100)
                throw new ArgumentOutOfRangeException();

            for (int p = 0; p < pathCount; ++p)
            {
                string[] line = Console.ReadLine().Split();
                string sourceCity = line[0];
                string destinationCity = line[1];
                if (!cities.Contains(sourceCity) || !cities.Contains(destinationCity))
                    throw new ArgumentException();
            }

            Console.ReadLine();
        }
    }
}
