using System;
using System.Collections.Specialized;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.Events;
using WatiN.Core.Actions;
using WatiN.Core.DialogHandlers;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.Selenium
{
    public class SeleniumElement : INativeElement
    {
        private const string CSSTEXT = "cssText";

        private readonly IWebElement _webElement;

        public SeleniumElement(IWebElement webElement)
        {
            _webElement = webElement;
        }

        public INativeElementCollection Children
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElementCollection AllDescendants
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElementCollection TableRows
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElementCollection TableBodies
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElementCollection TableCells
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElementCollection Options
        {
            get { throw new NotImplementedException(); }
        }

        public string TextAfter
        {
            get { throw new NotImplementedException(); }
        }

        public string TextBefore
        {
            get { throw new NotImplementedException(); }
        }

        public string InnerText
        {
            get { return GetAttributeValue("innerText"); }
        }

        public INativeElement NextSibling
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElement PreviousSibling
        {
            get { throw new NotImplementedException(); }
        }

        public INativeElement Parent
        {
            get
            {
                try
                {
                    return new SeleniumElement(_webElement.FindElement(By.XPath("parent::*")));
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
        }

        public string GetAttributeValue(string attributeName)
        {
            return _webElement.GetAttribute(attributeName);
        }

        public void SetAttributeValue(string attributeName, string value)
        {
            string script = "";
            
            if (attributeName == "value")
            {
                script = string.Format("arguments[0].value = '{0}';",
                                        value);
            }
            else
            {
                script = string.Format("arguments[0].setAttribute('{0}', '{1}');",
                                            attributeName,
                                            value);
            }

            ScriptExecuting(script, _webElement);
        }

        public string GetStyleAttributeValue(string attributeName)
        {
            if (string.IsNullOrEmpty(attributeName))
                throw new ArgumentNullException("attributeName");

            return _webElement.GetCssValue(attributeName);
        }

        public void SetStyleAttributeValue(string attributeName, string value)
        {
            throw new NotImplementedException();
        }

        public void ScriptExecuting(string script, params object[] args)
        {
            var jsevent = new EventFiringWebDriver(((IWrapsDriver)_webElement).WrappedDriver);
            jsevent.ExecuteScript(script, args);
        }

        public void SetFocus()
        {
            var script = "arguments[0].focus();";
            ScriptExecuting(script, _webElement);
        }

        public void FireEvent(string eventName, NameValueCollection eventProperties)
        {
            string script = string.Empty;
            switch (eventName)
            {
                case "onKeyUp":
                    _webElement.SendKeys(char.ConvertFromUtf32(Convert.ToInt32(eventProperties["charCode"])));
                    break;
               /* case "onKeyPress":
                    _webElement.SendKeys(char.ConvertFromUtf32(Convert.ToInt32(eventProperties["charCode"])));
                    break;
                case "onKeyDown":
                    _webElement.SendKeys(char.ConvertFromUtf32(Convert.ToInt32(eventProperties["charCode"])));
                    break;*/
                case "onclick" :
                    _webElement.Click();
                    break;
            }
        }


        public void FireEventNoWait(string eventName, NameValueCollection eventProperties)
        {
            throw new NotImplementedException();
        }

        public bool IsElementReferenceStillValid()
        {
            throw new NotImplementedException();
        }

        public string TagName
        {
            get { return _webElement.TagName; }
        }

        public void Select()
        {
            var script = "arguments[0].select();";
            ScriptExecuting(script, _webElement);
        }

        public void SubmitForm()
        {
            throw new NotImplementedException();
        }

        public void SetFileUploadFile(DialogWatcher dialogWatcher, string fileName)
        {
            throw new NotImplementedException();
        }

        public void WaitUntilReady()
        {
            //По моему эта функция не нужна throw new NotImplementedException();
        }

        public Rectangle GetElementBounds()
        {
            throw new NotImplementedException();
        }

        public string GetJavaScriptElementReference()
        {
            throw new NotImplementedException();
        }

        public void Pin()
        {
            // IGNORE
        }

        public ITypeTextAction CreateTypeTextAction(TextField textField)
        {
            return new TypeTextAction(textField);
        }

        public ISelectAction CreateSelectAction(Option option)
        {
            return new SelectAction(option);
        }
    }
}
