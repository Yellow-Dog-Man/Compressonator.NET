using System;
using System.Collections.Generic;
using System.Text;

namespace Compressonator.NET
{
    public enum CMP_TextureDataType
    {
        TDT_XRGB = 0,  // An RGB texture padded to DWORD width.
        TDT_ARGB = 1,  // An ARGB texture.
        TDT_NORMAL_MAP = 2,  // A normal map.
        TDT_R = 3,  // A single component texture.
        TDT_RG = 4,  // A two component texture.
        TDT_YUV_SD = 5,  // An YUB Standard Definition texture.
        TDT_YUV_HD = 6,  // An YUB High Definition texture.
    }
}
