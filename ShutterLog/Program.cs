using System.CommandLine;

namespace ShutterLog;

class Program
{
    static int Main(string[] args)
    {
        ParseResult parseResult = Commands.ParseArgs(args);
        return parseResult.Invoke();
    }
}
