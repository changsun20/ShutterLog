namespace ShutterLog;

class Stat
{
    public static List<(string Label, int Count)> GetFocalLengthStat(List<Image> images)
    {
        var counts = new int[6];
        foreach (var image in images)
        {
            var idx = image.FocalLength switch
            {
                < 24 => 0,
                < 35 => 1,
                < 50 => 2,
                < 85 => 3,
                < 135 => 4,
                _ => 5,
            };
            counts[idx]++;
        }

        string[] labels = ["<24 mm", "24~34 mm", "35~49 mm", "50~84 mm", "85~134 mm", ">135 mm"];

        var hist = new List<(string, int)>();
        for (var i = 0; i < 6; i++)
        {
            hist.Add((labels[i], counts[i]));
        }
        return hist;
    }
}
