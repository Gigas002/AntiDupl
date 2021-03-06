﻿/*
* AntiDupl.NET Program (http://ermig1979.github.io/AntiDupl).
*
* Copyright (c) 2002-2018 Yermalayeu Ihar, 2013-2018 Borisov Dmitry.
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

using System;
using System.Text;
using AntiDupl.NET.Core.Enums;
using AntiDupl.NET.Core.Original;

namespace AntiDupl.NET.Core
{
    public class CoreImageInfo
    {
        public ulong id;
        public string path;
        public ulong size;
        public ulong time;
        public ImageType type;
        public uint width;
        public uint height;
        public double blockiness;
        public double blurring;
        public AdImageExifW exifInfo;

        public CoreImageInfo(ref AdImageInfoW imageInfo)
        {
            id = (ulong)imageInfo.id;
            path = imageInfo.path;
            size = imageInfo.size;
            time = imageInfo.time;
            type = imageInfo.type;
            width = imageInfo.width;
            height = imageInfo.height;
            blockiness = imageInfo.blockiness;
            blurring = imageInfo.blurring;
            exifInfo = imageInfo.exifInfo;
        }

        public string GetImageSizeString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(width);
            builder.Append(" x ");
            builder.Append(height);
            return builder.ToString();
        }

        public string GetImageTypeString()
        {
            StringBuilder builder = new StringBuilder();
            switch (type)
            {
                case ImageType.None:
                    builder.Append("");
                    break;
                case ImageType.Bmp:
                    builder.Append("BMP");
                    break;
                case ImageType.Gif:
                    builder.Append("GIF");
                    break;
                case ImageType.Jpeg:
                    builder.Append("JPG");
                    break;
                case ImageType.Png:
                    builder.Append("PNG");
                    break;
                case ImageType.Tiff:
                    builder.Append("TIFF");
                    break;
                case ImageType.Emf:
                    builder.Append("EMF");
                    break;
                case ImageType.Wmf:
                    builder.Append("WMF");
                    break;
                case ImageType.Exif:
                    builder.Append("EXIF");
                    break;
                case ImageType.Icon:
                    builder.Append("ICON");
                    break;
                case ImageType.Jp2:
                    builder.Append("JP2");
                    break;
                case ImageType.Psd:
                    builder.Append("PSD");
                    break;
                case ImageType.Dds:
                    builder.Append("DDS");
                    break;
            }
            return builder.ToString();
        }

        public string GetBlockinessString()
        {
            return blockiness.ToString("F2");
        }

        public string GetBlurringString()
        {
            return blurring.ToString("F2");
        }

        public string GetFileTimeString()
        {
            return DateTime.FromFileTime((long)time).ToString();
        }

        public string GetFileSizeString()
        {
            StringBuilder builder = new StringBuilder();
            string str = (Math.Ceiling(size / 1024.0)).ToString();
            int start = str.Length % 3;
            switch (start)
            {
                case 0:
                    break;
                case 1:
                    builder.Append(str[0]);
                    builder.Append(" ");
                    break;
                case 2:
                    builder.Append(str[0]);
                    builder.Append(str[1]);
                    builder.Append(" ");
                    break;
            }
            for (int i = start; i < str.Length; i += 3)
            {
                builder.Append(str[i + 0]);
                builder.Append(str[i + 1]);
                builder.Append(str[i + 2]);
                builder.Append(" ");
            }
            builder.Append("KB");
            return builder.ToString();
        }

        public string GetTipString()
        {
            // TODO
            return string.Empty;
            //Strings s = Resources.Strings.Current;
            //StringBuilder builder = new StringBuilder();

            //builder.AppendLine(path);

            //builder.Append(s.ResultsListView_ImageSize_Column_Text);
            //builder.Append(": ");
            //builder.AppendLine(GetImageSizeString());

            //builder.Append(s.ResultsListView_ImageType_Column_Text);
            //builder.Append(": ");
            //builder.AppendLine(GetImageTypeString());

            //builder.Append(s.ResultsListView_FileSize_Column_Text);
            //builder.Append(": ");
            //builder.Append(GetFileSizeString());

            //return builder.ToString();
        }

        public string GetDirectoryString()
        {
            int i = path.Length - 1;
            while (i >= 0 && path[i] != '\\') i--;
            if (i < 0)
                return "";
            else
                return path.Substring(0, i);
        }

        public string GetFileNameWithoutExtensionString()
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
}
