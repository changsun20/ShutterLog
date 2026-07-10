namespace ShutterLog;

class FileUtils
{
    public static List<Image> ReadFile(string path, bool isRecursive)
    {
        var searchOption = isRecursive switch
        {
            true => SearchOption.AllDirectories,
            false => SearchOption.TopDirectoryOnly,
        };
        var files = System.IO.Directory.EnumerateFiles(path, "*.JPG", searchOption);
        List<Image> images = new([]);
        foreach (var file in files)
        {
            Image image = MetadataUtils.GetMetadata(file);
            images.Add(image);
        }

        return images;
    }
}
