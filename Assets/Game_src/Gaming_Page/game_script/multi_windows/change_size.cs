using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class change_size : MonoBehaviour
{
    public TMP_InputField inputField;
    public ControllerWhitWindowPosition controller;

    void Start()
    {
        inputField.onEndEdit.AddListener(ChangeSize);
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
