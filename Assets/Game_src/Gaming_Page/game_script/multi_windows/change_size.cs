using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class change_size : MonoBehaviour
{
    public TMP_InputField inputField;
    public ControllerWhitWindowPosition controller;
    public Button yourButton; // 添加按钮引用
    public Processor_manager processor_manager;

    void Start()
    {
        inputField.onEndEdit.AddListener(ChangeSize);
        yourButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        inputField.text=processor_manager.GetExePath();
        processor_manager.OpenExe();
        Debug.Log("Button clicked");
    }
    void ChangeSize(string input)
    {
        if (float.TryParse(input, out float newSize))
        {
            //targetTransform.localScale = new Vector3(newSize, newSize, newSize);
            controller.ChangeRate(newSize);
            
            //controller.ChangeOrthographicSize(newSize);
        }
        else
        {
            Debug.LogError("Invalid input for size");
        }
    }
}
