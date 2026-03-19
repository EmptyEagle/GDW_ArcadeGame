using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isPressed;
    public Color pressedColor;
    public Color unpressedColor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().color = unpressedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButtonState(bool state)
    {
        isPressed = state;
        if (isPressed)
        {
            GetComponent<SpriteRenderer>().color = pressedColor;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = unpressedColor;
        }
    }
}
