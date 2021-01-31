#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA5392 // Use DefaultDllImportSearchPaths attribute for P/Invokes

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AntiDupl.NET.Core.Enums;

// TODO: temp
#pragma warning disable 1591

namespace AntiDupl.NET.Core.Original
{
    public class AntiDuplCore : IAsyncDisposable, IDisposable
    {
        #region Constants

        private const string DllPath = "AntiDupl.dll";

        private const uint PageSize = 16;

        private IntPtr Handle { get; }

        #endregion

        #region Properties

        // TODO: interface and generic method for options/paths?

        public AdSearchOptions SearchOptions
        {
            get
            {
                AdSearchOptions[] options = new AdSearchOptions[1];
                adOptionsGet(Handle, OptionsType.Search, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));

                return options[0];
            }
            set
            {
                AdSearchOptions[] options = new AdSearchOptions[1];
                options[0] = value;
                adOptionsSet(Handle, OptionsType.Search, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
            }
        }

        public AdCompareOptions CompareOptions
        {
            get
            {
                AdCompareOptions[] options = new AdCompareOptions[1];
                adOptionsGet(Handle, OptionsType.Compare, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));

                return options[0];
            }
            set
            {
                AdCompareOptions[] options = new AdCompareOptions[1];
                options[0] = value;
                adOptionsSet(Handle, OptionsType.Compare, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
            }
        }

        public AdDefectOptions DefectOptions
        {
            get
            {
                AdDefectOptions[] options = new AdDefectOptions[1];
                adOptionsGet(Handle, OptionsType.Defect, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));

                return options[0];
            }
            set
            {
                AdDefectOptions[] options = new AdDefectOptions[1];
                options[0] = value;
                adOptionsSet(Handle, OptionsType.Defect, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
            }
        }

        public AdAdvancedOptions AdvancedOptions
        {
            get
            {
                AdAdvancedOptions[] options = new AdAdvancedOptions[1];
                adOptionsGet(Handle, OptionsType.Advanced, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));

                return options[0];
            }
            set
            {
                AdAdvancedOptions[] options = new AdAdvancedOptions[1];
                options[0] = value;
                adOptionsSet(Handle, OptionsType.Advanced, Marshal.UnsafeAddrOfPinnedArrayElement(options, 0));
            }
        }

        public AdPathWithSubFolderW[] SearchPaths
        {
            get => GetPath(PathType.Search);
            set => SetPath(PathType.Search, value);
        }

        public AdPathWithSubFolderW[] IgnorePaths
        {
            get => GetPath(PathType.Ignore);
            set => SetPath(PathType.Ignore, value);
        }

        public AdPathWithSubFolderW[] ValidPaths
        {
            get => GetPath(PathType.Valid);
            set => SetPath(PathType.Valid, value);
        }

        public AdPathWithSubFolderW[] DeletePaths
        {
            get => GetPath(PathType.Delete);
            set => SetPath(PathType.Delete, value);
        }

        #endregion

        #region Constructors

        public AntiDuplCore() => Handle = IntPtr.Zero;

        public AntiDuplCore(string userPath) => Handle = adCreateW(userPath);

        #endregion

        #region Dispose

        private void ReleaseUnmanagedResources()
        {
            if (Handle == IntPtr.Zero) return;

            if (adRelease(Handle) != Error.AccessDenied) return;

            Stop();
            Task.Delay(10).Wait();
            adRelease(Handle);
        }

