using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    // Она же class TImageExif decimal в dll
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class AdImageExifW
    {
        private int isEmpty;

        public int IsEmpty
        {
            get => isEmpty;
            set => isEmpty = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        private string imageDescription;

        public string ImageDescription
        {
            get => imageDescription;
            set => imageDescription = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        private string equipMake;

        public string EquipMake
        {
            get => equipMake;
            set => equipMake = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        private string equipModel;

        public string EquipModel
        {
            get => equipModel;
            set => equipModel = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        private string softwareUsed;

        public string SoftwareUsed
        {
            get => softwareUsed;
            set => softwareUsed = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        private string dateTime;

        public string DateTime
        {
            get => dateTime;
            set => dateTime = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        private string artist;

        public string Artist
        {
            get => artist;
            set => artist = value;
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_EXIF_SIZE)]
        private string userComment;

        public string UserComment
        {
            get => userComment;
            set => userComment = value;
        }
    }
}
