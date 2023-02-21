using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdftifcutter.Helpers
{
    public interface IIndexedWriter : IDisposable
    {
        /// <summary>
        /// Add the page
        /// </summary>
        /// <param name="pi">1 based</param>
        void Add(int pi);
    }
}
