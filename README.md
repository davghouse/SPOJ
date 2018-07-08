SPOJ
=================

C# solutions to problems on SPOJ: https://www.spoj.com/users/davidgalehouse/.

Initially I want to solve the first 200 most solved problems, as recommended by some guides on Quora.
Then if I'm not there already I want to keep going until I'm past the tons of kids who spam the comment sections with "AC 1 go", "easy one :)", and "cakewalk".

I/O is separated from the actual solving of the problem to keep everything clean, barring special performance considerations in rare cases.
In the entry points to my solvers I'm usually not bothering to accept the most general parameter types for the given input.
For example, I just go with arrays instead of IReadOnlyLists, IReadOnlyCollections, or IEnumerables, even if I only need reads.
To do otherwise would be kind of annoying and would introduce some overhead.
The annoyances result when I want to consume the input array in-place and use it as a workspace.
The solver's job conceptually won't necessitate changing the array in any way, it'll just be convenient.
The overhead results from [slower iteration times](http://stackoverflow.com/q/4256928), even with no change to the underlying object, just exposing it through an interface.
That may never matter, but it's enough for me to justify taking the input in whatever form the I/O handler builds it.
It's going to get thrown out immediately after anyway.

I **do** bother thinking about parameter types (and lots of other details) for any reusable components, which are housed in the Spoj.Library project.
However, due to similar performance concerns as above and YAGNI, I usually don't bother programming with extensibility or safety in mind.
For example, I seal classes aggressively and expose collections through readonly interfaces without bothering to prevent casting back to the non-readonly implementations.

Problems are organized by difficulty, using [Civ V's difficulty levels](https://www.civfanatics.com/civ5/info/difficulties/).
It's not a linear scale from 1 to 8. Settler through Warlord are really easy.
Prince is easy, and King is almost easy.
Emperor can be hard at least at the start.
Immortal is hard or really hard if you don't get a good start.
Deity needs a really good start and probably a lot of save reloads and some cheesing.
It's hard to come up with rankings for some problems, and after reflection I tend to move solutions around once or twice.
But I'm trying to rank them on an absolute scale, not relative to my changing skill level.

Each solution has tags to aid searching and a quick summary of the problem to refresh my memory.
Each solution has unit testing, which relies on compiling them from their source programmatically via a CodeDOM provider.
This is necessary because I want all solutions to be submittable to SPOJ without modifications or namespace clutter, so their build actions have to be set to none to prevent compilation errors.

On the rare occasion that C# is too slow (usually due to I/O speed), I use C++.
Style-wise I try to make my C++ solutions look like my C# solutions, because I don't remember the more common conventions.
