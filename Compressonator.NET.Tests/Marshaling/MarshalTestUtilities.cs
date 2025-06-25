using System.Reflection;
using System.Runtime.InteropServices;

namespace Compressonator.NET.Tests
{
    // Creates a dumpable structure from a marshaled object, taking into account their attributes too.
    // Made to debug struct alignment issues. 
    public static class MarshalTestUtilities
    {
        public static MarshalRecord GetMarshalRecord(Type type)
        {
            var fields = GetMarshalFields(type);
            var nested = type.GetFields()
                .Where(f => IsNested(f) && ShouldInclude(f))
                .Select(f => GetMarshalRecord(f.FieldType));

            var size = Marshal.SizeOf(type);

            return new MarshalRecord()
            {
                TotalSize = size,
                Fields = fields,
                NestedRecords = nested,
                FieldSize= fields.Sum(f => f.ByteSize),
                NestedSize = nested.Sum(n => n.TotalSize),
                FieldCount = fields.Count(),
                NestedCount = nested.Count(),
            };
        }

        public static bool IsNested(FieldInfo f)
        {
            if (f.FieldType.IsPrimitive || f.FieldType.IsEnum || f.FieldType == typeof(string) || f.FieldType.IsArray)
                return false;
            return true;
        }

        public static bool ShouldInclude(FieldInfo f)
        {
            return true;
        }

        public static IEnumerable<MarshalField> GetMarshalFields(Type t)
        {
            var layout = t.GetFields()
                .Where(f => !IsNested(f) && ShouldInclude(f))
                .Select(f => new MarshalField(f.Name, GetBytes(f)));
            return layout;
        }

        private static int GetBytes(FieldInfo f)
        {
            var a = f.GetCustomAttribute<MarshalAsAttribute>();
            if (a == null)
                return Marshal.SizeOf(f.FieldType);

            if (f.FieldType.IsArray && f.FieldType.HasElementType)
            {
                var elementType = f.FieldType.GetElementType();
                if (elementType == null)
                    return -1;
                return Marshal.SizeOf(elementType) * GetBytes(a);
            }
                

            return GetBytes(a);
        }
        private static int GetBytes(MarshalAsAttribute a)
        {
            var type = a.Value;
            switch (type)
            {
                case UnmanagedType.U4:
                case UnmanagedType.I4:
                case UnmanagedType.R4:
                    return 4;
                case UnmanagedType.U2:
                case UnmanagedType.I2:
                    return 2;
                case UnmanagedType.U1:
                case UnmanagedType.I1:
                    return 1;
                case UnmanagedType.ByValTStr:
                    return a.SizeConst;
                case UnmanagedType.ByValArray:
                    return a.SizeConst;
                case UnmanagedType.LPStruct:
                    return -1;
                default:
                    return -1;
            }
        }
        public class MarshalRecord()
        {
            public int TotalSize = 0;
            public IEnumerable<MarshalField> Fields = Enumerable.Empty<MarshalField>();
            public IEnumerable<MarshalRecord> NestedRecords = Enumerable.Empty<MarshalRecord>();

            public int FieldSize = 0;
            public int NestedSize = 0;
            public int FieldCount = 0;
            public int NestedCount = 0;
        }
        public struct MarshalField
        {
            public string FieldName;
            public int ByteSize;

            public MarshalField(string name, int size)
            {
                FieldName = name;
                ByteSize = size;
            }
        }
    }
}
