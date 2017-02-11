using System.Collections.Generic;

namespace Daves.SpojSpace.Library
{
    public static class Mod10Exponentiator
    {
        // Last digit patterns for repeated exponentiations of bases 0 through 9.
        // A repeating last digit establishes the pattern, as the next last digit depends only
        // on the last digits being multiplied, not on any digits after, as those just
        // add parts that are divisible by 10 and hence don't affect the mod 10 calculation.
        private static readonly IReadOnlyList<IReadOnlyList<int>> _bases0To9LastDigitExponentiationPatterns;

        static Mod10Exponentiator()
        {
            int[] base0Pattern = new[] { 0 }; // 0, (0).
            int[] base1Pattern = new[] { 1 }; // 1, (1).
            int[] base2Pattern = new[] { 2, 4, 8, 6 }; // 2, 4, 8, 16, (32)
            int[] base3Pattern = new[] { 3, 9, 7, 1 }; // 3, 9, 27, 81, (243)
            int[] base4Pattern = new[] { 4, 6 }; // 4, 16, (64)
            int[] base5Pattern = new[] { 5 }; // 5, (25)
            int[] base6Pattern = new[] { 6 }; // 6, (36)
            int[] base7Pattern = new[] { 7, 9, 3, 1 }; // 7, 49, 343, 2401, (16807)
            int[] base8Pattern = new[] { 8, 4, 2, 6 }; // 8, 64, 512, 4096, (32768)
            int[] base9Pattern = new[] { 9, 1 }; // 9, 81, (729)

            _bases0To9LastDigitExponentiationPatterns = new int[][]
            {
                base0Pattern, base1Pattern, base2Pattern, base3Pattern, base4Pattern,
                base5Pattern, base6Pattern, base7Pattern, base8Pattern, base9Pattern
            };
        }

        public static int Compute(int b, int e)
        {
            if (e == 0) return 1;

            // The parts 10 or above of b don't impact the last digit of b^e; only the last digit d matters.
            // It's easy to see this by imagining the polynomial expansion of b^e = ((b - d) + d)^e,
            // where (b - d) is divisible by 10 (0 mod 10), so only the d^e term in the expansion matters.
            int d = b % 10;

            IReadOnlyList<int> lastDigitExponentiationPattern = _bases0To9LastDigitExponentiationPatterns[d];

            // Pattern starts at exponent of 1 and ends at exponent of lastDigitExponentiationPattern.Count,
            // so e mod the count almost gives the correct position, just have to move zero back to the end.
            int patternPosition = e % lastDigitExponentiationPattern.Count;
            if (patternPosition == 0)
            {
                patternPosition = lastDigitExponentiationPattern.Count;
            }

            return lastDigitExponentiationPattern[patternPosition - 1];
        }
    }
}
