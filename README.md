﻿# Compressonator.NET
.NET wrapper for the Compressonator library/framework by AMD, using P/Invoke

Compressonator binaries are sourced from [our fork of the Native Library](https://github.com/Yellow-Dog-Man/Compressonator). See [Native\Readme.md](Native\Readme.md) for more information.

Binaries are packed into the NuGet Package according to [DotNet Guidance](https://learn.microsoft.com/en-us/dotnet/standard/native-interop/native-library-loading)


## Test Images
This project uses Test Images for Snapshot Testing. You can read credits for these images on a [dedicated page](https://github.com/Yellow-Dog-Man/Compressonator.NET/tree/main/Compressonator.NET.Tests/Snapshot/Resources).

We are currently **NOT** looking for any additional test images. Each one adds a minute or two to our CI.

## Viewing Snapshot Files
Some of our Snapshot files are generated as DDS which can be tricky to view.

You can try:
- [Our Fork of PFIM Viewer](https://github.com/Yellow-Dog-Man/Pfim/tree/master/src/Pfim.Viewer) 
- [DDS Viewer](https://ddsviewer.com/) - But this crashed for me - Prime
