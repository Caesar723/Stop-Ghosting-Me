using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class ControllerWhitWindowPosition : MonoBehaviour
{
    [SerializeField] WindowPositionGetter positionGetter;
    [SerializeField] Transform CameraRoot;
    [SerializeField] float rateBetweenWindowsToGame;//1��Ӧ0.00342 100��Ӧ0.0343
    
    float rateBetweenWindowsToGameX, rateBetweenWindowsToGameY;
    bool isFocus = false;
    public TextMeshProUGUI fpsText;

    private Vector3 offset;
    private Vector3 lastMousePosition;
    private bool isDragging=false;
    private void Start()
    {
        offset=positionGetter.GetWindowPosition() ;
        
        //InvokeRepeating("CameraUpdate",0.5f,0.05f);
    }

    private void Update()
    {
        //Vector3 mousePosition = Input.mousePosition;
        //Debug.Log("Mouse Position: ");
    
        // if(isFocus)
        // {
        //     CameraUpdate();
        // }
        //var pos = positionGetter.GetWindowPosition();

        // if (pos == Vector2.zero)
        // {
        //     Debug.Log("zero");
        //     return;
        // }
        //CameraRoot.transform.position = pos * rateBetweenWindowsToGame ;
        //CameraUpdate();
        //帧数
        // if (Input.GetKey(KeyCode.W))
        // {
        //     CameraRoot.transform.position += new Vector3(0,0.2f,0);
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     CameraRoot.transform.position += new Vector3(0,-0.2f,0);
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     CameraRoot.transform.position += new Vector3(-0.2f,0,0);
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     CameraRoot.transform.position += new Vector3(0.2f,0,0);
        // }
        //当鼠标按下时
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
                CameraRoot.transform.position = (offset + deltaMousePosition) * rateBetweenWindowsToGame;
                //位置是offset + deltaMousePosition Vector2
                //Debug.Log("offset + deltaMousePosition: "+new Vector2(offset.x + deltaMousePosition.x*rateBetweenWindowsToGame*2,offset.y + deltaMousePosition.y*rateBetweenWindowsToGame*2));
                positionGetter.SetWindowsPosition(new Vector2(offset.x + deltaMousePosition.x,offset.y + deltaMousePosition.y));
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            lastMousePosition=positionGetter.GetCursorPosition();
            isDragging=false;
        }

        //CameraUpdate();
        //CameraRoot.transform.position = positionGetter.GetWindowPosition() * rateBetweenWindowsToGame ;
        
    }
    
    public void CameraUpdate(bool isOnlyOnce = false)
    {
        //Debug.Log(positionGetter.GetWindowPosition());
        //显示在左上角计时器计算与上一个帧的时间差
        var deltaTime = Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + 1.0f / deltaTime;

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
    public void ChangeRate(float rateX,float rateY)
    {
        rateBetweenWindowsToGameX = rateX;
        rateBetweenWindowsToGameY = rateY;
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
