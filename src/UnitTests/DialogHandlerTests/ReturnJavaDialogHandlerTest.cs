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
using System.Threading;
using NUnit.Framework;
using SHDocVw;
using WatiN.Core.Constraints;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using WatiN.Core.Native.Windows;
using WatiN.Core.UnitTests.TestUtils;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.UnitTests.DialogHandlerTests
{
    [TestFixture]
    public class ReturnJavaDialogHandlerTest : BaseWatiNTest
    {
        [Test]
        public void WhenOnBeforeUnloadReturnJavaDialogIsShown_ClickingOnOkShouldCloseIE()
        {
            using (var ie = new IE(OnBeforeUnloadJavaDialogURI))
            {
                var returnDialogHandler = new ReturnDialogHandler();
                ie.AddDialogHandler(returnDialogHandler);

                var hWnd = ie.hWnd;
                // can't use ie.Close() here cause this will cleanup the registered
                // returnDialogHandler which leads to a timeout on the WaitUntilExists
                var internetExplorer = (IWebBrowser2)ie.InternetExplorer;
                internetExplorer.Quit();

                returnDialogHandler.WaitUntilExists();
                returnDialogHandler.OKButton.Click();

                Thread.Sleep(2000);
                Assert.IsFalse(Browser.Exists<IE>(new AttributeConstraint("hwnd", hWnd.ToString())));
            }
        }

        [Test]
        public void WhenOnBeforeUnloadReturnJavaDialogIsShown_ClickingOnCancelShouldKeepIEOpen()
        {
            using (var ie = new IE(OnBeforeUnloadJavaDialogURI))
            {
                var returnDialogHandler = new ReturnDialogHandler();
                ie.AddDialogHandler(returnDialogHandler);

                var hWnd = ie.hWnd;

                // can't use ie.Close() here cause this will cleanup the registered
                // returnDialogHandler which leads to a timeout on the WaitUntilExists
                var internetExplorer = (IWebBrowser2)ie.InternetExplorer;
                internetExplorer.Quit();

                returnDialogHandler.WaitUntilExists();
                returnDialogHandler.CancelButton.Click();

                Thread.Sleep(2000);
                Assert.IsTrue(Browser.Exists<IE>(new AttributeConstraint("hwnd", hWnd.ToString())));

                // finally close the ie instance
                internetExplorer.Quit();
                returnDialogHandler.WaitUntilExists();
                returnDialogHandler.OKButton.Click();
            }
        }

        [Test]
        public void WhenOnBeforeUnloadReturnJavaDialogIsShown_ClickingOnCancelShouldKeepIE9Open()
        {
            using (var ie = new IE(OnBeforeUnloadJavaDialogURI))
            {
                var returnDialogHandler = new ReturnDialogHandlerIE9();
                ie.AddDialogHandler(returnDialogHandler);

                var hWnd = ie.hWnd;

                // can't use ie.Close() here cause this will cleanup the registered
                // returnDialogHandler which leads to a timeout on the WaitUntilExists
                var internetExplorer = (IWebBrowser2)ie.InternetExplorer;
                internetExplorer.Quit();

                returnDialogHandler.WaitUntilExists();
                returnDialogHandler.CancelButton.Click();

                Thread.Sleep(2000);
                Assert.IsTrue(Browser.Exists<IE>(new AttributeConstraint("hwnd", hWnd.ToString())));

                // finally close the ie instance
                internetExplorer.Quit();
                returnDialogHandler.WaitUntilExists();
                returnDialogHandler.OKButton.Click();
            }
        }
    }

    public class ReturnDialogHandlerIE9 : BaseDialogHandler
    {
        private Window _window;

        public WinButton CancelButton
        {
            get
            {
                var hwnd = GetChildWindowHwnd(_window.Hwnd, "5000200E");
                return hwnd != IntPtr.Zero? new WinButton(hwnd) : null;
            }
        }

        public WinButton OKButton
        {
            get
            {
                var hwnd = GetChildWindowHwnd(_window.Hwnd, "5000200F");
                return hwnd != IntPtr.Zero ? new WinButton(hwnd) : null;
            }
        }

        public override bool HandleDialog(Window window)
        {
            if (CanHandleDialog(window))
            {
                _window = window;

                while (window.Exists())
                {
                    Thread.Sleep(200);
                }
                return true;
            }
            return false;
        }

        public override bool CanHandleDialog(Window window)
        {
            return (window.StyleInHex == "96C00284");
        }

        public void WaitUntilExists(int waitDurationInSeconds)
        {
            var tryActionUntilTimeOut = new TryFuncUntilTimeOut(TimeSpan.FromSeconds(waitDurationInSeconds));
            tryActionUntilTimeOut.Try(() => Exists());

            if (!Exists())
            {
                throw new WatiNException(string.Format("Dialog not available within {0} seconds.", waitDurationInSeconds));
            }
        }

        public bool Exists()
        {
            return _window != null && _window.Exists();
        }

        private IntPtr GetChildWindowHwnd(IntPtr parentHwnd, string styleInHex)
        {
            var hWnd = IntPtr.Zero;
            NativeMethods.EnumChildWindows(parentHwnd, (childHwnd, lParam) =>
            {
                var window = new Window(childHwnd);
                Console.WriteLine("childhwnd: " + childHwnd);
                Console.WriteLine("childhwnd.styleinhex: " + window.StyleInHex);
                if (window.StyleInHex == styleInHex)
                {
                    hWnd = childHwnd;
                    return false;
                }

                return true;
            }, IntPtr.Zero);

            return hWnd;
        }

        public void WaitUntilExists()
        {
            WaitUntilExists(30);
        }
    }
}