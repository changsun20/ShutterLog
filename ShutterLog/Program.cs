using System.CommandLine;

namespace ShutterLog;

class Program
{
    static int Main(string[] args)
    {
        RootCommand rootCommand = new("Sample app for System.CommandLine");

        ParseResult parseResult = rootCommand.Parse(args);
        ReadFile();
        return parseResult.Invoke();
    }

    private static void ReadFile()
    {
        var files = System.IO.Directory.EnumerateFiles(".");
        foreach (var file in files)
        {
            Console.WriteLine(file);
            if (file.EndsWith(".NEF"))
            {
                Image image = MetadataUtils.GetMetadata(file);
                image.PrintStats();
            }
        }
    }
}
