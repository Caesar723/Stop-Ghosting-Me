using System.Diagnostics;
using UnityEngine;
using System.IO;
using Unity.Netcode;

public class Processor_manager : NetworkBehaviour
{
    // 保存进程对象
    private Process exeProcess;

    // 启动 exe 文件

    public string GetExePath(){
        #if UNITY_STANDALONE_WIN
            string exePath = Application.dataPath.Replace("_Data", ".exe");
            return exePath;
        #endif
        #if UNITY_STANDALONE_OSX
            string exePath = Application.dataPath.Replace("_Data", ".app");
            string appPath = exePath.Substring(0, exePath.LastIndexOf(".app") + 4);
            return appPath;
        #endif
        
    }
    public Process OpenExe()
    {
        // if (!IsHost)
        // {
            
        //     return;
        // }

        string exePath = GetExePath();

        #if UNITY_STANDALONE_OSX
            var psi = new ProcessStartInfo
            {
                FileName = "open",
                Arguments = $"-n \"{exePath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };
            exeProcess = Process.Start(psi);
        #endif  

        #if UNITY_STANDALONE_WIN
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            exeProcess = Process.Start(psi);
        #endif
        
        return exeProcess;
    }

    // 关闭 exe 进程
    public void CloseExe(Process process)
    {
        if (!IsHost)
        {
            return;
        }

        if (process != null && !process.HasExited)
        {
            // 尝试优雅地关闭进程
            process.CloseMainWindow();

            // 如果需要强制关闭，使用 Kill()
            // exeProcess.Kill();

            UnityEngine.Debug.Log("关闭 exe 进程");
        }
        else
        {
            UnityEngine.Debug.LogWarning("进程未启动或已经退出");
        }
    }
}
