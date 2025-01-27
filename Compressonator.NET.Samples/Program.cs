using Compressonator.NET;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"SDK Supported: " + SDK_NativeMethods.IsSupported);
        Console.WriteLine($"Framwork Supported: " + FrameworkNativeMethods.IsSupported);
    }
}
