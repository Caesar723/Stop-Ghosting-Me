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
    [SerializeField] public AudioClip typeSound;
    bool isTypingSoundPlaying = false;
    float elapsedTime = 0;
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
                        filePath = "human_temperature_phrases";
                        break;
                    case "monster":
                        filePath = "monster_temperature_phrases";
                        break;
                    case "neutral":
                        filePath = "neutral_temperature_phrases";
                        break;
                    default:
                        filePath = "neutral_temperature_phrases";
                        break;
                }
                break;
            case "sound":
                switch (textType_sound)
                {
                    case "human":
                        filePath = "human_sound_phrases";
                        break;
                    case "monster":
                        filePath = "monster_sound_phrases";
                        break;
                    case "neutral":
                        filePath = "neutral_sound_phrases";
                        break;
                    default:
                        filePath = "neutral_sound_phrases";
                        break;
                }
                break;
            case "light":
                switch (textType_light)
                {
                    case "human":
                        filePath = "human_light_phrases";
                        break;
                    case "monster":
                        filePath = "monster_light_phrases";
                        break;
                    case "neutral":
                        filePath = "neutral_light_phrases";
                        break;
                    default:
                        filePath = "neutral_light_phrases";
                        break;
                }
                break;
            default:
                filePath = "neutral_sound_phrases";
                break;
        }
        LoadPhrasesFromResources(filePath);
        if (phrases.Count > 0)
        {
            string randomPhrase = phrases[Random.Range(0, phrases.Count)];

            // stop the previous phrase if it is still running
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                AudioManager.instance.environmentSource.Stop(); // Stop any ongoing typing sound
            }

            // start the new typing
            typingCoroutine = StartCoroutine(TypeText(randomPhrase));
        }
    }

    private void LoadPhrasesFromResources(string fileName)
    {
        phrases.Clear();
        TextAsset textFile = Resources.Load<TextAsset>(fileName);
        if (textFile != null)
        {
            string[] lines = textFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
            phrases.AddRange(lines);
        }
        else
        {
            Debug.LogError("Phrase file not found in Resources: " + fileName);
        }
    }

    private IEnumerator TypeText(string phrase)
    {
        textUI.text = "";
        isTypingSoundPlaying = true;
        elapsedTime = 0f;
        AudioManager.instance.PlayEnvironmentSound(typeSound);

        foreach (char c in phrase)
        {
            textUI.text += c;
            elapsedTime += typingSpeed;
            if (elapsedTime >= 6.24f)
            {
                AudioManager.instance.environmentSource.Stop();
                isTypingSoundPlaying = false;
            }
            yield return new WaitForSeconds(typingSpeed);
        }

        AudioManager.instance.environmentSource.Stop();
        isTypingSoundPlaying = false;
        yield return new WaitForSeconds(3);
        textUI.text = "";
        typingCoroutine = null;
    }
}
