using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AdCompareOptions
    {
        public int checkOnEquality;
        public int transformedImage;
        public int sizeControl;
        public int typeControl;
        public int ratioControl;
        public int thresholdDifference;
        public int minimalImageSize;
        public int maximalImageSize;
        public int compareInsideOneFolder;
        public int compareInsideOneSearchPath;
        public AlgorithmComparing algorithmComparing;
    }
}
