using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkConnection : NetworkBehaviour
{
    private float nowTime = 10f;
    public static event Action OnNetDone;
    
    
    //[SceneName] public string firstScene;
    private async void Start()
    {
        if(NetworkManager.Singleton.IsConnectedClient || IsHost)
        {
            return;
        }
        await Task.Yield();
        TestServer();

    }
    // [EditorButton]
    private async void TestServer()
    {
        NetworkManager.Singleton.StartClient();
        if(Application.isEditor)
        {
            await Task.Delay(1000);
        }
        else
        {
            await Task.Delay(2000);
        }
        // 判断当前是否有主机
        if (NetworkManager.Singleton.IsConnectedClient)
        {
            Debug.Log("HasServer");
            OnNetDone?.Invoke();
        }
        else
        {
            TestHost();
            
        }
      
    }
    
    private async void TestHost()
    {
        nowTime = 10f;
        NetworkManager.Singleton.Shutdown();
        while (nowTime>0)
        {
            nowTime-= Time.deltaTime;
            if(!NetworkManager.Singleton.ShutdownInProgress)
            {
                break;
            }
            await Task.Yield();
        }
        NetworkManager.Singleton.StartHost();
        OnNetDone?.Invoke();
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
           "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
           NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }


    
    

}
