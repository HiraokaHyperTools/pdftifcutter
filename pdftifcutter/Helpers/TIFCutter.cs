using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdftifcutter.Helpers
{
    public class TIFCutter : ICutter
    {
        private readonly FIMULTIBITMAP _tif;

        public TIFCutter(String fpin)
        {
            _tif = FreeImage.OpenMultiBitmapEx(fpin, false, true, true);
        }

        public IIndexedWriter New(string fpout)
        {
            return new TIFWriter(fpout, _tif);
        }

        public int NPages
        {
            get { return FreeImage.GetPageCount(_tif); }
        }

        private class TIFWriter : IIndexedWriter
        {
            private readonly FIMULTIBITMAP _src;
            private FIMULTIBITMAP _dst;

            public TIFWriter(string fpout, FIMULTIBITMAP tif)
            {
                _src = tif;
                _dst = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, fpout, true, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);
            }

            public void Add(int pi)
            {
                var dib = FreeImage.LockPage(_src, pi - 1);
                try
                {
                    FreeImage.AppendPage(_dst, dib);
                }
                finally
                {
                    FreeImage.UnlockPage(_src, dib, false);
                }
            }

            public void Dispose()
            {
                FreeImage.CloseMultiBitmapEx(ref _dst, FREE_IMAGE_SAVE_FLAGS.TIFF_LZW | FREE_IMAGE_SAVE_FLAGS.TIFF_CCITTFAX3);
            }
        }
    }
}
