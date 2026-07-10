namespace ShutterLog;

class Stat
{
    public static List<(string Label, int Count)> GetFocalLengthStat(List<Image> images)
    {
        Func<Image, double> selector = img => img.FocalLength;
        Func<double, int> bucket = v =>
            v switch
            {
                <= 20 => 0,
                <= 28 => 1,
                <= 38 => 2,
                <= 60 => 3,
                <= 105 => 4,
                <= 200 => 5,
                <= 400 => 6,
                _ => 7,
            };
        string[] labels =
        [
            "<=20 mm",
            "21-28 mm",
            "29-38 mm",
            "39-60 mm",
            "61-105 mm",
            "106-200 mm",
            "201-400 mm",
            ">400 mm",
        ];

        return BuildStat(images, selector, bucket, labels);
    }

    public static List<(string Label, int Count)> GetExposureTimeStat(List<Image> images)
    {
        Func<Image, int> selector = img => img.ExposureTimeDenominator;
        Func<int, int> bucket = v =>
            v switch
            {
                <= 15 => 0,
                <= 30 => 1,
                <= 60 => 2,
                <= 125 => 3,
                <= 250 => 4,
                <= 500 => 5,
                <= 1000 => 6,
                _ => 7,
            };
        string[] labels =
        [
            ">=1/15 s",
            "1/15-1/30 s",
            "1/30-1/60 s",
            "1/60-1/125 s",
            "1/125-1/250 s",
            "1/250-1/500 s",
            "1/500-1/1000 s",
            "<1/1000 s",
        ];

        return BuildStat(images, selector, bucket, labels);
    }

    public static List<(string Label, int Count)> GetFNumberStat(List<Image> images)
    {
        Func<Image, double> selector = img => img.FNumber;
        Func<double, int> bucket = v =>
            v switch
            {
                <= 1.4 => 0,
                <= 2.8 => 1,
                <= 4 => 2,
                <= 5.6 => 3,
                <= 8 => 4,
                <= 11 => 5,
                <= 16 => 6,
                _ => 7,
            };
        string[] labels =
        [
            "<= f/1.4",
            "f/1.4-2.8",
            "f/2.8-4",
            "f/4-5.6",
            "f/5.6-8",
            "f/8-11",
            "f/11-16",
            ">f/16",
        ];

        return BuildStat(images, selector, bucket, labels);
    }

    public static List<(string Label, int Count)> GetISOStat(List<Image> images)
    {
        Func<Image, int> selector = img => img.ISO;
        Func<int, int> bucket = v =>
            v switch
            {
                <= 100 => 0,
                <= 200 => 1,
                <= 400 => 2,
                <= 800 => 3,
                <= 1600 => 4,
                <= 3200 => 5,
                <= 6400 => 6,
                _ => 7,
            };
        string[] labels =
        [
            "<=100",
            "100-200",
            "200-400",
            "400-800",
            "800-1600",
            "1600-3200",
            "3200-6400",
            ">6400",
        ];

        return BuildStat(images, selector, bucket, labels);
    }

    private static List<(string Label, int Count)> BuildStat<T>(
        List<Image> images,
        Func<Image, T> selector,
        Func<T, int> bucket,
        string[] labels
    )
    {
        var counts = new int[labels.Length];
        foreach (var image in images)
        {
            var idx = bucket(selector(image));
            counts[idx]++;
        }

        var hist = new List<(string, int)>();
        for (var i = 0; i < labels.Length; i++)
        {
            hist.Add((labels[i], counts[i]));
        }
        return hist;
    }
}
