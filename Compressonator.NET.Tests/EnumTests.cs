namespace Compressonator.NET.Tests;

// Tests are designed to ensure that both native components DLLs fully support what we (In .NET) support.
[TestClass]
public class EnumTests
{
    // If there's an enum mismatch, it can lead to unreliable undefined behavior.
    // We test both classes, because they use different compilation chains, and could get out of sync.
    [TestEnum(typeof(CMP_FORMAT))]
    [DataTestMethod]
    public void TestCMP_FORMAT(CMP_FORMAT format)
    {
        Assert.IsTrue(SDK_NativeMethods.CMP_IsValidFormat(format), $"CMP_Format must be valid for: {format}");
        Assert.IsTrue(FrameworkNativeMethods.CMP_IsValidFormat(format), $"CMP_Format must be valid for: {format}");
    }
}
