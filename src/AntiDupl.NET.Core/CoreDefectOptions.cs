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

using AntiDupl.NET.Core.Original;

namespace AntiDupl.NET.Core
{
    public class CoreDefectOptions
    {
        public bool checkOnDefect;
        public bool checkOnBlockiness;
        public int blockinessThreshold;
        public bool checkOnBlockinessOnlyNotJpeg;
        public bool checkOnBlurring;
        public int blurringThreshold;

        public CoreDefectOptions()
        {
        }

        public CoreDefectOptions(CoreDefectOptions defectOptions)
        {
            checkOnDefect = defectOptions.checkOnDefect;
            checkOnBlockiness = defectOptions.checkOnBlockiness;
            blockinessThreshold = defectOptions.blockinessThreshold;
            checkOnBlockinessOnlyNotJpeg = defectOptions.checkOnBlockinessOnlyNotJpeg;
            checkOnBlurring = defectOptions.checkOnBlurring;
            blurringThreshold = defectOptions.blurringThreshold;
        }

        public CoreDefectOptions(ref AdDefectOptions defectOptions)
        {
            checkOnDefect = defectOptions.checkOnDefect != Constants.FALSE;
            checkOnBlockiness = defectOptions.checkOnBlockiness != Constants.FALSE;
            blockinessThreshold = defectOptions.blockinessThreshold;
            checkOnBlockinessOnlyNotJpeg = defectOptions.checkOnBlockinessOnlyNotJpeg != Constants.FALSE;
            checkOnBlurring = defectOptions.checkOnBlurring != Constants.FALSE;
            blurringThreshold = defectOptions.blurringThreshold;
        }

        public void ConvertTo(ref AdDefectOptions defectOptions)
        {
            defectOptions.checkOnDefect = checkOnDefect ? Constants.TRUE : Constants.FALSE;
            defectOptions.checkOnBlockiness = checkOnBlockiness ? Constants.TRUE : Constants.FALSE;
            defectOptions.blockinessThreshold = blockinessThreshold;
            defectOptions.checkOnBlockinessOnlyNotJpeg = checkOnBlockinessOnlyNotJpeg ? Constants.TRUE : Constants.FALSE;
            defectOptions.checkOnBlurring = checkOnBlurring ? Constants.TRUE : Constants.FALSE;
            defectOptions.blurringThreshold = blurringThreshold;
        }

        public CoreDefectOptions Clone()
        {
            return new CoreDefectOptions(this);
        }

        public bool Equals(CoreDefectOptions defectOptions)
        {
            return
                checkOnDefect == defectOptions.checkOnDefect &&
                checkOnBlockiness == defectOptions.checkOnBlockiness &&
                blockinessThreshold == defectOptions.blockinessThreshold &&
                 checkOnBlockinessOnlyNotJpeg == defectOptions.checkOnBlockinessOnlyNotJpeg &&
                checkOnBlurring == defectOptions.checkOnBlurring &&
                blurringThreshold == defectOptions.blurringThreshold;
        }
    }
}
