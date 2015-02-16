using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium;

namespace WatiN.Core.Native.Selenium
{
    public class SeleniumDocument : INativeDocument
    {
        private readonly SeleniumBrowser _browser;
        private readonly SeleniumElement containingFrameElement;

        public SeleniumDocument(SeleniumBrowser browser) :
            this(browser, null)
        {

        }
        public SeleniumDocument(SeleniumBrowser browser, SeleniumElement containingFrameElement)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            _browser = browser;
            this.containingFrameElement = containingFrameElement;
        }

        public INativeElementCollection AllElements
        {
            get { return new SeleniumElementCollection(_browser.Browser.FindElements(By.XPath("//*")).ToList()); }
        }

        public INativeElement ContainingFrameElement
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElement Body
        {
            get { return new SeleniumElement(_browser.Browser.FindElement(By.XPath("//body"))); }
        }

        public string Url
        {
            get { return _browser.Browser.Url; }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElement ActiveElement
        {
            get { throw new NotImplementedException(); }
        }

        public string JavaScriptVariableName
        {
            get { throw new NotImplementedException(); }
        }

        public IList<INativeDocument> Frames
        {
            get { throw new NotImplementedException(); }
        }

        public void RunScript(string scriptCode, string language)
        {
            throw new NotImplementedException();
        }

        public string GetPropertyValue(string propertyName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rectangle> GetTextBounds(string text)
        {
            throw new NotImplementedException();
        }

        public bool ContainsText(string text)
        {
            var innertext = ((SeleniumElement)_browser.NativeDocument.Body).InnerText;

            if (innertext == null) return false;

            return innertext.IndexOf(text) >= 0;
        }
    }
}