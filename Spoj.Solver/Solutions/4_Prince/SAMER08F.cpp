#include <iostream>

int const limit = 100;

int sumFromOneUntil(int n)
{
  return n * (n + 1) / 2;
}

// 3410 http://www.spoj.com/problems/SAMER08F/ Feynman
// Returns the number of squares in a grid of n x n squares, for 1 <= n <= 100.
// (see .cs version for more info)
int main()
{
  int squareCounts[limit + 1];
  squareCounts[0] = 0;
  squareCounts[1] = 1;
  squareCounts[2] = 5;

  for (int n = 3; n <= limit; ++n)
      squareCounts[n] = 1 + 4 * sumFromOneUntil(n - 1) + squareCounts[n - 2];

  int n;
  while (std::cin >> n && n != 0)
    std::cout << squareCounts[n] << std::endl;

  return 0;
}
