using System;
using System.Runtime.InteropServices;
using AntiDupl.NET.Core.Enums;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class AdResultW
    {
        public ResultType type;
        [MarshalAs(UnmanagedType.Struct)]
        public AdImageInfoW first;
        [MarshalAs(UnmanagedType.Struct)]
        public AdImageInfoW second;
        public DefectType defect;
        public double difference;
        public TransformType transform;
        public IntPtr group;
        public IntPtr groupSize;
        public HintType hint;
    }
}
