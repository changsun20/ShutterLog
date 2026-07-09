using MetadataExtractor;

namespace ShutterLog;

class MetadataUtils
{
    public static Image GetMetadata(string file)
    {
        var directories = ImageMetadataReader.ReadMetadata(file);

        int ISO = 0;
        int ExposureTimeDenominator = 0;
        double FNumber = 0.0;
        double FocalLength = 0.0;
        foreach (var directory in directories)
        {
            if (directory.Name == "Exif SubIFD")
            {
                foreach (var tag in directory.Tags)
                {
                    var description = tag.Description;
                    if (tag.Name == "Focal Length")
                    {
                        FocalLength = ParseFocalLength(description);
                        Console.WriteLine(FocalLength);
                    }
                    if (tag.Name == "ISO Speed Ratings")
                    {
                        ISO = ParseISO(description);
                        Console.WriteLine(ISO);
                    }
                    if (tag.Name == "Exposure Time")
                    {
                        ExposureTimeDenominator = ParseExposureTimeDenominator(description);
                        Console.WriteLine(ExposureTimeDenominator);
                    }
                    if (tag.Name == "F-Number")
                    {
                        FNumber = ParseFNumber(description);
                        Console.WriteLine(FNumber);
                    }
                }
            }
        }
        return new Image(FocalLength, ExposureTimeDenominator, FNumber, ISO);
    }

    private static int ParseISO(string TagDescription)
    {
        return int.Parse(TagDescription);
    }

    private static int ParseExposureTimeDenominator(string TagDescription)
    {
        return int.Parse(TagDescription.Split("/")[1].Split(" ")[0]);
    }

    private static double ParseFNumber(string TagDescription)
    {
        return double.Parse(TagDescription.Split("/")[1]);
    }

    private static double ParseFocalLength(string TagDescription)
    {
        return double.Parse(TagDescription.Split(" ")[0]);
    }
}
