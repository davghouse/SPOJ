#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdexcept>
#include <vector>
using namespace std;

int GreatestCommonDivisor(int a, int b);

// See COMDIV.cs--this solution was submitted using C++ because C# I/O is too slow.
class COMDIV
{
public:
  COMDIV() : _factorSieve(_sieveLimit + 1)
  {
    _factorSieve[0] = -1;
    _factorSieve[1] = -1;

    for (int n = 2; n * n <= _sieveLimit; ++n)
    {
      if (_factorSieve[n] == 0)
      {
        for (int nextPotentiallyUnsievedMultiple = n * n;
          nextPotentiallyUnsievedMultiple <= _sieveLimit;
          nextPotentiallyUnsievedMultiple += n)
        {
          _factorSieve[nextPotentiallyUnsievedMultiple] = n;
        }
      }
    }

    _primes.push_back(2);

    for (int n = 3; n <= _sieveLimit; n += 2)
    {
      if (_factorSieve[n] == 0)
      {
          _primes.push_back(n);
      }
    }
  }

  int Solve(int a, int b)
  {
    int gcd = GreatestCommonDivisor(a, b);
    int result = 1;

    // We can use the sieve to extract the prime factors directly.
    if (gcd <= _sieveLimit)
    {
      while (gcd > 1)
      {
        int primeFactor = _factorSieve[gcd] == 0 ? gcd : _factorSieve[gcd];
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
      for (int p = 0; p < _primes.size(); ++p)
      {
        int prime = _primes[p];
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

private:
  static const int _limit = 1000000;
  static const int _sieveLimit = 10000; //(int)ceil(sqrt(_limit))
  vector<int> _factorSieve;
  vector <int> _primes;
};

int GreatestCommonDivisor(int a, int b)
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

int main()
{
  COMDIV solver;

  int remainingTestCases;
  scanf("%d", &remainingTestCases);

  while (remainingTestCases-- > 0)
  {
    int a, b;
    scanf("%d %d", &a, &b);

    printf("%d\n", solver.Solve(a, b));
  }

  return 0;
}
