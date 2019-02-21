SPOJ
=================

C# solutions to the 200 most-solved problems on SPOJ: https://www.spoj.com/users/davidgalehouse/

Solutions have unit testing, which relies on compiling them programmatically using Roslyn.
This is necessary because I want them to be submittable to SPOJ without modifications or namespace clutter, so their build actions have to be set to none to prevent compilation errors like [CS0101](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0101) and [CS0017](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0017).

To keep everything clean, I/O is separated from the actual problem solving.
Performance of .NET's number parsing and Console I/O can be an issue.
I use them whenever possible, but [custom I/O handling](Spoj.Library/IO/FastIO.cs) is sometimes necessary.

Reusable components are housed in the Spoj.Library project.
Due to performance concerns I usually don't bother programming with extensibility or safety in mind.

Problems are organized by difficulty using [the levels from Civ V](https://www.civfanatics.com/civ5/info/difficulties/).
