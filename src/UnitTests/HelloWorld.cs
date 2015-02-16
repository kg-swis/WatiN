using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WatiN.Core.UnitTests
{
    class HelloWorld
    {
        [Test]
        public void SearchForWatiNOnGoogle()
        {
            using (var browser = new Selenium("http://www.google.com"))
            {
                browser.TextField(Find.ByName("q")).TypeText("WatiN");
                browser.Button(Find.ByName("btnG")).Click();

                Assert.IsTrue(browser.ContainsText("WatiN"));
            }
        }
    }
}
