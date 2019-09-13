using System;
using System.Collections.Generic;

namespace Spoj.Library
{
    // This facilitates predicate-based binary searching, where the values being searched on
    // satisfy the predicate in an ordered manner, in one of two ways:
    // [false false false ... false true ... true true true] (true => anything larger is true)
    // [true true true ... true false ... false false false] (true => anything smaller is true)
    // In the first, the goal of the search is to locate the smallest value satisfying the predicate.
    // In the second, the goal of the search is to locate the largest value satisfying the predicate.
    // For more info, see: https://www.topcoder.com/community/data-science/data-science-tutorials/binary-search/.
    public static class BinarySearch
    {
        public enum Mode
        {
            FalseToTrue,
            TrueToFalse
        };

        public static int? Search(int start, int end, Predicate<int> verifier, Mode mode)
            => mode == Mode.FalseToTrue
            ? SearchFalseToTrue(start, end, verifier)
            : SearchTrueToFalse(start, end, verifier);

        // When given an array, the verifier should be built against the values in the array, not its
        // indices. To satisfy all needs, the index of the found value (rather than the value), is returned.
        public static int? Search(int[] values, Predicate<int> verifier, Mode mode)
            => Search(0, values.Length - 1, i => verifier(values[i]), mode);

        private static int? SearchFalseToTrue(int start, int end, Predicate<int> verifier)
        {
            if (start > end) return null;

            int mid;

            while (start != end)
            {
                mid = start + (end - start) / 2;

                if (verifier(mid))
                {
                    end = mid;
                }
                else
                {
                    start = mid + 1;
                }
            }

            return verifier(start) ? start : (int?)null;
        }

        private static int? SearchTrueToFalse(int start, int end, Predicate<int> verifier)
        {
            if (start > end) return null;

            int mid;

            while (start != end)
            {
                mid = start + (end - start + 1) / 2;

                if (verifier(mid))
                {
                    start = mid;
                }
                else
                {
                    end = mid - 1;
                }
            }

            return verifier(start) ? start : (int?)null;
        }
    }
}
