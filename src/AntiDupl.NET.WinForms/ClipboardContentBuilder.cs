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

        public void Add(CoreResult result)
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

        private void AddCommon(CoreResult result, ColumnOptions[] options)
        {
            if (options[(int)TypeVertical.Type].Visible)
                Append(result.type);
            if (options[(int)TypeVertical.Group].Visible)
                Append(result.group);
            if (options[(int)TypeVertical.Difference].Visible)
                Append(result.difference.ToString("F2"));
            if (options[(int)TypeVertical.Defect].Visible)
                Append(result.defect);
            if (options[(int)TypeVertical.Transform].Visible)
                Append(result.transform);
            if (options[(int)TypeVertical.Hint].Visible)
                Append(result.hint);
        }

        private void AddVertical(CoreResult result, ColumnOptions[] options)
        {
            if (options[(int)TypeVertical.FileName].Visible ||
                options[(int)TypeVertical.FileDirectory].Visible)
                Append(result.first.Path);
            if (options[(int)TypeVertical.ImageSize].Visible)
                Append(result.first.GetImageSizeString());
            if (options[(int)TypeVertical.ImageType].Visible)
                Append(result.first.GetImageTypeString());
            if (options[(int)TypeVertical.FileSize].Visible)
                Append(result.first.GetFileSizeString());
            if (options[(int)TypeVertical.FileTime].Visible)
                Append(result.first.GetFileTimeString());

            if (options[(int)TypeVertical.FileName].Visible ||
                options[(int)TypeVertical.FileDirectory].Visible)
                Append(result.second.Path);
            if (options[(int)TypeVertical.ImageSize].Visible)
                Append(result.second.GetImageSizeString());
            if (options[(int)TypeVertical.ImageType].Visible)
                Append(result.second.GetImageTypeString());
            if (options[(int)TypeVertical.FileSize].Visible)
                Append(result.second.GetFileSizeString());
            if (options[(int)TypeVertical.FileTime].Visible)
                Append(result.second.GetFileTimeString());
        }

        private void AddHorizontal(CoreResult result, ColumnOptions[] options)
        {
            if (options[(int)TypeHorizontal.FirstFileName].Visible ||
                options[(int)TypeHorizontal.FirstFileDirectory].Visible)
                Append(result.first.Path);
            if (options[(int)TypeHorizontal.FirstImageSize].Visible)
                Append(result.first.GetImageSizeString());
            if (options[(int)TypeHorizontal.FirstImageType].Visible)
                Append(result.first.GetImageTypeString());
            if (options[(int)TypeHorizontal.FirstFileSize].Visible)
                Append(result.first.GetFileSizeString());
            if (options[(int)TypeHorizontal.FirstFileTime].Visible)
                Append(result.first.GetFileTimeString());

            if (options[(int)TypeHorizontal.SecondFileName].Visible ||
                options[(int)TypeHorizontal.SecondFileDirectory].Visible)
                Append(result.second.Path);
            if (options[(int)TypeHorizontal.SecondImageSize].Visible)
                Append(result.second.GetImageSizeString());
            if (options[(int)TypeHorizontal.SecondImageType].Visible)
                Append(result.second.GetImageTypeString());
            if (options[(int)TypeHorizontal.SecondFileSize].Visible)
                Append(result.second.GetFileSizeString());
            if (options[(int)TypeHorizontal.SecondFileTime].Visible)
                Append(result.second.GetFileTimeString());
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
