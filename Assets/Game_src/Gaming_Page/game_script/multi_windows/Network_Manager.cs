using UnityEngine;
using Unity.Netcode;
public class Network_Manager:NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    private bool isChangingOwnership = false;


    private string message_cache_type = "";
    private string message_cache_apparence = "";

    private void Update() {
        //Debug.Log("Network_process Update");
    }
    override public void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            _renderer.color = Color.red;
        }
        else
        {
            _renderer.color = Color.blue;
        }
    }
    // override public void OnGainedOwnership()
    // {
    //     base.OnGainedOwnership();
       
    // }
    // override public void OnLostOwnership()
    // {
    //     base.OnLostOwnership();
        
    // }
    
    // private void FixedUpdate()
    // {

        
    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         moveServerRpc(new Vector2(0, 0.2f));
    //     }
    //     if (Input.GetKey(KeyCode.S))
    //     {
    //         moveServerRpc(new Vector2(0, -0.2f));
    //     }
    //     if (Input.GetKey(KeyCode.A))
    //     {
    //         moveServerRpc(new Vector2(-0.2f, 0));
    //     }
    //     if (Input.GetKey(KeyCode.D))
    //     {
    //         moveServerRpc(new Vector2(0.2f, 0));
    //     }
    //     _rigidbody.linearVelocity *= 0.99f;

    //     if(Input.GetKeyDown(KeyCode.Q) ){
    //         ulong targetClientId = NetworkManager.Singleton.LocalClientId;
    //         Debug.Log($"当前客户端 ID: {targetClientId}");
    //         var networkObject = GetComponent<NetworkObject>();

    //         if (networkObject.OwnerClientId != targetClientId)
    //         {
    //             //isChangingOwnership = true;

    //             if (!IsHost)
    //             {
    //                 Debug.Log("调用 ChangeOwnershipServerRPC");
    //                 //networkObject.ChangeOwnership(targetClientId);
    //                 ChangeOwnershipServerRPC(targetClientId);
    //             }
    //             else
    //             {
    //                 Debug.Log("直接更改所有权");
    //                 networkObject.ChangeOwnership(targetClientId);
    //             }
    //         }
    //         else
    //         {
    //             Debug.LogWarning("当前客户端已拥有该对象的所有权，无需更改。");
    //         }
    //     }

        
    // }
    // [ServerRpc(RequireOwnership = false)]
    // private void moveServerRpc(Vector2 newVelocity){
    //     moveClientRpc(newVelocity);
    // }
    // [ClientRpc]
    // private void moveClientRpc(Vector2 newVelocity){
    //     _rigidbody.linearVelocity+=newVelocity;
    // }
    
    // [ServerRpc(RequireOwnership = false)]
    // private void ChangeOwnershipServerRPC(ulong clientId)
    // {
    //     Debug.Log($"服务器收到 ChangeOwnershipServerRPC 请求，目标客户端 ID: {clientId}");
    //     var networkObject = GetComponent<NetworkObject>();

    //     if (networkObject.OwnerClientId != clientId)
    //     {
    //         networkObject.ChangeOwnership(clientId);
    //         //networkObject.ChangeOwnership(clientId);
    //         Debug.Log($"所有权已更改为客户端 ID: {clientId}");
    //     }
    //     else
    //     {
    //         Debug.LogWarning("服务器检测到目标客户端已经是当前所有者，无需更改所有权。");
    //     }

    //     isChangingOwnership = false;
    // }

    // private void HandleOwnershipChanged(ulong oldOwner, ulong newOwner)
    // {
    //     Debug.Log($"所有权已从客户端 {oldOwner} 更改为客户端 {newOwner}");
    //     // 在这里添加任何需要在所有权更改后执行的逻辑
    // }




    private void OnEnable()
    {
        // 添加客户端连接事件监听器
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }
    private void OnClientConnected(ulong clientId)
    {
        // 客户端连接时被调用
        Debug.Log($"Client connected with ClientId: {clientId}");
        // 可以在这里调用发送消息的方法
        SendMessageToClient(clientId, "Welcome to the server!");
    }

    [ClientRpc]
    private void ReceiveMessageTypeClientRpc(string message, ClientRpcParams rpcParams = default)
    {
        Debug.Log("Received message: " + message);
    }
    [ClientRpc]
    private void ReceiveMessageApparenceClientRpc(string message)// when receive appearance message change appearance
    {
        Debug.Log("Received message: " + message);
    }

    // 调用此方法来向指定的客户端发送消息
    public void SendMessageToClient(ulong clientId, string message)
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
}
