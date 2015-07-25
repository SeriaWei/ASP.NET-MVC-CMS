using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Easy.WinApi
{
    public class OpenApi
    {
        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            leftClick = 0x0002 | 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }
        /// <summary>
        /// 设置鼠标的位置
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        /// <summary>
        /// 模拟鼠标事件 mouse_event(MouseEventFlag.leftClick,0,0,0,UIntPtr.Zero)
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="data"></param>
        /// <param name="extraInfo"></param>
        [DllImport("User32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        /// <summary>
        /// 查找窗口句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName">标题名</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 查找窗口内按钮句柄
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="childe"></param>
        /// <param name="strclass"></param>
        /// <param name="FrmText"></param>
        /// <returns></returns>
        [DllImport("User32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);

        /// <summary>
        /// 向指定句柄发送消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    }
}
