using System;
using System.Collections.Generic;
using System.IO;

// https://www.spoj.com/problems/COMDIV/ #division #factors #io #math #primes #sieve
// Finds the number of common divisors shared by two numbers.
public static class COMDIV
{
    private const int _limit = 1000000;
    private const int _sieveLimit = 10000; // (int)ceil(sqrt(_limit))
    // This sieve is slightly different, rather than storing false for prime (unsieved) and true for not
    // prime (sieved), it stores 0 for prime and some prime factor (doesn't matter which) that divides
    // the number for not prime. And has entries for evens. Knowing some prime factor that divides n, we
    // can enumerate all its prime factors by dividing it by that factor, the quotient by its factor, etc.
    private static readonly int[] _factorSieve = new int[_sieveLimit + 1];
    private static readonly List<int> _primes = new List<int>(1229);

    static COMDIV()
    {
        // Check for n up to sqrt(_sieveLimit), as any non-primes <= _sieveLimit with a factor > sqrt(_sieveLimit)
        // must also have a factor < sqrt(_sieveLimit) (otherwise they'd be > _sieveLimit), and so already sieved.
        for (int n = 2; n * n <= _sieveLimit; ++n)
        {
            // If we haven't sieved it yet then it's a prime, so sieve its multiples.
            if (_factorSieve[n] == 0)
            {
                // Multiples of n less than n * n were already sieved from lower primes.
                for (int nextPotentiallyUnsievedMultiple = n * n;
                  nextPotentiallyUnsievedMultiple <= _sieveLimit;
                  nextPotentiallyUnsievedMultiple += n)
                {
                    _factorSieve[nextPotentiallyUnsievedMultiple] = n;
                }
            }
        }

        _primes.Add(2);

        for (int n = 3; n <= _sieveLimit; n += 2)
        {
            if (_factorSieve[n] == 0)
            {
                _primes.Add(n);
            }
        }
    }

    // If it's a common divisor, it must divide the GCD. Therefore, if we find the number of divisors
    // of the GCD, we find the number of common divisors. To find the number of divisors of a number,
    // we need to find its prime factorization. Each factor can be chosen a certain number of times,
    // from 0 up to its power in the factorization, independently of all other factors.
    // n = p1^e1 * p2^e2 * ... * pk^ek => (e1 + 1) * (e2 + 1) * ... * (ek + 1) different combinations
    // of prime factors. This corresponds to the number of divisors, since each different combination
    // has a different prime factorization and is therefore a different number. The case where no
    // factors are chosen corresponds to the divisor 1, which divides everything.
    public static int Solve(int a, int b)
    {
        int gcd = GreatestCommonDivisor(a, b);
        int result = 1;

        // We can use the sieve to extract the prime factors directly.
        if (gcd <= _sieveLimit)
        {
            while (gcd > 1)
            {
                int factor = _factorSieve[gcd];
                int primeFactor = factor == 0 ? gcd : factor;
                int primeFactorCount = 0;
                while (gcd % primeFactor == 0)
                {
                    gcd /= primeFactor;
                    ++primeFactorCount;
                }

                result *= primeFactorCount + 1;
            }
        }
        // We can use trial division up to sqrt(gcd) to find all prime factors.
        else
        {
            for (int p = 0; p < _primes.Count; ++p)
            {
                int prime = _primes[p];

                // Check for factors up to sqrt(gcd), as non-primes with a factor larger than that must also have a factor
                // less than that, otherwise they'd multiply together to make a number greater than n. The fact that gcd
                // is getting smaller doesn't matter. If this condition stops the loop, what remains of gcd is 1 or a single
                // prime factor. All primes less than 'prime' were already divided out, so for gcd to have multiple prime
                // factors they'd have to all be >= 'prime', but in that case the loop wouldn't stop here.
                if (prime * prime > gcd)
                    break;

                if (gcd % prime == 0)
                {
                    int primeFactorCount = 0;
                    while (gcd % prime == 0)
                    {
                        gcd /= prime;
                        ++primeFactorCount;
                    }

                    result *= primeFactorCount + 1;
                }
            }

            // What remains of gcd is either 1 or a (sole) prime we haven't counted yet.
            if (gcd != 1)
            {
                result *= 2;
            }
        }

        return result;
    }

    // This is a good article (first section): http://www.cut-the-knot.org/blue/Euclid.shtml.
    // One point to note, for a = bt + r, the gcd(a, b) divides a so it divides bt + r.
    // And it divides b, so it divides bt, which means for bt + r to be divisible by it,
    // r also needs to be divisible by it. So it divides both b and r. And the article
    // notes the importance of showing not only does it divide b and r, it's also their gcd.
    private static int GreatestCommonDivisor(int a, int b)
    {
        int temp;
        while (b != 0)
        {
            temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            FastIO.WriteNonNegativeInt(
                COMDIV.Solve(FastIO.ReadNonNegativeInt(), FastIO.ReadNonNegativeInt()));
            FastIO.WriteLine();
        }

        FastIO.Flush();
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _newLine = (byte)'\n';
    private const byte _minusSign = (byte)'-';
    private const byte _zero = (byte)'0';
    private const int _inputBufferLimit = 8192;
    private const int _outputBufferLimit = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

    private static readonly Stream _outputStream = Console.OpenStandardOutput();
    private static readonly byte[] _outputBuffer = new byte[_outputBufferLimit];
    private static readonly byte[] _digitsBuffer = new byte[11];
    private static int _outputBufferSize = 0;

    private static byte ReadByte()
    {
        if (_inputBufferIndex == _inputBufferSize)
        {
            _inputBufferIndex = 0;
            _inputBufferSize = _inputStream.Read(_inputBuffer, 0, _inputBufferLimit);
            if (_inputBufferSize == 0)
                return _null; // All input has been read.
        }

        return _inputBuffer[_inputBufferIndex++];
    }

    public static int ReadNonNegativeInt()
    {
        byte digit;

        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        do
        {
            digit = ReadByte();
        }
        while (digit < _minusSign);

        // Build up the integer from its digits, until we run into whitespace or the null byte.
        int result = digit - _zero;
        while (true)
        {
            digit = ReadByte();
            if (digit < _zero) break;
            result = result * 10 + (digit - _zero);
        }

        return result;
    }

    public static void WriteNonNegativeInt(int value)
    {
        int digitCount = 0;
        do
        {
            int digit = value % 10;
            _digitsBuffer[digitCount++] = (byte)(digit + _zero);
            value /= 10;
        } while (value > 0);

        if (_outputBufferSize + digitCount > _outputBufferLimit)
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        while (digitCount > 0)
        {
            _outputBuffer[_outputBufferSize++] = _digitsBuffer[--digitCount];
        }
    }

    public static void WriteLine()
    {
        if (_outputBufferSize == _outputBufferLimit) // else _outputBufferSize < _outputBufferLimit.
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        _outputBuffer[_outputBufferSize++] = _newLine;
    }

    public static void Flush()
    {
        _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
        _outputBufferSize = 0;
        _outputStream.Flush();
    }
}
