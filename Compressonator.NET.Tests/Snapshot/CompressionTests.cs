namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class CompressionTests : SnapshotTestingBase
{
    public const float DEFAULT_BC67COMPRESSION = 0.05f;
    public const float RESONITE_BC67COMPRESSION = 0.6f;

    [DataRow(CMP_FORMAT.BC1, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC2, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC3, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC4, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC5, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]

    // BC6H at default quality
    [DataRow(CMP_FORMAT.BC6H, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png", DEFAULT_BC67COMPRESSION)]
    [DataRow(CMP_FORMAT.BC6H, CMP_FORMAT.RGBA_8888, "Resources/squares.png", DEFAULT_BC67COMPRESSION)]
    [DataRow(CMP_FORMAT.BC6H, CMP_FORMAT.RGBA_8888, "Resources/shanghai.jpg", DEFAULT_BC67COMPRESSION)]

    // BC6H at the quality Resonite used to use - These are generated but disabled by default, because they take double the time as 0.05f
    //[DataRow(CMP_FORMAT.BC6H, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png", RESONITE_BC67COMPRESSION)]
    //[DataRow(CMP_FORMAT.BC6H, CMP_FORMAT.RGBA_8888, "Resources/squares.png", RESONITE_BC67COMPRESSION)]
    //[DataRow(CMP_FORMAT.BC6H, CMP_FORMAT.RGBA_8888, "Resources/shanghai.jpg", RESONITE_BC67COMPRESSION)]

    // BC7 at default quality
    [DataRow(CMP_FORMAT.BC7, CMP_FORMAT.RGBA_8888, "Resources/shanghai.jpg", DEFAULT_BC67COMPRESSION)]
    [DataRow(CMP_FORMAT.BC7, CMP_FORMAT.RGBA_8888, "Resources/squares.png", DEFAULT_BC67COMPRESSION)]
    [DataRow(CMP_FORMAT.BC7, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png", DEFAULT_BC67COMPRESSION)]

    // BC7 at the quality Resonite used to use, disabled, these took > 10 minutes
    //[DataRow(CMP_FORMAT.BC7, CMP_FORMAT.RGBA_8888, "Resources/shanghai.jpg", RESONITE_BC67COMPRESSION)]
    //[DataRow(CMP_FORMAT.BC7, CMP_FORMAT.RGBA_8888, "Resources/squares.jpg", RESONITE_BC67COMPRESSION)]
    //[DataRow(CMP_FORMAT.BC7, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png", RESONITE_BC67COMPRESSION)]
    [DataTestMethod]
    public async Task TestCompression(
        CMP_FORMAT targetFormat,
        CMP_FORMAT sourceFormat,
        string inputFileRelativePath,
        float quality = 0.9f,
        int maxThreads = 0)
    {
        var expectedFormat = sourceFormat;

        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath);

        Assert.AreEqual(CMP_ERROR.CMP_OK, res, "Image must load");
        Assert.AreEqual(expectedFormat, mipSetIn.format, "Image format must match expectations");

        CMP_MipSet mipSetOut = new();
        CMP_ERROR cmpStatus = CMP_ERROR.CMP_OK;
        var options = new CMP_CompressOptions()
        {
            destFormat = targetFormat,
            sourceFormat = sourceFormat,
            quality = quality,
            numThreads = (uint)maxThreads,
        };

        cmpStatus = SDK_NativeMethods.CMP_ConvertMipTexture(mipSetIn, mipSetOut, options, IntPtr.Zero);

        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Conversion process must succeed");

        await SnapshotUtilities.SaveVerifyDelete(mipSetOut);
    }

    [DataRow(CMP_FORMAT.BC1, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC2, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC3, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC4, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC5, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]

    // BC 6H and BC7 are blocked in CMP_ProcessTexture due to: https://github.com/Yellow-Dog-Man/compressonator/issues/10
    // [DataRow(CMP_FORMAT.BC6H, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    // [DataRow(CMP_FORMAT.BC7, CMP_FORMAT.RGBA_8888, "Resources/rainbow.png")]
    [DataTestMethod]
    public async Task TestProcessTexture(CMP_FORMAT targetFormat,
        CMP_FORMAT sourceFormat,
        string inputFileRelativePath,
        float quality = 0.9f,
        uint maxThreads = 0)
    {
        var expectedFormat = sourceFormat;

        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath);

        Assert.AreEqual(CMP_ERROR.CMP_OK, res);
        Assert.AreEqual(expectedFormat, mipSetIn.format);

        CMP_MipSet mipSetOut = new();
        CMP_ERROR cmpStatus = CMP_ERROR.CMP_OK;

        var compressOptions = new KernelOptions()
        {
            format = targetFormat,
            quality = quality,
            threads = (int)maxThreads,
            srcformat = mipSetIn.format,
            encodeWith = CMP_ComputeType.CMP_HPC,
        };
        cmpStatus = FrameworkNativeMethods.CMP_ProcessTexture(mipSetIn, mipSetOut, compressOptions, IntPtr.Zero);

        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        await SnapshotUtilities.SaveVerifyDelete(mipSetOut);
    }
}
