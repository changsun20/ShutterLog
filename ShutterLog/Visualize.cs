using Spectre.Console;

namespace ShutterLog;

class Visualize
{
    public static void DrawFocalLengthHistogram(List<(string, int)> hist)
    {
        var chart = new BarChart();
        foreach (var (label, count) in hist)
        {
            chart.AddItem(label, count);
        }
        AnsiConsole.Write(chart);
    }
}
