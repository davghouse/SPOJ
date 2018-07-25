using System;

// https://www.spoj.com/problems/HACKRNDM/ #sorting #window
// Finds the number of pairs of numbers that are a certain distance away from each other.
public static class HACKRNDM // v1, by sorting an array and then two passes
{
    // Sort the array, then traverse it in descending order with two indices. The small
    // index is always <= the big index. If the numbers aren't far apart enough yet, the
    // small index can be decreased because there's no way decreasing the big index
    // would help, the numbers would just get closer together. If the numbers are too far
    // apart, the big index must be decreased until that's no longer the case, otherwise
    // we could miss some pairs. Binary search could be used to find breakpoints more quickly.
    public static int Solve(int difference, int[] numbers)
    {
        Array.Sort(numbers);

        int count = 0;
        int bigIndex = numbers.Length - 1;
        int smallIndex = numbers.Length - 1;
        while (smallIndex >= 0)
        {
            int bigNumber = numbers[bigIndex];
            int smallNumber = numbers[smallIndex];

            if (bigNumber - smallNumber < difference)
            {
                --smallIndex;
            }
            else if (bigNumber - smallNumber == difference)
            {
                ++count;
                --smallIndex;
                --bigIndex;
            }
            else // bigNumber - smallNumber > difference
            {
                --bigIndex;
            }
        }

        return count;
    }
}

public static class Program
{
    private static void Main()
    {
        string[] line = Console.ReadLine().Split();
        int numberCount = int.Parse(line[0]);
        int difference = int.Parse(line[1]);

        int[] numbers = new int[numberCount];
        for (int i = 0; i < numberCount; ++i)
        {
            numbers[i] = int.Parse(Console.ReadLine());
        }

        Console.Write(HACKRNDM.Solve(difference, numbers));
    }
}
