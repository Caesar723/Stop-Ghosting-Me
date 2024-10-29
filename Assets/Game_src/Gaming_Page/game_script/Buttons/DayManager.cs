using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public int day=1;
    public int money;
    public float timer=0;
    public int ghosting=0;
    public EnergyManager energyManager;

    public DayTransitionUI dayTransitionUI;

    public bool isTransitioning = false;

    void Start()
    {
        day = 1;
        money = 0;
        StartCoroutine(HandleDayChange());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 90)
        {
            SceneManager.LoadScene("Ending3");
        }
        if (ghosting == 6)
        {
            SceneManager.LoadScene("Ending4");
        }
    }

    public void DayChange()
    {
        if (!isTransitioning) // change the day if no transition is in progress
        {
            StartCoroutine(HandleDayChange());
        }
    }

    private IEnumerator HandleDayChange()
    {
        timer = 0;
        isTransitioning = true; // flag to block interactions

        // day transition UI
        yield return dayTransitionUI.DayTransition(day);

        if (day < 6)
        {
            day++;
            // day change logic from here
            money += energyManager.currentEnergy * 1; // replace 1 with an energy-to-money multiplier
            energyManager.AddEnergy(100); // energy won't exceed 100
        }
        else
        {
            // end game logic
            if (money > 250) // change 0 to what we need (calculate it later during testing)
            {
                SceneManager.LoadScene("Ending1");
            }
            else
            {
                SceneManager.LoadScene("Ending2");
            }
        }
        timer = 0;
        ghosting = 0;
        isTransitioning = false; // allow interactions
    }
}
