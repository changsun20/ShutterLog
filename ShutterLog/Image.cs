namespace ShutterLog;

public class Image(double focalLength, int exposureTimeDenominator, double fNumber, int iso)
{
    public double FocalLength { get; init; } = focalLength;
    public int ExposureTimeDenominator { get; init; } = exposureTimeDenominator;
    public double FNumber { get; init; } = fNumber;
    public int ISO { get; init; } = iso;

    public void PrintStats()
    {
        Console.WriteLine(
            $"""
Focal Length: {FocalLength} mm
Exposure Time: 1/{ExposureTimeDenominator} s
F-Number: f/{FNumber}
ISO: {ISO}
"""
        );
    }
}
