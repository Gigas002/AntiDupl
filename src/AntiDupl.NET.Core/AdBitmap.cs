using System;
using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public class AdBitmap
    {
        private uint width;

        public uint Width
        {
            get => width;
            set => width = value;
        }

        private uint height;

        public uint Height
        {
            get => height;
            set => height = value;
        }

        private int stride;

        public int Stride
        {
            get => stride;
            set => stride = value;
        }

        private PixelFormatType format;

        public PixelFormatType Format
        {
            get => format;
            set => format = value;
        }

        private IntPtr data;

        public IntPtr Data
        {
            get => data;
            set => data = value;
        }
    }
}
