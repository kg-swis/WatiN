using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using WatiN.Core.Native.InternetExplorer;

namespace WatiN.Core.Native.Selenium
{
    public class SeleniumElementCollection : INativeElementCollection
    {
        private readonly List<IWebElement> _elementList;
        public SeleniumElementCollection(List<IWebElement> elementList)
        {
            _elementList = elementList;
        }

        public IEnumerable<INativeElement> GetElements()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INativeElement> GetElementsByTag(string tagName)
        {
            var seleniumElementList = _elementList.FindAll(n => n.TagName == tagName).Select(n => new SeleniumElement(n));
            return seleniumElementList;
        }

        public IEnumerable<INativeElement> GetElementsById(string id)
        {
            throw new NotImplementedException();
        }
    }
}