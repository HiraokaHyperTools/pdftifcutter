using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdftifcutter {
    class PDFCutter : Cutter {
        PdfReader reader;

        public PDFCutter(string fpin) {
            reader = new PdfReader(fpin);
            reader.ConsolidateNamedDestinations();
        }

        public override IWr New(string fpout) {
            return new PDFW(fpout, reader);
        }
    }

    class PDFW : IWr {
        PdfReader reader;
        Document document;
        PdfCopy copy;
        FileStream fs;

        public PDFW(String fpout, PdfReader reader) {
            this.reader = reader;
            this.fs = File.Create(fpout);
        }

        public void Add(int pi) {
            if (document == null) {
                document = new Document(reader.GetPageSizeWithRotation(pi));
                copy = new PdfCopy(document, fs);
                document.Open();
            }

            PdfImportedPage page = copy.GetImportedPage(reader, pi);
            copy.AddPage(page);
        }

        public void Dispose() {
            copy.Close();
            document.Close();
            fs.Close();
        }
    }
}
