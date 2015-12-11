using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pdftifcutter {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 3) {
                helpYa(); Environment.ExitCode = 1;
                return;
            }
            try {
                String fpin = args[0];
                Cutter cutter = Cutter.Get(fpin);
                int x = 1, cx = args.Length;
                while (x < cx) {
                    if (args[x] != "(") throw new ArgumentException();
                    x++;
                    String fpout = args[x];
                    using (var wr = cutter.New(fpout)) {
                        x++;
                        while (args[x] != ")") {
                            int pi;
                            int z0, z1;
                            Match M;
                            if (false) { }
                            else if ((M = Regex.Match(args[x], "^(?<a>\\d+)\\-(?<b>\\d+)$")).Success) {
                                z0 = Convert.ToInt32(M.Groups["a"].Value);
                                z1 = Convert.ToInt32(M.Groups["b"].Value);
                            }
                            else if ((M = Regex.Match(args[x], "^(?<a>\\d+)\\-$")).Success) {
                                z0 = Convert.ToInt32(M.Groups["a"].Value);
                                z1 = int.MaxValue;
                            }
                            else if ((M = Regex.Match(args[x], "^\\-(?<b>\\d+)$")).Success) {
                                z0 = 1;
                                z1 = Convert.ToInt32(M.Groups["b"].Value);
                            }
                            else if (int.TryParse(args[x], out pi)) {
                                z0 = z1 = pi;
                            }
                            else {
                                x++;
                                continue;
                            }
                            for (int z = Math.Max(1, z0); z <= Math.Min(z1, cutter.NPages); z++) {
                                wr.Add(z);
                            }
                            x++;
                        }
                        x++; // ")"
                    }
                }
            }
            catch (Exception err) {
                Console.Error.WriteLine("" + err);
                Environment.ExitCode = 2;
            }
        }

        private static void helpYa() {
            Console.Error.WriteLine("pdftifcutter in.pdf ( out-even.pdf 2 4 6 ) ( out-odd.pdf 1 3 5 )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out-even.tif 2 4 6 ) ( out-odd.tif 1 3 5 )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out.tif 1- )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out.tif 2-3 )");
            Console.Error.WriteLine("pdftifcutter in.tif ( out.tif 4- )");
        }
    }

    abstract class Cutter {
        public static Cutter Get(string fpin) {
            if ("|.tif|.tiff|".IndexOf(Path.GetExtension(fpin), StringComparison.InvariantCultureIgnoreCase) >= 0) {
                return new TIFCutter(fpin);
            }
            if ("|.pdf|".IndexOf(Path.GetExtension(fpin), StringComparison.InvariantCultureIgnoreCase) >= 0) {
                return new PDFCutter(fpin);
            }
            throw new NotSupportedException(Path.GetExtension(fpin));
        }

        public abstract IWr New(string fpout);

        public abstract int NPages { get; }
    }

    interface IWr : IDisposable {
        /// <summary>
        /// Add the page
        /// </summary>
        /// <param name="pi">1 based</param>
        void Add(int pi);
    }
}
