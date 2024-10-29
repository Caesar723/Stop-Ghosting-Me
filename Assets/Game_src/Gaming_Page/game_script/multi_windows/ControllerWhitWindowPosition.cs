using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Unity.Netcode;
public class ControllerWhitWindowPosition : MonoBehaviour
{
    [SerializeField] WindowPositionGetter positionGetter;
    [SerializeField] Transform CameraRoot;
    public Camera mainCamera;
    [SerializeField] float rateBetweenWindowsToGame;//1Ӧ0.00342 100Ӧ0.0343
    [SerializeField] float fixedOrthographicSize;
    [SerializeField] Transform EnvironmentRoot;
    [SerializeField] NetworkConnection networkConnection;
    
    
    float rateBetweenWindowsToGameX, rateBetweenWindowsToGameY;
    bool isFocus = false;

    //public TextMeshProUGUI fpsText;

    private Vector3 offset;
    private Vector3 offset_window;
    private Vector3 lastMousePosition;
    private bool isDragging=false;
    private Vector3 main_window_size=new Vector3(1920/2,1080/2,0);
    private Vector3 small_window_size=new Vector3(100,100,0);

    private void Start()
    {
        positionGetter.SetWindowsPositionToCenter();
        offset=positionGetter.GetWindowPosition();//positionGetter.GetWindowPosition() ;
        
        //InvokeRepeating("CameraUpdate",0.5f,0.05f);
        #if UNITY_STANDALONE_WIN
            offset_window=new Vector3(0,0,0);
        #endif
        #if UNITY_STANDALONE_OSX
            offset_window=main_window_size/2-small_window_size/2+new Vector3(5.5f,0,0);
        #endif
    }

    private void Update()
    {
        if(isFocus)
        {
            if (networkConnection.isHost_Connect)
            {
                HostUpdate();
            }
            else if(networkConnection.isClient_Connect)
            {
                ClientUpdate();
            }
        }
        
    }
    private void HostUpdate()
    {
        positionGetter.SetWindowSize(1920/2,1080/2);
        mainCamera.orthographicSize =5;
        EnvironmentRoot.transform.position = positionGetter.GetWindowPosition() * rateBetweenWindowsToGame + new Vector2(1.7f,-2.5f);
        CameraRoot.transform.position = positionGetter.GetWindowPosition() * rateBetweenWindowsToGame ;

        //Debug.Log(positionGetter.GetWindowBarHeightPublic());
    }

    private void ClientUpdate()
    {
        positionGetter.SetWindowSize(100,100);
        mainCamera.orthographicSize = 5/(fixedOrthographicSize/100);
        if(Input.GetMouseButtonDown(0))
        {
            offset=positionGetter.GetWindowPosition() ;
            lastMousePosition=positionGetter.GetCursorPosition();
            
            isDragging=true;
        }
        if(Input.GetMouseButton(0))
        {
            //offset+鼠标位置的变化
            if(isDragging)
            {
                Vector3 cursorPosition = new Vector3(positionGetter.GetCursorPosition().x,positionGetter.GetCursorPosition().y,0);
                Vector3 lastMousePosition2 = new Vector3(lastMousePosition.x,lastMousePosition.y,0);
                Vector3 deltaMousePosition = cursorPosition - lastMousePosition2;
                // #if UNITY_STANDALONE_WIN
                // deltaMousePosition.y = -deltaMousePosition.y;
                // #endif
                CameraRoot.transform.position = (offset + deltaMousePosition-offset_window) * rateBetweenWindowsToGame;
                //位置是offset + deltaMousePosition Vector2
                //Debug.Log("offset + deltaMousePosition: "+new Vector2(offset.x + deltaMousePosition.x*rateBetweenWindowsToGame*2,offset.y + deltaMousePosition.y*rateBetweenWindowsToGame*2));
                #if UNITY_STANDALONE_WIN
                    positionGetter.SetWindowsPosition(new Vector2(offset.x + deltaMousePosition.x,-offset.y + deltaMousePosition.y));
                #endif
                #if UNITY_STANDALONE_OSX
                    positionGetter.SetWindowsPosition(new Vector2(offset.x + deltaMousePosition.x,offset.y + deltaMousePosition.y));
                #endif
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            lastMousePosition=positionGetter.GetCursorPosition();
            isDragging=false;
        }

        //CameraUpdate();

        CameraRoot.transform.position = (positionGetter.GetWindowPosition()- new Vector2(offset_window.x,offset_window.y)) * rateBetweenWindowsToGame ;
       
        
       
    }
    
    public void CameraUpdate(bool isOnlyOnce = false)
    {
        //Debug.Log(positionGetter.GetWindowPosition());
        //显示在左上角计时器计算与上一个帧的时间差
        var deltaTime = Time.unscaledDeltaTime;
        //fpsText.text = "FPS: " + 1.0f / deltaTime;

        var pos = positionGetter.GetWindowPosition();
        
        
        if (pos == Vector2.zero)
        {
            Debug.Log("zero");
            return;
        }
        if(isFocus)
        {
            //CameraRoot.transform.position = pos * rateBetweenWindowsToGame ;
            //CameraRoot.transform.position += new Vector3(deltaTime,0,0);
        }
        //var pos = positionGetter.GetWindowPosition();
        //CameraRoot.transform.position =  new Vector2(pos.x * rateBetweenWindowsToGameX, pos.y * rateBetweenWindowsToGameY);
        //CameraUpdate();   
        // if(!isFocus || isOnlyOnce)
        // {
        //     return;
        // }
        // await Task.Delay(10);
        // CameraUpdate(isOnlyOnce);
    }
    public void ChangeRate(float rate)
    {
        rateBetweenWindowsToGame = rate;
    }
    // public void ChangeRate(float rateX,float rateY)
    // {
    //     rateBetweenWindowsToGameX = rateX;
    //     rateBetweenWindowsToGameY = rateY;
    // }
    public void ChangeOrthographicSize(float size)
    {
        fixedOrthographicSize = size;
    }
    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
        Debug.Log(isFocus);
        if(focus)
        {
            offset=positionGetter.GetWindowPosition() * rateBetweenWindowsToGame;
            //CameraUpdate();
        }
    }
    
    
}
