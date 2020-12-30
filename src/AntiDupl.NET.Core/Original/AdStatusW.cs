using System;
using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AdStatusW
    {
        public StateType state;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_PATH_EX)]
        public string path;
        public UIntPtr current;
        public UIntPtr total;
    }
}
