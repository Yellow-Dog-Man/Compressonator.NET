namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class RoundTrip : SnapshotTestingBase
{
    [TestMethod]
    public async Task BasicRoundTrip()
    {
        // Verify that IN == OUT
        string sourceFile = "Resources/rainbow.png";
        var (res,set) = SnapshotUtilities.Load(sourceFile);

        await SnapshotUtilities.SaveVerifyDelete(set, "png");
    }

    [TestMethod]
    public void CompressDecompressValidate()
    {
        // This test is very similar to our quality evaluation tests, but its designed to be faster and easier
        // Suitable for "Smoke Tests". Easier to Debug etc.
        string sourceFile = "Resources/carrots.png";
        var tmpPath = CurrentFile.Relative(SnapshotUtilities.GetFileNameForTest("png"));

        CMP_Texture distortedTexture = SnapshotUtilities.RoundTripWithCompression(sourceFile, CMP_FORMAT.BC1, 0.05f);

        SnapshotUtilities.Save(tmpPath, distortedTexture);

        File.Delete(tmpPath);
    }
}
