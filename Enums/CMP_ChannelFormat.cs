using System;
using System.Collections.Generic;
using System.Text;

namespace Compressonator.NET
{
    public enum CMP_ChannelFormat
    {
        CF_8bit = 0,  // 8-bit integer data.
        CF_Float16 = 1,  // 16-bit float data.
        CF_Float32 = 2,  // 32-bit float data.
        CF_Compressed = 3,  // Compressed data.
        CF_16bit = 4,  // 16-bit integer data.
        CF_2101010 = 5,  // 10-bit integer data in the color channels & 2-bit integer data in the alpha channel.
        CF_32bit = 6,  // 32-bit integer data.
        CF_Float9995E = 7,  // 32-bit partial precision float.
        CF_YUV_420 = 8,  // YUV Chroma formats
        CF_YUV_422 = 9,  // YUV Chroma formats
        CF_YUV_444 = 10, // YUV Chroma formats
        CF_YUV_4444 = 11, // YUV Chroma formats
    }
}
