using MetadataExtractor;

namespace ShutterLog;

class MetadataUtils
{
    public static Image GetMetadata(string file)
    {
        var directories = ImageMetadataReader.ReadMetadata(file);

        int iso = 0;
        int exposureTimeDenominator = 0;
        double fNumber = 0.0;
        double focalLength = 0.0;
        foreach (var directory in directories)
        {
            if (directory.Name == "Exif SubIFD")
            {
                foreach (var tag in directory.Tags)
                {
                    var description = tag.Description;
                    if (tag.Name == "Focal Length")
                    {
                        focalLength = ParseFocalLength(description);
                    }
                    if (tag.Name == "ISO Speed Ratings")
                    {
                        iso = ParseISO(description);
                    }
                    if (tag.Name == "Exposure Time")
                    {
                        exposureTimeDenominator = ParseExposureTimeDenominator(description);
                    }
                    if (tag.Name == "F-Number")
                    {
                        fNumber = ParseFNumber(description);
                    }
                }
            }
        }
        return new Image(focalLength, exposureTimeDenominator, fNumber, iso);
    }

    private static int ParseISO(string tagDescription)
    {
        return int.Parse(tagDescription);
    }

    private static int ParseExposureTimeDenominator(string tagDescription)
    {
        // e.g. 1/60 s
        if (tagDescription.Contains('/'))
        {
            return int.Parse(tagDescription.Split("/")[1].Split(" ")[0]);
        }
        // e.g. 0.02 s
        else
        {
            double time = double.Parse(tagDescription.Split(" ")[0]);
            int denominator = int.Parse($"{1 / time}".Split(".")[0]);
            return denominator;
        }
    }

    private static double ParseFNumber(string tagDescription)
    {
        // e.g. f/1.8
        return double.Parse(tagDescription.Split("/")[1]);
    }

    private static double ParseFocalLength(string tagDescription)
    {
        // e.g. 50 mm
        return double.Parse(tagDescription.Split(" ")[0]);
    }
}
