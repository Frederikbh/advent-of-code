original source: [https://adventofcode.com/2016/day/16](https://adventofcode.com/2016/day/16)
## --- Day 16: Dragon Checksum ---
You're done scanning this part of the network, but you've left traces of your presence. You need to overwrite some disks with random-looking data to cover your tracks and update the local security system with a new checksum for those disks.

For the data to not be suspicious, it needs to have certain properties; purely random data will be detected as tampering. To generate appropriate random data, you'll need to use a modified [dragon curve](https://en.wikipedia.org/wiki/Dragon_curve).

Start with an appropriate initial state (your puzzle input). Then, so long as you don't have enough data yet to fill the disk, repeat the following steps:


 - Call the data you have at this point "a".
 - Make a copy of "a"; call this copy "b".
 - Reverse the order of the characters in "b".
 - In "b", replace all instances of <code>0</code> with <code>1</code> and all <code>1</code>s with <code>0</code>.
 - The resulting data is "a", then a single <code>0</code>, then "b".

For example, after a single step of this process,


 - <code>1</code> becomes <code>100</code>.
 - <code>0</code> becomes <code>001</code>.
 - <code>11111</code> becomes <code>11111000000</code>.
 - <code>111100001010</code> becomes <code>1111000010100101011110000</code>.

Repeat these steps until you have enough data to fill the desired disk.

Once the data has been generated, you also need to create a checksum of that data. Calculate the checksum <em>only</em> for the data that fits on the disk, even if you generated more data than that in the previous step.

The checksum for some given data is created by considering each non-overlapping <em>pair</em> of characters in the input data.  If the two characters match (<code>00</code> or <code>11</code>), the next checksum character is a <code>1</code>.  If the characters do not match (<code>01</code> or <code>10</code>), the next checksum character is a <code>0</code>. This should produce a new string which is exactly half as long as the original. If the length of the checksum is <em>even</em>, repeat the process until you end up with a checksum with an <em>odd</em> length.

For example, suppose we want to fill a disk of length <code>12</code>, and when we finally generate a string of at least length <code>12</code>, the first <code>12</code> characters are <code>110010110100</code>. To generate its checksum:


 - Consider each pair: <code>11</code>, <code>00</code>, <code>10</code>, <code>11</code>, <code>01</code>, <code>00</code>.
 - These are same, same, different, same, different, same, producing <code>110101</code>.
 - The resulting string has length <code>6</code>, which is <em>even</em>, so we repeat the process.
 - The pairs are <code>11</code> (same), <code>01</code> (different), <code>01</code> (different).
 - This produces the checksum <code>100</code>, which has an <em>odd</em> length, so we stop.

Therefore, the checksum for <code>110010110100</code> is <code>100</code>.

Combining all of these steps together, suppose you want to fill a disk of length <code>20</code> using an initial state of <code>10000</code>:


 - Because <code>10000</code> is too short, we first use the modified dragon curve to make it longer.
 - After one round, it becomes <code>10000011110</code> (<code>11</code> characters), still too short.
 - After two rounds, it becomes <code>10000011110010000111110</code> (<code>23</code> characters), which is enough.
 - Since we only need <code>20</code>, but we have <code>23</code>, we get rid of all but the first <code>20</code> characters: <code>10000011110010000111</code>.
 - Next, we start calculating the checksum; after one round, we have <code>0111110101</code>, which <code>10</code> characters long (<em>even</em>), so we continue.
 - After two rounds, we have <code>01100</code>, which is <code>5</code> characters long (<em>odd</em>), so we are done.

In this example, the correct checksum would therefore be <code>01100</code>.

The first disk you have to fill has length <code>272</code>. Using the initial state in your puzzle input, <em>what is the correct checksum</em>?


## --- Part Two ---
The second disk you have to fill has length <code>35651584</code>. Again using the initial state in your puzzle input, <em>what is the correct checksum</em> for this disk?


