SPOJ
=================

C# solutions to problems on SPOJ: https://www.spoj.com/users/davidgalehouse/.

Each solution has tags to aid searching and a quick summary of the problem to refresh my memory.
Each solution has unit testing, which relies on compiling them from their source programmatically using Roslyn.
This is necessary because I want all solutions to be submittable to SPOJ without modifications or namespace clutter, so their build actions have to be set to none to prevent compilation errors like [CS0101](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0101) and [CS0017](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0017).

I/O is separated from the actual solving of the problem to keep everything clean, barring special performance considerations.
Reusable components are housed in the Spoj.Library project.
Due to performance concerns I usually don't bother programming with extensibility or safety in mind.
For example, I seal classes aggressively and expose collections through readonly interfaces without bothering to prevent casting back to the non-readonly implementations.

Problems are organized by difficulty, using [Civ V's difficulty levels](https://www.civfanatics.com/civ5/info/difficulties/).
I'm trying to rank them on an absolute scale, not relative to my changing skill level.

I/O performance in C# can be an issue for some problems.
I use System.Console I/O whenever possible, but custom I/O handling is sometimes necessary.

On the rare occasions that C# is unavailable or insufficient, I use C++.
Style-wise I try to make my C++ solutions look like my C# solutions, because I don't remember the more common conventions.
