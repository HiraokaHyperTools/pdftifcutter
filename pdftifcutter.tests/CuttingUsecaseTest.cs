using NUnit.Framework;
using pdftifcutter.Helpers;
using pdftifcutter.Usecases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace pdftifcutter.tests
{
    internal class CuttingUsecaseTest
    {
        [Test]
        [TestCase(@"%SAMPLES%\\123456.pdf ( out-even.pdf 2 4 6 ) ( out-odd.pdf 1 3 5 )")]
        [TestCase(@"%SAMPLES%\\123456.tif ( out-even.tif 2 4 6 ) ( out-odd.tif 1 3 5 )")]
        [TestCase(@"%SAMPLES%\\123456.tif ( out1.tif 1- )")]
        [TestCase(@"%SAMPLES%\\123456.tif ( out23.tif 2-3 )")]
        [TestCase(@"%SAMPLES%\\123456.tif ( out456.tif 4- )")]
        public void Usages(string args)
        {
            Run(args);
        }

        [TestCase(@"%SAMPLES%\\notfound.tif ( notout.tif 4- )", "notout.tif")]
        public void OutOfOrderTIF(string args, string outFile)
        {
            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            Assert.Throws<FileNotFoundException>(() => Run(args));
            FileAssert.DoesNotExist(outFile);
        }

        [TestCase(@"%SAMPLES%\\notfound.pdf ( notout.pdf 4- )", "notout.pdf")]
        public void OutOfOrderPDF(string args, string outFile)
        {
            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            Assert.Throws<IOException>(() => Run(args));
            FileAssert.DoesNotExist(outFile);
        }

        private void Run(string args)
        {
            Environment.SetEnvironmentVariable("SAMPLES", SamplesHelper.Resolve("."));
            Run(
                Regex.Split(args.Trim(), "\\s+")
                    .Select(Environment.ExpandEnvironmentVariables)
                    .ToArray()
            );
        }

        private void Run(string[] args)
        {
            String inputPath = args[0];
            var specs = new SpecParser(args.Skip(1).ToArray());
            if (specs.Valid)
            {
                ICutter cutter = new CutterFactory().Get(inputPath);
                foreach (var spec in specs.Specs)
                {
                    if (File.Exists(spec.OutputPath))
                    {
                        File.Delete(spec.OutputPath);
                    }

                    new CuttingUsecase().Cut(spec.OutputPath, cutter, spec);

                    FileAssert.Exists(spec.OutputPath);
                }
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
