namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class RoundTrip : SnapshotTestingBase
{
    [TestMethod]
    public async Task BasicRoundTrip()
    {
        string sourceFile = "Resources/rainbow.png";
        var (res,set) = SnapshotUtilities.Load(sourceFile);

        Assert.AreEqual(CMP_FORMAT.RGBA_8888, set.format);

        await SnapshotUtilities.SaveVerifyDelete(set, "png");
    }
}
