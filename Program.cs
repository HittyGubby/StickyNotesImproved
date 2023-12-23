using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    const int GWL_EXSTYLE = -20;
    const int WS_EX_NOACTIVATE = 0x00000008;
    const int HWND_TOPMOST = -1;
    const int SWP_HIDEWINDOW = 0x0080;

    static void Main()
        {
            IntPtr hWnd = FindWindow(null, "Sticky Notes"); //find window
            if (hWnd == IntPtr.Zero) //nonexistent
            {
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Windows\Sysnative\StikyNot.exe");
                    Process.Start(startInfo); //start stickynotes
                    Thread.Sleep(100);
                    hWnd = FindWindow(null, "Sticky Notes"); //find window again
                }
                int style = GetWindowLong(hWnd, GWL_EXSTYLE);
                style |= WS_EX_NOACTIVATE;
                SetWindowLong(hWnd, GWL_EXSTYLE, style);
                SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_HIDEWINDOW);
            }
            else
            {
                int style = GetWindowLong(hWnd, GWL_EXSTYLE);
                style |= WS_EX_NOACTIVATE; //set window style
                SetWindowLong(hWnd, GWL_EXSTYLE, style);
                SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_HIDEWINDOW); //make top
            }
        }
    }