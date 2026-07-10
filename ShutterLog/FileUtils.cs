namespace ShutterLog;

class FileUtils
{
    public static List<Image> ReadImages(string path, bool isRecursive, string[] extensions)
    {
        try
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
                    Image? image = MetadataUtils.GetMetadata(file);
                    if (image is not null)
                    {
                        images.Add(image);
                    }
                }
            }

            return images;
        }
        catch (DirectoryNotFoundException)
        {
            Console.Error.WriteLine(
                $"Warning: Failed to read directory {path}. Please check if the path is correct."
            );
            return [];
        }
        catch (UnauthorizedAccessException)
        {
            Console.Error.WriteLine(
                $"Warning: Failed to read directory {path}. Please check if have the access to it."
            );
            return [];
        }
        catch (IOException)
        {
            Console.Error.WriteLine(
                $"Warning: Failed to read directory {path}. An IO error occurred."
            );
            return [];
        }
    }
}
