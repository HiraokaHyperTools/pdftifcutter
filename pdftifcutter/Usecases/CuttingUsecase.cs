using pdftifcutter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdftifcutter.Usecases
{
    internal class CuttingUsecase
    {
        public void Cut(string outputPath, ICutter cutter, Spec spec)
        {
            using (var deleteIncomplete = new DeleteIncomplete(outputPath))
            using (var writer = cutter.New(outputPath))
            {
                var any = false;
                foreach (var selector in spec.Selectors)
                {
                    var range = new RangeParser(selector);
                    if (range.Valid)
                    {
                        for (int z = Math.Max(1, range.From); z <= Math.Min(range.To, cutter.NPages); z++)
                        {
                            writer.Add(z);
                            any = true;
                        }
                    }
                }
                if (any)
                {
                    deleteIncomplete.MarkAsKeep();
                }
            }
        }
    }
}
