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
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;

namespace AntiDupl.NET
{
    // TODO: Resources namespace

    public static class Resources
    {
        private static string GetPath(string path, string name, string extension) => string.Format(CultureInfo.InvariantCulture, "{0}\\{1}{2}", path, name, extension);

        private static string CreateIfNotExists(string path)
        {
            Directory.CreateDirectory(path);

            return path;
        }

        internal static string GetDefaultUserPath() => CreateIfNotExists(string.Format(CultureInfo.InvariantCulture, "{0}\\user", Application.StartupPath));

        internal static string UserPath { get; set; }

        internal static string ProfilesPath => CreateIfNotExists(string.Format(CultureInfo.InvariantCulture, "{0}\\profiles", UserPath));

        internal static string DataPath => string.Format(CultureInfo.InvariantCulture, "{0}\\data", Application.StartupPath);

        private static string Path => string.Format(CultureInfo.InvariantCulture, "{0}\\resources", DataPath);

        internal static class Images
        {
            internal static Image GetNullImage()
            {
                Bitmap bitmap = new Bitmap(1, 1);
                bitmap.SetPixel(0, 0, Color.FromArgb(0, 0, 0, 0));
                return bitmap;
            }

            internal static Image GetImageWithBlackCircle(int width, int height, double radius)
            {
                Bitmap bitmap = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    int xx = x - width / 2;
                    for (int y = 0; y < height; y++)
                    {
                        int yy = y - height / 2;
                        bitmap.SetPixel(x, y,
                                        xx * xx + yy * yy < radius * radius
                                            ? Color.FromArgb(0xFF, 0, 0, 0)
                                            : Color.FromArgb(0, 0, 0, 0));
                    }
                }

                return bitmap;
            }

            internal static Image Get(string name)
            {
                Image image;

                try
                {
                    string extension = System.IO.Path.GetExtension(name);

                    if (string.IsNullOrWhiteSpace(extension))
                        extension = Extension;

                    image = Image.FromFile(GetPath(Path, System.IO.Path.GetFileNameWithoutExtension(name), extension));
                }
                catch
                {
                    image = GetNullImage();
                }

                return image;
            }

            private static string Path => string.Format(CultureInfo.InvariantCulture, "{0}\\images", Resources.Path);

            private static string Extension => ".img";
        }

        internal static class Icons
        {
            internal static Icon Get(Size size)
            {
                Icon icon = Get();
                return new Icon(icon, size);
            }

            internal static Icon Get()
            {
                Icon icon = null;

                try
                {
                    icon = new Icon(GetPath(Path, "Icon", Extension));
                }
                catch
                {
                    // TODO: should there really be a second try-catch block?
                    try
                    {
                        icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                return icon;
            }

            private static string Path => string.Format(CultureInfo.InvariantCulture, "{0}\\icons", Resources.Path);

            private static string Extension => ".ico";
        }

        public static class Strings
        {
            public delegate void CurrentChangeHandler();

            /// <summary>
            /// Событие вызываемое при смене языка
            /// </summary>
            public static event CurrentChangeHandler OnCurrentChange;

            internal static void Initialize()
            {
                MStrings.Clear();

                MStrings.Add(StringsDefaultEnglish.Get());
                MStrings.Add(StringsDefaultRussian.Get());

                DirectoryInfo directoryInfo = new DirectoryInfo(Path);
                if (directoryInfo.Exists)
                {
                    FileInfo[] fileInfos = directoryInfo.GetFiles(Filter, SearchOption.TopDirectoryOnly);
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        NET.Strings strings = Load(fileInfo.FullName);

                        if (strings == null) continue;

                        string name = System.IO.Path.GetFileNameWithoutExtension(fileInfo.FullName);

                        if (string.Compare(name, StringsDefaultRussian.Get().Name, StringComparison.Ordinal) == 0
                         || string.Compare(name, StringsDefaultEnglish.Get().Name, StringComparison.Ordinal) == 0)
                            continue;

                        strings.Name = name;
                        MStrings.Add(strings);
                    }
                }

                try
                {
                    CreateIfNotExists(Path);
                    Save(StringsDefaultEnglish.Get());
                    Save(StringsDefaultRussian.Get());
                }
                catch
                {
                    // ignored
                }
            }

            private static NET.Strings Load(string path)
            {
                FileInfo fileInfo = new FileInfo(path);
                if (!fileInfo.Exists) return null;

                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(NET.Strings));
                    using FileStream fileStream = File.OpenRead(path);
                    NET.Strings strings = (NET.Strings)xmlSerializer.Deserialize(fileStream);
                    return strings;
                }
                catch
                {
                    return null;
                }
            }

