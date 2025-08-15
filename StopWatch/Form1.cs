using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StopWatch
{
    public partial class Form1 : Form
    {
        private long elapsedSecs = 0;

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")] 
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public Form1()
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            InitializeComponent();
            UpdateTime();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            UpdateTime();
            timer.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer.Stop();
            UpdateTime();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            timer.Stop();
            elapsedSecs = 0;
            UpdateTime();
        }
        private void UpdateTime()
        {
            long hours = elapsedSecs / 60 / 60;
            long minutes = (elapsedSecs / 60) - hours * 60;
            long seconds = elapsedSecs - hours * 60 * 60 - minutes * 60;
            lblTime.Text = string.Format("{0}:{1}:{2}",
                hours.ToString("00"),
                minutes.ToString("00"),
                seconds.ToString("00"));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            elapsedSecs++;
            UpdateTime();
        }
    }
}
