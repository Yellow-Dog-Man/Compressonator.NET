namespace Compressonator.NET.Tests.Snapshot;

[TestCategory("Smoke")]
[TestClass]
public class RoundTrip : SnapshotTestingBase
{
    
    [TestMethod]
    [TestProperty("CI", "true")]
    public async Task BasicRoundTrip()
    {
        // Verify that IN == OUT
        string sourceFile = "Resources/rainbow.png";
        var (res,set) = SnapshotUtilities.Load(sourceFile);

        await SnapshotUtilities.SaveVerifyDelete(set, "png");
    }

    [TestMethod]
    [TestProperty("CI", "true")]
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
