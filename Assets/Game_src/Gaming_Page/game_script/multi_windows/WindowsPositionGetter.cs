using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowPositionGetter : MonoBehaviour
{
    [SerializeField] bool isTest;

    // Windows-specific code
    #if UNITY_STANDALONE_WIN
    public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

    [DllImport("kernel32.dll")]
    public static extern void SetLastError(uint dwErrCode);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);






    private IntPtr nowWindow;
    [Serializable]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    [Serializable]
    private struct POINT
    {
        public int X;
        public int Y;
    }
    #endif
    


    // Mac-specific code
    #if UNITY_STANDALONE_OSX
    [DllImport("MacWindowPlugin")]
    private static extern void SetWindowPosition(float x, float y);

    [DllImport("MacWindowPlugin")]
    private static extern void GetWindowPosition(out float x, out float y);

    [DllImport("MacWindowPlugin")]
    private static extern void GetCursorPosition(out float x, out float y);


    [DllImport("MacWindowPlugin")]
    private static extern float GetWindowBarHeight();

    [DllImport("__Internal")]
    private static extern void OpenSelfApp(string appPath);
    

    [DllImport("MacWindowPlugin")]
    private static extern void SetWindowSize(float width, float height);
    #endif

    // Shared variables
    [SerializeField] Vector2 windowPosition;
    [SerializeField] Vector2 screenSize;
    Resolution primaryRes;
    public int offsetX, offsetY;

    void Start()
    {
        primaryRes = Screen.currentResolution;
        screenSize = new Vector2(primaryRes.width, primaryRes.height);

        #if UNITY_STANDALONE_WIN
        nowWindow = GetProcessWndWin();
        #endif

        if (!Application.isEditor || isTest)
        {
            SetWindowsPositionToCenter();
        }
    }

    public Vector2 GetWindowPosition()
    {
        #if UNITY_STANDALONE_WIN
        GetWindowRect(nowWindow, out RECT rect);
        windowPosition = new Vector2(rect.left + (rect.right - rect.left) / 2, rect.top + (rect.bottom - rect.top) / 2);
        #elif UNITY_STANDALONE_OSX
        float x, y;
        GetWindowPosition(out x, out y);
        windowPosition = new Vector2(x, y);
        #endif

        return windowPosition;
    }
    public Vector2 GetCursorPosition()
    {
        #if UNITY_STANDALONE_WIN
        POINT point;
        GetCursorPos(out point);
        return new Vector2(point.x, point.y);
        #elif UNITY_STANDALONE_OSX
        float x, y;
        GetCursorPosition(out x, out y);
        return new Vector2(x, y);
        #endif
    }

    const int ScreenSizeX = 1920/2, ScreenSizeY = 1080/2;

    public void SetWindowsPositionToCenter()
    {
        SetWindowsPosition(new Vector2(screenSize.x / 2 - offsetX, screenSize.y / 2 - offsetY));
    }

    public void SetWindowsPosition(Vector2 pos)
    {
        #if UNITY_STANDALONE_WIN
        const uint SWP_NOOWNERZORDER = 0x0200;
        const uint SWP_NOSIZE = 0x0001;
        SetWindowPos(nowWindow, 0, (int)pos.x, (int)pos.y - GetWindowBarHeight() / 2,
            ScreenSizeX, ScreenSizeY + GetWindowBarHeight(), SWP_NOOWNERZORDER | SWP_NOSIZE);
        #elif UNITY_STANDALONE_OSX
        //SetWindowSize(ScreenSizeX, ScreenSizeY + GetWindowBarHeight());
        SetWindowPosition(pos.x, pos.y - GetWindowBarHeight() / 2);
        #endif
    }
    public void SetWindowSize(int width, int height)
    {
        #if UNITY_STANDALONE_WIN
        const uint SWP_NOOWNERZORDER = 0x0200;
        const uint SWP_NOMOVE = 0x0002;
        SetWindowPos(nowWindow, 0, 0, 0, width, height + GetWindowBarHeight(), SWP_NOOWNERZORDER | SWP_NOMOVE);
        #elif UNITY_STANDALONE_OSX
        SetWindowSize(width, height + GetWindowBarHeight());
        #endif
    }
    public float GetWindowBarHeightPublic()
    {
        return GetWindowBarHeight();
    }


    #if UNITY_STANDALONE_WIN
    public static int GetWindowBarHeight()
    {
        IntPtr handle = GetForegroundWindow();

        // SM_CYSIZEFRAME and SM_CYCAPTION correspond to the window border and title bar dimensions
        int borderWidth = GetSystemMetrics(32); // SM_CXSIZEFRAME
        int titleBarHeight = GetSystemMetrics(4); // SM_CYCAPTION

        return borderWidth + titleBarHeight;
    }


    public static IntPtr GetProcessWndWin()
    {
        IntPtr ptrWnd = IntPtr.Zero;
        uint pid = (uint)Process.GetCurrentProcess().Id;  // Current process ID

        bool bResult = EnumWindows(new WNDENUMPROC(delegate (IntPtr hwnd, uint lParam)
        {
            uint id = 0;

            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref id);
                if (id == lParam)    // Found the main window handle for the process
                {
                    ptrWnd = hwnd;   // Cache the handle
                    SetLastError(0);    // Set no error
                    return false;   // Return false to stop enumerating windows
                }
            }

            return true;

        }), pid);

        return (!bResult && Marshal.GetLastWin32Error() == 0) ? ptrWnd : IntPtr.Zero;
    }
    #endif
    
}
