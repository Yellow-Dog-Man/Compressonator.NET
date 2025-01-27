using Compressonator.NET;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"SDK Supported: " + SDK_NativeMethods.IsSupported);
        Console.WriteLine($"Framwork Supported: " + FrameworkNativeMethods.IsSupported);

        //TODO: I tried for like 3 hours to get a texture conversion/compression working but I have no idea what I'm doing - Prime.
    }
}
