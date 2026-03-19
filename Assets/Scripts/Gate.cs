using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour
{
    private float startPosY;
    private float gateHeight = 3;
    private float currentVerticalPos;
    private float gateSpeed = 0.05f;
    
    // Typically all required buttons need to be pressed for the gate to open
    // CURRENTLY ONLY THE FIRST INDEX BUTTON OPENS THE GATE
    public GameObject[] requiredButtons;
    // Alternatively, all override button(s) combined force the gate to be open
    public GameObject[] overrideButtons;
    // Only one of the required buttons is required to be pressed to open the gate if this is true, otherwise all required buttons need to be pressed
    // CURRENTLY NOT WORKING
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
                buttonRequirementMet = false;
                break;
            }
        }
        
        // Loop through the typically required buttons to check if all are pressed
        for (int i = 0; i < requiredButtons.Length; i++)
        {
            Button curButtonCheck = requiredButtons[i].GetComponent<Button>();
            // There's no need to check for required buttons if the button requirements have been met by the override
            if (buttonRequirementMet && overrideButtons.Length > 0)
            {
                break;
            }
            // If the button requirements haven't been met by the override, restart the sequence by setting the requirement to met and looping through the required buttons
            else
            {
                buttonRequirementMet = true;
            }
            // If only one button is required to be pressed, break out of the loop with a success
            if (onlyOneRequired && curButtonCheck.isPressed)
            {
                break;
            }
            // Else, if a required button is not pressed, break out of the loop with a failure
            else if (!curButtonCheck.isPressed)
            {
                buttonRequirementMet = false;
                break;
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
