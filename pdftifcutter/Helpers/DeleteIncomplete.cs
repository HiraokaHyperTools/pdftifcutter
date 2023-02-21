using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pdftifcutter.Helpers
{
    internal class DeleteIncomplete : IDisposable
    {
        private string _outputPath;
        private bool _keep;

        public DeleteIncomplete(string outputPath)
        {
            _outputPath = outputPath;
        }

        public void Dispose()
        {
            if (_keep)
            {
                return;
            }

            try
            {
                if (File.Exists(_outputPath))
                {
                    File.Delete(_outputPath);
                }
            }
            catch
            {
                Console.Error.WriteLine("Failed to delete incomplete file: " + _outputPath);
            }
        }

        internal void MarkAsKeep()
        {
            _keep = true;
        }
    }
}
