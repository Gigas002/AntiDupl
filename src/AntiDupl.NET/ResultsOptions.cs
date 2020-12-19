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

using System.Drawing;

namespace AntiDupl.NET
{
    /// <summary>
    /// Setting for table out (ListView).
    /// Установки для таблицы вывода.
    /// </summary>
    public sealed class ResultsOptions
    {
        #region Fields and Properties

        private const int DefaultThumbnailWidthMax = 128;

        private const int DefaultThumbnailHeightMax = 128;

        public delegate void ViewModeChangeHandler(ViewMode viewMode);

        public event ViewModeChangeHandler OnViewModeChange;

        private ViewMode _mViewMode = ViewMode.VerticalPairTable;

        internal ViewMode ViewMode
        {
            get => _mViewMode;
            set
            {
                if (_mViewMode == value) return;

                _mViewMode = value;
                OnViewModeChange?.Invoke(_mViewMode);
            }
        }

        internal bool IsPairTableView() => _mViewMode == ViewMode.VerticalPairTable ||
                                           _mViewMode == ViewMode.HorizontalPairTable;

        public delegate void ImageViewChangeHandler();

        public event ImageViewChangeHandler OnImageViewChange;

        private bool _mStretchSmallImages;

        internal bool StretchSmallImages
        {
            get => _mStretchSmallImages;
            set
            {
                if (_mStretchSmallImages == value) return;

                _mStretchSmallImages = value;
                OnImageViewChange?.Invoke();
            }
        }

        private bool _mProportionalImageSize = true;

        internal bool ProportionalImageSize
        {
            get => _mProportionalImageSize;
            set
            {
                if (_mProportionalImageSize == value) return;

                _mProportionalImageSize = value;
                OnImageViewChange?.Invoke();
            }
        }

        public delegate void HighlightDifferenceChangeHandler();

        public event HighlightDifferenceChangeHandler OnHighlightDifferenceChange;

        internal void RaiseEventOnHighlightDifferenceChange() => OnHighlightDifferenceChange?.Invoke();

        internal bool HighlightDifference { get; set; }

        internal float DifferenceThreshold { get; set; } = 99.90F;

        internal bool NotHighlightIfFragmentsMoreThan { get; set; } = true;

        internal int NotHighlightMaxFragments { get; set; } = 32;

        internal bool HighlightAllDifferences { get; set; } = true;

        internal int MaxFragmentsForHighlight { get; set; } = 10;

        internal int AmountOfFragmentsOnX { get; set; } = 16;

        internal int AmountOfFragmentsOnY { get; set; } = 16;

        internal int NormalizedSizeOfImage { get; set; } = 512;

        internal int PenThickness { get; set; } = 2;

        private bool _mShowNeighboursImages;

        internal bool ShowNeighboursImages
        {
            get => _mShowNeighboursImages;
            set
            {
                if (_mShowNeighboursImages == value) return;

                _mShowNeighboursImages = value;
                OnImageViewChange?.Invoke();
            }
        }

        internal ColumnOptions[] ColumnOptionsVertical;

        internal ColumnOptions[] ColumnOptionsHorizontal;

        internal int SortTypeDefault;

        internal bool IncreasingDefault;

        internal int SplitterDistanceVerticalMaximized;

        internal int SplitterDistanceVerticalNormal;

        internal int SplitterDistanceHorizontalMaximized;

        internal int SplitterDistanceHorizontalNormal;

        internal Size ThumbnailSizeMax = new Size(DefaultThumbnailWidthMax, DefaultThumbnailHeightMax);

        #endregion

        #region Constructors

