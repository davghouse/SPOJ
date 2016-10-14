#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

// See OLOLO.cs--this solution was submitted using C++ because C# I/O is too slow.
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
