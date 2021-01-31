using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public class AdGroup
    {
        private IntPtr id;

        public int Id
        {
            get => Convert.ToInt32(id, CultureInfo.InvariantCulture);
            set => id = new IntPtr(value);
        }

        private IntPtr size;

        public uint Size
        {
            get => Convert.ToUInt32(size, CultureInfo.InvariantCulture);
            set => size = new IntPtr(value);
        }

        public AdImageInfoW[] Images { get; set; }

        public Size MaxSize { get; set; }
    }
}
