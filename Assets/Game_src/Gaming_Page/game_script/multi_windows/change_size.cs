using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class change_size : MonoBehaviour
{
    public TMP_InputField inputField;
    public ControllerWhitWindowPosition controller;

    public Processor_manager processor_manager;

    void Start()
    {
        inputField.onEndEdit.AddListener(ChangeSize);
    }

    void ChangeSize(string input)
    {
        if (float.TryParse(input, out float newSize))
        {
            //targetTransform.localScale = new Vector3(newSize, newSize, newSize);
            //controller.ChangeRate(newSize);
            inputField.text=processor_manager.GetExePath();
            processor_manager.OpenExe();
            //controller.ChangeOrthographicSize(newSize);
        }
        else
        {
            Debug.LogError("Invalid input for size");
        }
    }
}
