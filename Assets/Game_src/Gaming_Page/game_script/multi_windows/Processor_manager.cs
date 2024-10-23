using System.Diagnostics;
using UnityEngine;
using System.IO;
using Unity.Netcode;

public class Processor_manager : NetworkBehaviour
{
    // 保存进程对象
    private Process exeProcess;

    // 启动 exe 文件
    public void OpenExe()
    {
        if (!IsHost)
        {
            
            return;
        }

        string exePath;
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            exePath = Application.dataPath.Replace("_Data", ".exe");
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            exePath = Application.dataPath.Replace("_Data", ".app");
        }
        else
        {
            UnityEngine.Debug.LogError("不支持的操作系统");
            return;
        }

        if (File.Exists(exePath))
        {
            // 启动 exe 并保存进程对象
            exeProcess = Process.Start(exePath);
            UnityEngine.Debug.Log("重新打开文件: " + exePath);
        }
        else
        {
            UnityEngine.Debug.LogError("未找到文件");
        }
    }

    // 关闭 exe 进程
    public void CloseExe()
    {
        if (!IsHost)
        {
            return;
        }

        if (exeProcess != null && !exeProcess.HasExited)
        {
            // 尝试优雅地关闭进程
            exeProcess.CloseMainWindow();

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
