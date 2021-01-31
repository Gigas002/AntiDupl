/*
* AntiDupl.NET Program (http://ermig1979.github.io/AntiDupl).
*
* Copyright (c) 2002-2018 Yermalayeu Ihar.
*
* Permission is hereby granted, free of charge, to any person obtaining a copy 
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
* copies of the Software, and to permit persons to whom the Software is 
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in 
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using AntiDupl.NET.Core;

namespace AntiDupl.NET.WinForms
{
    /// <summary>
    /// Хранилище эскизов изображений.
    /// </summary>
    internal sealed class ThumbnailStorage
    {
        #region Fields

        private readonly AntiDuplCore _mCore;
        private readonly Options _mOptions;
        private readonly Dictionary<long, Bitmap> _mStorage = new();
        private readonly Mutex _mMutex = new Mutex();

        #endregion

        #region Constructor

        public ThumbnailStorage(AntiDuplCore core, Options options)
        {
            _mCore = core;
            _mOptions = options;
        }

        #endregion

        #region Methods

        public void Clear()
        {
            _mMutex.WaitOne();
            _mStorage.Clear();
            _mMutex.ReleaseMutex();
        }

        /// <summary>
        /// Существует ли в хранилише изображение по переданному изображению.
        /// </summary>
        /// <param name="imageInfo"></param>
        /// <returns></returns>
        public bool Exists(AdImageInfoW imageInfo)
        {
            bool result = false;
            _mMutex.WaitOne();
            if (_mStorage.ContainsKey(imageInfo.Id))
            {
                Bitmap bitmap = _mStorage[imageInfo.Id];
                if (bitmap != null)
                {
                    Size size = GetThumbnailSize(imageInfo);
                    result = bitmap.Height == size.Height && bitmap.Width == size.Width;
                }
            }

            _mMutex.ReleaseMutex();
            return result;
        }

        /// <summary>
        /// Загружаем в хранилише уменьшенное изображение по переданному пути.
        /// </summary>
        /// <param name="imageInfo"></param>
        /// <returns></returns>
        public Bitmap Get(AdImageInfoW imageInfo)
        {
            Size size = GetThumbnailSize(imageInfo);
            _mMutex.WaitOne();
            _mStorage.TryGetValue(imageInfo.Id, out Bitmap bitmap);
            if (bitmap == null || bitmap.Height != size.Height || bitmap.Width != size.Width)
            {
                _mMutex.ReleaseMutex(); // поток может работать дальше
                //bitmap = _mCore.LoadBitmap(size, imageInfo.path);
                bitmap = BitmapWorker.LoadBitmap(_mCore, size, imageInfo.Path);
                _mMutex.WaitOne();
                _mStorage[imageInfo.Id] = bitmap;
            }

            _mMutex.ReleaseMutex();
            return bitmap;
        }

        private Size GetThumbnailSize(AdImageInfoW imageInfo)
        {
            Size sizeMax = _mOptions.resultsOptions.ThumbnailSizeMax;
            return sizeMax.Width * imageInfo.Height > sizeMax.Height * imageInfo.Width
                       ? new Size(sizeMax.Width, (int)(sizeMax.Height * imageInfo.Height / imageInfo.Width))
                       : new Size((int)(sizeMax.Width * imageInfo.Width / imageInfo.Height), sizeMax.Height);
        }

        #endregion
    }
}
