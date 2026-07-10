using Spectre.Console;

namespace ShutterLog;

class Visualize
{
    public static void VisualizeAll(List<Image> images)
    {
        var focalLengthPanel = buildPanel(Stat.GetFocalLengthStat(images), "Focal Length");
        var exposureTimePanel = buildPanel(Stat.GetExposureTimeStat(images), "Exposure Time");
        var fNumberPanel = buildPanel(Stat.GetFNumberStat(images), "F-Number");
        var isoPanel = buildPanel(Stat.GetISOStat(images), "ISO");

        var grid = buildGrid(focalLengthPanel, exposureTimePanel, fNumberPanel, isoPanel);

        AnsiConsole.Write(grid);
    }

    private static Panel buildPanel(List<(string, int)> hist, string header)
    {
        var chart = new BarChart();
        foreach (var (label, count) in hist)
        {
            chart.AddItem(label, count);
        }
        var panel = new Panel(chart).Header(header, Justify.Center).RoundedBorder();
        return panel;
    }

    private static Grid buildGrid(Panel panel1, Panel panel2, Panel panel3, Panel panel4)
    {
        var grid = new Grid().AddColumns(2).AddRow(panel1, panel2).AddRow(panel3, panel4);
        return grid;
    }
}
