/*
* AntiDupl.NET Program (http://ermig1979.github.io/AntiDupl).
*
* Copyright (c) 2002-2018 Yermalayeu Ihar.
*
* Permission is hereby granted, free of charge, to any person obtaining a copy 
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
* copies of the Software, and to permit persons to whom the Software is 
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in 
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

using System.Collections;
using AntiDupl.NET.Core.Original;

namespace AntiDupl.NET.Core
{
    public class CoreSearchOptions
    {
        private static string[] s_jpegExtensions = { "JPEG", "JFIF", "JPG", "JPE", "JIFF", "JIF", "J", "JNG", "JFF" };
        private static string[] s_tiffExtensions = { "TIF", "TIFF" };
        private static string[] s_bmpExtensions = { "BMP", "DIB", "RLE" };
        private static string[] s_gifExtensions = { "GIF" };
        private static string[] s_pngExtensions = { "PNG" };
        private static string[] s_emfExtensions = { "EMF", "EMZ" };
        private static string[] s_wmfExtensions = { "WMF" };
        private static string[] s_exifExtensions = { "EXIF" };
        private static string[] s_iconExtensions = { "ICON", "ICO", "ICN" };
        private static string[] s_jp2Extensions = { "JP2", "J2K", "J2C", "JPC", "JPF", "JPX" };
        private static string[] s_psdExtensions = { "PSD" };
        private static string[] s_ddsExtensions = { "DDS" };
        private static string[] s_tgaExtensions = { "TGA", "TPIC" };
        private static string[] s_webpExtensions = { "WEBP" };

        public bool subFolders;
        public bool system;
        public bool hidden;
        public bool JPEG;
        public bool BMP;
        public bool GIF;
        public bool PNG;
        public bool TIFF;
        public bool EMF;
        public bool WMF;
        public bool EXIF;
        public bool ICON;
        public bool JP2;
        public bool PSD;
        public bool DDS;
        public bool TGA;
        public bool WEBP;

        public CoreSearchOptions()
        {
        }

        public CoreSearchOptions(CoreSearchOptions searchOptions)
        {
            system = searchOptions.system;
            hidden = searchOptions.hidden;
            JPEG = searchOptions.JPEG;
            BMP = searchOptions.BMP;
            GIF = searchOptions.GIF;
            PNG = searchOptions.PNG;
            TIFF = searchOptions.TIFF;
            EMF = searchOptions.EMF;
            WMF = searchOptions.WMF;
            EXIF = searchOptions.EXIF;
            ICON = searchOptions.ICON;
            JP2 = searchOptions.JP2;
            PSD = searchOptions.PSD;
            DDS = searchOptions.DDS;
            TGA = searchOptions.TGA;
            WEBP = searchOptions.WEBP;
        }

        public CoreSearchOptions(AdSearchOptions searchOptions)
        {
            system = searchOptions.system != Constants.FALSE;
            hidden = searchOptions.hidden != Constants.FALSE;
            JPEG = searchOptions.JPEG != Constants.FALSE;
            BMP = searchOptions.BMP != Constants.FALSE;
            GIF = searchOptions.GIF != Constants.FALSE;
            PNG = searchOptions.PNG != Constants.FALSE;
            TIFF = searchOptions.TIFF != Constants.FALSE;
            EMF = searchOptions.EMF != Constants.FALSE;
            WMF = searchOptions.WMF != Constants.FALSE;
            EXIF = searchOptions.EXIF != Constants.FALSE;
            ICON = searchOptions.ICON != Constants.FALSE;
            JP2 = searchOptions.JP2 != Constants.FALSE;
            PSD = searchOptions.PSD != Constants.FALSE;
            DDS = searchOptions.DDS != Constants.FALSE;
            TGA = searchOptions.TGA != Constants.FALSE;
            WEBP = searchOptions.WEBP != Constants.FALSE;
        }

        public void ConvertTo(ref AdSearchOptions searchOptions)
        {
            searchOptions.system = system ? Constants.TRUE : Constants.FALSE;
            searchOptions.hidden = hidden ? Constants.TRUE : Constants.FALSE;
            searchOptions.JPEG = JPEG ? Constants.TRUE : Constants.FALSE;
            searchOptions.BMP = BMP ? Constants.TRUE : Constants.FALSE;
            searchOptions.GIF = GIF ? Constants.TRUE : Constants.FALSE;
            searchOptions.PNG = PNG ? Constants.TRUE : Constants.FALSE;
            searchOptions.TIFF = TIFF ? Constants.TRUE : Constants.FALSE;
            searchOptions.EMF = EMF ? Constants.TRUE : Constants.FALSE;
            searchOptions.WMF = WMF ? Constants.TRUE : Constants.FALSE;
            searchOptions.EXIF = EXIF ? Constants.TRUE : Constants.FALSE;
            searchOptions.ICON = ICON ? Constants.TRUE : Constants.FALSE;
            searchOptions.JP2 = JP2 ? Constants.TRUE : Constants.FALSE;
            searchOptions.PSD = PSD ? Constants.TRUE : Constants.FALSE;
            searchOptions.DDS = DDS ? Constants.TRUE : Constants.FALSE;
            searchOptions.TGA = TGA ? Constants.TRUE : Constants.FALSE;
            searchOptions.WEBP = WEBP ? Constants.TRUE : Constants.FALSE;
        }

        public CoreSearchOptions Clone()
        {
            return new CoreSearchOptions(this);
        }

        public bool Equals(CoreSearchOptions searchOptions)
        {
            return
                system == searchOptions.system &&
                hidden == searchOptions.hidden &&
                JPEG == searchOptions.JPEG &&
                BMP == searchOptions.BMP &&
                GIF == searchOptions.GIF &&
                PNG == searchOptions.PNG &&
                TIFF == searchOptions.TIFF &&
                EMF == searchOptions.EMF &&
                WMF == searchOptions.WMF &&
                EXIF == searchOptions.EXIF &&
                ICON == searchOptions.ICON &&
                JP2 == searchOptions.JP2 &&
                PSD == searchOptions.PSD &&
                DDS == searchOptions.DDS &&
                TGA == searchOptions.TGA &&
                WEBP == searchOptions.WEBP;
        }

        public string[] GetActualExtensions()
        {
            ArrayList extensions = new ArrayList();
            if (JPEG)
                extensions.AddRange(s_jpegExtensions);
            if (TIFF)
                extensions.AddRange(s_tiffExtensions);
            if (BMP)
                extensions.AddRange(s_bmpExtensions);
            if (GIF)
                extensions.AddRange(s_gifExtensions);
            if (PNG)
                extensions.AddRange(s_pngExtensions);
            if (EMF)
                extensions.AddRange(s_emfExtensions);
            if (WMF)
                extensions.AddRange(s_wmfExtensions);
            if (ICON)
                extensions.AddRange(s_iconExtensions);
            if (JP2)
                extensions.AddRange(s_jp2Extensions);
            if (PSD)
                extensions.AddRange(s_psdExtensions);
            if (DDS)
                extensions.AddRange(s_ddsExtensions);
            if (TGA)
                extensions.AddRange(s_tgaExtensions);
            if (WEBP)
                extensions.AddRange(s_tgaExtensions);
            return (string[])extensions.ToArray(typeof(string));
        }
    }
}
