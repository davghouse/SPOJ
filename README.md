SPOJ
============

C# solutions to problems on SPOJ: http://www.spoj.com/users/davidgalehouse/.

Initially I want to solve the first 200 most solved problems, as recommended by some guides on Quora.
Then if I'm not there already I want to keep going until I'm past the tons of kids who spam the comment sections with "AC 1 go", "easy one :)", and "cakewalk".

Each problem has tags to aid searching and a quick summary of the problem statement to refresh my memory.
I/O is separated from the actual solving of the problem to keep everything clean.

In the entry points to my solvers I'm usually not bothering to accept the most general parameter types for the given input.
For example, I just go with arrays instead of IReadOnlyLists, IReadOnlyCollections, or IEnumerables, even if I only need reads.
To do otherwise would be kind of annoying and would introduce some overhead.
The annoyances result when I want to consume the input array in-place and use it as a workspace.
The solver's job conceptually won't necessitate changing the array in any way, it'll just be convenient.
The overhead results from [slower iteration times](http://stackoverflow.com/q/4256928]), even with no change to the underlying object, just exposing it through an interface.
That may never matter, but it's enough for me to justify taking the input in whatever form the I/O handler builds it.
I **do** worry about that stuff for any reusable components, which are housed in the Spoj.Library project.

On the rare occasion that C# is too slow (usually due to I/O speed), I use C++.
Style-wise I try to make my C++ solutions look like my C# solutions, because I don't remember the more common conventions.
