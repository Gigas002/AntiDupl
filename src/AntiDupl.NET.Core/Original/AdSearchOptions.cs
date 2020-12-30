using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AdSearchOptions
    {
        public int system;
        public int hidden;
        public int JPEG;
        public int BMP;
        public int GIF;
        public int PNG;
        public int TIFF;
        public int EMF;
        public int WMF;
        public int EXIF;
        public int ICON;
        public int JP2;
        public int PSD;
        public int DDS;
        public int TGA;
        public int WEBP;
    }
}