            private static void Save(NET.Strings strings)
            {
                try
                {
                    using TextWriter writer = new StreamWriter(GetPath(Path, strings.Name, Extension));
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(NET.Strings));
                    xmlSerializer.Serialize(writer, strings);
                }
                catch
                {
                    // ignored
                }
            }

            private static string Path => $"{Resources.Path}\\strings";

            private static string Extension => ".xml";

            private static string Filter => "*.xml";

            internal static int Count => MStrings.Count;

            internal static int CurrentIndex { get; private set; }

            internal static NET.Strings Current
            {
                get
                {
                    if (CurrentIndex < Count && CurrentIndex >= 0)
                        return (NET.Strings)MStrings[CurrentIndex];

                    return null;
                }
            }

            internal static NET.Strings Get(int index)
            {
                if (index < Count && index >= 0)
                    return (NET.Strings)MStrings[index];

                return null;
            }

            private static void SetCurrent(int index)
            {
                if (index == CurrentIndex || index >= Count || index < 0) return;

                CurrentIndex = index;
                OnCurrentChange?.Invoke();
            }

            internal static void SetCurrent(string name)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (string.Compare(Get(i).Name, name, StringComparison.Ordinal) != 0) continue;

                    SetCurrent(i);
                    return;
                }

                for (int i = 0; i < Count; i++)
                {
                    if (string.Compare(Get(i).Name, StringsDefaultEnglish.Get().Name, StringComparison.Ordinal) != 0)
                        continue;

                    SetCurrent(i);
                    return;
                }
            }

            private static bool IsCurrentRussian() => string.Compare(Current.Name, StringsDefaultRussian.Get().Name, StringComparison.Ordinal) == 0;

            public static bool IsCurrentEnglish() => string.Compare(Current.Name, StringsDefaultEnglish.Get().Name, StringComparison.Ordinal) == 0;

            internal static bool IsCurrentRussianFamily() => IsCurrentRussian() ||
                                                             string.Compare(Current.Name, "Belarusian", StringComparison.Ordinal) == 0 ||
                                                             string.Compare(Current.Name, "Ukrainian", StringComparison.Ordinal) == 0;

            private static readonly ArrayList MStrings = new ArrayList();

            internal static void Update() => OnCurrentChange?.Invoke();
        }

        internal static class WebLinks
        {
            internal const string GithubComAntidupl = "http://ermig1979.github.io/AntiDupl";

            private const string GithubComAntiduplEnglish = "http://ermig1979.github.io/AntiDupl/english/index.html";

            private const string GithubComAntiduplRussian = "http://ermig1979.github.io/AntiDupl/russian/index.html";

            internal const string Version = "http://ermig1979.github.io/AntiDupl/version.xml";

            internal const string Simd = "http://ermig1979.github.io/Simd";

            internal const string OpenJpeg = "http://www.openjpeg.org";

            internal const string LibWebp = "https://github.com/webmproject/libwebp";

            internal const string LibJpegTurbo = "http://www.libjpeg-turbo.org";

            internal static string GithubComAntiduplCurrent => Strings.IsCurrentRussianFamily() ? GithubComAntiduplRussian : GithubComAntiduplEnglish;
        }

        internal static class Help
        {
            private static string GetUrl(string page)
            {
                StringBuilder builder = new StringBuilder(WebLinks.GithubComAntidupl);
                builder.Append("/data/help");
                builder.Append(Strings.IsCurrentRussianFamily() ? "/russian" : "/english");
                builder.Append("/index.html");

                if (page == null) return builder.ToString();

                builder.Append("?page=");
                builder.Append(page);

                return builder.ToString();
            }

            private sealed class Starter
            {
                private readonly string _mUrl;

                public Starter(Form form, string url)
                {
                    _mUrl = url;
                    form.HelpButton = true;
                    form.HelpButtonClicked += OnHelpButtonClicked;
                }

                private void OnHelpButtonClicked(object sender, CancelEventArgs e)
                {
                    Show(_mUrl);
                }
            }

            internal static void Show(string url)
            {
                try
                {
                    if (url.Substring(0, 4).ToUpper() != "HTTP")
                    {
                        const string keyName = @"HTTP\shell\open\command";
                        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(keyName, false);
                        if (registryKey == null) return;

                        string defaultBrouserPath = ((string)registryKey.GetValue(null, null))?.Split('"')[1];
                        Process.Start(defaultBrouserPath ?? throw new NullReferenceException(nameof(defaultBrouserPath)), url);
                    }
                    else
                    {
                        Process.Start(url);
                    }
                }
                catch
                {
                    // ignored
                }
            }

            internal static void Bind(Form form, string path)
            {
                form.Tag = new Starter(form, path);
            }

            internal static string Index => GetUrl(null);

            internal static string Options => GetUrl("options.html");

            internal static string Paths => GetUrl("paths.html");

            internal static string HotKeys => GetUrl("hotkeys.html");
        }

        internal static class Logs
        {
            internal static string Performance => $"{UserPath}\\performance.txt";
        }
    }
}

