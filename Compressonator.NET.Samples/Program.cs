using Compressonator.NET;
using Compressonator.NET.Tests.Snapshot;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine($"SDK Supported: " + SDK_NativeMethods.IsSupported);
        Console.WriteLine($"Framwork Supported: " + FrameworkNativeMethods.IsSupported);

        FrameworkNativeMethods.CMP_InitFramework();

        var tests = new CompressionTests();
        await tests.TestCompression(CMP_FORMAT.BC6H, "Resources/rainbow.png", 0.05f, 1);

        //CompressIcon();
    }

    private static void CompressIcon()
    {
        string sourceFile = Path.GetFullPath("../../../../Images/icon.png");
        string targetFile = Path.GetFullPath("../../../../Images/icon.dds");
        Console.WriteLine($"Processing source file: {sourceFile}");

        CMP_MipSet mipSetIn = new();
        var cmpStatus = FrameworkNativeMethods.CMP_LoadTexture(sourceFile, mipSetIn);
        if (cmpStatus != CMP_ERROR.CMP_OK)
        {
            Console.WriteLine($"Error {cmpStatus}: Loading source file!");
            return;
        }

        var prevMipLevels = mipSetIn.mipLevels;
        if (mipSetIn.mipLevels <= 1)
        {
            const int requestLevel = 10;

            int nMinSize = FrameworkNativeMethods.CMP_CalcMinMipSize(mipSetIn.height, mipSetIn.width, requestLevel);
            _ = FrameworkNativeMethods.CMP_GenerateMIPLevels(mipSetIn, nMinSize);
        }

        if (prevMipLevels != mipSetIn.mipLevels)
            Console.WriteLine($"Changed mipmap levels {prevMipLevels}->{mipSetIn.mipLevels}");

        var mipSetCmp = new CMP_MipSet();
        var compressOptions = new CMP_CompressOptions()
        {
            quality = 0.9f,
            sourceFormat = CMP_FORMAT.Unknown,
            destFormat = CMP_FORMAT.DXT1,
            DXT1UseAlpha = true,
            alphaThreshold = 127
        };
        cmpStatus = SDK_NativeMethods.CMP_ConvertMipTexture(mipSetIn, mipSetCmp, compressOptions, IntPtr.Zero);
        if (cmpStatus != CMP_ERROR.CMP_OK)
        {
            Console.WriteLine($"Error {cmpStatus}: Processing texture!");
            return;
        }

        cmpStatus = FrameworkNativeMethods.CMP_SaveTexture(targetFile, mipSetCmp);
        if (cmpStatus != CMP_ERROR.CMP_OK)
        {
            Console.WriteLine($"Error {cmpStatus}: Saving texture!");
            return;
        }

        Console.WriteLine($"Saved texture: {targetFile}");
    }
}
