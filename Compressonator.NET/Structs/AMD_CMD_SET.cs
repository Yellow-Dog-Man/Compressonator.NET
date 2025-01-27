using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AMD_CMD_SET
    {
        public fixed byte strCommand[Constants.AMD_MAX_CMD_STR];
        public fixed byte strParameter[Constants.AMD_MAX_CMD_PARAM];
    }
}
