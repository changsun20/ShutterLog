using System.CommandLine;

namespace ShutterLog;

class Commands
{
    public static ParseResult ParseArgs(string[] args)
    {
        RootCommand rootCommand = new("Sample app for System.CommandLine");
        Command checkCommand = buildCheckCommand();
        rootCommand.Subcommands.Add(checkCommand);

        return rootCommand.Parse(args);
    }

    private static Command buildCheckCommand()
    {
        Command checkCommand = new(
            "check",
            "Generate metadata statistics report for the given path"
        );

        Argument<string> pathArgument = new("path")
        {
            Description = "Path to the directory of photos",
            DefaultValueFactory = _ => ".",
        };
        checkCommand.Arguments.Add(pathArgument);

        var recursiveOption = new Option<bool>("--recursive", "-r")
        {
            Description = "Recurse reading photos into subdirectories",
        };
        checkCommand.Options.Add(recursiveOption);

        var extensionsOption = new Option<string[]>("--extensions", "-e")
        {
            Description = "File extensions to include",
            AllowMultipleArgumentsPerToken = true,
            DefaultValueFactory = _ => [".jpg"],
        };
        checkCommand.Options.Add(extensionsOption);

        checkCommand.SetAction(parseResult =>
            checkAction(
                parseResult.GetValue(pathArgument),
                parseResult.GetValue(recursiveOption),
                parseResult.GetValue(extensionsOption)
            )
        );

        return checkCommand;
    }

    private static void checkAction(string path, bool isRecursive, string[] extensionsOption)
    {
        var images = FileUtils.ReadImages(path, isRecursive, extensionsOption);
        Visualize.VisualizeAll(images);
    }
}
