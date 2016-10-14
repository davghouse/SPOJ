using System;
using System.Collections.Generic;

// 4560 http://www.spoj.com/problems/ANARC05B/ The Double HeLiX
// Maximizes the sum while traversing a pair of intersecting, ordered sequences.
public static class ANARC05B
{
    // Sequences are effectively one-indexed as the first element is the sequence size.
    // And they're strictly increasing. And there's no cost for jumping between sequences,
    // so we can just do this greedily. The sequence to use from each intersection point
    // is the one whose sum until the next intersection point is greatest.
    public static int Solve(int[] firstSequence, int[] secondSequence)
    {
        int firstSequenceLength = firstSequence[0];
        int secondSequenceLength = secondSequence[0];
        var intersectionPoints = new Queue<Tuple<int, int>>();

        for (int fi = 1; fi <= firstSequenceLength; ++fi)
        {
            // Could save work by only searching from the previous intersection point (w/o queue), nah.
            int si = Array.BinarySearch(secondSequence, 1, secondSequenceLength, firstSequence[fi]);
            if (si > 0)
            {
                intersectionPoints.Enqueue(Tuple.Create(fi, si));
            }
        }

        int maximumSum = 0;
        int indexSummedThroughForFirstSequence = 0;
        int indexSummedThroughForSecondSequence = 0;

        while (intersectionPoints.Count > 0)
        {
            var intersectionPoint = intersectionPoints.Dequeue();

            maximumSum += Math.Max(
                GetSumBetween(firstSequence,
                    startIndex: indexSummedThroughForFirstSequence + 1,
                    endIndex: intersectionPoint.Item1),
                GetSumBetween(secondSequence,
                    startIndex: indexSummedThroughForSecondSequence + 1,
                    endIndex: intersectionPoint.Item2));

            indexSummedThroughForFirstSequence = intersectionPoint.Item1;
            indexSummedThroughForSecondSequence = intersectionPoint.Item2;
        }

        // One more choice: should we fall off the end of the first or the second (if we haven't already)?
        maximumSum += Math.Max(
            GetSumBetween(firstSequence,
                startIndex: indexSummedThroughForFirstSequence + 1,
                endIndex: firstSequenceLength),
            GetSumBetween(secondSequence,
                startIndex: indexSummedThroughForSecondSequence + 1,
                endIndex: secondSequenceLength));

        return maximumSum;
    }

    private static int GetSumBetween(int[] sequence, int startIndex, int endIndex)
    {
        int sum = 0;
        for (int i = startIndex; i <= endIndex; ++i)
        {
            sum += sequence[i];
        }

        return sum;
    }
}

public static class Program
{
    private static void Main()
    {
        int[] firstSequence;
        int[] secondSequence;

        while ((firstSequence = Array.ConvertAll(
            Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
            int.Parse)).Length != 1)
        {
            secondSequence = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            Console.WriteLine(
                ANARC05B.Solve(firstSequence, secondSequence));
        }
    }
}
