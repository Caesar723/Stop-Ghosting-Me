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
    [SerializeField] WindowPositionGetter positionGetter;
    public Camera mainCamera;
    public bool isHost_Connect = false;
    public bool isClient_Connect = false;
    public Character_apperance character_apperance;
    public float fixedOrthographicSize = 5.0f; // 固定的相机正交大小

    // list of GameObject
    [SerializeField] GameObject[] GameObject_hide_list;

    //list of processor
    private List<System.Diagnostics.Process> processor_list = new List<System.Diagnostics.Process>();
    [SerializeField] Processor_manager processor_manager;
    private string camera_type_cache="0";
    
    
    
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
            start_become_client();
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
        start_become_host();
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


    // private void Start()
    // {
    //     // 添加客户端连接事件监听器
        
    //     
        
    // }
    private void start_become_host()
    {
        isHost_Connect = true;
        positionGetter.SetWindowSize(1920/2,1080/2);
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        //character_apperance.ChangeAppearance(true);
    }
    private void start_become_client()
    {
        isClient_Connect = true;
        
        foreach (var item in GameObject_hide_list)
        {
            item.SetActive(false);
        }
        positionGetter.SetWindowSize(100,100);
        //mainCamera.orthographicSize = fixedOrthographicSize/(599/100);
        
    }
    private void OnClientConnected(ulong clientId)
    {
        // 客户端连接时被调用
        Debug.Log(NetworkManager.Singleton.LocalClientId);
        Debug.Log($"Client connected with ClientId: {clientId}");
        // 可以在这里调用发送消息的方法
        
        SendMessageTypeToClient(clientId, camera_type_cache);
        SendMessageApparenceToClient(character_apperance.GetAppearance());
        
        
    }

    [ClientRpc]
    private void ReceiveMessageTypeClientRpc(string message, ClientRpcParams rpcParams = default)
    {
        
        character_apperance.CheckCameraType(message);
        Debug.Log("Received message: " + message);
    }
    [ClientRpc]
    private void ReceiveMessageApparenceClientRpc(string message)// when receive appearance message change appearance
    {   
        if (!IsHost)
        {
            Debug.Log("Received message: " + message);
            character_apperance.SetAppearance(message);
        }
    }



    // 调用此方法来向指定的客户端发送消息
    public void SendMessageTypeToClient(ulong clientId, string message)
    {
        // 创建 ClientRpcParams 实例
        var rpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };

        // 调用 RPC，向特定客户端发送消息
        ReceiveMessageTypeClientRpc(message, rpcParams);
    }
    public void SendMessageApparenceToClient(string message){
        // 向所有客户端发送消息
        var rpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { NetworkManager.Singleton.LocalClientId }
            }
        };
        ReceiveMessageApparenceClientRpc(message);
    }

    public void start_camera(string type)
    {
        Debug.Log("start_camera: " + type);
        camera_type_cache = type;
        processor_list.Add(processor_manager.OpenExe());
    }
    public void close_all_camera()
    {
        foreach (var process in processor_list)
        {
            processor_manager.CloseExe(process);
        }
        processor_list.Clear();
    }
    


    
    

}
