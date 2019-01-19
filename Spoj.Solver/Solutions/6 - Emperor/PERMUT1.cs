using System;

// https://www.spoj.com/problems/PERMUT1/ #dynamic-programming-2d
// Finds the number of permutations of 1 ... n with k inversions.
public static class PERMUT1
{
    private static readonly int[,] _countOfSequencesOfLengthNWithKInversions = new int[13, 67];

    // Consider sequences of length 3:
    // 1 2 3: 0 inversions
    // 1 3 2: 1 inversion  (3 > 2)
    // 2 1 3: 1 inversion  (2 > 1)
    // 2 3 1: 2 inversions (2 > 1, 3 > 1)
    // 3 1 2: 2 inversions (3 > 1, 3 > 2)
    // 3 2 1: 3 inversions (3 > 2, 3 > 1, 2 > 1)
    // So the row for n=3 in our DP table above will look like:
    // 0 1 2 3 (possible # of inversions for sequences of length 3, AKA, potential 'k' values for n=3)
    // 1 2 2 1 (sequence counts for each of those # of inversions, AKA, counts for each 'k' for n=3)
    // Now we need to see how to figure out the counts for n=4 sequences, using our n=3 knowledge.
    // Observe how we can produce the n=4 permutations from the n=3 permutations:
    // 1 2 3 'spawns' 4 n=4 permutations:
    // (4) 1 2 3
    // 1 (4) 2 3
    // 1 2 (4) 3
    // 1 2 3 (4)
    // 1 3 2 spawns 4 n=4 permutations:
    // (4) 1 3 2
    // 1 (4) 3 2
    // 1 3 (4) 2
    // 1 3 2 (4)
    // And so on; each of the 6 permutations for n=3 spawns 4 n=4 permutations. For each of these new
    // sequences, we're adding a new largest element, (4). Because it's the largest element, it contributes
    // an obvious number of inversions--1 inversion for each element to its right. And all the inversions
    // from n=3 are still there--adding new elements doesn't change existing inversions. So:
    // For each n=3 sequence with 0 inversions, we get a 3, 2, 1, and 0 inversion n=4 sequence (see 1 2 3 above).
    // For each n=3 sequence with 1 inversions, we get a 4, 3, 2, and 1 inversion n=4 sequence (see 1 3 2 above).
    // Here's the full breakdown:
    // For each 0-inversion n=3, we get a: 3, 2, 1, 0 inversion n=4
    // For each 1-inversion n=3, we get a: 4, 3, 2, 1 inversion n=4
    // For each 2-inversion n=3, we get a: 5, 4, 3, 2 inversion n=4
    // For each 3-inversion n=3, we get a: 6, 5, 4, 3 inversion n=4.
    // We can pivot the data to see it from n=4's perspective (format is [sequence length, inversion number] count)
    // [4,0] = [3,0]
    // [4,1] = [3,0] + [3,1]
    // [4,2] = [3,0] + [3,1] + [3,2]
    // [4,3] = [3,0] + [3,1] + [3,2] + [3,3]
    // [4,4] = [3,1] + [3,2] + [3,3]
    // [4,5] = [3,2] + [3,3]
    // [4,6] = [3,3]
    // For example, for each [3,1] we get a [4,3] because we can put the 4 at index 1 in the new sequence, gaining
    // 2 inversions from the 2 elements to its right, and retaining the 1 inversion from the original n=3 sequence.
    // Here's the same breakdown for n=5, just so it's clearer what the pattern is:
    // [5,0]  = [4,0]
    // [5,1]  = [4,0] + [4,1]
    // [5,2]  = [4,0] + [4,1] + [4,2]
    // [5,3]  = [4,0] + [4,1] + [4,2] + [4,3]
    // [5,4]  = [4,0] + [4,1] + [4,2] + [4,3] + [4,4]
    // [5,5]  = [4,1] + [4,2] + [4,3] + [4,4] + [4,5]
    // [5,6]  = [4,2] + [4,3] + [4,4] + [4,5] + [4,6]
    // [5,7]  = [4,3] + [4,4] + [4,5] + [4,6]
    // [5,8]  = [4,4] + [4,5] + [4,6]
    // [5,9]  = [4,5] + [4,6]
    // [5,10] = [4,6]
    static PERMUT1()
    {
        // There is 1 sequence of length 0 (empty sequence). It has 0 inversions.
        // For all higher inversion numbers, [0, 1+] already equals 0.
        _countOfSequencesOfLengthNWithKInversions[0, 0] = 1;

        // There is 1 sequence of length 1 (single element 1). It has 0 inversions.
        // For all higher inversion numbers, [1, 1+] already equals 0.
        _countOfSequencesOfLengthNWithKInversions[1, 0] = 1;

        for (int n = 2; n <= 12; ++n)
        {
            // Max inversions is when the elements are ordered descendingly, so for
            // n=4 sequence that's 4,3,2,1 which has 3+2+1 = 4*3/2 inversions.
            int maxInversionsForNMinus1 = (n - 1) * (n - 2) / 2;
            int maxInversionsForN = n * (n - 1) / 2;
            int pyramidSumOfNMinus1InversionCounts = 0;

            for (int k = 0; k <= maxInversionsForN; ++k)
            {
                if (k <= maxInversionsForNMinus1)
                {
                    pyramidSumOfNMinus1InversionCounts
                        += _countOfSequencesOfLengthNWithKInversions[n - 1, k];
                }

                if (k >= n)
                {
                    pyramidSumOfNMinus1InversionCounts
                        -= _countOfSequencesOfLengthNWithKInversions[n - 1, k - n];
                }

                _countOfSequencesOfLengthNWithKInversions[n, k] = pyramidSumOfNMinus1InversionCounts;
            }
        }
    }

    public static int Solve(int n, int k)
    {
        if (k > 66) // Max number of inversions for n=12 is 12*11/2 = 66.
            return 0;

        return _countOfSequencesOfLengthNWithKInversions[n, k];
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();

            Console.WriteLine(
                PERMUT1.Solve(int.Parse(line[0]), int.Parse(line[1])));
        }
    }
}
