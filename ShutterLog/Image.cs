namespace ShutterLog;

public class Image(double focalLength, double exposureTime, double fNumber, int iso)
{
    public double FocalLength { get; init; } = focalLength;
    public double ExposureTime { get; init; } = exposureTime;
    public double FNumber { get; init; } = fNumber;
    public int ISO { get; init; } = iso;

    public override string ToString()
    {
        return $"""
Focal Length: {FocalLength} mm
Exposure Time: {ExposureTime} s
F-Number: f/{FNumber}
ISO: {ISO}
""";
    }
}
