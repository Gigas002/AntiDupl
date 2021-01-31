using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace AntiDupl.NET.Core
{
    [Serializable]
    [XmlRoot(ElementName = "advancedOptions")]
    [StructLayout(LayoutKind.Sequential)]
    public class AdAdvancedOptions
    {
        private int deleteToRecycleBin = 1;

        [XmlAttribute(AttributeName = "deleteToRecycleBin")]
        public bool DeleteToRecycleBin
        {
            get => Convert.ToBoolean(deleteToRecycleBin);
            set => deleteToRecycleBin = Convert.ToInt32(value);
        }

        private int mistakeDataBase = 1;

        [XmlAttribute(AttributeName = "mistakeDataBase")]
        public bool MistakeDataBase
        {
            get => Convert.ToBoolean(mistakeDataBase);
            set => mistakeDataBase = Convert.ToInt32(value);
        }

        private int ratioResolution = 32;

        [XmlAttribute(AttributeName = "ratioResolution")]
        public int RatioResolution
        {
            get => ratioResolution;
            set => ratioResolution = value;
        }

        private int compareThreadCount;

        [XmlAttribute(AttributeName = "compareThreadCount")]
        public int CompareThreadCount
        {
            get => compareThreadCount;
            set => compareThreadCount = value;
        }

        private int collectThreadCount;

        [XmlAttribute(AttributeName = "collectThreadCount")]
        public int CollectThreadCount
        {
            get => collectThreadCount;
            set => collectThreadCount = value;
        }

        private int reducedImageSize = 32;

        [XmlAttribute(AttributeName = "reducedImageSize")]
        public int ReducedImageSize
        {
            get => reducedImageSize;
            set => reducedImageSize = value;
        }

        private int undoQueueSize = 10;

        [XmlAttribute(AttributeName = "undoQueueSize")]
        public int UndoQueueSize
        {
            get => undoQueueSize;
            set => undoQueueSize = value;
        }

        private int resultCountMax = 100000;

        [XmlAttribute(AttributeName = "resultCountMax")]
        public int ResultCountMax
        {
            get => resultCountMax;
            set => resultCountMax = value;
        }

        private int ignoreFrameWidth;

        [XmlAttribute(AttributeName = "ignoreFrameWidth")]
        public int IgnoreFrameWidth
        {
            get => ignoreFrameWidth;
            set => ignoreFrameWidth = value;
        }

        private int useLibJpegTurbo = 1;

        [XmlAttribute(AttributeName = "useLibJpegTurbo")]
        public bool UseLibJpegTurbo
        {
            get => Convert.ToBoolean(useLibJpegTurbo);
            set => useLibJpegTurbo = Convert.ToInt32(value);
        }
    }
}
