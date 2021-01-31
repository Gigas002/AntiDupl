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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AntiDupl.NET.Core;
using AntiDupl.NET.Core.Enums;
using AntiDupl.NET.Core.Original;
using AntiDupl.NET.WinForms.GUIControl;

namespace AntiDupl.NET.WinForms
{
    /// <summary>
    /// Set table of out defect and dublicate pair.
    /// Установка таблицы вывода дефектов и дубликатов.
    /// </summary>
    internal sealed class ResultRowSetter
    {
        private readonly Options _mOptions;

        private readonly DataGridView _mDataGridView;

        private Image _mNullIcon;

        private Image _mDefectIcon;

        private Image _mDuplPairVerticalIcon;

        private Image _mDuplPairHorizontalIcon;

        private Image _mUnknownDefectIcon;

        private Image _mJpegEndMarkerIsAbsentIcon;

        private Image _mBlockinessIcon;

        private Image _mBlurringIcon;

        private Image _mDeleteDefectIcon;

        private Image _mDeleteFirstVerticalIcon;

        private Image _mDeleteFirstHorizontalIcon;

        private Image _mDeleteSecondVerticalIcon;

        private Image _mDeleteSecondHorizontalIcon;

        private Image _mRenameFirstToSecondVerticalIcon;

        private Image _mRenameFirstToSecondHorizontalIcon;

        private Image _mRenameSecondToFirstVerticalIcon;

        private Image _mRenameSecondToFirstHorizontalIcon;

        private Image _mTurn0Icon;

        private Image _mTurn90Icon;

        private Image _mTurn180Icon;

        private Image _mTurn270Icon;

        private Image _mMirrorTurn0Icon;

        private Image _mMirrorTurn90Icon;

        private Image _mMirrorTurn180Icon;

        private Image _mMirrorTurn270Icon;

        private string _mDefectIconToolTipText;

        private string _mDuplPairIconToolTipText;

        private string _mUnknownDefectIconToolTipText;

        private string _mJpegEndMarkerIsAbsentIconToolTipText;

        private string _mBlockinessIconToolTipText;

        private string _mBlurringIconToolTipText;

        private string _mDeleteDefectIconToolTipText;

        private string _mDeleteFirstIconToolTipText;

        private string _mDeleteSecondIconToolTipText;

        private string _mRenameFirstToSecondIconToolTipText;

        private string _mRenameSecondToFirstIconToolTipText;

        private string _mTurn0IconToolTipText;

        private string _mTurn90IconToolTipText;

        private string _mTurn180IconToolTipText;

        private string _mTurn270IconToolTipText;

        private string _mMirrorTurn0IconToolTipText;

        private string _mMirrorTurn90IconToolTipText;

        private string _mMirrorTurn180IconToolTipText;

        private string _mMirrorTurn270IconToolTipText;

        public ResultRowSetter(Options options, DataGridView dataGridView)
        {
            _mOptions = options;
            _mDataGridView = dataGridView;
            InitializeImages();
            UpdateStrings();
            Resources.Strings.OnCurrentChange += UpdateStrings;
        }

