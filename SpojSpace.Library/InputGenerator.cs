using System;
using System.Collections.Generic;
using System.Linq;

namespace SpojSpace.Library
{
    public static class InputGenerator
    {
        // This isn't thread-safe, but it doesn't matter (yet): http://csharpindepth.com/Articles/Chapter12/Random.aspx.
        public static readonly Random Rand = new Random();

        // Inclusive min and max values.
        public static int[] GenerateRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            int[] ints = new int[count];
            for (int i = 0; i < count; ++i)
            {
                ints[i] = Rand.Next(minValue, maxValue + 1);
            }

            return ints;
        }

        // Inclusive min and max values.
        public static int[] GenerateDistinctRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            var distinctRandomInts = new HashSet<int>();
            int[] ints = new int[count];
            while (distinctRandomInts.Count < count)
            {
                int num = Rand.Next(minValue, maxValue + 1);
                if (distinctRandomInts.Add(num))
                {
                    ints[distinctRandomInts.Count - 1] = num;
                }
            }

            return ints;
        }

        // Inclusive min and max values.
        public static string GenerateRandomString(int length, char minValue = 'a', char maxValue = 'z')
            => new string(GenerateRandomInts(length, minValue, maxValue).Select(i => (char)i).ToArray());

        // Inclusive min and max values.
        public static int[,] GenerateRandomEvenOddPairs(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            if (minValue == maxValue)
                throw new NotSupportedException("Can't generate even and odd numbers from a single number.");

            int[,] evenOddPairs = new int[count, 2];
            for (int i = 0; i < count; ++i)
            {
                int even = Rand.Next(minValue, maxValue + 1);
                int odd = Rand.Next(minValue, maxValue + 1);
                evenOddPairs[i, 0] = even % 2 == 0 ? even
                    : even - 1 >= minValue ? even - 1
                    : even + 1;
                evenOddPairs[i, 1] = odd % 2 == 1 ? odd
                    : odd - 1 >= minValue ? odd - 1
                    : odd + 1;
            }

            return evenOddPairs;
        }

        public static int[,] GenerateRandomMinMaxPairs(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            int[,] minMaxPairs = new int[count, 2];
            for (int i = 0; i < count; ++i)
            {
                int num1 = Rand.Next(minValue, maxValue + 1);
                int num2 = Rand.Next(minValue, maxValue + 1);
                minMaxPairs[i, 0] = Math.Min(num1, num2);
                minMaxPairs[i, 1] = Math.Max(num1, num2);
            }

            return minMaxPairs;
        }
    }
}
