using System.Collections;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int day=1;
    public int money;
    public int timer=0;
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
        isTransitioning = true; // flag to block interactions

        // day transition UI (!!!! BEFORE THIS, DON'T FORGET TO CLOSE ALL THE CAMERA WINDOWS (ONLY THE MAIN ONE SHOULD BE KEPT !!!!)
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
            if (money > 0) // change 0 to what we need (calculate it later during testing)
            {
                // ending 1
            }
            else
            {
                // ending 2
            }
        }

        isTransitioning = false; // allow interactions
    }
}
