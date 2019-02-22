SPOJ
=================

C# solutions to the 200 most-solved problems on SPOJ: https://www.spoj.com/users/davidgalehouse/

Solutions are unit tested, which relies on compiling them from source programmatically using Roslyn.
This is necessary because I want them to be submittable to SPOJ without modification or namespace clutter, so their build actions have to be set to none to prevent compilation errors like [CS0101](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0101) and [CS0017](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0017).

To keep everything clean, I/O is separated from the actual problem solving.
Performance of .NET's number parsing and Console I/O can be an issue.
I use the framework whenever possible, but [custom I/O handling](Spoj.Library/IO/FastIO.cs) is sometimes necessary.

Reusable components are housed in the Spoj.Library project.
Due to performance concerns I usually don't bother programming with extensibility or safety in mind.

Problems are organized (roughly) by difficulty using [the levels from Civ V](https://civilization.fandom.com/wiki/Difficulty_level_(Civ5)), from Settler (easiest) to Deity (hardest).

Submission History
------------------
|Date|Problem|Solution|Tags|Difficulty|
|----|-------|--------|:--:|----------|
|2015/04/28|[TEST](https://www.spoj.com/problems/TEST/)|[TEST.cs](Spoj.Solver/Solutions/1%20-%20Settler/TEST.cs)|#io|Settler|
|2015/09/20|[PRIME1](https://www.spoj.com/problems/PRIME1/)|[PRIME1.cs](Spoj.Solver/Solutions/5%20-%20King/PRIME1.cs)|#primes #sieve|King|
|2015/12/28|[ADDREV](https://www.spoj.com/problems/ADDREV/)|[ADDREV.cs](Spoj.Solver/Solutions/2%20-%20Chieftain/ADDREV.cs)|#ad-hoc #digits|Chieftain|
|2016/01/01|[ONP](https://www.spoj.com/problems/ONP/)|[ONP.cs](Spoj.Solver/Solutions/4%20-%20Prince/ONP.cs)|#parsing #recursion #stack #strings|Prince|
|2016/01/05|[FCTRL](https://www.spoj.com/problems/FCTRL/)|[FCTRL.cs](Spoj.Solver/Solutions/5%20-%20King/FCTRL.cs)|#factorial #factors #math|King|
|2016/06/04|[FCTRL2](https://www.spoj.com/problems/FCTRL2/)|[FCTRL2.cs](Spoj.Solver/Solutions/6%20-%20Emperor/FCTRL2.cs)|#big-numbers #factorial|Emperor|
|2016/06/05|[SAMER08F](https://www.spoj.com/problems/SAMER08F/)|[SAMER08F.cs](Spoj.Solver/Solutions/5%20-%20King/SAMER08F.cs)|#ad-hoc #dynamic-programming-1d #math|King|
|2016/06/05|[NSTEPS](https://www.spoj.com/problems/NSTEPS/)|[NSTEPS.cs](Spoj.Solver/Solutions/2%20-%20Chieftain/NSTEPS.cs)|#ad-hoc|Chieftain|
|2016/06/07|[ACPC10A](https://www.spoj.com/problems/ACPC10A/)|[ACPC10A.cs](Spoj.Solver/Solutions/3%20-%20Warlord/ACPC10A.cs)|#sequence|Warlord|
|2016/06/08|[CANDY](https://www.spoj.com/problems/CANDY/)|[CANDY.cs](Spoj.Solver/Solutions/3%20-%20Warlord/CANDY.cs)|#ad-hoc #division|Warlord|
|2016/06/10|[FASHION](https://www.spoj.com/problems/FASHION/)|[FASHION.cs](Spoj.Solver/Solutions/4%20-%20Prince/FASHION.cs)|#ad-hoc #sorting|Prince|
|2016/06/10|[TOANDFRO](https://www.spoj.com/problems/TOANDFRO/)|[TOANDFRO.cs](Spoj.Solver/Solutions/3%20-%20Warlord/TOANDFRO.cs)|#ad-hoc #strings|Warlord|
|2016/06/12|[AE00](https://www.spoj.com/problems/AE00/)|[AE00.cs](Spoj.Solver/Solutions/4%20-%20Prince/AE00.cs)|#division #math|Prince|
|2016/06/12|[LASTDIG](https://www.spoj.com/problems/LASTDIG/)|[LASTDIG.cs](Spoj.Solver/Solutions/5%20-%20King/LASTDIG.cs)|#digits #mod-math|King|
|2016/06/16|[JULKA](https://www.spoj.com/problems/JULKA/)|[JULKA.cs](Spoj.Solver/Solutions/6%20-%20Emperor/JULKA.cs)|#big-numbers #math|Emperor|
|2016/06/19|[CANDY3](https://www.spoj.com/problems/CANDY3/)|[CANDY3.cs](Spoj.Solver/Solutions/3%20-%20Warlord/CANDY3.cs)|#division #mod-math|Warlord|
|2016/06/19|[COINS](https://www.spoj.com/problems/COINS/)|[COINS.cs](Spoj.Solver/Solutions/5%20-%20King/COINS.cs)|#dynamic-programming-1d #recursion|King|
|2016/06/25|[EIGHTS](https://www.spoj.com/problems/EIGHTS/)|[EIGHTS.cs](Spoj.Solver/Solutions/4%20-%20Prince/EIGHTS.cs)|#digits #math|Prince|
|2016/06/26|[HANGOVER](https://www.spoj.com/problems/HANGOVER/)|[HANGOVER.cs](Spoj.Solver/Solutions/3%20-%20Warlord/HANGOVER.cs)|#binary-search #sequence|Warlord|
|2016/06/28|[PALIN](https://www.spoj.com/problems/PALIN/)|[PALIN.cs](Spoj.Solver/Solutions/5%20-%20King/PALIN.cs)|#ad-hoc|King|
|2016/06/30|[ACODE](https://www.spoj.com/problems/ACODE/)|[ACODE_v1.cs](Spoj.Solver/Solutions/5%20-%20King/ACODE_v1.cs)|#dynamic-programming #memoization #recursion|King|
|2016/07/03|[WILLITST](https://www.spoj.com/problems/WILLITST/)|[WILLITST.cs](Spoj.Solver/Solutions/4%20-%20Prince/WILLITST.cs)|#game #math|Prince|
|2016/07/03|[ABSYS](https://www.spoj.com/problems/ABSYS/)|[ABSYS.cs](Spoj.Solver/Solutions/3%20-%20Warlord/ABSYS.cs)|#ad-hoc #strings|Warlord|
|2016/07/04|[CANTON](https://www.spoj.com/problems/CANTON/)|[CANTON.cs](Spoj.Solver/Solutions/5%20-%20King/CANTON.cs)|#ad-hoc #math #sequence|King|
|2016/07/05|[STAMPS](https://www.spoj.com/problems/STAMPS/)|[STAMPS.cs](Spoj.Solver/Solutions/3%20-%20Warlord/STAMPS.cs)|#sorting|Warlord|
|2016/07/06|[PERMUT2](https://www.spoj.com/problems/PERMUT2/)|[PERMUT2.cs](Spoj.Solver/Solutions/4%20-%20Prince/PERMUT2.cs)|#ad-hoc #permutations|Prince|
|2016/07/07|[ARMY](https://www.spoj.com/problems/ARMY/)|[ARMY.cs](Spoj.Solver/Solutions/2%20-%20Chieftain/ARMY.cs)|#ad-hoc|Chieftain|
|2016/07/09|[TRICOUNT](https://www.spoj.com/problems/TRICOUNT/)|[TRICOUNT.cs](Spoj.Solver/Solutions/5%20-%20King/TRICOUNT.cs)|#math #proof|King|
|2016/07/10|[AP2](https://www.spoj.com/problems/AP2/)|[AP2.cs](Spoj.Solver/Solutions/4%20-%20Prince/AP2.cs)|#math #sequence|Prince|
|2016/07/10|[FENCE1](https://www.spoj.com/problems/FENCE1/)|[FENCE1.cs](Spoj.Solver/Solutions/3%20-%20Warlord/FENCE1.cs)|#math|Warlord|
|2016/07/10|[STPAR](https://www.spoj.com/problems/STPAR/)|[STPAR.cs](Spoj.Solver/Solutions/5%20-%20King/STPAR.cs)|#ad-hoc #greedy #stack|King|
|2016/07/12|[PT07Y](https://www.spoj.com/problems/PT07Y/)|[PT07Y.cs](Spoj.Solver/Solutions/5%20-%20King/PT07Y.cs)|#graph-theory #tree|King|
|2016/07/16|[GIRLSNBS](https://www.spoj.com/problems/GIRLSNBS/)|[GIRLSNBS.cs](Spoj.Solver/Solutions/3%20-%20Warlord/GIRLSNBS.cs)|#division|Warlord|
|2016/07/17|[NGM](https://www.spoj.com/problems/NGM/)|[NGM.cs](Spoj.Solver/Solutions/5%20-%20King/NGM.cs)|#game|King|
|2016/07/17|[INVCNT](https://www.spoj.com/problems/INVCNT/)|[INVCNT.cs](Spoj.Solver/Solutions/6%20-%20Emperor/INVCNT.cs)|#ad-hoc #bst|Emperor|
|2016/07/21|[CRDS](https://www.spoj.com/problems/CRDS/)|[CRDS.cs](Spoj.Solver/Solutions/3%20-%20Warlord/CRDS.cs)|#ad-hoc #sequence|Warlord|
|2016/07/21|[EDIST](https://www.spoj.com/problems/EDIST/)|[EDIST.cs](Spoj.Solver/Solutions/6%20-%20Emperor/EDIST.cs)|#dynamic-programming-2d #strings|Emperor|
|2016/07/26|[BEENUMS](https://www.spoj.com/problems/BEENUMS/)|[BEENUMS.cs](Spoj.Solver/Solutions/5%20-%20King/BEENUMS.cs)|#formula #math #proof|King|
|2016/08/02|[PT07Z](https://www.spoj.com/problems/PT07Z/)|[PT07Z.cs](Spoj.Solver/Solutions/7%20-%20Immortal/PT07Z.cs)|#bfs #graph-theory #longest-path #proof #tree|Immortal|
|2016/08/07|[AGGRCOW](https://www.spoj.com/problems/AGGRCOW/)|[AGGRCOW.cs](Spoj.Solver/Solutions/6%20-%20Emperor/AGGRCOW.cs)|#binary-search #greedy #optimization|Emperor|
|2016/08/08|[BISHOPS](https://www.spoj.com/problems/BISHOPS/)|[BISHOPS.cs](Spoj.Solver/Solutions/4%20-%20Prince/BISHOPS.cs)|#ad-hoc #math|Prince|
|2016/08/08|[BYTESM2](https://www.spoj.com/problems/BYTESM2/)|[BYTESM2.cs](Spoj.Solver/Solutions/5%20-%20King/BYTESM2.cs)|#dynamic-programming-2d #path-optimization|King|
|2016/08/09|[OLOLO](https://www.spoj.com/problems/OLOLO/)|[OLOLO.cs](Spoj.Solver/Solutions/6%20-%20Emperor/OLOLO.cs)|#binary #io|Emperor|
|2016/08/09|[OFFSIDE](https://www.spoj.com/problems/OFFSIDE/)|[OFFSIDE.cs](Spoj.Solver/Solutions/4%20-%20Prince/OFFSIDE.cs)|#ad-hoc|Prince|
|2016/08/10|[MAXLN](https://www.spoj.com/problems/MAXLN/)|[MAXLN.cs](Spoj.Solver/Solutions/5%20-%20King/MAXLN.cs)|#math #proof|King|
|2016/08/11|[HPYNOS](https://www.spoj.com/problems/HPYNOS/)|[HPYNOS.cs](Spoj.Solver/Solutions/4%20-%20Prince/HPYNOS.cs)|#digits #simulation|Prince|
|2016/08/11|[NY10A](https://www.spoj.com/problems/NY10A/)|[NY10A.cs](Spoj.Solver/Solutions/3%20-%20Warlord/NY10A.cs)|#buckets #sorting #strings|Warlord|
|2016/08/14|[LASTDIG2](https://www.spoj.com/problems/LASTDIG2/)|[LASTDIG2.cs](Spoj.Solver/Solutions/5%20-%20King/LASTDIG2.cs)|#big-numbers #digits #mod-math|King|
|2016/08/14|[HUBULLU](https://www.spoj.com/problems/HUBULLU/)|[HUBULLU.cs](Spoj.Solver/Solutions/6%20-%20Emperor/HUBULLU.cs)|#game #proof|Emperor|
|2016/08/15|[EASYPROB](https://www.spoj.com/problems/EASYPROB/)|[EASYPROB.cs](Spoj.Solver/Solutions/4%20-%20Prince/EASYPROB.cs)|#binary #recursion|Prince|
|2016/08/15|[ARITH2](https://www.spoj.com/problems/ARITH2/)|[ARITH2.cs](Spoj.Solver/Solutions/4%20-%20Prince/ARITH2.cs)|#ad-hoc #parsing #strings|Prince|
|2016/08/21|[GSS1](https://www.spoj.com/problems/GSS1/)|[GSS1.cs](Spoj.Solver/Solutions/7%20-%20Immortal/GSS1.cs)|#divide-and-conquer #segment-tree|Immortal|
|2016/09/05|[BITMAP](https://www.spoj.com/problems/BITMAP/)|[BITMAP.cs](Spoj.Solver/Solutions/5%20-%20King/BITMAP.cs)|#bfs|King|
|2016/09/05|[PARTY](https://www.spoj.com/problems/PARTY/)|[PARTY.cs](Spoj.Solver/Solutions/5%20-%20King/PARTY.cs)|#dynamic-programming-2d #knapsack #optimization|King|
|2016/09/07|[ETF](https://www.spoj.com/problems/ETF/)|[ETF.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ETF.cs)|#factors #formula #math #primes #sieve|Emperor|
|2016/09/08|[MARBLES](https://www.spoj.com/problems/MARBLES/)|[MARBLES.cs](Spoj.Solver/Solutions/5%20-%20King/MARBLES.cs)|#big-numbers #combinatorics #math|King|
|2016/09/09|[TRT](https://www.spoj.com/problems/TRT/)|[TRT_v1.cs](Spoj.Solver/Solutions/6%20-%20Emperor/TRT_v1.cs)|#memoization #optimization #recursion|Emperor|
|2016/09/10|[EGYPIZZA](https://www.spoj.com/problems/EGYPIZZA/)|[EGYPIZZA.cs](Spoj.Solver/Solutions/4%20-%20Prince/EGYPIZZA.cs)|#ad-hoc #division|Prince|
|2016/09/10|[AMR10G](https://www.spoj.com/problems/AMR10G/)|[AMR10G.cs](Spoj.Solver/Solutions/5%20-%20King/AMR10G.cs)|#sorting|King|
|2016/09/11|[AIBOHP](https://www.spoj.com/problems/AIBOHP/)|[AIBOHP.cs](Spoj.Solver/Solutions/6%20-%20Emperor/AIBOHP.cs)|#dynamic-programming-2d #optimization|Emperor|
|2016/09/12|[FARIDA](https://www.spoj.com/problems/FARIDA/)|[FARIDA.cs](Spoj.Solver/Solutions/4%20-%20Prince/FARIDA.cs)|#dynamic-programming-1d|Prince|
|2016/09/14|[ANARC09A](https://www.spoj.com/problems/ANARC09A/)|[ANARC09A.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ANARC09A.cs)|#greedy #recursion #stack|Emperor|
|2016/09/17|[PIGBANK](https://www.spoj.com/problems/PIGBANK/)|[PIGBANK_v1.cs](Spoj.Solver/Solutions/5%20-%20King/PIGBANK_v1.cs)|#dynamic-programming-1d #knapsack #optimization|King|
|2016/09/17|[NHAY](https://www.spoj.com/problems/NHAY/)|[NHAY.cs](Spoj.Solver/Solutions/6%20-%20Emperor/NHAY.cs)|#strings|Emperor|
|2016/09/18|[MIXTURES](https://www.spoj.com/problems/MIXTURES/)|[MIXTURES.cs](Spoj.Solver/Solutions/6%20-%20Emperor/MIXTURES.cs)|#dynamic-programming-2d #memoization #optimization|Emperor|
|2016/09/19|[SBANK](https://www.spoj.com/problems/SBANK/)|[SBANK.cs](Spoj.Solver/Solutions/6%20-%20Emperor/SBANK.cs)|#hash-table #radix-sort #sorting|Emperor|
|2016/09/19|[JAVAC](https://www.spoj.com/problems/JAVAC/)|[JAVAC.cs](Spoj.Solver/Solutions/3%20-%20Warlord/JAVAC.cs)|#ad-hoc #strings|Warlord|
|2016/09/21|[QUADAREA](https://www.spoj.com/problems/QUADAREA/)|[QUADAREA.cs](Spoj.Solver/Solutions/2%20-%20Chieftain/QUADAREA.cs)|#formula|Chieftain|
|2016/09/21|[HOTELS](https://www.spoj.com/problems/HOTELS/)|[HOTELS.cs](Spoj.Solver/Solutions/5%20-%20King/HOTELS.cs)|#greedy #optimization #sliding-window|King|
|2016/09/22|[DOTAA](https://www.spoj.com/problems/DOTAA/)|[DOTAA.cs](Spoj.Solver/Solutions/3%20-%20Warlord/DOTAA.cs)|#ad-hoc #division #game #io|Warlord|
|2016/09/25|[HORRIBLE](https://www.spoj.com/problems/HORRIBLE/)|[HORRIBLE_v1.cs](Spoj.Solver/Solutions/7%20-%20Immortal/HORRIBLE_v1.cs)|#divide-and-conquer #lazy #segment-tree|Immortal|
|2016/10/02|[BUGLIFE](https://www.spoj.com/problems/BUGLIFE/)|[BUGLIFE.cs](Spoj.Solver/Solutions/6%20-%20Emperor/BUGLIFE.cs)|#dfs #graph-theory|Emperor|
|2016/10/02|[FACEFRND](https://www.spoj.com/problems/FACEFRND/)|[FACEFRND.cs](Spoj.Solver/Solutions/4%20-%20Prince/FACEFRND.cs)|#ad-hoc #hash-table|Prince|
|2016/10/02|[GUESSING](https://www.spoj.com/problems/GUESSING/)|[GUESSING.txt](Spoj.Solver/Solutions/2%20-%20Chieftain/GUESSING.txt)|#ad-hoc|Chieftain|
|2016/10/02|[GCD2](https://www.spoj.com/problems/GCD2/)|[GCD2.cs](Spoj.Solver/Solutions/4%20-%20Prince/GCD2.cs)|#big-numbers #gcd #math|Prince|
|2016/10/03|[ARRAYSUB](https://www.spoj.com/problems/ARRAYSUB/)|[ARRAYSUB.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ARRAYSUB.cs)|#deque #sliding-window|Emperor|
|2016/10/04|[GSS3](https://www.spoj.com/problems/GSS3/)|[GSS3.cs](Spoj.Solver/Solutions/7%20-%20Immortal/GSS3.cs)|#divide-and-conquer #segment-tree|Immortal|
|2016/10/05|[TWOSQRS](https://www.spoj.com/problems/TWOSQRS/)|[TWOSQRS.cs](Spoj.Solver/Solutions/6%20-%20Emperor/TWOSQRS.cs)|#factors #math #mod-math #primes #sieve|Emperor|
|2016/10/06|[ANARC05B](https://www.spoj.com/problems/ANARC05B/)|[ANARC05B.cs](Spoj.Solver/Solutions/5%20-%20King/ANARC05B.cs)|#sequence #sorting|King|
|2016/10/07|[TDPRIMES](https://www.spoj.com/problems/TDPRIMES/)|[TDPRIMES.cs](Spoj.Solver/Solutions/5%20-%20King/TDPRIMES.cs)|#primes #sieve|King|
|2016/10/08|[ABCDEF](https://www.spoj.com/problems/ABCDEF/)|[ABCDEF.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ABCDEF.cs)|#ad-hoc #combinatorics #hash-table #math|Emperor|
|2016/10/09|[MAJOR](https://www.spoj.com/problems/MAJOR/)|[MAJOR_v1.cs](Spoj.Solver/Solutions/5%20-%20King/MAJOR_v1.cs)|#ad-hoc|King|
|2016/10/09|[ACPC11B](https://www.spoj.com/problems/ACPC11B/)|[ACPC11B.cs](Spoj.Solver/Solutions/5%20-%20King/ACPC11B.cs)|#binary-search #merge #sorting|King|
|2016/10/10|[PPATH](https://www.spoj.com/problems/PPATH/)|[PPATH.cs](Spoj.Solver/Solutions/6%20-%20Emperor/PPATH.cs)|#graph-theory #primes #sieve|Emperor|
|2016/10/11|[FIBOSUM](https://www.spoj.com/problems/FIBOSUM/)|[FIBOSUM.cs](Spoj.Solver/Solutions/6%20-%20Emperor/FIBOSUM.cs)|#formula #math #memoization #mod-math #sequence|Emperor|
|2016/10/12|[PIR](https://www.spoj.com/problems/PIR/)|[PIR.cs](Spoj.Solver/Solutions/3%20-%20Warlord/PIR.cs)|#formula #math|Warlord|
|2016/10/12|[ENIGMATH](https://www.spoj.com/problems/ENIGMATH/)|[ENIGMATH.cs](Spoj.Solver/Solutions/4%20-%20Prince/ENIGMATH.cs)|#gcd #math|Prince|
|2016/10/14|[SILVER](https://www.spoj.com/problems/SILVER/)|[SILVER.cs](Spoj.Solver/Solutions/6%20-%20Emperor/SILVER.cs)|#ad-hoc #binary #combinatorics #proof|Emperor|
|2016/10/14|[COMDIV](https://www.spoj.com/problems/COMDIV/)|[COMDIV.cs](Spoj.Solver/Solutions/6%20-%20Emperor/COMDIV.cs)|#division #factors #io #math #primes #sieve|Emperor|
|2016/10/15|[AMR12D](https://www.spoj.com/problems/AMR12D/)|[AMR12D.cs](Spoj.Solver/Solutions/3%20-%20Warlord/AMR12D.cs)|#ad-hoc #strings|Warlord|
|2016/10/15|[GLJIVE](https://www.spoj.com/problems/GLJIVE/)|[GLJIVE.cs](Spoj.Solver/Solutions/4%20-%20Prince/GLJIVE.cs)|#ad-hoc #binary #sequence|Prince|
|2016/10/15|[DANGER](https://www.spoj.com/problems/DANGER/)|[DANGER.cs](Spoj.Solver/Solutions/4%20-%20Prince/DANGER.cs)|#formula #game #math|Prince|
|2016/10/16|[HISTOGRA](https://www.spoj.com/problems/HISTOGRA/)|[HISTOGRA.cs](Spoj.Solver/Solutions/7%20-%20Immortal/HISTOGRA.cs)|#ad-hoc #optimization #sliding-window #stack|Immortal|
|2016/10/16|[MCOINS](https://www.spoj.com/problems/MCOINS/)|[MCOINS.cs](Spoj.Solver/Solutions/5%20-%20King/MCOINS.cs)|#dynamic-programming #game|King|
|2016/10/17|[ACPC10D](https://www.spoj.com/problems/ACPC10D/)|[ACPC10D.cs](Spoj.Solver/Solutions/5%20-%20King/ACPC10D.cs)|#dynamic-programming-2d #path-optimization|King|
|2016/10/17|[ALICESIE](https://www.spoj.com/problems/ALICESIE/)|[ALICESIE.cs](Spoj.Solver/Solutions/4%20-%20Prince/ALICESIE.cs)|#division #sieve|Prince|
|2016/10/18|[PHONELST](https://www.spoj.com/problems/PHONELST/)|[PHONELST.cs](Spoj.Solver/Solutions/6%20-%20Emperor/PHONELST.cs)|#strings #trie|Emperor|
|2016/10/19|[CPRMT](https://www.spoj.com/problems/CPRMT/)|[CPRMT.cs](Spoj.Solver/Solutions/4%20-%20Prince/CPRMT.cs)|#ad-hoc #buckets #strings|Prince|
|2016/10/19|[MISERMAN](https://www.spoj.com/problems/MISERMAN/)|[MISERMAN.cs](Spoj.Solver/Solutions/5%20-%20King/MISERMAN.cs)|#dynamic-programming-2d #path-optimization|King|
|2016/10/22|[DISUBSTR](https://www.spoj.com/problems/DISUBSTR/)|[DISUBSTR.cs](Spoj.Solver/Solutions/6%20-%20Emperor/DISUBSTR.cs)|#sorting #strings #suffixes|Emperor|
|2016/11/12|[BYECAKES](https://www.spoj.com/problems/BYECAKES/)|[BYECAKES.cs](Spoj.Solver/Solutions/3%20-%20Warlord/BYECAKES.cs)|#ad-hoc #division #optimization|Warlord|
|2016/11/12|[FAVDICE](https://www.spoj.com/problems/FAVDICE/)|[FAVDICE.cs](Spoj.Solver/Solutions/6%20-%20Emperor/FAVDICE.cs)|#math #probability #proof|Emperor|
|2016/11/12|[DIEHARD](https://www.spoj.com/problems/DIEHARD/)|[DIEHARD.cs](Spoj.Solver/Solutions/5%20-%20King/DIEHARD.cs)|#game #memoization|King|
|2016/11/14|[DQUERY](https://www.spoj.com/problems/DQUERY/)|[DQUERY.cs](Spoj.Solver/Solutions/8%20-%20Deity/DQUERY.cs)|#bit #offline #sorting|Deity|
|2016/11/16|[SUMITR](https://www.spoj.com/problems/SUMITR/)|[SUMITR.cs](Spoj.Solver/Solutions/5%20-%20King/SUMITR.cs)|#dynamic-programming-2d #golf #path-optimization|King|
|2017/02/26|[CSTREET](https://www.spoj.com/problems/CSTREET/)|[CSTREET.cs](Spoj.Solver/Solutions/7%20-%20Immortal/CSTREET.cs)|#graph-theory #greedy #heap #mst #prims|Immortal|
|2018/04/10|[BUSYMAN](https://www.spoj.com/problems/BUSYMAN/)|[BUSYMAN.cs](Spoj.Solver/Solutions/5%20-%20King/BUSYMAN.cs)|#ad-hoc #greedy #sorting|King|
|2018/07/08|[ABCPATH](https://www.spoj.com/problems/ABCPATH/)|[ABCPATH.cs](Spoj.Solver/Solutions/5%20-%20King/ABCPATH.cs)|#dag #dfs #greedy #memoization|King|
|2018/07/08|[KGSS](https://www.spoj.com/problems/KGSS/)|[KGSS.cs](Spoj.Solver/Solutions/7%20-%20Immortal/KGSS.cs)|#divide-and-conquer #segment-tree|Immortal|
|2018/07/09|[HACKRNDM](https://www.spoj.com/problems/HACKRNDM/)|[HACKRNDM_v1.cs](Spoj.Solver/Solutions/5%20-%20King/HACKRNDM_v1.cs)|#sliding-window #sorting|King|
|2018/07/23|[SHPATH](https://www.spoj.com/problems/SHPATH/)|[SHPATH.cs](Spoj.Solver/Solutions/7%20-%20Immortal/SHPATH.cs)|#dijkstras #graph-theory #greedy #heap #shortest-path|Immortal|
|2018/07/26|[AMR11E](https://www.spoj.com/problems/AMR11E/)|[AMR11E.cs](Spoj.Solver/Solutions/5%20-%20King/AMR11E.cs)|#factors #math #primes #sieve|King|
|2018/07/26|[SUMFOUR](https://www.spoj.com/problems/SUMFOUR/)|[SUMFOUR.cs](Spoj.Solver/Solutions/6%20-%20Emperor/SUMFOUR.cs)|#ad-hoc #binary-search #combinatorics #hash-table|Emperor|
|2018/07/27|[MUL](https://www.spoj.com/problems/MUL/)|[MUL.cs](Spoj.Solver/Solutions/2%20-%20Chieftain/MUL.cs)|#big-numbers|Chieftain|
|2018/07/28|[BRCKTS](https://www.spoj.com/problems/BRCKTS/)|[BRCKTS.cs](Spoj.Solver/Solutions/7%20-%20Immortal/BRCKTS.cs)|#divide-and-conquer #segment-tree|Immortal|
|2018/07/29|[LABYR1](https://www.spoj.com/problems/LABYR1/)|[LABYR1.cs](Spoj.Solver/Solutions/7%20-%20Immortal/LABYR1.cs)|#bfs #graph-theory #longest-path #proof #tree|Immortal|
|2018/07/30|[MICEMAZE](https://www.spoj.com/problems/MICEMAZE/)|[MICEMAZE.cs](Spoj.Solver/Solutions/7%20-%20Immortal/MICEMAZE.cs)|#dijkstras #graph-theory #greedy #heap #shortest-path|Immortal|
|2018/08/03|[LCA](https://www.spoj.com/problems/LCA/)|[LCA.cs](Spoj.Solver/Solutions/7%20-%20Immortal/LCA.cs)|#divide-and-conquer #graph-theory #lca #segment-tree #stack #tree|Immortal|
|2018/08/09|[INTEST](https://www.spoj.com/problems/INTEST/)|[INTEST.cs](Spoj.Solver/Solutions/6%20-%20Emperor/INTEST.cs)|#io|Emperor|
|2018/08/11|[INOUTEST](https://www.spoj.com/problems/INOUTEST/)|[INOUTEST.cs](Spoj.Solver/Solutions/6%20-%20Emperor/INOUTEST.cs)|#io|Emperor|
|2019/01/04|[QTREE](https://www.spoj.com/problems/QTREE/)|[QTREE_v1.cs](Spoj.Solver/Solutions/8%20-%20Deity/QTREE_v1.cs)|#divide-and-conquer #graph-theory #hld #lca #segment-tree #tree|Deity|
|2019/01/06|[CADYDIST](https://www.spoj.com/problems/CADYDIST/)|[CADYDIST.cs](Spoj.Solver/Solutions/3%20-%20Warlord/CADYDIST.cs)|#ad-hoc #sorting|Warlord|
|2019/01/06|[EC_CONB](https://www.spoj.com/problems/EC_CONB/)|[EC_CONB.cs](Spoj.Solver/Solutions/3%20-%20Warlord/EC_CONB.cs)|#ad-hoc #binary|Warlord|
|2019/01/07|[RPLC](https://www.spoj.com/problems/RPLC/)|[RPLC.cs](Spoj.Solver/Solutions/3%20-%20Warlord/RPLC.cs)|#ad-hoc|Warlord|
|2019/01/07|[CAM5](https://www.spoj.com/problems/CAM5/)|[CAM5.cs](Spoj.Solver/Solutions/6%20-%20Emperor/CAM5.cs)|#dfs #disjoint-sets|Emperor|
|2019/01/09|[BOMARBLE](https://www.spoj.com/problems/BOMARBLE/)|[BOMARBLE.cs](Spoj.Solver/Solutions/3%20-%20Warlord/BOMARBLE.cs)|#math|Warlord|
|2019/01/10|[ABSP1](https://www.spoj.com/problems/ABSP1/)|[ABSP1.cs](Spoj.Solver/Solutions/4%20-%20Prince/ABSP1.cs)|#ad-hoc #proof|Prince|
|2019/01/11|[ROOTCIPH](https://www.spoj.com/problems/ROOTCIPH/)|[ROOTCIPH.cs](Spoj.Solver/Solutions/7%20-%20Immortal/ROOTCIPH.cs)|#factors #formula #io #math|Immortal|
|2019/01/13|[MRECAMAN](https://www.spoj.com/problems/MRECAMAN/)|[MRECAMAN.cs](Spoj.Solver/Solutions/3%20-%20Warlord/MRECAMAN.cs)|#ad-hoc #memoization|Warlord|
|2019/01/13|[UPDATEIT](https://www.spoj.com/problems/UPDATEIT/)|[UPDATEIT.cs](Spoj.Solver/Solutions/7%20-%20Immortal/UPDATEIT.cs)|#bit|Immortal|
|2019/01/13|[BYTESE2](https://www.spoj.com/problems/BYTESE2/)|[BYTESE2.cs](Spoj.Solver/Solutions/5%20-%20King/BYTESE2.cs)|#ad-hoc #sorting|King|
|2019/01/13|[TRGRID](https://www.spoj.com/problems/TRGRID/)|[TRGRID.cs](Spoj.Solver/Solutions/5%20-%20King/TRGRID.cs)|#ad-hoc #recursion|King|
|2019/01/14|[JNEXT](https://www.spoj.com/problems/JNEXT/)|[JNEXT.cs](Spoj.Solver/Solutions/5%20-%20King/JNEXT.cs)|#ad-hoc #greedy #sorting|King|
|2019/01/14|[NY10E](https://www.spoj.com/problems/NY10E/)|[NY10E.cs](Spoj.Solver/Solutions/5%20-%20King/NY10E.cs)|#dynamic-programming-2d|King|
|2019/01/15|[ZSUM](https://www.spoj.com/problems/ZSUM/)|[ZSUM.cs](Spoj.Solver/Solutions/5%20-%20King/ZSUM.cs)|#ad-hoc #math #mod-math #sequence|King|
|2019/01/16|[POUR1](https://www.spoj.com/problems/POUR1/)|[POUR1.cs](Spoj.Solver/Solutions/6%20-%20Emperor/POUR1.cs)|#ad-hoc #mod-math #simulation|Emperor|
|2019/01/17|[NOTATRI](https://www.spoj.com/problems/NOTATRI/)|[NOTATRI.cs](Spoj.Solver/Solutions/6%20-%20Emperor/NOTATRI.cs)|#binary-search #sliding-window #sorting|Emperor|
|2019/01/18|[LENGFACT](https://www.spoj.com/problems/LENGFACT/)|[LENGFACT.cs](Spoj.Solver/Solutions/4%20-%20Prince/LENGFACT.cs)|#formula #math|Prince|
|2019/01/18|[CRSCNTRY](https://www.spoj.com/problems/CRSCNTRY/)|[CRSCNTRY.cs](Spoj.Solver/Solutions/5%20-%20King/CRSCNTRY.cs)|#dynamic-programming-2d|King|
|2019/01/19|[NAKANJ](https://www.spoj.com/problems/NAKANJ/)|[NAKANJ.cs](Spoj.Solver/Solutions/5%20-%20King/NAKANJ.cs)|#bfs|King|
|2019/01/19|[PERMUT1](https://www.spoj.com/problems/PERMUT1/)|[PERMUT1.cs](Spoj.Solver/Solutions/6%20-%20Emperor/PERMUT1.cs)|#dynamic-programming-2d #permutations|Emperor|
|2019/01/24|[ABA12C](https://www.spoj.com/problems/ABA12C/)|[ABA12C.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ABA12C.cs)|#dynamic-programming-2d #knapsack #optimization|Emperor|
|2019/01/25|[TWENDS](https://www.spoj.com/problems/TWENDS/)|[TWENDS.cs](Spoj.Solver/Solutions/6%20-%20Emperor/TWENDS.cs)|#dynamic-programming-2d #game #optimization|Emperor|
|2019/01/26|[GAMES](https://www.spoj.com/problems/GAMES/)|[GAMES.cs](Spoj.Solver/Solutions/5%20-%20King/GAMES.cs)|#ad-hoc #gcd #proof|King|
|2019/01/26|[CUBEFR](https://www.spoj.com/problems/CUBEFR/)|[CUBEFR.cs](Spoj.Solver/Solutions/4%20-%20Prince/CUBEFR.cs)|#sieve|Prince|
|2019/01/26|[ALIEN](https://www.spoj.com/problems/ALIEN/)|[ALIEN.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ALIEN.cs)|#sliding-window|Emperor|
|2019/01/27|[EKO](https://www.spoj.com/problems/EKO/)|[EKO.cs](Spoj.Solver/Solutions/6%20-%20Emperor/EKO.cs)|#binary-search #sorting|Emperor|
|2019/01/27|[ONEZERO](https://www.spoj.com/problems/ONEZERO/)|[ONEZERO.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ONEZERO.cs)|#ad-hoc #bfs #bitmask #mod-math|Emperor|
|2019/01/27|[ASSIGN](https://www.spoj.com/problems/ASSIGN/)|[ASSIGN.cs](Spoj.Solver/Solutions/7%20-%20Immortal/ASSIGN.cs)|#bitmask #dynamic-programming|Immortal|
|2019/01/28|[CHOCOLA](https://www.spoj.com/problems/CHOCOLA/)|[CHOCOLA.cs](Spoj.Solver/Solutions/6%20-%20Emperor/CHOCOLA.cs)|#ad-hoc #greedy #sorting|Emperor|
|2019/01/29|[DSUBSEQ](https://www.spoj.com/problems/DSUBSEQ/)|[DSUBSEQ.cs](Spoj.Solver/Solutions/6%20-%20Emperor/DSUBSEQ.cs)|#dynamic-programming #memoization #mod-math|Emperor|
|2019/01/29|[TSHOW1](https://www.spoj.com/problems/TSHOW1/)|[TSHOW1.cs](Spoj.Solver/Solutions/5%20-%20King/TSHOW1.cs)|#binary #math|King|
|2019/01/30|[ATOMS](https://www.spoj.com/problems/ATOMS/)|[ATOMS.cs](Spoj.Solver/Solutions/3%20-%20Warlord/ATOMS.cs)|#math|Warlord|
|2019/01/30|[HIGHWAYS](https://www.spoj.com/problems/HIGHWAYS/)|[HIGHWAYS.cs](Spoj.Solver/Solutions/7%20-%20Immortal/HIGHWAYS.cs)|#dijkstras #graph-theory #greedy #heap #shortest-path|Immortal|
|2019/01/30|[PT07X](https://www.spoj.com/problems/PT07X/)|[PT07X.cs](Spoj.Solver/Solutions/6%20-%20Emperor/PT07X.cs)|#graph-theory #greedy|Emperor|
|2019/01/31|[MFLAR10](https://www.spoj.com/problems/MFLAR10/)|[MFLAR10.cs](Spoj.Solver/Solutions/3%20-%20Warlord/MFLAR10.cs)|#ad-hoc|Warlord|
|2019/01/31|[CEQU](https://www.spoj.com/problems/CEQU/)|[CEQU.cs](Spoj.Solver/Solutions/5%20-%20King/CEQU.cs)|#formula #gcd #math|King|
|2019/01/31|[EBOXES](https://www.spoj.com/problems/EBOXES/)|[EBOXES.cs](Spoj.Solver/Solutions/4%20-%20Prince/EBOXES.cs)|#math|Prince|
|2019/02/01|[IITKWPCB](https://www.spoj.com/problems/IITKWPCB/)|[IITKWPCB.cs](Spoj.Solver/Solutions/5%20-%20King/IITKWPCB.cs)|#factors #math #proof|King|
|2019/02/02|[BWIDOW](https://www.spoj.com/problems/BWIDOW/)|[BWIDOW.cs](Spoj.Solver/Solutions/3%20-%20Warlord/BWIDOW.cs)|#ad-hoc|Warlord|
|2019/02/02|[SCPC11B](https://www.spoj.com/problems/SCPC11B/)|[SCPC11B.cs](Spoj.Solver/Solutions/3%20-%20Warlord/SCPC11B.cs)|#ad-hoc #sorting|Warlord|
|2019/02/02|[ANARC09B](https://www.spoj.com/problems/ANARC09B/)|[ANARC09B.cs](Spoj.Solver/Solutions/4%20-%20Prince/ANARC09B.cs)|#gcd|Prince|
|2019/02/02|[SPEED](https://www.spoj.com/problems/SPEED/)|[SPEED.cs](Spoj.Solver/Solutions/6%20-%20Emperor/SPEED.cs)|#ad-hoc #gcd #math|Emperor|
|2019/02/03|[UJ](https://www.spoj.com/problems/UJ/)|[UJ.cs](Spoj.Solver/Solutions/3%20-%20Warlord/UJ.cs)|#big-numbers|Warlord|
|2019/02/03|[WACHOVIA](https://www.spoj.com/problems/WACHOVIA/)|[WACHOVIA.cs](Spoj.Solver/Solutions/5%20-%20King/WACHOVIA.cs)|#dynamic-programming-2d #knapsack #optimization|King|
|2019/02/03|[MAYA](https://www.spoj.com/problems/MAYA/)|[MAYA.cs](Spoj.Solver/Solutions/3%20-%20Warlord/MAYA.cs)|#ad-hoc|Warlord|
|2019/02/03|[STREETR](https://www.spoj.com/problems/STREETR/)|[STREETR.cs](Spoj.Solver/Solutions/4%20-%20Prince/STREETR.cs)|#ad-hoc #gcd|Prince|
|2019/02/03|[WORDCNT](https://www.spoj.com/problems/WORDCNT/)|[WORDCNT.cs](Spoj.Solver/Solutions/3%20-%20Warlord/WORDCNT.cs)|#ad-hoc|Warlord|
|2019/02/03|[POLEVAL](https://www.spoj.com/problems/POLEVAL/)|[POLEVAL.cs](Spoj.Solver/Solutions/3%20-%20Warlord/POLEVAL.cs)|#ad-hoc|Warlord|
|2019/02/04|[RPLN](https://www.spoj.com/problems/RPLN/)|[RPLN.cs](Spoj.Solver/Solutions/7%20-%20Immortal/RPLN.cs)|#divide-and-conquer #segment-tree|Immortal|
|2019/02/04|[BLINNET](https://www.spoj.com/problems/BLINNET/)|[BLINNET.cs](Spoj.Solver/Solutions/6%20-%20Emperor/BLINNET.cs)|#disjoint-sets #mst|Emperor|
|2019/02/05|[SUBST1](https://www.spoj.com/problems/SUBST1/)|[SUBST1.cs](Spoj.Solver/Solutions/6%20-%20Emperor/SUBST1.cs)|#sorting #strings #suffixes|Emperor|
|2019/02/05|[QTREE2](https://www.spoj.com/problems/QTREE2/)|[QTREE2.cs](Spoj.Solver/Solutions/7%20-%20Immortal/QTREE2.cs)|#divide-and-conquer #graph-theory #lca #segment-tree #tree|Immortal|
|2019/02/06|[APS](https://www.spoj.com/problems/APS/)|[APS.cs](Spoj.Solver/Solutions/6%20-%20Emperor/APS.cs)|#factors #primes #sieve|Emperor|
|2019/02/06|[SHOP](https://www.spoj.com/problems/SHOP/)|[SHOP.cs](Spoj.Solver/Solutions/7%20-%20Immortal/SHOP.cs)|#dijkstras #graph-theory #greedy #heap #shortest-path|Immortal|
|2019/02/07|[ANARC08H](https://www.spoj.com/problems/ANARC08H/)|[ANARC08H.cs](Spoj.Solver/Solutions/5%20-%20King/ANARC08H.cs)|#formula #game|King|
|2019/02/07|[CODESPTB](https://www.spoj.com/problems/CODESPTB/)|[CODESPTB.cs](Spoj.Solver/Solutions/7%20-%20Immortal/CODESPTB.cs)|#bit #sorting|Immortal|
|2019/02/08|[NEG2](https://www.spoj.com/problems/NEG2/)|[NEG2.cs](Spoj.Solver/Solutions/6%20-%20Emperor/NEG2.cs)|#ad-hoc #binary|Emperor|
|2019/02/09|[CATM](https://www.spoj.com/problems/CATM/)|[CATM.cs](Spoj.Solver/Solutions/5%20-%20King/CATM.cs)|#bfs #simulation|King|
|2019/02/09|[PON](https://www.spoj.com/problems/PON/)|[PON.cs](Spoj.Solver/Solutions/6%20-%20Emperor/PON.cs)|#math #primes|Emperor|
|2019/02/10|[SQRBR](https://www.spoj.com/problems/SQRBR/)|[SQRBR.cs](Spoj.Solver/Solutions/6%20-%20Emperor/SQRBR.cs)|#dynamic-programming-2d|Emperor|
|2019/02/10|[ROCK](https://www.spoj.com/problems/ROCK/)|[ROCK.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ROCK.cs)|#dynamic-programming-2d|Emperor|
|2019/02/10|[IOIPALIN](https://www.spoj.com/problems/IOIPALIN/)|[IOIPALIN.cs](Spoj.Solver/Solutions/6%20-%20Emperor/IOIPALIN.cs)|#dynamic-programming-2d #optimization|Emperor|
|2019/02/10|[ELEVTRBL](https://www.spoj.com/problems/ELEVTRBL/)|[ELEVTRBL.cs](Spoj.Solver/Solutions/4%20-%20Prince/ELEVTRBL.cs)|#bfs|Prince|
|2019/02/10|[ANARC05H](https://www.spoj.com/problems/ANARC05H/)|[ANARC05H.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ANARC05H.cs)|#dynamic-programming-2d|Emperor|
|2019/02/11|[LITE](https://www.spoj.com/problems/LITE/)|[LITE.cs](Spoj.Solver/Solutions/7%20-%20Immortal/LITE.cs)|#divide-and-conquer #lazy #segment-tree|Immortal|
|2019/02/11|[SCUBADIV](https://www.spoj.com/problems/SCUBADIV/)|[SCUBADIV.cs](Spoj.Solver/Solutions/6%20-%20Emperor/SCUBADIV.cs)|#memoization #recursion|Emperor|
|2019/02/12|[FREQUENT](https://www.spoj.com/problems/FREQUENT/)|[FREQUENT.cs](Spoj.Solver/Solutions/7%20-%20Immortal/FREQUENT.cs)|#divide-and-conquer #segment-tree|Immortal|
|2019/02/12|[CPCRC1C](https://www.spoj.com/problems/CPCRC1C/)|[CPCRC1C.cs](Spoj.Solver/Solutions/6%20-%20Emperor/CPCRC1C.cs)|#ad-hoc #digits|Emperor|
|2019/02/13|[YODANESS](https://www.spoj.com/problems/YODANESS/)|[YODANESS.cs](Spoj.Solver/Solutions/6%20-%20Emperor/YODANESS.cs)|#ad-hoc #bst|Emperor|
|2019/02/13|[MATSUM](https://www.spoj.com/problems/MATSUM/)|[MATSUM.cs](Spoj.Solver/Solutions/7%20-%20Immortal/MATSUM.cs)|#bit|Immortal|
|2019/02/13|[PIE](https://www.spoj.com/problems/PIE/)|[PIE.cs](Spoj.Solver/Solutions/6%20-%20Emperor/PIE.cs)|#binary-search #optimization|Emperor|
|2019/02/13|[ALLIZWEL](https://www.spoj.com/problems/ALLIZWEL/)|[ALLIZWEL.cs](Spoj.Solver/Solutions/6%20-%20Emperor/ALLIZWEL.cs)|#dfs #recursion|Emperor|
|2019/02/14|[MULTQ3](https://www.spoj.com/problems/MULTQ3/)|[MULTQ3.cs](Spoj.Solver/Solutions/8%20-%20Deity/MULTQ3.cs)|#divide-and-conquer #lazy #segment-tree|Deity|
|2019/02/14|[M3TILE](https://www.spoj.com/problems/M3TILE/)|[M3TILE.cs](Spoj.Solver/Solutions/6%20-%20Emperor/M3TILE.cs)|#ad-hoc #dynamic-programming #recursion|Emperor|
|2019/02/14|[GNYR09F](https://www.spoj.com/problems/GNYR09F/)|[GNYR09F.cs](Spoj.Solver/Solutions/5%20-%20King/GNYR09F.cs)|#binary #dynamic-programming-3d|King|
|2019/02/15|[BEADS](https://www.spoj.com/problems/BEADS/)|[BEADS.cs](Spoj.Solver/Solutions/5%20-%20King/BEADS.cs)|#ad-hoc #sorting|King|
|2019/02/15|[CHICAGO](https://www.spoj.com/problems/CHICAGO/)|[CHICAGO.cs](Spoj.Solver/Solutions/7%20-%20Immortal/CHICAGO.cs)|#dijkstras #graph-theory #greedy #heap #shortest-path|Immortal|
|2019/02/15|[RENT](https://www.spoj.com/problems/RENT/)|[RENT.cs](Spoj.Solver/Solutions/6%20-%20Emperor/RENT.cs)|#binary-search #dynamic-programming-1d #sorting|Emperor|
|2019/02/16|[WORDS1](https://www.spoj.com/problems/WORDS1/)|[WORDS1.cs](Spoj.Solver/Solutions/7%20-%20Immortal/WORDS1.cs)|#euler-path #graph-theory|Immortal|
|2019/02/16|[BABTWR](https://www.spoj.com/problems/BABTWR/)|[BABTWR.cs](Spoj.Solver/Solutions/6%20-%20Emperor/BABTWR.cs)|#ad-hoc #dynamic-programming-1d|Emperor|
|2019/02/17|[KQUERY](https://www.spoj.com/problems/KQUERY/)|[KQUERY.cs](Spoj.Solver/Solutions/7%20-%20Immortal/KQUERY.cs)|#bit #offline #sorting|Immortal|
|2019/02/18|[MKTHNUM](https://www.spoj.com/problems/MKTHNUM/)|[MKTHNUM.cs](Spoj.Solver/Solutions/8%20-%20Deity/MKTHNUM.cs)|#binary-search #divide-and-conquer #merge #segment-tree #sorting|Deity|
