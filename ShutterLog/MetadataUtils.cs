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
                    }
                    if (tag.Name == "ISO Speed Ratings")
                    {
                        ISO = ParseISO(description);
                    }
                    if (tag.Name == "Exposure Time")
                    {
                        ExposureTimeDenominator = ParseExposureTimeDenominator(description);
                    }
                    if (tag.Name == "F-Number")
                    {
                        FNumber = ParseFNumber(description);
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
        // e.g. 1/60 s
        if (TagDescription.Contains('/'))
        {
            return int.Parse(TagDescription.Split("/")[1].Split(" ")[0]);
        }
        // e.g. 0.02 s
        else
        {
            double time = double.Parse(TagDescription.Split(" ")[0]);
            int denominator = int.Parse($"{1 / time}".Split(".")[0]);
            return denominator;
        }
    }

    private static double ParseFNumber(string TagDescription)
    {
        // e.g. f/1.8
        return double.Parse(TagDescription.Split("/")[1]);
    }

    private static double ParseFocalLength(string TagDescription)
    {
        // e.g. 50 mm
        return double.Parse(TagDescription.Split(" ")[0]);
    }
}
