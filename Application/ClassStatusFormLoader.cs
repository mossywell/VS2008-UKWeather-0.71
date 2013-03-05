using System;
using System.Threading;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	public class StatusFormLoader
	{
		#region Class Fields
		private string     _strMessage;
		private Thread     _thread;
		private FormStatus _frmStatus;
		#endregion

		#region Constructor
		public StatusFormLoader(string message)
		{
      _strMessage = message;
		}
		#endregion

		#region Utility Methods
		public void AddText(string message)
		{
			// Invoked within the form's AddText method
			_frmStatus.AddText(message);
		}

		public void Show()
		{
			_thread = new Thread(new System.Threading.ThreadStart(LoadForm));
			_thread.Start();
		}

		public void Stop()
		{
			// Invoked within the form's Stop method
			_frmStatus.Stop();
		}

		public void Close()
		{
			// Close and dispose of the form	
	    _frmStatus.Close();
			_frmStatus.Dispose();

			// Stop the thread and wait for the stop
			if(_thread.ThreadState == ThreadState.WaitSleepJoin)
				_thread.Interrupt();

			_thread.Abort();
			_thread.Join();
		}

		private void LoadForm()
		{
			_frmStatus = new FormStatus(_strMessage);
			_frmStatus.ShowDialog();
		}
		#endregion
	}
}
