/*
* AntiDupl.NET Program (http://ermig1979.github.io/AntiDupl).
*
* Copyright (c) 2002-2018 Yermalayeu Ihar, 2013-2018 Borisov Dmitry.
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

namespace AntiDupl.NET.Core
{
    public class CoreDll
    {
        private const string DllPath = "AntiDupl.dll";

        //TODO
        //public CoreDll() : base("runtimes/win-x64/native/AntiDupl.dll") { }
        public CoreDll() //: base("AntiDupl.dll")
        {
            //
        }


        //-----------API constants:--------------------------------------------

        public const int FALSE = 0;
        public const int TRUE = 1;

        public const int MAX_PATH = 260;
        public const int MAX_PATH_EX = 32768;

        //-----------API enumerations------------------------------------------

        public enum Error : int
        {
            Ok = 0,
            Unknown = 1,
            AccessDenied = 2,
            InvalidPointer = 3,
            InvalidFileFormat = 4,
            InvalidPathType = 5,
            InvalidOptionsType = 6,
            InvalidFileType = 7,
            InvalidSortType = 8,
            InvalidGlobalActionType = 9,
            InvalidThreadId = 10,
            InvalidStartPosition = 11,
            OutputBufferIsTooSmall = 12,
            FileIsNotExists = 13,
            CantOpenFile = 14,
            CantCreateFile = 15,
            CantReadFile = 16,
            CantWriteFile = 17,
            InvalidFileName = 18,
            InvalidLocalActionType = 19,
            InvalidTargetType = 20,
            InvalidIndex = 21,
            ZeroTarget = 22,
            PathTooLong = 23,
            CantLoadImage = 24,
            InvalidBitmap = 25,
            InvalidThreadType = 26,
            InvalidActionEnableType = 27,
            InvalidParameterCombination = 28,
            InvalidRenameCurrentType = 29,
            InvalidInfoType = 30,
            InvalidGroupId = 31,
            InvalidSelectionType = 32,
        }

        public enum PathType : int
        {
            Search = 0,
            Ignore = 1,
            Valid = 2,
            Delete = 3,
        }

        public enum OptionsType : int
        {
            SetDefault = -1,
            Search = 0,
            Compare = 1,
            Defect = 2,
            Advanced = 3,
        }

        public enum FileType : int
        {
            Options = 0,
            Result = 1,
            MistakeDataBase = 2,
            ImageDataBase = 3,
            Temporary = 4,
        }

        public enum SortType : int
        {
            ByType = 0,
            BySortedPath = 1,
            BySortedName = 2,
            BySortedDirectory = 3,
            BySortedSize = 4,
            BySortedTime = 5,
            BySortedType = 6,
            BySortedWidth = 7,
            BySortedHeight = 8,
            BySortedArea = 9,
            BySortedRatio = 10,
            BySortedBlockiness = 11,
            BySortedBlurring = 12,
            ByFirstPath = 13,
            ByFirstName = 14,
            ByFirstDirectory = 15,
            ByFirstSize = 16,
            ByFirstTime = 17,
            ByFirstType = 18,
            ByFirstWidth = 19,
            ByFirstHeight = 20,
            ByFirstArea = 21,
            ByFirstRatio = 22,
            ByFirstBlockiness = 23,
            ByFirstBlurring = 24,
            BySecondPath = 25,
            BySecondName = 26,
            BySecondDirectory = 27,
            BySecondSize = 28,
            BySecondTime = 29,
            BySecondType = 30,
            BySecondWidth = 31,
            BySecondHeight = 32,
            BySecondArea = 33,
            BySecondRatio = 34,
            BySecondBlockiness = 35,
            BySecondBlurring = 36,
            ByDefect = 37,
            ByDifference = 38,
            ByTransform = 39,
            ByGroup = 40,
            ByGroupSize = 41,
            ByHint = 42,
        }

        public enum GlobalActionType : int
        {
            SetHint = 0,
            SetGroup = 1,
            Refresh = 2,
            Undo = 3,
            Redo = 4,
        }

        public enum LocalActionType : int //то же что и  enum adLocalActionType : adInt32, также еще в class HotKeyOptions enum Action
        {
            DeleteDefect = 0,
            DeleteFirst = 1,
            DeleteSecond = 2,
            DeleteBoth = 3,
            RenameFirstToSecond = 4,
            RenameSecondToFirst = 5,
            RenameFirstLikeSecond = 6,
            RenameSecondLikeFirst = 7,
            MoveFirstToSecond = 8,
            MoveSecondToFirst = 9,
            MoveAndRenameFirstToSecond = 10,
            MoveAndRenameSecondToFirst = 11,
            PerformHint = 12,
            Mistake = 13,
        }

        public enum ActionEnableType : int
        {
            Any = 0,
            Defect = 1,
            DuplPair = 2,
            PerformHint = 3,
            Undo = 4,
            Redo = 5,
        }

        public enum TargetType : int
        {
            Current = 0,
            Selected = 1,
        }

        public enum RenameCurrentType : int
        {
            First = 0,
            Second = 1,
        }

        public enum StateType : int
        {
            None = 0,
            Work = 1,
            Wait = 2,
            Stop = 3,
        }

        public enum ResultType : int
        {
            None = 0,
            DefectImage = 1,
            DuplImagePair = 2,
        }

        public enum ImageType : int
        {
            None = 0,
            Bmp = 1,
            Gif = 2,
            Jpeg = 3,
            Png = 4,
            Tiff = 5,
            Emf = 6,
            Wmf = 7,
            Exif = 8,
            Icon = 9,
            Jp2 = 10,
            Psd = 11,
            Dds = 12,
            Tga = 13,
            Webp = 14,
        }

        public enum DefectType : int
        {
            None = 0,
            Unknown = 1,
            JpegEndMarkerIsAbsent = 2,
            Blockiness = 3,
            Blurring = 4,
        }

        public enum TransformType : int
        {
            Turn_0 = 0,
            Turn_90 = 1,
            Turn_180 = 2,
            Turn_270 = 3,
            MirrorTurn_0 = 4,
            MirrorTurn_90 = 5,
            MirrorTurn_180 = 6,
            MirrorTurn_270 = 7,
        }

        public enum HintType : int
        {
            None = 0,
            DeleteFirst = 1,
            DeleteSecond = 2,
            RenameFirstToSecond = 3,
            RenameSecondToFirst = 4,
        }

        public enum PixelFormatType : int
        {
            None = 0,
            Argb32 = 1,
        }

        public enum ThreadType : int
        {
            Main = 0,
            Collect = 1,
            Compare = 2,
        }

        public enum VersionType : int
        {
            AntiDupl = 0,
            Simd = 1,
            OpenJpeg = 2,
            Webp = 3,
            TurboJpeg = 4,
        }

        public enum SelectionType : int
        {
            SelectCurrent = 0,
            UnselectCurrent = 1,
            SelectAll = 2,
            UnselectAll = 3,
            SelectAllButThis = 4,
        }

        public enum AlgorithmComparing : int
        {
            SquaredSum = 0,
            SSIM = 1,
        };

        //-----------API structures--------------------------------------------

        [StructLayout(LayoutKind.Sequential)]
        public struct adSearchOptions
        {
            public int system;
            public int hidden;
            public int JPEG;
            public int BMP;
            public int GIF;
            public int PNG;
            public int TIFF;
            public int EMF;
            public int WMF;
            public int EXIF;
            public int ICON;
            public int JP2;
            public int PSD;
            public int DDS;
            public int TGA;
            public int WEBP;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct adCompareOptions
        {
            public int checkOnEquality;
            public int transformedImage;
            public int sizeControl;
            public int typeControl;
            public int ratioControl;
            public int thresholdDifference;
            public int minimalImageSize;
            public int maximalImageSize;
            public int compareInsideOneFolder;
            public int compareInsideOneSearchPath;
            public AlgorithmComparing algorithmComparing;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct adDefectOptions
        {
            public int checkOnDefect;
            public int checkOnBlockiness;
            public int blockinessThreshold;
            public int checkOnBlockinessOnlyNotJpeg;
            public int checkOnBlurring;
            public int blurringThreshold;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct adAdvancedOptions
        {
            public int deleteToRecycleBin;
            public int mistakeDataBase;
            public int ratioResolution;
            public int compareThreadCount;
            public int collectThreadCount;
            public int reducedImageSize;
            public int undoQueueSize;
            public int resultCountMax;
            public int ignoreFrameWidth;
            public int useLibJpegTurbo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct adStatistic
        {
            public UIntPtr scanedFolderNumber;
            public UIntPtr searchedImageNumber;
            public ulong searchedImageSize;
            public UIntPtr collectedImageNumber;
            public UIntPtr comparedImageNumber;
            public UIntPtr collectThreadCount;
            public UIntPtr compareThreadCount;
            public UIntPtr defectImageNumber;
            public UIntPtr duplImagePairNumber;
            public UIntPtr renamedImageNumber;
            public UIntPtr deletedImageNumber;
            public ulong deletedImageSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct adStatusW
        {
            public StateType state;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH_EX)]
            public string path;
            public UIntPtr current;
            public UIntPtr total;
        }

        public const int MAX_EXIF_SIZE = 260;
        // Она же class TImageExif decimal в dll
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct adImageExifW
        {
            public int isEmpty;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_EXIF_SIZE)]
            public string imageDescription;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_EXIF_SIZE)]
            public string equipMake;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_EXIF_SIZE)]
            public string equipModel;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_EXIF_SIZE)]
            public string softwareUsed;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_EXIF_SIZE)]
            public string dateTime;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_EXIF_SIZE)]
            public string artist;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_EXIF_SIZE)]
            public string userComment;
        }

        // Она же структура TImageInfo в dll.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct adImageInfoW
        {
            public IntPtr id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH_EX)]
            public string path;
            public ulong size;
            public ulong time;
            public ImageType type;
            public uint width;
            public uint height;
            public double blockiness;
            public double blurring;
            public adImageExifW exifInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class adResultW
        {
            public ResultType type;
            [MarshalAs(UnmanagedType.Struct)]
            public adImageInfoW first;
            [MarshalAs(UnmanagedType.Struct)]
            public adImageInfoW second;
            public DefectType defect;
            public double difference;
            public TransformType transform;
            public IntPtr group;
            public IntPtr groupSize;
            public HintType hint;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct adGroup
        {
            public IntPtr id;
            public IntPtr size;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct adBitmap
        {
            public uint width;
            public uint height;
            public int stride;
            public PixelFormatType format;
            public IntPtr data;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct adPathWithSubFolderW
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH_EX)]
            public string path;
            public int enableSubFolder;
        }

        #region API functions new

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adVersionGet(VersionType versionType, IntPtr pVersion, IntPtr pVersionSize);

        public Error AdVersionGet(VersionType versionType, IntPtr pVersion, IntPtr pVersionSize) =>
            adVersionGet(versionType, pVersion, pVersionSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern IntPtr adCreateW(string userPath);

        public IntPtr AdCreateW(string userPath) => adCreateW(userPath);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adRelease(IntPtr handle);

        public Error AdRelease(IntPtr handle) => adRelease(handle);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adStop(IntPtr handle);

        public Error AdStop(IntPtr handle) => adStop(handle);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSearch(IntPtr handle);

        public Error AdSearch(IntPtr handle) => adSearch(handle);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adLoadW(IntPtr handle, FileType fileType, string fileName, int check);

        public Error AdLoadW(IntPtr handle, FileType fileType, string fileName, int check) =>
            adLoadW(handle, fileType, fileName, check);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSaveW(IntPtr handle, FileType fileType, string fileName);

        public Error AdSaveW(IntPtr handle, FileType fileType, string fileName) =>
            adSaveW(handle, fileType, fileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adClear(IntPtr handle, FileType fileType);

        public Error AdClear(IntPtr handle, FileType fileType) => adClear(handle, fileType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adOptionsGet(IntPtr handle, OptionsType optionsType, IntPtr pOptions);

        public Error AdOptionsGet(IntPtr handle, OptionsType optionsType, IntPtr pOptions) =>
            adOptionsGet(handle, optionsType, pOptions);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adOptionsSet(IntPtr handle, OptionsType optionsType, IntPtr pOptions);

        public Error AdOptionsSet(IntPtr handle, OptionsType optionsType, IntPtr pOptions) =>
            adOptionsSet(handle, optionsType, pOptions);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adPathWithSubFolderSetW(IntPtr handle, PathType pathType, IntPtr pPaths, IntPtr pathSize);

        public Error AdPathWithSubFolderSetW(IntPtr handle, PathType pathType, IntPtr pPaths, IntPtr pathSize) =>
            adPathWithSubFolderSetW(handle, pathType, pPaths, pathSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adPathGetW(IntPtr handle, PathType pathType, IntPtr pPath, IntPtr pPathSize);

        public Error AdPathGetW(IntPtr handle, PathType pathType, IntPtr pPath, IntPtr pPathSize) =>
            adPathGetW(handle, pathType, pPath, pPathSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adPathSetW(IntPtr handle, PathType pathType, IntPtr pPath, IntPtr pathSize);

        public Error AdPathSetW(IntPtr handle, PathType pathType, IntPtr pPath, IntPtr pathSize) =>
            adPathSetW(handle, pathType, pPath, pathSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adStatisticGet(IntPtr handle, IntPtr pStatistic);

        public Error AdStatisticGet(IntPtr handle, IntPtr pStatistic) => adStatisticGet(handle, pStatistic);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adStatusGetW(IntPtr handle, ThreadType threadType, IntPtr threadId, IntPtr pStatusW);

        public Error AdStatusGetW(IntPtr handle, ThreadType threadType, IntPtr threadId, IntPtr pStatusW) =>
            adStatusGetW(handle, threadType, threadId, pStatusW);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultSort(IntPtr handle, SortType sortType, int increasing);

        public Error AdResultSort(IntPtr handle, SortType sortType, int increasing) =>
            adResultSort(handle, sortType, increasing);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultApply(IntPtr handle, GlobalActionType globalActionType);

        public Error AdResultApply(IntPtr handle, GlobalActionType globalActionType) =>
            adResultApply(handle, globalActionType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultApplyTo(IntPtr handle, LocalActionType localActionType, TargetType targetType);

        public Error AdResultApplyTo(IntPtr handle, LocalActionType localActionType, TargetType targetType) =>
            adResultApplyTo(handle, localActionType, targetType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adCanApply(IntPtr handle, ActionEnableType actionEnableType, IntPtr pEnable);

        public Error AdCanApply(IntPtr handle, ActionEnableType actionEnableType, IntPtr pEnable) =>
            adCanApply(handle, actionEnableType, pEnable);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adRenameCurrentW(IntPtr handle, RenameCurrentType renameCurrentType, string newFileName);

        public Error AdRenameCurrentW(IntPtr handle, RenameCurrentType renameCurrentType, string newFileName) =>
            adRenameCurrentW(handle, renameCurrentType, newFileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adMoveCurrentGroupW(IntPtr handle, string directory);

        public Error AdMoveCurrentGroupW(IntPtr handle, string directory) => adMoveCurrentGroupW(handle, directory);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adRenameCurrentGroupAsW(IntPtr handle, string fileName);

        public Error AdRenameCurrentGroupAsW(IntPtr handle, string fileName) =>
            adRenameCurrentGroupAsW(handle, fileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adResultGetW(IntPtr handle, IntPtr pStartFrom, IntPtr pResult, IntPtr pResultSize);

        public Error AdResultGetW(IntPtr handle, IntPtr pStartFrom, IntPtr pResult, IntPtr pResultSize) =>
            adResultGetW(handle, pStartFrom, pResult, pResultSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSelectionSet(IntPtr handle, IntPtr pStartFrom, UIntPtr size, int value);

        public Error AdSelectionSet(IntPtr handle, IntPtr pStartFrom, UIntPtr size, int value) =>
            adSelectionSet(handle, pStartFrom, size, value);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adSelectionGet(IntPtr handle, IntPtr pStartFrom, IntPtr pSelection, IntPtr pSelectionSize);

        public Error AdSelectionGet(IntPtr handle, IntPtr pStartFrom, IntPtr pSelection, IntPtr pSelectionSize) =>
            adSelectionGet(handle, pStartFrom, pSelection, pSelectionSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adCurrentSet(IntPtr handle, IntPtr index);

        public Error AdCurrentSet(IntPtr handle, IntPtr index) => adCurrentSet(handle, index);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adCurrentGet(IntPtr handle, IntPtr pIndex);

        public Error AdCurrentGet(IntPtr handle, IntPtr pIndex) => adCurrentGet(handle, pIndex);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adGroupGet(IntPtr handle, IntPtr pStartFrom, IntPtr pGroup, IntPtr pGroupSize);

        public Error AdGroupGet(IntPtr handle, IntPtr pStartFrom, IntPtr pGroup, IntPtr pGroupSize) =>
            adGroupGet(handle, pStartFrom, pGroup, pGroupSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoGetW(IntPtr handle, IntPtr groupId, IntPtr pStartFrom, IntPtr pImageInfo, IntPtr pImageInfoSize);

        public Error AdImageInfoGetW(IntPtr handle, IntPtr groupId, IntPtr pStartFrom, IntPtr pImageInfo,
                                     IntPtr pImageInfoSize) =>
            adImageInfoGetW(handle, groupId, pStartFrom, pImageInfo, pImageInfoSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoSelectionSet(IntPtr handle, IntPtr groupId, IntPtr index, SelectionType selectionType);

        public Error
            AdImageInfoSelectionSet(IntPtr handle, IntPtr groupId, IntPtr index, SelectionType selectionType) =>
            adImageInfoSelectionSet(handle, groupId, index, selectionType);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoSelectionGet(IntPtr handle, IntPtr groupId, IntPtr pStartFrom, IntPtr pSelection, IntPtr pSelectionSize);

        public Error AdImageInfoSelectionGet(IntPtr handle, IntPtr groupId, IntPtr pStartFrom, IntPtr pSelection,
                                             IntPtr pSelectionSize) =>
            adImageInfoSelectionGet(handle, groupId, pStartFrom, pSelection, pSelectionSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adImageInfoRenameW(IntPtr handle, IntPtr groupId, IntPtr index, string newFileName);

        public Error AdImageInfoRenameW(IntPtr handle, IntPtr groupId, IntPtr index, string newFileName) =>
            adImageInfoRenameW(handle, groupId, index, newFileName);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern Error adLoadBitmapW(IntPtr handle, string fileName, IntPtr pBitmap);

        public Error AdLoadBitmapW(IntPtr handle, string fileName, IntPtr pBitmap) =>
            adLoadBitmapW(handle, fileName, pBitmap);

        #endregion
    }
}
