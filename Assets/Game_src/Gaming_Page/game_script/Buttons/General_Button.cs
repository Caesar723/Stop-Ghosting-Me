using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class General_Button : MonoBehaviour
{
    public EnergyManager energyManager;
    private bool isPressed = false;
    public Sprite standardSprite;
    public Sprite pressedSprite;
    public UnityEvent onClick; // for the event
    public int energyCost; // cost of using the button
    public float yChange; // to make buttonpress look okay

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standardSprite;
    }

    private void OnMouseEnter()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(207, 207, 207, 255);
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }

    public void OnMouseDown()
    {
        if (!isPressed && energyManager.UseEnergy(energyCost))
        {
            spriteRenderer.sprite = pressedSprite;
            onClick.Invoke(); // Iin inspector, now there is onClick event for each button, we can add whatever we need to run through there 
            isPressed = true;
            this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - yChange);
            
            // sound of pressing the button here
        }
        else
        {
            // sound of not being able to perform an action here
        }
    }

    public void OnMouseUp()
    {
        if (isPressed)
        {
            isPressed = false;
            spriteRenderer.sprite = standardSprite;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + yChange);

            // sound of unpressing button here
        }

    }

}
