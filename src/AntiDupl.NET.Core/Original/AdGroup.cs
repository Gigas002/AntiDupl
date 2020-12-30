using System;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AdGroup
    {
        public IntPtr id;
        public IntPtr size;
    }
}
