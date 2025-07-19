using Compressonator.NET.Utilities;

namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class RoundTrip : SnapshotTestingBase
{
    [TestMethod]
    public async Task BasicRoundTrip()
    {
        string sourceFile = "Resources/rainbow.png";
        var (res,set) = SnapshotUtilities.Load(sourceFile);

        await SnapshotUtilities.SaveVerifyDelete(set, "png");
    }

    [TestMethod]
    public async Task CompressDecompressValidate()
    {
        CMP_ERROR cmpStatus = CMP_ERROR.CMP_OK;

        string sourceFile = "Resources/carrots.png";
        var (res, originalSet) = SnapshotUtilities.Load(sourceFile, mipLevels:0);

        CMP_MipSet compressedSet = new();
        compressedSet.format = CMP_FORMAT.BC1;
        var options = new CMP_CompressOptions()
        {
            destFormat = compressedSet.format,
            sourceFormat = originalSet.format,
        };

        // Compress to BC1
        cmpStatus = SDK_NativeMethods.CMP_ConvertMipTexture(originalSet, compressedSet, options);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);
        
        // Extract mip level 0
        CMP_Texture mipZeroTexture = new();

        cmpStatus = SDK_NativeMethods.CMP_MipSetToTexture(compressedSet, 0, mipZeroTexture);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        // Immediately turn around and re-decompress it
        CMP_Texture decompressed = new();

        TextureUtilities.CopyDimensions(mipZeroTexture, decompressed);
        decompressed.format = SnapshotUtilities.DEFAULT_SOURCE_FORMAT;
        decompressed.transcodeFormat = SnapshotUtilities.DEFAULT_SOURCE_FORMAT;
        decompressed.AllocateDataPointer();

        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        options = new CMP_CompressOptions()
        {
            destFormat = SnapshotUtilities.DEFAULT_SOURCE_FORMAT,
        };

        cmpStatus = SDK_NativeMethods.CMP_ConvertTexture(mipZeroTexture, decompressed, options);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        // Standard Save Verify Delete
        await SnapshotUtilities.SaveVerifyDelete(decompressed, "png");
    }
}
