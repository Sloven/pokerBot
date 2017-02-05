using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;

namespace WindowManipulations
{
    public class Ops
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetTopWindow(IntPtr parentHandle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName,int nMaxCount);

        /// <summary>
        /// Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyWindow(IntPtr hWnd);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        static extern bool SetForegroundWindow(IntPtr Hwnd);

        [DllImport("user32.Dll")]
        public static extern int PostMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass 
        // IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            IntPtr ProcessId);

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        /// The GetForegroundWindow function returns a handle to the 
        /// foreground window.
        //[DllImport("user32.dll")]
        //public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach,
            uint idAttachTo, bool fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(HandleRef hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        const UInt32 WM_CLOSE = 0x0010;
        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;

        const int BM_CLICK = 0x00F5;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }

        public static IntPtr FindWindowByCaption(string ParentTitle)
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, ParentTitle);
            return windowPtr;
        }

        public static IntPtr FindChild(string ParentTitle, string childCaption, string ChildClass = "")
        {
            var windowPtr = FindWindowByCaption(ParentTitle);
            if (windowPtr == IntPtr.Zero)
                return IntPtr.Zero;

            IntPtr child = GetWindow(windowPtr, GetWindow_Cmd.GW_CHILD | GetWindow_Cmd.GW_HWNDFIRST);
            while (child != IntPtr.Zero)
            {
                child = GetWindow(child, GetWindow_Cmd.GW_HWNDNEXT);

                IntPtr hwnd = child;
                int length = (int)SendMessage(hwnd, WM_GETTEXTLENGTH, IntPtr.Zero, null);
                StringBuilder sCapt = new StringBuilder(length + 1);
                StringBuilder sClass = new StringBuilder(length + 1);
                
                SendMessage(hwnd, WM_GETTEXT, (IntPtr)sCapt.Capacity, sCapt);
                GetClassName(hwnd, sClass, sClass.Capacity);
                string cCapt = sCapt.ToString();
                string cClass = sClass.ToString();

                if (cCapt.Contains(childCaption) && (cClass == ChildClass || String.IsNullOrEmpty(ChildClass)))
                    return child;
            }
            return IntPtr.Zero;
            
        }

        public static void ClickChild(string ParentTitle, string ChildCaption, int WaitTime, string ChildClass = "")
        {
            Thread.Sleep(WaitTime);
            Point p = ChildTopLeft(ParentTitle, ChildCaption, ChildClass);
            if(p.X>0 && p.Y > 0)
                MouseSimulator.MoveLClick(p.X,p.Y);
        }

        public static void ClickWindow(string Title, string WClass = "#32770", int xDelta = 5, int yDelta = 5)
        {
            var windowPtr = FindWindow(WClass, Title);
            if (windowPtr == IntPtr.Zero)
                return;

            RECT r = new RECT();
            GetWindowRect(windowPtr, out r);
            MouseSimulator.MoveLClick(r.Left + xDelta, r.Top + yDelta);
        }

        public static void SendWindowKey(IntPtr hWnd, VirtualKeyCode kcode, int xDelta = 5, int yDelta = 5)
        {
            RECT r = new RECT();
            GetWindowRect(hWnd, out r);
            MouseSimulator.MoveLClick(r.Left + xDelta, r.Top + yDelta);

            InputSimulator.SimulateKeyPress(kcode);
        }


        public static bool WaitActiveWindow(string ClassName, string Title, int Timeout)
        {
            IntPtr wndw = FindWindow(ClassName, Title);
            IntPtr actw = GetForegroundWindow();
            bool IsActive = wndw == actw;
            while ((wndw == IntPtr.Zero || (wndw != IntPtr.Zero && !IsActive)) && Timeout > 0)
            {
                Thread.Sleep(1000);
                wndw = FindWindow(ClassName, Title);
                IsActive = wndw == GetForegroundWindow();
                actw = GetForegroundWindow();
                Console.WriteLine("{0:x}",actw);
                Timeout -= 1000;
            }

            return Timeout > 0;
        }

        public static bool WaitUntillWindowExists(string ClassName, string Title, int Timeout)
        {
            IntPtr wndw = FindWindow(ClassName, Title);
            while (wndw != IntPtr.Zero)
            {
                Thread.Sleep(1000);
                wndw = FindWindow(ClassName, Title);
                Timeout -= 1000;
            }

            return Timeout > 0;
        }

        public static bool WaitUntillWindowAppears(string ClassName, string Title, int Timeout)
        {
            IntPtr wndw = FindWindow(ClassName, Title);
            while (wndw == IntPtr.Zero)
            {
                Thread.Sleep(1000);
                wndw = FindWindow(ClassName, Title);
                Timeout -= 1000;
            }

            return Timeout > 0;
        }

        public static Point ChildTopLeft(string ParentTitle, string ChildCaption, string ChildClass)
        {
            Point p = new Point(-1, -1);
            IntPtr Mainwindow = FindWindowByCaption(ParentTitle);
            if (Mainwindow == IntPtr.Zero)
                return p;

            IntPtr cntrl = FindChild(ParentTitle, ChildCaption);
            ForceForegroundWindow(Mainwindow);
            RECT r = new RECT();
            GetWindowRect(cntrl, out r);

            if (r.Top > 0 && r.Left > 0)
                return new Point(r.Left + 5, r.Top + 5);

            return p;
        }

        
        
        public static void ClosePopup(string popupCaption)
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, popupCaption);
            if (windowPtr == IntPtr.Zero)
                return;

            PostMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }


        private static void ForceForegroundWindow(IntPtr hWnd)
        {
            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            uint appThread = GetCurrentThreadId();
            const uint SW_SHOW = 5;

            if (foreThread != appThread)
            {
                AttachThreadInput(foreThread, appThread, true);
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, SW_SHOW);
                AttachThreadInput(foreThread, appThread, false);
            }

            else
            {
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, SW_SHOW);
            }

        }

    }
}
