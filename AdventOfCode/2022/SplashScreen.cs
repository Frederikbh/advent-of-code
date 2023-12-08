
using System;

namespace AdventOfCode.Y2022;

class SplashScreenImpl : SplashScreen {

    public void Show() {

        var color = Console.ForegroundColor;
        Write(0xcc00, false, "           ▄█▄ ▄▄█ ▄ ▄ ▄▄▄ ▄▄ ▄█▄  ▄▄▄ ▄█  ▄▄ ▄▄▄ ▄▄█ ▄▄▄\n           █▄█ █ █ █ █ █▄█ █ █ █   █ █ █▄ ");
            Write(0xcc00, false, " █  █ █ █ █ █▄█\n           █ █ █▄█ ▀▄▀ █▄▄ █ █ █▄  █▄█ █   █▄ █▄█ █▄█ █▄▄  $year = 2022\n            ");
            Write(0xcc00, false, "\n           ");
            Write(0x888888, false, "  - /\\ -  -        -       -     -      -    -          \n            - /  \\/\\  -    -     -  -    - ");
            Write(0x888888, false, "  -  /\\   -     -       \n           @@@#@@@@@#@####@@@@##@###@#@@@@@#@@@@#@#@@@@@#@@@  25 **\n       ");
            Write(0x888888, false, "    #@@|@#@#@@@@@@@@#@#####@#@@@@@#@#@@#@#@@@@@@#@@@@  24 **\n           ##@@##@@#@@@#@@#@#@@#@@@#@@@");
            Write(0x888888, false, "@@@@#@@@@@@#@@@@@#@#@  23 **\n           @@@@@#@#@@#@#@#@#@#@#@@@@@@##@#@@###@#@@@#@@@##@@  22 **\n   ");
            Write(0x888888, false, "        @@@@@@@@@@@#@@@@#@@##@@@@@#@#@@##@@@@@@#@@@@@@@@#  21 **\n           #@@@@@@##@@@@#@@@@@@@@#@");
            Write(0x888888, false, "@#@@#@@#@@#@#@@@@@@@@@@@#  20 **\n           @@#@@@@@@@@#@@@@@@@#@#@@@@#@@#@@@##@#@#@#@@@@@@@@  19 **");
            Write(0x888888, false, "\n           @#@@@@@@@@@#@@@@###@#@@@@#@@##@@#@@@#@@@@@#@@@@@@  18 **\n           @@@#@@#@##@@###@#@@@");
            Write(0x888888, false, "#@@@@@@@#@@@@#@@@@@@@@@@@@@@@  17 **\n           @#@@@@@@@#@@@@#@##@#@@@@##@@#@@@@@@@#@@@@@#@#@@@@  1");
            Write(0x888888, false, "6 **\n           @@#@@@@@#@@@#@#@@@#@##@@@##@#@@#@@#@@##@@@@@@@@@#  15 **\n           @@@#@@@#@@@@@@@@");
            Write(0x888888, false, "@@@@#@@@##@@#@@@#@@##@@@@@####@@@  14 **\n           @@@@@#@@#@#@@#@#@##@@@@@@@#@#@@@@#@@##@@#@@@@##@");
            Write(0x888888, false, "@  13 **\n           @@@@#@#@##@@##@@@@#@@|@#@@#@@@@@##@@@#@@@@@@@@@@@  12 **\n           @@@@##@@##@@");
            Write(0x888888, false, "@@@#@@@#@@@@@@@@@@@@@#@@@@#@@#@@@@@@@  11 **\n           ##@#@@#@#@@#@@@@@@##@@@@@#@#@@@#@@#@@@@@#@@#");
            Write(0x888888, false, "@@#@#  10 **\n           @@@@@#@@@@@@@@#@@@@@@#@@#@@@@@#@@@@@##@#@@@@#@#@#   9 **\n           #@@@@###");
            Write(0x888888, false, "@@#@@@@@@@@@@@@@@@@@@@@#@@@@@@@@#@@@#@#@@   8 **\n           #@#@@##@#");
            Write(0x7fbd39, false, "#");
            Write(0x427322, false, "#");
            Write(0x5eabb4, false, ".~~.");
            Write(0x7fbd39, false, "@");
            Write(0x488813, false, "#@@@");
            Write(0x4d8b03, false, "#");
            Write(0x488813, false, "@");
            Write(0x1461f, false, "@");
            Write(0x7fbd39, false, "#");
            Write(0xd0b376, false, ".");
            Write(0x1461f, false, "@");
            Write(0x7fbd39, false, "@");
            Write(0x888888, false, "@@#@@@@@@@##@@#@@##@@@   7 **\n           @@@#@@#|");
            Write(0x7fbd39, false, "@");
            Write(0x488813, false, "#@");
            Write(0x4d8b03, false, "@");
            Write(0x5eabb4, false, ".~~.");
            Write(0x488813, false, "@");
            Write(0x4d8b03, false, "#");
            Write(0x427322, false, "#");
            Write(0x7fbd39, false, "##");
            Write(0x4d8b03, false, "@");
            Write(0x427322, false, "#");
            Write(0xd0b376, false, "..");
            Write(0x488813, false, "@");
            Write(0x1461f, false, "@");
            Write(0x488813, false, "#");
            Write(0x427322, false, "@");
            Write(0x888888, false, "@@@#@#@##@@#@#@@@@#@   6 **\n           #@@@@@");
            Write(0x488813, false, "#");
            Write(0x427322, false, "#");
            Write(0x488813, false, "@");
            Write(0x4d8b03, false, "@");
            Write(0x427322, false, "@");
            Write(0x7fbd39, false, "@#");
            Write(0x5eabb4, false, ".~~.");
            Write(0x427322, false, "@@");
            Write(0xd0b376, false, ".");
            Write(0xeeeeee, false, "/\\");
            Write(0xd0b376, false, ".'");
            Write(0x7fbd39, false, "@");
            Write(0x488813, false, "@");
            Write(0x7fbd39, false, "@");
            Write(0x488813, false, "#");
            Write(0x7fbd39, false, "@@");
            Write(0x888888, false, "@#@@@@@#@#@@@@@@@##   5 **\n           @@#");
            Write(0x4d8b03, false, "@");
            Write(0x7fbd39, false, "@");
            Write(0x427322, false, "@@@");
            Write(0x4d8b03, false, "#");
            Write(0x488813, false, "#");
            Write(0x1461f, false, "@");
            Write(0xd0b376, false, ".'");
            Write(0x5eabb4, false, " ~  ");
            Write(0xd0b376, false, "'.");
            Write(0xeeeeee, false, "/\\");
            Write(0xd0b376, false, "'.");
            Write(0xeeeeee, false, "/\\");
            Write(0xd0b376, false, "' .");
            Write(0x4d8b03, false, "@@@");
            Write(0x888888, false, "@##@@@@#@#@#@#@@##   4 **\n           ");
            Write(0x7fbd39, false, "#");
            Write(0x427322, false, "@");
            Write(0x4d8b03, false, "#");
            Write(0x7fbd39, false, "@@");
            Write(0x427322, false, "@");
            Write(0x4d8b03, false, "#");
            Write(0x427322, false, "#@");
            Write(0xd0b376, false, "_/");
            Write(0x5eabb4, false, " ~   ~  ");
            Write(0xd0b376, false, "\\ ' '. '.'.");
            Write(0x488813, false, "@@");
            Write(0x888888, false, "#@@@@@#@@@|@@##@@   3 **\n           ");
            Write(0xd0b376, false, "-~------'");
            Write(0x5eabb4, false, "    ~    ~ ");
            Write(0xd0b376, false, "'--~-----~-~----___________--  ");
            Write(0x888888, false, " 2 **\n           ");
            Write(0x5eabb4, false, "  ~    ~  ~      ~     ~ ~   ~     ~  ~  ~   ~     ");
            Write(0x888888, false, " 1 **\n           \n");
            
        Console.ForegroundColor = color;
        Console.WriteLine();
    }

   private static void Write(int rgb, bool bold, string text){
       Console.Write($"\u001b[38;2;{(rgb>>16)&255};{(rgb>>8)&255};{rgb&255}{(bold ? ";1" : "")}m{text}");
   }
}
