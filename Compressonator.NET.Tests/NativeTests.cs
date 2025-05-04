namespace Compressonator.NET.Tests;

[TestClass]
public sealed class NativeTests
{
    [TestMethod]
    public void TestSDKNative()
    {
        Assert.IsTrue(SDK_NativeMethods.IsSupported, "SDK Native methods should be supported");
    }

    [TestMethod]
    public void TestFrameworkNative()
    {
        Assert.IsTrue(FrameworkNativeMethods.IsSupported, "Framework Native Methods should be supported.");
    }

    /// <summary>
    /// Test that this method just doesn't throw any exceptions.
    /// </summary>
    [TestMethod]
    public void TestLibraryInitialization()
    {
        FrameworkNativeMethods.CMP_InitFramework();
    }
}
