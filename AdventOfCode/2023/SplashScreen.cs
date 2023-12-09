
using System;

namespace AdventOfCode.Y2023;

class SplashScreenImpl : SplashScreen {

    public void Show() {

        var color = Console.ForegroundColor;
        Write(0xcc00, false, "           ▄█▄ ▄▄█ ▄ ▄ ▄▄▄ ▄▄ ▄█▄  ▄▄▄ ▄█  ▄▄ ▄▄▄ ▄▄█ ▄▄▄\n           █▄█ █ █ █ █ █▄█ █ █ █   █ █ █▄ ");
            Write(0xcc00, false, " █  █ █ █ █ █▄█\n           █ █ █▄█ ▀▄▀ █▄▄ █ █ █▄  █▄█ █   █▄ █▄█ █▄█ █▄▄  /* 2023 */\n            \n ");
            Write(0xcc00, false, "                                                           \n                                        ");
            Write(0xcc00, false, "                    \n                                                            \n                  ");
            Write(0xcc00, false, "                                          \n                                                         ");
            Write(0xcc00, false, "   \n                                                            \n                                   ");
            Write(0xcc00, false, "                         \n                                                            \n           ");
            Write(0x888888, false, "            *        ");
            Write(0xe3b585, false, "' ''...                       ");
            Write(0x888888, false, "10\n                     ");
            Write(0xe3b585, false, ".   ~ ~ ~ ~   ");
            Write(0x6b4d3b, false, "### ");
            Write(0xe3b585, false, "''.                  \n                   .' ~  ");
            Write(0xcc00, false, ",");
            Write(0xffff66, true, "* ");
            Write(0xe3b585, false, "~ ~ ~ ~ ");
            Write(0x6b4d3b, false, "##### ");
            Write(0xe3b585, false, "'.                  ");
            Write(0x888888, false, " 9 **\n                   ");
            Write(0xe3b585, false, ": ~ ");
            Write(0xcc00, false, "'");
            Write(0x5555bb, false, "(~)");
            Write(0xcc00, false, ", ");
            Write(0xe3b585, false, "~ ");
            Write(0xffff66, true, "* ");
            Write(0xe3b585, false, "~ ~ ~ ");
            Write(0x6b4d3b, false, "### ");
            Write(0xe3b585, false, ":                  ");
            Write(0x888888, false, " 8 **\n                   ");
            Write(0xe3b585, false, "'. ~ ");
            Write(0xcc00, false, "\" ' ");
            Write(0xe3b585, false, "~ ~ ~   ");
            Write(0x6b4d3b, false, "##### ");
            Write(0xe3b585, false, ".'                \n                     '.. ~ ~ ");
            Write(0xffff66, true, "* ");
            Write(0xe3b585, false, "~ ");
            Write(0x6b4d3b, false, "##### ");
            Write(0xe3b585, false, "..'");
            Write(0xcc00, false, ".'''''''''...       ");
            Write(0x888888, false, " 7 **\n                        ");
            Write(0xe3b585, false, "'''.........'''");
            Write(0xcc00, false, "' ");
            Write(0x5555bb, false, "~ ");
            Write(0xcc00, false, ".'");
            Write(0xffff66, true, "*");
            Write(0xcc00, false, ". ");
            Write(0x5555bb, false, "~  ");
            Write(0xcc00, false, "..  ''.    ");
            Write(0x888888, false, " 6 **\n                                   ");
            Write(0xcc00, false, ".' ");
            Write(0x5555bb, false, "~    ");
            Write(0xcc00, false, "'...' ");
            Write(0x5555bb, false, "~");
            Write(0xcc00, false, "'  '.");
            Write(0x5555bb, false, "~  ");
            Write(0xcc00, false, "'.\n                                   :         ");
            Write(0x5555bb, false, "~     ");
            Write(0xcc00, false, "'. ");
            Write(0xffff66, true, "*");
            Write(0xcc00, false, "'.");
            Write(0x5555bb, false, "~ ");
            Write(0xcc00, false, ":  ");
            Write(0x888888, false, " 5 **\n                            ");
            Write(0xffffff, false, "...''''");
            Write(0xcc00, false, "'.         .''.");
            Write(0x5555bb, false, "~  ");
            Write(0xcc00, false, "'..' .'\n                         ");
            Write(0xffffff, false, ".''         ");
            Write(0xcc00, false, "'..  ");
            Write(0x5555bb, false, "~");
            Write(0xcc00, false, "..'");
            Write(0xffff66, true, "*   ");
            Write(0xcc00, false, "'. ");
            Write(0x5555bb, false, "~ ");
            Write(0xcc00, false, "..'    ");
            Write(0x888888, false, " 4 **\n                       ");
            Write(0xffffff, false, ".'               ");
            Write(0xcc00, false, "'''..");
            Write(0xd4dde4, false, "/");
            Write(0xcc00, false, "......'''     \n                       ");
            Write(0xffffff, false, ":             /\\    ");
            Write(0xccccff, false, "-");
            Write(0xd4dde4, false, "/  ");
            Write(0xffffff, false, ":            \n                       '.            ");
            Write(0xccccff, false, "-   - ");
            Write(0xd4dde4, false, "/  ");
            Write(0xffffff, false, ".'            \n                         '..    ");
            Write(0xccccff, false, "-     -   ");
            Write(0xffff66, true, "*");
            Write(0xffffff, false, "..'                ");
            Write(0x888888, false, " 3 **\n               ");
            Write(0x9b715b, false, "----@        ");
            Write(0xffffff, false, "'''..");
            Write(0xffff66, true, "*");
            Write(0xffffff, false, "......'''                   ");
            Write(0x888888, false, " 2 **\n             ");
            Write(0xffff66, true, "* ");
            Write(0x9b715b, false, "! /^\\                                          ");
            Write(0x888888, false, " 1 **\n           \n");
            
        Console.ForegroundColor = color;
        Console.WriteLine();
    }

   private static void Write(int rgb, bool bold, string text){
       Console.Write($"\u001b[38;2;{(rgb>>16)&255};{(rgb>>8)&255};{rgb&255}{(bold ? ";1" : "")}m{text}");
   }
}
