using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
