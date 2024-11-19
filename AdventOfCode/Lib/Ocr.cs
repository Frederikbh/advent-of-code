using System.Text.RegularExpressions;

namespace AdventOfCode.Lib;

public static class OcrExtension
{
    public static OcrString Ocr(this string st) => new(st);
}

public record OcrString(string St)
{
    public override string ToString()
    {
        var lines = St.Split("\n")
            .SkipWhile(string.IsNullOrWhiteSpace)
            .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        while (lines.All(line => line.StartsWith(' ')))
        {
            lines = GetRect(lines, 1, 0, lines[0].Length - 1, lines.Length)
                .Split("\n");
        }

        while (lines.All(line => line.EndsWith(' ')))
        {
            lines = GetRect(lines, 0, 0, lines[0].Length - 1, lines.Length)
                .Split("\n");
        }

        var width = lines[0].Length;

        var smallAlphabet = StripMargin(
            """
            
                    | A    B    C    E    F    G    H    I    J    K    L    O    P    R    S    U    Y    Z    
                    |  ##  ###   ##  #### ####  ##  #  #  ###   ## #  # #     ##  ###  ###   ### #  # #   ##### 
                    | #  # #  # #  # #    #    #  # #  #   #     # # #  #    #  # #  # #  # #    #  # #   #   # 
                    | #  # ###  #    ###  ###  #    ####   #     # ##   #    #  # #  # #  # #    #  #  # #   #  
                    | #### #  # #    #    #    # ## #  #   #     # # #  #    #  # ###  ###   ##  #  #   #   #   
                    | #  # #  # #  # #    #    #  # #  #   #  #  # # #  #    #  # #    # #     # #  #   #  #    
                    | #  # ###   ##  #### #     ### #  #  ###  ##  #  # ####  ##  #    #  # ###   ##    #  #### 
                    
            """);

        var largeAlphabet = StripMargin(
            """
            
                    | A       B       C       E       F       G       H       J       K       L       N       P       R       X       Z
                    |   ##    #####    ####   ######  ######   ####   #    #     ###  #    #  #       #    #  #####   #####   #    #  ######  
                    |  #  #   #    #  #    #  #       #       #    #  #    #      #   #   #   #       ##   #  #    #  #    #  #    #       #  
                    | #    #  #    #  #       #       #       #       #    #      #   #  #    #       ##   #  #    #  #    #   #  #        #  
                    | #    #  #    #  #       #       #       #       #    #      #   # #     #       # #  #  #    #  #    #   #  #       #   
                    | #    #  #####   #       #####   #####   #       ######      #   ##      #       # #  #  #####   #####     ##       #    
                    | ######  #    #  #       #       #       #  ###  #    #      #   ##      #       #  # #  #       #  #      ##      #     
                    | #    #  #    #  #       #       #       #    #  #    #      #   # #     #       #  # #  #       #   #    #  #    #      
                    | #    #  #    #  #       #       #       #    #  #    #  #   #   #  #    #       #   ##  #       #   #    #  #   #       
                    | #    #  #    #  #    #  #       #       #   ##  #    #  #   #   #   #   #       #   ##  #       #    #  #    #  #       
                    | #    #  #####    ####   ######  #        ### #  #    #   ###    #    #  ######  #    #  #       #    #  #    #  ######  
                    
            """);

        var charMap =
            lines.Length == smallAlphabet.Length - 1 ? smallAlphabet :
            lines.Length == largeAlphabet.Length - 1 ? largeAlphabet :
            throw new Exception("Could not find alphabet");

        var charWidth = charMap == smallAlphabet
            ? 5
            : 8;
        var charHeight = charMap == smallAlphabet
            ? 6
            : 10;
        var res = "";

        for (var i = 0; i < width; i += charWidth)
        {
            res += Detect(lines, i, charWidth, charHeight, charMap);
        }

        return res;
    }

    private string[] StripMargin(string st) =>
    (
        from line in Regex.Split(st, "\r?\n")
        where Regex.IsMatch(line, @"^\s*\| ")
        select Regex.Replace(line, @"^\s* \| ", "")
    ).ToArray();

    public string Detect(string[] text, int iColLetter, int charWidth, int charHeight, string[] charMap)
    {
        var textRect = GetRect(text, iColLetter, 0, charWidth, charHeight);

        for (var iCol = 0; iCol < charMap[0].Length; iCol += charWidth)
        {
            var ch = charMap[0][iCol]
                .ToString();
            var charPattern = GetRect(charMap, iCol, 1, charWidth, charHeight);
            var found = Enumerable.Range(0, charPattern.Length)
                .All(
                    i =>
                    {
                        var textWhiteSpace = " .".Contains(textRect[i]);
                        var charWhiteSpace = " .".Contains(charPattern[i]);

                        return textWhiteSpace == charWhiteSpace;
                    });

            if (found)
            {
                return ch;
            }
        }

        throw new Exception($"Unrecognized letter: \n{textRect}\n");
    }

    private string GetRect(IReadOnlyList<string> str, int iCol0, int iRow0, int containerCol, int containerRow)
    {
        var res = "";

        for (var iRow = iRow0; iRow < iRow0 + containerRow; iRow++)
        {
            for (var iCol = iCol0; iCol < iCol0 + containerCol; iCol++)
            {
                var ch = iRow < str.Count && iCol < str[iRow].Length
                    ? str[iRow][iCol]
                    : ' ';
                res += ch;
            }

            if (iRow + 1 != iRow0 + containerRow)
            {
                res += "\n";
            }
        }

        return res;
    }
}
