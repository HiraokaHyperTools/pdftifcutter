using pdftifcutter.Helpers;
using pdftifcutter.Usecases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pdftifcutter
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 3)
            {
                HelpYa();
                return 1;
            }
            try
            {
                String inputPath = args[0];
                var specs = new SpecParser(args.Skip(1).ToArray());
                if (specs.Valid)
                {
                    ICutter cutter = new CutterFactory().Get(inputPath);
                    foreach (var spec in specs.Specs)
                    {
                        new CuttingUsecase().Cut(spec.OutputPath, cutter, spec);
                    }
                    return 0;
                }
                else
                {
                    HelpYa();
                    return 1;
                }
            }
            catch (Exception err)
            {
                Console.Error.WriteLine("" + err);
                return 2;
            }
        }

        private static void HelpYa()
        {
            Console.Error.WriteLine("pdftifcutter in.pdf ( out-even.pdf 2 4 6 ) ( out-odd.pdf 1 3 5 )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out-even.tif 2 4 6 ) ( out-odd.tif 1 3 5 )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out.tif 1- )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out.tif 2-3 )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out.tif 4- )");
        }
    }
}
