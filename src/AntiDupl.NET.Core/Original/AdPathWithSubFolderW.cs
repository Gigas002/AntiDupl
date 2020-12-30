using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AdPathWithSubFolderW
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_PATH_EX)]
        public string path;
        public int enableSubFolder;
    }
}
