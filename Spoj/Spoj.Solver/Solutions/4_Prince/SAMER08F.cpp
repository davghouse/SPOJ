#include <iostream>

int const limit = 100;

int sumFromOneUntil(int n)
{
  return n * (n + 1) / 2;
}

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
