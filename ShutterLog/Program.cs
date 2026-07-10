namespace ShutterLog;

class Program
{
    static int Main(string[] args)
    {
        var parseResult = Commands.ParseArgs(args);
        return parseResult.Invoke();
    }
}