        private void InitializeImages()
        {
            _mNullIcon = Resources.Images.GetNullImage();

            _mDefectIcon = Resources.Images.Get("DefectIcon");
            _mDuplPairVerticalIcon = Resources.Images.Get("DuplPairVerticalIcon");
            _mDuplPairHorizontalIcon = Resources.Images.Get("DuplPairHorizontalIcon");

            _mUnknownDefectIcon = Resources.Images.Get("UnknownDefectIcon");
            _mJpegEndMarkerIsAbsentIcon = Resources.Images.Get("JpegEndMarkerIsAbsentIcon");
            _mBlockinessIcon = Resources.Images.Get("BlockinessIcon");
            _mBlurringIcon = Resources.Images.Get("BlurringIcon");

            _mDeleteDefectIcon = Resources.Images.Get("DeleteDefectIcon");
            _mDeleteFirstVerticalIcon = Resources.Images.Get("DeleteFirstVerticalIcon");
            _mDeleteFirstHorizontalIcon = Resources.Images.Get("DeleteFirstHorizontalIcon");
            _mDeleteSecondVerticalIcon = Resources.Images.Get("DeleteSecondVerticalIcon");
            _mDeleteSecondHorizontalIcon = Resources.Images.Get("DeleteSecondHorizontalIcon");
            _mRenameFirstToSecondVerticalIcon = Resources.Images.Get("RenameFirstToSecondVerticalIcon");
            _mRenameFirstToSecondHorizontalIcon = Resources.Images.Get("RenameFirstToSecondHorizontalIcon");
            _mRenameSecondToFirstVerticalIcon = Resources.Images.Get("RenameSecondToFirstVerticalIcon");
            _mRenameSecondToFirstHorizontalIcon = Resources.Images.Get("RenameSecondToFirstHorizontalIcon");

            _mTurn0Icon = Resources.Images.Get("Turn_0_Icon");
            _mTurn90Icon = Resources.Images.Get("Turn_90_Icon");
            _mTurn180Icon = Resources.Images.Get("Turn_180_Icon");
            _mTurn270Icon = Resources.Images.Get("Turn_270_Icon");
            _mMirrorTurn0Icon = Resources.Images.Get("MirrorTurn_0_Icon");
            _mMirrorTurn90Icon = Resources.Images.Get("MirrorTurn_90_Icon");
            _mMirrorTurn180Icon = Resources.Images.Get("MirrorTurn_180_Icon");
            _mMirrorTurn270Icon = Resources.Images.Get("MirrorTurn_270_Icon");
        }

        private void UpdateStrings()
        {
            Strings s = Resources.Strings.Current;

            _mDefectIconToolTipText = s.ResultRowSetter_DefectIcon_ToolTip_Text;
            _mDuplPairIconToolTipText = s.ResultRowSetter_DuplPairIcon_ToolTip_Text;

            _mUnknownDefectIconToolTipText = s.ResultRowSetter_UnknownDefectIcon_ToolTip_Text;
            _mJpegEndMarkerIsAbsentIconToolTipText = s.ResultRowSetter_JpegEndMarkerIsAbsentIcon_ToolTip_Text;
            _mBlockinessIconToolTipText = s.ResultRowSetter_blockinessIcon_ToolTip_Text;
            _mBlurringIconToolTipText = s.ResultRowSetter_blurringIcon_ToolTip_Text;

            _mDeleteDefectIconToolTipText = s.ResultRowSetter_DeleteDefectIcon_ToolTip_Text;
            _mDeleteFirstIconToolTipText = s.ResultRowSetter_DeleteFirstIcon_ToolTip_Text;
            _mDeleteSecondIconToolTipText = s.ResultRowSetter_DeleteSecondIcon_ToolTip_Text;
            _mRenameFirstToSecondIconToolTipText = s.ResultRowSetter_RenameFirstToSecondIcon_ToolTip_Text;
            _mRenameSecondToFirstIconToolTipText = s.ResultRowSetter_RenameSecondToFirstIcon_ToolTip_Text;

            _mTurn0IconToolTipText = s.ResultRowSetter_Turn_0_Icon_ToolTip_Text;
            _mTurn90IconToolTipText = s.ResultRowSetter_Turn_90_Icon_ToolTip_Text;
            _mTurn180IconToolTipText = s.ResultRowSetter_Turn_180_Icon_ToolTip_Text;
            _mTurn270IconToolTipText = s.ResultRowSetter_Turn_270_Icon_ToolTip_Text;
            _mMirrorTurn0IconToolTipText = s.ResultRowSetter_MirrorTurn_0_Icon_ToolTip_Text;
            _mMirrorTurn90IconToolTipText = s.ResultRowSetter_MirrorTurn_90_Icon_ToolTip_Text;
            _mMirrorTurn180IconToolTipText = s.ResultRowSetter_MirrorTurn_180_Icon_ToolTip_Text;
            _mMirrorTurn270IconToolTipText = s.ResultRowSetter_MirrorTurn_270_Icon_ToolTip_Text;
        }

