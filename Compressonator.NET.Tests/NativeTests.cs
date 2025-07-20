namespace Compressonator.NET.Tests;

[TestClass]
public sealed class NativeTests
{
    [TestMethod]
    [TestProperty("CI", "true")]
    public void TestSDKNative()
    {
        Assert.IsTrue(SDK_NativeMethods.IsSupported, "SDK Native methods should be supported");
    }

    [TestMethod]
    [TestProperty("CI", "true")]
    public void TestFrameworkNative()
    {
        Assert.IsTrue(FrameworkNativeMethods.IsSupported, "Framework Native Methods should be supported.");
    }

    /// <summary>
    /// Test that this method just doesn't throw any exceptions.
    /// </summary>
    [TestProperty("CI", "true")]
    [TestMethod]
    public void TestLibraryInitialization()
    {
        FrameworkNativeMethods.CMP_InitFramework();
    }

    [TestProperty("CI", "true")]
    [TestMethod]
    // This was causing some CI Crashes, let's write a test
    public void TestFreeingMipMaps()
    {
        var mip = new CMP_MipSet();
        mip.Dispose();

        // We don't have any assertions, it just must not throw.
    }

    [TestProperty("CI", "true")]
    [TestMethod]
    // This was causing some CI Crashes, let's write a test
    public void TestFreeingMipMapsMultipleTimes()
    {
        var mip = new CMP_MipSet();
        FrameworkNativeMethods.CMP_FreeMipSet(mip);
        FrameworkNativeMethods.CMP_FreeMipSet(mip);

        // We don't have any assertions, it just must not throw.
    }

    [TestProperty("CI", "true")]
    [TestMethod]
    // This was causing some CI Crashes, let's write a test
    public void TestMipSetDisposeTwice()
    {
        var mip = new CMP_MipSet();
        mip.Dispose();
        mip.Dispose();

        // We don't have any assertions, it just must not throw.
    }

    // The structure of GCAssert.AssertGarbageCollectable requires a factory due to its use of WeakReferences
    // This structure, wraps the factory in a nice way, that's easier to type :)
    public static IEnumerable<object[]> GCTests()
    {
        static object[] w<T>(Func<T> factory) where T : class => new object[] { typeof(T), factory };

        yield return w(() => new CMP_MipSet());
        yield return w(() => new CMP_Texture());
        yield return w(() => new CMP_MipLevel());
    }

    [TestMethod]
    [TestProperty("CI", "true")]
    [DynamicData(nameof(GCTests))]
    public void TestGC(Type t, Func<object> typeCreator)
    {
        GCAssert.AssertGarbageCollectable(typeCreator);
    }
}
