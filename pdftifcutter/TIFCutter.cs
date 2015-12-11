using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdftifcutter {
    class TIFCutter : Cutter {
        FIMULTIBITMAP tif;

        public TIFCutter(String fpin) {
            tif = FreeImage.OpenMultiBitmapEx(fpin, false, true, true);
        }

        public override IWr New(string fpout) {
            return new TIFW(fpout, tif);
        }

        public override int NPages {
            get { return FreeImage.GetPageCount(tif); }
        }
    }

    class TIFW : IWr {
        FIMULTIBITMAP src, dst;

        public TIFW(string fpout, FIMULTIBITMAP tif) {
            this.src = tif;
            this.dst = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, fpout, true, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);
        }

        public void Add(int pi) {
            var dib = FreeImage.LockPage(src, pi - 1);
            try {
                FreeImage.AppendPage(dst, dib);
            }
            finally {
                FreeImage.UnlockPage(src, dib, false);
            }
        }

        public void Dispose() {
            FreeImage.CloseMultiBitmapEx(ref dst, FREE_IMAGE_SAVE_FLAGS.TIFF_LZW | FREE_IMAGE_SAVE_FLAGS.TIFF_CCITTFAX3);
        }
    }
}
