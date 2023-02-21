using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdftifcutter.Helpers
{
    public interface ICutter
    {
        IIndexedWriter New(string fpout);
        
        int NPages { get; }
    }
}
