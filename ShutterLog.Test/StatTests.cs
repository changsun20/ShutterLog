namespace ShutterLog.Test;

public class StatTests
{
    [Fact]
    public void GetFocalLengthStat_MixedInputs_ReturnsCorrectBuckets()
    {
        var images = new List<Image>
        {
            new Image(10, 0, 0, 0),
            new Image(15, 0, 0, 0),
            new Image(35, 0, 0, 0),
            new Image(500, 0, 0, 0),
        };

        var result = Stat.GetFocalLengthStat(images);

        Assert.Equal(8, result.Count);

        Assert.Equal(2, result[0].Count);
        Assert.Equal(0, result[1].Count);
        Assert.Equal(1, result[2].Count);
        Assert.Equal(1, result[7].Count);

        Assert.Equal("<=20 mm", result[0].Label);
        Assert.Equal(">400 mm", result[7].Label);
    }
}
