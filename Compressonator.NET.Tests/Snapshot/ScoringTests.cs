using Compressonator.NET.Tests.Snapshot;
using ssimulacra2.NET;

namespace Compressonator.NET.Tests;

/// <summary>
/// Scoring tests are designed to evaluate the compression algorithm's quality in a machine readable way and for speed.
/// They are not designed for visual inspection. See our snapshot tests, which are designed for visual inspection.
/// </summary>
[TestClass]
public class ScoringTests
{
    public const bool DISABLE_SLOW_ONES = false;

    // All times recorded on Prime's Main Development Machine (Intel i7-9700K CPU)
    // All times vary a little by up to 5 s
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.05f, 89.43)] // 20 s
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.1f, 89.54)] // 16-20 s
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.2f, 89.58)] // 21 s
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.25f, 94.47)] //1.8 Minutes
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.3f, 94.47, DISABLE_SLOW_ONES)] // 1.9 Minutes
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.35f, 94.51, DISABLE_SLOW_ONES)] // 2.3 Minutes
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.4f, 94.51, DISABLE_SLOW_ONES)] // 2.4 Minutes
    [DataRow(CMP_FORMAT.BC7, "Resources/squares.png", 0.6f, 94.62, DISABLE_SLOW_ONES)] // 8+ Minutes

    [DataRow(CMP_FORMAT.BC7, "Resources/wings.png", 0.05f, 89)] //1.8 Minutes
    [DataRow(CMP_FORMAT.BC7, "Resources/wings.png", 0.1f, 89)] //1.8 Minutes
    [DataRow(CMP_FORMAT.BC7, "Resources/wings.png", 0.25f, 94)] //1.8 Minutes

    [DataRow(CMP_FORMAT.BC7, "Resources/colorpatch.png", 0.05f, 89)]
    [DataRow(CMP_FORMAT.BC7, "Resources/colorpatch.png", 0.1f, 89)]
    [DataRow(CMP_FORMAT.BC7, "Resources/colorpatch.png", 0.25f, 94)]
    [TestMethod]
    public void TestExpectedSsimulacraScore(
        CMP_FORMAT targetFormat,
        string inputFileRelativePath,
        float quality = 0.9f,
        double threshold = 90,
        bool disabled = false)
    {
        if (disabled)
            Assert.Inconclusive($"This test is disabled");

        var originalPath = CurrentFile.Relative(inputFileRelativePath);
        var distortedPath = CurrentFile.Relative(SnapshotUtilities.GetFileNameForTest("png"));

        CMP_Texture distortedTexture = SnapshotUtilities.RoundTripWithCompression(inputFileRelativePath, targetFormat, quality);

        SnapshotUtilities.Save(distortedPath, distortedTexture);

        var score = Ssimulacra2.ComputeFromFiles(originalPath, distortedPath);

        File.Delete(distortedPath); //Delete before assertions to keep "tidy"

        string displayScore = string.Format("{0:F2}", score);

        Console.WriteLine($"{displayScore} >= {threshold}");
        Console.WriteLine($"Unrounded Score: {score}");

        Assert.IsTrue(score >= threshold, $"Expected end result to be at or above threshold. {displayScore} >= {threshold} (Unrounded Score: {score})");
    }
}
