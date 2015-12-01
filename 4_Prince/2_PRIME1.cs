using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

public class PRIME1
{
    public const int Billion = 1000000000;

    public static void Main()
    {
        SieveStrategy strategy = SieveStrategy.UpToSqrt;

        SieveOfEratosthenes sieve = new SieveOfEratosthenes(Billion, strategy);

        int numberOfTestCases = int.Parse(Console.ReadLine());

        for(int i = 0; i < numberOfTestCases; ++i)
        {
            string[] limits = Console.ReadLine().Split();
            int lower = Int32.Parse(limits[0]);
            int upper = Int32.Parse(limits[1]);

            for(int j = lower; j <= upper; ++j)
            {
                if(sieve.IsPrime(j))
                {
                    Console.WriteLine(j);
                }
            }
            Console.WriteLine();
        }
    }
}

public class SieveOfEratosthenes
{
    private Sieve _sieve;

    /// <summary>
    ///  Creates a Sieve of Eratosthenes where upperLimit is the highest integer you care about. 
    /// </summary>
    /// <param name="upperLimit"></param>
    public SieveOfEratosthenes(int upperLimit, SieveStrategy strategy)
    {
        if (strategy == SieveStrategy.Everything)
        {
            _sieve = new SieveEverything(upperLimit);
        }
        else if (strategy == SieveStrategy.UpToSqrt)
        {
            _sieve = new SieveUpToSqrt(upperLimit);
        }
    }

    public bool IsPrime(int integer)
    {
        return _sieve.IsPrime(integer);
    }

    private class SieveEverything : Sieve 
    {
        public SieveEverything(int upperLimit)
        {
            _notPrimes = new BitArray(upperLimit + 1);

            _notPrimes[0] = true;
            _notPrimes[1] = true;

            // Only mark multiples up to the sqrt of the upperLimit, as anything above is a prime or a multiple of something below. 
            for (int i = 2; i <= Math.Sqrt(upperLimit); ++i)
            {
                // If it's a prime, mark its multiples as not prime.
                if (!_notPrimes[i])
                {
                    int j = i*(i - 1);
                    while ((j += i) <= upperLimit)
                    {
                        _notPrimes[j] = true;
                    }
                }
            }
        }

        public override bool IsPrime(int integer)
        {
            return !_notPrimes[integer];
        }
    }

    private class SieveUpToSqrt : Sieve
    {
        private int _sqrtOfLimit;
        private SieveEverything _sieveEverythingUpToSqrt;
        private List<int> _primes;

        public SieveUpToSqrt(int upperLimit)
        {
            _sqrtOfLimit = (int)Math.Sqrt(upperLimit);

            _sieveEverythingUpToSqrt = new SieveEverything(_sqrtOfLimit);

            _primes = new List<int>(4000);

            for(int i = 0; i <= _sqrtOfLimit; ++i)
            {
                if(_sieveEverythingUpToSqrt.IsPrime(i))
                {
                    _primes.Add(i);
                }
            }
        }

        public override bool IsPrime(int integer)
        {
            if (integer <= _sqrtOfLimit)
            {
                return _sieveEverythingUpToSqrt.IsPrime(integer);
            }
            else
            {
                for (int i = 0; i < _primes.Count && _primes[i] <= Math.Sqrt(integer); ++i)
                {
                    if (integer % _primes[i] == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

    private abstract class Sieve
    {
        // Indices correspond to integer values; their value in the BitArray is false if they're prime and true if they're not.
        protected BitArray _notPrimes;

        public abstract bool IsPrime(int integer);
    }
}

public enum SieveStrategy
{
    Everything,
    UpToSqrt,
}
