using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionDisplay : MonoBehaviour
{
    public TextMeshProUGUI hoverTextUI;
    public float typingSpeed = 0.02f;

    private Coroutine typingCoroutine;

    public void ShowHoverText(string text)
    {
        // stop any ongoing typing to avoid overlaps
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        // start a new typing
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        hoverTextUI.text = ""; // clear text
        foreach (char c in text)
        {
            hoverTextUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null; // clear references
    }

    public void ClearHoverText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        hoverTextUI.text = "";
        typingCoroutine = null;
    }
}
