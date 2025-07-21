using Compressonator.NET.Tests.Snapshot;

namespace Compressonator.NET.Tests;

/// <summary>
/// These tests output PNGs, rather than DDS files. This means you can more easily evaluate qualities in your browser etc.
/// </summary>
[TestClass]
public class QualityEvaluationTests: SnapshotTestingBase
{
    static List<string> paths = new List<string>()
    {
        "Resources/rainbow.png",
        "Resources/carrots.png",
        "Resources/colorpatch.png",
        "Resources/squares.png",

        //"Resources/wings.png",
        //"Resources/tulipbody.png"
    };

    [DataRow(CMP_FORMAT.BC1, 1f)]
    [DataRow(CMP_FORMAT.BC2, 1f)]
    [DataRow(CMP_FORMAT.BC3, 1f)]
    [DataRow(CMP_FORMAT.BC4, 1f)]
    [DataRow(CMP_FORMAT.BC5, 1f)]

    [DataRow(CMP_FORMAT.BC6H, 0.05f)]
    [DataRow(CMP_FORMAT.BC6H, 0.10f)]
    [DataRow(CMP_FORMAT.BC6H, 0.25f)]

    [DataRow(CMP_FORMAT.BC7, 0.05f)]
    [DataRow(CMP_FORMAT.BC7, 0.10f)]
    [DataRow(CMP_FORMAT.BC7, 0.2f)]
    [DataRow(CMP_FORMAT.BC7, 0.25f)]
    [TestMethod]
    [TestProperty("CI", "false")]
    public async Task VerifyTexture(CMP_FORMAT targetFormat, float quality, bool disabled = false)
    {
        TestUtilities.GuardCITests(targetFormat, quality);

        if (disabled)
            Assert.Inconclusive($"This test is disabled");

        foreach (string path in paths)
        {
            var settings = new VerifySettings();
            settings.UseDirectory("QualityEvaluationTests/"+ Path.GetFileName(path));
            settings.UseFileName($"{targetFormat}-{quality}");

            /// TMP: REMOVE IT!
            settings.AutoVerify();
            var distortedPath = CurrentFile.Relative(SnapshotUtilities.GetFileNameForTest("png"));

            CMP_Texture distortedTexture = SnapshotUtilities.RoundTripWithCompression(path, targetFormat, quality);

            await SnapshotUtilities.SaveVerifyDelete(distortedTexture, "png", settings);
        }
    }
}
