using System.Net.NetworkInformation;

namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class CompressionTests : SnapshotTestingBase
{
    [DataRow(CMP_FORMAT.BC1, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC2, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC3, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC4, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC5, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC7, "Resources/rainbow.png")]
    [DataTestMethod]
    public async Task TestCompression(CMP_FORMAT targetFormat, string inputFileRelativePath)
    {
        var sourceFormat = CMP_FORMAT.RGBA_8888;

        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath);
        Assert.AreEqual(sourceFormat, mipSetIn.format);

        var compressOptions = new KernelOptions()
        {
            format = targetFormat,
            quality = 0.9f,
            threads = 0,
            srcformat = sourceFormat
        };

        CMP_MipSet mipSetOut = new();

        var cmpStatus = FrameworkNativeMethods.CMP_ProcessTexture(mipSetIn, mipSetOut, compressOptions, IntPtr.Zero);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        await SnapshotUtilities.SaveVerifyDelete("Out/cheese.dds", mipSetOut);
    }

}
