using System;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class AdPathWithSubFolderW
    {
        #region Properties

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_PATH_EX)]
        private string path;

        public string Path
        {
            get => path;
            set => path = value;
        }

        private int enableSubFolder;

        public bool EnableSubFolder
        {
            get => Convert.ToBoolean(enableSubFolder);
            set => enableSubFolder = Convert.ToInt32(value);
        }

        #endregion

        public AdPathWithSubFolderW() { }

        public AdPathWithSubFolderW(string path, bool enableSubFolder)
        {
            Path = path;
            EnableSubFolder = enableSubFolder;
        }
    }
}
