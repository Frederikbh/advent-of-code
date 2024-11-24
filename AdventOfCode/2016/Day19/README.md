original source: [https://adventofcode.com/2016/day/19](https://adventofcode.com/2016/day/19)
## --- Day 19: An Elephant Named Joseph ---
The Elves contact you over a highly secure emergency channel. Back at the North Pole, the Elves are busy misunderstanding [White Elephant parties](https://en.wikipedia.org/wiki/White_elephant_gift_exchange).

Each Elf brings a present. They all sit in a circle, numbered starting with position <code>1</code>. Then, starting with the first Elf, they take turns stealing all the presents from the Elf to their left.  An Elf with no presents is removed from the circle and does not take turns.

For example, with five Elves (numbered <code>1</code> to <code>5</code>):

<pre>
<code>  1
5   2
 4 3
</code>
</pre>


 - Elf <code>1</code> takes Elf <code>2</code>'s present.
 - Elf <code>2</code> has no presents and is skipped.
 - Elf <code>3</code> takes Elf <code>4</code>'s present.
 - Elf <code>4</code> has no presents and is also skipped.
 - Elf <code>5</code> takes Elf <code>1</code>'s two presents.
 - Neither Elf <code>1</code> nor Elf <code>2</code> have any presents, so both are skipped.
 - Elf <code>3</code> takes Elf <code>5</code>'s three presents.

So, with <em>five</em> Elves, the Elf that sits starting in position <code>3</code> gets all the presents.

With the number of Elves given in your puzzle input, <em>which Elf gets all the presents?</em>


## --- Part Two ---
Realizing the folly of their present-exchange rules, the Elves agree to instead steal presents from the Elf <em>directly across the circle</em>. If two Elves are across the circle, the one on the left (from the perspective of the stealer) is stolen from.  The other rules remain unchanged: Elves with no presents are removed from the circle entirely, and the other elves move in slightly to keep the circle evenly spaced.

For example, with five Elves (again numbered <code>1</code> to <code>5</code>):


 - The Elves sit in a circle; Elf <code>1</code> goes first:
<pre>
<code>  <em>1</em>
5   2
 4 3
</code>
</pre>

 - Elves <code>3</code> and <code>4</code> are across the circle; Elf <code>3</code>'s present is stolen, being the one to the left. Elf <code>3</code> leaves the circle, and the rest of the Elves move in:
<pre>
<code>  <em>1</em>           1
5   2  -->  5   2
 4 -          4
</code>
</pre>

 - Elf <code>2</code> steals from the Elf directly across the circle, Elf <code>5</code>:
<pre>
<code>  1         1 
-   <em>2</em>  -->     2
  4         4 
</code>
</pre>

 - Next is Elf <code>4</code> who, choosing between Elves <code>1</code> and <code>2</code>, steals from Elf <code>1</code>:
<pre>
<code> -          2  
    2  -->
 <em>4</em>          4
</code>
</pre>

 - Finally, Elf <code>2</code> steals from Elf <code>4</code>:
<pre>
<code> <em>2</em>
    -->  2  
 -
</code>
</pre>


So, with <em>five</em> Elves, the Elf that sits starting in position <code>2</code> gets all the presents.

With the number of Elves given in your puzzle input, <em>which Elf now gets all the presents?</em>


