using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class EnergyShower: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public EnergyManager energyManager;
    private TextMeshProUGUI energyText;
    private string defaultText = "Energy";

    void Start()
    {
        energyText = GetComponent<TextMeshProUGUI>();
        energyText.text = defaultText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (energyManager != null)
        {
            energyText.text = energyManager.currentEnergy + "/100";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        energyText.text = defaultText;
    }
}