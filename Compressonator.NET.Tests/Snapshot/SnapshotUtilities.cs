

using System.Net.NetworkInformation;

namespace Compressonator.NET.Tests.Snapshot;

public static class SnapshotUtilities
{
    public const CMP_FORMAT DEFAULT_SOURCE_FORMAT = CMP_FORMAT.RGBA_8888;
    /// <summary>
    /// Standardized way to create the start of a compressonator chain for testing.
    /// </summary>
    /// <param name="relativePath"></param>
    /// <returns></returns>
    public static (CMP_ERROR, CMP_MipSet) Load(string relativePath, CMP_FORMAT sourceFormat = DEFAULT_SOURCE_FORMAT, int mipLevels = 3)
    {
        Assert.IsTrue(SDK_NativeMethods.CMP_IsValidFormat(sourceFormat), "Source format must be supported by native library");

        var path = CurrentFile.Relative(relativePath);
        CMP_MipSet mipSetIn = new();
        mipSetIn.maxMipLevels = mipLevels;
        mipSetIn.mipLevels = mipLevels;

        Assert.IsTrue(File.Exists(path), "Input file must exist!");

        var cmpStatus = FrameworkNativeMethods.CMP_LoadTexture(path, mipSetIn);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Load Must succeed");

        UpdateMips(mipSetIn);

        Assert.AreEqual(sourceFormat, mipSetIn.format, "Image format must match expectations");

        return (cmpStatus, mipSetIn);
    }
    public static string GetFileNameForTest(string extension = "dds", string extraInfo = "")
    {
        var randomName = Path.GetRandomFileName();
        var path = Path.GetFileNameWithoutExtension(randomName);
        var finalPath = $"{path}{extraInfo}.{extension}";
        return Path.Join("Out", finalPath);
    }

    public static async Task SaveVerifyDelete(CMP_Texture texture, string extension = "png", VerifySettings? settings = null)
    {
        var path = CurrentFile.Relative(GetFileNameForTest(extension));
        var cmpStatus = FrameworkNativeMethods.CMP_SaveTexture(path, texture);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Save operation must succeed");

        Assert.IsTrue(File.Exists(path), $"Saved file must exist:{path}");

        await VerifyDelete(settings, path);
    }

    public static async Task SaveVerifyDelete(CMP_MipSet set, string extension = "dds", VerifySettings? settings = null)
    {
        var path = CurrentFile.Relative(GetFileNameForTest(extension));
        var cmpStatus = FrameworkNativeMethods.CMP_SaveTexture(path, set);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Save operation must succeed");

        Assert.IsTrue(File.Exists(path), $"Saved file must exist:{path}");

        await VerifyDelete(settings, path);
    }

    private static async Task<VerifyResult> VerifyDelete(VerifySettings? settings, String path)
    {
        VerifyResult res;
        if (settings != null)
            res = await VerifyFile(path, settings);
        else
            res = await VerifyFile(path);

        File.Delete(path); //Clean-up
        Assert.IsFalse(File.Exists(path), $"Saved file must be cleaned up: {path}");

        Assert.IsNotNull(res, "Verify result must be present");
        Assert.IsNull(res.Target, "Verify result must not be error");
        return res;
    }

    public static void Save(string path, CMP_Texture texture)
    {
        CMP_ERROR cmpStatus = FrameworkNativeMethods.CMP_SaveTexture(path, texture);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Save operation must succeed");
        Assert.IsTrue(File.Exists(path), $"Saved file must exist: {path}");
    }

    /// <summary>
    /// Takes an input file, compresses it with <paramref name="targetFormat"/> at <paramref name="quality"/>.
    /// Then immediately decompresses it back to the source format.
    /// </summary>
    /// <param name="inputFileRelativePath"></param>
    /// <param name="targetFormat"></param>
    /// <param name="quality"></param>
    /// <returns></returns>
    public static CMP_Texture RoundTripWithCompression(string inputFileRelativePath, CMP_FORMAT targetFormat, float quality)
    {
        CMP_ERROR cmpStatus = CMP_ERROR.CMP_OK;

        // Load original
        var (res, mipSetIn) = SnapshotUtilities.Load(inputFileRelativePath);

        // Compress to target format and quality
        CMP_MipSet compressedSet = new();

        var options = new CMP_CompressOptions()
        {
            destFormat = targetFormat,
            sourceFormat = SnapshotUtilities.DEFAULT_SOURCE_FORMAT,
            quality = quality,
            encodeWidth = CMP_ComputeType.CMP_CPU,
            miplevels = 0
        };
        cmpStatus = SDK_NativeMethods.CMP_ConvertMipTexture(mipSetIn, compressedSet, options);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Compression process must succeed");

        CMP_MipSet mipSetOut = new();

        // Extract mip level 0
        CMP_Texture mipZeroTexture = new();

        cmpStatus = SDK_NativeMethods.CMP_MipSetToTexture(compressedSet, 0, mipZeroTexture);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        // De-compress mip 0
        options = new CMP_CompressOptions()
        {
            destFormat = SnapshotUtilities.DEFAULT_SOURCE_FORMAT,
            sourceFormat = targetFormat,
        };

        CMP_Texture decompressed = new();
        decompressed.CopyDimensionsFrom(mipZeroTexture);
        decompressed.format = SnapshotUtilities.DEFAULT_SOURCE_FORMAT;
        decompressed.transcodeFormat = SnapshotUtilities.DEFAULT_SOURCE_FORMAT;
        decompressed.AllocateDataPointer();

        options = new CMP_CompressOptions()
        {
            destFormat = SnapshotUtilities.DEFAULT_SOURCE_FORMAT,
        };

        cmpStatus = SDK_NativeMethods.CMP_ConvertTexture(mipZeroTexture, decompressed, options);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Decompression must succeeed");

        // Return final Decompressed Texture
        return decompressed;
    }

    public static int UpdateMips(CMP_MipSet mipSetIn)
    {
        // Already has mips
        if (mipSetIn.mipLevels > 1)
            return 0;
        
        const int requestLevel = 3;

        int nMinSize = FrameworkNativeMethods.CMP_CalcMinMipSize(mipSetIn.height, mipSetIn.width, requestLevel);
        return FrameworkNativeMethods.CMP_GenerateMIPLevels(mipSetIn, nMinSize);
    }
}
