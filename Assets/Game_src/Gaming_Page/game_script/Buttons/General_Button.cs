using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class General_Button : MonoBehaviour
{
    private bool isPressed = false;
    public Sprite standardSprite;
    public Sprite pressedSprite;
    public UnityEvent onClick; // for the event

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standardSprite;
    }

    void Update()
    {

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
        if (!isPressed)
        {
            spriteRenderer.sprite = pressedSprite;
            onClick.Invoke(); // In inspector, now there is onClick event for each button, we can add whatever we need to run through there 
            isPressed = true;
        }
    }

    public void OnMouseUp()
    {
        isPressed = false;
        spriteRenderer.sprite = standardSprite;
    }

}
