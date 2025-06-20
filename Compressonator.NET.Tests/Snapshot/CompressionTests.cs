namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class CompressionTests : SnapshotTestingBase
{
    [DataRow(CMP_FORMAT.BC1, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC2, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC3, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC4, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC5, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC7, "Resources/rainbow.png", 0.05f, 1)]
    [DataRow(CMP_FORMAT.BC6H, "Resources/rainbow.png", 0.05f, 1)]
    [DataRow(CMP_FORMAT.BC7, "Resources/rainbow.png", 0.05f, 2)]
    [DataRow(CMP_FORMAT.BC6H, "Resources/rainbow.png", 0.05f, 2)]
    [DataTestMethod]
    public async Task TestCompression(CMP_FORMAT targetFormat, string inputFileRelativePath, float quality = 0.9f, int maxThreads = 0)
    {
        var expectedFormat = CMP_FORMAT.RGBA_8888;

        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath);

        Assert.AreEqual(CMP_ERROR.CMP_OK, res);
        Assert.AreEqual(expectedFormat, mipSetIn.format);

        var compressOptions = new KernelOptions()
        {
            format = targetFormat,
            quality = quality,
            threads = maxThreads,
            srcformat = expectedFormat
        };

        CMP_MipSet mipSetOut = new();

        var cmpStatus = FrameworkNativeMethods.CMP_ProcessTexture(mipSetIn, mipSetOut, compressOptions, IntPtr.Zero);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        await SnapshotUtilities.SaveVerifyDelete(mipSetOut);

        FrameworkNativeMethods.CMP_FreeMipSet(mipSetIn);
    }

}
