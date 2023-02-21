using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdftifcutter.Helpers
{
    public class PDFCutter : ICutter
    {
        private readonly PdfReader _reader;

        public PDFCutter(string fpin)
        {
            _reader = new PdfReader(fpin);
            _reader.ConsolidateNamedDestinations();
        }

        public IIndexedWriter New(string fpout)
        {
            return new PDFWriter(fpout, _reader);
        }

        public int NPages
        {
            get { return _reader.NumberOfPages; }
        }

        private class PDFWriter : IIndexedWriter
        {
            private readonly PdfReader _reader;
            private readonly FileStream _fs;
            private Document _document;
            private PdfCopy _copy;

            public PDFWriter(String fpout, PdfReader reader)
            {
                _reader = reader;
                _fs = File.Create(fpout);
            }

            public void Add(int pi)
            {
                if (_document == null)
                {
                    _document = new Document(_reader.GetPageSizeWithRotation(pi));
                    _copy = new PdfCopy(_document, _fs);
                    _document.Open();
                }

                PdfImportedPage page = _copy.GetImportedPage(_reader, pi);
                _copy.AddPage(page);
            }

            public void Dispose()
            {
                _copy.Close();
                _document.Close();
                _fs.Close();
            }
        }
    }
}
