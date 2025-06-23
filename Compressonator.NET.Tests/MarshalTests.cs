using System.Runtime.InteropServices;

namespace Compressonator.NET.Tests;

[TestClass]
public class MarshalTests
{
    public const int CORRECT_COMPRESSOPTIONS_SIZE = 1523;
    public const int CORRECT_FORMAT_SIZE = 4;

    [TestMethod]
    public void TestCMP_CompressOptions()
    {
        Assert.AreEqual(CORRECT_COMPRESSOPTIONS_SIZE, Marshal.SizeOf<CMP_CompressOptions>(), "CMP_CompressOptions");
    }

    [TestMethod]
    public void TestCMP_FORMAT()
    {
        Type underlyingType = Enum.GetUnderlyingType(typeof(CMP_FORMAT));
        Assert.AreEqual(CORRECT_FORMAT_SIZE, Marshal.SizeOf(underlyingType));
    }
}
