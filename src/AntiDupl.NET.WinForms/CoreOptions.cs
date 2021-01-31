﻿/*
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

using System.IO;
using System.Xml.Serialization;
using AntiDupl.NET.Core;
using AntiDupl.NET.Core.Original;

//using System.Windows.Forms;

namespace AntiDupl.NET.WinForms
{
    public class CoreOptions
    {
        public AdSearchOptions searchOptions;
        public AdCompareOptions compareOptions;
        public AdDefectOptions defectOptions;
        public AdAdvancedOptions advancedOptions;

        public AdPathWithSubFolderW[] searchPath;
        public AdPathWithSubFolderW[] ignorePath;
        public AdPathWithSubFolderW[] validPath;
        public AdPathWithSubFolderW[] deletePath;

        public CoreOptions()
        {
            searchOptions = new AdSearchOptions();
            compareOptions = new AdCompareOptions();
            defectOptions = new AdDefectOptions();
            advancedOptions = new AdAdvancedOptions();

            searchPath = new AdPathWithSubFolderW[1];
            ignorePath = new AdPathWithSubFolderW[0];
            validPath = new AdPathWithSubFolderW[0];
            deletePath = new AdPathWithSubFolderW[0];
        }

        public CoreOptions(CoreLib core, bool onePath)
            : this()
        {
            //SetDefault(core, onePath);
        }

        public CoreOptions(CoreLib core)
            : this(core, false)
        {
        }

        public CoreOptions(CoreOptions options)
        {
            searchOptions = options.searchOptions;
            compareOptions = options.compareOptions;
            defectOptions = options.defectOptions;
            advancedOptions = options.advancedOptions;

            searchPath = PathClone(options.searchPath);
            ignorePath = PathClone(options.ignorePath);
            validPath = PathClone(options.validPath);
            deletePath = PathClone(options.deletePath);
        }

        public void SetDefault(CoreLib core, bool onePath)
        {
            CoreOptions old = new CoreOptions();
            old.Get(core, onePath);
            core.SetDefaultOptions();
            Get(core, onePath);
            old.Set(core, onePath);

            ignorePath = new AdPathWithSubFolderW[1];
            ignorePath[0] = new AdPathWithSubFolderW();
            ignorePath[0].Path = Resources.DataPath;
        }

        public void Get(CoreLib core, bool onePath)
        {
            searchOptions = core.searchOptions;
            compareOptions = core.compareOptions;
            defectOptions = core.defectOptions;
            advancedOptions = core.advancedOptions;
            if (onePath)
            {
                searchPath[0] = core.searchPath[0];
            }
            else
            {
                searchPath = core.searchPath;
                ignorePath = core.ignorePath;
                validPath = core.validPath;
                deletePath = core.deletePath;
            }
        }

        /// <summary>
        /// Устанавливает опции в ядре из текущих, для передачи в dll.
        /// </summary>
        /// <param name="core"></param>
        /// <param name="onePath"></param>
        public void Set(CoreLib core, bool onePath)
        {
            core.searchOptions = searchOptions;
            core.compareOptions = compareOptions;
            core.defectOptions = defectOptions;
            core.advancedOptions = advancedOptions;
            if (onePath)
            {
                AdPathWithSubFolderW[] tmpSearch = new AdPathWithSubFolderW[1];
                AdPathWithSubFolderW[] tmpOther = new AdPathWithSubFolderW[0];
                if (searchPath.Length > 0 && Directory.Exists(searchPath[0].Path)) tmpSearch[0] = searchPath[0];
                else
                    //TODO
                    tmpSearch[0].Path = string.Empty;
                //tmpSearch[0].path = Application.StartupPath;
                core.searchPath = tmpSearch;
                core.ignorePath = tmpOther;
                core.validPath = tmpOther;
                core.deletePath = tmpOther;
            }
            else
            {
                core.searchPath = searchPath;
                core.ignorePath = ignorePath;
                core.validPath = validPath;
                core.deletePath = deletePath;
            }
        }

        public void Validate(CoreLib core, bool onePath)
        {
            Set(core, onePath);
            Get(core, onePath);
        }

        public CoreOptions Clone()
        {
            return new CoreOptions(this);
        }

        public void CopyTo(ref CoreOptions options)
        {
            options.searchOptions = searchOptions;
            options.compareOptions = compareOptions;
            options.defectOptions = defectOptions;
            options.advancedOptions = advancedOptions;

            PathCopy(searchPath, ref options.searchPath);
            PathCopy(ignorePath, ref options.ignorePath);
            PathCopy(validPath, ref options.validPath);
            PathCopy(deletePath, ref options.deletePath);
        }

        public static void PathCopy(string[] source, ref string[] destination)
        {
            destination = new string[source.GetLength(0)];
            for (int i = 0; i < source.GetLength(0); ++i)
                destination[i] = (string)source[i].Clone();
        }

        public static void PathCopy(AdPathWithSubFolderW[] source, ref AdPathWithSubFolderW[] destination)
        {
            destination = new AdPathWithSubFolderW[source.GetLength(0)];
            for (int i = 0; i < source.GetLength(0); ++i)
                destination[i] = source[i];
        }

        public static string[] PathClone(string[] path)
        {
            string[] clone = new string[0];
            PathCopy(path, ref clone);
            return clone;
        }

        public static AdPathWithSubFolderW[] PathClone(AdPathWithSubFolderW[] path)
        {
            AdPathWithSubFolderW[] clone = new AdPathWithSubFolderW[0];
            PathCopy(path, ref clone);
            return clone;
        }

        public static bool Equals(string[] path1, string[] path2)
        {
            if (path1.Length != path2.Length)
                return false;
            for (int i = 0; i < path1.Length; ++i)
                if (path1[i].CompareTo(path2[i]) != 0)
                    return false;
            return true;
        }

        public bool Equals(CoreOptions options)
        {
            if (!searchOptions.Equals(options.searchOptions))
                return false;
            if (!compareOptions.Equals(options.compareOptions))
                return false;
            if (!defectOptions.Equals(options.defectOptions))
                return false;
            if (!advancedOptions.Equals(options.advancedOptions))
                return false;
            if (!Equals(searchPath, options.searchPath))
                return false;
            if (!Equals(ignorePath, options.ignorePath))
                return false;
            if (!Equals(validPath, options.validPath))
                return false;
            if (!Equals(deletePath, options.deletePath))
                return false;

            return true;
        }

        static public CoreOptions Load(string fileName, CoreLib core, bool onePath)
        {
            //FileInfo fileInfo = new FileInfo(fileName);
            //if (fileInfo.Exists)
            //{
            //    FileStream fileStream = null;
            //    try
            //    {
            //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CoreOptions));
            //        fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //        CoreOptions coreOptions = (CoreOptions)xmlSerializer.Deserialize(fileStream);
            //        fileStream.Close();
            //        coreOptions.Validate(core, onePath);
            //        return coreOptions;
            //    }
            //    catch
            //    {
            //        if (fileStream != null)
            //            fileStream.Close();
            //        return new CoreOptions(core);
            //    }
            //}
            //else
                return new CoreOptions(core);
        }

        public void Save(string fileName)
        {
            //TextWriter writer = null;
            //try
            //{
            //    writer = new StreamWriter(fileName);
            //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(CoreOptions));
            //    xmlSerializer.Serialize(writer, this);
            //}
            //catch
            //{
            //}
            //if (writer != null)
            //    writer.Close();
        }

        public string GetImageDataBasePath()
        {
            string directory =
                $"{Resources.UserPath}\\images\\{advancedOptions.ReducedImageSize}x{advancedOptions.ReducedImageSize}";
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
            return directory;
        }
    }
}
