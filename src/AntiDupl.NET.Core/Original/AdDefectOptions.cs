using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AdDefectOptions
    {
        public int checkOnDefect;
        public int checkOnBlockiness;
        public int blockinessThreshold;
        public int checkOnBlockinessOnlyNotJpeg;
        public int checkOnBlurring;
        public int blurringThreshold;
    }
}
