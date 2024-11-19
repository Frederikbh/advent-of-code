using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode.Lib;

public class ProblemName : Attribute
{
    public readonly string Name;

    public ProblemName(string name)
    {
        Name = name;
    }
}

public interface ISolver
{
    object? PartOne(string input);

    object? PartTwo(string input) => null;
}

public static class SolverExtensions
{
    public static IEnumerable<object?> Solve(this ISolver solver, string input)
    {
        yield return solver.PartOne(input);

        var res = solver.PartTwo(input);

        yield return res;
    }

    public static string GetName(this ISolver solver) =>
    (
        solver
            .GetType()
            .GetCustomAttribute(typeof(ProblemName)) as ProblemName
    )?.Name ?? string.Empty;

    public static string DayName(this ISolver solver) => $"Day {solver.Day()}";

    public static int Year(this ISolver solver) => Year(solver.GetType());

    public static int Year(Type t) => int.Parse(t.FullName?.Split('.')[1][1..] ?? string.Empty);

    public static int Day(this ISolver solver) => Day(solver.GetType());

    public static int Day(Type t) => int.Parse(t.FullName?.Split('.')[2][3..] ?? string.Empty);

    public static string BaseProjectDir() => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

    public static string WorkingDir(int year) => Path.Combine(BaseProjectDir(), year.ToString());

    public static string WorkingDir(int year, int day) => Path.Combine(WorkingDir(year), "Day" + day.ToString("00"));

    public static string WorkingDir(this ISolver solver) => WorkingDir(solver.Year(), solver.Day());

    public static ISplashScreen SplashScreen(this ISolver solver)
    {
        var splashScreen = Assembly.GetEntryAssembly()
            !.GetTypes()
            .Where(
                t => t.GetTypeInfo()
                    .IsClass && typeof(ISplashScreen).IsAssignableFrom(t))
            .Single(t => Year(t) == solver.Year());

        return (ISplashScreen)Activator.CreateInstance(splashScreen)!;
    }
}

public record SolverResult(string[] Answers, string[] Errors);

public class Runner
{
    private static string GetNormalizedInput(string file)
    {
        var input = File.ReadAllText(file);

        // on Windows we have "\r\n", not sure if this causes more harm or not
        input = input.Replace("\r", "");

        if (input.EndsWith('\n'))
        {
            input = input[..^1];
        }

        return input;
    }

    public static SolverResult RunSolver(ISolver solver)
    {
        var workingDir = solver.WorkingDir();
        const string Indent = "    ";
        Write(ConsoleColor.White, $"{Indent}{solver.DayName()}: {solver.GetName()}");
        WriteLine();
        var file = Path.Combine(workingDir, "input.in");
        var refoutFile = file.Replace(".in", ".refout");
        var refout = File.Exists(refoutFile)
            ? File.ReadAllLines(refoutFile)
            : null;
        var input = GetNormalizedInput(file);
        var iLine = 0;
        var answers = new List<string>();
        var errors = new List<string>();
        var stopwatch = Stopwatch.StartNew();

        foreach (var line in solver.Solve(input))
        {
            var ticks = stopwatch.ElapsedTicks;

            if (line is OcrString ocrString)
            {
                Console.WriteLine("\n" + ocrString.St.Indent(10, true));
            }

            answers.Add(line?.ToString() ?? string.Empty);
            var (statusColor, status, err) =
                refout == null || refout.Length <= iLine ? (ConsoleColor.Cyan, "?", null) :
                refout[iLine] == line?.ToString() ? (ConsoleColor.DarkGreen, "✓", null) :
                (ConsoleColor.Red, "X",
                    $"{solver.DayName()}: In line {iLine + 1} expected '{refout[iLine]}' but found '{line}'");

            if (err != null)
            {
                errors.Add(err);
            }

            Write(statusColor, $"{Indent}  {status}");
            Console.Write($" {line} ");
            var diff = ticks * 1000.0 / Stopwatch.Frequency;

            WriteLine(
                diff > 1000 ? ConsoleColor.Red :
                diff > 500 ? ConsoleColor.Yellow :
                ConsoleColor.DarkGreen,
                $"({diff:F3} ms)"
            );
            iLine++;
            stopwatch.Restart();
        }

        return new SolverResult(answers.ToArray(), errors.ToArray());
    }

    public static void RunAll(params ISolver[] solvers)
    {
        var errors = new List<string>();

        var lastYear = -1;

        foreach (var solver in solvers)
        {
            if (lastYear != solver.Year())
            {
                solver.SplashScreen()
                    .Show();
                lastYear = solver.Year();
            }

            var result = RunSolver(solver);
            WriteLine();
            errors.AddRange(result.Errors);
        }

        WriteLine();

        if (errors.Any())
        {
            WriteLine(ConsoleColor.Red, "Errors:\n" + string.Join("\n", errors));
        }
    }

    private static void WriteLine(ConsoleColor color = ConsoleColor.Gray, string text = "") =>
        Write(color, text + "\n");

    private static void Write(ConsoleColor color = ConsoleColor.Gray, string text = "")
    {
        var c = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = c;
    }
}
