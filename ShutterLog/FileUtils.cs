namespace ShutterLog;

class FileUtils
{
    public static List<Image> ReadImages(string path, bool isRecursive, string[] extensions)
    {
        var extSet = new HashSet<string>();
        foreach (var extension in extensions)
        {
            extSet.Add(extension.ToLowerInvariant());
        }

        var searchOption = isRecursive
            ? SearchOption.AllDirectories
            : SearchOption.TopDirectoryOnly;

        var files = Directory.EnumerateFiles(path, "*", searchOption);
        List<Image> images = new([]);
        foreach (var file in files)
        {
            if (extSet.Contains(Path.GetExtension(file).ToLowerInvariant()))
            {
                Image image = MetadataUtils.GetMetadata(file);
                images.Add(image);
            }
        }

        return images;
    }
}
