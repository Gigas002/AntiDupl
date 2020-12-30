using System;
using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    // Она же структура TImageInfo в dll.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AdImageInfoW
    {
        public IntPtr id;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_PATH_EX)]
        public string path;
        public ulong size;
        public ulong time;
        public ImageType type;
        public uint width;
        public uint height;
        public double blockiness;
        public double blurring;
        public AdImageExifW exifInfo;
    }
}
