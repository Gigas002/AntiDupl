using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    // Она же структура TImageInfo в dll.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class AdImageInfoW
    {
        #region Properties

        private IntPtr id;

        public long Id
        {
            get => Convert.ToInt64(id);
            set => id = new IntPtr(value);
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxPathEx)]
        private string path;

        public string Path
        {
            get => path;
            set => path = value;
        }

        private ulong size;

        public ulong Size
        {
            get => size;
            set => size = value;
        }

        private ulong time;

        public long Time
        {
            get => Convert.ToInt64(time);
            set => time = Convert.ToUInt64(value);
        }

        private ImageType type;

        public ImageType Type
        {
            get => type;
            set => type = value;
        }

        private uint width;

        public int Width
        {
            get => Convert.ToInt32(width);
            set => width = Convert.ToUInt32(value);
        }

        private uint height;

        public int Height
        {
            get => Convert.ToInt32(height);
            set => height = Convert.ToUInt32(value);
        }

        private double blockiness;

        public double Blockiness
        {
            get => blockiness;
            set => blockiness = value;
        }

        private double blurring;

        public double Blurring
        {
            get => blurring;
            set => blurring = value;
        }

        private AdImageExifW exifInfo;

        public AdImageExifW ExifInfo
        {
            get => exifInfo;
            set => exifInfo = value;
        }

        #endregion

        public string GetImageSizeString()
        {
            StringBuilder builder = new();
            builder.Append(Width);
            builder.Append(" x ");
            builder.Append(Height);

            return builder.ToString();
        }

        public string GetImageTypeString()
        {
            StringBuilder builder = new();

            switch (Type)
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

        public string GetBlockinessString() => Blockiness.ToString("F2", CultureInfo.InvariantCulture);

        public string GetBlurringString() => Blurring.ToString("F2", CultureInfo.InvariantCulture);

        public string GetFileTimeString() => DateTime.FromFileTime(Time).ToString(CultureInfo.InvariantCulture);

        public string GetFileSizeString()
        {
            StringBuilder builder = new();
            string str = Math.Ceiling(Size / 1024.0).ToString(CultureInfo.InvariantCulture);
            int start = str.Length % 3;

            switch (start)
            {
                case 0: break;
                case 1:
                    builder.Append(str[0]);
                    builder.Append(' ');

                    break;
                case 2:
                    builder.Append(str[0]);
                    builder.Append(str[1]);
                    builder.Append(' ');

                    break;
            }

            for (int i = start; i < str.Length; i += 3)
            {
                builder.Append(str[i + 0]);
                builder.Append(str[i + 1]);
                builder.Append(str[i + 2]);
                builder.Append(' ');
            }

            builder.Append("KB");

            return builder.ToString();
        }

        public string GetDirectoryString()
        {
            int i = Path.Length - 1;
            while (i >= 0 && Path[i] != '\\') i--;

            return i < 0 ? "" : Path.Substring(0, i);
        }

        public string GetFileNameWithoutExtensionString() => System.IO.Path.GetFileNameWithoutExtension(Path);
    }
}
