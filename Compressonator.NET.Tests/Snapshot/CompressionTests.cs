
namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class CompressionTests : SnapshotTestingBase
{
    public const CMP_FORMAT DEFAULT_SOURCE_FORMAT = CMP_FORMAT.RGBA_8888;

    public const float DEFAULT_BC67COMPRESSION_QUALITY = 0.05f;
    public const float RESONITE_BC67COMPRESSION_QUALITY = 0.6f;

    [DataRow(CMP_FORMAT.BC1, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC2, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC3, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC4, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC5, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]

    // BC6H at default quality
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/squares.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", DEFAULT_BC67COMPRESSION_QUALITY)]

    // BC6H at the quality Resonite used to use - These are generated but disabled by default, because they take double the time as 0.05f
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/squares.png", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", RESONITE_BC67COMPRESSION_QUALITY)]

    // BC7 at default quality
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/squares.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png", DEFAULT_BC67COMPRESSION_QUALITY)]

    // BC7 at the quality Resonite used to use, disabled, these took > 10 minutes
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/squares.jpg", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataTestMethod]
    public async Task TestCompression(
        CMP_FORMAT targetFormat,
        CMP_FORMAT sourceFormat,
        string inputFileRelativePath,
        float quality = 0.9f,
        int maxThreads = 0)
    {
        VerifySettings settings = new VerifySettings();

        if (targetFormat == CMP_FORMAT.BC7 || targetFormat == CMP_FORMAT.BC6H)
        {
            if (quality > DEFAULT_BC67COMPRESSION_QUALITY)
                Assert.Inconclusive($"BC6H and BC7 Tests, at a higher quality than {DEFAULT_BC67COMPRESSION_QUALITY} are disabled by default due to how long they take.");

            // see: https://github.com/Yellow-Dog-Man/Compressonator.NET/issues/20
            settings.UniqueForOSPlatform();
        }

        // see: https://github.com/Yellow-Dog-Man/Compressonator.NET/issues/21
        if (targetFormat == CMP_FORMAT.BC2 && inputFileRelativePath == "Resources/rainbow.png")
            settings.UniqueForOSPlatform();


        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath, targetFormat, sourceFormat);

        CMP_MipSet mipSetOut = new();
        CMP_ERROR cmpStatus = CMP_ERROR.CMP_OK;
        var options = new CMP_CompressOptions()
        {
            destFormat = targetFormat,
            sourceFormat = sourceFormat,
            quality = quality,
            numThreads = (uint)maxThreads
        };

        cmpStatus = SDK_NativeMethods.CMP_ConvertMipTexture(mipSetIn, mipSetOut, options);

        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Conversion process must succeed");

        await SnapshotUtilities.SaveVerifyDelete(mipSetOut,settings:settings);
    }

    [DataRow(CMP_FORMAT.BC1, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC2, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC3, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC4, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC5, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]

    // BC 6H and BC7 are blocked in CMP_ProcessTexture due to: https://github.com/Yellow-Dog-Man/compressonator/issues/10
    // [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    // [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataTestMethod]
    public async Task TestProcessTexture(CMP_FORMAT targetFormat,
        CMP_FORMAT sourceFormat,
        string inputFileRelativePath,
        float quality = 0.9f,
        uint maxThreads = 0)
    {
        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath, targetFormat, sourceFormat);

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

