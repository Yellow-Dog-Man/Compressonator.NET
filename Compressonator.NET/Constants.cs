using System;
using System.Collections.Generic;
using System.Text;

namespace Compressonator.NET
{
    public static class Constants
    {
        public const int AMD_MAX_CMDS = 20;
        public const int AMD_MAX_CMD_STR = 32;
        public const int AMD_MAX_CMD_PARAM = 16;

        public const int CMD_SET_SIZE = AMD_MAX_CMD_STR + AMD_MAX_CMD_PARAM;
        public const int ALL_CMD_SETS_SIZE = CMD_SET_SIZE * AMD_MAX_CMDS;

        public const int PRINT_INFO_SIZE = 8;
    }
}
