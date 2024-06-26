﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace nsForm
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
			//Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			//AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new Adaman_Form());
			Application.Run(new Main_Form());
		}

		private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
		{
			DialogResult result = DialogResult.Cancel;
			try
			{
				Exception ex = (Exception)t.Exception;

				string errorMsg = "UIThreadException\r\n\r\n";
				errorMsg += "Crash!\r\n\r\n";
				errorMsg += ex.Message + "\r\n\r\n";
				errorMsg += "Stack Trace:\r\n" + ex.StackTrace + "\r\n\r\n";
				ShowErrorDialog("UIThreadException", errorMsg);
			}
			catch
			{
				try
				{
					MessageBox.Show("Fatal Windows Forms Error", "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
				}
				finally
				{
					Application.Exit();
				}
			}

			// Exits the program when the user clicks Abort.
			if (result == DialogResult.Abort)
				Application.Exit();
		}

		private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception ex = (Exception)e.ExceptionObject;

				string errorMsg = "UnhandledException\r\n\r\n";
				errorMsg += "Crash!\r\n\r\n";
				errorMsg += ex.Message + "\r\n\r\n";
				errorMsg += "Stack Trace:\r\n" + ex.StackTrace + "\r\n\r\n";
				ShowErrorDialog("UnhandledException", errorMsg);

				if (!EventLog.SourceExists("UnhandledException"))
				{
					EventLog.CreateEventSource("UnhandledException", "Application");
				}
				EventLog myLog = new EventLog();
				myLog.Source = "UnhandledException";
				myLog.WriteEntry(errorMsg);
			}
			catch (Exception exc)
			{
				try
				{
					MessageBox.Show("Fatal Non-UI Error", "Fatal Non-UI Error. Could not write the error to the event log. Reason: " + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				finally
				{
					Application.Exit();
				}
			}
		}

		private static DialogResult ShowErrorDialog(string title, string errormsg)
		{
			return MessageBox.Show(errormsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
		}

	}
}
