using System;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AdStatistic
    {
        public UIntPtr scanedFolderNumber;
        public UIntPtr searchedImageNumber;
        public ulong searchedImageSize;
        public UIntPtr collectedImageNumber;
        public UIntPtr comparedImageNumber;
        public UIntPtr collectThreadCount;
        public UIntPtr compareThreadCount;
        public UIntPtr defectImageNumber;
        public UIntPtr duplImagePairNumber;
        public UIntPtr renamedImageNumber;
        public UIntPtr deletedImageNumber;
        public ulong deletedImageSize;
    };
}
