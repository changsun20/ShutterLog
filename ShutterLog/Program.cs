using System.CommandLine;

namespace ShutterLog;

class Program
{
    static int Main(string[] args)
    {
        ParseResult parseResult = Commands.ParseArgs(args);
        return parseResult.Invoke();
    }

    public static List<Image> ReadFile(string path)
    {
        var files = System.IO.Directory.EnumerateFiles(path, "*.JPG", SearchOption.AllDirectories);
        List<Image> images = new([]);
        foreach (var file in files)
        {
            Image image = MetadataUtils.GetMetadata(file);
            images.Add(image);
            // Console.WriteLine(file);
            // Console.WriteLine(image.ToString());
        }

        return images;
    }
}
