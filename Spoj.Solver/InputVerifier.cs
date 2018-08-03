using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/LCA/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int vertexCount = int.Parse(Console.ReadLine());
            if (vertexCount < 1 || vertexCount > 1000)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < vertexCount; ++i)
            {
                int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                int childCount = line[0];
                if (childCount < 0 || childCount > 999)
                    throw new ArgumentOutOfRangeException();
                if (line.Length != childCount + 1)
                    throw new ArgumentException();
            }

            int queryCount = int.Parse(Console.ReadLine());
            if (queryCount < 1 || queryCount > 1000)
                throw new ArgumentOutOfRangeException();

            for (int q = 0; q < queryCount; ++q)
            {
                string[] line = Console.ReadLine().Split();
                if (line.Length != 2)
                    throw new ArgumentException();

                int firstVertexID = int.Parse(line[0]);
                if (firstVertexID < 1 || firstVertexID > 1000)
                    throw new ArgumentOutOfRangeException();

                int secondVertexID = int.Parse(line[1]);
                if (secondVertexID < 1 || secondVertexID > 1000)
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
