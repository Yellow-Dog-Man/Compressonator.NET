

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

        return (cmpStatus, mipSetIn);
    }

    public static async Task SaveVerifyDelete(string relativePath, CMP_MipSet set)
    {
        var path = CurrentFile.Relative(relativePath);
        var cmpStatus = FrameworkNativeMethods.CMP_SaveTexture(path, set);

        Assert.AreEqual(CMP_ERROR.CMP_OK, cmpStatus);

        Assert.IsTrue(File.Exists(path));

        VerifyResult? res = await VerifyFile(path);
        Assert.IsNotNull(res);
        Assert.IsNull(res.Target);

        File.Delete(path); //Clean-up
    }
}
