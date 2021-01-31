using System;
using System.Globalization;
using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class AdStatusW
    {
        private StateType state;

        public StateType State
        {
            get => state;
            set => state = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxPathEx)]
        private string path;

        public string Path
        {
            get => path;
            set => path = value;
        }

        private UIntPtr current;

        public int Current
        {
            get => Convert.ToInt32(current, CultureInfo.InvariantCulture);
            set => current = new UIntPtr((uint)value);
        }

        public UIntPtr total;

        public int Total
        {
            get => Convert.ToInt32(total, CultureInfo.InvariantCulture);
            set => total = new UIntPtr((uint)value);
        }
    }
}
