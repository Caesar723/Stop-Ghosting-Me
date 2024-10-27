using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class General_Button : MonoBehaviour
{
    public EnergyManager energyManager;
    public DayManager gameManager;
    
    private bool isPressed = false;
    public Sprite standardSprite;
    public Sprite pressedSprite;
    public UnityEvent onClick; // for the event
    public int energyCost; // cost of using the button
    public float yChange; // to make buttonpress look okay

    public AudioClip clickSound;
    public AudioClip releaseSound;
    public AudioClip errorSound;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standardSprite;
    }

    private void OnMouseEnter()
    {
        if (Time.timeScale == 0) return;

        if (!gameManager.isTransitioning)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(207, 207, 207, 255);
        }
    }

    private void OnMouseExit()
    {
        if (Time.timeScale == 0) return;

        if (!gameManager.isTransitioning)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void OnMouseDown()
    {
        if (Time.timeScale == 0) return;

        if (!isPressed && !gameManager.isTransitioning)
        {
            if (energyManager.UseEnergy(energyCost))
            {
                spriteRenderer.sprite = pressedSprite;
                onClick.Invoke(); // in inspector, now there is onClick event for each button, we can add whatever we need to run through there 
                isPressed = true;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - yChange);

                AudioManager.instance.PlayEnvironmentSound(clickSound); // *click* sound
            }
            else
            {
                AudioManager.instance.PlayEnvironmentSound(errorSound); // error sound (vibration or something)
            }
        }
    }

    public void OnMouseUp()
    {
        if (Time.timeScale == 0) return;

        if (isPressed && !gameManager.isTransitioning)
        {
            isPressed = false;
            spriteRenderer.sprite = standardSprite;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + yChange);

            AudioManager.instance.PlayEnvironmentSound(releaseSound); // *click v2* sound here
        }

    }

}
