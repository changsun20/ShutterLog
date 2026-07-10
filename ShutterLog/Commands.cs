using System.CommandLine;

namespace ShutterLog;

class Commands
{
    public static ParseResult ParseArgs(string[] args)
    {
        RootCommand rootCommand = new("Sample app for System.CommandLine");

        Command checkCommand = new(
            "check",
            "Generate metadata statistics report for the given path"
        );
        rootCommand.Subcommands.Add(checkCommand);

        Argument<string> pathArgument = new("path")
        {
            Description = "path",
            DefaultValueFactory = parseResult => ".",
        };
        checkCommand.Arguments.Add(pathArgument);

        checkCommand.SetAction(parseResult => checkAction(parseResult.GetValue(pathArgument)));

        return rootCommand.Parse(args);
    }

    private static void checkAction(string path)
    {
        var images = Program.ReadFile(path);
        var focalStat = Stat.GetFocalLengthStat(images);
        Visualize.DrawFocalLengthHistogram(focalStat);
    }
}
