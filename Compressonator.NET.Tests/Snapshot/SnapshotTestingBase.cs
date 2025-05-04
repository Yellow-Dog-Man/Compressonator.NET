namespace Compressonator.NET.Tests.Snapshot;

[TestClass]
public abstract partial class SnapshotTestingBase: VerifyBase
{
    [TestMethod]
    public Task Run() =>
        VerifyChecks.Run();

    public SnapshotTestingBase()
    {
        FrameworkNativeMethods.CMP_InitFramework();
    }
}
