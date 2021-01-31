using System;

namespace AntiDupl.Native
{
    /// <summary>
    /// Info about prebuilt AntiDupl c++ aseembly
    /// </summary>
    public static class AssemblyInfo
    {
        /// <summary>
        /// Used simd version
        /// </summary>
        public static Version SimdVersion { get; } = new(4, 6, 96);

        /// <summary>
        /// Used libopenjpeg version
        /// </summary>
        public static Version LibOpenJpegVerion { get; } = new(2, 3, 1, 2);

        /// <summary>
        /// Used libwebp version
        /// </summary>
        public static Version LibWebpVersion { get; } = new(1, 1, 0, 1);

        /// <summary>
        /// Used turbojpeg version
        /// </summary>
        public static Version TurboJpegVersion { get; } = new(2, 0, 5, 1);
    }
}
