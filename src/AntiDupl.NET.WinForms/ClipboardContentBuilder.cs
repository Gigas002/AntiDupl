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

using System.Text;
using AntiDupl.NET.Core;
using TypeVertical = AntiDupl.NET.WinForms.GUIControl.ResultsListView.ColumnsTypeVertical;
using TypeHorizontal = AntiDupl.NET.WinForms.GUIControl.ResultsListView.ColumnsTypeHorizontal;

namespace AntiDupl.NET.WinForms
{
    public class ClipboardContentBuilder
    {
        private StringBuilder m_builder;
        private ResultsOptions m_options;
        private bool m_insertTab;

        public ClipboardContentBuilder(ResultsOptions options)
        {
            m_options = options;
            m_builder = new StringBuilder();
        }

        public void Add(AdResultW result)
        {
            m_insertTab = false;
            switch (m_options.ViewMode)
            {
                case ViewMode.VerticalPairTable:
                    AddCommon(result, m_options.ColumnOptionsVertical);
                    AddVertical(result, m_options.ColumnOptionsVertical);
                    break;
                case ViewMode.HorizontalPairTable:
                    AddCommon(result, m_options.ColumnOptionsHorizontal);
                    AddHorizontal(result, m_options.ColumnOptionsHorizontal);
                    break;
            }
            m_builder.AppendLine("");
        }

        public override string ToString()
        {
            return m_builder.ToString();
        }

        private void AddCommon(AdResultW result, ColumnOptions[] options)
        {
            if (options[(int)TypeVertical.Type].Visible)
                Append(result.Type);
            if (options[(int)TypeVertical.Group].Visible)
                Append(result.Group);
            if (options[(int)TypeVertical.Difference].Visible)
                Append(result.Difference.ToString("F2"));
            if (options[(int)TypeVertical.Defect].Visible)
                Append(result.Defect);
            if (options[(int)TypeVertical.Transform].Visible)
                Append(result.Transform);
            if (options[(int)TypeVertical.Hint].Visible)
                Append(result.Hint);
        }

        private void AddVertical(AdResultW result, ColumnOptions[] options)
        {
            if (options[(int)TypeVertical.FileName].Visible ||
                options[(int)TypeVertical.FileDirectory].Visible)
                Append(result.First.Path);
            if (options[(int)TypeVertical.ImageSize].Visible)
                Append(result.First.GetImageSizeString());
            if (options[(int)TypeVertical.ImageType].Visible)
                Append(result.First.GetImageTypeString());
            if (options[(int)TypeVertical.FileSize].Visible)
                Append(result.First.GetFileSizeString());
            if (options[(int)TypeVertical.FileTime].Visible)
                Append(result.First.GetFileTimeString());

            if (options[(int)TypeVertical.FileName].Visible ||
                options[(int)TypeVertical.FileDirectory].Visible)
                Append(result.Second.Path);
            if (options[(int)TypeVertical.ImageSize].Visible)
                Append(result.Second.GetImageSizeString());
            if (options[(int)TypeVertical.ImageType].Visible)
                Append(result.Second.GetImageTypeString());
            if (options[(int)TypeVertical.FileSize].Visible)
                Append(result.Second.GetFileSizeString());
            if (options[(int)TypeVertical.FileTime].Visible)
                Append(result.Second.GetFileTimeString());
        }

        private void AddHorizontal(AdResultW result, ColumnOptions[] options)
        {
            if (options[(int)TypeHorizontal.FirstFileName].Visible ||
                options[(int)TypeHorizontal.FirstFileDirectory].Visible)
                Append(result.First.Path);
            if (options[(int)TypeHorizontal.FirstImageSize].Visible)
                Append(result.First.GetImageSizeString());
            if (options[(int)TypeHorizontal.FirstImageType].Visible)
                Append(result.First.GetImageTypeString());
            if (options[(int)TypeHorizontal.FirstFileSize].Visible)
                Append(result.First.GetFileSizeString());
            if (options[(int)TypeHorizontal.FirstFileTime].Visible)
                Append(result.First.GetFileTimeString());

            if (options[(int)TypeHorizontal.SecondFileName].Visible ||
                options[(int)TypeHorizontal.SecondFileDirectory].Visible)
                Append(result.Second.Path);
            if (options[(int)TypeHorizontal.SecondImageSize].Visible)
                Append(result.Second.GetImageSizeString());
            if (options[(int)TypeHorizontal.SecondImageType].Visible)
                Append(result.Second.GetImageTypeString());
            if (options[(int)TypeHorizontal.SecondFileSize].Visible)
                Append(result.Second.GetFileSizeString());
            if (options[(int)TypeHorizontal.SecondFileTime].Visible)
                Append(result.Second.GetFileTimeString());
        }

        private void Append(object value)
        {
            if (m_insertTab)
                m_builder.Append("\t");
            m_builder.Append(value.ToString());
            m_insertTab = true;
        }
    }
}
