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
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace AntiDupl.NET
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            string customSavePath = null;

            // TODO: use CommandLineParser?
            if (GetParameter(args, "-s", ref customSavePath))
            {
                DirectoryInfo directoryInfo = new(customSavePath);

                if (!directoryInfo.Exists)
                {
                    // TODO: move to strings
                    throw new Exception(string.Format(CultureInfo.InvariantCulture,
                                                      "The directory '{0}' is not exists!", customSavePath));
                }

                Resources.UserPath = customSavePath;
            }
            else
            {
                Resources.UserPath = Resources.GetDefaultUserPath();
            }

            Resources.Strings.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using MainForm form = new();
            Application.Run(form);
        }

        private static bool GetParameter(IReadOnlyList<string> args, string name, ref string value)
        {
            for (int i = 0; i < args.Count - 1; i++)
            {
                if (string.Compare(args[i], name, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) != 0)
                {
                    continue;
                }

                value = args[i + 1];

                return true;
            }

            return false;
        }
    }
}