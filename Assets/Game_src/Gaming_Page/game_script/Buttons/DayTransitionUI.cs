using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DayTransitionUI : MonoBehaviour
{
    public Image fadePanel;           
    public TextMeshProUGUI dayText; 
    public float fadeDuration = 1.5f;   // duration of the fade in and fade out

    public IEnumerator DayTransition(int day)
    {
        fadePanel.gameObject.SetActive(true);
        dayText.text = "Day " + day;
        dayText.color = new Color(255, 255, 255, 0);
       if (day != 1) fadePanel.color = new Color(0, 0, 0, 0);

        // fade in
        yield return StartCoroutine(Fade(0, 1));

        // wait
        yield return new WaitForSeconds(1f);

        // fade out
        yield return StartCoroutine(Fade(1, 0));

        fadePanel.gameObject.SetActive(false);
    }

    public IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color panelColor = fadePanel.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            if (fadePanel.color.a != endAlpha) fadePanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);
            dayText.color = new Color(255, 255, 255, alpha);
            yield return null;
        }
        dayText.color = new Color(255, 255, 255, endAlpha);
        fadePanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, endAlpha);
    }
}
