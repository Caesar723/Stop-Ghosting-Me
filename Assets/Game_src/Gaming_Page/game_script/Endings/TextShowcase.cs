using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TextShowcase : MonoBehaviour
{
    [TextArea]
    public string[] endingTexts;
    public float typingSpeed = 0.05f;
    public TextMeshProUGUI uiTextMeshPro;

    public AudioClip typingSound;
    private bool isFinishedTyping = false;
    private bool isTypingSoundPlaying = false;

    private int currentPage = 0;
    float elapsedTime = 0;

    private void Awake()
    {
        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        if (uiTextMeshPro != null)
        {
            uiTextMeshPro.text = "";
        }

        while (currentPage < endingTexts.Length)
        {

            foreach (char letter in endingTexts[currentPage])
            {
                if (uiTextMeshPro != null)
                {
                    uiTextMeshPro.text += letter;
                }

                if (!isTypingSoundPlaying)
                {
                    isTypingSoundPlaying = true;
                    AudioManager.instance.PlayEnvironmentSound(typingSound);
                }

                yield return new WaitForSeconds(typingSpeed);
            }
            if (currentPage + 1 < endingTexts.Length)
            {
                AudioManager.instance.environmentSource.Stop();
                isTypingSoundPlaying = false;
                yield return new WaitForSeconds(2);
                uiTextMeshPro.text = "";
            }
            currentPage++;
        }
        isFinishedTyping = true;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 6.24)
        {
            elapsedTime = 0;
            isTypingSoundPlaying = false;
        }

        if (isFinishedTyping && Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainMenu();
        }

        if (isFinishedTyping && isTypingSoundPlaying)
        {
            AudioManager.instance.environmentSource.Stop();
            isTypingSoundPlaying = false;
        }
    }

    private void LoadMainMenu()
    {
        if (SceneManager.GetActiveScene().name != "Beginning") SceneManager.LoadScene("Main Menu 1");
        else SceneManager.LoadScene("game_finally");
    }
}