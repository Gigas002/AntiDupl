using System;
using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public class AdCompareOptions
    {
        private int checkOnEquality;

        public bool CheckOnEquality
        {
            get => Convert.ToBoolean(checkOnEquality);
            set => checkOnEquality = Convert.ToInt32(value);
        }

        private int transformedImage;

        public bool TransformedImage
        {
            get => Convert.ToBoolean(transformedImage);
            set => transformedImage = Convert.ToInt32(transformedImage);
        }

        private int sizeControl;

        public bool SizeControl
        {
            get => Convert.ToBoolean(sizeControl);
            set => sizeControl = Convert.ToInt32(value);
        }

        private int typeControl;

        public bool TypeControl
        {
            get => Convert.ToBoolean(typeControl);
            set => typeControl = Convert.ToInt32(value);
        }

        private int ratioControl;

        public bool RatioControl
        {
            get => Convert.ToBoolean(ratioControl);
            set => ratioControl = Convert.ToInt32(ratioControl);
        }

        private int thresholdDifference;

        public int ThresholdDifference
        {
            get => thresholdDifference;
            set => thresholdDifference = value;
        }

        private int minimalImageSize;

        public int MinimalImageSize
        {
            get => minimalImageSize;
            set => minimalImageSize = value;
        }

        private int maximalImageSize;

        public int MaximalImageSize
        {
            get => maximalImageSize;
            set => maximalImageSize = value;
        }

        private int compareInsideOneFolder;

        public bool CompareInsideOneFolder
        {
            get => Convert.ToBoolean(compareInsideOneFolder);
            set => compareInsideOneFolder = Convert.ToInt32(value);
        }

        private int compareInsideOneSearchPath;

        public bool CompareInsideOneSearchPath
        {
            get => Convert.ToBoolean(compareInsideOneSearchPath);
            set => compareInsideOneSearchPath = Convert.ToInt32(value);
        }

        private AlgorithmComparing algorithmComparing;

        public AlgorithmComparing AlgorithmComparing
        {
            get => algorithmComparing;
            set => algorithmComparing = value;
        }
    }
}
