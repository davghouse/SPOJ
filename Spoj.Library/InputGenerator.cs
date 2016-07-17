using System;
using System.Collections.Generic;

namespace Spoj.Library
{
    public static class InputGenerator
    {
        // Inclusive minValue, exclusive maxValue.
        public static int[] GenerateRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue)
        {
            var rand = new Random();

            int[] ints = new int[count];
            for (int i = 0; i < count; ++i)
            {
                ints[i] = rand.Next(minValue, maxValue);
            }

            return ints;
        }

        // Inclusive minValue, exclusive maxValue.
        public static int[] GenerateDistinctRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue)
        {
            var rand = new Random();

            var distinctRandomInts = new HashSet<int>();
            int[] ints = new int[count];
            while (distinctRandomInts.Count < count)
            {
                int randInt = rand.Next(minValue, maxValue);
                if (distinctRandomInts.Add(randInt))
                {
                    ints[distinctRandomInts.Count - 1] = randInt;
                }
            }

            return ints;
        }
    }
}
