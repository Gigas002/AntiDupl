using System;
using System.Drawing;
using AntiDupl.NET.Core;

namespace AntiDupl.NET.WinForms
{
    public static class BitmapWorker
    {
        public static Bitmap LoadBitmap(CoreLib coreLib, int width, int height, string path)
        {
            if (height * width == 0)
                return null;

            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            catch (Exception)
            {
                GC.Collect();
                try
                {
                    bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            System.Drawing.Imaging.BitmapData bitmapData = new System.Drawing.Imaging.BitmapData();
            bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                bitmapData);
            CoreDll.adBitmap[] pBitmap = new CoreDll.adBitmap[1];
            pBitmap[0].width = (uint)bitmapData.Width;
            pBitmap[0].height = (uint)bitmapData.Height;
            pBitmap[0].stride = bitmapData.Stride;
            pBitmap[0].format = CoreDll.PixelFormatType.Argb32;
            pBitmap[0].data = bitmapData.Scan0;
            CoreDll.Error error = coreLib.LoadBitmap(path, pBitmap);
            bitmap.UnlockBits(bitmapData);
            return error == CoreDll.Error.Ok ? bitmap : null;
        }

        /// <summary>
        /// Возврашает загруженное изображение по заланному пути и заданного размера.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        //public System.Drawing.Bitmap LoadBitmap(System.Drawing.Size size, string path)
        //{
        //    return LoadBitmap(size.Width, size.Height, path);
        //}

        public static Bitmap LoadBitmap(CoreLib coreLib, Size size, string path)
        {
            return LoadBitmap(coreLib, size.Width, size.Height, path);
        }

        //public System.Drawing.Bitmap LoadBitmap(CoreImageInfo imageInfo)
        //{
        //    return LoadBitmap((int)imageInfo.width, (int)imageInfo.height, imageInfo.path);
        //}

        public static Bitmap LoadBitmap(CoreLib coreLib, CoreImageInfo imageInfo)
        {
            return LoadBitmap(coreLib, (int)imageInfo.width, (int)imageInfo.height, imageInfo.path);
        }
    }
}
