namespace Compressonator.NET.Tests;

public static class TestUtilities
{
    public const float SLOW_QUALITY_THRESHOLD = 0.3f;
    public static bool IsCI => Environment.GetEnvironmentVariable("CI") == "true";

    public static void GuardCITests(CMP_FORMAT targetFormat, float quality)
    {
        if (!IsCI)
            return;

        if (targetFormat.IsSlowToCompute(quality))
            Assert.Inconclusive($"BC6H & BC7 Tests at higher qualities than {SLOW_QUALITY_THRESHOLD} are disabled in CI due to speed");
    }
}

public static class CMPFormatExtensions
{
    public static bool IsSlowToCompute(this CMP_FORMAT format, float quality = 1f)
    {
        switch (format)
        {
            case CMP_FORMAT.BC7:
            case CMP_FORMAT.BC6H:
            case CMP_FORMAT.BC6H_SF:
                if (quality > TestUtilities.SLOW_QUALITY_THRESHOLD)
                    return true;
                return false;
            default:
                return false;
        }
    }
}
