#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

// Here for reference--initially submitted using C++, but now OLOLO.cs is fast enough.
int main()
{
  int remainingPyanis, pyaniNumber, result = 0;
  scanf("%d", &remainingPyanis);

  while (remainingPyanis-- > 0)
  {
    scanf("%d", &pyaniNumber);
    result ^= pyaniNumber;
  }

  printf("%d", result);

  return 0;
}
