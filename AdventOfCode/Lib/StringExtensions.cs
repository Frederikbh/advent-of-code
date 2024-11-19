using System.Text.RegularExpressions;

namespace AdventOfCode.Lib;

public static partial class StringExtensions
{
    public static string StripMargin(this string st, string margin = "|") =>
        string.Join(
            "\n",
            from line in StripNewLine()
                .Split(st)
            select Regex.Replace(line, @"^\s*" + Regex.Escape(margin), "")
        );

    public static string Indent(this string st, int l, bool firstLine = false)
    {
        var indent = new string(' ', l);
        var res = string.Join(
            "\n" + new string(' ', l),
            from line in StripNewLine()
                .Split(st)
            select StripPipe()
                .Replace(line, "")
        );

        return firstLine
            ? indent + res
            : res;
    }

    [GeneratedRegex("\r?\n")]
    public static partial Regex StripNewLine();

    [GeneratedRegex(@"^\s*\|")]
    public static partial Regex StripPipe();
}
