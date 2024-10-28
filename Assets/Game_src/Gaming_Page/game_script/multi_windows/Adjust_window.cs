using UnityEngine;
using UnityEngine.UI;



class Adjust_window : MonoBehaviour
{
    [SerializeField] WindowPositionGetter window_position_getter;

    private void Start()
    {
        window_position_getter.SetWindowsPosition(new Vector2(2000, 2000));
        window_position_getter.SetWindowSize(1366, 768);
    }
    private void Update()
    {
        //window_position_getter.SetWindowsPosition(new Vector2(300, 300));
        window_position_getter.SetWindowSize(1366, 768);
    }
}
