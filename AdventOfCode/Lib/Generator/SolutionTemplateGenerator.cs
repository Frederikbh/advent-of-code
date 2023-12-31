using AdventOfCode.Model;

namespace AdventOfCode.Generator;

public class SolutionTemplateGenerator {
    public string Generate(Problem problem) {
        return $@"using System;
             |using System.Collections.Generic;
             |using System.Collections.Immutable;
             |using System.Linq;
             |using System.Text.RegularExpressions;
             |using System.Text;
             |
             |namespace AdventOfCode.Y{problem.Year}.Day{problem.Day:00};
             |
             |[ProblemName(""{problem.Title}"")]
             |public class Solution : ISolver 
             |{{
             |
             |    public object PartOne(string input) 
             |    {{
             |        return 0;
             |    }}
             |
             |    public object PartTwo(string input) 
             |    {{
             |        return 0;
             |    }}
             |}}
             |".StripMargin();
    }
}
