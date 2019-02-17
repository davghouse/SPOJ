#include <iostream>
using namespace std;

// Submitted using C++ because C# was unavailable--see BOMARBLE.cs for details.
class BOMARBLE
{
public:
  static int Solve(int n)
  {
    return (3 * n * n + 5 * n + 2) / 2;
  }
};

int main()
{
  int n;
  while (cin >> n && n != 0)
    cout << BOMARBLE::Solve(n) << endl;

  return 0;
}
