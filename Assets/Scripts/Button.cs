using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isPressed;
    public Color pressedColor;
    public Color unpressedColor;
    public int numberOfObjectsPressing;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetComponent<SpriteRenderer>().color = unpressedColor;
        numberOfObjectsPressing = 0;
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
            //GetComponent<SpriteRenderer>().color = pressedColor;
            Debug.Log("Button "+gameObject.name+" pressed");
        }
        else
        {
            //GetComponent<SpriteRenderer>().color = unpressedColor;
            Debug.Log("Button "+gameObject.name+" unpressed");
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ButtonWeightCollision"))
        {
            numberOfObjectsPressing++;
            if (numberOfObjectsPressing > 0)
            {
                SetButtonState(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // The following code runs once when the object leaves the button trigger, and two more times after that
        if (other.gameObject.CompareTag("ButtonWeightCollision"))
        {
            numberOfObjectsPressing--;
            if (numberOfObjectsPressing < 1)
            {
                SetButtonState(false);
            }
        }
    }
}
