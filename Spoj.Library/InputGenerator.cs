using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library
{
    public static class InputGenerator
    {
        // Inclusive min and max values.
        public static int[] GenerateRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            var rand = new Random();

            int[] ints = new int[count];
            for (int i = 0; i < count; ++i)
            {
                ints[i] = rand.Next(minValue, maxValue + 1);
            }

            return ints;
        }

        // Inclusive min and max values.
        public static int[] GenerateDistinctRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            var rand = new Random();

            var distinctRandomInts = new HashSet<int>();
            int[] ints = new int[count];
            while (distinctRandomInts.Count < count)
            {
                int randInt = rand.Next(minValue, maxValue + 1);
                if (distinctRandomInts.Add(randInt))
                {
                    ints[distinctRandomInts.Count - 1] = randInt;
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

            var rand = new Random();

            int[,] evenOddPairs = new int[count, 2];
            for (int i = 0; i < count; ++i)
            {
                var even = rand.Next(minValue, maxValue + 1);
                var odd = rand.Next(minValue, maxValue + 1);
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
            var rand = new Random();

            int[,] minMaxPairs = new int[count, 2];
            for (int i = 0; i < count; ++i)
            {
                var num1 = rand.Next(minValue, maxValue + 1);
                var num2 = rand.Next(minValue, maxValue + 1);
                minMaxPairs[i, 0] = Math.Min(num1, num2);
                minMaxPairs[i, 1] = Math.Max(num1, num2);
            }

            return minMaxPairs;
        }
    }
}
