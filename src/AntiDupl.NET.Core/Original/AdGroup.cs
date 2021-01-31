using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AntiDupl.NET.Core.Original
{
    [StructLayout(LayoutKind.Sequential)]
    public class AdGroup
    {
        private IntPtr id;

        public int Id
        {
            get => Convert.ToInt32(id, CultureInfo.InvariantCulture);
            set => id = new IntPtr(value);
        }

        private IntPtr size;

        public uint Size
        {
            get => Convert.ToUInt32(size, CultureInfo.InvariantCulture);
            set => size = new IntPtr(value);
        }

        public CoreImageInfo[] Images { get; set; }

        public Size MaxSize { get; set; }

        public AdGroup(CoreLib core)
        {
            if (core == null) throw new ArgumentNullException(nameof(core));

            Images = core.GetImageInfo(Id, 0, Size);
            MaxSize = new Size(0, 0);

            foreach (CoreImageInfo imageInfo in Images)
            {
                int width = Math.Max(MaxSize.Width, (int)imageInfo.width);
                int height = Math.Max(MaxSize.Height, (int)imageInfo.height);

                MaxSize = new Size(width, height);
            }
        }
    }
}
