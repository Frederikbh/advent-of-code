using System.Text;

using AdventOfCode.Lib.Model;

namespace AdventOfCode.Lib.Generator;

public class SplashScreenGenerator {
    public static string Generate(Calendar calendar) {
        var calendarPrinter = CalendarPrinter(calendar);
        return $$"""
                    |using AdventOfCode.Lib;
                    |
                    |namespace AdventOfCode._{{calendar.Year}};
                    |
                    |public class SplashScreenImpl : ISplashScreen {
                    |
                    |    public void Show() {
                    |
                    |        var color = Console.ForegroundColor;
                    |        {{calendarPrinter.Indent(12)}}
                    |        Console.ForegroundColor = color;
                    |        Console.WriteLine();
                    |    }
                    |
                    |   private static void Write(int rgb, bool bold, string text){
                    |       Console.Write($"\u001b[38;2;{(rgb>>16)&255};{(rgb>>8)&255};{rgb&255}{(bold ? ";1" : "")}m{text}");
                    |   }
                    |}
                    |
               """.StripMargin();
    }

    private static string CalendarPrinter(Calendar calendar) {

        var lines = calendar.Lines.Select(line =>
            new[] { new CalendarToken { Text = "           " } }.Concat(line)).ToList();

        var bw = new BufferWriter();
        foreach (var line in lines) {
            foreach (var token in line) {
                bw.Write(token.ConsoleColor, token.Text, token.Bold);
            }

            bw.Write(-1, "\n", false);
        }
        return bw.GetContent();
    }

    private class BufferWriter {
        private readonly StringBuilder _sb = new();
        private int _bufferColor = -1;
        private string _buffer = "";
        private bool _bufferBold;

        public void Write(int color, string text, bool bold) {
            if (!string.IsNullOrWhiteSpace(text)) {
                if (!string.IsNullOrWhiteSpace(_buffer) && (color != _bufferColor || _bufferBold != bold) ) {
                    Flush();
                }
                _bufferColor = color;
                _bufferBold = bold;
            }
            _buffer += text;
        }

        private void Flush() {
            while (_buffer.Length > 0) {
                var block = _buffer[..Math.Min(100, _buffer.Length)];
                _buffer = _buffer[block.Length..];
                block = block.Replace("\\", @"\\").Replace("\"", "\\\"").Replace("\n", "\\n");
                _sb.AppendLine($@"Write(0x{_bufferColor.ToString("x")}, {_bufferBold.ToString().ToLower()}, ""{block}"");");
            }
            _buffer = "";
        }

        public string GetContent() {
            Flush();
            return _sb.ToString();
        }
    }
}
