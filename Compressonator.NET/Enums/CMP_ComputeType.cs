namespace Compressonator.NET
{
    public enum CMP_ComputeType: uint
    {
        CMP_UNKNOWN = 0,
        CMP_CPU = 1,   //Use CPU Only, encoders defined CMP_CPUEncode or Compressonator lib will be used
        CMP_HPC = 2,   //Use CPU High Performance Compute Encoders with SPMD support defined in CMP_CPUEncode)
        CMP_GPU_OCL = 3,  //Use GPU Kernel Encoders to compress textures using OpenCL Framework
        CMP_GPU_DXC = 4,  //Use GPU Kernel Encoders to compress textures using DirectX Compute Framework
        CMP_GPU_VLK = 5,  //Use GPU Kernel Encoders to compress textures using Vulkan Compute Framework
        CMP_GPU_HW = 6    //Use GPU HW to encode textures , using gl extensions
    }
}
