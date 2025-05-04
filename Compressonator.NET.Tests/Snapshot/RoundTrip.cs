namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class RoundTrip : SnapshotTestingBase
{
    [TestMethod]
    public async Task BasicRoundTrip()
    {
        string sourceFile = CurrentFile.Relative("./Resources/rainbow.png");
        string targetFile = CurrentFile.Relative("./Out/rainbow.png");

        CMP_MipSet mipSetIn = new();
        var cmpStatus = FrameworkNativeMethods.CMP_LoadTexture(sourceFile, mipSetIn);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        cmpStatus = FrameworkNativeMethods.CMP_SaveTexture(targetFile, mipSetIn);

        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        await VerifyFile(targetFile);

        File.Delete(targetFile); //Clean-up
    }
}
