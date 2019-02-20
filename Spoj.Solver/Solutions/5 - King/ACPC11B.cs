using System;
using System.Text;

// https://www.spoj.com/problems/ACPC11B/ #binary-search #merge #sorting
// Finds the closest pair of altitudes, one from each of two mountains.
public static class ACPC11B
{
    // Knowing where an altitude on one mountain goes relative to the sort of the other mountain's
    // altitudes effectively tells us the closest altitudes (the ones to its left and right).
    // One option is to sort both and then perform a merge basically, but it's only necessary
    // to sort one and traverse the second while binary searching on the first. So it'll be
    // O(nlogn) + O(mlogn) ~ O((n + m)logn) sorting/binary searching on the n, the n + m
    // is commutative so might as well minimize the logn term by sorting the smallest array.
    public static int Solve(int[] firstAltitudes, int[] secondAltitudes)
    {
        int[] sortedAltitudes;
        int[] unsortedAltitudes;
        if (firstAltitudes.Length < secondAltitudes.Length)
        {
            Array.Sort(firstAltitudes);
            sortedAltitudes = firstAltitudes;
            unsortedAltitudes = secondAltitudes;
        }
        else
        {
            Array.Sort(secondAltitudes);
            sortedAltitudes = secondAltitudes;
            unsortedAltitudes = firstAltitudes;
        }

        int minimumDifference = int.MaxValue;
        for (int a = 0; a < unsortedAltitudes.Length; ++a)
        {
            int altitude = unsortedAltitudes[a];

            int index = Array.BinarySearch(sortedAltitudes, altitude);
            if (index >= 0)
            {
                // The altitude for this mountain is also an altitude for the other mountain.
                return 0;
            }
            else
            {
                // The altitude for this mountain isn't one on the other mountain, but the bitwise
                // complement of what's returned by binary search tells us the index of the first altitude
                // that's greater than this one, (or falls off the end of the array if bigger than all).
                index = ~index;
                if (index == 0)
                {
                    // Altitude is less than all altitudes on the other mountain.
                    minimumDifference = Math.Min(
                        minimumDifference,
                        sortedAltitudes[0] - altitude);
                }
                else if (index < sortedAltitudes.Length)
                {
                    // Altitude is between two altitudes on the other mountain.
                    minimumDifference = Math.Min(
                        minimumDifference,
                        altitude - sortedAltitudes[index - 1]);
                    minimumDifference = Math.Min(
                        minimumDifference,
                        sortedAltitudes[index] - altitude);
                }
                else
                {
                    // Altitude is greater than all altitudes on the other mountain.
                    minimumDifference = Math.Min(
                        minimumDifference,
                        altitude - sortedAltitudes[sortedAltitudes.Length - 1]);
                }
            }
        }

        return minimumDifference;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] firstLine = Console.ReadLine().Split();
            string[] secondLine = Console.ReadLine().Split();

            int[] firstAltitudes = new int[firstLine.Length - 1];
            for (int a = 0; a < firstLine.Length - 1; ++a)
            {
                firstAltitudes[a] = int.Parse(firstLine[a + 1]);
            }

            int[] secondAltitudes = new int[secondLine.Length - 1];
            for (int a = 0; a < secondLine.Length - 1; ++a)
            {
                secondAltitudes[a] = int.Parse(secondLine[a + 1]);
            }

            output.Append(
                ACPC11B.Solve(firstAltitudes, secondAltitudes));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
