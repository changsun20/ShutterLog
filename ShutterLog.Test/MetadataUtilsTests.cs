namespace ShutterLog.Test;

public class MetadataUtilsTests
{
    [Theory]
    [InlineData("800", 800)]
    [InlineData("100", 100)]
    [InlineData("0", 0)]
    [InlineData("Not a number", 0)]
    [InlineData(null, 0)]
    public void ParseISO_VariousInput_ReturnExpected(string? input, int expected)
    {
        int result = MetadataUtils.ParseISO(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("f/1.4", 1.4)]
    [InlineData("f/12", 12)]
    [InlineData("Not a number", 0)]
    [InlineData(null, 0)]
    public void ParseFNumber_VariousInput_ReturnExpected(string? input, double expected)
    {
        double result = MetadataUtils.ParseFNumber(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2 s", 2)]
    [InlineData("0.02 s", 0.02)]
    [InlineData("1/50 s", 0.02)]
    [InlineData("Not a number", 0)]
    [InlineData(null, 0)]
    public void ParseExposureTime_VariousInput_ReturnExpected(string? input, double expected)
    {
        double result = MetadataUtils.ParseExposureTime(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("18 mm", 18)]
    [InlineData("900 mm", 900)]
    [InlineData("Not a number", 0)]
    [InlineData(null, 0)]
    public void ParseFocalLength_VariousInput_ReturnExpected(string? input, double expected)
    {
        double result = MetadataUtils.ParseFocalLength(input);
        Assert.Equal(expected, result);
    }
}
