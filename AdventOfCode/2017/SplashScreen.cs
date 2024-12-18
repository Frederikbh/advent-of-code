using AdventOfCode.Lib;

namespace AdventOfCode._2017;

public class SplashScreenImpl : ISplashScreen {

    public void Show() {

        var color = Console.ForegroundColor;
        Write(0xcc00, false, "           ▄█▄ ▄▄█ ▄ ▄ ▄▄▄ ▄▄ ▄█▄  ▄▄▄ ▄█  ▄▄ ▄▄▄ ▄▄█ ▄▄▄\n           █▄█ █ █ █ █ █▄█ █ █ █   █ █ █▄ ");
            Write(0xcc00, false, " █  █ █ █ █ █▄█\n           █ █ █▄█ ▀▄▀ █▄▄ █ █ █▄  █▄█ █   █▄ █▄█ █▄█ █▄▄  // 2017\n            \n    ");
            Write(0xcc00, false, "       ");
            Write(0xcccccc, false, ".         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         .                \n  ");
            Write(0xcccccc, false, "         |                   ");
            Write(0x666666, false, "┌         ─         ─         ─         ─         ─         ─         ┬         o         ┌         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ─         *         ");
            Write(0x666666, false, "─         ─         ");
            Write(0x990099, false, "┤         [         ]         ├         ");
            Write(0x666666, false, "─         ─         ");
            Write(0x66ff, false, "|         (         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ┐         o         ─         ");
            Write(0x666666, false, "─         ─         ─         ┬         o                   ");
            Write(0xcccccc, false, "|           25 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "│         ┌         ─         ┐         o         ─         ┐         └         ─         ┘         ");
            Write(0x666666, false, "┌         ─         ─         ─         ─         ─         ─         ─         ─         ┤         ");
            Write(0x666666, false, "┌         ─         ─         ─         ─         *         o         ─         ─         ┬         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         o         └         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ┤         V                   ");
            Write(0xcccccc, false, "|           24 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ┘         V         └         ─         ─         ┴         ┐         ┌         ─         ");
            Write(0x666666, false, "┘         o         ─         ─         ─         ─         ─         ─         ─         ┘         ");
            Write(0x666666, false, "└         ─         ─         ┐         ┌         ┘         ┌         ─         *         └         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ");
            Write(0xaaaaaa, false, "[         ─         ]         ");
            Write(0x666666, false, "─         ─         ─         ┴         ┘                   ");
            Write(0xcccccc, false, "|           23 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "o         ─         ┴         ─         ─         ─         ─         ┘         └         ─         ");
            Write(0x666666, false, "┐         ┌         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ┘         └         ─         ┴         o         └         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         *                   ");
            Write(0xcccccc, false, "|           22 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "┌         ─         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "┘         ├         ─         ");
            Write(0xaaaaaa, false, "[         ─         ]         ");
            Write(0x666666, false, "─         ─         o         *         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0xff0000, false, "┤         |         ├         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ┤                   ");
            Write(0xcccccc, false, "|           21 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "├         ─         ─         ─         ");
            Write(0xff9900, false, "∧         ∧         ∧         ");
            Write(0x666666, false, "─         ─         ─         ─         ┘         *         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ┼         ┴         ┴         ┴         ┴         ┬         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ┬         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ┐         │                   ");
            Write(0xcccccc, false, "|           20 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ─         ─         ─         ─         ─         ─         ─         o         *         ");
            Write(0x666666, false, "─         ┐         └         ─         ┐         ┌         ─         ─         ─         ┤         ");
            Write(0x666666, false, "                                        ├         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ┘         ┌         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ┐         o         ┘         │                   ");
            Write(0xcccccc, false, "|           19 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "*         ─         ─         ┬         ─         ─         ─         ─         ─         ┘         ");
            Write(0x666666, false, "o         ┴         ─         ─         ┘         └         ─         ─         ─         ┤         ");
            Write(0x666666, false, "P         R         2         A         ├         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ┴         ─         o         ┌         ─         ─         ");
            Write(0x666666, false, "─         ┘         ┌         ─         ┘                   ");
            Write(0xcccccc, false, "|           18 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ─         ┐         │         ┌         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "┬         ┴         ┴         ┴         ┬         ─         ─         ─         ─         ┤         ");
            Write(0x666666, false, "          0         3         G         ├         ─         ┐         ┌         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         *         └         ─         ─         ");
            Write(0x666666, false, "─         ─         ┴         ─         o                   ");
            Write(0xcccccc, false, "|           17 ");
            Write(0x666666, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "┌         ─         ┘         =         └         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "┤                                       ├         ─         ─         ─         ─         ┴         ");
            Write(0x666666, false, "┬         ┬         ┬         ┬         ┴         ─         ┘         └         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ┐         └         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ");
            Write(0xffff66, true, "*                   ");
            Write(0xcccccc, false, "|           16 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "├         ─         ─         ─         ┬         ─         ─         ─         ─         o         ");
            Write(0x666666, false, "┤                                       ├         ─         ┐         ┌         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ");
            Write(0x66ff, false, "|         (         ");
            Write(0x666666, false, "─         ─         ┘         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ─         ─         ");
            Write(0x66ff, false, "|         (         ");
            Write(0x666666, false, "─         ─         ┘                   ");
            Write(0xcccccc, false, "|           15 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ─         ─         ┐         └         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "┤                                       ├         ─         ┘         └         ─         ┬         ");
            Write(0x666666, false, "─         o         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ┬         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ┐         └         ─         ─         ");
            Write(0xaaaaaa, false, "[         ─         ]         ");
            Write(0x666666, false, "─         ─         ┐                   ");
            Write(0xcccccc, false, "|           14 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "o         ─         ┐         └         ┬         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "┴         ┬         ┬         ┬         ┴         ─         ┬         ─         ─         ┘         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "┐         └         ─         ─         ┐         o         ─         ┘         ┌         ─         ");
            Write(0x666666, false, "┐         o         ─         ─         ┴         ┬         ┴         ┴         ┴         ┬         ");
            Write(0x666666, false, "─         ─         ─         ┘                   ");
            Write(0xcccccc, false, "|           13 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "┌         ─         ┴         o         │         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ─         ┐         ");
            Write(0x666666, false, "└         ─         o         ┌         ┘         └         ─         ─         ┐         └         ");
            Write(0x666666, false, "─         ─         ─         ┴         o         └         ─         ─         ─         ─         ");
            Write(0x666666, false, "┤                                       ├         ─         ─         ─         ┐                   ");
            Write(0xcccccc, false, "|           12 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ─         ─         ─         ┘         └         ─         ─         ─         ─         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "┌         ─         ─         o         └         ─         ─         ─         ┘         o         ");
            Write(0x666666, false, "─         ─         ┐         └         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ┤                                       ├         ");
            Write(0x666666, false, "─         ─         ─         ┤                   ");
            Write(0xcccccc, false, "|           11 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ");
            Write(0xaaaaaa, false, "[         ─         ]         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ┘         └         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ┬         ┬         ┴         ┴         ┴         ┬         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ┤                                       ├         ─         ─         o         │         ");
            Write(0x666666, false, "          ");
            Write(0xcccccc, false, "|           10 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "├         ─         ─         ─         ─         ┐         ┌         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "└         ┤                             1         ├         ─         ─         ┬         ┴         ");
            Write(0x666666, false, "┴         ┴         ┬         ─         ─         ─         ─         ┴         ┬         ┬         ");
            Write(0x666666, false, "┬         ┴         ─         ─         ─         ┘                   ");
            Write(0xcccccc, false, "|            9 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "│         o         ─         ┬         o         └         ┴         ─         ─         o         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ┬         ┴         ┴         ┴         ┴         ┴         ┼         ─         ┤         ");
            Write(0x666666, false, "          A         0         ├         ─         ─         ┤                                       ");
            Write(0x666666, false, "├         ─         ─         ─         ─         ─         ─         ┐         └         ─         ");
            Write(0x666666, false, "─         ─         ─         ┐                   ");
            Write(0xcccccc, false, "|            8 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ─         ─         ┤         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ┘         o         ┤                             ");
            Write(0x666666, false, "                              ├         ─         ┤                   P         7         ├         ");
            Write(0x666666, false, "─         ─         ┤                             H         ├         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ┘         o         ─         ─         ─         ─         ┘         ");
            Write(0x666666, false, "          ");
            Write(0xcccccc, false, "|            7 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "o         ─         ─         ┘         └         ─         ─         ");
            Write(0x9900, false, "o         T         o         ");
            Write(0x666666, false, "─         ─         ┤                   E         N         C         R         ├         ─         ");
            Write(0x666666, false, "┤                   L         1         ├         ─         ─         ┤                             ");
            Write(0x666666, false, "C         ├         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "o                   ");
            Write(0xcccccc, false, "|            6 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "o         ─         ─         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ┤                   ");
            Write(0x666666, false, "Y         P         T         R         ├         ─         ┴         ┬         ┬         ┬         ");
            Write(0x666666, false, "┴         ─         ─         ┤                             F         ├         ─         ─         ");
            Write(0x666666, false, "┐         ┌         ─         ─         ─         ─         ─         ─         ┐         └         ");
            Write(0x666666, false, "┐                   ");
            Write(0xcccccc, false, "|            5 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "┌         ─         ─         ┘         ┌         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ┴         ┬         ┬         ┬         ┬         ┬         ┴         ─         ");
            Write(0x666666, false, "─         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "┌         ─         ─         ─         ─         ┴         ┬         ┬         ┬         ┼         ");
            Write(0x666666, false, "─         ─         ┘         └         ─         ─         ─         ─         ┐         o         ");
            Write(0x666666, false, "┴         ─         ┘                   ");
            Write(0xcccccc, false, "|            4 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ─         ─         ─         ┴         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "o         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ─         ─         ─         ┐         ┌         ─         ─         ─         ┘         ");
            Write(0x666666, false, "└         ─         ─         ─         ─         ┐         o         ─         ┘         └         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ┐         └         ─         ");
            Write(0x666666, false, "─         ─         ┐                   ");
            Write(0xcccccc, false, "|            3 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "┌         ─         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ┘         o         ─         ─         ─         ┴         ┘         ┌         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "├         ─         ─         ─         ┬         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         o         └         ─         ─         ─         ─         ┤                   ");
            Write(0xcccccc, false, "|            2 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "|                   ");
            Write(0x666666, false, "└         ─         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ┴         ─         ");
            Write(0x666666, false, "─         o         ");
            Write(0xffff66, true, "*         ");
            Write(0x666666, false, "─         ─         ─         ┘         └         ─         ─         o         └         ─         ");
            Write(0x666666, false, "─         ─         ─         ─         ─         ─         ─         ─         ─         ─         ");
            Write(0x666666, false, "─         ┘                   ");
            Write(0xcccccc, false, "|            1 ");
            Write(0xffff66, false, "**\n           ");
            Write(0xcccccc, false, "'         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         -         -         ");
            Write(0xcccccc, false, "-         -         -         -         -         -         -         -         '                \n  ");
            Write(0xcccccc, false, "         \n");
            
        Console.ForegroundColor = color;
        Console.WriteLine();
    }

   private static void Write(int rgb, bool bold, string text){
       Console.Write($"\u001b[38;2;{(rgb>>16)&255};{(rgb>>8)&255};{rgb&255}{(bold ? ";1" : "")}m{text}");
   }
}
