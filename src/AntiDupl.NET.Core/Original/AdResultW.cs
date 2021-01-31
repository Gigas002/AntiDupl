using System;
using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class AdResultW
    {
        private ResultType type;

        public ResultType Type
        {
            get => type;
            set => type = value;
        }

        [MarshalAs(UnmanagedType.Struct)]
        private AdImageInfoW first;

        public AdImageInfoW First
        {
            get => first;
            set => first = value;
        }

        [MarshalAs(UnmanagedType.Struct)]
        private AdImageInfoW second;

        public AdImageInfoW Second
        {
            get => second;
            set => second = value;
        }

        private DefectType defect;

        public DefectType Defect
        {
            get => defect;
            set => defect = value;
        }

        private double difference;

        public double Difference
        {
            get => difference;
            set => difference = value;
        }

        private TransformType transform;

        public TransformType Transform
        {
            get => transform;
            set => transform = value;
        }

        private IntPtr group;

        public int Group
        {
            get => Convert.ToInt32(group);
            set => group = new IntPtr(value);
        }

        private IntPtr groupSize;

        public int GroupSize
        {
            get => Convert.ToInt32(groupSize);
            set => groupSize = new IntPtr(value);
        }

        private HintType hint;

        public HintType Hint
        {
            get => hint;
            set => hint = value;
        }
    }
}
