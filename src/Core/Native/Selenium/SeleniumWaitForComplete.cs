using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatiN.Core.Native.Selenium
{
    class SeleniumWaitForComplete : WaitForCompleteBase
    {
        private readonly SeleniumBrowser _nativeBrowser;

        public SeleniumWaitForComplete(SeleniumBrowser nativeBrowser, int waitForCompleteTimeOut) : base(waitForCompleteTimeOut)
        {
            _nativeBrowser = nativeBrowser;
        }

        protected override void InitialSleep()
        {
            // Seems like this is not needed
        }

        protected override void WaitForCompleteOrTimeout()
        {
            WaitWhileDocumentNotAvailable();
        }

        protected virtual void WaitWhileDocumentNotAvailable()
        {
            WaitUntil(() => !_nativeBrowser.IsLoading(),
                      () => "waiting for main document becoming available");

            //_nativeBrowser.ClientPort.InitializeDocument();
        }
    }
}
