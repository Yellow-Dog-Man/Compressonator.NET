namespace Compressonator.NET.Tests;

// Please don't use this outside of tests.
// Largely from: https://stackoverflow.com/a/9827349 + other answers on the same question.
// Also: https://stackoverflow.com/questions/3829928/under-what-circumstances-we-need-to-call-gc-collect-twice from why we GC.Collect Twice.
internal static class GCAssert
{
    public static void AssertGarbageCollectable<T>(Func<T> create)
        where T : class
    {
        WeakReference reference = CreateAndDropReference(create);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        Assert.IsFalse(reference.IsAlive, $"{typeof(T).Name} should have been garbage collected");
    }

    private static WeakReference CreateAndDropReference<T>(Func<T> create)
        where T : class
    {
        var obj = create();

        if (obj is IDisposable disposable)
            disposable.Dispose();

        return new WeakReference(obj);
    }
}
