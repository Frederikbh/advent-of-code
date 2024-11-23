using AdventOfCode.Lib;

namespace AdventOfCode._2015.Day23;

[ProblemName("Opening the Turing Lock")]
public class Solution : ISolver
{
    private static readonly Dictionary<char, int> s_registerMap = new()
    {
        { 'a', 0 },
        { 'b', 1 }
    };

    private enum OpCode
    {
        Hlf,
        Tpl,
        Inc,
        Jmp,
        Jie,
        Jio
    };

    private struct Instruction
    {
        public OpCode Operation;
        public int Arg1;
        public int Arg2;
    }

    public object PartOne(string input)
    {
        var instructions = ParseInstructions(input);
        var registers = new int[2];

        Execute(instructions, registers);

        return registers[s_registerMap['b']];
    }

    public object PartTwo(string input)
    {
        var instructions = ParseInstructions(input);
        var registers = new int[2];

        registers[s_registerMap['a']] = 1;

        Execute(instructions, registers);

        return registers[s_registerMap['b']];
    }

    private static void Execute(List<Instruction> instructions, int[] registers)
    {
        var i = 0;

        while (i >= 0 && i < instructions.Count)
        {
            var instr = instructions[i];

            switch (instr.Operation)
            {
                case OpCode.Hlf:
                    registers[instr.Arg1] /= 2;
                    i++;

                    break;
                case OpCode.Tpl:
                    registers[instr.Arg1] *= 3;
                    i++;

                    break;
                case OpCode.Inc:
                    registers[instr.Arg1]++;
                    i++;

                    break;
                case OpCode.Jmp:
                    i += instr.Arg1;

                    break;
                case OpCode.Jie:
                    if (registers[instr.Arg1] % 2 == 0)
                    {
                        i += instr.Arg2;
                    }
                    else
                    {
                        i++;
                    }

                    break;
                case OpCode.Jio:
                    if (registers[instr.Arg1] == 1)
                    {
                        i += instr.Arg2;
                    }
                    else
                    {
                        i++;
                    }

                    break;
            }
        }
    }

    private static List<Instruction> ParseInstructions(string input)
    {
        var instructions = new List<Instruction>();
        var lines = input.Split('\n');

        foreach (var line in lines)
        {
            var parts = line.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);
            var instr = new Instruction();

            switch (parts[0])
            {
                case "hlf":
                    instr.Operation = OpCode.Hlf;
                    ParseArg(parts[1], out instr.Arg1);

                    break;
                case "tpl":
                    instr.Operation = OpCode.Tpl;
                    ParseArg(parts[1], out instr.Arg1);

                    break;
                case "inc":
                    instr.Operation = OpCode.Inc;
                    ParseArg(parts[1], out instr.Arg1);

                    break;
                case "jmp":
                    instr.Operation = OpCode.Jmp;
                    ParseArg(parts[1], out instr.Arg1);

                    break;
                case "jie":
                    instr.Operation = OpCode.Jie;
                    ParseArg(parts[1], out instr.Arg1);
                    ParseArg(parts[2], out instr.Arg2);

                    break;
                case "jio":
                    instr.Operation = OpCode.Jio;
                    ParseArg(parts[1], out instr.Arg1);
                    ParseArg(parts[2], out instr.Arg2);

                    break;
            }

            instructions.Add(instr);
        }

        return instructions;
    }

    private static void ParseArg(string arg, out int value) =>
        value = char.IsLetter(arg[0])
            ? s_registerMap[arg[0]]
            : int.Parse(arg);
}
