using NUnit.Framework;
using pdftifcutter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdftifcutter.tests
{
    public class CutterFactoryTest
    {
        [Test]
        public void TIF()
        {
            Assert.IsInstanceOf<TIFCutter>(new CutterFactory().Get(SamplesHelper.Resolve("123456.tif")));
            Assert.IsInstanceOf<TIFCutter>(new CutterFactory().Get(SamplesHelper.Resolve("123456.tiff")));
        }

        [Test]
        public void PDF()
        {
            Assert.IsInstanceOf<PDFCutter>(new CutterFactory().Get(SamplesHelper.Resolve("123456.pdf")));
        }
    }
}
