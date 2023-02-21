using NUnit.Framework;
using pdftifcutter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdftifcutter.tests
{
    public class SpecParserTest
    {
        [Test]
        public void Some()
        {
            {
                var it = new SpecParser(new string[] { "(", ")" });
                Assert.False(it.Valid);
            }
            {
                var it = new SpecParser(new string[] { "(", });
                Assert.False(it.Valid);
            }
            {
                var it = new SpecParser(new string[] { });
                Assert.False(it.Valid);
            }

            {
                var it = new SpecParser(new string[] { "(", "out.pdf", ")" });
                Assert.True(it.Valid);
                Assert.That(it.Specs.Count, Is.EqualTo(1));
                Assert.That(it.Specs[0].OutputPath, Is.EqualTo("out.pdf"));
                Assert.That(it.Specs[0].Selectors.Count, Is.EqualTo(0));
            }

            {
                var it = new SpecParser(new string[] { "(", "out.pdf", "1", "2", "3", ")" });
                Assert.True(it.Valid);
                Assert.That(it.Specs.Count, Is.EqualTo(1));
                Assert.That(it.Specs[0].OutputPath, Is.EqualTo("out.pdf"));
                Assert.That(it.Specs[0].Selectors.Count, Is.EqualTo(3));
                Assert.That(it.Specs[0].Selectors[0], Is.EqualTo("1"));
                Assert.That(it.Specs[0].Selectors[1], Is.EqualTo("2"));
                Assert.That(it.Specs[0].Selectors[2], Is.EqualTo("3"));
            }

            {
                var it = new SpecParser(new string[] { "(", "a.pdf", "1-2", ")", "(", "b.pdf", "3-4", ")", });
                Assert.True(it.Valid);
                Assert.That(it.Specs.Count, Is.EqualTo(2));
                Assert.That(it.Specs[0].OutputPath, Is.EqualTo("a.pdf"));
                Assert.That(it.Specs[0].Selectors.Count, Is.EqualTo(1));
                Assert.That(it.Specs[0].Selectors[0], Is.EqualTo("1-2"));
                Assert.That(it.Specs[1].OutputPath, Is.EqualTo("b.pdf"));
                Assert.That(it.Specs[1].Selectors.Count, Is.EqualTo(1));
                Assert.That(it.Specs[1].Selectors[0], Is.EqualTo("3-4"));
            }

        }
    }
}
