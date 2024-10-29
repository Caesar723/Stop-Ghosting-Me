using UnityEngine;
using UnityEngine.UI;



class Adjust_window : MonoBehaviour
{
    [SerializeField] WindowPositionGetter window_position_getter;

    private bool in_position = false;
    private bool in_size = false;
    private void Start()
    {
        window_position_getter.SetWindowsPosition(new Vector2(100,100));
        window_position_getter.SetWindowSize(1366, 768);
    }
    private void Update()
    {
        //window_position_getter.SetWindowsPosition(new Vector2(300, 300));
        Debug.Log(window_position_getter.GetWindowPosition() + " " + new Vector2(100, 100));
        if(!in_position)
        {
            window_position_getter.SetWindowsPosition(new Vector2(100, 100));
            if(window_position_getter.GetWindowPosition() == new Vector2(100, 100))
            {
                in_position = true;
            }
        }
       
        window_position_getter.SetWindowSize(1366, 768);
            
    }

    
}
