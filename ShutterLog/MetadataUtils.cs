using MetadataExtractor;

namespace ShutterLog;

public class MetadataUtils
{
    public static Image? GetMetadata(string file)
    {
        try
        {
            var directories = ImageMetadataReader.ReadMetadata(file);

            int iso = 0;
            double exposureTime = 0.0;
            double fNumber = 0.0;
            double focalLength = 0.0;
            foreach (var directory in directories)
            {
                if (directory.Name == "Exif SubIFD")
                {
                    foreach (var tag in directory.Tags)
                    {
                        var description = tag.Description;
                        switch (tag.Name)
                        {
                            case "Focal Length":
                                focalLength = ParseFocalLength(description);
                                break;
                            case "ISO Speed Ratings":
                                iso = ParseISO(description);
                                break;
                            case "Exposure Time":
                                exposureTime = ParseExposureTime(description);
                                break;
                            case "F-Number":
                                fNumber = ParseFNumber(description);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return new Image(focalLength, exposureTime, fNumber, iso);
        }
        catch (ImageProcessingException)
        {
            Console.Error.WriteLine(
                $"Warning: Cannot read file {file}. Please check if the photo is corrupted."
            );
            return null;
        }
        catch (IOException)
        {
            Console.Error.WriteLine($"Warning: Cannot read file {file}. An IO error occurred.");
            return null;
        }
    }

    internal static int ParseISO(string? tagDescription)
    {
        if (tagDescription is null)
        {
            return 0;
        }

        return int.TryParse(tagDescription, out int iso) ? iso : 0;
    }

    private static double ParseExposureTime(string? tagDescription)
    {
        if (tagDescription is null)
        {
            return 0.0;
        }

        var parts = tagDescription.Split("/");

        // e.g. 1/60 s
        if (parts.Length >= 2)
        {
            if (int.TryParse(parts[1].Split(" ")[0], out int denominator))
            {
                return int.TryParse(parts[0], out int numerator)
                    ? (double)numerator / (double)denominator
                    : 0.0;
            }
            else
            {
                return 0.0;
            }
        }
        // e.g. 0.02 s
        else
        {
            return double.TryParse(tagDescription.Split(" ")[0], out double time) ? time : 0.0;
        }
    }

    private static double ParseFNumber(string? tagDescription)
    {
        if (tagDescription is null)
        {
            return 0.0;
        }

        // e.g. f/1.8
        var parts = tagDescription.Split("/");

        if (parts.Length < 2)
        {
            return 0.0;
        }
        return double.TryParse(parts[1], out double fNumber) ? fNumber : 0.0;
    }

    private static double ParseFocalLength(string? tagDescription)
    {
        if (tagDescription is null)
        {
            return 0.0;
        }
        // e.g. 50 mm
        return double.TryParse(tagDescription.Split(" ")[0], out double focal) ? focal : 0.0;
    }
}
