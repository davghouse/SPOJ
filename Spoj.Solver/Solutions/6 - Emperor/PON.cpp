#include <cstdlib>
#include <iostream>
using namespace std;

typedef unsigned long long ull;

// Submitted using C++ because C# was unavailable--see PON.cs for details.
class PON
{
public:
  static bool Solve(ull n)
  {
    return MillerRabinTest(n, 10);
  }

private:
  static bool MillerRabinTest(ull n, int witnessCount)
  {
    if (n < 2) return false;
    if (n == 2 || n == 3) return true;
    if ((n & 1) == 0) return false;

    ull d = n - 1;
    int r = 0;
    while ((d & 1) == 0)
    {
      d >>= 1;
      ++r;
    }

    while (witnessCount-- > 0)
    {
      if (!MillerRabinWitness(n, d, r))
        return false; // composite
    }

    return true; // probably prime
  }

  static bool MillerRabinWitness(ull n, ull d, int r)
  {
      ull a = rand() % (n - 3) + 2;
      ull x = ModularPow(a, d, n);

      if (x == 1 || x == n - 1)
         return true;

      while (--r > 0)
      {
        x = ModularMultiply(x, x, n);

        if (x == n - 1)
          return true;
      }

      return false; // composite
  }

  static ull ModularMultiply(ull a, ull b, ull modulus)
  {
    ull result = 0;
    a = a % modulus;
    while (b > 0)
    {
      if ((b & 1) == 1)
      {
        result = (result + a) % modulus;
      }
      a = (a << 1) % modulus;
      b >>= 1;
    }

    return result % modulus;
  }

  static ull ModularPow(ull base, ull exponent, ull modulus)
  {
    ull result = 1;
    base = base % modulus;
    while (exponent != 0)
    {
        if ((exponent & 1) == 1)
        {
            result = ModularMultiply(result, base, modulus);
        }

        base = ModularMultiply(base, base, modulus);
        exponent >>= 1;
    }

    return result;
  }
};

int main()
{
  int remainingTestCases;
  cin >> remainingTestCases;

  while (remainingTestCases-- > 0)
  {
    ull n;
    cin >> n;

    cout << (PON::Solve(n) ? "YES" : "NO") << endl;
  }

  return 0;
}
