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
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue");

            var rand = new Random();

            var ints = new int[count];
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
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue");

            var rand = new Random();

            var distinctRandomInts = new HashSet<int>();
            var ints = new int[count];
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
    }
}
