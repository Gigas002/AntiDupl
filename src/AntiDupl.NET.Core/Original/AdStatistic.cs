using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public class AdStatistic
    {
        private UIntPtr scanedFolderNumber;

        public uint ScanedFolderNumber
        {
            get => Convert.ToUInt32(scanedFolderNumber, CultureInfo.InvariantCulture);
            set => scanedFolderNumber = new UIntPtr(value);
        }

        private UIntPtr searchedImageNumber;

        public uint SearchedImageNumber
        {
            get => Convert.ToUInt32(searchedImageNumber, CultureInfo.InvariantCulture);
            set => searchedImageNumber = new UIntPtr(value);
        }

        private ulong searchedImageSize;

        public ulong SearchedImageSize
        {
            get => searchedImageSize;
            set => searchedImageSize = value;
        }

        public UIntPtr collectedImageNumber;

        public uint CollectedImageNumber
        {
            get => Convert.ToUInt32(collectedImageNumber, CultureInfo.InvariantCulture);
            set => collectedImageNumber = new UIntPtr(value);
        }

        public UIntPtr comparedImageNumber;

        public uint ComparedImageNumber
        {
            get => Convert.ToUInt32(comparedImageNumber, CultureInfo.InvariantCulture);
            set => comparedImageNumber = new UIntPtr(value);
        }

        public UIntPtr collectThreadCount;

        public uint CollectThreadCount
        {
            get => Convert.ToUInt32(collectThreadCount, CultureInfo.InvariantCulture);
            set => collectThreadCount = new UIntPtr(value);
        }

        public UIntPtr compareThreadCount;

        public uint CompareThreadCount
        {
            get => Convert.ToUInt32(compareThreadCount, CultureInfo.InvariantCulture);
            set => compareThreadCount = new UIntPtr(value);
        }

        public UIntPtr defectImageNumber;

        public uint DefectImageNumber
        {
            get => Convert.ToUInt32(defectImageNumber, CultureInfo.InvariantCulture);
            set => defectImageNumber = new UIntPtr(value);
        }

        public UIntPtr duplImagePairNumber;

        public uint DuplImagePairNumber
        {
            get => Convert.ToUInt32(duplImagePairNumber, CultureInfo.InvariantCulture);
            set => duplImagePairNumber = new UIntPtr(value);
        }

        public UIntPtr renamedImageNumber;

        public uint RenamedImageNumber
        {
            get => Convert.ToUInt32(renamedImageNumber, CultureInfo.InvariantCulture);
            set => renamedImageNumber = new UIntPtr(value);
        }

        public UIntPtr deletedImageNumber;

        public uint DeletedImageNumber
        {
            get => Convert.ToUInt32(deletedImageNumber, CultureInfo.InvariantCulture);
            set => deletedImageNumber = new UIntPtr(value);
        }

        private ulong deletedImageSize;

        public ulong DeletedImageSize
        {
            get => deletedImageSize;
            set => deletedImageSize = value;
        }
    };
}
