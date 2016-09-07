namespace Spoj.Library.Helpers
{
    public static class MathHelper
    {
        // http://stackoverflow.com/a/600306
        public static bool IsPowerOfTwo(long n)
            => n <= 0 ? false : (n & (n - 1)) == 0;

        public static int GetFirstPowerOfTwoAtOrAfter(int value)
        {
            int result = 1;
            while (result < value)
            {
                result <<= 1;
            }

            return result;
        }
    }
}
