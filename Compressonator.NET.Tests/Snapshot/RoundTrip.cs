namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class RoundTrip : SnapshotTestingBase
{
    [TestMethod]
    public async Task BasicRoundTrip()
    {
        string sourceFile = "Resources/rainbow.png";
        string targetFile = "Out/rainbow.png";

        var (res,set) = SnapshotUtilities.Load(sourceFile);

        await SnapshotUtilities.SaveVerifyDelete(targetFile, set);
    }
}
