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

using AntiDupl.NET.Core.Enums;
using AntiDupl.NET.Core.Original;

namespace AntiDupl.NET.Core
{
    public class CoreCompareOptions
    {
        public bool checkOnEquality;
        public bool transformedImage;
        public bool sizeControl;
        public bool typeControl;
        public bool ratioControl;
        public int thresholdDifference;
        public int minimalImageSize;
        public int maximalImageSize;
        public bool compareInsideOneFolder;
        public bool compareInsideOneSearchPath;
        public AlgorithmComparing algorithmComparing;

        public CoreCompareOptions()
        {
        }

        public CoreCompareOptions(CoreCompareOptions compareOptions)
        {
            checkOnEquality = compareOptions.checkOnEquality;
            transformedImage = compareOptions.transformedImage;
            sizeControl = compareOptions.sizeControl;
            typeControl = compareOptions.typeControl;
            ratioControl = compareOptions.ratioControl;
            algorithmComparing = compareOptions.algorithmComparing;
            thresholdDifference = compareOptions.thresholdDifference;
            minimalImageSize = compareOptions.minimalImageSize;
            maximalImageSize = compareOptions.maximalImageSize;
            compareInsideOneFolder = compareOptions.compareInsideOneFolder;
            compareInsideOneSearchPath = compareOptions.compareInsideOneSearchPath;
        }

        public CoreCompareOptions(ref AdCompareOptions compareOptions)
        {
            checkOnEquality = compareOptions.checkOnEquality != Constants.FALSE;
            transformedImage = compareOptions.transformedImage != Constants.FALSE;
            sizeControl = compareOptions.sizeControl != Constants.FALSE;
            typeControl = compareOptions.typeControl != Constants.FALSE;
            ratioControl = compareOptions.ratioControl != Constants.FALSE;
            algorithmComparing = compareOptions.algorithmComparing;
            thresholdDifference = compareOptions.thresholdDifference;
            minimalImageSize = compareOptions.minimalImageSize;
            maximalImageSize = compareOptions.maximalImageSize;
            compareInsideOneFolder = compareOptions.compareInsideOneFolder != Constants.FALSE;
            compareInsideOneSearchPath = compareOptions.compareInsideOneSearchPath != Constants.FALSE;
        }

        public void ConvertTo(ref AdCompareOptions compareOptions)
        {
            compareOptions.checkOnEquality = checkOnEquality ? Constants.TRUE : Constants.FALSE;
            compareOptions.transformedImage = transformedImage ? Constants.TRUE : Constants.FALSE;
            compareOptions.sizeControl = sizeControl ? Constants.TRUE : Constants.FALSE;
            compareOptions.typeControl = typeControl ? Constants.TRUE : Constants.FALSE;
            compareOptions.ratioControl = ratioControl ? Constants.TRUE : Constants.FALSE;
            compareOptions.algorithmComparing = algorithmComparing;
            compareOptions.thresholdDifference = thresholdDifference;
            compareOptions.minimalImageSize = minimalImageSize;
            compareOptions.maximalImageSize = maximalImageSize;
            compareOptions.compareInsideOneFolder = compareInsideOneFolder ? Constants.TRUE : Constants.FALSE;
            compareOptions.compareInsideOneSearchPath = compareInsideOneSearchPath ? Constants.TRUE : Constants.FALSE;
        }

        public CoreCompareOptions Clone()
        {
            return new CoreCompareOptions(this);
        }

        public bool Equals(CoreCompareOptions compareOptions)
        {
            return
                checkOnEquality == compareOptions.checkOnEquality &&
                transformedImage == compareOptions.transformedImage &&
                sizeControl == compareOptions.sizeControl &&
                typeControl == compareOptions.typeControl &&
                ratioControl == compareOptions.ratioControl &&
                algorithmComparing == compareOptions.algorithmComparing &&
                thresholdDifference == compareOptions.thresholdDifference &&
                minimalImageSize == compareOptions.minimalImageSize &&
                maximalImageSize == compareOptions.maximalImageSize &&
                compareInsideOneFolder == compareOptions.compareInsideOneFolder &&
                compareInsideOneSearchPath == compareOptions.compareInsideOneSearchPath;
        }
    }
}
