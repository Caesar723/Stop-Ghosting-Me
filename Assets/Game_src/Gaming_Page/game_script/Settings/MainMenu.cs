using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel, mainButtons;
    public Image fadePanel;


    private void Start()
    {
        fadePanel.gameObject.SetActive(false);
    }

    public void StartGame() // otherwise won't work
    {
        StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        fadePanel.gameObject.SetActive(true);
        float elapsed = 0f;
        Color panelColor = fadePanel.color;

        while (elapsed < 1.5f)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsed / 1.5f);
            fadePanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);
            yield return null;
        }
        fadePanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, 1);
        
        SceneManager.LoadScene("Beginning"); // replace with the game scene later
    }

    public void OpenSettings()
    {
        if (settingsPanel.active)
        {
            settingsPanel.SetActive(false);
            mainButtons.SetActive(true);
        }
        else
        {
            settingsPanel.SetActive(true);
            mainButtons.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
