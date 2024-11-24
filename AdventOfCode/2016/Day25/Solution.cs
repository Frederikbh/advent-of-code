using AdventOfCode.Lib;        

namespace AdventOfCode._2016.Day25;

[ProblemName("Clock Signal")]
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
    private enum OpCode { Cpy, Inc, Dec, Jnz, Tgl,
        Out
    }

    private record Instruction
    {
        public OpCode Operation;
        public int Arg1; // Can be a register index or an immediate value
        public bool Arg1IsRegister;
        public int Arg2; // For Cpy and Jnz: register index or immediate value
        public bool Arg2IsRegister;
    }

    public object PartOne(string input)
    {
        for (var i = 0;; i++)
        {
            var instructions = ParseInstructions(input);

            var registers = new int[4]; 
            registers[s_registerMap['a']] = i;

            var outputs = Execute(instructions, registers).Take(100);

            var isRepeating = true;
            var expectedBit = 0;
            foreach (var output in outputs)
            {
                if (output == expectedBit)
                {
                    expectedBit = 1 - expectedBit;
                }
                else
                {
                    isRepeating = false;
                    break;
                }
            }

            if (isRepeating)
            {
                return i;
            }
        }
    }

    public object PartTwo(string input) => 0;

    private static List<Instruction> ParseInstructions(string input)
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
                case "tgl":
                    instr.Operation = OpCode.Tgl;
                    ParseArg(parts[1], out instr.Arg1, out instr.Arg1IsRegister);
                    break;
                case "out":
                    instr.Operation = OpCode.Out;
                    ParseArg(parts[1], out instr.Arg1, out instr.Arg1IsRegister);
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

    private static IEnumerable<int> Execute(List<Instruction> instructions, int[] registers)
    {
        var i = 0;
        while (i >= 0 && i < instructions.Count)
        {
            var instr = instructions[i];

            if (i + 5 < instructions.Count &&
                instructions[i].Operation == OpCode.Inc &&
                instructions[i + 1].Operation == OpCode.Dec &&
                instructions[i + 2].Operation == OpCode.Jnz &&
                instructions[i + 3].Operation == OpCode.Dec &&
                instructions[i + 4].Operation == OpCode.Jnz)
            {
                // Fetch involved registers
                var a = instructions[i].Arg1;
                var b = instructions[i + 1].Arg1;
                var c = instructions[i + 3].Arg1;

                // Validate the first jnz (b loop)
                if (instructions[i + 2].Arg1IsRegister &&
                    instructions[i + 2].Arg1 == b &&
                    instructions[i + 2].Arg2 == -2) // Check jump range within loop
                {
                    // Validate the second jnz (c loop)
                    if (instructions[i + 4].Arg1IsRegister &&
                        instructions[i + 4].Arg1 == c &&
                        instructions[i + 4].Arg2 == -5) // Check jump range within loop
                    {
                        // Perform the multiplication directly
                        registers[a] += registers[b] * registers[c];

                        // Reset the registers used in the multiplication
                        registers[b] = 0;
                        registers[c] = 0;

                        // Skip the entire pattern
                        i += 5;
                        continue;
                    }
                }
            }

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
                case OpCode.Tgl:
                    var target = i + (instr.Arg1IsRegister ? registers[instr.Arg1] : instr.Arg1);
                    if (target >= 0 && target < instructions.Count)
                    {
                        var targetInstr = instructions[target];
                        switch (targetInstr.Operation)
                        {
                            case OpCode.Cpy:
                                targetInstr.Operation = OpCode.Jnz;
                                break;
                            case OpCode.Inc:
                                targetInstr.Operation = OpCode.Dec;
                                break;
                            case OpCode.Dec:
                                targetInstr.Operation = OpCode.Inc;
                                break;
                            case OpCode.Jnz:
                                targetInstr.Operation = OpCode.Cpy;
                                break;
                            case OpCode.Tgl:
                                targetInstr.Operation = OpCode.Inc;
                                break;
                            default:
                                throw new InvalidOperationException($"Unknown instruction: {targetInstr.Operation}");
                        }
                    }
                    i++;
                    break;
                case OpCode.Out:
                    yield return instr.Arg1IsRegister ? registers[instr.Arg1] : instr.Arg1;
                    i++;
                    break;
            }
        }

        yield break;
    }
}
