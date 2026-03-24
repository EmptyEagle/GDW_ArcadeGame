using System.Collections;
using UnityEngine;

public class Piston : MonoBehaviour
{
    private Vector3 startPos;
    public float targetHeightY;
    private float currentVerticalPos;
    public GameObject pistonCylinder;
    private Rigidbody2D pistonCylinderRb;

    public enum PistonFacing
    {
        Up,
        Down,
        Left,
        Right
    }

    public PistonFacing pistonFacing;
    
    // Typically all required buttons need to be pressed for the elevator to activate
    public GameObject[] requiredButtons;
    // Alternatively, all override button(s) combined force the elevator to activate
    public GameObject[] overrideButtons;
    // Only one of the required buttons is required to be pressed for the elevator to activate if this is true, otherwise all required buttons need to be pressed
    public bool onlyOneRequired;
    public bool buttonRequirementMet;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = pistonCylinder.transform.position;
        pistonCylinderRb = pistonCylinder.GetComponent<Rigidbody2D>();
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
                //Debug.Log("Override button [" + overrideButtons[i].name + "] not pressed.");
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
                //Debug.Log("Override button(s) pressed. Skipping required button check.");
                break;
            }
            // If the button requirements haven't been met by the override, restart the sequence by setting the requirement to met and looping through the required buttons
            else if (i == 0)
            {
                buttonRequirementMet = true;
                //Debug.Log("Override button(s) not pressed or none are present. Starting required button check.");
            }
            // If only one button is required to be pressed, loop through all the buttons to see if one is pressed
            if (onlyOneRequired && curButtonCheck.isPressed)
            {
                //Debug.Log("Required button [" + requiredButtons[i].name + "] pressed. ONLY ONE REQUIRED.");
                buttonRequirementMet = true;
                break;
            }
            else if (onlyOneRequired)
            {
                //Debug.Log("Required button [" + requiredButtons[i].name + "] not pressed. ONLY ONE REQUIRED.");
                buttonRequirementMet = false;
            }
            // Else, if a required button is not pressed, break out of the loop with a failure
            else if (!curButtonCheck.isPressed)
            {
                //Debug.Log("Required button [" + requiredButtons[i].name + "] not pressed.");
                buttonRequirementMet = false;
                break;
            }
            else if (curButtonCheck.isPressed)
            {
                //Debug.Log("Required button [" + requiredButtons[i].name + "] pressed.");
            }
        }
        
        if (buttonRequirementMet)
        {
            InitiateActuatePiston();
        }
        else
        {
            InitiateDeactuatePiston();
        }
    }
    
    private void InitiateActuatePiston()
    {
        switch (pistonFacing)
        {
            case PistonFacing.Up:
                pistonCylinderRb.MovePosition(new Vector3(pistonCylinder.transform.position.x, startPos.y + targetHeightY, pistonCylinder.transform.position.z));
                break;
            case PistonFacing.Down:
                pistonCylinderRb.MovePosition(new Vector3(pistonCylinder.transform.position.x, startPos.y - targetHeightY, pistonCylinder.transform.position.z));
                break;
            case PistonFacing.Left:
                pistonCylinderRb.MovePosition(new Vector3(startPos.x - targetHeightY, pistonCylinder.transform.position.y, pistonCylinder.transform.position.z));
                break;
            case PistonFacing.Right:
                pistonCylinderRb.MovePosition(new Vector3(startPos.x + targetHeightY, pistonCylinder.transform.position.y, pistonCylinder.transform.position.z));
                break;
        }
    }

    private void InitiateDeactuatePiston()
    {
        switch (pistonFacing)
        {
            case PistonFacing.Up:
                pistonCylinderRb.MovePosition(new Vector3(pistonCylinder.transform.position.x, startPos.y, pistonCylinder.transform.position.z));
                break;
            case PistonFacing.Down:
                pistonCylinderRb.MovePosition(new Vector3(pistonCylinder.transform.position.x, startPos.y, pistonCylinder.transform.position.z));
                break;
            case PistonFacing.Left:
                pistonCylinderRb.MovePosition(new Vector3(startPos.x, pistonCylinder.transform.position.y, pistonCylinder.transform.position.z));
                break;
            case PistonFacing.Right:
                pistonCylinderRb.MovePosition(new Vector3(startPos.x, pistonCylinder.transform.position.y, pistonCylinder.transform.position.z));
                break;
        }
    }
}
