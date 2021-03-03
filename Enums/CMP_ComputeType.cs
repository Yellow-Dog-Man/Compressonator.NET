using System;
using System.Collections.Generic;
using System.Text;

namespace Compressonator.NET
{
    public enum CMP_ComputeType
    {
        CMP_CPU = 0,   //Use CPU Only, encoders defined CMP_CPUEncode or Compressonator lib will be used
        CMP_HPC = 1,   //Use CPU High Performance Compute Encoders with SPMD support defined in CMP_CPUEncode)
        CMP_GPU = 2,   //Use GPU Kernel Encoders to compress textures using Default GPU Framework auto set by the codecs been used
        CMP_GPU_OCL = 3,   //Use GPU Kernel Encoders to compress textures using OpenCL Framework
        CMP_GPU_DXC = 4,   //Use GPU Kernel Encoders to compress textures using DirectX Compute Framework
        CMP_GPU_VLK = 5    //Use GPU Kernel Encoders to compress textures using Vulkan Compute Framework
    }
}
