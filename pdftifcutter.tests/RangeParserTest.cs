using NUnit.Framework;
using pdftifcutter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdftifcutter.tests
{
    public class RangeParserTest
    {
        [Test]
        public void Some()
        {
            {
                var it = new RangeParser("123");
                Assert.True(it.Valid);
                Assert.That(it.From, Is.EqualTo(123));
                Assert.That(it.To, Is.EqualTo(123));
            }
            {
                var it = new RangeParser("12-");
                Assert.True(it.Valid);
                Assert.That(it.From, Is.EqualTo(12));
                Assert.That(it.To, Is.EqualTo(int.MaxValue));
            }
            {
                var it = new RangeParser("-34");
                Assert.True(it.Valid);
                Assert.That(it.From, Is.EqualTo(1));
                Assert.That(it.To, Is.EqualTo(34));
            }
            {
                var it = new RangeParser("22-33");
                Assert.True(it.Valid);
                Assert.That(it.From, Is.EqualTo(22));
                Assert.That(it.To, Is.EqualTo(33));
            }
            {
                var it = new RangeParser("");
                Assert.False(it.Valid);
            }
            {
                var it = new RangeParser("a");
                Assert.False(it.Valid);
            }
            {
                var it = new RangeParser("-");
                Assert.False(it.Valid);
            }
        }
    }
}
