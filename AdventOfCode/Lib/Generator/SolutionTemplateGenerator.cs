using AdventOfCode.Lib.Model;

namespace AdventOfCode.Lib.Generator;

public class SolutionTemplateGenerator {
    public static string Generate(Problem problem) =>
        $$"""
              |using System;
              |using System.Collections.Generic;
              |using System.Collections.Immutable;
              |using System.Linq;
              |using System.Text.RegularExpressions;
              |using System.Text;
              |
              |using AdventOfCode.Lib;        
              |
              |namespace AdventOfCode._{{problem.Year}}.Day{{problem.Day:00}};
              |
              |[ProblemName("{{problem.Title}}")]
              |public class Solution : ISolver 
              |{
              |
              |    public object PartOne(string input) 
              |    {
              |        return 0;
              |    }
              |
              |    public object PartTwo(string input) 
              |    {
              |        return 0;
              |    }
              |}
              |
        """.StripMargin();
}
