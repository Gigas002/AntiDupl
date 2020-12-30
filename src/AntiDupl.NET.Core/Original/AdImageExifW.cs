using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    // Она же class TImageExif decimal в dll
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AdImageExifW
    {
        public int isEmpty;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        public string imageDescription;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        public string equipMake;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        public string equipModel;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        public string softwareUsed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        public string dateTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        public string artist;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        public string userComment;
    }
}
