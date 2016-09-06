namespace Spoj.Library.Primes
{
    public abstract class PrimeDecider
    {
        public PrimeDecider(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; }

        public abstract bool IsPrime(int n);
    }
}