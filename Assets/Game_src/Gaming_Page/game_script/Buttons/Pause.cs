using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the settings panel GameObject
    private bool isPaused = false;    // To track the pause state

    private void Start()
    {
        // Ensure the settings panel is initially inactive
        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        if (settingsPanel.activeSelf) // Check if the settings panel is active
        {
            settingsPanel.SetActive(false); // Close settings panel
            ResumeGame(); // Resume the game
        }
        else
        {
            settingsPanel.SetActive(true); // Open settings panel
            PauseGame(); // Pause the game
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        isPaused = true; // Update the pause state
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game by resetting time scale
        isPaused = false; // Update the pause state
    }
}