        internal ResultsOptions(ResultsOptions options)
        {
            ColumnOptionsVertical = new ColumnOptions[(int)ResultsListView.ColumnsTypeVertical.Size];
            for (int i = 0; i < ColumnOptionsVertical.Length; i++)
                ColumnOptionsVertical[i] = options.ColumnOptionsVertical[i];
            ColumnOptionsHorizontal = new ColumnOptions[(int)ResultsListView.ColumnsTypeHorizontal.Size];
            for (int i = 0; i < ColumnOptionsHorizontal.Length; i++)
                ColumnOptionsHorizontal[i] = options.ColumnOptionsHorizontal[i];
            SortTypeDefault = options.SortTypeDefault;
            IncreasingDefault = options.IncreasingDefault;
            SplitterDistanceVerticalMaximized = options.SplitterDistanceVerticalMaximized;
            SplitterDistanceVerticalNormal = options.SplitterDistanceVerticalNormal;
            SplitterDistanceHorizontalMaximized = options.SplitterDistanceHorizontalMaximized;
            SplitterDistanceHorizontalNormal = options.SplitterDistanceHorizontalNormal;
            ThumbnailSizeMax = options.ThumbnailSizeMax;

            HighlightDifference = options.HighlightDifference;
            DifferenceThreshold = options.DifferenceThreshold;
            NotHighlightIfFragmentsMoreThan = options.NotHighlightIfFragmentsMoreThan;
            NotHighlightMaxFragments = options.NotHighlightMaxFragments;
            HighlightAllDifferences = options.HighlightAllDifferences;
            MaxFragmentsForHighlight = options.MaxFragmentsForHighlight;
            AmountOfFragmentsOnX = options.AmountOfFragmentsOnX;
            AmountOfFragmentsOnY = options.AmountOfFragmentsOnY;
            NormalizedSizeOfImage = options.NormalizedSizeOfImage;
            PenThickness = options.PenThickness;
        }

        internal ResultsOptions()
        {
            ColumnOptionsVertical = new ColumnOptions[(int)ResultsListView.ColumnsTypeVertical.Size];
            ColumnOptionsHorizontal = new ColumnOptions[(int)ResultsListView.ColumnsTypeHorizontal.Size];
            SetDefault();
        }

        #endregion

        #region Methods

        internal ResultsOptions Clone() => new ResultsOptions(this);

        internal void CopyTo(ref ResultsOptions options)
        {
            for (int i = 0; i < ColumnOptionsVertical.Length; i++)
                options.ColumnOptionsVertical[i] = ColumnOptionsVertical[i];
            for (int i = 0; i < ColumnOptionsHorizontal.Length; i++)
                options.ColumnOptionsHorizontal[i] = ColumnOptionsHorizontal[i];
            options.SortTypeDefault = SortTypeDefault;
            options.IncreasingDefault = IncreasingDefault;
            options.SplitterDistanceVerticalMaximized = SplitterDistanceVerticalMaximized;
            options.SplitterDistanceVerticalNormal = SplitterDistanceVerticalNormal;
            options.SplitterDistanceHorizontalMaximized = SplitterDistanceHorizontalMaximized;
            options.SplitterDistanceHorizontalNormal = SplitterDistanceHorizontalNormal;
            options.ThumbnailSizeMax = ThumbnailSizeMax;
        }

        internal bool Equals(ResultsOptions options)
        {
            for (int i = 0; i < ColumnOptionsVertical.Length; i++)
                if (!Equals(ColumnOptionsVertical[i], options.ColumnOptionsVertical[i]))
                    return false;
            for (int i = 0; i < ColumnOptionsHorizontal.Length; i++)
                if (!Equals(ColumnOptionsHorizontal[i], options.ColumnOptionsHorizontal[i]))
                    return false;

            if (SortTypeDefault != options.SortTypeDefault)
                return false;
            if (IncreasingDefault != options.IncreasingDefault)
                return false;
            if (SplitterDistanceVerticalMaximized != options.SplitterDistanceVerticalMaximized)
                return false;
            if (SplitterDistanceVerticalNormal != options.SplitterDistanceVerticalNormal)
                return false;
            if (SplitterDistanceHorizontalMaximized != options.SplitterDistanceHorizontalMaximized)
                return false;
            if (SplitterDistanceHorizontalNormal != options.SplitterDistanceHorizontalNormal)
                return false;

            return ThumbnailSizeMax == options.ThumbnailSizeMax;
        }

        private void SetDefault()
        {
            SortTypeDefault = (int)CoreDll.SortType.ByDifference;
            IncreasingDefault = true;

            SplitterDistanceVerticalMaximized = MainSplitContainer.VIEW_MIN_WIDTH;
            SplitterDistanceVerticalNormal = MainSplitContainer.VIEW_MIN_WIDTH;
            SplitterDistanceHorizontalMaximized = MainSplitContainer.VIEW_MIN_HEIGHT;
            SplitterDistanceHorizontalNormal = MainSplitContainer.VIEW_MIN_HEIGHT;

            ThumbnailSizeMax = new Size(DefaultThumbnailWidthMax, DefaultThumbnailHeightMax);

            _mViewMode = ViewMode.VerticalPairTable;

            SetDefaultVerticalColumns();
            SetDefaultHorizontalColumns();
        }

