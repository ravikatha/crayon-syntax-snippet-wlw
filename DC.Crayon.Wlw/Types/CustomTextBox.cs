using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DC.Crayon.Wlw
{
	public class CustomTextBox : TextBox
	{
		#region Native
		/// <summary>
		/// Paste message
		/// </summary>
		private const int WM_PASTE = 0x0302;

		/// <summary>
		/// User messages
		/// </summary>
		private const int WM_USER = 0x0400;

		/// <summary>
		/// Pasted message
		/// </summary>
		private const int WM_APP_PASTED = WM_USER + 1;

		/// <summary>
		/// Post Message
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="uMsg"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);
		#endregion

		#region Events
		/// <summary>
		/// Content pasted into the textbox
		/// </summary>
		public event EventHandler Pasted;
		#endregion

		#region Paste Handling
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_PASTE)
			{
				if (IsHandleCreated)
				{
					PostMessage(this.Handle, WM_APP_PASTED, IntPtr.Zero, IntPtr.Zero);
				}
			}
			else if (m.Msg == WM_APP_PASTED)
			{
				OnPasted();
			}

			// Handle all messages normally
			base.WndProc(ref m);
		}
		#endregion

		#region Handlers
		/// <summary>
		/// Posted handler
		/// </summary>
		protected virtual void OnPasted()
		{
			if (Pasted != null)
			{
				Pasted(this, EventArgs.Empty);
			}
		}
		#endregion
	}
}
