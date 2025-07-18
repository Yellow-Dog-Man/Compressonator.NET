namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public abstract partial class SnapshotTestingBase: VerifyBase
{
    public bool Initialized { get; } = false;
    [TestMethod]
    public Task Run() =>
        VerifyChecks.Run();

    public SnapshotTestingBase()
    {
        if (!Initialized)
            FrameworkNativeMethods.CMP_InitFramework();
        Initialized = true;
    }
}
