using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour
{
    private float startPosY;
    private float gateHeight = 3;
    private float currentVerticalPos;
    private float gateSpeed = 0.05f;
    
    // Typically all required buttons need to be pressed for the gate to open
    public GameObject[] requiredButtons;
    // Alternatively, all override button(s) combined force the gate to be open
    public GameObject[] overrideButtons;
    // Only one of the required buttons is required to be pressed to open the gate if this is true, otherwise all required buttons need to be pressed
    public bool onlyOneRequired;
    private bool buttonRequirementMet;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Start with a success for the sake of returning it later if nothing changes
        buttonRequirementMet = true;
        
        // Loop through the override buttons to check if all are pressed
        for (int i = 0; i < overrideButtons.Length; i++)
        {
            Button curButtonCheck = overrideButtons[i].GetComponent<Button>();
            // If the the button requirement isn't met at this point, make sure all the override buttons are pressed
            if (!curButtonCheck.isPressed)
            {
                Debug.Log("Override button [" + overrideButtons[i].name + "] not pressed.");
                buttonRequirementMet = false;
                break;
            }
        }
        
        // Loop through the typically required buttons to check if all are pressed
        for (int i = 0; i < requiredButtons.Length; i++)
        {
            Button curButtonCheck = requiredButtons[i].GetComponent<Button>();
            // There's no need to check for required buttons if the button requirements have been met by the override
            if (i == 0 && buttonRequirementMet && overrideButtons.Length > 0)
            {
                Debug.Log("Override button(s) pressed. Skipping required button check.");
                break;
            }
            // If the button requirements haven't been met by the override, restart the sequence by setting the requirement to met and looping through the required buttons
            else if (i == 0)
            {
                buttonRequirementMet = true;
                Debug.Log("Override button(s) not pressed or none are present. Starting required button check.");
            }
            // If only one button is required to be pressed, loop through all the buttons to see if one is pressed
            if (onlyOneRequired && curButtonCheck.isPressed)
            {
                Debug.Log("Required button [" + requiredButtons[i].name + "] pressed. ONLY ONE REQUIRED.");
                buttonRequirementMet = true;
                break;
            }
            else if (onlyOneRequired)
            {
                Debug.Log("Required button [" + requiredButtons[i].name + "] not pressed. ONLY ONE REQUIRED.");
                buttonRequirementMet = false;
            }
            // Else, if a required button is not pressed, break out of the loop with a failure
            else if (!curButtonCheck.isPressed)
            {
                Debug.Log("Required button [" + requiredButtons[i].name + "] not pressed.");
                buttonRequirementMet = false;
                break;
            }
            else if (curButtonCheck.isPressed)
            {
                Debug.Log("Required button [" + requiredButtons[i].name + "] pressed.");
            }
        }
        
        if (buttonRequirementMet)
        {
            InitiateOpenGate();
        }
        else
        {
            InitiateCloseGate();
        }
    }

    // Call this method from the button script to open the gate
    public void InitiateOpenGate()
    {
        if (startPosY + gateHeight > currentVerticalPos)
        {
            StopAllCoroutines();
            StartCoroutine(OpenGate());
        }
    }
    
    // Call this method from the button script to close the gate
    public void InitiateCloseGate()
    {
        if (startPosY < currentVerticalPos)
        {
            StopAllCoroutines();
            StartCoroutine(CloseGate());
        }
    }

    IEnumerator OpenGate()
    {
        while (startPosY + gateHeight > currentVerticalPos)
        {
            transform.Translate(Vector3.up * gateSpeed);
            currentVerticalPos = transform.position.y;
            yield return new WaitForSeconds(0.02f);
            //Debug.Log("Opening gate");
        }
        //Debug.Log("Gate opened");
    }
    
    IEnumerator CloseGate()
    {
        while (startPosY < currentVerticalPos)
        {
            transform.Translate(Vector3.down * gateSpeed);
            currentVerticalPos = transform.position.y;
            yield return new WaitForSeconds(0.005f);
            //Debug.Log("Closing gate");
        }
        //Debug.Log("Gate closed");
    }
}
