using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day12;

[ProblemName("Leonardo's Monorail")]
public class Solution : ISolver
{
    // Define register indices for faster access
    private static readonly Dictionary<char, int> s_registerMap = new()
        {
            { 'a', 0 },
            { 'b', 1 },
            { 'c', 2 },
            { 'd', 3 }
        };

    // Define instruction types
    private enum OpCode { Cpy, Inc, Dec, Jnz }

    private struct Instruction
    {
        public OpCode Operation;
        public int Arg1; // Can be a register index or an immediate value
        public bool Arg1IsRegister;
        public int Arg2; // For Cpy and Jnz: register index or immediate value
        public bool Arg2IsRegister;
    }

    public object PartOne(string input)
    {
        var instructions = ParseInstructions(input);
        var registers = new int[4]; // Registers a, b, c, d

        Execute(instructions, registers);

        return registers[s_registerMap['a']];
    }

    public object PartTwo(string input)
    {
        var instructions = ParseInstructions(input);
        var registers = new int[4];
        registers[s_registerMap['c']] = 1;

        Execute(instructions, registers);

        return registers[s_registerMap['a']];
    }

    private List<Instruction> ParseInstructions(string input)
    {
        var instructions = new List<Instruction>();
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var instr = new Instruction();

            switch (parts[0])
            {
                case "cpy":
                    instr.Operation = OpCode.Cpy;
                    ParseArg(parts[1], out instr.Arg1, out instr.Arg1IsRegister);
                    ParseArg(parts[2], out instr.Arg2, out instr.Arg2IsRegister);
                    break;
                case "inc":
                    instr.Operation = OpCode.Inc;
                    ParseArg(parts[1], out instr.Arg1, out instr.Arg1IsRegister);
                    break;
                case "dec":
                    instr.Operation = OpCode.Dec;
                    ParseArg(parts[1], out instr.Arg1, out instr.Arg1IsRegister);
                    break;
                case "jnz":
                    instr.Operation = OpCode.Jnz;
                    ParseArg(parts[1], out instr.Arg1, out instr.Arg1IsRegister);
                    ParseArg(parts[2], out instr.Arg2, out instr.Arg2IsRegister);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown instruction: {parts[0]}");
            }

            instructions.Add(instr);
        }

        return instructions;
    }

    private static void ParseArg(string arg, out int value, out bool isRegister)
    {
        if (char.IsLetter(arg[0]))
        {
            value = s_registerMap[arg[0]];
            isRegister = true;
        }
        else
        {
            value = int.Parse(arg);
            isRegister = false;
        }
    }

    private static void Execute(List<Instruction> instructions, int[] registers)
    {
        var i = 0;
        while (i >= 0 && i < instructions.Count)
        {
            var instr = instructions[i];
            switch (instr.Operation)
            {
                case OpCode.Cpy:
                    if (instr.Arg2IsRegister)
                    {
                        var val = instr.Arg1IsRegister ? registers[instr.Arg1] : instr.Arg1;
                        registers[instr.Arg2] = val;
                    }
                    i++;
                    break;
                case OpCode.Inc:
                    if (instr.Arg1IsRegister)
                    {
                        registers[instr.Arg1]++;
                    }
                    i++;
                    break;
                case OpCode.Dec:
                    if (instr.Arg1IsRegister)
                    {
                        registers[instr.Arg1]--;
                    }
                    i++;
                    break;
                case OpCode.Jnz:
                    var cmp = instr.Arg1IsRegister ? registers[instr.Arg1] : instr.Arg1;
                    if (cmp != 0)
                    {
                        var jump = instr.Arg2IsRegister ? registers[instr.Arg2] : instr.Arg2;
                        i += jump;
                    }
                    else
                    {
                        i++;
                    }
                    break;
            }
        }
    }
}
