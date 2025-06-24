using System.Globalization;
using System.Reflection;

namespace Compressonator.NET.Tests;

// This is purely for nicely formatted tests. It ends up creating a new test case for each enum value.
//Based on:
// - https://github.com/microsoft/testfx/issues/233#issuecomment-812200932 &
// - https://www.meziantou.net/mstest-v2-data-tests.htm
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestEnumAttribute(Type enumType) : Attribute, ITestDataSource
{
    public IEnumerable<object[]> GetData(MethodInfo methodInfo)
    {
        var values = Enum.GetValues(enumType);

        foreach (var e in values)
        {
            yield return new object[] { e };
        }
    }

    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        if (data != null)
            return string.Format(CultureInfo.CurrentCulture, "{0}", data[0]?.ToString());

        return null;
    }
}
