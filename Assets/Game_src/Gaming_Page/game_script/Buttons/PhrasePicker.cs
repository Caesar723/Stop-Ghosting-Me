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
    public string button_type;
    public Character_apperance character;

    private List<string> phrases = new List<string>();
    private Coroutine typingCoroutine;

    public void Start()
    {
        character = FindObjectOfType<Character_apperance>(); // if there can be more then one at a time (which shouldn't be the case?), needs to change 
    }

    public void DisplayRandomPhraseFromFile(string button_type)
    {
        string filePath;
        string textType_temperature = character.textType_temperature;
        string textType_sound = character.textType_sound;
        string textType_light = character.textType_light;
        switch (button_type)
        {
            case "temperature":
                switch (textType_temperature)
                {
                    case "human":
                        filePath = "Assets/Resources/human_temperature_phrases.txt";
                        break;
                    case "monster":
                        filePath = "Assets/Resources/monster_temperature_phrases.txt";
                        break;
                    case "neutral":
                        filePath = "Assets/Resourcesneutral_temperature_phrases.txt";
                        break;
                    default:
                        filePath = "Assets/Resources/neutral_temperature_phrases.txt";
                        break;
                }
                break;
            case "sound":
                switch (textType_sound)
                {
                    case "human":
                        filePath = "Assets/Resources/human_sound_phrases.txt";
                        break;
                    case "monster":
                        filePath = "Assets/Resources/monster_sound_phrases.txt";
                        break;
                    case "neutral":
                        filePath = "Assets/Resources/neutral_sound_phrases.txt";
                        break;
                    default:
                        filePath = "Assets/Resources/neutral_sound_phrases.txt";
                        break;
                }
                break;
            case "light":
                switch (textType_light)
                {
                    case "human":
                        filePath = "Assets/Resources/human_light_phrases.txt";
                        break;
                    case "monster":
                        filePath = "Assets/Resources/monster_light_phrases.txt";
                        break;
                    case "neutral":
                        filePath = "Assets/Resources/neutral_light_phrases.txt";
                        break;
                    default:
                        filePath = "Assets/Resources/neutral_light_phrases.txt";
                        break;
                }
                break;
            default:
                filePath = "Assets/Resources/neutral_sound_phrases.txt";
                break;
        }
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
