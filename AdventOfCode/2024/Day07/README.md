original source: [https://adventofcode.com/2024/day/7](https://adventofcode.com/2024/day/7)
## --- Day 7: Bridge Repair ---
The Historians take you to a familiar [rope bridge](/2022/day/9) over a river in the middle of a jungle. The Chief isn't on this side of the bridge, though; maybe he's on the other side?

When you go to cross the bridge, you notice a group of engineers trying to repair it. (Apparently, it breaks pretty frequently.) You won't be able to cross until it's fixed.

You ask how long it'll take; the engineers tell you that it only needs final calibrations, but some young elephants were playing nearby and <em>stole all the operators</em> from their calibration equations! They could finish the calibrations if only someone could determine which test values could possibly be produced by placing any combination of operators into their calibration equations (your puzzle input).

For example:

<pre>
<code>190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
</code>
</pre>

Each line represents a single equation. The test value appears before the colon on each line; it is your job to determine whether the remaining numbers can be combined with operators to produce the test value.

Operators are <em>always evaluated left-to-right</em>, <em>not</em> according to precedence rules. Furthermore, numbers in the equations cannot be rearranged. Glancing into the jungle, you can see elephants holding two different types of operators: <em>add</em> (<code>+</code>) and <em>multiply</em> (<code>*</code>).

Only three of the above equations can be made true by inserting operators:


 - <code>190: 10 19</code> has only one position that accepts an operator: between <code>10</code> and <code>19</code>. Choosing <code>+</code> would give <code>29</code>, but choosing <code>*</code> would give the test value (<code>10 * 19 = 190</code>).
 - <code>3267: 81 40 27</code> has two positions for operators. Of the four possible configurations of the operators, <em>two</em> cause the right side to match the test value: <code>81 + 40 * 27</code> and <code>81 * 40 + 27</code> both equal <code>3267</code> (when evaluated left-to-right)!
 - <code>292: 11 6 16 20</code> can be solved in exactly one way: <code>11 + 6 * 16 + 20</code>.

The engineers just need the <em>total calibration result</em>, which is the sum of the test values from just the equations that could possibly be true. In the above example, the sum of the test values for the three equations listed above is <code><em>3749</em></code>.

Determine which equations could possibly be true. <em>What is their total calibration result?</em>


## --- Part Two ---
The engineers seem concerned; the total calibration result you gave them is nowhere close to being within safety tolerances. Just then, you spot your mistake: some well-hidden elephants are holding a <em>third type of operator</em>.

The [concatenation](https://en.wikipedia.org/wiki/Concatenation) operator (<code>||</code>) combines the digits from its left and right inputs into a single number. For example, <code>12 || 345</code> would become <code>12345</code>. All operators are still evaluated left-to-right.

Now, apart from the three equations that could be made true using only addition and multiplication, the above example has three more equations that can be made true by inserting operators:


 - <code>156: 15 6</code> can be made true through a single concatenation: <code>15 || 6 = 156</code>.
 - <code>7290: 6 8 6 15</code> can be made true using <code>6 * 8 || 6 * 15</code>.
 - <code>192: 17 8 14</code> can be made true using <code>17 || 8 + 14</code>.

Adding up all six test values (the three that could be made before using only <code>+</code> and <code>*</code> plus the new three that can now be made by also using <code>||</code>) produces the new <em>total calibration result</em> of <code><em>11387</em></code>.

Using your new knowledge of elephant hiding spots, determine which equations could possibly be true. <em>What is their total calibration result?</em>


