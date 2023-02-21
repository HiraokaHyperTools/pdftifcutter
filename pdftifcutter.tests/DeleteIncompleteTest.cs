using NUnit.Framework;
using pdftifcutter.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pdftifcutter.tests
{
    public class DeleteIncompleteTest
    {
        [Test]
        public void DoDelete()
        {
            var file = Path.GetTempFileName();
            using (var a = new DeleteIncomplete(file))
            {

            }
            FileAssert.DoesNotExist(file);
        }

        [Test]
        public void DoNotDelete()
        {
            var file = Path.GetTempFileName();
            using (var a = new DeleteIncomplete(file))
            {
                a.MarkAsKeep();
            }
            FileAssert.Exists(file);
        }
    }
}
