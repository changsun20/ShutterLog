using Spectre.Console;

namespace ShutterLog;

class Visualize
{
    public static void VisualizeAll(List<Image> images)
    {
        AnsiConsole.Write(new Rule("ShutterLog Report").RuleStyle("bold").Centered());

        var focalLengthPanel = buildPanel(
            Stat.GetFocalLengthStat(images),
            "Focal Length",
            Color.DodgerBlue1
        );
        var exposureTimePanel = buildPanel(
            Stat.GetExposureTimeStat(images),
            "Exposure Time",
            Color.SteelBlue
        );
        var fNumberPanel = buildPanel(Stat.GetFNumberStat(images), "F-Number", Color.CadetBlue);
        var isoPanel = buildPanel(Stat.GetISOStat(images), "ISO", Color.DarkCyan);

        var grid = buildGrid(focalLengthPanel, exposureTimePanel, fNumberPanel, isoPanel);

        AnsiConsole.Write(grid);
        AnsiConsole.MarkupLine($"  [grey]Total photos:[/] [bold]{images.Count}[/]");
    }

    private static Panel buildPanel(List<(string, int)> hist, string header, Color color)
    {
        var chart = new BarChart();
        foreach (var (label, count) in hist)
        {
            chart.AddItem(label, count, color);
        }
        var panel = new Panel(chart)
            .Header(header, Justify.Center)
            .RoundedBorder()
            .BorderColor(color);
        return panel;
    }

    private static Grid buildGrid(Panel panel1, Panel panel2, Panel panel3, Panel panel4)
    {
        var grid = new Grid().AddColumns(2).AddRow(panel1, panel2).AddRow(panel3, panel4);
        return grid;
    }
}
