using System.Runtime.InteropServices;

namespace Compressonator.NET.Tests;

[TestClass]
public class MarshalTests
{
    // These are manually calculated from the C++ code, if they change, you will need to get them.
    // Usually sizeof(CMP_CompressOptions), I dont see this changing much.
    public const int CORRECT_COMPRESSOPTIONS_SIZE = 1523;
    public const int CORRECT_FORMAT_SIZE = 4;

    [TestMethod]
    public void TestCMP_CompressOptions()
    {
        var options = new CMP_CompressOptions();
        Assert.AreEqual(CORRECT_COMPRESSOPTIONS_SIZE, Marshal.SizeOf<CMP_CompressOptions>(), "CMP_CompressOptions should have the correct size");
        Assert.AreEqual(CORRECT_COMPRESSOPTIONS_SIZE, (int)options.size, "CMP_CompressOptions's size should be auto-set on construct");
    }

    [TestMethod]
    public void TestCMP_FORMAT()
    {
        Type underlyingType = Enum.GetUnderlyingType(typeof(CMP_FORMAT));
        Assert.AreEqual(CORRECT_FORMAT_SIZE, Marshal.SizeOf(underlyingType));
    }
}
