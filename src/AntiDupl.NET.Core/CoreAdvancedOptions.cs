﻿/*
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

using AntiDupl.NET.Core.Original;

namespace AntiDupl.NET.Core
{
    public class CoreAdvancedOptions
    {
        public bool deleteToRecycleBin;
        public bool mistakeDataBase;
        public int ratioResolution;
        public int compareThreadCount;
        public int collectThreadCount;
        public int reducedImageSize;
        public int undoQueueSize;
        public int resultCountMax;
        public int ignoreFrameWidth;
        public bool useLibJpegTurbo;

        public CoreAdvancedOptions()
        {
        }

        public CoreAdvancedOptions(CoreAdvancedOptions advancedOptions)
        {
            deleteToRecycleBin = advancedOptions.deleteToRecycleBin;
            mistakeDataBase = advancedOptions.mistakeDataBase;
            ratioResolution = advancedOptions.ratioResolution;
            compareThreadCount = advancedOptions.compareThreadCount;
            collectThreadCount = advancedOptions.collectThreadCount;
            reducedImageSize = advancedOptions.reducedImageSize;
            undoQueueSize = advancedOptions.undoQueueSize;
            resultCountMax = advancedOptions.resultCountMax;
            ignoreFrameWidth = advancedOptions.ignoreFrameWidth;
            useLibJpegTurbo = advancedOptions.useLibJpegTurbo;
        }

        public CoreAdvancedOptions(ref AdAdvancedOptions advancedOptions)
        {
            deleteToRecycleBin = advancedOptions.deleteToRecycleBin != Constants.FALSE;
            mistakeDataBase = advancedOptions.mistakeDataBase != Constants.FALSE;
            ratioResolution = advancedOptions.ratioResolution;
            compareThreadCount = advancedOptions.compareThreadCount;
            collectThreadCount = advancedOptions.collectThreadCount;
            reducedImageSize = advancedOptions.reducedImageSize;
            undoQueueSize = advancedOptions.undoQueueSize;
            resultCountMax = advancedOptions.resultCountMax;
            ignoreFrameWidth = advancedOptions.ignoreFrameWidth;
            useLibJpegTurbo = advancedOptions.useLibJpegTurbo != Constants.FALSE;
        }

        public void ConvertTo(ref AdAdvancedOptions advancedOptions)
        {
            advancedOptions.deleteToRecycleBin = deleteToRecycleBin ? Constants.TRUE : Constants.FALSE;
            advancedOptions.mistakeDataBase = mistakeDataBase ? Constants.TRUE : Constants.FALSE;
            advancedOptions.ratioResolution = ratioResolution;
            advancedOptions.compareThreadCount = compareThreadCount;
            advancedOptions.collectThreadCount = collectThreadCount;
            advancedOptions.reducedImageSize = reducedImageSize;
            advancedOptions.undoQueueSize = undoQueueSize;
            advancedOptions.resultCountMax = resultCountMax;
            advancedOptions.ignoreFrameWidth = ignoreFrameWidth;
            advancedOptions.useLibJpegTurbo = useLibJpegTurbo ? Constants.TRUE : Constants.FALSE;
        }

        public CoreAdvancedOptions Clone()
        {
            return new CoreAdvancedOptions(this);
        }

        public bool Equals(CoreAdvancedOptions advancedOptions)
        {
            return
                deleteToRecycleBin == advancedOptions.deleteToRecycleBin &&
                mistakeDataBase == advancedOptions.mistakeDataBase &&
                ratioResolution == advancedOptions.ratioResolution &&
                compareThreadCount == advancedOptions.compareThreadCount &&
                collectThreadCount == advancedOptions.collectThreadCount &&
                reducedImageSize == advancedOptions.reducedImageSize &&
                undoQueueSize == advancedOptions.undoQueueSize &&
                resultCountMax == advancedOptions.resultCountMax &&
                ignoreFrameWidth == advancedOptions.ignoreFrameWidth &&
                useLibJpegTurbo == advancedOptions.useLibJpegTurbo;
        }
    }
}
