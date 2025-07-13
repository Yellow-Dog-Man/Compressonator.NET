using System.Runtime.InteropServices;
using static VerifyTests.VerifyMarshaling;

namespace Compressonator.NET.Tests;

[UsesVerify]
[TestClass]
public partial class MarshalTests
{
    // These are manually calculated from the C++ code, if they change, you will need to get them.
    // Usually sizeof(CMP_CompressOptions), I dont see this changing much.
    public const int CORRECT_COMPRESSOPTIONS_SIZE = 1511;
    public const int CORRECT_COMPRESSOPTIONS_FIELDS = 54;
    public const int CORRECT_FORMAT_SIZE = 4;

    [TestMethod]
    public async Task TestCMP_CompressOptions()
    {
        var options = new CMP_CompressOptions();
        Assert.AreEqual(CORRECT_COMPRESSOPTIONS_SIZE, Marshal.SizeOf<CMP_CompressOptions>(), "CMP_CompressOptions should have the correct size");
        Assert.AreEqual(CORRECT_COMPRESSOPTIONS_SIZE, (int)options.size, "CMP_CompressOptions's size should be auto-set on construct");

        await VerifyMemoryLayout(typeof(CMP_CompressOptions));
    }

    [TestMethod]
    public void TestCMP_FORMAT()
    {
        Type underlyingType = Enum.GetUnderlyingType(typeof(CMP_FORMAT));
        Assert.AreEqual(CORRECT_FORMAT_SIZE, Marshal.SizeOf(underlyingType));
    }

    [DataRow(12, typeof(KernelPerformanceStats))]
    [DataRow(388, typeof(KernelDeviceInfo))]
    [DataRow(48, typeof(AMD_CMD))]
    [DataTestMethod]
    public async Task TestStructs(int correctSize, Type t)
    {
        Assert.AreEqual(correctSize, Marshal.SizeOf(t), $"{nameof(t)} must marshal as {correctSize} bytes");

        await VerifyMemoryLayout(t);
    }
}
