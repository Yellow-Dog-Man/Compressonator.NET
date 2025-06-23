using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Compressonator.NET
{
    public static class CMP_Format_Extensions
    {
        public const UnmanagedType CMP_FORMAT_MARSHAL = UnmanagedType.U4;
    }
    public enum CMP_FORMAT
    {
        Unknown = 0x0000,  // Undefined texture format.

        // Key to format types 0xFnbC
        // C = 0 is uncompressed C > 0 is compressed
        //
        // For C = 0 uncompressed
        // F = 1 is Float data, F = 0 is Byte data,
        // nb is a format type
        //
        // For C >= 1 Compressed
        // F = 1 is signed data, F = 0 is unsigned data,
        // b = format is a BCn block comprerssor where b is 1..7 for BC1..BC7,
        // C > 1 is a varaiant of the format (example: swizzled format for DXTC, or a signed version)
        //

        // Channel Component formats --------------------------------------------------------------------------------
        // Byte Format 0x0nn0
        RGBA_8888_S = 0x0010,  // RGBA format with signed 8-bit fixed channels.
        ARGB_8888_S = 0x0020,  // ARGB format with signed 8-bit fixed channels.
        ARGB_8888 = 0x0030,  // ARGB format with 8-bit fixed channels.
        ABGR_8888 = 0x0040,  // ABGR format with 8-bit fixed channels.
        RGBA_8888 = 0x0050,  // RGBA format with 8-bit fixed channels.
        BGRA_8888 = 0x0060,  // BGRA format with 8-bit fixed channels.
        RGB_888 = 0x0070,  // RGB format with 8-bit fixed channels.
        RGB_888_S = 0x0080,  // RGB format with 8-bit fixed channels.
        BGR_888 = 0x0090,  // BGR format with 8-bit fixed channels.
        RG_8_S = 0x00A0,  // Two component format with signed 8-bit fixed channels.
        RG_8 = 0x00B0,  // Two component format with 8-bit fixed channels.
        R_8_S = 0x00C0,  // Single component format with signed 8-bit fixed channel.
        R_8 = 0x00D0,  // Single component format with 8-bit fixed channel.
        ARGB_2101010 = 0x00E0,  // ARGB format with 10-bit fixed channels for color & a 2-bit fixed channel for alpha.
        RGBA_1010102 = 0x00F0,  // RGBA format with 10-bit fixed channels for color & a 2-bit fixed channel for alpha.
        ARGB_16 = 0x0100,  // ARGB format with 16-bit fixed channels.
        ABGR_16 = 0x0110,  // ABGR format with 16-bit fixed channels.
        RGBA_16 = 0x0120,  // RGBA format with 16-bit fixed channels.
        BGRA_16 = 0x0130,  // BGRA format with 16-bit fixed channels.
        RG_16 = 0x0140,  // Two component format with 16-bit fixed channels.
        R_16 = 0x0150,  // Single component format with 16-bit fixed channels.

        // Float Format 0x1nn0
        RGBE_32F = 0x1000,  // RGB format with 9-bit floating point each channel and shared 5 bit exponent
        ARGB_16F = 0x1010,  // ARGB format with 16-bit floating-point channels.
        ABGR_16F = 0x1020,  // ABGR format with 16-bit floating-point channels.
        RGBA_16F = 0x1030,  // RGBA format with 16-bit floating-point channels.
        BGRA_16F = 0x1040,  // BGRA format with 16-bit floating-point channels.
        RG_16F = 0x1050,  // Two component format with 16-bit floating-point channels.
        R_16F = 0x1060,  // Single component with 16-bit floating-point channels.
        ARGB_32F = 0x1070,  // ARGB format with 32-bit floating-point channels.
        ABGR_32F = 0x1080,  // ABGR format with 32-bit floating-point channels.
        RGBA_32F = 0x1090,  // RGBA format with 32-bit floating-point channels.
        BGRA_32F = 0x10A0,  // BGRA format with 32-bit floating-point channels.
        RGB_32F = 0x10B0,  // RGB format with 32-bit floating-point channels.
        BGR_32F = 0x10C0,  // BGR format with 32-bit floating-point channels.
        RG_32F = 0x10D0,  // Two component format with 32-bit floating-point channels.
        R_32F = 0x10E0,  // Single component with 32-bit floating-point channels.

        // Lossless Based Compression Formats --------------------------------------------------------------------------------
        // Format 0x2nn0
        BROTLIG = 0x2000,  //< Lossless CMP format compression : Prototyping

        // Compression formats ------------ GPU Mapping DirectX, Vulkan and OpenGL formats and comments --------
        // Compressed Format 0xSnn1..0xSnnF   (Keys 0x00Bv..0x00Bv) S =1 is signed, 0 = unsigned, B =Block Compressors 1..7 (BC1..BC7) and v > 1 is a variant like signed or swizzle
        BC1 = 0x0011,      // DXGI_FORMAT_BC1_UNORM GL_COMPRESSED_RGBA_S3TC_DXT1_EXT A four component opaque (or 1-bit alpha)
                           // compressed texture format for Microsoft DirectX10. Identical to DXT1.  Four bits per pixel.
        BC2 = 0x0021,      // DXGI_FORMAT_BC2_UNORM VK_FORMAT_BC2_UNORM_BLOCK GL_COMPRESSED_RGBA_S3TC_DXT3_EXT A four component
                           // compressed texture format with explicit alpha for Microsoft DirectX10. Identical to DXT3. Eight bits per pixel.
        BC3 = 0x0031,      // DXGI_FORMAT_BC3_UNORM VK_FORMAT_BC3_UNORM_BLOCK GL_COMPRESSED_RGBA_S3TC_DXT5_EXT A four component
                           // compressed texture format with interpolated alpha for Microsoft DirectX10. Identical to DXT5. Eight bits per pixel.
        BC4 = 0x0041,      // DXGI_FORMAT_BC4_UNORM VK_FORMAT_BC4_UNORM_BLOCK GL_COMPRESSED_RED_RGTC1 A single component
                           // compressed texture format for Microsoft DirectX10. Identical to ATI1N. Four bits per pixel.
        BC4_S = 0x1041,    // DXGI_FORMAT_BC4_SNORM VK_FORMAT_BC4_SNORM_BLOCK GL_COMPRESSED_SIGNED_RED_RGTC1 A single component
                           // compressed texture format for Microsoft DirectX10. Identical to ATI1N. Four bits per pixel.
        BC5 = 0x0051,      // DXGI_FORMAT_BC5_UNORM VK_FORMAT_BC5_UNORM_BLOCK GL_COMPRESSED_RG_RGTC2 A two component
                           // compressed texture format for Microsoft DirectX10. Identical to ATI2N_XY. Eight bits per pixel.
        BC5_S = 0x1051,    // DXGI_FORMAT_BC5_SNORM VK_FORMAT_BC5_SNORM_BLOCK GL_COMPRESSED_RGBA_BPTC_UNORM A two component
                           // compressed texture format for Microsoft DirectX10. Identical to ATI2N_XY. Eight bits per pixel.
        BC6H = 0x0061,  // DXGI_FORMAT_BC6H_UF16 VK_FORMAT_BC6H_UFLOAT_BLOCK GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT BC6H compressed texture format (UF)
        BC6H_SF = 0x1061,  // DXGI_FORMAT_BC6H_SF16 VK_FORMAT_BC6H_SFLOAT_BLOCK GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT   BC6H compressed texture format (SF)
        BC7 = 0x0071,  // DXGI_FORMAT_BC7_UNORM VK_FORMAT_BC7_UNORM_BLOCK GL_COMPRESSED_RGBA_BPTC_UNORM BC7  compressed texture format

        ATI1N = 0x0141,       // DXGI_FORMAT_BC4_UNORM VK_FORMAT_BC4_UNORM_BLOCK GL_COMPRESSED_RED_RGTC1 Single component
                              // compression format using the same technique as DXT5 alpha. Four bits per pixel.
        ATI2N = 0x0151,       // DXGI_FORMAT_BC5_UNORM VK_FORMAT_BC5_UNORM_BLOCK GL_COMPRESSED_RG_RGTC2 Two component compression format using the same
                              // technique as DXT5 alpha. Designed for compression of tangent space normal maps. Eight bits per pixel.
        ATI2N_XY = 0x0152,    // DXGI_FORMAT_BC5_UNORM VK_FORMAT_BC5_UNORM_BLOCK GL_COMPRESSED_RG_RGTC2 Two component compression format using the
                              // same technique as DXT5 alpha. The same as ATI2N but with the channels swizzled. Eight bits per pixel.
        ATI2N_DXT5 = 0x0153,  // DXGI_FORMAT_BC5_UNORM VK_FORMAT_BC5_UNORM_BLOCK GL_COMPRESSED_RG_RGTC2 ATI2N like format
                              // using DXT5. Intended for use on GPUs that do not natively support ATI2N. Eight bits per pixel.

        DXT1 = 0x0211,  // DXGI_FORMAT_BC1_UNORM VK_FORMAT_BC1_RGB_UNORM_BLOCK GL_COMPRESSED_RGBA_S3TC_DXT1_EXT
                        // A DXTC compressed texture matopaque (or 1-bit alpha). Four bits per pixel.
        DXT3 = 0x0221,  // DXGI_FORMAT_BC2_UNORM VK_FORMAT_BC2_UNORM_BLOCK GL_COMPRESSED_RGBA_S3TC_DXT3_EXT
                        // DXTC compressed texture format with explicit alpha. Eight bits per pixel.

        DXT5 = 0x0231,       // DXGI_FORMAT_BC3_UNORM VK_FORMAT_BC3_UNORM_BLOCK GL_COMPRESSED_RGBA_S3TC_DXT5_EXT
                             // DXTC compressed texture format with interpolated alpha. Eight bits per pixel.
        DXT5_xGBR = 0x0252,  // DXGI_FORMAT_UNKNOWN DXT5 with the red component swizzled into the alpha channel. Eight bits per pixel.
        DXT5_RxBG = 0x0253,  // DXGI_FORMAT_UNKNOWN swizzled DXT5 format with the green component swizzled into the alpha channel. Eight bits per pixel.
        DXT5_RBxG = 0x0254,  // DXGI_FORMAT_UNKNOWN swizzled DXT5 format with the green component swizzled
                             // into the alpha channel & the blue component swizzled into the green channel. Eight bits per pixel.
        DXT5_xRBG = 0x0255,  // DXGI_FORMAT_UNKNOWN swizzled DXT5 format with the green component swizzled into
                             // the alpha channel & the red component swizzled into the green channel. Eight bits per pixel.
        DXT5_RGxB = 0x0256,  // DXGI_FORMAT_UNKNOWN swizzled DXT5 format with the blue component swizzled into the alpha channel. Eight bits per pixel.
        DXT5_xGxR = 0x0257,  // two-component swizzled DXT5 format with the red component swizzled into the alpha channel &
                             // the green component in the green channel. Eight bits per pixel.

        ATC_RGB = 0x0301,  // CMP - a compressed RGB format.
        ATC_RGBA_Explicit = 0x0302,  // CMP - a compressed ARGB format with explicit alpha.
        ATC_RGBA_Interpolated = 0x0303,  // CMP - a compressed ARGB format with interpolated alpha.

        ASTC = 0x0A01,  // DXGI_FORMAT_UNKNOWN   VK_FORMAT_ASTC_4x4_UNORM_BLOCK to VK_FORMAT_ASTC_12x12_UNORM_BLOCK
        APC = 0x0A02,  // APC Texture Compressor
        PVRTC = 0x0A03,  //

        ETC_RGB = 0x0E01,  // DXGI_FORMAT_UNKNOWN VK_FORMAT_ETC2_R8G8B8_UNORM_BLOCK GL_COMPRESSED_RGB8_ETC2  backward compatible
        ETC2_RGB = 0x0E02,  // DXGI_FORMAT_UNKNOWN VK_FORMAT_ETC2_R8G8B8_UNORM_BLOCK GL_COMPRESSED_RGB8_ETC2
        ETC2_SRGB = 0x0E03,  // DXGI_FORMAT_UNKNOWN VK_FORMAT_ETC2_R8G8B8_SRGB_BLOCK GL_COMPRESSED_SRGB8_ETC2
        ETC2_RGBA = 0x0E04,  // DXGI_FORMAT_UNKNOWN VK_FORMAT_ETC2_R8G8B8A8_UNORM_BLOCK GL_COMPRESSED_RGBA8_ETC2_EAC
        ETC2_RGBA1 = 0x0E05,  // DXGI_FORMAT_UNKNOWN VK_FORMAT_ETC2_R8G8B8A1_UNORM_BLOCK GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2
        ETC2_SRGBA = 0x0E06,  // DXGI_FORMAT_UNKNOWN VK_FORMAT_ETC2_R8G8B8A8_SRGB_BLOCK GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC
        ETC2_SRGBA1 = 0x0E07,  // DXGI_FORMAT_UNKNOWN VK_FORMAT_ETC2_R8G8B8A1_SRGB_BLOCK GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2

        // New Compression Formats -------------------------------------------------------------
        BINARY = 0x0B01,  //< Binary/Raw Data Format
        GTC = 0x0B02,  //< GTC   Fast Gradient Texture Compressor
        BASIS = 0x0B03,  //< BASIS compression

        MAX = 0xFFFF  // Invalid Format
    }
}
