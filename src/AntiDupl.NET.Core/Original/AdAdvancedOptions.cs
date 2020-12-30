using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AdAdvancedOptions
    {
        public int deleteToRecycleBin;
        public int mistakeDataBase;
        public int ratioResolution;
        public int compareThreadCount;
        public int collectThreadCount;
        public int reducedImageSize;
        public int undoQueueSize;
        public int resultCountMax;
        public int ignoreFrameWidth;
        public int useLibJpegTurbo;
    }
}