        internal void Check()
        {
            if (ColumnOptionsVertical.Length >= (int)ResultsListView.ColumnsTypeVertical.Size
             && ColumnOptionsHorizontal.Length >= (int)ResultsListView.ColumnsTypeHorizontal.Size) return;

            ResultsOptions options = new ResultsOptions();
            if (ColumnOptionsVertical.Length < options.ColumnOptionsVertical.Length)
            {
                for (int i = 0; i < ColumnOptionsVertical.Length; ++i)
                {
                    options.ColumnOptionsVertical[i].Visible = ColumnOptionsVertical[i].Visible;
                    options.ColumnOptionsVertical[i].Width = ColumnOptionsVertical[i].Width;
                    options.ColumnOptionsVertical[i].Order = ColumnOptionsVertical[i].Order;
                }

                ColumnOptionsVertical = options.ColumnOptionsVertical;
            }

            if (ColumnOptionsHorizontal.Length >= options.ColumnOptionsHorizontal.Length) return;

            for (int i = 0; i < ColumnOptionsHorizontal.Length; ++i)
            {
                options.ColumnOptionsHorizontal[i].Visible = ColumnOptionsHorizontal[i].Visible;
                options.ColumnOptionsHorizontal[i].Width = ColumnOptionsHorizontal[i].Width;
                options.ColumnOptionsHorizontal[i].Order = ColumnOptionsHorizontal[i].Order;
            }

            ColumnOptionsHorizontal = options.ColumnOptionsHorizontal;
        }

        private void SetDefaultVerticalColumns()
        {
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Type].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Type].Width = 35;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Type].Order = 0;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Group].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Group].Width = 40;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Group].Order = 1;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Difference].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Difference].Width = 40;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Difference].Order = 2;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Defect].Visible = false;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Defect].Width = 25;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Defect].Order = 3;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Transform].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Transform].Width = 35;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Transform].Order = 4;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Hint].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Hint].Width = 30;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Hint].Order = 5;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileName].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileName].Width = 100;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileName].Order = 6;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileDirectory].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileDirectory].Width = 230;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileDirectory].Order = 7;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.ImageSize].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.ImageSize].Width = 70;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.ImageSize].Order = 8;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.ImageType].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.ImageType].Width = 40;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.ImageType].Order = 9;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Blockiness].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Blockiness].Width = 55;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Blockiness].Order = 10;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Blurring].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Blurring].Width = 55;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.Blurring].Order = 11;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileSize].Visible = true;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileSize].Width = 55;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileSize].Order = 12;

            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileTime].Visible = false;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileTime].Width = 115;
            ColumnOptionsVertical[(int)ResultsListView.ColumnsTypeVertical.FileTime].Order = 13;
        }

        private void SetDefaultHorizontalColumns()
        {
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Type].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Type].Width = 35;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Type].Order = 0;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Group].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Group].Width = 40;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Group].Order = 1;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Difference].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Difference].Width = 40;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Difference].Order = 2;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Visible = false;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Width = 25;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Defect].Order = 3;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Width = 35;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Transform].Order = 4;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Width = 30;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.Hint].Order = 5;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileDirectory].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileDirectory].Width = 230;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileDirectory].Order = 7;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageSize].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageSize].Width = 70;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageSize].Order = 8;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageType].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageType].Width = 40;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstImageType].Order = 9;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Width = 55;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlockiness].Order = 10;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Width = 55;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstBlurring].Order = 11;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Width = 55;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileSize].Order = 12;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileTime].Visible = false;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileTime].Width = 115;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.FirstFileTime].Order = 13;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileDirectory].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileDirectory].Width = 230;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileDirectory].Order = 14;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageSize].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageSize].Width = 70;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageSize].Order = 15;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageType].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageType].Width = 40;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondImageType].Order = 16;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlockiness].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlockiness].Width = 55;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlockiness].Order = 17;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlurring].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlurring].Width = 55;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondBlurring].Order = 18;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Visible = true;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Width = 55;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileSize].Order = 19;

            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileTime].Visible = false;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileTime].Width = 115;
            ColumnOptionsHorizontal[(int)ResultsListView.ColumnsTypeHorizontal.SecondFileTime].Order = 20;
        }

        #endregion
    }
}
