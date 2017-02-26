using System;
using System.Collections.Generic;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It's seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: http://www.spoj.com/problems/CSTREET/ (it's improperly formatted)
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 100)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            int pricePerFurlong = int.Parse(Console.ReadLine());
            if (pricePerFurlong < 1)
                throw new FormatException();

            int buildingCount = int.Parse(Console.ReadLine());
            if (buildingCount < 1 || buildingCount > 1000)
                throw new FormatException();

            int streetCount = int.Parse(Console.ReadLine());
            if (streetCount < 1 || streetCount > 300000)
                throw new FormatException();

            // Used to verify the graph is simple (no multiple edges/different streets between same buildings).
            var streets = new HashSet<Tuple<int, int>>();
            for (int s = 1; s <= streetCount; ++s)
            {
                string[] line = Console.ReadLine().Split();

                int firstBuilding = int.Parse(line[0]);
                if (firstBuilding < 1 || firstBuilding > buildingCount)
                    throw new FormatException();

                int secondBuilding = int.Parse(line[1]);
                if (secondBuilding < 1 || secondBuilding > buildingCount)
                    throw new FormatException();

                // Verify we're working with integers for the length, and street costs fit in ints too.
                int length = int.Parse(line[2]);
                double d_length = double.Parse(line[2]);
                if (d_length - length > 0.000001)
                    throw new FormatException();

                int cost = length * pricePerFurlong;
                double d_weight = (double)length * pricePerFurlong;
                if (d_weight - cost > 0.000001)
                    throw new FormatException();

                if (cost == int.MaxValue)
                    throw new FormatException();

                if (firstBuilding <= secondBuilding)
                {
                    streets.Add(Tuple.Create(firstBuilding, secondBuilding));
                }
                else
                {
                    streets.Add(Tuple.Create(secondBuilding, firstBuilding));
                }
            }
        }
    }
}
