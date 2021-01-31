using System;
using System.Drawing;
using AntiDupl.NET.Core;
using AntiDupl.NET.Core.Enums;
using AntiDupl.NET.Core.Original;

namespace AntiDupl.NET.WinForms
{
    public static class BitmapWorker
    {
        public static Bitmap LoadBitmap(AntiDuplCore coreLib, int width, int height, string path)
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
            AdBitmap[] pBitmap = new AdBitmap[1];
            pBitmap[0].Width = (uint)bitmapData.Width;
            pBitmap[0].Height = (uint)bitmapData.Height;
            pBitmap[0].Stride = bitmapData.Stride;
            pBitmap[0].Format = PixelFormatType.Argb32;
            pBitmap[0].Data = bitmapData.Scan0;
            Error error = coreLib.LoadBitmap(path, pBitmap);
            bitmap.UnlockBits(bitmapData);
            return error == Error.Ok ? bitmap : null;
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

        public static Bitmap LoadBitmap(AntiDuplCore coreLib, Size size, string path)
        {
            return LoadBitmap(coreLib, size.Width, size.Height, path);
        }

        //public System.Drawing.Bitmap LoadBitmap(CoreImageInfo imageInfo)
        //{
        //    return LoadBitmap((int)imageInfo.width, (int)imageInfo.height, imageInfo.path);
        //}

        public static Bitmap LoadBitmap(AntiDuplCore coreLib, AdImageInfoW imageInfo)
        {
            return LoadBitmap(coreLib, imageInfo.Width, imageInfo.Height, imageInfo.Path);
        }
    }
}
