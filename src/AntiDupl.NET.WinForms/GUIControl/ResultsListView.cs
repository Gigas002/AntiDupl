﻿/*
* AntiDupl.NET Program (http://ermig1979.github.io/AntiDupl).
*
* Copyright (c) 2002-2018 Yermalayeu Ihar, 2013-2015 Borisov Dmitry.
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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AntiDupl.NET.Core;
using AntiDupl.NET.Core.Enums;
using AntiDupl.NET.Core.Original;
using AntiDupl.NET.WinForms.Forms;

namespace AntiDupl.NET.WinForms.GUIControl
{
    /// <summary>
    /// Class output result in ListView.
    /// Класс вывода результатов в ListView.
    /// </summary>
    public class ResultsListView : DataGridView
    {
        public enum ColumnsTypeVertical
        {
            Type,
            Group,
            GroupSize,
            Difference,
            Defect,
            Transform,
            Hint,
            FileName,
            FileDirectory,
            ImageSize,
            ImageType,
            Blockiness,
            Blurring,
            FileSize,
            FileTime,
            Size
        }

        public enum ColumnsTypeHorizontal
        {
            Type,
            Group,
            GroupSize,
            Difference,
            Defect,
            Transform,
            Hint,
            FirstFileName,
            FirstFileDirectory,
            FirstImageSize,
            FirstImageType,
            FirstBlockiness,
            FirstBlurring,
            FirstFileSize,
            FirstFileTime,
            SecondFileName,
            SecondFileDirectory,
            SecondImageSize,
            SecondImageType,
            SecondBlockiness,
            SecondBlurring,
            SecondFileSize,
            SecondFileTime,
            Size
        }

        private MainSplitContainer m_mainSplitContainer;
        private AntiDuplCore m_core;
        private Options m_options;
        public CoreOptions CoreOptions { get { return m_coreOptions; } }
        private CoreOptions m_coreOptions;
        private AdResultW[] m_results;
        private ViewMode m_viewMode = ViewMode.VerticalPairTable;

        private int m_firstSelectedRowIndex = -1;
        private int m_currentRowIndex = -1;
        private int m_rowCountOnPage = 1;
        private bool m_isShiftDown = false;
        private bool m_isControlDown = false;
        private bool m_updateColumnOrder = false;
        private bool m_makeAction = false;
        private bool m_isMouseDragSelecting = false;
        private int m_firstDragSelectedRowIndex = -1;
        private int m_lastDragSelectedRowIndex = -1;
        private int m_lowestDragSelectedIndex = -1;
        private int m_highestDragSelectedIndex = -1;

        ContextMenuStrip m_contextMenuStrip;
        ResultRowSetter m_resultRowSetter;

        /// <summary>
        /// Returns the lower value of the first and last drag-selected row indices.
        /// </summary>
        private int LowerDragSelectedRowIndex
        {
            get
            {
                return Math.Min(m_firstDragSelectedRowIndex, m_lastDragSelectedRowIndex);
            }
        }

        /// <summary>
        /// Returns the higher value of the first and last drag-selected row indices.
        /// </summary>
        private int HigherDragSelectedRowIndex
        {
            get
            {
                return Math.Max(m_firstDragSelectedRowIndex, m_lastDragSelectedRowIndex);
            }
        }

        public ResultsListView(AntiDuplCore core, Options options, CoreOptions coreOptions, MainSplitContainer mainSplitContainer)
        {
            m_core = core;
            m_options = options;
            m_coreOptions = coreOptions;
            m_mainSplitContainer = mainSplitContainer;
            m_results = new AdResultW[0];
            m_resultRowSetter = new ResultRowSetter(m_options, this);
            InitializeComponents();
            if (m_options.resultsOptions.ViewMode == ViewMode.VerticalPairTable)
            {
                m_viewMode = ViewMode.HorizontalPairTable;
                SetViewMode(ViewMode.VerticalPairTable);
            }
            else
            {
                m_viewMode = ViewMode.VerticalPairTable;
                SetViewMode(ViewMode.HorizontalPairTable);
            }
            SetCurrentRow(0);
            Resources.Strings.OnCurrentChange += new Resources.Strings.CurrentChangeHandler(UpdateStrings);
        }

        private void InitializeComponents()
        {
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            ColumnHeadersVisible = true;
            RowHeadersVisible = false;
            CellBorderStyle = DataGridViewCellBorderStyle.Single;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AllowUserToResizeRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            BackgroundColor = Color.White;
            ReadOnly = true;
            ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            DoubleBuffered = true;
            Location = new Point(0, 0);
            Dock = DockStyle.Fill;
            m_contextMenuStrip = new ResultsListViewContextMenu(m_core, m_options, m_coreOptions, m_mainSplitContainer);
            m_contextMenuStrip.KeyUp += new KeyEventHandler(OnContextMenuKeyUp);
        }

        public void UpdateStrings()
        {
            Strings s = Resources.Strings.Current;

            Columns[(int)ColumnsTypeVertical.Type].Name = s.ResultsListView_Type_Column_Text;
            Columns[(int)ColumnsTypeVertical.Group].Name = s.ResultsListView_Group_Column_Text;
            Columns[(int)ColumnsTypeVertical.GroupSize].Name = s.ResultsListView_GroupSize_Column_Text;
            Columns[(int)ColumnsTypeVertical.Difference].Name = s.ResultsListView_Difference_Column_Text;
            Columns[(int)ColumnsTypeVertical.Defect].Name = s.ResultsListView_Defect_Column_Text;
            Columns[(int)ColumnsTypeVertical.Transform].Name = s.ResultsListView_Transform_Column_Text;
            Columns[(int)ColumnsTypeVertical.Hint].Name = s.ResultsListView_Hint_Column_Text;
            if (m_options.resultsOptions.ViewMode == ViewMode.VerticalPairTable)
            {
                Columns[(int)ColumnsTypeVertical.FileName].Name = s.ResultsListView_FileName_Column_Text;
                Columns[(int)ColumnsTypeVertical.FileDirectory].Name = s.ResultsListView_FileDirectory_Column_Text;
                Columns[(int)ColumnsTypeVertical.ImageSize].Name = s.ResultsListView_ImageSize_Column_Text;
                Columns[(int)ColumnsTypeVertical.ImageType].Name = s.ResultsListView_ImageType_Column_Text;
                Columns[(int)ColumnsTypeVertical.Blockiness].Name = s.ResultsListView_Blockiness_Column_Text;
                Columns[(int)ColumnsTypeVertical.Blurring].Name = s.ResultsListView_Blurring_Column_Text;
                Columns[(int)ColumnsTypeVertical.FileSize].Name = s.ResultsListView_FileSize_Column_Text;
                Columns[(int)ColumnsTypeVertical.FileTime].Name = s.ResultsListView_FileTime_Column_Text;
            }
            if (m_options.resultsOptions.ViewMode == ViewMode.HorizontalPairTable)
            {
                Columns[(int)ColumnsTypeHorizontal.FirstFileName].Name = s.ResultsListView_FirstFileName_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.FirstFileDirectory].Name = s.ResultsListView_FirstFileDirectory_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.FirstImageSize].Name = s.ResultsListView_FirstImageSize_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.FirstImageType].Name = s.ResultsListView_FirstImageType_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.FirstBlockiness].Name = s.ResultsListView_FirstBlockiness_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.FirstBlurring].Name = s.ResultsListView_FirstBlurring_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.FirstFileSize].Name = s.ResultsListView_FirstFileSize_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.FirstFileTime].Name = s.ResultsListView_FirstFileTime_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondFileName].Name = s.ResultsListView_SecondFileName_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondFileDirectory].Name = s.ResultsListView_SecondFileDirectory_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondImageSize].Name = s.ResultsListView_SecondImageSize_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondImageType].Name = s.ResultsListView_SecondImageType_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondBlockiness].Name = s.ResultsListView_SecondBlockiness_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondBlurring].Name = s.ResultsListView_SecondBlurring_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondFileSize].Name = s.ResultsListView_SecondFileSize_Column_Text;
                Columns[(int)ColumnsTypeHorizontal.SecondFileTime].Name = s.ResultsListView_SecondFileTime_Column_Text;
            }

            m_mainSplitContainer.UpdateResults();
        }

        public void UpdateColumnsVisibility()
        {
            m_updateColumnOrder = false;
            if (m_options.resultsOptions.ViewMode == ViewMode.VerticalPairTable)
            {
                for (int i = 0; i < (int)ColumnsTypeVertical.Size; i++)
                {
                    Columns[i].Visible = m_options.resultsOptions.ColumnOptionsVertical[i].Visible;
                }
            }
            if (m_options.resultsOptions.ViewMode == ViewMode.HorizontalPairTable)
            {
                for (int i = 0; i < (int)ColumnsTypeHorizontal.Size; i++)
                {
                    Columns[i].Visible = m_options.resultsOptions.ColumnOptionsHorizontal[i].Visible;
                }
            }
            m_updateColumnOrder = true;
        }

        public int GetTotalResultCount()
        {
            return m_results.Length;
        }

        public int GetCurrentRowIndex()
        {
            return m_currentRowIndex;
        }

        public int GetSelectedResultCount()
        {
            int selectedResultsCount = 0;
            for (int i = 0; i < Rows.Count; i++)
            {
                DataGridViewCustomRow row = (DataGridViewCustomRow)Rows[i];
                if (row.selected && i >= 0 && i < m_results.Length)
                {
                    selectedResultsCount++;
                }
            }
            return selectedResultsCount;
        }

        public void MakeAction(LocalActionType action, TargetType target)
        {
            m_makeAction = true;
            ProgressForm progressForm = new ProgressForm(action, target, m_core, m_options, m_coreOptions, m_mainSplitContainer);
            progressForm.Execute();
            m_makeAction = false;
        }

        public void RefreshResults()
        {
            ProgressForm progressForm = new ProgressForm(ProgressForm.Type.RefreshResults, m_core, m_options, m_coreOptions, m_mainSplitContainer);
            progressForm.Execute();
        }

        /// <summary>
        /// Меняет путь у текущей картинки на заданный.
        /// </summary>
        /// <param name="renameCurrentType"></param>
        /// <param name="newFileName">Новый путь</param>
        public void RenameCurrent(RenameCurrentType renameCurrentType, string newFileName)
        {
            m_makeAction = true;
            ProgressForm progressForm = new ProgressForm(renameCurrentType, newFileName, m_core, m_options, m_coreOptions, m_mainSplitContainer);
            progressForm.Execute();
            m_makeAction = false;
        }

        /// <summary>
        /// Перенести текущую группу в папку.
        /// </summary>
        /// <param name="directory"></param>
        public void MoveCurrentGroupToDirectory(string directory)
        {
            m_makeAction = true;
            ProgressForm progressForm = new ProgressForm(ProgressForm.Type.MoveCurrentGroup, directory, m_core, m_options, m_coreOptions, m_mainSplitContainer);
            progressForm.Execute();
            m_makeAction = false;
        }

        public void RenameCurrentGroupAs(string filename)
        {
            m_makeAction = true;
            ProgressForm progressForm = new ProgressForm(ProgressForm.Type.RenameCurrentGroupAs, filename, m_core, m_options, m_coreOptions, m_mainSplitContainer);
            progressForm.Execute();
            m_makeAction = false;
        }

        /// <summary>
        /// MakeAction by hotkey.
        /// </summary>
        /// <param name="hotKey"></param>
        private void MakeAction(Keys hotKey)
        {
            if (hotKey == (Keys.Z | Keys.Control) && m_core.CanApply(ActionEnableType.Undo))
            {
                ProgressForm progressForm = new ProgressForm(ProgressForm.Type.Undo, m_core, m_options, m_coreOptions, m_mainSplitContainer);
                progressForm.Execute();
                return;
            }

            if (hotKey == (Keys.Y | Keys.Control) && m_core.CanApply(ActionEnableType.Redo))
            {
                ProgressForm progressForm = new ProgressForm(ProgressForm.Type.Redo, m_core, m_options, m_coreOptions, m_mainSplitContainer);
                progressForm.Execute();
                return;
            }

            if (m_currentRowIndex >= 0 && m_currentRowIndex < m_results.Length)
            {
                if (m_results[m_currentRowIndex].Type == ResultType.DefectImage)
                {
                    if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentDefectDelete])
                        MakeAction(LocalActionType.DeleteDefect, TargetType.Current);
                    else if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentMistake])
                        MakeAction(LocalActionType.Mistake, TargetType.Current);
                    return;
                }
                if (m_results[m_currentRowIndex].Type == ResultType.DuplImagePair)
                {
                    if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentDuplPairDeleteFirst])
                        MakeAction(LocalActionType.DeleteFirst, TargetType.Current);
                    else if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentDuplPairDeleteSecond])
                        MakeAction(LocalActionType.DeleteSecond, TargetType.Current);
                    else if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentDuplPairDeleteBoth])
                        MakeAction(LocalActionType.DeleteBoth, TargetType.Current);
                    else if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentDuplPairRenameFirstToSecond])
                        MakeAction(LocalActionType.RenameFirstToSecond, TargetType.Current);
                    else if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentDuplPairRenameSecondToFirst])
                        MakeAction(LocalActionType.RenameSecondToFirst, TargetType.Current);
                    else if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.CurrentMistake])
                        MakeAction(LocalActionType.Mistake, TargetType.Current);
                    else if (hotKey == m_options.hotKeyOptions.keys[(int)HotKeyOptions.Action.ShowNeighbours])
                        m_options.resultsOptions.ShowNeighboursImages = !m_options.resultsOptions.ShowNeighboursImages;
                    return;
                }
            }
        }

        public void UpdateResults()
        {
            GetResults();
            UpdateRows();
            Invalidate();
        }

        public void ClearResults()
        {
            m_results = new AdResultW[0];
            Rows.Clear();
            RowCount = 1;
            m_currentRowIndex = 0;
        }

        private void GetResults()
        {
            uint resultSize = m_core.GetResultSize();
            if (resultSize == 0)
            {
                m_results = new AdResultW[0];
                return;
            }
            m_results = m_core.GetResult(0, resultSize);
        }

        private void UpdateRows()
        {
            if (m_results.Length == 0)
            {
                Rows.Clear();
                RowCount = 1;
                SetCurrentRow(0);
            }
            else
            {
                if (m_results.Length < RowCount - 1000)//rows are removed very slowly!!!
                    Rows.Clear();
                RowCount = m_results.Length;

                bool[] selection = m_core.GetSelection(0, (uint)m_results.Length);

                for (int i = 0; i < Rows.Count; i++)
                {
                    DataGridViewCustomRow row = (DataGridViewCustomRow)Rows[i];
                    row.updated = false;
                    if (selection != null)
                        row.selected = selection[i];
                }
                int current = m_core.GetCurrent();
                if (current != -1)
                {
                    SetCurrentRow(current);
                }
                else
                {
                    SetCurrentRow(0);
                    SetRowSelection(0, 1, true);
                }
            }
        }

        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewCustomRow row = (DataGridViewCustomRow)Rows[index];
            if (!row.updated && index >= 0 && index < m_results.Length)
            {
                m_resultRowSetter.Set(m_results[index], row);
                row.updated = true;
            }
            row.current = (index == m_currentRowIndex);
            base.OnRowPrePaint(e);
        }

        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn dataGridViewColumn = Columns[e.ColumnIndex];

            SortType sortType = SortType.ByType;
            if (m_options.resultsOptions.ViewMode == ViewMode.VerticalPairTable)
            {
                switch ((ColumnsTypeVertical)e.ColumnIndex)
                {
                    case ColumnsTypeVertical.Type:
                        sortType = SortType.ByType;
                        break;
                    case ColumnsTypeVertical.Group:
                        sortType = SortType.ByGroup;
                        break;
                    case ColumnsTypeVertical.GroupSize:
                        sortType = SortType.ByGroupSize;
                        break;
                    case ColumnsTypeVertical.Difference:
                        sortType = SortType.ByDifference;
                        break;
                    case ColumnsTypeVertical.Defect:
                        sortType = SortType.ByDefect;
                        break;
                    case ColumnsTypeVertical.Transform:
                        sortType = SortType.ByTransform;
                        break;
                    case ColumnsTypeVertical.Hint:
                        sortType = SortType.ByHint;
                        break;
                    case ColumnsTypeVertical.FileName:
                        sortType = SortType.BySortedName;
                        break;
                    case ColumnsTypeVertical.FileDirectory:
                        sortType = SortType.BySortedDirectory;
                        break;
                    case ColumnsTypeVertical.ImageSize:
                        sortType = SortType.BySortedArea;
                        break;
                    case ColumnsTypeVertical.ImageType:
                        sortType = SortType.BySortedType;
                        break;
                    case ColumnsTypeVertical.Blockiness:
                        sortType = SortType.BySortedBlockiness;
                        break;
                    case ColumnsTypeVertical.Blurring:
                        sortType = SortType.BySortedBlurring;
                        break;
                    case ColumnsTypeVertical.FileSize:
                        sortType = SortType.BySortedSize;
                        break;
                    case ColumnsTypeVertical.FileTime:
                        sortType = SortType.BySortedTime;
                        break;
                }
            }
            if (m_options.resultsOptions.ViewMode == ViewMode.HorizontalPairTable)
            {
                switch ((ColumnsTypeHorizontal)e.ColumnIndex)
                {
                    case ColumnsTypeHorizontal.Type:
                        sortType = SortType.ByType;
                        break;
                    case ColumnsTypeHorizontal.Group:
                        sortType = SortType.ByGroup;
                        break;
                    case ColumnsTypeHorizontal.GroupSize:
                        sortType = SortType.ByGroupSize;
                        break;
                    case ColumnsTypeHorizontal.Difference:
                        sortType = SortType.ByDifference;
                        break;
                    case ColumnsTypeHorizontal.Defect:
                        sortType = SortType.ByDefect;
                        break;
                    case ColumnsTypeHorizontal.Transform:
                        sortType = SortType.ByTransform;
                        break;
                    case ColumnsTypeHorizontal.Hint:
                        sortType = SortType.ByHint;
                        break;
                    case ColumnsTypeHorizontal.FirstFileName:
                        sortType = SortType.ByFirstName;
                        break;
                    case ColumnsTypeHorizontal.FirstFileDirectory:
                        sortType = SortType.ByFirstDirectory;
                        break;
                    case ColumnsTypeHorizontal.FirstImageSize:
                        sortType = SortType.ByFirstArea;
                        break;
                    case ColumnsTypeHorizontal.FirstImageType:
                        sortType = SortType.ByFirstType;
                        break;
                    case ColumnsTypeHorizontal.FirstFileSize:
                        sortType = SortType.ByFirstSize;
                        break;
                    case ColumnsTypeHorizontal.FirstBlockiness:
                        sortType = SortType.ByFirstBlockiness;
                        break;
                    case ColumnsTypeHorizontal.FirstBlurring:
                        sortType = SortType.ByFirstBlurring;
                        break;
                    case ColumnsTypeHorizontal.FirstFileTime:
                        sortType = SortType.ByFirstTime;
                        break;
                    case ColumnsTypeHorizontal.SecondFileName:
                        sortType = SortType.BySecondName;
                        break;
                    case ColumnsTypeHorizontal.SecondFileDirectory:
                        sortType = SortType.BySecondDirectory;
                        break;
                    case ColumnsTypeHorizontal.SecondImageSize:
                        sortType = SortType.BySecondArea;
                        break;
                    case ColumnsTypeHorizontal.SecondImageType:
                        sortType = SortType.BySecondType;
                        break;
                    case ColumnsTypeHorizontal.SecondFileSize:
                        sortType = SortType.BySecondSize;
                        break;
                    case ColumnsTypeHorizontal.SecondBlockiness:
                        sortType = SortType.BySecondBlockiness;
                        break;
                    case ColumnsTypeHorizontal.SecondBlurring:
                        sortType = SortType.BySecondBlurring;
                        break;
                    case ColumnsTypeHorizontal.SecondFileTime:
                        sortType = SortType.BySecondTime;
                        break;
                }
            }

            ListSortDirection direction = ListSortDirection.Ascending;
            switch (dataGridViewColumn.HeaderCell.SortGlyphDirection)
            {
                case SortOrder.None:
                    direction = ListSortDirection.Ascending;
                    dataGridViewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    break;
                case SortOrder.Ascending:
                    direction = ListSortDirection.Descending;
                    dataGridViewColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    break;
                case SortOrder.Descending:
                    direction = ListSortDirection.Ascending;
                    dataGridViewColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    break;
            }

            for (int col = 0; col < ColumnCount; col++)
                if (col != e.ColumnIndex)
                    Columns[col].HeaderCell.SortGlyphDirection = SortOrder.None;

            m_options.resultsOptions.SortTypeDefault = (int)sortType;
            m_options.resultsOptions.IncreasingDefault = direction == ListSortDirection.Ascending;
            m_core.SortResult(sortType, direction == ListSortDirection.Ascending);

            m_mainSplitContainer.UpdateResults();
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            if (m_options.resultsOptions.ViewMode == ViewMode.VerticalPairTable)
            {
                m_options.resultsOptions.ColumnOptionsVertical[e.Column.Index].Width = Columns[e.Column.Index].Width;
            }
            if (m_options.resultsOptions.ViewMode == ViewMode.HorizontalPairTable)
            {
                m_options.resultsOptions.ColumnOptionsHorizontal[e.Column.Index].Width = Columns[e.Column.Index].Width;
            }
        }

        protected override void OnColumnDisplayIndexChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnDisplayIndexChanged(e);
            if (m_updateColumnOrder)
            {
                if (m_options.resultsOptions.ViewMode == ViewMode.VerticalPairTable)
                {
                    m_options.resultsOptions.ColumnOptionsVertical[e.Column.Index].Order = Columns[e.Column.Index].DisplayIndex;
                }
                if (m_options.resultsOptions.ViewMode == ViewMode.HorizontalPairTable)
                {
                    m_options.resultsOptions.ColumnOptionsHorizontal[e.Column.Index].Order = Columns[e.Column.Index].DisplayIndex;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            InvalidateRow(m_currentRowIndex);
            base.OnResize(e);
            m_rowCountOnPage = Math.Max(1, ClientSize.Height / RowTemplate.Height - 1);
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            InvalidateRow(m_currentRowIndex);
            base.OnScroll(e);
        }

        private void OnContextMenuKeyUp(object sender, KeyEventArgs e)
        {
            m_isShiftDown = e.Shift;
            m_isControlDown = e.Control;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            m_isShiftDown = e.Shift;
            m_isControlDown = e.Control;
            base.OnKeyUp(e);
        }

        public void SetKeyDownEvent(KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            m_isShiftDown = e.Shift;
            m_isControlDown = e.Control;
            if (m_isShiftDown)
            {
                if (m_firstSelectedRowIndex == -1)
                    m_firstSelectedRowIndex = m_currentRowIndex;
            }
            base.OnKeyDown(e);
            MakeAction(e.KeyData);
        }

        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (e != null && e.RowIndex >= 0 && e.RowIndex < Rows.Count)
            {
                if (e.Button == MouseButtons.Left)
                {
                    SetCurrentRow(e.RowIndex);
                    SetRowSelection(false);
                    Invalidate();
                    m_isMouseDragSelecting = true;
                    m_firstDragSelectedRowIndex = e.RowIndex;
                    m_lastDragSelectedRowIndex = e.RowIndex;
                    m_lowestDragSelectedIndex = e.RowIndex;
                    m_highestDragSelectedIndex = e.RowIndex;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    DataGridViewCustomRow row = (DataGridViewCustomRow)Rows[e.RowIndex];
                    if (!m_isControlDown && !row.selected)
                    {
                        SetCurrentRow(e.RowIndex);
                        SetRowSelection(false);
                        Invalidate();
                    }
                    ContextMenuStrip = m_core.CanApply(ActionEnableType.Any) ? m_contextMenuStrip : null;
                }
            }
            else
            {
                ContextMenuStrip = null;
            }
            base.OnCellMouseDown(e);
        }

        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            if (m_isMouseDragSelecting && e != null && e.RowIndex >= 0 && e.RowIndex < Rows.Count)
            {
                // Drag-select a range of grid rows
                m_lastDragSelectedRowIndex = e.RowIndex;
                m_lowestDragSelectedIndex = Math.Min(m_lowestDragSelectedIndex, e.RowIndex);
                m_highestDragSelectedIndex = Math.Max(m_highestDragSelectedIndex, e.RowIndex);
                SetCurrentRow(e.RowIndex);
                if (m_lowestDragSelectedIndex < LowerDragSelectedRowIndex)
                {
                    SetRowSelection(m_lowestDragSelectedIndex, LowerDragSelectedRowIndex, false);
                }
                if (m_highestDragSelectedIndex > HigherDragSelectedRowIndex)
                {
                    SetRowSelection(HigherDragSelectedRowIndex + 1, m_highestDragSelectedIndex + 1, false);
                }
                SetRowSelection(LowerDragSelectedRowIndex, HigherDragSelectedRowIndex + 1, true);
                Refresh();
                m_mainSplitContainer.SelectedResultsChanged();
            }
            base.OnCellMouseEnter(e);
        }

        protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        {
            if (e != null && e.Button == MouseButtons.Left)
            {
                m_isMouseDragSelecting = false;
            }
            base.OnCellMouseUp(e);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            Keys keyData = e.KeyData;
            if (e.KeyCode == Keys.Up)
            {
                SetCurrentRow(Math.Max(0, m_currentRowIndex - 1));
                keyData = (e.Shift ? Keys.Shift : 0) | e.KeyCode;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SetCurrentRow(Math.Min(m_results.Length - 1, m_currentRowIndex + 1));
                keyData = (e.Shift ? Keys.Shift : 0) | e.KeyCode;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                SetCurrentRow(Math.Max(0, m_currentRowIndex - m_rowCountOnPage));
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                SetCurrentRow(Math.Min(m_results.Length - 1, m_currentRowIndex + m_rowCountOnPage));
            }
            else if (e.KeyCode == Keys.Home)
            {
                SetCurrentRow(0);
                keyData = Keys.Up | Keys.Control;
            }
            else if (e.KeyCode == Keys.End)
            {
                SetCurrentRow(m_results.Length - 1);
                keyData = Keys.Down | Keys.Control;
            }

            if (e.KeyCode == Keys.A && m_isControlDown)
            {
                SetRowSelection(true);
            }
            else
            {
                if (!m_isControlDown || m_isShiftDown)
                {
                    SetRowSelection(false);
                }
            }
            Invalidate();
            return base.ProcessDataGridViewKey(new KeyEventArgs(keyData));
        }

        private void SetCurrentRow(int index)
        {
            if (index >= 0 && index < Rows.Count)
            {
                if (m_currentRowIndex >= 0 && m_currentRowIndex < Rows.Count)
                    ((DataGridViewCustomRow)Rows[m_currentRowIndex]).current = false;
                if (m_results.Length > 0)
                {
                    ((DataGridViewCustomRow)Rows[index]).current = true;
                    m_core.SetCurrent(index);
                }
                else
                {
                    m_core.SetCurrent(-1);
                }
                m_currentRowIndex = index;

                if (m_firstSelectedRowIndex >= Rows.Count)
                    m_firstSelectedRowIndex = m_currentRowIndex;

                if (m_makeAction)
                {
                    for (int col = 0; col < Rows[index].Cells.Count; col++)
                    {
                        if (Rows[index].Cells[col].Visible)
                        {
                            CurrentCell = Rows[index].Cells[col];
                            break;
                        }
                    }
                }
                m_mainSplitContainer.CurrentResultChanged();
            }
        }

        public void SetRowSelection(bool isControlA)
        {
            if (isControlA)
            {
                SetRowSelection(0, Rows.Count, true);
            }
            else
            {
                if (m_currentRowIndex != -1)
                {
                    if (m_isShiftDown)
                    {
                        if (m_firstSelectedRowIndex == -1 || m_firstSelectedRowIndex >= Rows.Count)
                            m_firstSelectedRowIndex = m_currentRowIndex;

                        int selectionBegin = Math.Min(m_firstSelectedRowIndex, m_currentRowIndex);
                        int selectionEnd = Math.Max(m_firstSelectedRowIndex, m_currentRowIndex);
                        SetRowSelection(0, selectionBegin, false);
                        SetRowSelection(selectionBegin, selectionEnd + 1, true);
                        SetRowSelection(selectionEnd + 1, Rows.Count, false);
                    }
                    else
                    {
                        m_firstSelectedRowIndex = -1;
                        if (!m_isControlDown)
                        {
                            SetRowSelection(0, Rows.Count, false);
                        }
                        bool value = !((DataGridViewCustomRow)Rows[m_currentRowIndex]).selected;
                        SetRowSelection(m_currentRowIndex, m_currentRowIndex + 1, value);
                    }
                }
            }
            m_mainSplitContainer.SelectedResultsChanged();
        }

        private void SetRowSelection(int beginRowIndex, int endRowIndex, bool value)
        {
            for (int i = beginRowIndex; i < endRowIndex; i++)
            {
                DataGridViewCustomRow row = (DataGridViewCustomRow)Rows[i];
                row.selected = value;
            }
            m_core.SetSelection((UInt32)beginRowIndex, (UInt32)(endRowIndex - beginRowIndex), value);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (CanFocus)
            {
                Focus();
            }
        }

        public void SetViewMode(ViewMode viewMode)
        {
            if (m_viewMode != viewMode)
            {
                m_viewMode = viewMode;
                m_updateColumnOrder = false;
                SetColumns(viewMode);
                UpdateStrings();
                UpdateColumnsVisibility();
                ((ResultsListViewContextMenu)m_contextMenuStrip).SetViewMode(viewMode);
                m_updateColumnOrder = true;
            }
        }

        private void SetColumns(ViewMode viewMode)
        {
            Rows.Clear();
            RowCount = 1;
            if (viewMode == ViewMode.VerticalPairTable)
            {
                ColumnCount = (int)ColumnsTypeVertical.Size;
                for (int i = 0; i < (int)ColumnsTypeVertical.Size; i++)
                {
                    Columns[i].Name = ((ColumnsTypeVertical)i).ToString();
                    Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                    Columns[i].Width = m_options.resultsOptions.ColumnOptionsVertical[i].Width;
                    Columns[i].DisplayIndex = m_options.resultsOptions.ColumnOptionsVertical[i].Order;
                }
                Rows[0].Cells[0] = new DataGridViewDoubleTextBoxCell("0", "0");
            }
            if (viewMode == ViewMode.HorizontalPairTable)
            {
                ColumnCount = (int)ColumnsTypeHorizontal.Size;
                for (int i = 0; i < (int)ColumnsTypeHorizontal.Size; i++)
                {
                    Columns[i].Name = ((ColumnsTypeHorizontal)i).ToString();
                    Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                    Columns[i].Width = m_options.resultsOptions.ColumnOptionsHorizontal[i].Width;
                    Columns[i].DisplayIndex = m_options.resultsOptions.ColumnOptionsHorizontal[i].Order;
                }
                Rows[0].Cells[0] = new DataGridViewTextBoxCell();
                Rows[0].Cells[0].Value = "0";
            }
            RowTemplate = new DataGridViewCustomRow();
            RowTemplate.Height = Rows[0].Cells[0].PreferredSize.Height;
            Rows.Clear();
            UpdateRows();
        }

        public override DataObject GetClipboardContent()
        {
            DataObject dataObject = new DataObject();
            if (m_results.Length > 0)
            {
                ClipboardContentBuilder builder = new ClipboardContentBuilder(m_options.resultsOptions);
                for (int i = 0; i < Rows.Count; i++)
                {
                    DataGridViewCustomRow row = (DataGridViewCustomRow)Rows[i];
                    if (row.selected)
                        builder.Add(m_results[i]);
                }
                dataObject.SetText(builder.ToString());
            }
            return dataObject;
        }

        public AdResultW GetCurrentResult()
        {
            if (m_currentRowIndex < m_results.Length && m_currentRowIndex >= 0)
                return m_results[m_currentRowIndex];
            return null;
        }

        public bool MoveEnable()
        {
            bool moveEnable = false;
            if (m_results.Length > 0)
            {
                for (int i = 0; i < Rows.Count; i++)
                {
                    DataGridViewCustomRow row = (DataGridViewCustomRow)Rows[i];
                    if (row.selected)
                    {
                        if (String.IsNullOrEmpty(m_results[i].Second.Path))
                            return false;
                        if (!Path.GetDirectoryName(m_results[i].First.Path).Equals(Path.GetDirectoryName(m_results[i].Second.Path)))
                        {
                            moveEnable = true;
                            break;
                        }
                    }
                }
            }
            return moveEnable;
        }
    }
}
