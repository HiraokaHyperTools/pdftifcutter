using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pdftifcutter.tests
{
    internal static class SamplesHelper
    {
        public static string Resolve(string file) => Path.Combine(
            TestContext.CurrentContext.WorkDirectory, "..", "..", "..", "Samples", file
        );
    }
}
