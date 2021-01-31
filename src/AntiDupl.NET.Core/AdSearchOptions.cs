using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public class AdSearchOptions
    {
        private int system;

        public bool System
        {
            get => Convert.ToBoolean(system);
            set => system = Convert.ToInt32(value);
        }

        private int hidden;

        public bool Hidden
        {
            get => Convert.ToBoolean(hidden);
            set => hidden = Convert.ToInt32(value);
        }

        private int JPEG;

        public bool Jpeg
        {
            get => Convert.ToBoolean(JPEG);
            set => JPEG = Convert.ToInt32(value);
        }

        private int BMP;

        public bool Bmp
        {
            get => Convert.ToBoolean(BMP);
            set => BMP = Convert.ToInt32(value);
        }

        private int GIF;

        public bool Gif
        {
            get => Convert.ToBoolean(GIF);
            set => GIF = Convert.ToInt32(value);
        }

        private int PNG;

        public bool Png
        {
            get => Convert.ToBoolean(PNG);
            set => PNG = Convert.ToInt32(value);
        }

        private int TIFF;

        public bool Tiff
        {
            get => Convert.ToBoolean(TIFF);
            set => TIFF = Convert.ToInt32(value);
        }

        private int EMF;

        public bool Emf
        {
            get => Convert.ToBoolean(EMF);
            set => EMF = Convert.ToInt32(value);
        }

        private int WMF;

        public bool Wmf
        {
            get => Convert.ToBoolean(WMF);
            set => WMF = Convert.ToInt32(value);
        }

        private int EXIF;

        public bool Exif
        {
            get => Convert.ToBoolean(EXIF);
            set => EXIF = Convert.ToInt32(value);
        }

        private int ICON;

        public bool Icon
        {
            get => Convert.ToBoolean(ICON);
            set => ICON = Convert.ToInt32(value);
        }

        private int JP2;

        public bool Jp2
        {
            get => Convert.ToBoolean(JP2);
            set => JP2 = Convert.ToInt32(value);
        }

        private int PSD;

        public bool Psd
        {
            get => Convert.ToBoolean(PSD);
            set => PSD = Convert.ToInt32(value);
        }

        private int DDS;

        public bool Dds
        {
            get => Convert.ToBoolean(DDS);
            set => DDS = Convert.ToInt32(value);
        }

        private int TGA;

        public bool Tga
        {
            get => Convert.ToBoolean(TGA);
            set => TGA = Convert.ToInt32(value);
        }

        private int WEBP;

        public bool Webp
        {
            get => Convert.ToBoolean(WEBP);
            set => WEBP = Convert.ToInt32(value);
        }

        private static readonly string[] SJpegExtensions = { "JPEG", "JFIF", "JPG", "JPE", "JIFF", "JIF", "J", "JNG", "JFF" };

        private static readonly string[] STiffExtensions = { "TIF", "TIFF" };

        private static readonly string[] SBmpExtensions = { "BMP", "DIB", "RLE" };

        private static readonly string[] SGifExtensions = { "GIF" };

        private static readonly string[] SPngExtensions = { "PNG" };

        private static readonly string[] SEmfExtensions = { "EMF", "EMZ" };

        private static readonly string[] SWmfExtensions = { "WMF" };

        private static readonly string[] SExifExtensions = { "EXIF" };

        private static readonly string[] SIconExtensions = { "ICON", "ICO", "ICN" };

        private static readonly string[] SJp2Extensions = { "JP2", "J2K", "J2C", "JPC", "JPF", "JPX" };

        private static readonly string[] SPsdExtensions = { "PSD" };

        private static readonly string[] SDdsExtensions = { "DDS" };

        private static readonly string[] STgaExtensions = { "TGA", "TPIC" };

        private static readonly string[] SWebpExtensions = { "WEBP" };

        public bool SubFolders { get; set; }

        public string[] GetActualExtensions()
        {
            ArrayList extensions = new ArrayList();
            if (Jpeg)
                extensions.AddRange(SJpegExtensions);
            if (Tiff)
                extensions.AddRange(STiffExtensions);
            if (Bmp)
                extensions.AddRange(SBmpExtensions);
            if (Gif)
                extensions.AddRange(SGifExtensions);
            if (Png)
                extensions.AddRange(SPngExtensions);
            if (Emf)
                extensions.AddRange(SEmfExtensions);
            if (Wmf)
                extensions.AddRange(SWmfExtensions);
            if (Icon)
                extensions.AddRange(SIconExtensions);
            if (Jp2)
                extensions.AddRange(SJp2Extensions);
            if (Psd)
                extensions.AddRange(SPsdExtensions);
            if (Dds)
                extensions.AddRange(SDdsExtensions);
            if (Tga)
                extensions.AddRange(STgaExtensions);
            if (Webp)
                extensions.AddRange(STgaExtensions);

            return (string[])extensions.ToArray(typeof(string));
        }
    }
}
