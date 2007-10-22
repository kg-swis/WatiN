#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
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

using System.IO;
using NUnit.Framework;
using WatiN.Core.DialogHandlers;

namespace WatiN.Core.UnitTests.DialogHandlerTests
{
	[TestFixture]
	public class FileDownloadHandlerTests
	{
		[Test, Ignore("Because of timeout issues, run this test manually and not automated"), Category("InternetConnectionNeeded")]
		public void DownloadOpen()
		{
			WatiN.Core.DialogHandlers.FileDownloadHandler dhdl = new WatiN.Core.DialogHandlers.FileDownloadHandler(WatiN.Core.DialogHandlers.FileDownloadOptionEnum.Open);

			IE ie = new IE();
			ie.AddDialogHandler(dhdl);
			ie.WaitForComplete();
			ie.GoTo("http://watin.sourceforge.net/WatiNRecorder.zip");

			dhdl.WaitUntilFileDownloadDialogIsHandled(5);
			dhdl.WaitUntilDownloadCompleted(20);
			ie.Close();
		}

		[Test, Ignore("Because of timeout issues, run this test manually and not automated"), Category("InternetConnectionNeeded")]
		public void DownloadSave()
		{
			FileInfo file = new FileInfo(@"c:\temp\test.zip");
			file.Directory.Create();
			file.Delete();

			FileDownloadHandler fileDownloadHandler = new FileDownloadHandler(file.FullName);

			using (IE ie = new IE())
			{
				ie.AddDialogHandler(fileDownloadHandler);

				ie.GoTo("http://watin.sourceforge.net/WatiN-1.0.0.4000-net-1.1.msi");
				//        ie.GoTo("http://watin.sourceforge.net/WatiNRecorder.zip");

				fileDownloadHandler.WaitUntilFileDownloadDialogIsHandled(15);
				fileDownloadHandler.WaitUntilDownloadCompleted(200);
			}

			Assert.IsTrue(file.Exists, file.FullName + " file does not exist after download");
		}

		[Test, Ignore("Because of timeout issues, run this test manually and not automated"), Category("InternetConnectionNeeded")]
		public void DownloadRun()
		{
			WatiN.Core.DialogHandlers.FileDownloadHandler dhdl = new WatiN.Core.DialogHandlers.FileDownloadHandler(WatiN.Core.DialogHandlers.FileDownloadOptionEnum.Run);
			IE ie = new IE();
			ie.AddDialogHandler(dhdl);
			ie.WaitForComplete();
			ie.GoTo("http://watin.sourceforge.net/WatiN-1.0.0.4000-net-1.1.msi");

			dhdl.WaitUntilFileDownloadDialogIsHandled(5);
			dhdl.WaitUntilDownloadCompleted(20);
			ie.Close();
		}
	}
}