using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Slider energySlider;
    public int maxEnergy = 100;
    public int currentEnergy;

    void Start()
    {
        currentEnergy = maxEnergy;     // full energy in the beginning
        UpdateEnergyUI();
    }

    public bool UseEnergy(int amount)
    {
        // check if there is enough energy to perform an action
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;   
            UpdateEnergyUI();          // update gauge
            return true;               // we can call function
        }
        else
        {
            Debug.Log("Not enough energy!"); // testing
            return false;
        }
    }

    private void UpdateEnergyUI()
    {
        energySlider.value = currentEnergy;
    }

    public void AddEnergy(int amount)
    {
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        UpdateEnergyUI();
    }
}
