using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class PhrasePicker : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.05f;

    private List<string> phrases = new List<string>();
    private Coroutine typingCoroutine;

    public void DisplayRandomPhraseFromFile(string filePath)
    {
        LoadPhrasesFromFile(filePath);
        if (phrases.Count > 0)
        {
            string randomPhrase = phrases[Random.Range(0, phrases.Count)];

            // stop the previous phrase if it is still running
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            // start the new typing
            typingCoroutine = StartCoroutine(TypeText(randomPhrase));
        }
    }

    private void LoadPhrasesFromFile(string filePath) // change filePath depending on what to load
    {
        phrases.Clear(); // clear phrases
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            phrases.AddRange(lines);
        }
        else
        {
            Debug.LogError("Phrase file not found at " + filePath);
        }
    }

    private IEnumerator TypeText(string phrase)
    {
        textUI.text = ""; // clear text
        foreach (char c in phrase)
        {
            textUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(3);

        textUI.text = "";

        typingCoroutine = null;
    }
}
