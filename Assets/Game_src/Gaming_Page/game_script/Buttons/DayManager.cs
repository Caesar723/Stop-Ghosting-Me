using UnityEngine;

public class DayManager : MonoBehaviour
{
    int day;
    int money;
    public EnergyManager energyManager;
    void Start()
    {
        day = 1;
        money = 0;
    }

    void Update()
    {
        
    }

    public void DayChange() // we need to call it when the day changes (for example, after 6 characters where checked)
    {
        if (day < 6)
        {
            day++;
            //day change mechanic (probably just fade in and fadeout screen, maybe also stop playing music)
            money += energyManager.currentEnergy * 1; // instead of 1, energy to money multiplier
            energyManager.AddEnergy(100); // I already made sure that energy won't go over 100
        }
        else
        {
            // logic of ending the game
            if (money > 0) // change 0 to what we want
            {
                // ending 1
            }
            else
            {
                // ending 2
            }
        }
    }
}
