using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel, mainButtons;
    public void PlayGame()
    {
        SceneManager.LoadScene("game");
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
