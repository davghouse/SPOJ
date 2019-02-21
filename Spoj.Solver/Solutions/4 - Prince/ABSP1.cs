using System;

// https://www.spoj.com/problems/ABSP1/ #ad-hoc #proof
// Sums up the absolute differences of all distinct pairs in a pre-sorted array.
public static class ABSP1
{
    // Consider A0, A1, A2, ..., An-1, in increasing order. As the guy's solution
    // attempt shows, the answer is:
    //   |A0 - A1| + |A0 - A2| + ... + |A0 - An-1|
    // + |A1 - A2| + |A1 - A3| + ... + |A1 - An-1|
    // ...
    // + |An-2 - An-1|
    // We can get rid of the absolute value notation by making use of the fact
    // that the array is ordered:
    //   (A1 - A0) + (A2 - A0) + ... + (An-1 - A0)
    // + (A2 - A1) + (A3 - A1) + ... + (An-1 - A1)
    // ...
    // + (An-1 - An-2)
    // 
    // =   -(n - 1)A0 - (n - 2)A1 - ... - An-2
    //    + (n - 1)An-1 + (n - 2)An-2 + ... + A1
    // => Contribution from Ai =  -(n - 1 - i)Ai + iAi
    // n - 1 - i is the count of numbers larger than Ai--effectively, Ai is subtracted
    // from the sum when paired with numbers larger than it. i is the count of numbers
    // smaller than Ai--effectively, Ai is added to the sum when paired with numbers
    // smaller than it.
    public static long Solve(int[] numbers)
    {
        long absoluteDifferencesSum = 0;
        for (int i = 0; i < numbers.Length; ++i)
        {
            int number = numbers[i];
            int smallerNumbersCount = i;
            int largerNumbersCount = numbers.Length - 1 - i;

            absoluteDifferencesSum += (smallerNumbersCount - largerNumbersCount) * (long)number;
        }

        return absoluteDifferencesSum;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int numberCount = int.Parse(Console.ReadLine());
            int[] numbers = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            Console.WriteLine(
                ABSP1.Solve(numbers));
        }
    }
}
