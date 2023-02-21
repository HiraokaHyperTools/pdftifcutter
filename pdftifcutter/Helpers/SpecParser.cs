using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdftifcutter.Helpers
{
    public class SpecParser
    {
        public List<Spec> Specs { get; set; } = new List<Spec>();
        public bool Valid { get; set; }

        public SpecParser(string[] args)
        {
            var en = args.Cast<string>().GetEnumerator();
            var any = false;
            while (en.MoveNext())
            {
                if (en.Current != "(")
                {
                    return;
                }
                if (!en.MoveNext())
                {
                    return;
                }
                var set = new Spec
                {
                    OutputPath = en.Current,
                    Selectors = new List<string>(),
                };
                while (true)
                {
                    if (!en.MoveNext())
                    {
                        return;
                    }
                    if (en.Current == ")")
                    {
                        break;
                    }
                    else
                    {
                        set.Selectors.Add(en.Current);
                    }
                }

                Specs.Add(set);
                any = true;
            }
            Valid = any;
        }
    }
}
