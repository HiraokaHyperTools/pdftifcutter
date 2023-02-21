using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace pdftifcutter.Helpers
{
    public class RangeParser
    {
        public int From { get; set; }
        public int To { get; set; }
        public bool Valid { get; set; }

        public RangeParser(string selector)
        {
            Match match;
            if (false) { }
            else if ((match = Regex.Match(selector, "^(?<a>\\d+)\\-(?<b>\\d+)$")).Success)
            {
                From = Convert.ToInt32(match.Groups["a"].Value);
                To = Convert.ToInt32(match.Groups["b"].Value);
            }
            else if ((match = Regex.Match(selector, "^(?<a>\\d+)\\-$")).Success)
            {
                From = Convert.ToInt32(match.Groups["a"].Value);
                To = int.MaxValue;
            }
            else if ((match = Regex.Match(selector, "^\\-(?<b>\\d+)$")).Success)
            {
                From = 1;
                To = Convert.ToInt32(match.Groups["b"].Value);
            }
            else if (int.TryParse(selector, out int pageNum))
            {
                From = To = pageNum;
            }
            else
            {
                return;
            }

            Valid = true;
        }
    }
}
