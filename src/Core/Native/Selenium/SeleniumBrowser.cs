#region WatiN Copyright (C) 2006-2011 Jeroen van Menen

//Copyright 2006-2011 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System;
using System.Runtime.InteropServices;
using System.Threading;
using mshtml;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using SHDocVw;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.Selenium
{
	public class SeleniumBrowser : INativeBrowser
	{
        private  IWebDriver _browser;

	    public IWebDriver Browser
	    {
	        get { return _browser;  }
	    }

	    public SeleniumBrowser()
	    {
	        InitializeBrowser();
	    }

        private void InitializeBrowser()
        {
            if (_browser != null)
            {
                _browser.Close();
                _browser.Dispose();
            }

            _browser = new InternetExplorerDriver();
           // _browser.Manage().Window.Maximize();
        }

	    public void NavigateTo(Uri url)
	    {
	        _browser.Navigate().GoToUrl(url);
	    }

	    public void NavigateToNoWait(Uri url)
	    {
	        throw new NotImplementedException();
	    }

	    public bool GoBack()
	    {
	         _browser.Navigate().Back();
	        return true;
	    }

	    public bool GoForward()
	    {
	         _browser.Navigate().Forward();
	        return true;
	    }

	    public void Reopen()
	    {
	        throw new NotImplementedException();
	    }

	    public void Refresh()
	    {
	         _browser.Navigate().Refresh();
	    }

	    public IntPtr hWnd
	    {
	        get { return new IntPtr(int.Parse(_browser.WindowHandles[0])); }
	    }

	    public INativeDocument NativeDocument
	    {
	        get
	        {
                var timeout = TimeSpan.FromSeconds(Settings.WaitForCompleteTimeOut);
                var tryActionUntilTimeOut = new TryFuncUntilTimeOut(timeout)
                {
                    ExceptionMessage = () => string.Format("waiting {0} seconds for document to become available.", Settings.WaitUntilExistsTimeOut)
                };

                var doc = tryActionUntilTimeOut.Try(() => new SeleniumDocument(this));
                return doc; 
	        }
	    }

	    public void Close()
	    {
	        throw new NotImplementedException();
	    }

	    internal bool IsLoading()
        {
            return true;
        }
    }
}