        public void Set(AdResultW result, DataGridViewCustomRow row)
        {
            switch (result.Type)
            {
                case ResultType.DefectImage:
                    SetDefectToRow(row.Cells, result);
                    break;
                case ResultType.DuplImagePair:
                    SetDuplPairToRow(row.Cells, result);
                    break;
            }
        }

        private void SetDefectToRow(DataGridViewCellCollection cells, AdResultW result)
        {
            if (result.Type != ResultType.DefectImage)
                throw new Exception("Bad result type!");

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Type] = new DataGridViewImageCell
            {
                Value = _mDefectIcon,
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ToolTipText = _mDefectIconToolTipText
            };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Group] = new DataGridViewTextBoxCell
            {
                Value = result.Group == -1 ? "" : result.Group.ToString(),
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.GroupSize] = new DataGridViewTextBoxCell
            {
                Value = result.GroupSize == -1 ? "" : result.GroupSize.ToString(),
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Difference].Value = "";

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect] = new DataGridViewImageCell
            {
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            switch (result.Defect)
            {
                case DefectType.None:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Value = _mNullIcon;
                    break;
                case DefectType.Unknown:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Value = _mUnknownDefectIcon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].ToolTipText = _mUnknownDefectIconToolTipText;
                    break;
                case DefectType.JpegEndMarkerIsAbsent:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Value = _mJpegEndMarkerIsAbsentIcon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].ToolTipText = _mJpegEndMarkerIsAbsentIconToolTipText;
                    break;
                case DefectType.Blockiness:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Value = _mBlockinessIcon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].ToolTipText = _mBlockinessIconToolTipText;
                    break;
                case DefectType.Blurring:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Value = _mBlurringIcon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect].ToolTipText = _mBlurringIconToolTipText;
                    break;
            }

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform] = new DataGridViewTextBoxCell { Value = "" };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint] = new DataGridViewImageCell
            {
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            switch (result.Hint)
            {
                case HintType.DeleteFirst:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Value = _mDeleteDefectIcon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint].ToolTipText = _mDeleteDefectIconToolTipText;
                    break;
                default:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Value = _mNullIcon;
                    break;
            }

            switch (_mOptions.resultsOptions.ViewMode)
            {
                case ViewMode.VerticalPairTable:
                    SetDefectToRowVertical(cells, result);
                    break;
                case ViewMode.HorizontalPairTable:
                    SetDefectToRowHorizontal(cells, result);
                    break;
            }
        }

        /// <summary>
        /// Set cell defect in vertical mode.
        /// Установка яйчейки дефектов в вертикальном режиме.
        /// </summary>
        private static void SetDefectToRowVertical(DataGridViewCellCollection cells, AdResultW result)
        {
            for (int col = (int)ResultsListView.ColumnsTypeVertical.FileName; col < (int)ResultsListView.ColumnsTypeVertical.Size; col++)
                cells[col] = new DataGridViewTextBoxCell();

            cells[(int)ResultsListView.ColumnsTypeVertical.FileName].Value = Path.GetFileName(result.First.Path);
            cells[(int)ResultsListView.ColumnsTypeVertical.FileDirectory].Value = result.First.GetDirectoryString();
            cells[(int)ResultsListView.ColumnsTypeVertical.ImageSize].Value = result.First.GetImageSizeString();
            cells[(int)ResultsListView.ColumnsTypeVertical.ImageType].Value = result.First.GetImageTypeString();

            cells[(int)ResultsListView.ColumnsTypeVertical.Blockiness].Value = result.First.GetBlockinessString();
            cells[(int)ResultsListView.ColumnsTypeVertical.Blockiness].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            cells[(int)ResultsListView.ColumnsTypeVertical.Blurring].Value = result.First.GetBlurringString();
            cells[(int)ResultsListView.ColumnsTypeVertical.Blurring].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            cells[(int)ResultsListView.ColumnsTypeVertical.FileSize].Value = result.First.GetFileSizeString();
            cells[(int)ResultsListView.ColumnsTypeVertical.FileSize].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            cells[(int)ResultsListView.ColumnsTypeVertical.FileTime].Value = result.First.GetFileTimeString();
        }

        /// <summary>
        /// Set cell defect in horizontal mode.
        /// Установка яйчейки дефектов в горизонтальном режиме.
        /// </summary>
        private static void SetDefectToRowHorizontal(DataGridViewCellCollection cells, AdResultW result)
        {
            for (int col = (int)ResultsListView.ColumnsTypeHorizontal.FirstFileName; col < (int)ResultsListView.ColumnsTypeHorizontal.Size; col++)
                cells[col] = new DataGridViewTextBoxCell();

            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileName].Value = Path.GetFileName(result.First.Path);
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileDirectory].Value = result.First.GetDirectoryString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageSize].Value = result.First.GetImageSizeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageType].Value = result.First.GetImageTypeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Value = result.First.GetBlockinessString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Value = result.First.GetBlurringString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Value = result.First.GetFileSizeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileTime].Value = result.First.GetFileTimeString();

            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileName].Value = "";
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileDirectory].Value = "";
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageSize].Value = "";
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageType].Value = "";
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Value = "";
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileTime].Value = "";
        }

        private void SetDuplPairToRow(DataGridViewCellCollection cells, AdResultW result)
        {
            if (result.Type != ResultType.DuplImagePair)
                throw new Exception("Bad result type!");

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Type] = new DataGridViewImageCell
            {
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ToolTipText = _mDuplPairIconToolTipText
            };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Group] = new DataGridViewTextBoxCell
            {
                Value = result.Group == -1 ? "" : result.Group.ToString(),
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.GroupSize] = new DataGridViewTextBoxCell
            {
                Value = result.GroupSize == -1 ? "" : result.GroupSize.ToString(),
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Difference] = new DataGridViewTextBoxCell
            {
                Value = result.Difference.ToString("F2"),
                Style =
                {
                    Font = new Font(Control.DefaultFont,
                                    result.Difference == 0 ? FontStyle.Bold : FontStyle.Regular),
                    ForeColor = result.Difference == 0 ? Color.LightGreen : Control.DefaultForeColor,
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Defect] = new DataGridViewTextBoxCell { Value = "" };

            cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform] = new DataGridViewImageCell
            {
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            switch (result.Transform)
            {
                case TransformType.Turn_0:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mTurn0Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mTurn0IconToolTipText;
                    break;
                case TransformType.Turn_90:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mTurn90Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mTurn90IconToolTipText;
                    break;
                case TransformType.Turn_180:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mTurn180Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mTurn180IconToolTipText;
                    break;
                case TransformType.Turn_270:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mTurn270Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mTurn270IconToolTipText;
                    break;
                case TransformType.MirrorTurn_0:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mMirrorTurn0Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mMirrorTurn0IconToolTipText;
                    break;
                case TransformType.MirrorTurn_90:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mMirrorTurn90Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mMirrorTurn90IconToolTipText;
                    break;
                case TransformType.MirrorTurn_180:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mMirrorTurn180Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mMirrorTurn180IconToolTipText;
                    break;
                case TransformType.MirrorTurn_270:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Value = _mMirrorTurn270Icon;
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Transform].ToolTipText = _mMirrorTurn270IconToolTipText;
                    break;
            }

            cells[(int)ResultsListView.ColumnsTypeVertical.Hint] = new DataGridViewImageCell
            {
                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            switch (result.Hint)
            {
                case HintType.DeleteFirst:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].ToolTipText = _mDeleteFirstIconToolTipText;
                    break;
                case HintType.DeleteSecond:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].ToolTipText = _mDeleteSecondIconToolTipText;
                    break;
                case HintType.RenameFirstToSecond:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].ToolTipText = _mRenameFirstToSecondIconToolTipText;
                    break;
                case HintType.RenameSecondToFirst:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].ToolTipText = _mRenameSecondToFirstIconToolTipText;
                    break;
                default:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].Value = _mNullIcon;
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].ToolTipText = "";
                    break;
            }

            switch (_mOptions.resultsOptions.ViewMode)
            {
                case ViewMode.VerticalPairTable:
                    SetDuplPairToRowVertical(cells, result);
                    break;
                case ViewMode.HorizontalPairTable:
                    SetDuplPairToRowHorizontal(cells, result);
                    break;
            }
        }

        /// <summary>
        /// Set cell duplicate pair in vertical mode.
        /// Установка яйчеек пар дубликатов в вертикальном режиме.
        /// </summary>
        private void SetDuplPairToRowVertical(DataGridViewCellCollection cells, AdResultW result)
        {
            cells[(int)ResultsListView.ColumnsTypeVertical.Type].Value = _mDuplPairVerticalIcon;

            switch (result.Hint)
            {
                case HintType.DeleteFirst:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].Value = _mDeleteFirstVerticalIcon;
                    break;
                case HintType.DeleteSecond:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].Value = _mDeleteSecondVerticalIcon;
                    break;
                case HintType.RenameFirstToSecond:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].Value = _mRenameFirstToSecondVerticalIcon;
                    break;
                case HintType.RenameSecondToFirst:
                    cells[(int)ResultsListView.ColumnsTypeVertical.Hint].Value = _mRenameSecondToFirstVerticalIcon;
                    break;
            }

            cells[(int)ResultsListView.ColumnsTypeVertical.FileName] =
                new DataGridViewDoubleTextBoxCell(Path.GetFileName(result.First.Path),
                                                  Path.GetFileName(result.Second.Path));
            cells[(int)ResultsListView.ColumnsTypeVertical.FileDirectory] =
                new DataGridViewDoubleTextBoxCell(result.First.GetDirectoryString(),
                                                  result.Second.GetDirectoryString());

            DataGridViewDoubleTextBoxCell doubleCell = new DataGridViewDoubleTextBoxCell(result.First.GetImageSizeString(), result.Second.GetImageSizeString());
            if (result.First.Height * result.First.Width > result.Second.Height * result.Second.Width)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.Second;
            else if (result.First.Height * result.First.Width < result.Second.Height * result.Second.Width)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.First;
            cells[(int)ResultsListView.ColumnsTypeVertical.ImageSize] = doubleCell;

            doubleCell = new DataGridViewDoubleTextBoxCell(result.First.GetImageTypeString(), result.Second.GetImageTypeString());
            if (result.First.Type != result.Second.Type)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.Both;
            cells[(int)ResultsListView.ColumnsTypeVertical.ImageType] = doubleCell;

            doubleCell = new DataGridViewDoubleTextBoxCell(result.First.GetFileSizeString(), result.Second.GetFileSizeString());
            if (result.First.Size > result.Second.Size)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.Second;
            else if (result.First.Size < result.Second.Size)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.First;
            cells[(int)ResultsListView.ColumnsTypeVertical.FileSize] = doubleCell;
            cells[(int)ResultsListView.ColumnsTypeVertical.FileSize].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            doubleCell = new DataGridViewDoubleTextBoxCell(result.First.GetBlockinessString(), result.Second.GetBlockinessString());
            if (result.First.Blockiness > result.Second.Blockiness) //подсветка highlight
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.First;
            else if (result.First.Blockiness < result.Second.Blockiness)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.Second;
            cells[(int)ResultsListView.ColumnsTypeVertical.Blockiness] = doubleCell;
            cells[(int)ResultsListView.ColumnsTypeVertical.Blockiness].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            doubleCell = new DataGridViewDoubleTextBoxCell(result.First.GetBlurringString(), result.Second.GetBlurringString());
            if (result.First.Blurring > result.Second.Blurring)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.First;
            else if (result.First.Blurring < result.Second.Blurring)
                doubleCell.markType = DataGridViewDoubleTextBoxCell.MarkType.Second;
            cells[(int)ResultsListView.ColumnsTypeVertical.Blurring] = doubleCell;
            cells[(int)ResultsListView.ColumnsTypeVertical.Blurring].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            cells[(int)ResultsListView.ColumnsTypeVertical.FileTime] = new DataGridViewDoubleTextBoxCell(result.First.GetFileTimeString(), result.Second.GetFileTimeString());
        }

        private void SetDuplPairToRowHorizontal(DataGridViewCellCollection cells, AdResultW result)
        {
            cells[(int)ResultsListView.ColumnsTypeHorizontal.Type].Value = _mDuplPairHorizontalIcon;

            switch (result.Hint)
            {
                case HintType.DeleteFirst:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Value = _mDeleteFirstHorizontalIcon;
                    break;
                case HintType.DeleteSecond:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Value = _mDeleteSecondHorizontalIcon;
                    break;
                case HintType.RenameFirstToSecond:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Value = _mRenameFirstToSecondHorizontalIcon;
                    break;
                case HintType.RenameSecondToFirst:
                    cells[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Value = _mRenameSecondToFirstHorizontalIcon;
                    break;
            }

            for (int col = (int)ResultsListView.ColumnsTypeHorizontal.FirstFileName; col < (int)ResultsListView.ColumnsTypeHorizontal.Size; col++)
                cells[col] = new DataGridViewTextBoxCell();

            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileName].Value = Path.GetFileName(result.First.Path);
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileDirectory].Value = result.First.GetDirectoryString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageSize].Value = result.First.GetImageSizeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageType].Value = result.First.GetImageTypeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Value = result.First.GetBlockinessString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Value = result.First.GetBlurringString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Value = result.First.GetFileSizeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileTime].Value = result.First.GetFileTimeString();

            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileName].Value = Path.GetFileName(result.Second.Path);
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileDirectory].Value = result.Second.GetDirectoryString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageSize].Value = result.Second.GetImageSizeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageType].Value = result.Second.GetImageTypeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlockiness].Value = result.Second.GetBlockinessString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlockiness].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlurring].Value = result.Second.GetBlurringString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlurring].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Value = result.Second.GetFileSizeString();
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileTime].Value = result.Second.GetFileTimeString();

            if (result.First.Height * result.First.Width > result.Second.Height * result.Second.Width) //подсветка highlight
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageSize].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageSize].Style.ForeColor = Color.Red;
            }
            else if (result.First.Height * result.First.Width < result.Second.Height * result.Second.Width)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageSize].Style.ForeColor = Color.Red;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageSize].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
            }

            if (result.First.Size > result.Second.Size)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Style.ForeColor = Color.Red;
            }
            else if (result.First.Size < result.Second.Size)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Style.ForeColor = Color.Red;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
            }

            if (result.First.Blockiness > result.Second.Blockiness)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Style.ForeColor = Color.Red;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlockiness].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
            }
            else if (result.First.Blurring < result.Second.Blockiness)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlockiness].Style.ForeColor = Color.Red;
            }

            if (result.First.Blurring > result.Second.Blurring)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Style.ForeColor = Color.Red;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlurring].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
            }
            else if (result.First.Blurring < result.Second.Blurring)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Style.ForeColor = _mDataGridView.DefaultCellStyle.ForeColor;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlurring].Style.ForeColor = Color.Red;
            }

            if (result.First.Type != result.Second.Type)
            {
                cells[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageType].Style.ForeColor = Color.Red;
                cells[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageType].Style.ForeColor = Color.Red;
            }
        }
    }
}
