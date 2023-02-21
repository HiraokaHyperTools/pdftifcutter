using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pdftifcutter.Helpers
{
    public class CutterFactory
    {
        public class Entry
        {
            public string Extension { get; set; }
            public Func<string, ICutter> Factory { get; set; }
        }

        public List<Entry> Entries { get; set; } = new List<Entry>();

        public CutterFactory()
        {
            Entries.Add(new Entry { Extension = ".tif", Factory = it => new TIFCutter(it), });
            Entries.Add(new Entry { Extension = ".tiff", Factory = it => new TIFCutter(it), });
            Entries.Add(new Entry { Extension = ".pdf", Factory = it => new PDFCutter(it), });
        }

        public ICutter Get(string inputPath)
        {
            var inputExtension = Path.GetExtension(inputPath);

            var hit = Entries
                .FirstOrDefault(it => StringComparer.InvariantCultureIgnoreCase.Compare(it.Extension, inputExtension) == 0);

            if (hit != null)
            {
                return hit.Factory(inputPath);
            }
            else
            {
                throw new NotSupportedException(inputExtension);
            }
        }
    }
}
