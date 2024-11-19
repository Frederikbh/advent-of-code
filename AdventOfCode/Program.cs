using System.Reflection;
using System.Text.RegularExpressions;

using AdventOfCode;
using AdventOfCode.Lib;

var solverTypes = Assembly.GetEntryAssembly()!.GetTypes()
    .Where(
        t => t.GetTypeInfo()
            .IsClass && typeof(ISolver).IsAssignableFrom(t))
    .OrderBy(t => t.FullName)
    .ToArray();

var action =
    Command(
        args,
        Args("update", "([0-9]+)/([0-9]+)"),
        m =>
        {
            var year = int.Parse(m[1]);
            var day = int.Parse(m[2]);

            return () => new Updater().Update(year, day)
                .Wait();
        }) ??
    Command(
        args,
        Args("update", "today"),
        _ =>
        {
            var dt = DateTime.UtcNow.AddHours(-5);

            if (dt is { Month: 12, Day: >= 1 and <= 25 })
            {
                return () => new Updater().Update(dt.Year, dt.Day)
                    .Wait();
            }

            throw new AocCommunicationError("Event is not active. This option works in Dec 1-25 only)");
        }) ??
    Command(
        args,
        Args("upload", "([0-9]+)/([0-9]+)"),
        m =>
        {
            var year = int.Parse(m[1]);
            var day = int.Parse(m[2]);

            return () =>
            {
                var solverType = solverTypes.First(
                    solverType =>
                        SolverExtensions.Year(solverType) == year &&
                        SolverExtensions.Day(solverType) == day);

                new Updater().Upload(GetSolvers(solverType)[0])
                    .Wait();
            };
        }) ??
    Command(
        args,
        Args("upload", "today"),
        _ =>
        {
            var dt = DateTime.UtcNow.AddHours(-5);

            if (dt is { Month: 12, Day: >= 1 and <= 25 })
            {
                var solverType = solverTypes.First(
                    solverType =>
                        SolverExtensions.Year(solverType) == dt.Year &&
                        SolverExtensions.Day(solverType) == dt.Day);

                return () =>
                    new Updater().Upload(GetSolvers(solverType)[0])
                        .Wait();
            }

            throw new AocCommunicationError("Event is not active. This option works in Dec 1-25 only)");
        }) ??
    Command(
        args,
        Args("([0-9]+)/(Day)?([0-9]+)"),
        m =>
        {
            var year = int.Parse(m[0]);
            var day = int.Parse(m[2]);
            var solverTypesSelected = solverTypes.First(
                solverType =>
                    SolverExtensions.Year(solverType) == year &&
                    SolverExtensions.Day(solverType) == day);

            return () => Runner.RunAll(GetSolvers(solverTypesSelected));
        }) ??
    Command(
        args,
        Args("[0-9]+"),
        m =>
        {
            var year = int.Parse(m[0]);
            var solverTypesSelected = solverTypes.Where(
                solverType =>
                    SolverExtensions.Year(solverType) == year);

            return () => Runner.RunAll(GetSolvers(solverTypesSelected.ToArray()));
        }) ??
    Command(
        args,
        Args("([0-9]+)/all"),
        m =>
        {
            var year = int.Parse(m[0]);
            var solverTypesSelected = solverTypes.Where(
                solverType =>
                    SolverExtensions.Year(solverType) == year);

            return () => Runner.RunAll(GetSolvers(solverTypesSelected.ToArray()));
        }) ??
    Command(args, Args("all"), _ => { return () => Runner.RunAll(GetSolvers(solverTypes)); }) ??
    Command(
        args,
        Args("today"),
        _ =>
        {
            var dt = DateTime.UtcNow.AddHours(-5);

            if (dt is { Month: 12, Day: >= 1 and <= 25 })
            {
                var solverTypesSelected = solverTypes.First(
                    solverType =>
                        SolverExtensions.Year(solverType) == dt.Year &&
                        SolverExtensions.Day(solverType) == dt.Day);

                return () =>
                    Runner.RunAll(GetSolvers(solverTypesSelected));
            }

            throw new AocCommunicationError("Event is not active. This option works in Dec 1-25 only)");
        }) ??
    Command(
        args,
        Args("calendars"),
        _ =>
        {
            return () =>
            {
                var solverTypesSelected = (
                    from solverType in solverTypes
                    group solverType by SolverExtensions.Year(solverType)
                    into g
                    orderby SolverExtensions.Year(g.First()) descending
                    select g.First()
                ).ToArray();

                var solvers = GetSolvers(solverTypesSelected);

                foreach (var solver in solvers)
                {
                    solver.SplashScreen()
                        .Show();
                }
            };
        }) ??
    (() => { Console.WriteLine(Usage.Get()); });

try
{
    action();
}
catch (AggregateException a)
{
    if (a.InnerExceptions.Count == 1 && a.InnerException is AocCommunicationError)
    {
        Console.WriteLine(a.InnerException.Message);
    }
    else
    {
        throw;
    }
}

return;

ISolver[] GetSolvers(params Type[] solvers)
{
    return solvers.Select(t => Activator.CreateInstance(t) as ISolver)
        .ToArray()!;
}

Action? Command(IReadOnlyCollection<string> args, IReadOnlyCollection<string> regexes, Func<string[], Action> parse)
{
    if (args.Count != regexes.Count)
    {
        return null;
    }

    var matches = args.Zip(regexes, (arg, regex) => new Regex("^" + regex + "$").Match(arg))
        .ToList();

    if (!matches.All(match => match.Success))
    {
        return null;
    }

    try
    {
        return parse(
            matches.SelectMany(
                    m =>
                        m.Groups.Count > 1
                            ? m.Groups.Cast<Group>()
                                .Skip(1)
                                .Select(g => g.Value)
                            :
                            [
                                m.Value
                            ]
                )
                .ToArray());
    }
    catch
    {
        return null;
    }
}

string[] Args(params string[] regex)
{
    return regex;
}

namespace AdventOfCode
{
    public class Usage
    {
        public static string Get() =>
            """
                
                > Usage: dotnet run [arguments]
                > 1) To run the solutions and admire your advent calendar:
                
                >  [year]/[day|all]      Solve the specified problems
                >  today                 Shortcut to the above
                >  [year]                Solve the whole year
                >  all                   Solve everything
                
                >  calendars             Show the calendars
                
                > 2) To start working on new problems:
                > login to https://adventofcode.com, then copy your session cookie, and export 
                > it in your console like this
                
                >  export SESSION=73a37e9a72a...
                
                > then run the app with
                
                >  update [year]/[day]   Prepares a folder for the given day, updates the input,
                >                        the readme and creates a solution template.
                >  update today          Shortcut to the above.
                
                > 3) To upload your answer:
                > set up your SESSION variable as above.
                
                >  upload [year]/[day]   Upload the answer for the selected year and day.
                >  upload today          Shortcut to the above.
                
                > 
             """.StripMargin("> ");
    }
}
