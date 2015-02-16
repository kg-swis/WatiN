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
using WatiN.Core.Native;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using WatiN.Core.Native.Selenium;

namespace WatiN.Core
{

	public class Selenium : Browser
	{
        private readonly SeleniumBrowser _browser;
        private string _url;

	    public Selenium()
	    {
            _browser = new SeleniumBrowser();
	    }

        public Selenium(string url)
        {
            _url = url;
            _browser = new SeleniumBrowser();
            _browser.NavigateTo(new Uri(url));
        }

	    public override void WaitForComplete(int waitForCompleteTimeOut)
	    {
	        //WaitForComplete(new SeleniumWaitForComplete(_browser, waitForCompleteTimeOut));
            _browser.Browser.Manage().Timeouts().ImplicitlyWait(new TimeSpan(waitForCompleteTimeOut));
	    }

	    public override INativeBrowser NativeBrowser
	    {
	        get { return _browser; }
	    }

	    public override void Close()
	    {
	        _browser.Browser.Close();
	    }

        public void ForceClose()
        {
            _browser.Browser.Quit();
        }

	    protected override void Dispose(bool disposing)
	    {
	        ForceClose();
	        base.Dispose(disposing);
	    }
	}
}