        public bool IsDisposed { get; private set; }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="Dispose()"/>
        /// <param name="disposing">Dispose static fields?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                // Occurs only if called by programmer. Dispose static things here
            }

            ReleaseUnmanagedResources();

            IsDisposed = true;
        }

        /// <inheritdoc />
        public ValueTask DisposeAsync()
        {
#pragma warning disable CA1031 // Do not catch general exception types

            try
            {
                Dispose();

                return default;
            }
            catch (Exception exception)
            {
                return ValueTask.FromException(exception);
            }

#pragma warning restore CA1031 // Do not catch general exception types
        }

        ~AntiDuplCore() => Dispose(false);

        #endregion

        #region DllImports

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adOptionsGet(IntPtr handle, OptionsType optionsType, IntPtr pOptions);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adOptionsSet(IntPtr handle, OptionsType optionsType, IntPtr pOptions);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern IntPtr adCreateW(string userPath);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adRelease(IntPtr handle);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adStop(IntPtr handle);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSearch(IntPtr handle);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adLoadW(IntPtr handle, FileType fileType, string fileName, int check);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSaveW(IntPtr handle, FileType fileType, string fileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adClear(IntPtr handle, FileType fileType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adPathWithSubFolderSetW(IntPtr handle, PathType pathType, IntPtr pPaths, IntPtr pathSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adPathGetW(IntPtr handle, PathType pathType, IntPtr pPath, IntPtr pPathSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adStatisticGet(IntPtr handle, IntPtr pStatistic);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adStatusGetW(IntPtr handle, ThreadType threadType, IntPtr threadId, IntPtr pStatusW);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultSort(IntPtr handle, SortType sortType, int increasing);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultApply(IntPtr handle, GlobalActionType globalActionType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultApplyTo(IntPtr handle, LocalActionType localActionType, TargetType targetType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adCanApply(IntPtr handle, ActionEnableType actionEnableType, IntPtr pEnable);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adRenameCurrentW(IntPtr handle, RenameCurrentType renameCurrentType, string newFileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adMoveCurrentGroupW(IntPtr handle, string directory);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adRenameCurrentGroupAsW(IntPtr handle, string fileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultGetW(IntPtr handle, IntPtr pStartFrom, IntPtr pResult, IntPtr pResultSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSelectionSet(IntPtr handle, IntPtr pStartFrom, UIntPtr size, int value);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSelectionGet(IntPtr handle, IntPtr pStartFrom, IntPtr pSelection, IntPtr pSelectionSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adCurrentSet(IntPtr handle, IntPtr index);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adCurrentGet(IntPtr handle, IntPtr pIndex);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adGroupGet(IntPtr handle, IntPtr pStartFrom, IntPtr pGroup, IntPtr pGroupSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoGetW(IntPtr handle, IntPtr groupId, IntPtr pStartFrom, IntPtr pImageInfo, IntPtr pImageInfoSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoSelectionSet(IntPtr handle, IntPtr groupId, IntPtr index, SelectionType selectionType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoSelectionGet(IntPtr handle, IntPtr groupId, IntPtr pStartFrom, IntPtr pSelection, IntPtr pSelectionSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoRenameW(IntPtr handle, IntPtr groupId, IntPtr index, string newFileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adLoadBitmapW(IntPtr handle, string fileName, IntPtr pBitmap);

        #endregion

        #region Public methods

        public Error Stop() => adStop(Handle);

        public Error Search() => adSearch(Handle);

        public Error Load(FileType fileType, string fileName, bool check) => adLoadW(Handle, fileType, fileName, Convert.ToInt32(check));

        public Error Save(FileType fileType, string fileName) => adSaveW(Handle, fileType, fileName);

        public Error Clear(FileType fileType) => adClear(Handle, fileType);

        public Error SetPath(PathType pathType, AdPathWithSubFolderW[] path)
        {
            char[] buffer = new char[path.Length * (Constants.MaxPathEx + 1)];

            for (int i = 0; i < path.Length; i++)
            {
                path[i].Path.CopyTo(0, buffer, i * (Constants.MaxPathEx + 1), path[i].Path.Length);
                buffer[(Constants.MaxPathEx + 1) * i + Constants.MaxPathEx] = path[i].EnableSubFolder ? '1' : '0';
            }

            return adPathWithSubFolderSetW(Handle, pathType, Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), new IntPtr(path.Length));
        }

        private string BufferToString(char[] buffer, int startIndex, int maxSize)
        {
            if (startIndex >= buffer.Length) return null;

            int i = 0, n = Math.Min(maxSize, buffer.Length - startIndex);

            for (; i < n; ++i)
            {
                if (buffer[startIndex + i] == '0') break;
            }

            return new string(buffer, startIndex, i);
        }

        public AdPathWithSubFolderW[] GetPath(PathType pathType)
        {
            AdPathWithSubFolderW[] pathWsf = Array.Empty<AdPathWithSubFolderW>();
            IntPtr[] size = new IntPtr[1];

            if (adPathGetW(Handle, pathType, new IntPtr(1), Marshal.UnsafeAddrOfPinnedArrayElement(size, 0)) != Error.OutputBufferIsTooSmall)
                return pathWsf;

            char[] buffer = new char[(Constants.MaxPathEx + 1) * size[0].ToInt32()];

            if (adPathGetW(Handle, pathType, Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), Marshal.UnsafeAddrOfPinnedArrayElement(size, 0)) != Error.Ok)
                return pathWsf;

            pathWsf = new AdPathWithSubFolderW[size[0].ToInt32()];

            for (int i = 0; i < size[0].ToInt32(); ++i)
            {
                pathWsf[i] = new AdPathWithSubFolderW
                {
                    Path = BufferToString(buffer, i * (Constants.MaxPathEx + 1), Constants.MaxPathEx),
                    EnableSubFolder = buffer[(Constants.MaxPathEx + 1) * i + Constants.MaxPathEx] == '1'
                };
            }

            return pathWsf;
        }

        public AdStatistic GetStatistic()
        {
            // TODO: remove?

            try
            {
                object statisticO = new AdStatistic();
                byte[] statisticB = new byte[Marshal.SizeOf(statisticO)];
                GCHandle statisticH = GCHandle.Alloc(statisticB, GCHandleType.Pinned);

                try
                {
                    IntPtr statisticP = statisticH.AddrOfPinnedObject();

                    if (adStatisticGet(Handle, statisticP) == Error.Ok)
                    {
                        AdStatistic statistic = (AdStatistic)Marshal.PtrToStructure(statisticP, statisticO.GetType());

                        return statistic;
                    }
                }
                finally
                {
                    statisticH.Free();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public AdStatusW StatusGet(ThreadType threadType, int threadId)
        {
            // TODO: remove?

            try
            {
                object statusO = new AdStatusW();
                byte[] statusB = new byte[Marshal.SizeOf(statusO)];
                GCHandle statusH = GCHandle.Alloc(statusB, GCHandleType.Pinned);

                try
                {
                    IntPtr statusP = statusH.AddrOfPinnedObject();

                    if (adStatusGetW(Handle, threadType, new IntPtr(threadId), statusP) == Error.Ok)
                    {
                        AdStatusW statusW = (AdStatusW)Marshal.PtrToStructure(statusP, statusO.GetType());

                        return statusW;
                    }
                }
                finally
                {
                    statusH.Free();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public Error SortResult(SortType sortType, bool increasing) => adResultSort(Handle, sortType, Convert.ToInt32(increasing));

        public Error ApplyToResult(GlobalActionType globalActionType) => adResultApply(Handle, globalActionType);

        public Error ApplyToResult(LocalActionType localActionType, TargetType targetType) => adResultApplyTo(Handle, localActionType, targetType);

        public bool CanApply(ActionEnableType actionEnableType)
        {
            // TODO: modify

            try
            {
                int[] enableB = new int[1];
                GCHandle enableH = GCHandle.Alloc(enableB, GCHandleType.Pinned);

                try
                {
                    IntPtr enableP = enableH.AddrOfPinnedObject();

                    if (adCanApply(Handle, actionEnableType, enableP) == Error.Ok) return Convert.ToBoolean(enableB[0]);
                }
                finally
                {
                    enableH.Free();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public Error RenameCurrent(RenameCurrentType renameCurrentType, string newFileName) => adRenameCurrentW(Handle, renameCurrentType, newFileName);

        public Error MoveCurrentGroup(string directory) => adMoveCurrentGroupW(Handle, directory);

        public Error RenameCurrentGroupAs(string fileName) => adRenameCurrentGroupAsW(Handle, fileName);

        public AdResultW[] GetResult(uint startFrom, uint size)
        {
            uint resultSize = GetResultSize();

            if (resultSize <= startFrom) return null;

            object resultObject = new AdResultW();
            int sizeOfResult = Marshal.SizeOf(resultObject);
            byte[] buffer = new byte[sizeOfResult * PageSize];
            size = Math.Min(resultSize - startFrom, size);
            AdResultW[] results = new AdResultW[size];
            uint pageCount = (uint)(size / PageSize + (size % PageSize > 0 ? 1 : 0));

            for (uint page = 0; page < pageCount; ++page)
            {
                UIntPtr[] pStartFrom = new UIntPtr[1];
                pStartFrom[0] = new UIntPtr(startFrom + page * PageSize);

                UIntPtr[] pSize = new UIntPtr[1];
                pSize[0] = new UIntPtr(PageSize);

                if (adResultGetW(Handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                                 Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
                                 Marshal.UnsafeAddrOfPinnedArrayElement(pSize, 0)) != Error.Ok)
                    continue;

                for (uint i = 0; i < pSize[0].ToUInt32(); ++i)
                {
                    IntPtr pResult = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)(i * sizeOfResult));
                    AdResultW result = (AdResultW)Marshal.PtrToStructure(pResult, resultObject.GetType());
                    results[page * PageSize + i] = result;
                }
            }

            return results;
        }

        public uint GetResultSize()
        {
            // TODO

            try
            {
                UIntPtr[] startFromB = new UIntPtr[1];
                startFromB[0] = new UIntPtr(uint.MaxValue);
                GCHandle startFromH = GCHandle.Alloc(startFromB, GCHandleType.Pinned);

                try
                {
                    IntPtr startFromP = startFromH.AddrOfPinnedObject();
                    IntPtr resultP = new(1);
                    IntPtr resultSizeP = new(1);

                    if (adResultGetW(Handle, startFromP, resultP, resultSizeP) == Error.InvalidStartPosition) return startFromB[0].ToUInt32();
                }
                finally
                {
                    startFromH.Free();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return 0;
        }

        public Error SetSelection(uint startFrom, uint size, bool value)
        {
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(startFrom);

            return adSelectionSet(Handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0), new UIntPtr(size),
                                  Convert.ToInt32(value));
        }

        public bool[] GetSelection(uint startFrom, uint size)
        {
            int[] pSelection = new int[size];
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(startFrom);
            UIntPtr[] pSelectionSize = new UIntPtr[1];
            pSelectionSize[0] = new UIntPtr(size);

            if (adSelectionGet(Handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                               Marshal.UnsafeAddrOfPinnedArrayElement(pSelection, 0),
                               Marshal.UnsafeAddrOfPinnedArrayElement(pSelectionSize, 0)) != Error.Ok) return null;

            bool[] selection = new bool[pSelectionSize[0].ToUInt32()];
            for (int i = 0; i < selection.Length; ++i)
                selection[i] = Convert.ToBoolean(pSelection[i]);

            return selection;
        }

        public Error SetCurrent(int index) => adCurrentSet(Handle, new IntPtr(index));

        public int GetCurrent()
        {
            IntPtr[] index = new IntPtr[1];
            index[0] = new IntPtr();

            if (adCurrentGet(Handle, Marshal.UnsafeAddrOfPinnedArrayElement(index, 0)) == Error.Ok) return index[0].ToInt32();

            return -1;
        }

        public AdGroup[] GetGroup(uint startFrom, uint size)
        {
            uint groupSize = GetGroupSize();

            if (groupSize <= startFrom) return null;

            object groupObject = new AdGroup();
            int sizeOfGroup = Marshal.SizeOf(groupObject);
            size = Math.Min(groupSize - startFrom, size);
            byte[] buffer = new byte[sizeOfGroup * size];
            AdGroup[] groups = new AdGroup[size];
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(startFrom);
            UIntPtr[] pSize = new UIntPtr[1];
            pSize[0] = new UIntPtr(size);

            if (adGroupGet(Handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                           Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
                           Marshal.UnsafeAddrOfPinnedArrayElement(pSize, 0)) != Error.Ok)
                return groups;

            for (uint i = 0; i < pSize[0].ToUInt32(); ++i)
            {
                IntPtr pGroup = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)(i * sizeOfGroup));
                AdGroup group = (AdGroup)Marshal.PtrToStructure(pGroup, groupObject.GetType());
                groups[i] = group;
            }

            return groups;
        }

        public uint GetGroupSize()
        {
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(uint.MaxValue);

            return adGroupGet(Handle, Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0), new IntPtr(1),
                              new IntPtr(1)) == Error.InvalidStartPosition
                       ? pStartFrom[0].ToUInt32()
                       : 0;
        }

        public AdImageInfoW[] GetImageInfo(int groupId, uint startFrom, uint size)
        {
            uint imageInfoSize = GetImageInfoSize(groupId);

            if (imageInfoSize <= startFrom) return null;

            object imageInfoObject = new AdImageInfoW();
            int sizeOfImageInfo = Marshal.SizeOf(imageInfoObject);
            byte[] buffer = new byte[sizeOfImageInfo * PageSize];
            size = Math.Min(imageInfoSize - startFrom, size);
            AdImageInfoW[] imageInfos = new AdImageInfoW[size];
            uint pageCount = (uint)(size / PageSize + (size % PageSize > 0 ? 1 : 0));
            for (uint page = 0; page < pageCount; ++page)
            {
                UIntPtr[] pStartFrom = new UIntPtr[1];
                pStartFrom[0] = new UIntPtr(startFrom + page * PageSize);

                UIntPtr[] pSize = new UIntPtr[1];
                pSize[0] = new UIntPtr(PageSize);

                if (adImageInfoGetW(Handle, new IntPtr(groupId),
                                    Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                                    Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0),
                                    Marshal.UnsafeAddrOfPinnedArrayElement(pSize, 0)) != Error.Ok)
                    continue;

                for (uint i = 0; i < pSize[0].ToUInt32(); ++i)
                {
                    IntPtr pImageInfo = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)(i * sizeOfImageInfo));
                    AdImageInfoW imageInfo = (AdImageInfoW)Marshal.PtrToStructure(pImageInfo, imageInfoObject.GetType());
                    imageInfos[page * PageSize + i] = imageInfo;
                }
            }

            return imageInfos;
        }

        public uint GetImageInfoSize(int groupId)
        {
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(uint.MaxValue);

            return adImageInfoGetW(Handle, new IntPtr(groupId), Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                                   new IntPtr(1), new IntPtr(1)) == Error.InvalidStartPosition
                       ? pStartFrom[0].ToUInt32()
                       : 0;
        }

        public Error SetSelection(int groupId, int index, SelectionType selectionType) => adImageInfoSelectionSet(Handle, new IntPtr(groupId), new IntPtr(index), selectionType);

        public bool[] GetSelection(int groupId, uint startFrom, uint size)
        {
            int[] pSelection = new int[size];
            UIntPtr[] pStartFrom = new UIntPtr[1];
            pStartFrom[0] = new UIntPtr(startFrom);
            UIntPtr[] pSelectionSize = new UIntPtr[1];
            pSelectionSize[0] = new UIntPtr(size);

            if (adImageInfoSelectionGet(Handle, new IntPtr(groupId),
                                        Marshal.UnsafeAddrOfPinnedArrayElement(pStartFrom, 0),
                                        Marshal.UnsafeAddrOfPinnedArrayElement(pSelection, 0),
                                        Marshal.UnsafeAddrOfPinnedArrayElement(pSelectionSize, 0)) != Error.Ok)
                return null;

            bool[] selection = new bool[pSelectionSize[0].ToUInt32()];
            for (int i = 0; i < selection.Length; ++i) selection[i] = Convert.ToBoolean(pSelection[i]);

            return selection;
        }

        public Error Rename(int groupId, int index, string newFileName) => adImageInfoRenameW(Handle, new IntPtr(groupId), new IntPtr(index), newFileName);

        public Error LoadBitmap(string path, AdBitmap[] pBitmap) => adLoadBitmapW(Handle, path, Marshal.UnsafeAddrOfPinnedArrayElement(pBitmap, 0));

        #endregion
    }
}

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA5392 // Use DefaultDllImportSearchPaths attribute for P/Invokes
