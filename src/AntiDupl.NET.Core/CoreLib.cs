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

using System;
using System.Runtime.InteropServices;
using System.Threading;
using AntiDupl.NET.Core.Enums;
using AntiDupl.NET.Core.Original;

namespace AntiDupl.NET.Core
{
    public class CoreLib : IDisposable
    {
        private const uint VERSION_SIZE = 40;
        private const uint PAGE_SIZE = 16;

        private IntPtr m_handle = IntPtr.Zero;
        private CoreDll m_dll = null;

        //-----------Public functions----------------------------------------------

        public CoreLib(string userPath)
        {
            try
            {
                m_dll = new CoreDll();
            }
            catch
            {
                throw new Exception("Can't load core library!");
            }

            // TODO
            //if (Version.Compatible(GetVersion(CoreDll.VersionType.AntiDupl)))
            m_handle = m_dll.AdCreateW(userPath);
            //else throw new Exception("Incompatible core library version!");
        }

        ~CoreLib()
        {
            Dispose();
        }

        public void Release()
        {
            if (m_dll != null && m_handle != IntPtr.Zero)
            {
                if (m_dll.AdRelease(m_handle) == Error.AccessDenied)
                {
                    Stop();
                    Thread.Sleep(10);
                    m_dll.AdRelease(m_handle);
                }
            }
        }

        public void Dispose()
        {
            Release();
            if (m_dll != null)
            {
                //m_dll.Dispose();
                m_dll = null;
            }
            GC.SuppressFinalize(this);
        }

