﻿﻿
namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public class CompressionTests : SnapshotTestingBase
{
    public const CMP_FORMAT DEFAULT_SOURCE_FORMAT = SnapshotUtilities.DEFAULT_SOURCE_FORMAT;

    public const float DEFAULT_BC67COMPRESSION_QUALITY = 0.05f;
    public const float RESONITE_BC67COMPRESSION_QUALITY = 0.6f;

    [DataRow(CMP_FORMAT.BC1, DEFAULT_SOURCE_FORMAT,  "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC2, DEFAULT_SOURCE_FORMAT,  "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC3, DEFAULT_SOURCE_FORMAT,  "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC4, DEFAULT_SOURCE_FORMAT,  "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC5, DEFAULT_SOURCE_FORMAT,  "Resources/rainbow.png")]

    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png", RESONITE_BC67COMPRESSION_QUALITY)]

    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT,  "Resources/rainbow.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT,  "Resources/rainbow.png", RESONITE_BC67COMPRESSION_QUALITY)]

    // Paperclips
    [DataRow(CMP_FORMAT.BC1, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png")]
    [DataRow(CMP_FORMAT.BC2, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png")]
    [DataRow(CMP_FORMAT.BC3, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png")]
    [DataRow(CMP_FORMAT.BC4, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png")]
    [DataRow(CMP_FORMAT.BC5, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png")]

    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png", RESONITE_BC67COMPRESSION_QUALITY)]

    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/paperclips.png", RESONITE_BC67COMPRESSION_QUALITY)]

    // Carrots
    [DataRow(CMP_FORMAT.BC1, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png")]
    [DataRow(CMP_FORMAT.BC2, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png")]
    [DataRow(CMP_FORMAT.BC3, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png")]
    [DataRow(CMP_FORMAT.BC4, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png")]
    [DataRow(CMP_FORMAT.BC5, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png")]

    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png", RESONITE_BC67COMPRESSION_QUALITY)]

    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/carrots.png", RESONITE_BC67COMPRESSION_QUALITY)]

    // Misc
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/squares.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/flowers.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/helicopter.png", DEFAULT_BC67COMPRESSION_QUALITY)]

    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/squares.png", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/flowers.png", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/helicopter.png", RESONITE_BC67COMPRESSION_QUALITY)]

    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/squares.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/flowers.png", DEFAULT_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/helicopter.png", DEFAULT_BC67COMPRESSION_QUALITY)]

    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/squares.png", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/shanghai.jpg", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/flowers.png", RESONITE_BC67COMPRESSION_QUALITY)]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/helicopter.png", RESONITE_BC67COMPRESSION_QUALITY)]

    [TestProperty("CI", "true")]
    [DataTestMethod]
    public async Task TestCompression(
        CMP_FORMAT targetFormat,
        CMP_FORMAT sourceFormat,
        string inputFileRelativePath,
        float quality = 0.9f,
        int maxThreads = 0)
    {
        VerifySettings settings = new VerifySettings();

        // see: https://github.com/Yellow-Dog-Man/Compressonator.NET/issues/20
        settings.UniqueForOSPlatform();

        TestUtilities.GuardCITests(targetFormat, quality);

        //// see: https://github.com/Yellow-Dog-Man/Compressonator.NET/issues/21
        //if (targetFormat == CMP_FORMAT.BC2 && inputFileRelativePath == "Resources/rainbow.png")
        //    settings.UniqueForOSPlatform();

        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath, sourceFormat, mipLevels: 3);
        Assert.IsTrue(SDK_NativeMethods.CMP_IsValidFormat(targetFormat), "Target format must be supported by native library");

        CMP_MipSet mipSetOut = new();
        CMP_ERROR cmpStatus = CMP_ERROR.CMP_OK;
        var options = new CMP_CompressOptions()
        {
            destFormat = targetFormat,
            sourceFormat = sourceFormat,
            quality = quality,
            numThreads = (uint)maxThreads,
            encodeWidth = CMP_ComputeType.CMP_CPU
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
    [DataRow(CMP_FORMAT.BC6H, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [DataRow(CMP_FORMAT.BC7, DEFAULT_SOURCE_FORMAT, "Resources/rainbow.png")]
    [TestMethod]
    [TestProperty("CI", "true")]
    public async Task TestProcessTexture(CMP_FORMAT targetFormat,
        CMP_FORMAT sourceFormat,
        string inputFileRelativePath,
        float quality = 0.9f,
        uint maxThreads = 0)
    {
        TestUtilities.GuardCITests(targetFormat, quality);
        VerifySettings settings = new VerifySettings();
        // see: https://github.com/Yellow-Dog-Man/Compressonator.NET/issues/21
        if (targetFormat == CMP_FORMAT.BC2 && inputFileRelativePath == "Resources/rainbow.png")
            settings.UniqueForOSPlatform();

        

        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath, sourceFormat);
        Assert.IsTrue(SDK_NativeMethods.CMP_IsValidFormat(targetFormat), "Target format must be supported by native library");

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

        await SnapshotUtilities.SaveVerifyDelete(mipSetOut, settings: settings);
    }

    public bool IsSlow(CMP_FORMAT format)
    {
        switch(format)
        {
            case CMP_FORMAT.BC7:
            case CMP_FORMAT.BC6H:
                return true;
            default:
                return false;
        }
    }
}

