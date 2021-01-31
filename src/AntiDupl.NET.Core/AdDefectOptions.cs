using System;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public class AdDefectOptions
    {
        private int checkOnDefect;

        public bool CheckOnDefect
        {
            get => Convert.ToBoolean(checkOnDefect);
            set => checkOnDefect = Convert.ToInt32(value);
        }

        private int checkOnBlockiness;

        public bool CheckOnBlockiness
        {
            get => Convert.ToBoolean(checkOnBlockiness);
            set => checkOnBlockiness = Convert.ToInt32(value);
        }

        private int blockinessThreshold;

        public int BlockinessThreshold
        {
            get => blockinessThreshold;
            set => blockinessThreshold = value;
        }

        private int checkOnBlockinessOnlyNotJpeg;

        public bool CheckOnBlockinessOnlyNotJpeg
        {
            get => Convert.ToBoolean(checkOnBlockinessOnlyNotJpeg);
            set => checkOnBlockinessOnlyNotJpeg = Convert.ToInt32(value);
        }

        private int checkOnBlurring;

        public bool CheckOnBlurring
        {
            get => Convert.ToBoolean(checkOnBlurring);
            set => checkOnBlurring = Convert.ToInt32(value);
        }

        private int blurringThreshold;

        public int BlurringThreshold
        {
            get => blurringThreshold;
            set => blurringThreshold = value;
        }
    }
}