        public CoreVersion GetVersion(VersionType versionType)
        {
            try
            {
                sbyte[] versionB = new sbyte[VERSION_SIZE];
                IntPtr[] sizeB = new IntPtr[1];
                sizeB[0] = new IntPtr(VERSION_SIZE);
                GCHandle versionH = GCHandle.Alloc(versionB, GCHandleType.Pinned);
                GCHandle sizeH = GCHandle.Alloc(sizeB, GCHandleType.Pinned);
                try
                {
                    IntPtr versionP = versionH.AddrOfPinnedObject();
                    IntPtr sizeP = sizeH.AddrOfPinnedObject();
                    if (m_dll.AdVersionGet(versionType, versionP, sizeP) == Error.Ok)
                    {
                        return new CoreVersion(versionB);
                    }
                }
                finally
                {
                    versionH.Free();
                    sizeH.Free();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public bool IsInited()
        {
            return m_handle != IntPtr.Zero;
        }

        public bool IsWork()
        {
            CoreStatus status = StatusGet(ThreadType.Main, 0);
            if (status != null)
                return status.state != StateType.None;
            else
                return false;
        }

        public bool Stop()
        {
            return m_dll.AdStop(m_handle) == Error.Ok;
        }

        public bool Search()
        {
            return m_dll.AdSearch(m_handle) == Error.Ok;
        }

        public bool Load(FileType fileType, string fileName, bool check)
        {
            return m_dll.AdLoadW(m_handle, fileType, fileName, check ? Constants.TRUE : Constants.FALSE) == Error.Ok;
        }

        public bool Save(FileType fileType, string fileName)
        {
            return m_dll.AdSaveW(m_handle, fileType, fileName) == Error.Ok;
        }

        public bool Clear(FileType fileType)
        {
            return m_dll.AdClear(m_handle, fileType) == Error.Ok;
        }

        public bool SetDefaultOptions()
        {
            return m_dll.AdOptionsSet(m_handle, OptionsType.SetDefault, IntPtr.Zero) == Error.Ok;
        }

        public CoreStatistic GetStatistic()
        {
            try
            {
                object statisticO = new AdStatistic();
                byte[] statisticB = new byte[Marshal.SizeOf(statisticO)];
                GCHandle statisticH = GCHandle.Alloc(statisticB, GCHandleType.Pinned);
                try
                {
                    IntPtr statisticP = statisticH.AddrOfPinnedObject();
                    if (m_dll.AdStatisticGet(m_handle, statisticP) == Error.Ok)
                    {
                        AdStatistic statistic = (AdStatistic)Marshal.PtrToStructure(statisticP, statisticO.GetType());
                        return new CoreStatistic(ref statistic);
                    }
                }
                finally
                {
                    statisticH.Free();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public CoreStatus StatusGet(ThreadType threadType, int threadId)
        {
            try
            {
                object statusO = new AdStatusW();
                byte[] statusB = new byte[Marshal.SizeOf(statusO)];
                GCHandle statusH = GCHandle.Alloc(statusB, GCHandleType.Pinned);
                try
                {
                    IntPtr statusP = statusH.AddrOfPinnedObject();
                    if (m_dll.AdStatusGetW(m_handle, threadType, new IntPtr(threadId), statusP) == Error.Ok)
                    {
                        AdStatusW statusW = (AdStatusW)Marshal.PtrToStructure(statusP, statusO.GetType());
                        return new CoreStatus(ref statusW);
                    }
                }
                finally
                {
                    statusH.Free();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public bool SortResult(SortType sortType, bool increasing)
        {
            return m_dll.AdResultSort(m_handle, sortType, increasing ? Constants.TRUE : Constants.FALSE) == Error.Ok;
        }

        public bool ApplyToResult(GlobalActionType globalActionType)
        {
            return m_dll.AdResultApply(m_handle, globalActionType) == Error.Ok;
        }

        public bool ApplyToResult(LocalActionType localActionType, TargetType targetType)
        {
            return m_dll.AdResultApplyTo(m_handle, localActionType, targetType) == Error.Ok;
        }

        public bool CanApply(ActionEnableType actionEnableType)
        {
            try
            {
                int[] enableB = new int[1];
                GCHandle enableH = GCHandle.Alloc(enableB, GCHandleType.Pinned);
                try
                {
                    IntPtr enableP = enableH.AddrOfPinnedObject();
                    if (m_dll.AdCanApply(m_handle, actionEnableType, enableP) == Error.Ok)
                    {
                        return enableB[0] != Constants.FALSE;
                    }
                }
                finally
                {
                    enableH.Free();
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public bool RenameCurrent(RenameCurrentType renameCurrentType, string newFileName)
        {
            return m_dll.AdRenameCurrentW(m_handle, renameCurrentType, newFileName) == Error.Ok;
        }

        public bool MoveCurrentGroup(string directory)
        {
            return m_dll.AdMoveCurrentGroupW(m_handle, directory) == Error.Ok;
        }

        public bool RenameCurrentGroupAs(string fileName)
        {
            return m_dll.AdRenameCurrentGroupAsW(m_handle, fileName) == Error.Ok;
        }

        public AdResultW[] GetResult(uint startFrom, uint size)
        {
            uint resultSize = GetResultSize();
            if (resultSize > startFrom)
            {
                object resultObject = new AdResultW();
                int sizeOfResult = Marshal.SizeOf(resultObject);
                byte[] buffer = new byte[sizeOfResult * PAGE_SIZE];
                size = Math.Min(resultSize - startFrom, size);
                AdResultW[] results = new AdResultW[size];
                uint pageCount = (uint)(size / PAGE_SIZE + (size % PAGE_SIZE > 0 ? 1 : 0));
                for (uint page = 0; page < pageCount; ++page)
                {
                    UIntPtr[] pStartFrom = new UIntPtr[1];
                    pStartFrom[0] = new UIntPtr(startFrom + page * PAGE_SIZE);

                    UIntPtr[] pSize = new UIntPtr[1];
                    pSize[0] = new UIntPtr(PAGE_SIZE);

                    if (m_dll.AdResultGetW(m_handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                        Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
                        Marshal.UnsafeAddrOfPinnedArrayElement(pSize, 0)) == Error.Ok)
                    {
                        for (uint i = 0; i < pSize[0].ToUInt32(); ++i)
                        {
                            IntPtr pResult = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)(i * sizeOfResult));
                            AdResultW result = (AdResultW)Marshal.PtrToStructure(pResult, resultObject.GetType());
                            results[page * PAGE_SIZE + i] = result;
                        }

                    }
                }
                return results;
            }
            return null;
        }

        public uint GetResultSize()
        {
            try
            {
                UIntPtr[] startFromB = new UIntPtr[1];
                startFromB[0] = new UIntPtr(uint.MaxValue);
                GCHandle startFromH = GCHandle.Alloc(startFromB, GCHandleType.Pinned);
                try
                {
                    IntPtr startFromP = startFromH.AddrOfPinnedObject();
                    IntPtr resultP = new IntPtr(1);
                    IntPtr resultSizeP = new IntPtr(1);
                    if (m_dll.AdResultGetW(m_handle, startFromP, resultP, resultSizeP) == Error.InvalidStartPosition)
                    {
                        return startFromB[0].ToUInt32();
                    }
                }
                finally
                {
                    startFromH.Free();
                }
            }
            catch (Exception)
            {
            }
            return 0;
        }

        public bool SetSelection(uint startFrom, uint size, bool value)
        {
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(startFrom);
            return m_dll.AdSelectionSet(m_handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0), new UIntPtr(size),
                value ? Constants.TRUE : Constants.FALSE) == Error.Ok;
        }

        public bool[] GetSelection(uint startFrom, uint size)
        {
            int[] pSelection = new int[size];
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(startFrom);
            UIntPtr[] pSelectionSize = new UIntPtr[1];
            pSelectionSize[0] = new UIntPtr(size);
            if (m_dll.AdSelectionGet(m_handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                Marshal.UnsafeAddrOfPinnedArrayElement(pSelection, 0),
                Marshal.UnsafeAddrOfPinnedArrayElement(pSelectionSize, 0)) == Error.Ok)
            {
                bool[] selection = new bool[pSelectionSize[0].ToUInt32()];
                for (int i = 0; i < selection.Length; ++i)
                    selection[i] = pSelection[i] != Constants.FALSE;
                return selection;
            }
            return null;
        }

        public bool SetCurrent(int index)
        {
            return m_dll.AdCurrentSet(m_handle, new IntPtr(index)) == Error.Ok;
        }

        public int GetCurrent()
        {
            IntPtr[] index = new IntPtr[1];
            index[0] = new IntPtr();
            if (m_dll.AdCurrentGet(m_handle, Marshal.UnsafeAddrOfPinnedArrayElement(index, 0)) == Error.Ok)
            {
                return index[0].ToInt32();
            }
            return -1;
        }

        public AdGroup[] GetGroup(uint startFrom, uint size)
        {
            uint groupSize = GetGroupSize();
            //if (groupSize > startFrom)
            //{
            //    object groupObject = new AdGroup();
            //    int sizeOfGroup = Marshal.SizeOf(groupObject);
            //    size = Math.Min(groupSize - startFrom, size);
            //    byte[] buffer = new byte[sizeOfGroup * size];
            //    CoreGroup[] groups = new CoreGroup[size];
            //    UIntPtr[] pStartFrom = new UIntPtr[1];
            //    pStartFrom[0] = new UIntPtr(startFrom);
            //    UIntPtr[] pSize = new UIntPtr[1];
            //    pSize[0] = new UIntPtr(size);
            //    if (m_dll.AdGroupGet(m_handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
            //        Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
            //        Marshal.UnsafeAddrOfPinnedArrayElement(pSize, 0)) == Error.Ok)
            //    {
            //        for (uint i = 0; i < pSize[0].ToUInt32(); ++i)
            //        {
            //            IntPtr pGroup = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)(i * sizeOfGroup));
            //            AdGroup group = (AdGroup)Marshal.PtrToStructure(pGroup, groupObject.GetType());
            //            groups[i] = new CoreGroup(ref group, this);
            //        }
            //    }
            //    return groups;
            //}
            return null;
        }

        /// <summary>
        /// Возврашает общее количество групп.
        /// </summary>
        /// <returns></returns>
        public uint GetGroupSize()
        {
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(uint.MaxValue);
            if (m_dll.AdGroupGet(m_handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                new IntPtr(1), new IntPtr(1)) == Error.InvalidStartPosition)
            {
                return pStartFrom[0].ToUInt32();
            }
            return 0;
        }

        /// <summary>
        /// Возврашает массив CoreImageInfo содержащихся в переданной группе.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="startFrom"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public AdImageInfoW[] GetImageInfo(int groupId, uint startFrom, uint size)
        {
            uint imageInfoSize = GetImageInfoSize(groupId);
            //if (imageInfoSize > startFrom)
            //{
            //    object imageInfoObject = new AdImageInfoW();
            //    int sizeOfImageInfo = Marshal.SizeOf(imageInfoObject);
            //    byte[] buffer = new byte[sizeOfImageInfo * PAGE_SIZE];
            //    size = Math.Min(imageInfoSize - startFrom, size);
            //    CoreImageInfo[] imageInfos = new CoreImageInfo[size];
            //    uint pageCount = (uint)(size / PAGE_SIZE + (size % PAGE_SIZE > 0 ? 1 : 0));
            //    for (uint page = 0; page < pageCount; ++page)
            //    {
            //        UIntPtr[] pStartFrom = new UIntPtr[1];
            //        pStartFrom[0] = new UIntPtr(startFrom + page * PAGE_SIZE);

            //        UIntPtr[] pSize = new UIntPtr[1];
            //        pSize[0] = new UIntPtr(PAGE_SIZE);

            //        if (m_dll.AdImageInfoGetW(m_handle, new IntPtr(groupId), Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
            //            Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
            //            Marshal.UnsafeAddrOfPinnedArrayElement(pSize, 0)) == Error.Ok)
            //        {
            //            for (uint i = 0; i < pSize[0].ToUInt32(); ++i)
            //            {
            //                IntPtr pImageInfo = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)(i * sizeOfImageInfo));
            //                AdImageInfoW imageInfo = (AdImageInfoW)Marshal.PtrToStructure(pImageInfo, imageInfoObject.GetType());
            //                imageInfos[page * PAGE_SIZE + i] = new CoreImageInfo(ref imageInfo);
            //            }

            //        }
            //    }
            //    return imageInfos;
            //}
            return null;
        }

        /// <summary>
        /// Возвращает количество изображений в переданной группе.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public uint GetImageInfoSize(int groupId)
        {
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(uint.MaxValue);
            if (m_dll.AdImageInfoGetW(m_handle, new IntPtr(groupId), Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                new IntPtr(1), new IntPtr(1)) == Error.InvalidStartPosition)
            {
                return pStartFrom[0].ToUInt32();
            }
            return 0;
        }

        public bool SetSelection(int groupId, int index, SelectionType selectionType)
        {
            return m_dll.AdImageInfoSelectionSet(m_handle, new IntPtr(groupId), new IntPtr(index), selectionType) == Error.Ok;
        }

        public bool[] GetSelection(int groupId, uint startFrom, uint size)
        {
            int[] pSelection = new int[size];
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(startFrom);
            UIntPtr[] pSelectionSize = new UIntPtr[1];
            pSelectionSize[0] = new UIntPtr(size);
            if (m_dll.AdImageInfoSelectionGet(m_handle, new IntPtr(groupId), Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                Marshal.UnsafeAddrOfPinnedArrayElement(pSelection, 0),
                Marshal.UnsafeAddrOfPinnedArrayElement(pSelectionSize, 0)) == Error.Ok)
            {
                bool[] selection = new bool[pSelectionSize[0].ToUInt32()];
                for (int i = 0; i < selection.Length; ++i)
                    selection[i] = pSelection[i] != Constants.FALSE;
                return selection;
            }
            return null;
        }

        public bool Rename(int groupId, int index, string newFileName)
        {
            return m_dll.AdImageInfoRenameW(m_handle, new IntPtr(groupId), new IntPtr(index), newFileName) == Error.Ok;
        }

        public Error LoadBitmap(string path, AdBitmap[] pBitmap)
        {
            return m_dll.AdLoadBitmapW(m_handle, path, Marshal.UnsafeAddrOfPinnedArrayElement(pBitmap, 0));
        }
        //public System.Drawing.Bitmap LoadBitmap(int width, int height, string path)
        //{
        //    if (height * width == 0)
        //        return null;

        //    System.Drawing.Bitmap bitmap = null;
        //    try
        //    {
        //        bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        //    }
        //    catch (System.Exception)
        //    {
        //        GC.Collect();
        //        try
        //        {
        //            bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        //        }
        //        catch (System.Exception)
        //        {
        //            return null;
        //        }
        //    }
        //    System.Drawing.Imaging.BitmapData bitmapData = new System.Drawing.Imaging.BitmapData();
        //    bitmapData = bitmap.LockBits(
        //        new System.Drawing.Rectangle(0, 0, width, height),
        //        System.Drawing.Imaging.ImageLockMode.WriteOnly,
        //        System.Drawing.Imaging.PixelFormat.Format32bppArgb,
        //        bitmapData);
        //    CoreDll.adBitmap[] pBitmap = new CoreDll.adBitmap[1];
        //    pBitmap[0].width = (uint)bitmapData.Width;
        //    pBitmap[0].height = (uint)bitmapData.Height;
        //    pBitmap[0].stride = bitmapData.Stride;
        //    pBitmap[0].format = CoreDll.PixelFormatType.Argb32;
        //    pBitmap[0].data = bitmapData.Scan0;
        //    CoreDll.Error error = m_dll.adLoadBitmapW(m_handle, path, Marshal.UnsafeAddrOfPinnedArrayElement(pBitmap, 0));
        //    bitmap.UnlockBits(bitmapData);
        //    return error == CoreDll.Error.Ok ? bitmap : null;
        //}

        ///// <summary>
        ///// Возврашает загруженное изображение по заланному пути и заданного размера.
        ///// </summary>
        ///// <param name="size"></param>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public System.Drawing.Bitmap LoadBitmap(System.Drawing.Size size, string path)
        //{
        //    return LoadBitmap(size.Width, size.Height, path);
        //}

        //public System.Drawing.Bitmap LoadBitmap(CoreImageInfo imageInfo)
        //{
        //    return LoadBitmap((int)imageInfo.width, (int)imageInfo.height, imageInfo.path);
        //}

        //-----------Public properties----------------------------------------------

        #region Public properties

        public CoreSearchOptions searchOptions
        {
            get
            {
                AdSearchOptions[] options = new AdSearchOptions[1];
                m_dll.AdOptionsGet(m_handle, OptionsType.Search, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
                return new CoreSearchOptions(options[0]);
            }
            set
            {
                AdSearchOptions[] options = new AdSearchOptions[1];
                value.ConvertTo(ref options[0]);
                m_dll.AdOptionsSet(m_handle, OptionsType.Search, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
            }
        }

        public AdCompareOptions compareOptions { get; set; }
        //{
        //    get
        //    {
        //        AdCompareOptions[] options = new AdCompareOptions[1];
        //        m_dll.AdOptionsGet(m_handle, OptionsType.Compare, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
        //        return new AdCompareOptions(ref options[0]);
        //    }
        //    set
        //    {
        //        AdCompareOptions[] options = new AdCompareOptions[1];
        //        value.ConvertTo(ref options[0]);
        //        m_dll.AdOptionsSet(m_handle, OptionsType.Compare, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
        //    }
        //}

        public AdDefectOptions defectOptions { get; set; }
        //{
        //    get
        //    {
        //        AdDefectOptions[] options = new AdDefectOptions[1];
        //        m_dll.AdOptionsGet(m_handle, OptionsType.Defect, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
        //        return new CoreDefectOptions(ref options[0]);
        //    }
        //    set
        //    {
        //        AdDefectOptions[] options = new AdDefectOptions[1];
        //        value.ConvertTo(ref options[0]);
        //        m_dll.AdOptionsSet(m_handle, OptionsType.Defect, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
        //    }
        //}

        public AdAdvancedOptions advancedOptions { get; set; }
        //{
        //    get
        //    {
        //        AdAdvancedOptions[] options = new AdAdvancedOptions[1];
        //        m_dll.AdOptionsGet(m_handle, OptionsType.Advanced, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
        //        return options[0];
        //    }
        //    set
        //    {
        //        AdAdvancedOptions[] options = new AdAdvancedOptions[1]; //создаем массив из одного значения
        //        //value.ConvertTo(ref options[0]); //конвертируем переданный класс
        //        options[0] = value;
        //        m_dll.AdOptionsSet(m_handle, OptionsType.Advanced, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
        //    }
        //}

        public AdPathWithSubFolderW[] searchPath
        {
            get
            {
                return GetPath(PathType.Search);
            }
            set
            {
                SetPath(PathType.Search, value);
            }
        }

        public AdPathWithSubFolderW[] ignorePath
        {
            get
            {
                return GetPath(PathType.Ignore);
            }
            set
            {
                SetPath(PathType.Ignore, value);
            }
        }

        public AdPathWithSubFolderW[] validPath
        {
            get
            {
                return GetPath(PathType.Valid);
            }
            set
            {
                SetPath(PathType.Valid, value);
            }
        }

        public AdPathWithSubFolderW[] deletePath
        {
            get
            {
                return GetPath(PathType.Delete);
            }
            set
            {
                SetPath(PathType.Delete, value);
            }
        }

        #endregion

        //-----------Private functions:--------------------------------------------
        #region private

        static private string BufferToString(char[] buffer, int startIndex, int maxSize)
        {
            if (startIndex >= buffer.Length)
                return null;
            int i = 0, n = Math.Min(maxSize, buffer.Length - startIndex);
            for (; i < n; ++i)
            {
                if (buffer[startIndex + i] == (char)0)
                    break;
            }
            return new string(buffer, startIndex, i);
        }

        private AdPathWithSubFolderW[] GetPath(PathType pathType)
        {
            AdPathWithSubFolderW[] pathWSF = Array.Empty<AdPathWithSubFolderW>();
            IntPtr[] size = new IntPtr[1];
            string[] path = new string[0];
            if (m_dll.AdPathGetW(m_handle, pathType, new IntPtr(1), Marshal.UnsafeAddrOfPinnedArrayElement(size, 0)) ==
                            Error.OutputBufferIsTooSmall)
            {
                char[] buffer = new char[(Constants.MAX_PATH_EX + 1) * size[0].ToInt32()];
                if (m_dll.AdPathGetW(m_handle, pathType, Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
                    Marshal.UnsafeAddrOfPinnedArrayElement(size, 0)) == Error.Ok)
                {
                    pathWSF = new AdPathWithSubFolderW[size[0].ToInt32()];
                    for (int i = 0; i < size[0].ToInt32(); ++i)
                    {
                        pathWSF[i] = new AdPathWithSubFolderW();
                        pathWSF[i].Path = BufferToString(buffer, i * (Constants.MAX_PATH_EX + 1), Constants.MAX_PATH_EX);
                        if (buffer[(Constants.MAX_PATH_EX + 1) * i + Constants.MAX_PATH_EX] == (char)1)
                            pathWSF[i].EnableSubFolder = true;
                        else
                            pathWSF[i].EnableSubFolder = false;
                    }
                }
            }
            return pathWSF;
        }

        private bool SetPath(PathType pathType, AdPathWithSubFolderW[] path)
        {
            char[] buffer = new char[path.Length * (Constants.MAX_PATH_EX + 1)];
            for (int i = 0; i < path.Length; i++)
            {
                path[i].Path.CopyTo(0, buffer, i * (Constants.MAX_PATH_EX + 1), path[i].Path.Length);
                buffer[(Constants.MAX_PATH_EX + 1) * i + Constants.MAX_PATH_EX] = path[i].EnableSubFolder ? (char)1 : (char)0;
            }

            return m_dll.AdPathWithSubFolderSetW(m_handle,
                pathType,
                Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
                new IntPtr(path.Length)) == Error.Ok;
        }

        #endregion
    };
}
