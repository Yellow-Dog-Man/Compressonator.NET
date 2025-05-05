namespace Compressonator.NET
{
    public enum CMP_ERROR
    {
        CMP_OK = 0,                            // Ok.
        CMP_ABORTED,                           // The conversion was aborted.
        CMP_ERR_INVALID_SOURCE_TEXTURE,        // The source texture is invalid.
        CMP_ERR_INVALID_DEST_TEXTURE,          // The destination texture is invalid.
        CMP_ERR_UNSUPPORTED_SOURCE_FORMAT,     // The source format is not a supported format.
        CMP_ERR_UNSUPPORTED_DEST_FORMAT,       // The destination format is not a supported format.
        CMP_ERR_UNSUPPORTED_GPU_ASTC_DECODE,   // The gpu hardware is not supported.
        CMP_ERR_UNSUPPORTED_GPU_BASIS_DECODE,  // The gpu hardware is not supported.
        CMP_ERR_SIZE_MISMATCH,                 // The source and destination texture sizes do not match.
        CMP_ERR_UNABLE_TO_INIT_CODEC,          // Compressonator was unable to initialize the codec needed for conversion.
        CMP_ERR_UNABLE_TO_INIT_DECOMPRESSLIB,  // GPU_Decode Lib was unable to initialize the codec needed for decompression .
        CMP_ERR_UNABLE_TO_INIT_COMPUTELIB,     // Compute Lib was unable to initialize the codec needed for compression.
        CMP_ERR_CMP_DESTINATION,               // Error in compressing destination texture
        CMP_ERR_MEM_ALLOC_FOR_MIPSET,          // Memory Error: allocating MIPSet compression level data buffer
        CMP_ERR_UNKNOWN_DESTINATION_FORMAT,    // The destination Codec Type is unknown! In SDK refer to GetCodecType()
        CMP_ERR_FAILED_HOST_SETUP,             // Failed to setup Host for processing
        CMP_ERR_PLUGIN_FILE_NOT_FOUND,         // The required plugin library was not found
        CMP_ERR_UNABLE_TO_LOAD_FILE,           // The requested file was not loaded
        CMP_ERR_UNABLE_TO_CREATE_ENCODER,      // Request to create an encoder failed
        CMP_ERR_UNABLE_TO_LOAD_ENCODER,        // Unable to load an encode library
        CMP_ERR_NOSHADER_CODE_DEFINED,         // No shader code is available for the requested framework
        CMP_ERR_GPU_DOESNOT_SUPPORT_COMPUTE,   // The GPU device selected does not support compute
        CMP_ERR_NOPERFSTATS,                   // No Performance Stats are available
        CMP_ERR_GPU_DOESNOT_SUPPORT_CMP_EXT,   // The GPU does not support the requested compression extension!
        CMP_ERR_GAMMA_OUTOFRANGE,              // Gamma value set for processing is out of range
        CMP_ERR_PLUGIN_SHAREDIO_NOT_SET,       // The plugin C_PluginSetSharedIO call was not set and is required for this plugin to operate
        CMP_ERR_UNABLE_TO_INIT_D3DX,           // Unable to initialize DirectX SDK or get a specific DX API
        CMP_FRAMEWORK_NOT_INITIALIZED,         // CMP_InitFramework failed or not called.
        CMP_ERR_GENERIC                        // An unknown error occurred.
    }
}
