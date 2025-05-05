

using System.Net.NetworkInformation;

namespace Compressonator.NET.Tests.Snapshot;

public static class SnapshotUtilities
{
    /// <summary>
    /// Standardized way to create the start of a compressonator chain for testing.
    /// </summary>
    /// <param name="relativePath"></param>
    /// <returns></returns>
    public static (CMP_ERROR, CMP_MipSet) Load(string relativePath)
    {
        var path = CurrentFile.Relative(relativePath);
        CMP_MipSet mipSetIn = new();
        var cmpStatus = FrameworkNativeMethods.CMP_LoadTexture(path, mipSetIn);
        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        UpdateMips(mipSetIn);

        return (cmpStatus, mipSetIn);
    }
    public static string GetFileNameForTest(string extension = "dds")
    {
        return Path.ChangeExtension(Path.GetRandomFileName(), extension);
    }

    public static async Task SaveVerifyDelete(CMP_MipSet set, string extension = "dds")
    {
        var path = CurrentFile.Relative(GetFileNameForTest(extension));
        var cmpStatus = FrameworkNativeMethods.CMP_SaveTexture(path, set);

        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus, "Save operation must succeed"); 

        Assert.IsTrue(File.Exists(path), $"Saved file must exist:{path}");

        VerifyResult? res = await VerifyFile(path);
        Assert.IsNotNull(res, "Verify result must be present");
        Assert.IsNull(res.Target, "Verify result must not be error");

        File.Delete(path); //Clean-up

        Assert.IsFalse(File.Exists(path), $"Saved file must be cleaned up: {path}");
    }

    public static int UpdateMips(CMP_MipSet mipSetIn)
    {
        var prevMipLevels = mipSetIn.mipLevels;
        if (mipSetIn.mipLevels > 1)
            return 0;
        
        const int requestLevel = 10;

        int nMinSize = FrameworkNativeMethods.CMP_CalcMinMipSize(mipSetIn.height, mipSetIn.width, requestLevel);
        return FrameworkNativeMethods.CMP_GenerateMIPLevels(mipSetIn, nMinSize);
    }
}
